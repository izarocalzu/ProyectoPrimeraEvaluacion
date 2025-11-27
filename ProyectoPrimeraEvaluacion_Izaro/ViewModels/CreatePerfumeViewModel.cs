using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoPrimeraEvaluacion_Izaro.Models;
using ProyectoPrimeraEvaluacion_Izaro.Services;

namespace ProyectoPrimeraEvaluacion_Izaro.ViewModels;

public partial class CreatePerfumeViewModel : ViewModelBase
{
    private NavigationService _navigationService;
    
    private APIService apiService { get; set; } = new();
    
    [ObservableProperty] private ObservableCollection<string> marcasList = new();

    
    public CreatePerfumeViewModel()
    {
        LoadMarcas();
    }

    [RelayCommand]
    public async Task CreateNewPerfume()
    {
        var perfume = new ProductModel()
        {
            
        };
        await apiService.CrearProducto(perfume);
    }

    public CreatePerfumeViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public void LoadMarcas()
    {
        MarcasList.Add("Yves Saint Laurant");
        MarcasList.Add("Dior");
        MarcasList.Add("Narcisso Rodriguez");
        MarcasList.Add("Carolina Herrera");
        MarcasList.Add("Hugo Boss");
        MarcasList.Add("Paco Rabanne");
        MarcasList.Add("Pedro del Hierro");
        MarcasList.Add("Jean Paul Gaultier");
    }
}