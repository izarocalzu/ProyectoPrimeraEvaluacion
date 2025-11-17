using ProyectoPrimeraEvaluacion_Izaro.ViewModels;
using ProyectoPrimeraEvaluacion.Services;

namespace ProyectoPrimeraEvaluacion.ViewModels;

public partial class CreatePerfumeViewModel : ViewModelBase
{
    private NavigationService _navigationService;

    
    public CreatePerfumeViewModel()
    {
        
    }

    public CreatePerfumeViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }
}