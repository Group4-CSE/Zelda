using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using testMonogame.Interfaces;

namespace testMonogame.Rooms
{

    class Room : IRoom
    {
        const int screenX = 200;
        const int screenY = 100;

        const int mapScreenLocX = 0;
        const int mapScreenLocY = 0;

        int mapX;
        int mapY;

        const int mapXGrid = 24;
        const int mapYGrid = 12;

        //The factor that each block is scaled up by
        const int blockSizeMod = 2;
        //the base dimensions of a block square
        const int blockBaseDimension = 16;

        Texture2D texture;
        Dictionary<String, Texture2D> sprites;


        //Lists of stuff
        List<IObject> Blocks;
        List<IObject> Items;
        List<IEnemy> Enemies;
        List<IPlayerProjectile> PlayerProjectiles;
        List<IEnemyProjectile> EnemyProjectiles;

        int Background;
        bool Walls;

        Rectangle wallSourceRect = new Rectangle(0, 0, 16 * blockBaseDimension, 11 * blockBaseDimension);
        Rectangle undergroundSourceRect = new Rectangle(265, 176, 16 * blockBaseDimension, 11 * blockBaseDimension);
        Rectangle floorSourceRect = new Rectangle(272, 32, 12 * blockBaseDimension, 7 * blockBaseDimension);
        Rectangle wallDestRect = new Rectangle(screenX, screenY, 16 * blockBaseDimension * blockSizeMod, 11 * blockBaseDimension * blockSizeMod);
        Rectangle floorDestRect = new Rectangle(screenX + (2 * blockBaseDimension * blockSizeMod), screenY + (2 * blockBaseDimension * blockSizeMod),
            12 * blockBaseDimension * blockSizeMod, 7 * blockBaseDimension * blockSizeMod);


        const int finalMapWidth = 141;
        const int finalMapHeight = 69;
        Rectangle mapSourceRect = new Rectangle(0, 0, 47, 23);
        Rectangle mapDestRect = new Rectangle(0, 0, finalMapWidth, finalMapHeight);

        public Room(Texture2D inTexture, String sourceFile, Dictionary<String, Texture2D> spriteSheets)
        {
            texture = inTexture;
            sprites = spriteSheets;

            Background = 0;
            Walls = false;

            Blocks = new List<IObject>();
            Items = new List<IObject>();
            Enemies = new List<IEnemy>();
            PlayerProjectiles = new List<IPlayerProjectile>();
            EnemyProjectiles = new List<IEnemyProjectile>();

            mapX = 0;
            mapY = 0;



            loadRoomFromFile(sourceFile);


        }

        public void AddEnemyProjectile(IEnemyProjectile projectile) { EnemyProjectiles.Add(projectile); }
        public void RemoveEnemyProjectile(IEnemyProjectile projectile) { EnemyProjectiles.Remove(projectile); }
        public void AddPlayerProjectile(IPlayerProjectile projectile) { PlayerProjectiles.Add(projectile); }
        public void RemovePlayerProjectile(IPlayerProjectile projectile) { PlayerProjectiles.Remove(projectile); }

