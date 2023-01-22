using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Console;


//works pattern "ChainOfResponsibility"

namespace WarClan
{   

    class Receiver
    {
        public bool warriorsExecute { get; set; }
        public bool dragonsExecute { get; set; }
        public bool robotsExecute { get; set; }

        public Receiver() { }

        public Receiver(bool wr, bool dr, bool rb)
        {
            warriorsExecute = wr;
            dragonsExecute = dr;
            robotsExecute = rb;
        }
    }

    abstract class HandlerCommand
    {      
        public HandlerCommand Successor { get; set; }
        public abstract void Handle(Receiver receiver);
    }   

    class WarriorsCommandHandler : HandlerCommand
    {
        Head head = new Head();
        string nameComand;

        public WarriorsCommandHandler() { }

        public WarriorsCommandHandler(Clan clan, string _nameCommand)
        {
            head.SetCommand(new SquadCommand(clan, "W"));
            nameComand = _nameCommand;
        }

        public override void Handle(Receiver receiver)
        {
            if (receiver.warriorsExecute == true)
            {
                if (nameComand == "Forward")
                    head.PressForward();
                if (nameComand == "Back")
                    head.PressBack();
                if (nameComand == "Attack")
                    head.PressAttack();
            }                
            else if (Successor != null)
                Successor.Handle(receiver);
        }
    }

    class DragonsCommandHandler : HandlerCommand
    {
        Head head = new Head();
        string nameComand;

        public DragonsCommandHandler() { }

        public DragonsCommandHandler(Clan clan, string _nameCommand)
        {
            head.SetCommand(new SquadCommand(clan, "D"));
            nameComand = _nameCommand;
        }

        public override void Handle(Receiver receiver)
        {
            if (receiver.dragonsExecute == true)
            {
                if (nameComand == "Forward")
                    head.PressForward();
                if (nameComand == "Back")
                    head.PressBack();
                if (nameComand == "Attack")
                    head.PressAttack();
            }
            else if (Successor != null)
                Successor.Handle(receiver);
        }
    }

    class RobotsCommandHandler : HandlerCommand
    {
        Head head = new Head();
        string nameComand;

        public RobotsCommandHandler() { }

        public RobotsCommandHandler(Clan clan, string _nameCommand)
        {
            head.SetCommand(new SquadCommand(clan, "R"));
            nameComand = _nameCommand;
        }

        public override void Handle(Receiver receiver)
        {
            if (receiver.robotsExecute == true)
            {
                if (nameComand == "Forward")
                    head.PressForward();
                if (nameComand == "Back")
                    head.PressBack();
                if (nameComand == "Attack")
                    head.PressAttack();
            }
            else if (Successor != null)
            {
                Successor.Handle(receiver);
                MessageBox.Show("Выполнить приказ невозможно!");
            }                
        }
    }
}
