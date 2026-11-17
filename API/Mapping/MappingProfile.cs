using AutoMapper;
using core.Entities;
using core.Resources; 

namespace API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskItem, TaskReadResource>();
            CreateMap<TaskUpdateResource, TaskItem>();
        }
    }
}