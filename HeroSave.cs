using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WarClan
{
    public class HeroSave
    {    
        public int NumberLives { get; set; }
        public int Сoordinate { get; set; }
        public bool isShoot { get; set; }
        public IHeroState State { get; set; }

        public HeroSave(Hero hero)
        {
            NumberLives = hero.NumberLives;
            Сoordinate = hero.Сoordinate;
            isShoot = hero.isShoot;
            State = hero.State;
        }

        //из объекта heroSave переносятся данные в объект hero
        public void Restore(HeroSave heroSave, Hero hero)
        {
            hero.NumberLives = heroSave.NumberLives;
            hero.Сoordinate = heroSave.Сoordinate;
            hero.isShoot = heroSave.isShoot;
            hero.State = heroSave.State;
        }
                
        //восстановление данных из hero в heroSave
        public void ArmyRestore(Hero[] army, HeroSave[] armySave)
        {
            int n = army.Length;           

            for (int i = 0; i < n; i++)
            {
                Restore(armySave[i], army[i]);
            }
        }
    }

    public class ArmySave
    {
        HeroSave[] armySave { set; get; }

        public ArmySave(Hero[] army)
        {
            int n = army.Length;
            armySave = new HeroSave[n];
            for (int i = 0; i < n; i++)
            {
                armySave[i] = new HeroSave(army[i]);
            }
        }

        public void ArmyRestore(Hero[] army)
        {
            int n = army.Length;

            for (int i = 0; i < n; i++)
            {
                armySave[i].Restore(armySave[i], army[i]);
            }
        }
    }
}
