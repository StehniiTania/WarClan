using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//works pattern Memento

namespace WarClan
{
    public interface IOriginator
    {
        object GetMemento();
        void SetMemento(object memento);
    }

    public class TestState : IOriginator
    {
        public Clan clan1 { get; set; }
        public Clan clan2 { get; set; }  
        
        public TestState (Clan _clan1, Clan _clan2)
        {
            clan1 = _clan1;
            clan2 = _clan2;
        }

        public object GetMemento() //создает объект типа мементо
        {
            return new Memento
            {
                armySave1 = new ArmySave(clan1.army),
                armySave2 = new ArmySave(clan2.army)
            };
        }
        public void SetMemento(object memento) //восстанавливает
        {
            //if (Object.ReferenceEquals(memento, null)) 
            if (memento == null)
                throw new ArgumentNullException("memento");
            if (!(memento is Memento))
                throw new ArgumentException("memento");
            ((Memento)memento).armySave1.ArmyRestore(clan1.army);
            ((Memento)memento).armySave2.ArmyRestore(clan2.army);
        }
        class Memento
        {
            public ArmySave armySave1 { get; set; }
            public ArmySave armySave2 { get; set; }
        }
    }
    public class Caretaker
    {
        private object m_memento;
        public void SaveState(IOriginator originator)
        {
            if (originator == null)
                throw new ArgumentNullException("originator");
            m_memento = originator.GetMemento();
            
        }
        public void RestoreState(IOriginator originator)
        {
            if (originator == null)
                throw new ArgumentNullException("originator");
            if (m_memento == null)
                throw new InvalidOperationException("m_memento == null");
            originator.SetMemento(m_memento);
        }
    }
}
