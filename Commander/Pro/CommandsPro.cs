using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Pro{
    public class CommandsPro : Profile{
        public CommandsPro()
        {
            CreateMap<Command,CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandUpdateDto,Command>();
            CreateMap<Command,CommandUpdateDto>();
        }
    }
}