using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using testMonogame.Interfaces;
using testMonogame.Rooms;

namespace testMonogame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private Texture2D aquaSheet;
        private Texture2D basicEnemy;
        private Texture2D doors;
        private Texture2D fire;
        private Texture2D goriya;
        private Texture2D itemSheet;
        private Texture2D oldMan;
        private Texture2D playerSheet;
        private Texture2D tileSet;
        private Texture2D wallmaster;
        private Texture2D playerProjectiles;
        Dictionary<String, Texture2D> sprites = new Dictionary<string, Texture2D>();

        List<ISprite> activeEnemyProjectiles = new List<ISprite>();
        List<ISprite> removedEnemyProjectiles = new List<ISprite>();
        List<ISprite> addedEnemyProjectiles = new List<ISprite>();
        List<ISprite> activePlayerProjectiles = new List<ISprite>();
        List<ISprite> removedPlayerProjectiles = new List<ISprite>();
        List<ISprite> addedPlayerProjectiles = new List<ISprite>();

        private RoomLoader roomLoad;
        Dictionary<String, IRoom> rooms = new Dictionary<string, IRoom>();
        String roomKey = "";


        EnemyObjectCollision EOCol = new EnemyObjectCollision();
        PlayerWallCollision PWCol = new PlayerWallCollision();
        EnemyWallCollision EWCol = new EnemyWallCollision();
        PlayerProjectileWallCollision PPWCol = new PlayerProjectileWallCollision();
        EnemyProjectileWallCollision EPWCol = new EnemyProjectileWallCollision();
        PlayerObjectCollision POCol = new PlayerObjectCollision();
        PlayerEnemyCollision PECol = new PlayerEnemyCollision();

        public void AddEnemyProjectile(IEnemyProjectile projectile) { rooms[roomKey].AddEnemyProjectile(projectile); }
        public void RemoveEnemyProjectile(IEnemyProjectile projectile) { rooms[roomKey].RemoveEnemyProjectile(projectile); }
        public void AddPlayerProjectile(IPlayerProjectile projectile) { rooms[roomKey].AddPlayerProjectile(projectile); }
        public void RemovePlayerProjectile(IPlayerProjectile projectile) { rooms[roomKey].RemovePlayerProjectile(projectile); }
        void removeProjectiles()
        {
            foreach (ISprite removed in removedEnemyProjectiles)
            {
                activeEnemyProjectiles.Remove(removed);
            }
            removedEnemyProjectiles.Clear();
            foreach (ISprite removed in removedPlayerProjectiles)
            {
                activePlayerProjectiles.Remove(removed);
            }
            removedPlayerProjectiles.Clear();
        }
        void addProjectiles()
        {
            foreach (ISprite removed in addedEnemyProjectiles)
            {
                activeEnemyProjectiles.Add(removed);
            }
            addedEnemyProjectiles.Clear();
            foreach (ISprite removed in addedPlayerProjectiles)
            {
                activePlayerProjectiles.Add(removed);
            }
            addedPlayerProjectiles.Clear();
        }
        public void LoadRoom(int roomNum)
        {
            String name = "Room" + roomNum;
            if (rooms.ContainsKey(name)) roomKey = name;
            else
            {
                rooms.Add(name, roomLoad.Load(name + ".txt"));
            }
            player.X = 400;
            player.Y = 324;
        }




        IPlayer player;

        IController keyController;
        IController mouseController;



        List<ISprite> blocks;
        List<ISprite> items;
        List<ISprite> enemies;

        int blockCounter;
        int itemCounter;
        int enemyCounter;

        Song song;
        Sounds sounds = new Sounds();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


        }

        public IPlayer getPlayer()
        {
            return player;

        }


        protected override void Initialize()
        {
            //test

            blockCounter = 0;
            itemCounter = 0;
            enemyCounter = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            aquaSheet = Content.Load<Texture2D>("aquamentus");
            basicEnemy = Content.Load<Texture2D>("basicenemy");
            doors = Content.Load<Texture2D>("doors");
            fire = Content.Load<Texture2D>("fire");
            goriya = Content.Load<Texture2D>("goriyav2");
            itemSheet = Content.Load<Texture2D>("itemset");
            oldMan = Content.Load<Texture2D>("oldman");
            playerSheet = Content.Load<Texture2D>("playersheet");
            tileSet = Content.Load<Texture2D>("tileset");
            wallmaster = Content.Load<Texture2D>("wallmasters");
            playerProjectiles = Content.Load<Texture2D>("PlayerProjectiles");
            Texture2D backgrounds = Content.Load<Texture2D>("Backgrounds");
            Texture2D map = Content.Load<Texture2D>("Level1Map");

            sprites.Add("aquamentus", aquaSheet);
            sprites.Add("basicenemy", basicEnemy);
            sprites.Add("doors", doors);
            sprites.Add("fire", fire);
            sprites.Add("goriya", goriya);
            sprites.Add("itemset", itemSheet);
            sprites.Add("oldman", oldMan);
            sprites.Add("playersheet", playerSheet);
            sprites.Add("tileset", tileSet);
            sprites.Add("wallmasters", wallmaster);
            sprites.Add("PlayerProjectiles", playerProjectiles);
            sprites.Add("map", map);
            sprites.Add("Backgrounds", backgrounds);

            roomLoad = new RoomLoader(sprites);
            rooms.Add("Room1", roomLoad.Load("Room1.txt"));
            roomKey = "Room1";

            blocks = new List<ISprite>();
            items = new List<ISprite>();
            enemies = new List<ISprite>();

            //Loads all of the sounds
            sounds.LoadSounds(Content);
           

            player = new Player(Content.Load<Texture2D>("playersheet"), new Vector2(500, 200), playerProjectiles, sounds);

            keyController = new KeyboardController(this);
            mouseController = new MouseController(this);


        }

        protected override void Update(GameTime gameTime)
        {

            rooms[roomKey].Update(this);

            player.Update(this);




            keyController.Update();
            mouseController.Update();

            EOCol.detectCollision(rooms[roomKey].GetEnemies(), rooms[roomKey].GetBlocks());
            PWCol.detectCollision(player, rooms[roomKey].GetWallDestRect(), rooms[roomKey].GetFloorDestRect());
            EWCol.detectCollision(rooms[roomKey].GetEnemies(), rooms[roomKey].GetWallDestRect(), rooms[roomKey].GetFloorDestRect());
            PPWCol.detectCollision(rooms[roomKey].GetPlayerProjectiles(), rooms[roomKey].GetWallDestRect(), rooms[roomKey].GetFloorDestRect(), this);
            EPWCol.detectCollision(rooms[roomKey].GetEnemeyProjectile(), rooms[roomKey].GetWallDestRect(), rooms[roomKey].GetFloorDestRect(), this);
            POCol.detectCollision(player, rooms[roomKey].GetItems(), rooms[roomKey].GetBlocks(), rooms[roomKey]);
            PECol.playerEnemyDetection(player, rooms[roomKey].GetEnemies(), rooms[roomKey]);

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();



            rooms[roomKey].Draw(_spriteBatch);

            player.Draw(_spriteBatch);






            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void s2reset()
        {
            //for sprint 2 reset
            this.Initialize();
        }

        public void cycleBlock(int incDec)
        {
            blockCounter += incDec;

            if (blockCounter == blocks.Count) blockCounter = 0;
            else if (blockCounter < 0) blockCounter = blocks.Count - 1;
        }

        public void cycleItem(int incDec)
        {
            itemCounter += incDec;


            if (itemCounter == items.Count) itemCounter = 0;
            else if (itemCounter < 0) itemCounter = items.Count - 1;
        }

        public void cycleEnemy(int incDec)
        {
            enemyCounter += incDec;

            if (enemyCounter == enemies.Count) enemyCounter = 0;
            else if (enemyCounter < 0) enemyCounter = enemies.Count - 1;

        }
    }
}
