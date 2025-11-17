using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using ProyectoPrimeraEvaluacion_Izaro.ViewModels;
using ProyectoPrimeraEvaluacion_Izaro.Views;
using ProyectoPrimeraEvaluacion.Services;
using ProyectoPrimeraEvaluacion.ViewModels;

namespace ProyectoPrimeraEvaluacion_Izaro;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var navigationService = new NavigationService(); // Crea el servicio
        
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(navigationService) // Pasa el servicio al ViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}