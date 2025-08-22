using AutoMapper;
using PropertyApi.Dtos;
using PropertyApi.Models;

namespace PropertyApi.Mappings
{
  public class PropertyProfile : Profile
  {
    public PropertyProfile()
    {
      CreateMap<Property, PropertyDto>()
        .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
        .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
        .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
        .ForMember(d => d.CodeInternal, o => o.MapFrom(s => s.CodeInternal))
        .ForMember(d => d.Year, o => o.MapFrom(s => s.Year));
    }
  }
}