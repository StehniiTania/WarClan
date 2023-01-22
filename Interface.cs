using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//works pattern Bridge

namespace WarClan
{
    //устанавливает форму вывода
    public interface IInterface
    {        
        int TypeInterface();
    }

    class TextInterface : IInterface
    {
        public int TypeInterface() 
        {   return 1;    }
    }

    class PictureInterface : IInterface
    {
        public int TypeInterface() 
        {   return 2;   }
    }    

    class PictureTextInterface : IInterface
    {
        public int TypeInterface() 
        {  return 3;  }
    }

    abstract class InterfaceWindow
    {
        protected IInterface _interface;
        public IInterface Interface
        {
            set { _interface = value; }
        }
        public InterfaceWindow(IInterface interf)
        {
            _interface = interf;
        }

        public InterfaceWindow() { }

        public virtual int DoInterface()
        {
            return _interface.TypeInterface();
        }
    }

    class InterfaceWindowText : InterfaceWindow
    {
        public InterfaceWindowText(IInterface interf) : base(interf) { }

        public InterfaceWindowText() : base() { }        
    }

    class InterfaceWindowPicture : InterfaceWindow
    {
        public InterfaceWindowPicture(IInterface interf) : base(interf) { }

        public InterfaceWindowPicture() : base() { }
    }

    class InterfaceWindowTextPicture : InterfaceWindow
    {
        public InterfaceWindowTextPicture(IInterface interf) : base(interf) { }

        public InterfaceWindowTextPicture() : base() { }
    }
}