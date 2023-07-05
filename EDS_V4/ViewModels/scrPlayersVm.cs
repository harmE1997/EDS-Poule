using EDS_V4.Code;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDS_V4.ViewModels
{
    public class scrPlayersVm : ViewModelBase
    {
        private Views.TotoForm totoForm;
        private bool existingPlayer = false;
        public static PlayerManager PlayerManager { get; private set; }

        private List<string> players;
        public List<string> Players { get => players; set => this.RaiseAndSetIfChanged(ref players, value); }

        private string selectedPlayer;
        public string SelectedPlayer { get => selectedPlayer; set => this.RaiseAndSetIfChanged(ref  selectedPlayer, value); }

        public ReactiveCommand<Unit, Unit> LoadPlayerCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> RemovePlayerCommand { get; private set; }

        public scrPlayersVm()
        { 
            PlayerManager = new PlayerManager();
            SettingsVm.SettingsEvent += SettingsChangedEvent;

            var PlayerCommandsCanExecute = this.WhenAnyValue(
                x => x.SelectedPlayer,
                (a) => { return !string.IsNullOrEmpty(a); }).ObserveOn(RxApp.MainThreadScheduler);

            LoadPlayerCommand = ReactiveCommand.Create(() => { this.cmdLoadPlayer(); }, PlayerCommandsCanExecute);
            RemovePlayerCommand = ReactiveCommand.Create(() => { this.cmdRemovePlayer(); }, PlayerCommandsCanExecute);
        }

        public void NewPlayerCommand()
        { 
            totoForm = new Views.TotoForm();         
            totoForm.Closed += TotoClosedEvent;
            totoForm.Show();
        }

        private void cmdRemovePlayer()
        {
            var res = PlayerManager.RemovePlayer(SelectedPlayer);
            if (res == 0)
            {
                RefreshPlayers();
                PopupManager.ShowMessage("Player succesfully removed");
            }

            else
                PopupManager.ShowMessage("Cannot remove player. Player not existing");
        }

        private void cmdLoadPlayer()
        {   
            totoForm = new Views.TotoForm(PlayerManager.FindPlayer(SelectedPlayer));
            totoForm.Closed += TotoClosedEvent;
            existingPlayer = true;
            totoForm.Show();
        }

        private void TotoClosedEvent(object sender, EventArgs e)
        {
            if ((totoForm.DataContext as TotoFormVm).PredictionsSubmittedFlag == false)
                return;
            var player = (totoForm.DataContext as TotoFormVm).ActivePlayer;
            var res = PlayerManager.AddPlayer(player, existingPlayer);
            if (res == 0)
            {
                existingPlayer = false;
                RefreshPlayers();
                PopupManager.ShowMessage("Player succesfully Created/Saved");
            }

            else if(res == 1) { PopupManager.ShowMessage("Cannot create/save player. Invalid player"); }
            else
                PopupManager.ShowMessage("Cannot create/save player. No permission to overwrite");
        }

        private void SettingsChangedEvent()
        {
            PlayerManager.LoadPlayers();
            RefreshPlayers();
        }

        private void RefreshPlayers()
        { 
            List<string> playernames = new List<string>();
            foreach (var player in PlayerManager.Players) 
            { 
                playernames.Add(player.Name);
            }

            playernames.Sort();
            Players = playernames;
        }
    }
}
