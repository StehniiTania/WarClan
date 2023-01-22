using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//pattern State

namespace WarClan
{    
    public interface IHeroState
    {
        void Recovery(Hero hero);  //восстановление
        void Wounding(Hero hero);  //нанесение ран
    }

    //здоров
    class HeroStateHealthy : IHeroState
    {
        public void Recovery(Hero hero)
        {
            //накапливаем силы, но ничего не происходит
        }
        public void Wounding(Hero hero)
        {
            hero.NumberLives -= 1;
            if (hero.NumberLives == 2)
                hero.State = new HeroStateWounded();
        }        
    }

    //ранен
    class HeroStateWounded : IHeroState
    {
        public void Recovery(Hero hero)
        {
            if (hero.NumberLives < 5) //герой не может иметь больше 5-ти жизней
                hero.NumberLives += 1;

            if (hero.NumberLives == 3) 
                hero.State = new HeroStateHealthy();
        }
        public void Wounding(Hero hero)
        {
            hero.NumberLives -= 1;
            if (hero.NumberLives == 0)
                hero.State = new HeroStateDied();
        }
    }

    //погиб
    class HeroStateDied : IHeroState
    {
        public void Recovery(Hero hero)
        {
            //уже ничего не поможет
        }
        public void Wounding(Hero hero)
        {
            //куда уже хуже
        }
    }
}