        void loadRoomFromFile(String sourceFile)
        {
            String rootDir = Directory.GetCurrentDirectory();
            rootDir = rootDir.Substring(0, rootDir.Length - 24);
            StreamReader reader = new StreamReader(rootDir + "/Content/" + sourceFile);
            //"D:/CSE 3902/3902GitHubDesktopClone/Zelda/testMonogame/testMonogame/Content/Room9.txt"
            String section = "";



            while (!reader.EndOfStream)
            {
                
                String line = reader.ReadLine();
                
                //Sections are all uppercase
                if (line.ToUpper().Equals(line) && !line.Contains(','))
                {
                    if (line.Equals("BACKGROUND"))
                    {
                        Background = 1;
                    }
                    else if (line.Equals("UNDERGROUND")){
                        Background = 2;
                    }
                    else if (line.Equals("WALLS"))
                    {
                        Walls = true;
                    }


                    section = line;
                    
                }

                //handle depending on section
                else
                {
                    
                    switch (section)
                    {
                        
                        case "BLOCKS":
                            addBlock(line);
                            break;
                        case "ITEMS":
                            addItem(line);
                            break;
                        case "ENEMIES":
                            addEnemy(line);
                            break;
                        case "DOORS":
                            addDoor(line);
                            break;
                        case "MAP":
                            addMap(line);
                            break;
                    }
                }

            }
        }
        void addMap(String line)
        {
            String[] split = line.Split(',');
            mapX = Int32.Parse(split[0]) - 1;
            mapY = Int32.Parse(split[1]) - 1;

        }
        void addDoor(String line)
        {
            float x;
            float y;
            //up, left, right, down
            String[] split = line.Split(',');
            int direction;
            IObject door;
            switch (split[1])
            {
                case "up":
                    x = screenX + (7 * blockBaseDimension * blockSizeMod) + ((blockBaseDimension * blockSizeMod) / 2);
                    y = screenY + (1 * blockBaseDimension * blockSizeMod);
                    direction = 0;
                    break;
                case "left":
                    y = screenY + (5 * blockBaseDimension * blockSizeMod);
                    x = screenX + (blockSizeMod * blockBaseDimension);
                    direction = 1;
                    break;
                case "right":
                    y = screenY + (5 * blockBaseDimension * blockSizeMod);
                    x = screenX + (blockSizeMod * blockBaseDimension * 14);
                    direction = 2;
                    break;
                case "down":
                    x = screenX + (7 * blockBaseDimension * blockSizeMod) + ((blockBaseDimension * blockSizeMod) / 2);
                    y = screenY + (9 * blockBaseDimension * blockSizeMod);
                    direction = 3;
                    break;
                default:
                    x = 0;
                    y = 0;
                    direction = 0;
                    break;
            }
            switch (split[0])
            {
                //keys temporarily set to 0. may have to do switch later to determine room number
                case "closed":
                    door = new ClosedDoor(direction, new Vector2(x, y), sprites["doors"], 0, false);
                    break;
                case "open":
                    door = new OpenDoor(direction, new Vector2(x, y), sprites["doors"], 0, false);
                    break;
                case "cave":
                    door = new CaveDoor(direction, new Vector2(x, y), sprites["doors"]);
                    break;
                case "locked":
                    door = new LockedDoor(direction, new Vector2(x, y), sprites["doors"], 0, true);
                    break;
                default:
                    door = null;
                    break;
            }
            Blocks.Add(door);
        }
        void addEnemy(String line)
        {
            String[] split = line.Split(',');
            IEnemy enemy;

            float x = ((Int32.Parse(split[1]) - 1) * blockBaseDimension * blockSizeMod) + screenX + (2 * blockBaseDimension * blockSizeMod);
            float y = ((Int32.Parse(split[2]) - 1) * blockBaseDimension * blockSizeMod) + screenY + (2 * blockBaseDimension * blockSizeMod);

            switch (split[0])
            {
                case "aquamentus":
                    enemy = new AquamentusEnemy(sprites["aquamentus"], new Vector2(x, y));
                    break;
                case "gel":
                    enemy = new GelEnemy(sprites["basicenemy"], new Vector2(x, y));
                    break;
                case "goriya":
                    enemy = new GoriyaEnemy(sprites["goriya"], sprites["PlayerProjectiles"], new Vector2(x, y));
                    break;
                case "keese":
                    enemy = new KeeseEnemy(sprites["basicenemy"], new Vector2(x, y));
                    break;
                case "oldman":
                    enemy = new OldMan(sprites["oldman"], new Vector2(x+8, y));
                    break;
                case "stalfos":
                    enemy = new StalfosEnemy(sprites["basicenemy"], new Vector2(x, y));
                    break;
                case "trap":
                    enemy = new TrapEnemy(sprites["basicenemy"], new Vector2(x, y));
                    break;
                case "wallmaster":
                    enemy = new TrapEnemy(sprites["wallmasters"], new Vector2(x, y));
                    break;
                default:
                    enemy = null;
                    break;
            }
            Enemies.Add(enemy);

        }

        void addBlock(String line)
        {
            String[] split = line.Split(',');
            IObject block;
            //Debug.WriteLine(split[0]);
            float x = ((Int32.Parse(split[1]) - 1) * blockBaseDimension * blockSizeMod) + screenX + (2 * blockBaseDimension * blockSizeMod);
            float y = ((Int32.Parse(split[2]) - 1) * blockBaseDimension * blockSizeMod) + screenY + (2 * blockBaseDimension * blockSizeMod);

            switch (split[0])
            {
                case "bluesandblock":
                    block = new BlueSandBlock(sprites["tileset"], new Vector2(x, y));
                    break;
                case "dragonblock":
                    block = new DragonBlock(sprites["tileset"], new Vector2(x, y));
                    break;
                case "dungeonblock":
                    block = new DungeonBlock(sprites["tileset"], new Vector2(x, y));
                    break;
                case "pushableblock":
                    block = new PushableBlock(sprites["tileset"], new Vector2(x, y));
                    break;
                case "stairsblock":
                    block = new StairsBlock(sprites["tileset"], new Vector2(x, y));
                    break;
                case "fireblock":
                    block = new FireBlock(sprites["fire"], new Vector2(x+8, y));
                    break;
                case "fishblock":
                    block = new FishBlock(sprites["tileset"], new Vector2(x, y));
                    break;
                case "solidblock":
                    Color c = Color.Transparent;
                    if (split[3] == "blue") c = Color.Blue;
                    block = new SolidBlock(sprites["Backgrounds"], new Vector2(x, y),c);
                    break;
                default:
                    block = new DungeonBlock(sprites["tileset"], new Vector2(x, y));
                    break;
            }
            Blocks.Add(block);

        }

