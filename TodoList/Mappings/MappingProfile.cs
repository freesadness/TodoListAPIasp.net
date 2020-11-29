using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApplication1.Models;
using System.Threading.Tasks;

namespace WebApplication1.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoTask, TodoTaskDto>();
            CreateMap<TodoTaskDto, TodoTask>();
            CreateMap<User, UserCredential>();
            CreateMap<UserCredential, User>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
