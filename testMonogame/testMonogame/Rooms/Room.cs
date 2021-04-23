using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;
using testMonogame.Interfaces;

namespace testMonogame.Rooms
{

    class Room : IRoom
    {

        public int screenX { get; set; }
        public int screenY { get; set; }

        const int mapScreenLocX = 0;
        const int mapScreenLocY = 0;

        int mapX;
        int mapY;
        int mapOffsetX;
        int mapOffsetY;


        //room conditions
        Rectangle bombRectangle;
        Rectangle blockRectangle;
        bool hiddenItems;

        const int mapXGrid = 32;
        const int mapYGrid = 16;

        //The factor that each block is scaled up by
        const int blockSizeMod = 2;
        //the base dimensions of a block square
        const int blockBaseDimension = 16;

        Dictionary<String, Texture2D> sprites;


        //Lists of stuff
        List<IObject> Blocks;
        List<IObject> Items;
        List<IObject> HiddenItemList;
        List<IEnemy> Enemies;
        List<IPlayerProjectile> PlayerProjectiles;
        List<IEnemyProjectile> EnemyProjectiles;

        int Background;
        bool Walls;

        Rectangle wallSourceRect = new Rectangle(0, 0, 16 * blockBaseDimension, 11 * blockBaseDimension);
        Rectangle undergroundSourceRect = new Rectangle(265, 176, 16 * blockBaseDimension, 11 * blockBaseDimension);
        Rectangle floorSourceRect = new Rectangle(272, 32, 12 * blockBaseDimension, 7 * blockBaseDimension);
        Rectangle floor2SourceRect = new Rectangle(560, 194, 12 * blockBaseDimension, 7 * blockBaseDimension);
        Rectangle wallDestRect;
        Rectangle floorDestRect;


        //transition stuff
        //save initial positions for transitions
        Rectangle originalWall;
        Rectangle originalFloor;

        Boolean isTransition;

        ESpawner EnemySpawner;

        public Room(int inMapX, int inMapY, int inBG, bool inWalls,
            Dictionary<String, Texture2D> spriteSheets,
            List<IObject> inBlocks,
            List<IObject> inItems,
            List<IEnemy> inEnemies,
            Rectangle inBombRectangle,
            Rectangle inBlockRectangle,
            bool inHideItems,
            ESpawner eSpawner
            )
        {

            bombRectangle = inBombRectangle;
            blockRectangle = inBlockRectangle;
            hiddenItems = inHideItems;
            EnemySpawner = eSpawner;

            screenX = 130;
            screenY = 110;

            wallDestRect = new Rectangle(screenX, screenY, 16 * blockBaseDimension * blockSizeMod, 11 * blockBaseDimension * blockSizeMod);
            floorDestRect = new Rectangle(screenX + (2 * blockBaseDimension * blockSizeMod), screenY + (2 * blockBaseDimension * blockSizeMod),
               12 * blockBaseDimension * blockSizeMod, 7 * blockBaseDimension * blockSizeMod);

            originalFloor = new Rectangle(screenX + (2 * blockBaseDimension * blockSizeMod), screenY + (2 * blockBaseDimension * blockSizeMod),
                12 * blockBaseDimension * blockSizeMod, 7 * blockBaseDimension * blockSizeMod);
            originalWall = new Rectangle(screenX, screenY, 16 * blockBaseDimension * blockSizeMod, 11 * blockBaseDimension * blockSizeMod);

            sprites = spriteSheets;

            Background = inBG;
            Walls = inWalls;

            Blocks = inBlocks;
            if (!hiddenItems) Items = inItems;
            else
            {
                HiddenItemList = inItems;
                Items = new List<IObject>();
            }
            Enemies = inEnemies;
            PlayerProjectiles = new List<IPlayerProjectile>();
            EnemyProjectiles = new List<IEnemyProjectile>();

            mapX = inMapX;
            mapY = inMapY;

            isTransition = false;


        }

        public IObject getDrops(IEnemy enemy)
        {
            Random randomNumber = new Random();
            int dropNum = randomNumber.Next(18);
            int[] rupee = { 1, 3, 6, 7 };
            int[] bomb = { 0, 5, 8 };
            int[] heart = { 2, 4, 9 };
            Rectangle enemyRect = enemy.getDestRect();
            Vector2 position = new Vector2(enemyRect.X, enemyRect.Y);
            Texture2D itemSprites = sprites["itemset"];
            IObject drop = null;


            if (enemy.getHealth() <= 0)
            {
                if (rupee.Contains(dropNum))
                {
                    drop = new RupeeItem(itemSprites, position);
                }
                else if (bomb.Contains(dropNum))
                {
                    drop = new BombItem(itemSprites, position);
                }
                else if (heart.Contains(dropNum))
                {
                    drop = new HeartItem(itemSprites, position);
                }
            }

            return drop;
        }

