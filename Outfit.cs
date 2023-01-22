using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

//works pattern Decorator

namespace WarClan
{     
    
    //снаряжение
    abstract class Outfit : Hero
    {
        protected Hero hero;
        public Random random = new Random();
        public Color color1 = new Color();       
        public Outfit(string name, bool height, string pict, Hero _hero) : base (name, height, pict)
        {
            hero = _hero;            
        }
    }


    class OutfitHero : Outfit
    {
        public OutfitHero(Hero h) : base(h.Name, h.Height, h.Picture, h)       
        {
            AddOutfit();
        }

        public override void AddOutfit()
        { 
            //раздаем оружие в зависимости от наименования героя
            if (hero.Name.StartsWith("WARR"))                    
                Weapon = "Sword";
            if (hero.Name.StartsWith("Warrior"))
                Weapon = "Club";
            if (hero.Name.StartsWith("DRAG"))
                Weapon = "Mag.ring";
            if (hero.Name.StartsWith("Dragon"))
                Weapon = "Mag.flute";
            if (hero.Name.StartsWith("ROB"))
                Weapon = "Laser gun";
            if (hero.Name.StartsWith("Robot"))
                Weapon = "Lightsaber";

            //рандомно создаются цвет и количество жизней
            NumberLives = random.Next(3, 5);
            ColorBrush = color1.colorBrush[random.Next(color1.colorBrush.Length)];
            Thread.Sleep(100);
        }
    }
}
