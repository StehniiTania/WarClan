using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//works pattern Command

namespace WarClan
{
    public class Head
    {
        ICommand command;

        public Head() { }

        public void SetCommand(ICommand _command)
        {
            command = _command;
        }

        public void PressForward()
        {
            command.Forward();
        }

        public void PressBack()
        {
            command.Back();
        }

        public void PressAttack()
        {
            command.Attack();
        }

    }

    public interface ICommand
    {
        void Forward();
        void Back();
        void Attack();
    }

    //команды для отряда воинов
    class SquadCommand : ICommand
    {
        Clan clan;
        string nameSquad; 

        public SquadCommand(Clan _clan, string _nameSquad)
        {
            clan = _clan;
            nameSquad = _nameSquad;
        }
        public void Forward()
        {
            clan.SquadForward(nameSquad);                    
        }
        public void Back()
        {
            clan.SquadBack(nameSquad);
        }
        public void Attack()
        {
            clan.SquadAttack(nameSquad);        
        }
    }   
}