        public void AddEnemyProjectile(IEnemyProjectile projectile) { EnemyProjectiles.Add(projectile); }
        public void RemoveEnemyProjectile(IEnemyProjectile projectile) { EnemyProjectiles.Remove(projectile); }
        public void AddPlayerProjectile(IPlayerProjectile projectile) { PlayerProjectiles.Add(projectile); }
        public void RemovePlayerProjectile(IPlayerProjectile projectile) { PlayerProjectiles.Remove(projectile); }
        public void RemoveItem(IObject item) { Items.Remove(item); }
        public void RemoveEnemy(IEnemy enemy)
        {
            IObject drop = getDrops(enemy);
            if (drop != null)
            {
                Items.Add(drop);
            }
            Enemies.Remove(enemy);
        }


        public void Draw(SpriteBatch spriteBatch)
        {

            if (Walls) spriteBatch.Draw(sprites["Backgrounds"], wallDestRect, wallSourceRect, Color.White);
            if (Background == 1) spriteBatch.Draw(sprites["Backgrounds"], floorDestRect, floorSourceRect, Color.White);
            else if (Background == 2) spriteBatch.Draw(sprites["Backgrounds"], wallDestRect, undergroundSourceRect, Color.White);
            else if (Background == 3) spriteBatch.Draw(sprites["Backgrounds"], floorDestRect, floor2SourceRect, Color.White);

            //draw blocks
            foreach (IObject block in Blocks)
            {
                block.Draw(spriteBatch);
            }

            if (isTransition)
            {
                return;
            }

            //draw enemies
            foreach (IEnemy enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }

            //draw items
            foreach (IObject item in Items)
            {
                item.Draw(spriteBatch);
            }


            //spriteBatch.Draw(sprites["Backgrounds"], bombRectangle, new Rectangle(40, 200, 3, 3), Color.Orange);
            IPlayerProjectile[] arrPlayer = PlayerProjectiles.ToArray();

            //draw projectiles
            foreach (IPlayerProjectile projectile in arrPlayer)
            {
                projectile.Draw(spriteBatch);
            }
            IEnemyProjectile[] arrEnemy = EnemyProjectiles.ToArray();
            foreach (IEnemyProjectile projectile in arrEnemy)
            {
                projectile.Draw(spriteBatch);
            }



            //spriteBatch.Draw(sprites["map"], mapDestRect, mapSourceRect, Color.White);
            spriteBatch.Draw(sprites["Backgrounds"], new Rectangle(mapOffsetX + (mapX * mapXGrid) + (mapXGrid / 4), mapOffsetY + (mapY * mapYGrid), (mapXGrid - 5) / 2, mapYGrid - 4), new Rectangle(40, 200, 3, 3), Color.Gray);


        }

        public void Update(GameManager game, GameTime gametime)
        {
            if (isTransition)
            {
                return;
            }

            foreach (IObject block in Blocks)
            {
                block.Update(game);
                if (block.getDestRect().Intersects(blockRectangle))
                {
                    foreach (IObject door in Blocks)
                    {
                        if (door is IDoor && door is ClosedDoor)
                        {
                            IDoor d = (IDoor)door;
                            //Debug.WriteLine("open");
                            d.openDoor();
                            
                        }
                    }
                }
            }
            foreach (IEnemy enemy in Enemies)
            {
                enemy.Update(game);
            }

            //TODO: Wrap this in bool to switch to hordemode
            if (game.IsHorde())
            {
                EnemySpawner.Update();
            }
            

            if (hiddenItems && Enemies.Count == 0)
            {
                hiddenItems = false;
                foreach (IObject item in HiddenItemList)
                {
                    Items.Add(item);
                }
                HiddenItemList.Clear();
            }
            foreach (IObject item in Items)
            {
                item.Update(game);
            }



            IPlayerProjectile[] arrPlayer = PlayerProjectiles.ToArray();
            foreach (IPlayerProjectile projectile in arrPlayer)
            {
                projectile.Update(game);
                if (projectile is ExplosionPlayerProjectile && projectile.getDestRect().Intersects(bombRectangle))
                {
                    //Debug.WriteLine("BOOM");
                    foreach (IObject door in Blocks)
                    {
                        if (door is IDoor && door is CaveDoor)
                        {
                            IDoor d = (IDoor)door;
                            d.openDoor();
                            
                        }
                    }
                }
            }
            IEnemyProjectile[] arrEnemy = EnemyProjectiles.ToArray();
            foreach (IEnemyProjectile projectile in arrEnemy)
            {
                projectile.Update(game);
            }
            mapOffsetX = game.getHUD().hudX;
            mapOffsetY = game.getHUD().hudY;
        }

