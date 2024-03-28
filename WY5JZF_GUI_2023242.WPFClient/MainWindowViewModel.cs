using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WY5JZF_HFT_2023241.Models;

namespace WY5JZF_GUI_2023242.WPFClient
{
    class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Player> Players { get; set; }
        private Player selectedPlayer;
        private bool isPlayerVisible;

        public bool IsPlayerVisible
        {
            get { return isPlayerVisible; }
            set { isPlayerVisible = value; }
        }
        private bool isDivisionVisible;

        public bool IsDivisionVisible
        {
            get { return isDivisionVisible; }
            set { isDivisionVisible = value; }
        }
        private bool isTeamVisible;

        public bool IsTeamVisible
        {
            get { return isTeamVisible; }
            set { isTeamVisible = value; }
        }



        public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set 
            { 
                if (value != null)
                {
                    selectedPlayer = new Player()
                    {
                        PlayerName = value.PlayerName,
                        PlayerId = value.PlayerId,
                        Position = value.Position,
                        AvgPoints = value.AvgPoints,
                        Salary = value.Salary,
                        TeamID = value.TeamID
                    };
                    OnPropertyChanged();
                    (DeletePlayerCommand as RelayCommand).NotifyCanExecuteChanged();
                }
               
                
            }
        }


        public MainWindowViewModel()
        {
            
            Players = new RestCollection<Player>("http://localhost:40930/", "player", "hub");

            CreatePlayerCommand = new RelayCommand(() =>
            {
                Player playertoAdd = new Player();
                if (SelectedPlayer.PlayerName != null) 
                {
                    playertoAdd.PlayerName = SelectedPlayer.PlayerName;
                    playertoAdd.Salary = SelectedPlayer.Salary;
                    playertoAdd.TeamID = SelectedPlayer.TeamID;
                    playertoAdd.AvgPoints = SelectedPlayer.AvgPoints;
                    playertoAdd.Position = SelectedPlayer.Position;
                    Players.Add(playertoAdd);
                }
                else
                {
                    Players.Add(SelectedPlayer);
                }
                

            });
            DeletePlayerCommand = new RelayCommand(() =>
            {
                Players.Delete(SelectedPlayer.PlayerId);
            },
            () =>
            {
                return SelectedPlayer != null;
            });

            UpdatePlayerCommand = new RelayCommand(() =>
            {
                Players.Update(selectedPlayer);
            });

            SelectedPlayer = new Player();
            IsPlayerVisible = false;
        }

        public ICommand CreatePlayerCommand { get; set; }
        public ICommand DeletePlayerCommand { get; set; }
        public ICommand UpdatePlayerCommand { get; set; }
    }
}
