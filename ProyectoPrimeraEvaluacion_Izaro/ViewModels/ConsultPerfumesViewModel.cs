using System.Threading.Tasks;
using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoPrimeraEvaluacion_Izaro.Models;
using ProyectoPrimeraEvaluacion_Izaro.Services;

namespace ProyectoPrimeraEvaluacion_Izaro.ViewModels;
 
public partial class ConsultPerfumesViewModel : ViewModelBase
{
    private NavigationService _navigationService;
    
    [ObservableProperty] private AvaloniaList<ProductModel> listaProductos;
    
    private APIService apiService { get; set; } = new();

    public ConsultPerfumesViewModel()
    {
        
    }
    
    public ConsultPerfumesViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }
    
    [RelayCommand]
    public async Task ObtenerProductosAsync()
    {
        ListaProductos = await apiService.ObtenerProductos();
    }
}