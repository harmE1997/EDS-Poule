using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace EDS_V4.Views
{
    public partial class isoFileBrowser : UserControl
    {
        private static char seperatorChar = ';';

        public bool BrowseForDirectory { get; set; }
        public static readonly StyledProperty<bool> BrowseForDirectoryProperty = AvaloniaProperty.Register<isoFileBrowser, bool>(nameof(BrowseForDirectory), defaultValue: false);

        private string browserresult;
        public string BrowserResult
        {
            get { return browserresult; }
            set
            {
                SetAndRaise(BrowserResultProperty, ref browserresult, value);
                this.FindControl<TextBox>("tbOutput").Text = value;
            }
        }
        public static readonly DirectProperty<isoFileBrowser, string> BrowserResultProperty = AvaloniaProperty.RegisterDirect<isoFileBrowser, string>(nameof(BrowserResult), o => o.BrowserResult, (o, v) => o.BrowserResult = v, defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);

        public string FileType { get; set; }
        public static readonly StyledProperty<string> FileTypeProperty = AvaloniaProperty.Register<isoFileBrowser, string>(nameof(FileType), defaultValue: "");

        public isoFileBrowser()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public async void Browse(object sender, RoutedEventArgs e)
        {
            string[] paths = new string[] { };
            var browsebtn = this.FindControl<Button>("btnBrowse");
            browsebtn.IsEnabled = false;
            paths = await GetPaths();
            var res = filearraytostring(paths);
            if (res != "")
                BrowserResult = res;
            browsebtn.IsEnabled = true;
        }

        private async Task<string[]> GetPaths()
        {
            var topLevel = TopLevel.GetTopLevel(this);
            var directory = Path.GetDirectoryName(EDS_V4.Code.GeneralConfiguration.AdminFileLocation);
            List<string> results = new();
            if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory))
                directory = @"C:";

            if (BrowseForDirectory)
            {
                var options = new FolderPickerOpenOptions() { SuggestedStartLocation = topLevel.StorageProvider.TryGetFolderFromPathAsync(directory).Result, AllowMultiple = false };
                var res = await topLevel.StorageProvider.OpenFolderPickerAsync(options);
                foreach (var result in res)
                    results.Add(result.Path.LocalPath);
            }

            else
            {
                var options = new FilePickerOpenOptions() { SuggestedStartLocation = topLevel.StorageProvider.TryGetFolderFromPathAsync(directory).Result, AllowMultiple = true, FileTypeFilter = new[] { Excel } };
                var res = await topLevel.StorageProvider.OpenFilePickerAsync(options);
                foreach (var result in res)
                    results.Add(result.Path.LocalPath);
            }

            return results.ToArray();
        }

        private string filearraytostring(string[] files)
        {
            if (files == null)
                return "";

            string output = "";
            foreach (string file in files)
            {
                output += file + seperatorChar;
            }

            if (output != "")
                output = output.Remove(output.LastIndexOf(seperatorChar));

            return output;
        }

        public static FilePickerFileType Excel { get; } = new("Exccel Files")
        {
            Patterns = new[] { "*.xls", "*.xlsx" }
        };
    }
}
