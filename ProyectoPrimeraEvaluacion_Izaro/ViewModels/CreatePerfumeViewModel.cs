using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Collections;
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
    [ObservableProperty] private AvaloniaList<ProductModel> productos = new();

    [ObservableProperty] private string productCode;
    [ObservableProperty] private string productDescription;
    [ObservableProperty] private string selectedBrand;
    [ObservableProperty] private bool isLimitedEdition;
    [ObservableProperty] private DateTime creationDate = DateTime.Today;
    [ObservableProperty] private double volumeValue = 100.0;
    
    
    // ELIMINADO: [ObservableProperty] private Perfumes perfumesModel = new();
    // ELIMINADO: [ObservableProperty] private ProductModel perfume = new();
    

    [ObservableProperty] private ProductModel selectedProduct;

    public CreatePerfumeViewModel()
    {
        
    }
    
    public CreatePerfumeViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;
        LoadMarcas();
    }

    /*private void LoadPerfumes()
    {
        ProductModel p = new ProductModel(1,"1","Hola","Tecnologia", 100.0m, true, DateTime.Now);
        this.PerfumesModel.productosList.Add(p);
    }*/
    
    [RelayCommand]
    public async Task CreateNewPerfumeAsync()
    {
        var perfume = new ProductModel()
        {
            Code = ProductCode,
            Description = ProductDescription,
            Brand = SelectedBrand,
            IsLimited = IsLimitedEdition,
            Volume = VolumeValue,
            CreationDate = CreationDate
        };
        
        try
        {
            await apiService.CrearProducto(perfume); 
            Console.WriteLine("Perfume creado con éxito. Por favor, pulsa 'Actualizar Lista' para verlo.");
         
        }
        catch (Exception ex)
        {
            Console.WriteLine("¡ERROR! No se pudo crear el perfume. Revisa los datos. Mensaje de error: " + ex.Message);
        }
    }
    
    [RelayCommand]
    public void CancelCreate()
    {
        ProductCode = string.Empty;
        ProductDescription = string.Empty;
        SelectedBrand = null;
        IsLimitedEdition = false;
        VolumeValue = 0.0;
        CreationDate = DateTime.Today;
    }
    
    /*[RelayCommand]
    public async Task CreateNewPerfume()
    {
        var perfume = new ProductModel()
        {
            Code = ProductCode,
            Description = ProductDescription,
            Brand = SelectedBrand,
            IsLimited = IsLimitedEdition,
            Volume = 11,
            CreationDate = DateTime.Today
        };
        await apiService.CrearProducto(perfume);
    }*/

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