        void addItem(String line)
        {
            String[] split = line.Split(',');
            IObject item;
            float x = ((Int32.Parse(split[1]) - 1) * blockBaseDimension * blockSizeMod) + screenX + (2 * blockBaseDimension * blockSizeMod);
            float y = ((Int32.Parse(split[2]) - 1) * blockBaseDimension * blockSizeMod) + screenY + (2 * blockBaseDimension * blockSizeMod);
            switch (split[0])
            {
                case "bomb":
                    item = new BombItem(sprites["itemset"], new Vector2(x, y));
                    break;
                case "boomerang":
                    item = new BoomerangItem(sprites["itemset"], new Vector2(x, y));
                    break;
                case "bow":
                    item = new BowItem(sprites["itemset"], new Vector2(x, y));
                    break;
                case "clock":
                    item = new ClockItem(sprites["itemset"], new Vector2(x, y));
                    break;
                case "compass":
                    item = new CompassItem(sprites["itemset"], new Vector2(x, y));
                    break;
                case "fairy":
                    item = new FairyItem(sprites["itemset"], new Vector2(x, y));
                    break;
                case "heart":
                    item = new HeartItem(sprites["itemset"], new Vector2(x, y));
                    break;
                case "key":
                    item = new KeyItem(sprites["itemset"], new Vector2(x, y));
                    break;
                case "map":
                    item = new MapItem(sprites["itemset"], new Vector2(x, y));
                    break;
                case "permanentheart":
                    item = new PermanentHeartItem(sprites["itemset"], new Vector2(x, y));
                    break;
                case "rupee":
                    item = new RupeeItem(sprites["itemset"], new Vector2(x, y));
                    break;
                case "triforce":
                    item = new TriforceItem(sprites["itemset"], new Vector2(x+24, y+8));
                    break;
                default:
                    item = null;
                    break;
            }
            Items.Add(item);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (Walls) spriteBatch.Draw(sprites["Backgrounds"], wallDestRect, wallSourceRect, Color.White);
            if (Background==1) spriteBatch.Draw(sprites["Backgrounds"], floorDestRect, floorSourceRect, Color.White);
            else if (Background == 2) spriteBatch.Draw(sprites["Backgrounds"], wallDestRect, undergroundSourceRect, Color.White);

            foreach (IObject block in Blocks)
            {
                block.Draw(spriteBatch);
            }
            foreach (IEnemy enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (IObject item in Items)
            {
                item.Draw(spriteBatch);
            }

            IPlayerProjectile[] arrPlayer = PlayerProjectiles.ToArray();
            foreach (IPlayerProjectile projectile in arrPlayer)
            {
                projectile.Draw(spriteBatch);
            }
            IEnemyProjectile[] arrEnemy = EnemyProjectiles.ToArray();
            foreach (IEnemyProjectile projectile in arrEnemy)
            {
                projectile.Draw(spriteBatch);
            }


            
            spriteBatch.Draw(sprites["map"], mapDestRect, mapSourceRect, Color.White);

            spriteBatch.Draw(sprites["Backgrounds"], new Rectangle((mapX * mapXGrid)+6, (mapY * mapYGrid), 9, 9), new Rectangle(40, 200, 3, 3), Color.Gray);


        }

        public void Update(Game1 game)
        {
            foreach (IObject block in Blocks)
            {
                block.Update(game);
            }
            foreach (IEnemy enemy in Enemies)
            {
                enemy.Update(game);
            }
            foreach (IObject item in Items)
            {
                item.Update(game);
            }
            IPlayerProjectile[] arrPlayer = PlayerProjectiles.ToArray();
            foreach (IPlayerProjectile projectile in arrPlayer)
            {
                projectile.Update(game);
            }
            IEnemyProjectile[] arrEnemy = EnemyProjectiles.ToArray();
            foreach (IEnemyProjectile projectile in arrEnemy)
            {
                projectile.Update(game);
            }
        }

        public List<IObject> GetBlocks()
        {
            return Blocks;
        }

        public List<IObject> GetItems()
        {
            return Items;
        }

        public List<IEnemy> GetEnemies()
        {
            return Enemies;
        }

        public List<IPlayerProjectile> GetPlayerProjectiles()
        {
            return PlayerProjectiles;
        }

        public List<IEnemyProjectile> GetEnemeyProjectile()
        {
            return EnemyProjectiles;
        }

        public void CloseRoom()
        {
            PlayerProjectiles.Clear();
            EnemyProjectiles.Clear();
        }
    }
}