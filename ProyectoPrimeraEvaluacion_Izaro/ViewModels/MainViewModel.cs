using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoPrimeraEvaluacion.Services;

namespace ProyectoPrimeraEvaluacion.ViewModels;

public partial class MainViewModel : ObservableObject
{
    // Propiedad que controla la visibilidad del menú/overlay de Login
    [ObservableProperty]
    private bool _isUserLoggedIn = false; 

    public NavigationService NavigationService { get; }
    public IAsyncRelayCommand GoogleAuthCommand { get; }
    
    // Constructor inyectado desde App.axaml.cs (como ya lo tienes)
    public MainViewModel(NavigationService navigationService)
    {
        NavigationService = navigationService;
        GoogleAuthCommand = new AsyncRelayCommand(ExecuteGoogleAuthAsync);
        
        // Opcional: Si quieres empezar en la vista de bienvenida en el diseñador
        // if (Design.IsDesignMode)
        // {
        //     IsUserLoggedIn = true;
        //     NavigationService.NavigateTo(NavigationService.HOME_VIEW);
        // }
    }
    
    // Este método simula la lógica de autenticación real
    // En tu código REAL, aquí llamarías a tu IGoogleAuthService
    private async Task<bool> AuthTest()
    {
        // === ZONA DE AUTENTICACIÓN REAL ===
        
        // Simulación de una operación asíncrona de red/BD
        await Task.Delay(500); 

        // Puedes forzar una excepción para probar el 'catch'
        // if (DateTime.Now.Second % 2 == 0) throw new Exception("Error de conexión simulado.");
        
        return true; // Reemplaza esto con el resultado de tu LoginWithGoogleAsync()
    }


    private async Task ExecuteGoogleAuthAsync()
    {
        bool loginExitoso = false;
        
        try
        {
            // 1. Ejecutar la lógica de autenticación
            loginExitoso = await AuthTest(); 

            if (loginExitoso)
            {
                // 2. Si es exitoso, actualiza el estado (esto oculta la capa de login)
                IsUserLoggedIn = true; 
                
                // 3. Navega a la Vista 2 (Inicio/Bienvenida)
                // Si esta línea falla, la excepción será capturada.
                NavigationService.NavigateTo(NavigationService.HOME_VIEW); 
            }
            else
            {
                Console.WriteLine("Login fallido. Credenciales inválidas.");
            }
        }
        catch (Exception ex)
        {
            // ***** ESTO EVITA QUE LA APLICACIÓN SE CIERRE *****
            
            // Si llega aquí, significa que la llamada a AuthTest() o NavigateTo() falló.
            Console.WriteLine($"Error crítico durante la operación: {ex.Message}");
            
            // Opcional: Mostrar un diálogo de error al usuario.
            
            // Asegurarse de que el usuario no esté logueado
            IsUserLoggedIn = false;
        }
    }
}