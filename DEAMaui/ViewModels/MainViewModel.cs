using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesdeElBanquillo.Models;
using DesdeElBanquillo.Services;
using System.Diagnostics;

namespace DesdeElBanquillo.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly MockDataService _dataService;

        [ObservableProperty]
        private string _searchQuery = string.Empty;

        public ObservableCollection<League> Leagues { get; } = new();
        public ObservableCollection<QuickAccessItem> QuickAccessItems { get; } = new();
        public ObservableCollection<Team> Teams { get; } = new();

        public MainViewModel(MockDataService dataService)
        {
            _dataService = dataService;
            Title = "Desde el Banquillo";
            LoadData();
        }

        private void LoadData()
        {
            IsBusy = true;

            QuickAccessItems.Clear();
            foreach (var item in _dataService.GetQuickAccessItems())
            {
                QuickAccessItems.Add(item);
            }

            Leagues.Clear();
            foreach (var league in _dataService.GetLeagues())
            {
                Leagues.Add(league);
            }

            Teams.Clear();
            foreach (var team in _dataService.GetTeams())
            {
                Teams.Add(team);
            }

            IsBusy = false;
        }

        [RelayCommand]
        private void Refresh()
        {
            LoadData();
        }

        [RelayCommand]
        private void Search()
        {
            // Lógica de búsqueda (se implementará más adelante)
        }

        [RelayCommand]
        private void ViewItem(QuickAccessItem item)
        {
            // Navegar a la página correspondiente
        }
    }
}