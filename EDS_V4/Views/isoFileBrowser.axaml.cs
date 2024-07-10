using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
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
            if(res != "")
                BrowserResult = res;
            browsebtn.IsEnabled = true;
        }

        private async Task<string[]> GetPaths()
        {
            if (BrowseForDirectory)
            {
                OpenFolderDialog dialog = new OpenFolderDialog();
                dialog.Directory = Path.GetDirectoryName(EDS_V4.Code.GeneralConfiguration.AdminFileLocation);
                if (string.IsNullOrEmpty(dialog.Directory) || !Directory.Exists(dialog.Directory))
                    dialog.Directory = @"C:";
                return new string[] { await dialog.ShowAsync(new Window()) };
            }

            else
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Directory = Path.GetDirectoryName(EDS_V4.Code.GeneralConfiguration.AdminFileLocation);
                if (string.IsNullOrEmpty(dialog.Directory) || !Directory.Exists(dialog.Directory))
                    dialog.Directory = @"C:";
                dialog.AllowMultiple = true;
                if (!string.IsNullOrEmpty(FileType))
                    dialog.Filters.Add(new FileDialogFilter() { Name = "Filter", Extensions = { FileType } });
                return await dialog.ShowAsync(new Window());
            }
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
    }
}
