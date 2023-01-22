using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

//works pattern Factory Method 

namespace WarClan
{
    public class Color
    {
        public Brush[] colorBrush = new Brush[]
        {
            Brushes.Turquoise, Brushes.Yellow, Brushes.DodgerBlue, Brushes.Lavender,
            Brushes.LightPink, Brushes.LightSeaGreen, Brushes.LightYellow,
            Brushes.Chocolate, Brushes.CornflowerBlue, Brushes.YellowGreen,
            Brushes.Orchid, Brushes.LightSkyBlue, Brushes.LightSalmon, Brushes.DarkGoldenrod
        };
    }

    //абстрактный класс создатель героя
    abstract class CreatorHero
    {
        public string Name { get; set; }

        public CreatorHero()
        {
            Name = "";
        }

        public CreatorHero(string name)
        {
            Name = name;
        }

        abstract public Hero Create();

        abstract public Hero Create(string name, bool height, string pic);


    }

    //создатель воинов
    class CreatorWarrior : CreatorHero
    {
        public CreatorWarrior(string n) : base(n)  { }

        public CreatorWarrior() { }

        public override Hero Create() 
        {
            return new Warrior();
        }

        public override Hero Create(string name, bool height, string pict)
        {
            return new Warrior(name, height, pict);
        }
    }

    //создатель драконов
    class CreatorDragon : CreatorHero
    {
        public CreatorDragon(string n) : base(n) { }

        public CreatorDragon() { }

        public override Hero Create()
        {
            return new Dragon();
        }

        public override Hero Create(string name, bool height, string pict)
        {
            return new Dragon(name, height, pict);
        }
    }

    //создатель роботов
    class CreatorRobot : CreatorHero
    {
        public CreatorRobot(string n) : base(n)  { }

        public CreatorRobot() { }

        public override Hero Create()
        {
            return new Robot();
        }

        public override Hero Create(string name, bool height, string pict)//, Brush color)
        {
            return new Robot(name, height, pict);//, color);
        }
    }

 

    //абстрактный класс герой
    public abstract class Hero 
    {
        public string Name { get; set; } //задается конструктором
        public bool Height { get; set; } //True-high, False - short, задается конструктором
        public string Picture { get; set; } //ссылка на картинку, задается контруктором
        public Brush ColorBrush { get; set; } //цвет, добавл. декоратором 
        public string Weapon { get; set; } //оружие, добавл. декоратором 
        public int NumberLives { get; set; } //количество жизней, добавл. декоратором 
        public int Сoordinate { get; set; } //координаты на поле боя, меняется в процессе игры       
        public bool isShoot { get; set; } //статус - стреляет герой или нет, по умолчанию нет

        public IHeroState State { get; set; }  //pattern State

        public Hero() { }

        public Hero(string name, bool height, string pict)
        {
            Name = name;
            Height = height;
            Picture = pict;
            State = new HeroStateHealthy(); //по умолчанию здоров
            isShoot = false; //по умолчанию не стреляет
        }     
            
        public virtual void Recovery()
        {
            State.Recovery(this);
        }
        public virtual void Wounding()
        {
            State.Wounding(this);
        }

        public override string ToString()
        {
            return Name + " " + Сoordinate;
        }

        public virtual void AddOutfit()  {} //добавление снаряжения
        public virtual void AddCoordinate(int n) { } //добавление координат       
    }

    //класс воин
    class Warrior : Hero
    {   
        public Warrior() {}

        public Warrior(string name, bool height, string pict) :
            base(name, height, pict)  
        {
            State = new HeroStateHealthy(); //по умолчанию здоров
            isShoot = false; //по умолчанию не стреляет
        }
        public override void Recovery()
        {
            State.Recovery(this);
        }
        public override void Wounding()
        {
            State.Wounding(this);
        }
    }

    //класс дракон
    class Dragon : Hero
    {
        public Dragon() {}

        public Dragon(string name, bool height, string pict) :
            base(name, height, pict)     
        {
            State = new HeroStateHealthy(); //по умолчанию здоров
            isShoot = false; //по умолчанию не стреляет
        }
        public override void Recovery()
        {
            State.Recovery(this);
        }
        public override void Wounding()
        {
            State.Wounding(this);
        }
    }

    //класс робот
    class Robot : Hero
    {
        public Robot() {}

        public Robot(string name, bool height, string pict) :
            base(name, height, pict)   
        {
            State = new HeroStateHealthy(); //по умолчанию здоров
            isShoot = false; //по умолчанию не стреляет
        }
        public override void Recovery()
        {
            State.Recovery(this);
        }
        public override void Wounding()
        {
            State.Wounding(this);
        }
    }    
}

