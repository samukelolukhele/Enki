using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using server.Dto;
using server.Model;

namespace server.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<DayPlan, DayPlanDto>();
            CreateMap<Model.Task, TaskDto>();
        }
    }
}