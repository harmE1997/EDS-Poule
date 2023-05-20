using EDS_V4.Code;
using ReactiveUI;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EDS_V4.ViewModels
{
    public class Configurables
    {
        public string SaveFileLocation = "";
        public string AdminFileLocation = "";
        public int StartRow;
        public int FirstHalfSize;
        public int HomeColumn;
        public int OutColumn;
        public int HalfWayJump;
        public int HostSheet;
        public int RankingSheet;
        public int TopscorersSheet;
    }
    public class SettingsVm : ViewModelBase
    {
        public static event SettingsEventHandler? SettingsEvent;
        public delegate void SettingsEventHandler();

        private const string configeFileName = "EDS_settings.xml";
        private XmlHandler<Configurables> xmlhandler;
        private Configurables defaults;
        private Configurables configurables;


        public string AdminFileLocation { get { ReadConfigFromXml(); return configurables.AdminFileLocation; } set { this.RaiseAndSetIfChanged(ref configurables.AdminFileLocation, value); SaveCommandEnabled = true; } }
        public string SaveFileLocation { get => configurables.SaveFileLocation; set { this.RaiseAndSetIfChanged(ref configurables.SaveFileLocation, value); SaveCommandEnabled = true; } }

        public int StartRow { get => configurables.StartRow; set { this.RaiseAndSetIfChanged(ref configurables.StartRow, value); SaveCommandEnabled = true; } }
        public int FirstHalfSize { get => configurables.FirstHalfSize; set { this.RaiseAndSetIfChanged(ref configurables.FirstHalfSize, value); SaveCommandEnabled = true; } }
        public int HomeColumn { get => configurables.HomeColumn; set { this.RaiseAndSetIfChanged(ref configurables.HomeColumn, value); SaveCommandEnabled = true; } }
        public int OutColumn { get => configurables.OutColumn; set { this.RaiseAndSetIfChanged(ref configurables.OutColumn, value); SaveCommandEnabled = true; } }
        public int HalfWayJump { get => configurables.HalfWayJump; set { this.RaiseAndSetIfChanged(ref configurables.HalfWayJump, value); SaveCommandEnabled = true; } }
        public int HostSheet { get => configurables.HostSheet; set { this.RaiseAndSetIfChanged(ref configurables.HostSheet, value); SaveCommandEnabled = true; } }
        public int RankingSheet { get => configurables.RankingSheet; set { this.RaiseAndSetIfChanged(ref configurables.RankingSheet, value); SaveCommandEnabled = true; } }
        public int TopscorersSheet { get => configurables.TopscorersSheet; set { this.RaiseAndSetIfChanged (ref configurables.TopscorersSheet, value); SaveCommandEnabled = true; } }

        private bool savecommandenabled;
        public bool SaveCommandEnabled { get => savecommandenabled; set => this.RaiseAndSetIfChanged(ref savecommandenabled, value); }

        public SettingsVm()
        {
            var savefilelocation = "C:\\Users\\Harm\\OneDrive\\Documenten\\Sport\\Poules\\EDS Poule Docs\\2023-2024\\EDS2324";
            var adminloc = "C:\\Users\\Harm\\OneDrive\\Documenten\\Sport\\Poules\\EDS Poule Docs\\2023-2024\\EDS Poule Admin 2023-2024.xlsx";
            xmlhandler = new XmlHandler<Configurables>();
            defaults = new Configurables()
            {
                SaveFileLocation = savefilelocation,
                AdminFileLocation = adminloc,
                StartRow = 12,
                FirstHalfSize = 17,          
                HomeColumn = 7,
                OutColumn = 8,
                HalfWayJump = 10,
                HostSheet = 6,
                RankingSheet = 2,
                TopscorersSheet = 8
            };
            configurables = new();
            ReadConfigFromXml();
            SaveCommandEnabled = false;
            SettingsEvent?.Invoke();
        }

        public void SaveSettingsCommand()
        {
            WriteConfigToXml();
            SaveCommandEnabled = false;
        }

        private void WriteConfigToXml()
        {
            xmlhandler.WriteToXml(configeFileName, configurables, overwrite: true);
            ConfigurablesToConfigurations();
            SettingsEvent?.Invoke();
        }

        private void ReadConfigFromXml()
        {
            if (!File.Exists(configeFileName))
            {
                configurables = defaults;
                WriteConfigToXml();
                return;
            }
            configurables = xmlhandler.ReadFromXmlFile(configeFileName);
            ConfigurablesToConfigurations();
        }

        private void ConfigurablesToConfigurations()
        {
            GeneralConfiguration.AdminFileLocation = configurables.AdminFileLocation;
            GeneralConfiguration.SaveFileLocation = configurables.SaveFileLocation;

            ExcelConfiguration.FirstHalfSize = configurables.FirstHalfSize;
            ExcelConfiguration.HalfWayJump = configurables.HalfWayJump;
            ExcelConfiguration.HomeColumn = configurables.HomeColumn;
            ExcelConfiguration.HostSheet = configurables.HostSheet;
            ExcelConfiguration.OutColumn = configurables.OutColumn;
            ExcelConfiguration.RankingSheet = configurables.RankingSheet;
            ExcelConfiguration.StartRow = configurables.StartRow;
            ExcelConfiguration.TopscorersSheet = configurables.TopscorersSheet;
        }
    }
}
