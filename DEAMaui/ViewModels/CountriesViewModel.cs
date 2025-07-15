using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using DesdeElBanquilloApplication.Models;
using Microsoft.Maui.Controls;

public class CountriesViewModel : INotifyPropertyChanged
{
    private readonly ApiService _apiService;
    public ObservableCollection<Country> Countries { get; } = new();
    
    private Country _selectedCountry;
    public Country SelectedCountry
    {
        get => _selectedCountry;
        set { _selectedCountry = value; OnPropertyChanged(nameof(SelectedCountry)); }
    }

    public ICommand LoadCountriesCommand { get; }
    public ICommand AddCountryCommand { get; }
    public ICommand DeleteCountryCommand { get; }

    public CountriesViewModel()
    {
        _apiService = new ApiService();
        LoadCountriesCommand = new Command(async () => await LoadCountries());
        AddCountryCommand = new Command<string>(async (name) => await AddCountry(name));
        DeleteCountryCommand = new Command<Country>(async (country) => await DeleteCountry(country));
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    private async Task LoadCountries()
    {
        var countries = await _apiService.GetCountriesAsync();
        Countries.Clear();
        foreach (var country in countries)
        {
            Countries.Add(country);
        }
    }

    private async Task AddCountry(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return;
        var newCountry = new Country { Name = name };
        await _apiService.AddCountryAsync(newCountry);
        await LoadCountries(); // Recargar la lista
    }

    private async Task DeleteCountry(Country country)
    {
        if (country == null) return;
        await _apiService.DeleteCountryAsync(country.IdCountry);
        Countries.Remove(country);
    }
}