using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using testMonogame.Rooms;
using testMonogame.Interfaces;
namespace testMonogame
{
    public class GameManager
    {
        Game1 game;
        IPlayer player;
        private RoomLoader roomLoad;
        Dictionary<String, IRoom> rooms = new Dictionary<string, IRoom>();
        String roomKey = "";

        Dictionary<String, Texture2D> sprites = new Dictionary<string, Texture2D>();

        //collision detectors
        EnemyObjectCollision EOCol = new EnemyObjectCollision();
        PlayerWallCollision PWCol = new PlayerWallCollision();
        EnemyWallCollision EWCol = new EnemyWallCollision();
        PlayerProjectileWallCollision PPWCol = new PlayerProjectileWallCollision();
        //PlayerProjectileBlockCollision PPBCol = new PlayerProjectileBlockCollision();
        PlayerProjectileEnemyCollision PPECol = new PlayerProjectileEnemyCollision();
        //EnemyProjectileWallCollision EPWCol = new EnemyProjectileWallCollision();
        PlayerObjectCollision POCol = new PlayerObjectCollision();
        PlayerEnemyCollision PECol = new PlayerEnemyCollision();
        EnemyProjectileCollisionHandler EPCol;

        public GameManager(Game1 game, Dictionary<String, Texture2D> spriteSheet)
        {
            this.game = game;
            sprites = spriteSheet;

            //load room 1 first
            roomLoad = new RoomLoader(sprites);
            rooms.Add("Room1", roomLoad.Load("Room1.txt"));
            roomKey = "Room1";

            player = new Player(spriteSheet["playersheet"], new Vector2(500, 200), spriteSheet["PlayerProjectiles"]);
            

            EPCol = new EnemyProjectileCollisionHandler(this);

        }

        public void Update()
        {
            rooms[roomKey].Update(this);

            player.Update(this);

            EOCol.detectCollision(rooms[roomKey].GetEnemies(), rooms[roomKey].GetBlocks());
            PWCol.detectCollision(player, rooms[roomKey].GetWallDestRect(), rooms[roomKey].GetFloorDestRect());
            EWCol.detectCollision(rooms[roomKey].GetEnemies(), rooms[roomKey].GetWallDestRect(), rooms[roomKey].GetFloorDestRect());
            PPWCol.detectCollision(rooms[roomKey].GetPlayerProjectiles(), rooms[roomKey].GetWallDestRect(), rooms[roomKey].GetFloorDestRect(), this);
            //PPBCol.detectCollision(rooms[roomKey].GetPlayerProjectiles(), rooms[roomKey].GetBlocks(), this);
            PPECol.detectCollision(rooms[roomKey].GetPlayerProjectiles(), rooms[roomKey].GetEnemies(), this, rooms[roomKey]);
            //EPWCol.detectCollision(rooms[roomKey].GetEnemeyProjectile(), rooms[roomKey].GetWallDestRect(), rooms[roomKey].GetFloorDestRect(), this);
            POCol.detectCollision(player, rooms[roomKey].GetItems(), rooms[roomKey].GetBlocks(), rooms[roomKey],this);
            PECol.playerEnemyDetection(player, rooms[roomKey].GetEnemies(), rooms[roomKey]);
            EPCol.handleEnemyProjCollision(rooms[roomKey], player);
           
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            rooms[roomKey].Draw(spriteBatch);

            player.Draw(spriteBatch);
        }

        public void AddEnemyProjectile(IEnemyProjectile projectile) { rooms[roomKey].AddEnemyProjectile(projectile); }
        public void RemoveEnemyProjectile(IEnemyProjectile projectile) { rooms[roomKey].RemoveEnemyProjectile(projectile); }
        public void AddPlayerProjectile(IPlayerProjectile projectile) { rooms[roomKey].AddPlayerProjectile(projectile); }
        public void RemovePlayerProjectile(IPlayerProjectile projectile) { rooms[roomKey].RemovePlayerProjectile(projectile); }

        public void LoadRoom(int roomNum)
        {
            String name = "Room" + roomNum;
            if (rooms.ContainsKey(name)) roomKey = name;
            else
            {
                rooms.Add(name, roomLoad.Load(name + ".txt"));
                roomKey = name;
            }
            player.X = 400;
            player.Y = 324;
        }

        public IPlayer getPlayer()
        {
            return player;

        }

        public void Exit()
        {
            game.Exit();
        }

        public void s2reset()
        {
            //for sprint 2 reset
            game.s2reset();
        }
    }
}
