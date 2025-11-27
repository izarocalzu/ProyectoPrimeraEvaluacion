using ProyectoPrimeraEvaluacion_Izaro.Services;

namespace ProyectoPrimeraEvaluacion_Izaro.ViewModels;


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