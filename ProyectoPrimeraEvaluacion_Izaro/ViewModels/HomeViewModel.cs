using ProyectoPrimeraEvaluacion_Izaro.ViewModels;
using ProyectoPrimeraEvaluacion.Services;

namespace ProyectoPrimeraEvaluacion.ViewModels;


public partial class HomeViewModel : ViewModelBase
{
    private NavigationService _navigationService;

    public HomeViewModel()
    {
        
    }

    public HomeViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }
}