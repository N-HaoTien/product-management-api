using AutoMapper;
using Product.Api.Entities;
using Product.Api.Models;

namespace Product.Api;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<CatalogProduct, CatalogProductDto>().ReverseMap();
		CreateMap<CreateCatalogProductDto, CatalogProduct>();
		CreateMap<UpdateCatalogProductDto, CatalogProduct>();
	}
}