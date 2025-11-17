using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.UI.Controls;
using ProyectoPrimeraEvaluacion.ViewModels;
using ProyectoPrimeraEvaluacion.Views;

namespace ProyectoPrimeraEvaluacion.Services;

public partial class NavigationService : ObservableObject
{
    public const string HOME_VIEW = "home";
    public const string CREATE_VIEW = "create";
    public const string CONSULT_VIEW = "consult";

    [ObservableProperty] private ContentControl currentView;

    private NavigationViewItem homeItem;
    private NavigationViewItem createItem;
    private NavigationViewItem consultItem;

    [ObservableProperty] private NavigationViewItem selectedMenuItem;
    
    [ObservableProperty] private ObservableCollection<NavigationViewItem> items = new();


    public NavigationService()
    {
        homeItem = new NavigationViewItem
        {
            Content = "Inicio",
            Tag = HOME_VIEW,
            IconSource = new SymbolIconSource { Symbol = Symbol.Home }
        };

        createItem = new NavigationViewItem
        {
            Content = "Crear producto",
            Tag = CREATE_VIEW,
            IconSource = new SymbolIconSource { Symbol = Symbol.New}
        };

        consultItem = new NavigationViewItem
        {
            Content = "Consultar productos",
            Tag = CONSULT_VIEW,
            IconSource = new SymbolIconSource { Symbol = Symbol.View }
        };
        
        Items.Add(homeItem);
        Items.Add(createItem);
        Items.Add(consultItem);
        
        NavigateTo(HOME_VIEW);
    }

    partial void OnSelectedMenuItemChanged(NavigationViewItem item)
    {
        NavigateTo(item.Tag.ToString()); 
    }
    
    public void NavigateTo(string tag)
    {
        if (tag.Equals(HOME_VIEW))
        {
            CurrentView = new HomeView
            {
                DataContext = new HomeViewModel(this)
            };
            SelectedMenuItem = homeItem;
        }
        else if (tag.Equals(CREATE_VIEW))
        {
            CurrentView = new CreatePerfumeView
            {
                DataContext = new CreatePerfumeViewModel(this)
            };
            SelectedMenuItem = createItem;
        }
        else if (tag.Equals(CONSULT_VIEW))
        {
            
        }
    }
}