using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarClan
{    
    public class Clan : ICloneable
    {
        public int Number { get; set; } //номер клана
        public Hero[] army { get; set; }
        public int N;   //количество героев
        int n_w; //количество воинов
        int n_d; //количество драконов
        int n_r; //количество роботов
        Random random = new Random();

        public Clan() { }

        public Clan(int nomer, int nomHero)
        {
            Number = nomer;
            N = nomHero;
            army = new Hero[N];
            n_w = N / 3;
            n_d = N / 3;
            n_r = N - n_w - n_d;
            //Name = "Klan" + nomName;
            CreatorWarrior creatorWarrior = new CreatorWarrior();
            CreatorDragon creatorDragon = new CreatorDragon();
            CreatorRobot creatorRobot = new CreatorRobot();
            Hero hero = creatorWarrior.Create();

            int n_h;
            Color color = new Color();
            n_h = random.Next(1, n_w - 1);  //определяем количество высоких воинов
            //создаем высоких воинов
            for (int i = 0; i < n_h; i++)
            {
                hero = creatorWarrior.Create(("WARR" + (i + 1)), true, (@"Picture\Warrior" + Number + "h.png"));
                hero = new OutfitHero(hero);
                army[i] = hero;
            }
            //создаем низких воинов
            for (int i = 0; i < n_w - n_h; i++)
            {
                hero = creatorWarrior.Create(("Warrior" + (n_h + i + 1)), false, (@"Picture\Warrior" + Number + "s.png"));
                hero = new OutfitHero(hero);
                army[n_h + i] = hero;
            }
            n_h = random.Next(1, n_d - 1);  //определяем количество больших драконов
            //создаем больших драконов
            for (int i = 0; i < n_h; i++)
            {
                hero = creatorDragon.Create(("DRAG" + (i + 1)), true, (@"Picture\Dragon" + Number + "h.png"));
                hero = new OutfitHero(hero);
                army[n_w + i] = hero;
            }
            //создаем маленьких драконов
            for (int i = 0; i < n_d - n_h; i++)
            {
                hero = creatorDragon.Create(("Dragon" + (n_h + i + 1)), false, (@"Picture\Dragon" + Number + "s.png"));
                hero = new OutfitHero(hero);
                army[n_w + n_h + i] = hero;
            }
            n_h = random.Next(1, n_w - 1);  //определяем количество больших роботов
            //создаем больших роботов
            for (int i = 0; i < n_h; i++)
            {
                hero = creatorRobot.Create(("ROB" + (i + 1)), true, (@"Picture\Robot" + Number + "h.png"));
                hero = new OutfitHero(hero);
                army[n_w + n_d + i] = hero;
            }
            //создаем маленьких роботов
            for (int i = 0; i < n_r - n_h; i++)
            {
                hero = creatorRobot.Create(("Robot" + (n_h + i + 1)), false, (@"Picture\Robot" + Number + "s.png"));
                hero = new OutfitHero(hero);
                army[n_w + n_d + n_h + i] = hero;
            }
        }

        //продвижение героя вперед
        public void HeroMoveForward(Hero hero)
        {
            if (Number == 1)
            {
                if (CheckCoordinate(hero.Сoordinate + N))
                    hero.Сoordinate += N;
            }
            else
            {
                if (CheckCoordinate(hero.Сoordinate - N))
                    hero.Сoordinate -= N;
            }
        }

        //продвижение героя назад
        public void HeroMoveBack(Hero hero)
        {
            if (Number == 1)
            {
                if (CheckCoordinate(hero.Сoordinate - N))
                    hero.Сoordinate -= N;
            }
            else
            {
                if (CheckCoordinate(hero.Сoordinate + N))
                    hero.Сoordinate += N;
            }
        }

        //проверка есть ли возможность перейти на такую координату на поле
        public bool CheckCoordinate(int coordinate)
        {
            if (Number == 1) //для бойцов 1-го клана
            {
                if (coordinate < N * 3 && coordinate >= N)
                    return true;
                else return false;
            }
            else //для бойцов 2-го клана
            {
                if (coordinate < N * 5 && coordinate >= N * 3)
                    return true;
                else return false;
            }
        }

        //команда отряд вперед - название отряда передается
        //с помощью string nameSquad
        public void SquadForward(string nameSquad)
        {            
            for (int i = 0; i < N; i++)
            {
                if (army[i].Name.StartsWith(nameSquad))
                {
                    //только если герой здоров, делает шаг вперед
                    if (army[i].State is HeroStateHealthy)
                        HeroMoveForward(army[i]);
                }                        
            }
        }

        //команда отряд назад - название отряда передается
        //с помощью string nameSquad
        public void SquadBack(string nameSquad)
        {
            for (int i = 0; i < N; i++)
            {
                if (army[i].Name.StartsWith(nameSquad))
                {
                    HeroMoveBack(army[i]);
                    //всем отступившим присваиватся статус - не стреляет
                    army[i].isShoot = false;
                    //для каждого отступившего запускается восстановление
                    //если он ранен
                    if (army[i].State is HeroStateWounded)                    
                    army[i].Recovery();
                }                    
            }
        }

        public void SquadAttack(string nameSquad)
        {
            for (int i = 0; i < N; i++)
            {
                if (army[i].Name.StartsWith(nameSquad))
                {
                    //герои рандомно получают статус - стрелять
                    if (random.Next(3) == 2)
                        army[i].isShoot = true;
                }
            }
        }


        public object Clone()
        {
            return new Clan() 
            {
                Number = this.Number,
                N = this.N,
                army = this.army,
                n_w = this.n_w,
                n_d = N / 3,
                n_r = N - n_w - n_d                
            };
        }
    }
}
