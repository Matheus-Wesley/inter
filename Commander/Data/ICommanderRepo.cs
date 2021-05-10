using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data{
    public interface ICommanderRepo{

        bool SaveChanges();
        List<Command> GetAppCommands();
        //mudando de IEnumerable para lista
        Command GetCommandById(int id);
        void CreateCommand(Command cmd);
        void UpdateCommand(Command cmd);
        //cmd=????
        void DeleteCommand(Command cmd);
    }
}