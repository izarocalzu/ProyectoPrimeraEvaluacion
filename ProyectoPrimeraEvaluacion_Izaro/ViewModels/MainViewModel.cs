using System;
using System.Threading.Tasks;
using Avalonia.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoPrimeraEvaluacion.Services;
using ProyectoPrimeraEvaluacion_Izaro.Data;
using ProyectoPrimeraEvaluacion_Izaro.Models;
using ProyectoPrimeraEvaluacion_Izaro.Services;

namespace ProyectoPrimeraEvaluacion.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private string imageUrl;
    [ObservableProperty] private AvaloniaList<Usuario> listaUsuarios=new();
    [ObservableProperty] private AvaloniaList<ProductModel> listaProductos=new();

    [ObservableProperty] private bool isLogeado = false;
    [ObservableProperty] private NavigationService navigationService=new();
    
    private APIService apiService { get; set; } = new();
    

    [RelayCommand]
    public async Task CrearProductoAsync()
    {
        var p = new ProductModel()
        {
            
        };
        await apiService.CrearProducto(p);
    }
    
    [RelayCommand]
    public async Task ModificarProductoAsync(ProductModel p)
    {
        if (p == null)
        {
            Console.WriteLine("No has seleccionado nada.");
            return;
        }
        try
        {
            p.Code = "REF MODIFICADA";
            bool okModificar = await apiService.ModificarProducto(p);
            if (okModificar)
            {
                Console.WriteLine("Producto modificado");
                await ObtenerProductosAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al actualizar producto. " + ex.Message);
        }
    }

    [RelayCommand]
    public async Task EliminarProductoAsync(ProductModel p)
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
                Console.WriteLine("Producto modificado");
                await ObtenerProductosAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al eliminar producto. " + ex.Message);
        }
    }

    [RelayCommand]
    public async Task ObtenerProductosAsync()
    {
        ListaProductos = await apiService.ObtenerProductos();
    }

    [RelayCommand]
    public async Task ObtenerUsuariosAsync()
    {
        ListaUsuarios = await new DBService().ObtenerTodosLosUsuarios();
    }

    [RelayCommand]
    public async Task RegisterUserAsync()
    {
        var authservice = new GoogleAuthService();
        Usuario usuario = await authservice.LoginAsync(new Usuario());
        ImageUrl = usuario.ImageUrl;

    }

    [RelayCommand]
    public async Task LoginUsuarioAsync(Usuario user)
    {
        if (user == null)
        {
            var authservice = new GoogleAuthService();
            Usuario usuario = await authservice.LoginAsync(new Usuario());
            ImageUrl = usuario.ImageUrl;
            ListaUsuarios = await new DBService().ObtenerTodosLosUsuarios();
            IsLogeado = true;
            
            
        }
        else
        {
            var authservice = new GoogleAuthService();
            var usuario = await authservice.LoginAsync(user);
            ListaUsuarios = await new DBService().ObtenerTodosLosUsuarios();
            IsLogeado = true;
        }
    }
}