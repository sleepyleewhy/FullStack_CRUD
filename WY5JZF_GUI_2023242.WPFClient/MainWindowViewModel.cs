﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WY5JZF_HFT_2023241.Models;

namespace WY5JZF_GUI_2023242.WPFClient
{
    class MainWindowViewModel : ObservableRecipient
    {

        RestService restService = new RestService("http://localhost:40930/");
        public RestCollection<Player> Players { get; set; }
        private Player selectedPlayer;
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
                    (UpdatePlayerCommand as RelayCommand).NotifyCanExecuteChanged();
                }
                
            }
        }
        public RestCollection<Team> Teams { get; set; }

        private Team selectedTeam;

        public Team SelectedTeam
        {
            get { return selectedTeam; }
            set 
            {
                if(value != null)
                {
                    selectedTeam = new Team()
                    {
                        TeamName = value.TeamName,
                        TeamId = value.TeamId,
                        DivisionID = value.DivisionID,
                        FanCount = value.FanCount,
                        Players = value.Players
                    };
                    OnPropertyChanged();
                    (DeleteTeamCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateTeamCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public RestCollection<Division> Divisions { get; set; }

        private Division selectedDivision;

        public Division SelectedDivision
        {
            get { return selectedDivision; }
            set 
            {
                if (value != null)
                {
                    selectedDivision = new Division()
                    {
                        DivisionName = value.DivisionName,
                        DivisionId = value.DivisionId,
                        Population = value.Population,
                        Teams = value.Teams
                    };
                    OnPropertyChanged();
                    (DeleteDivisionCommand as RelayCommand).NotifyCanExecuteChanged();
                    (UpdateDivisionCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }





        public MainWindowViewModel()
        {
            
            Players = new RestCollection<Player>("http://localhost:40930/", "player", "hub");
            Teams = new RestCollection<Team>("http://localhost:40930/", "team", "hub");
            Divisions= new RestCollection<Division>("http://localhost:40930/", "division", "hub");

            


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
            }, () => { return selectedPlayer != null; });

            SelectedPlayer = new Player();

            CreateTeamCommand = new RelayCommand(() =>
            {
                
                if (SelectedTeam.TeamName != null)
                {
                    Team teamToAdd = new Team();
                    teamToAdd.TeamName = SelectedTeam.TeamName;
                    teamToAdd.FanCount = SelectedTeam.FanCount;
                    teamToAdd.DivisionID = SelectedTeam.DivisionID;
                    Teams.Add(teamToAdd);
                }
                else
                {
                    Teams.Add(SelectedTeam);
                }
            });

            UpdateTeamCommand = new RelayCommand(() =>
            {
                Teams.Update(SelectedTeam);
            },
            () =>
            {
                return SelectedTeam != null;
            });
            DeleteTeamCommand = new RelayCommand(() =>
            {
                Teams.Delete(SelectedTeam.TeamId);
            },
            () =>
            {
                return SelectedTeam != null;
            });

            SelectedTeam = new Team();

            CreateDivisionCommand = new RelayCommand(() =>
            {
                if (SelectedDivision.DivisionName != null)
                {
                    Division divisionToAdd = new Division();
                    divisionToAdd.DivisionName = SelectedDivision.DivisionName;
                    divisionToAdd.Population = SelectedDivision.Population;
                    Divisions.Add(divisionToAdd);
                }
                else
                {
                    Divisions.Add(SelectedDivision);
                }
            });
            UpdateDivisionCommand = new RelayCommand(() =>
            {
                Divisions.Update(SelectedDivision);
            },
            () =>
            {
                return SelectedDivision != null;
            });
            DeleteDivisionCommand = new RelayCommand(() =>
            {
                Divisions.Delete(SelectedDivision.DivisionId);
            },
            () =>
            {
                return SelectedDivision != null;
            });

            SelectedDivision = new Division();

            AllPosPlayerInTeamCommand = new RelayCommand(() =>
            {
                AllPosPlayerInTeamList = new ObservableCollection<Player>(restService.Get<Player>($"Stat/AllPosPlayerInTeam/{AllPosPlayerInTeamPosition}/{AllPosPlayerInTeamTeam.TeamId}"));
                OnPropertyChanged(nameof(AllPosPlayerInTeamList));
            },
            () =>
            {
                int helper;
                if(AllPosPlayerInTeamPosition != null && int.TryParse(AllPosPlayerInTeamPosition,out helper))
                {
                    return AllPosPlayerInTeamTeam != null && helper > 0 && helper< 6;
                }
                else
                {
                    return false;
                }
                
            });

            AvgPointsPerTeamCommand = new RelayCommand(() =>
            {
                AvgPointsPerTeamResult = Math.Round(restService.GetSingle<double>($"Stat/AvgPointsPerTeam/{AvgPointsPerTeamTeam.TeamId}"),2);
                OnPropertyChanged(nameof(AvgPointsPerTeamResult));
            },
            () =>
            {
                return AvgPointsPerTeamTeam != null;
            });

            Top3PointsInDivCommand = new RelayCommand(() =>
            {
                Top3PointsInDivResult = new ObservableCollection<Player>(restService.Get<Player>($"Stat/Top3PointsInDiv/{Top3PointsInDivDiv.DivisionId}"));
                OnPropertyChanged(nameof(Top3PointsInDivResult));
            },
            () =>
            {
                return Top3PointsInDivDiv != null;
            });

            AllFansPerDivisionCommand = new RelayCommand(() =>
            {
                AllFansPerDivisionResult = restService.GetSingle<int>($"/Stat/AllFansPerDivision/{AllFansPerDivisionDiv.DivisionId}");
                OnPropertyChanged(nameof(AllFansPerDivisionResult));
            },
            () =>
            {
                return AllFansPerDivisionDiv != null;
            });

            TeamWithMostSalaryCostInDivCommand = new RelayCommand(() =>
            {
                TeamWithMostSalaryCostInDivResult = restService.GetSingle<Team>($"/Stat/TeamWithMostSalaryCostInDiv/{TeamWithMostSalaryCostInDivDiv.DivisionId}");
                OnPropertyChanged(nameof(TeamWithMostSalaryCostInDivResult));
            },
            () =>
            {
                return TeamWithMostSalaryCostInDivDiv != null;
            });
        }

        public ICommand CreatePlayerCommand { get; set; }
        public ICommand DeletePlayerCommand { get; set; }
        public ICommand UpdatePlayerCommand { get; set; }

        public ICommand CreateTeamCommand { get; set; }
        public ICommand DeleteTeamCommand { get; set; }
        public ICommand UpdateTeamCommand { get; set; }

        public ICommand CreateDivisionCommand { get; set; }
        public ICommand DeleteDivisionCommand { get; set; }
        public ICommand UpdateDivisionCommand { get; set; }

        public ICommand AllPosPlayerInTeamCommand { get; set; }

        public ObservableCollection<Player> AllPosPlayerInTeamList { get; set; }

        private string allPosPlayerinTeamPosition;

        public string AllPosPlayerInTeamPosition
        {
            get { return allPosPlayerinTeamPosition; }
            set { allPosPlayerinTeamPosition = value;
                (AllPosPlayerInTeamCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        private Team allPosPlayerInTeamTeam;

        public Team AllPosPlayerInTeamTeam
        {
            get { return allPosPlayerInTeamTeam; }
            set { allPosPlayerInTeamTeam = value;
                (AllPosPlayerInTeamCommand as RelayCommand).NotifyCanExecuteChanged(); }
        }

        public ICommand AvgPointsPerTeamCommand { get; set; }
        
        public double AvgPointsPerTeamResult { get; set; }

        private Team avgPointsPerTeamTeam;

        public Team AvgPointsPerTeamTeam
        {
            get { return avgPointsPerTeamTeam; }
            set { avgPointsPerTeamTeam = value;
                (AvgPointsPerTeamCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public ICommand Top3PointsInDivCommand { get; set; }
        public ObservableCollection<Player> Top3PointsInDivResult { get; set; }
        private Division top3PointsInDivDiv;

        public Division Top3PointsInDivDiv
        {
            get { return top3PointsInDivDiv; }
            set { top3PointsInDivDiv = value;
                (Top3PointsInDivCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand AllFansPerDivisionCommand { get; set; }

        private Division allFansPerDivisionDiv;

        public Division AllFansPerDivisionDiv
        {
            get { return allFansPerDivisionDiv; }
            set { allFansPerDivisionDiv = value;
                (AllFansPerDivisionCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public int AllFansPerDivisionResult { get; set; }


        public ICommand TeamWithMostSalaryCostInDivCommand { get; set; }

        public Team TeamWithMostSalaryCostInDivResult { get; set; }

        private Division teamWithMostSalaryCostInDivDiv;

        public Division TeamWithMostSalaryCostInDivDiv
        {
            get { return teamWithMostSalaryCostInDivDiv; }
            set { teamWithMostSalaryCostInDivDiv = value;
                (TeamWithMostSalaryCostInDivCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }








    }
}
