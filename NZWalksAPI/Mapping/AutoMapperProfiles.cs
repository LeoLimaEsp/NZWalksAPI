using AutoMapper;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Intall Mapper and Dependency injection (Register in Program.cs)
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();

            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        }
    }
}
