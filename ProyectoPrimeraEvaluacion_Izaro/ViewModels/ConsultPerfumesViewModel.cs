using System;
using System.Collections.ObjectModel;
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
    
    [ObservableProperty] private ProductModel productModel = new();

    [ObservableProperty] private AvaloniaList<ProductModel> listaProductos;
    
    [ObservableProperty] private ObservableCollection<string> marcasList = new();

    [ObservableProperty] private ProductModel selectedProduct;
    [ObservableProperty] private bool isLimited = false;

    private APIService apiService { get; set; } = new();

    public ConsultPerfumesViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public ConsultPerfumesViewModel()
    {
        ObtenerProductosAsync();
        LoadMarcas();
    }
    
    [RelayCommand]
    public async Task ActualizarProductosAsync()
    {
        await ObtenerProductosAsync(); 
    }

    public async Task ObtenerProductosAsync()
    {
        ListaProductos = await apiService.ObtenerProductos();
    }
    
    [RelayCommand]
    public void LoadPerfumeSelected()
    {
        ProductModel = new ProductModel(SelectedProduct);
    }

    [RelayCommand]
    public void OpenDeleteDialog()
    {
        DeleteDialog deleteDialog = new DeleteDialog();
        //entrantesDialog.DataContext = new PizzaViewModel();
        DialogHost.Show(deleteDialog, "DeleteDialog");
    }
    
    [RelayCommand]
    public async Task DeletePerfumeAsync(ProductModel p)
    {
        if (p == null)
        {
            Console.WriteLine("No has seleccionado nada.");
            return;
        }

        try
        {
            bool okEliminar = await apiService.EliminarProducto(p);
            if (okEliminar)
            {
                await ObtenerProductosAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al eliminar producto. " + ex.Message);
        }
        finally
        {
            DialogHost.Close("DeleteDialog");
        }
    }
    
    [RelayCommand]
    public void CloseDeleteDialog()
    {
        DialogHost.Close("DeleteDialog");
    }
    
    [RelayCommand]
    public void OpenEditDialog()
    {
        EditDialog editDialog = new EditDialog();
        editDialog.DataContext = selectedProduct;
        DialogHost.Show(editDialog, "EditDialog");
    }
    
    
    [RelayCommand]
    public async Task SaveEditAsync()
    {
        if (SelectedProduct == null) return;

        try
        {
            bool exito = await apiService.ModificarProducto(SelectedProduct);
        
            if (exito)
            {
                Console.WriteLine("Perfume {SelectedProduct.Code} modificado con éxito.");
                await ObtenerProductosAsync(); 
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al guardar cambios: " + ex.Message);
        }
        finally
        {
            DialogHost.Close("EditDialog");
        }
    }
    
    [RelayCommand]
    public void CloseEditDialog()
    {
        DialogHost.Close("EditDialog");
    }

    [RelayCommand]
    public void NavigateTo(string tag_view)
    {
        _navigationService.NavigateTo(tag_view);
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