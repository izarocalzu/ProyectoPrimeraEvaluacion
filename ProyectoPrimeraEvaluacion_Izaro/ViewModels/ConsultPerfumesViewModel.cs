using System.Threading.Tasks;
using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentAvalonia.UI.Controls;
using DialogHostAvalonia;
using ProyectoPrimeraEvaluacion_Izaro.Models;
using ProyectoPrimeraEvaluacion_Izaro.Services;
using ProyectoPrimeraEvaluacion_Izaro.Views.Dialogs;
using DialogHost = DialogHostAvalonia.DialogHost;

namespace ProyectoPrimeraEvaluacion_Izaro.ViewModels;

public partial class ConsultPerfumesViewModel : ViewModelBase
{
    private NavigationService _navigationService;

    [ObservableProperty] private AvaloniaList<ProductModel> listaProductos;

    [ObservableProperty] private ProductModel selectedProduct;

    private APIService apiService { get; set; } = new();

    public ConsultPerfumesViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public ConsultPerfumesViewModel()
    {
        ObtenerProductosAsync();
    }

    public async Task ObtenerProductosAsync()
    {
        ListaProductos = await apiService.ObtenerProductos();
    }

    [RelayCommand]
    public void OpenDeleteDialog()
    {
        DeleteDialog deleteDialog = new DeleteDialog();
        //entrantesDialog.DataContext = new PizzaViewModel();
        DialogHost.Show(deleteDialog, "DeleteDialog");
    }
}