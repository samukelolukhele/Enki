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
            CreateMap<User, GetUserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<DayPlan, DayPlanDto>();
            CreateMap<CreateDayPlanDto, DayPlan>();
            CreateMap<Model.Task, TaskDto>();
            CreateMap<CreateTaskDto, Model.Task>();
            CreateMap<Milestone, MilestoneDto>();
        }
    }
}