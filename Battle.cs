using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarClan
{
    public class Battle
    {
        int N;
        Clan clan1, clan2;

        public Battle (Clan _clan1, Clan _clan2)
        {
            N = _clan1.N;
            clan1 = _clan1;
            clan2 = _clan2;           
        }

        //проверка, находится ли герой 1-го клана на 3-й боевой линии
        bool CheckPosition_1(Hero hero)
        {
            if (hero.Сoordinate >= 2 * N && hero.Сoordinate < 3 * N)
                return true;
            else
                return false;
        }

        //проверка, находится ли герой 4-го клана на 4-й боевой линии
        bool CheckPosition_2(Hero hero)
        {
            if (hero.Сoordinate >= 3 * N && hero.Сoordinate < 4 * N)
                return true;
            else
                return false;
        }

        public void StartBattle()
        {
            for (int i = 0; i < N; i++)
            {
                //проверяем, находятся ли воины i-го номера на боевых линиях
                if (CheckPosition_1(clan1.army[i]) == true &&
                    CheckPosition_2(clan2.army[i]) == true)

                //если да, то они вступают в сражение
                {
                    //если у героя первого клана статус - стрелять
                    if (clan1.army[i].isShoot)
                    {
                        //для героя второго клана запускается процедура ранение
                        clan2.army[i].Wounding();

                        clan1.army[i].isShoot = false;
                    }


                    //если у героя второго клана статус - стрелять
                    if (clan2.army[i].isShoot)
                    { 
                        //для героя первого клана запускается процедура ранение
                        clan1.army[i].Wounding();

                        clan2.army[i].isShoot = false;
                    }
                }
            }            
        }
    }
}
