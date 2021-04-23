using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace testMonogame
{
    public class ESpawner
    {
        public Texture2D texture;
        public Vector2 pos;
        public Vector2 velocity;
        public GameManager Spawngame1;
        Dictionary<String, Texture2D> sprites;
        List<IEnemy> enemiesSpawn = new List<IEnemy>();

        //Name of enemy
        String eType;
        //In case we want to add random enemies to horde
        //Random random = new Random();
        //Spawn point for all new spawned enemies
        float x = 400, y = 200;

        /*
         Note: Game.IsHorde() is called on Room.cs, this might cause problems if want to
         add more features, but right now it works fine.
         */

        public ESpawner(GameManager game, List<IEnemy> Enemies, String enemyType, Dictionary<String, Texture2D> spritesSheet)
        {
            //List of Enemies
            enemiesSpawn = Enemies;
            //Specifc Enemy for creating more spawns of
            eType = enemyType;
            sprites = spritesSheet;

        }
        //Spawn timer
        float spawn = 0;

        public void Update()
        {
            spawn += 1;
            if (spawn >= 1)
            {
                spawn = 0;
                //Max 10 in a room
                if (enemiesSpawn.Count() < 10)
                {
                    switch (eType)
                    {
                        case "aquamentus":
                            enemiesSpawn.Add(new AquamentusEnemy(sprites["aquamentus"], new Vector2(x, y)));
                            break;
                        case "gel":
                            enemiesSpawn.Add(new GelEnemy(sprites["basicenemy"], new Vector2(x, y)));
                            break;
                        case "goriya":
                            enemiesSpawn.Add(new GoriyaEnemy(sprites["goriya"], sprites["PlayerProjectiles"], new Vector2(x, y)));
                            break;
                        case "keese":
                            enemiesSpawn.Add(new KeeseEnemy(sprites["basicenemy"], new Vector2(x, y)));
                            break;
                        case "oldman":
                            enemiesSpawn.Add(new OldMan(sprites["oldman"], new Vector2(x + 8, y)));
                            break;
                        case "stalfos":
                            enemiesSpawn.Add(new StalfosEnemy(sprites["basicenemy"], new Vector2(x, y)));
                            break;
                        case "trap":
                            enemiesSpawn.Add(new TrapEnemy(sprites["basicenemy"], new Vector2(x, y)));
                            break;
                        case "wallmaster":
                            enemiesSpawn.Add(new WallmasterEnemy(sprites["wallmasters"], new Vector2(x, y)));
                            break;
                        default:
                            break;
                    }
                }
            }
            }

    }
}