        public List<IObject> GetBlocks()
        {
            return Blocks;
        }
        public Rectangle GetWallDestRect()
        {
            return wallDestRect;
        }
        public Rectangle GetFloorDestRect()
        {
            return floorDestRect;
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

        public void shiftBlocks(int x, int y)
        {
            //shift all the blocks by x and y value
            foreach(IObject obj in Blocks)
            {
                IBlock block = (IBlock)obj;
                block.transitionShift(x, y);
            }
        }

        public void resetBlocks()
        {
            //reset all blocks to original positions
            foreach (IObject obj in Blocks)
            {
                IBlock block = (IBlock)obj;
                block.resetToOriginalPos();
            }
        }

        public void setTransitionSide(int side)
        {

            switch (side)
            {
                case 5:
                    //do same as case 0 -- case 5 only happens in room 3 stairs
                case 0:
                    //handle north transition, put room on top of the old room 
                    floorDestRect.Y = floorDestRect.Y - (11 * blockBaseDimension * blockSizeMod);
                    wallDestRect.Y = wallDestRect.Y - (11 * blockBaseDimension * blockSizeMod);
                    shiftBlocks(0, -(11 * blockBaseDimension * blockSizeMod));
                    break;
                case 1:
                    //handle west transition, put room left of the old room 
                    floorDestRect.X = floorDestRect.X - (16 * blockBaseDimension * blockSizeMod);
                    wallDestRect.X = wallDestRect.X - (16 * blockBaseDimension * blockSizeMod);
                    shiftBlocks(-(16 * blockBaseDimension * blockSizeMod), 0);
                    break;
                case 2:
                    //handle east transition, put room right of the old room 
                    floorDestRect.X = floorDestRect.X + (16 * blockBaseDimension * blockSizeMod);
                    wallDestRect.X = wallDestRect.X + (16 * blockBaseDimension * blockSizeMod);
                    shiftBlocks((16 * blockBaseDimension * blockSizeMod), 0);
                    break;
                case 4:
                    //do same as case 3 -- case 4 only happens for room 1 stairs
                case 3:
                    //handle south transition, put room bellow the old room 
                    floorDestRect.Y = floorDestRect.Y + (11 * blockBaseDimension * blockSizeMod);
                    wallDestRect.Y = wallDestRect.Y + (11 * blockBaseDimension * blockSizeMod);
                    shiftBlocks(0, (11 * blockBaseDimension * blockSizeMod));
                    break;
            }
        }
        public void setTransitioning(Boolean transition)
        {
            this.isTransition = transition;
        }
        public Boolean isTransitioning()
        {
            return isTransition;
        }
        public void transitionShift(int x, int y)
        {
            //shift floor and walls based on incoming x,y
            floorDestRect.X = floorDestRect.X + x;
            floorDestRect.Y = floorDestRect.Y + y;
            wallDestRect.X = wallDestRect.X + x;
            wallDestRect.Y = wallDestRect.Y + y;

            //shift blocks with the x,y
            shiftBlocks(x, y);

            //if room is shifted back to original position, transition is complete
            if (floorDestRect.X == originalFloor.X && floorDestRect.Y == originalFloor.Y)
            {
                isTransition = false;
            }
        }
        public void resetToOriginalPos()
        {
            //reset room back to original position after a transition shift
            floorDestRect.X = originalFloor.X;
            floorDestRect.Y = originalFloor.Y;
            wallDestRect.X = originalWall.X;
            wallDestRect.Y = originalWall.Y;

            //reset blocks
            resetBlocks();

        }

        public void isShiftDone()
        {
            if (floorDestRect.X == originalFloor.X && floorDestRect.Y == originalFloor.Y)
            {
                isTransition = false;
            }
        }
    }
}