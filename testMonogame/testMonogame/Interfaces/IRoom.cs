using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace testMonogame.Interfaces
{
    public interface IRoom
    {
        public int screenX { get; set; }
        public int screenY { get; set; }
        public void Draw(SpriteBatch spriteBatch);
        public void Update(GameManager game, GameTime gameTime);

        public List<IObject> GetBlocks();
        public List<IObject> GetItems();
        public List<IEnemy> GetEnemies();
        public List<IPlayerProjectile> GetPlayerProjectiles();
        public List<IEnemyProjectile> GetEnemeyProjectile();
        public Rectangle GetWallDestRect();
        public Rectangle GetFloorDestRect();

        public void CloseRoom();
        public void AddEnemyProjectile(IEnemyProjectile projectile);
        public void RemoveEnemyProjectile(IEnemyProjectile projectile);
        public void AddPlayerProjectile(IPlayerProjectile projectile);
        public void RemovePlayerProjectile(IPlayerProjectile projectile);
        public void RemoveItem(IObject item);
        public void RemoveEnemy(IEnemy enemy);

        public void setTransitionSide(int side);
        public void setTransitioning(Boolean transition);
        public Boolean isTransitioning();
        public void transitionShift(int x, int y);
        public void resetToOriginalPos();
        public void isShiftDone();
    }
}
