//mock para testes
using System;
using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data{
    public class CommanderMock : ICommanderRepo
    {
        //probmudarteste----UnitTeste1-commander.teste
        public List<Command> allcomands;
        public CommanderMock(){
            allcomands = new List<Command>{
                new Command{Id=0,firstName="Teste1",surname="Teste1",age=45,creationDate=DateTime.Now},
                new Command{Id=1,firstName="Teste2",surname="Teste2",age=51,creationDate=DateTime.Now},
                new Command{Id=2,firstName="Teste3",surname="Teste3",age=31,creationDate=DateTime.Now},
                new Command{Id=3,firstName="Teste4",surname="Teste4",age=29,creationDate=DateTime.Now},
                new Command{Id=4,firstName="Teste5",surname="Teste5",age=80,creationDate=DateTime.Now},
                new Command{Id=5,firstName="Teste6",surname="Teste6",age=33,creationDate=DateTime.Now}
            };
        }
        public void CreateCommand(Command cmd)
        {
            int cidc = 0;
            allcomands.ForEach(Command =>{
                if(cmd.Id>cidc){
                    cidc = Command.Id;
                }

            });
            cidc++;
            cmd.Id=cidc;
            allcomands.Add(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            int cidc = 0;
            foreach(Command c in allcomands){
                if(c.Id == cmd.Id){
                    break;
                }
                cidc++;
            } 
            allcomands.RemoveAt(cidc);
        }

        public List<Command> GetAppCommands()
        {
            return allcomands;
        }

        public Command GetCommandById(int id)
        {
           Command cmdP = new Command();
            cmdP.Id = -1;
            allcomands.ForEach(cmd => {
                if(cmd.Id == id){
                    cmdP.Id = cmd.Id;
                    cmdP.firstName = cmd.firstName;
                    cmdP.surname = cmd.surname;
                    cmdP.creationDate = cmd.creationDate;
                }
            });
            return cmdP;
        }

        public bool SaveChanges()
        {
            return true;
        }

        public void UpdateCommand(Command cmd)
        {
            //proberro--cmd-updatericommanderRepo
            allcomands.Find(cmd => cmd.Id == cmd.Id).firstName = cmd.firstName;
            allcomands.Find(cmd => cmd.Id == cmd.Id).surname = cmd.surname;
            allcomands.Find(user => cmd.Id == cmd.Id).age = cmd.age;
        }
    }
}