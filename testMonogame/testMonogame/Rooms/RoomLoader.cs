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
    class RoomLoader
    {
        int Background;
        bool Walls;
        int mapX;
        int mapY;
        Dictionary<String, Texture2D> sprites;
        List<IObject> Blocks= new List<IObject>();
        List<IObject> Items= new List<IObject>();
        List<IEnemy> Enemies=new List<IEnemy>();

        const int screenX = 150;
        const int screenY = 70;
        //The factor that each block is scaled up by
        const int blockSizeMod = 2;
        //the base dimensions of a block square
        const int blockBaseDimension = 16;

        public RoomLoader( Dictionary<String,Texture2D> spriteSheet)
        {
            
            sprites = spriteSheet;
        }

        public Room Load(String sourceFile)
        {
            Blocks = new List<IObject>();
            Items = new List<IObject>();
            Enemies = new List<IEnemy>();
            mapX = 0;
            mapY = 0;
            Background = 0;
            Walls = false;
            loadFromFile(sourceFile);
            return new Room(mapX, mapY, Background, Walls, sprites, Blocks, Items, Enemies);
        }
        void loadFromFile(String sourceFile)
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
                    else if (line.Equals("UNDERGROUND"))
                    {
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
                    x = screenX + (6 * blockBaseDimension * blockSizeMod) + ((blockBaseDimension * blockSizeMod));
                    y = screenY; 
                    direction = 0;
                    break;
                case "left":
                    y = screenY + (5 * blockBaseDimension * blockSizeMod) - blockBaseDimension;
                    x = screenX;
                    direction = 1;
                    break;
                case "right":
                    y = screenY + (5 * blockBaseDimension * blockSizeMod)-blockBaseDimension;
                    x = screenX + (blockSizeMod * blockBaseDimension * 14);
                    direction = 2;
                    break;
                case "down":
                    x = screenX + (6 * blockBaseDimension * blockSizeMod) + ((blockBaseDimension * blockSizeMod));
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
                    enemy = new OldMan(sprites["oldman"], new Vector2(x + 8, y));
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
                    block = new PushableBlock(sprites["tileset"], new Vector2(x, y),split[3]);
                    break;
                case "stairsblock":
                    block = new StairsBlock(sprites["tileset"], new Vector2(x, y));
                    break;
                case "fireblock":
                    block = new FireBlock(sprites["fire"], new Vector2(x + 8, y));
                    break;
                case "fishblock":
                    block = new FishBlock(sprites["tileset"], new Vector2(x, y));
                    break;
                case "solidblock":
                    Color c = Color.Transparent;
                    if (split[3] == "blue") c = Color.Blue;
                    block = new SolidBlock(sprites["Backgrounds"], new Vector2(x, y), c);
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
                    item = new TriforceItem(sprites["itemset"], new Vector2(x + 24, y + 8));
                    break;
                default:
                    item = null;
                    break;
            }
            Items.Add(item);
        }
    }
}
