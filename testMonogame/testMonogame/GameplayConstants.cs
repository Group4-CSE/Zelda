using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame
{
    public static class GameplayConstants
    {

        public static int STARTING_HEALTH;
        public static double PLAYER_TAKE_DAMAGE_MODIFIER;
        public static double PLAYER_DEAL_DAMAGE_MODIFIER;
        public static int PLAYER_STARTING_RUPEES;
        public static int PLAYER_STARTING_BOMBS;
        public static double PLAYER_SPEED_MODIFIER;
        public static double ENEMY_SPEED_MODIFIER;


        //EASY
        const int easyHealth = 5 * 4;//5 hearts
        const double easyTakeDamageMod = 0.5;//.5x damage on easy
        const double easyDealDamageMod = 2.0;//2x damage dealt on easy
        const int easyRupees = 150;//start with 150 rupees on easy
        const int easyBombs = 10;//start with 10 bombs on easy
        const double easyPlayerSpeed = 3.0;//player moves 3x speed on easy
        const double easyEnemySpeed = 1.0;//enemies move 1x as fast on easy
        //NORMAL
        const int normalHealth = 3 * 4;//3 hearts
        const double normalTakeDamageMod = 1.0;//1x damage on normal
        const double normalDealDamageMod = 1.0;//1x damage dealt on normal
        const int normalRupees = 50;//start with 50 rupees on normal
        const int normalBombs = 3;//start with 10 bombs on normal
        const double normalPlayerSpeed = 2.0;//player moves 2x speed on normal
        const double normalEnemySpeed = 1.0;//enemies move 1x as fast on normal
        //HARD
        const int hardHealth = 2 * 4;//2 hearts
        const double hardTakeDamageMod = 2.0;//2x damage on hard
        const double hardDealDamageMod = 0.5;//.5x damage dealt on hard
        const int hardRupees = 10;//start with 10 rupees on hard
        const int hardBombs = 1;//start with 1 bombs on hard
        const double hardPlayerSpeed = 1.0;//player moves 1x speed on hard
        const double hardEnemySpeed = 2.0;//enemies move 2x as fast on hard



        //initialize constants to values depending on difficulty
        public static void Initialize(int difficulty)
        {
            //0=easy, 1=normal,2=hard
            if (difficulty == 0)
            {
                STARTING_HEALTH = easyHealth;
                PLAYER_TAKE_DAMAGE_MODIFIER = easyTakeDamageMod;
                PLAYER_DEAL_DAMAGE_MODIFIER = easyDealDamageMod;
                PLAYER_STARTING_RUPEES = easyRupees;
                PLAYER_STARTING_BOMBS = easyBombs;
                PLAYER_SPEED_MODIFIER = easyPlayerSpeed;
                ENEMY_SPEED_MODIFIER = easyEnemySpeed;
                
            }
            else if (difficulty == 1)
            {
                STARTING_HEALTH = normalHealth;
                PLAYER_TAKE_DAMAGE_MODIFIER = normalTakeDamageMod;
                PLAYER_DEAL_DAMAGE_MODIFIER = normalDealDamageMod;
                PLAYER_STARTING_RUPEES = normalRupees;
                PLAYER_STARTING_BOMBS = normalBombs;
                PLAYER_SPEED_MODIFIER = normalPlayerSpeed;
                ENEMY_SPEED_MODIFIER = normalEnemySpeed;
            }
            else if (difficulty == 2)
            {
                STARTING_HEALTH = hardHealth;
                PLAYER_TAKE_DAMAGE_MODIFIER = hardTakeDamageMod;
                PLAYER_DEAL_DAMAGE_MODIFIER = hardDealDamageMod;
                PLAYER_STARTING_RUPEES = hardRupees;
                PLAYER_STARTING_BOMBS = hardBombs;
                PLAYER_SPEED_MODIFIER = hardPlayerSpeed;
                ENEMY_SPEED_MODIFIER = hardEnemySpeed;
            }
        }
    }
}
