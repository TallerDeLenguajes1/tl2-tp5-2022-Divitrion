using AutoMapper;
using Cadeteria.Models;
using Cadeteria.ViewModels;

public class PerfilDeMapeo : Profile
{
    public PerfilDeMapeo()
    {
        CreateMap<Cadete,CadeteViewModel>().ReverseMap();
    }
}