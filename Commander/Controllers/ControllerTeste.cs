using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
//classe criada para teste no commanderteste
//termi
namespace Commander.Controllers{
    [Route("api/commands")]
    [ApiController]

    public class ControllerTeste : ControllerBase{
        private readonly ICommanderRepo _repository;
        private IMapper _mapper;

        public ControllerTeste(ICommanderRepo repository,IMapper mapper){
            _repository=repository;
            _mapper=mapper;
        }
        [HttpGet]
        public List<Command> GetAllCommands(){
            List<Command> commandItems=_repository.GetAppCommands();
            return commandItems;
           
        }
        [HttpGet("{id}")]
        public ActionResult <Command> GetCommandById(int id){
            Command commandItems = _repository.GetCommandById(id);
            if(commandItems != null){
                return commandItems;
            }else{return NotFound();}
            
        }
        [HttpPost]
        public ActionResult<Command> CreateCommand(Command cmd)
        {
            cmd.creationDate=DateTime.Now;
            _repository.CreateCommand(cmd);
            return cmd;
        //pverro
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, Command commandUpdateDto){
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo == null){
                return NotFound();

            }
            if (commandUpdateDto.surname==null)
            {
                commandUpdateDto.surname=commandModelFromRepo.surname;
            }
            _repository.UpdateCommand(commandUpdateDto);
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
                return NotFound();
        }
         
        _repository.DeleteCommand(commandModelFromRepo);
        _repository.SaveChanges();

        return NoContent();
    }
    }
}