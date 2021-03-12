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
        const int screenX = 150;
        const int screenY = 70;

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

        public Room(int inMapX, int inMapY, int inBG, bool inWalls, 
            Dictionary<String, Texture2D> spriteSheets, 
            List<IObject> inBlocks,
            List<IObject> inItems,
            List<IEnemy> inEnemies)
        {
            sprites = spriteSheets;

            Background = inBG;
            Walls = inWalls;

            Blocks = inBlocks;
            Items = inItems;
            Enemies = inEnemies;
            PlayerProjectiles = new List<IPlayerProjectile>();
            EnemyProjectiles = new List<IEnemyProjectile>();

            mapX = inMapX;
            mapY = inMapY;



        }

        public void AddEnemyProjectile(IEnemyProjectile projectile) { EnemyProjectiles.Add(projectile); }
        public void RemoveEnemyProjectile(IEnemyProjectile projectile) { EnemyProjectiles.Remove(projectile); }
        public void AddPlayerProjectile(IPlayerProjectile projectile) { PlayerProjectiles.Add(projectile); }
        public void RemovePlayerProjectile(IPlayerProjectile projectile) { PlayerProjectiles.Remove(projectile); }

       


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
        public Rectangle GetWallDestRect()
        {
            return wallDestRect;
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