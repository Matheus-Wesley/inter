using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers{

    [Route("api/commands")]
    [ApiController]

    
    public class CommandsController : ControllerBase{
        public CommandsController(ICommanderRepo repository,IMapper mapper,ILogger<CommandsController> logger)
        {
            _repository=repository;
            _mapper=mapper;
            _logger=logger;
            
        
        }
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        [HttpGet]
        public ActionResult <IEnumerable<Command>> GetAllCommands(){
            _logger.Log(LogLevel.Information,MyLogEvents.ListItems,"/Commands GET ");
            var commandItems = _repository.GetAppCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        [HttpGet("{id}",Name = "GetCommandById")]
        public ActionResult <CommandReadDto> GetCommandById(int id){
            _logger.LogInformation(MyLogEvents.GetItem, "Getting user {Id}", id);
            var commandItem = _repository.GetCommandById(id);
            if (commandItem != null){
                _logger.LogInformation(MyLogEvents.GetItem, "Users getted {Id}", id);
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            _logger.LogWarning(MyLogEvents.GetItemNotFound, "Get({Id}) USER NOT FOUND", id);
            return NotFound();
        }
        
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommands(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
             _logger.LogInformation(MyLogEvents.InsertItem, "Inserting user");
             commandModel.creationDate=DateTime.Now;
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();
            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
            return CreatedAtRoute(nameof(GetCommandById),new{Id= commandReadDto.Id},commandReadDto);
        //pverro
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto){
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo == null){
                _logger.LogWarning(MyLogEvents.UpdateItemNotFound, "Put({Id}) USER NOT FOUND", id);
                return NotFound();

            }
            _mapper.Map(commandUpdateDto,commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id,JsonPatchDocument <CommandUpdateDto> patchDoc){
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo == null){
                return NotFound();

            }
            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch,ModelState);
            if(TryValidateModel(commandToPatch)){
                return ValidationProblem(ModelState);

            }
            _mapper.Map(commandToPatch,commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id){
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo == null){
                _logger.LogWarning(MyLogEvents.DeleteItem, "Delete({Id}) USER NOT FOUND", id);
                return NotFound();
        }
         
        _repository.DeleteCommand(commandModelFromRepo);
        _logger.LogInformation(MyLogEvents.DeleteItem, "Deleting user {Id}", id);
        _repository.SaveChanges();

        return NoContent();
    }
}
}