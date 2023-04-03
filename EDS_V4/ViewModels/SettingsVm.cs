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
        private const string configeFileName = "EDS_settings.xml";
        private XmlHandler<Configurables> xmlhandler;
        private Configurables defaults;
        private Configurables configurables;

        
        public string AdminFileLocation { get { ReadConfigFromXml(); return configurables.AdminFileLocation; } set { this.RaiseAndSetIfChanged(ref configurables.AdminFileLocation, value); } }
        public string SaveFileLocation { get => configurables.SaveFileLocation; set => this.RaiseAndSetIfChanged(ref configurables.SaveFileLocation, value); }

        public int StartRow { get => configurables.StartRow; set => this.RaiseAndSetIfChanged(ref configurables.StartRow, value); }
        public int FirstHalfSize { get => configurables.FirstHalfSize; set => this.RaiseAndSetIfChanged(ref configurables.FirstHalfSize, value); }
        public int HomeColumn { get => configurables.HomeColumn; set => this.RaiseAndSetIfChanged(ref configurables.HomeColumn, value); }
        public int OutColumn { get => configurables.OutColumn; set => this.RaiseAndSetIfChanged(ref configurables.OutColumn, value); }
        public int HalfWayJump { get => configurables.HalfWayJump; set => this.RaiseAndSetIfChanged(ref configurables.HalfWayJump, value); }
        public int HostSheet { get => configurables.HostSheet; set => this.RaiseAndSetIfChanged(ref configurables.HostSheet, value); }
        public int RankingSheet { get => configurables.RankingSheet; set => this.RaiseAndSetIfChanged(ref configurables.RankingSheet, value); }
        public int TopscorersSheet { get => configurables.TopscorersSheet; set => this.RaiseAndSetIfChanged (ref configurables.TopscorersSheet, value); }


        public SettingsVm()
        {
            var savefilelocation = "C:\\Users\\Harm\\OneDrive\\Documenten\\Sport\\Poules\\EDS Poule Docs\\2022-2023\\EDS2223";
            var adminloc = "C:\\Users\\Harm\\OneDrive\\Documenten\\Sport\\Poules\\EDS Poule Docs\\2022-2023\\EDS Poule Admin 2022-2023.xlsx";
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
        }

        public void SaveSettingsCommand()
        {
            WriteConfigToXml();
        }

        private void WriteConfigToXml()
        {
            xmlhandler.WriteToXml(configeFileName, configurables, overwrite: true);
            ConfigurablesToConfigurations();
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
