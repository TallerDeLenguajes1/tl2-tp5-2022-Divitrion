using Automapper;
using Cadeteria.Models;
using Cadeteria.Viewmodels;

public class PerfilDeMapeo 
{
    public PerfilDeMapeo()
    {
        CreateMap<Cadete,CadeteViewModel>().ReverseMap();
    }
}