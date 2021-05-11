using System;
using Xunit;
using Commander.Controllers;
using Commander.Data;
using Commander.Pro;
using Commander.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Teste
{
    public class UnitTest1
    {
                public CommanderMock repository{get;}
        //_
        private IMapper _mapper;
        
        public ControllerTeste control {get;}
        //Floger
        public UnitTest1(){
            _mapper = new MapperConfiguration(c => c.AddProfile<CommandsPro>()).CreateMapper();
            repository = new CommanderMock();
            control = new ControllerTeste(repository,_mapper);
        }
}
  [CollectionDefinition("Testes1", DisableParallelization = true)]
        public class DatabaseCollection : ICollectionFixture<UnitTest1>
    {

    }
        //tstget
        [Collection("Testes1")]
        public class GetTest{
            UnitTest1 teste;
            public GetTest(UnitTest1 tst1){
                teste=tst1;
            }
        
        [Fact]
        public void TestidRtn()
        {   //pegaumquejaexiste
            Assert.Equal("Teste1",teste.control.GetCommandById(0).Value.firstName);
        }
        [Fact]
        public void Test1AllCommands()
        {
            //todosestão aui
            Assert.Equal(6,teste.repository.GetAppCommands().Count);
        }
        //teste insert
        [Collection("Testes1")]
        public class TesteInsert{
            UnitTest1 teste;
            public TesteInsert(UnitTest1 tst1){
                teste=tst1;
            }
         [Fact]
         //criaumnovo
        public void testins_insert(){
            Command cmd=new Command();
            cmd.Id=6;
            cmd.firstName="Teste7";
            cmd.surname="Teste7";
            cmd.age=35;
            teste.control.CreateCommand(cmd);
            Command cmdinsert = new Command();
            cmdinsert=teste.repository.GetCommandById(6);
            Assert.Equal("Teste7", cmdinsert.firstName);
        }
        
        [Fact]
        //datecomdatetime
        public void testins_data(){
            Command cmd=new Command();
            cmd.Id=7;
            cmd.firstName="Teste8";
            cmd.surname="Teste8";
            cmd.age=87;
            teste.control.CreateCommand(cmd);
            DateTime creationTime = DateTime.Now;
            Command cmdinsert = new Command();
            cmdinsert=teste.repository.GetCommandById(8);
            bool Testedate = false;
            if(creationTime.Date == cmdinsert.creationDate.Date){
                Testedate=true;
            }
            Assert.False(Testedate);
        }
        //testedelete
        [Collection("Testes1")]
        public class TesteDelete{
            UnitTest1 teste;
            public TesteDelete(UnitTest1 tst1){
                teste=tst1;
            }  
        [Fact]
        public void DeleteUser_ReturnsIdNegativo()
        {
            teste.control.DeleteCommand(3);
            Assert.Equal(-1,teste.control.GetCommandById(3).Value.Id);
        }
        
        [Collection("Testes1")]
        public class TesteUpdate{
            UnitTest1 teste;
            public TesteUpdate(UnitTest1 tst1){
                teste=tst1;
            }
        [Fact]
        public void Updateteste(){
            Command cmd = new Command();
            cmd.Id=4;
            cmd.firstName="Matheus";
            cmd.surname="Silva";
            cmd.age=21;
            teste.control.UpdateCommand(4,cmd);
            cmd=teste.repository.GetCommandById(4);
            Assert.Equal("Teste5", cmd.firstName);
            Assert.Equal("Teste5", cmd.surname);
        }
        
        }
    }
}
}
}
