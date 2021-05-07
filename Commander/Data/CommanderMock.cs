//mock para testes
using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data{
    public class CommanderMock : ICommanderRepo
    {
        //probmudarteste----UnitTeste1-commander.teste
        public List<Command> allcomands;
        public CommanderMock(){
            allcomands = new List<Command>{
                new Command{Id=0,firstName="Cleber",surname="Wesley",age=45},
                new Command{Id=1,firstName="Pedro",surname="Pereira",age=51},
                new Command{Id=2,firstName="João",surname="Silva",age=31},
                new Command{Id=3,firstName="Luciano",surname="Lima",age=29},
                new Command{Id=4,firstName="Roberto",surname="Carlos",age=80},
                new Command{Id=5,firstName="Luiza",surname="Santos",age=33}
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
            throw new System.NotImplementedException();
        }

        public bool SaveChanges()
        {
            return true;
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}