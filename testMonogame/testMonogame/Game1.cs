using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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

        //private IController keyboard;

        List<ISprite> activeEnemyProjectiles= new List<ISprite>();
        List<ISprite> removedEnemyProjectiles = new List<ISprite>();
        List<ISprite> addedEnemyProjectiles = new List<ISprite>();
        List<ISprite> activePlayerProjectiles = new List<ISprite>();
        List<ISprite> removedPlayerProjectiles = new List<ISprite>();
        List<ISprite> addedPlayerProjectiles = new List<ISprite>();




        public void AddEnemyProjectile(ISprite projectile){ addedEnemyProjectiles.Add(projectile); }
        public void RemoveEnemyProjectile(ISprite projectile) { removedEnemyProjectiles.Add(projectile); }
        public void AddPlayerProjectile(ISprite projectile) { addedPlayerProjectiles.Add(projectile); }
        public void RemovePlayerProjectile(ISprite projectile) { removedPlayerProjectiles.Add(projectile); }
        void removeProjectiles()
        {
            foreach(ISprite removed in removedEnemyProjectiles)
            {
                activeEnemyProjectiles.Remove(removed);
            }
            removedEnemyProjectiles.Clear();
            foreach (ISprite removed in removedPlayerProjectiles)
            {
                activeEnemyProjectiles.Remove(removed);
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
                activeEnemyProjectiles.Add(removed);
            }
            addedPlayerProjectiles.Clear();
        }






        IPlayer player;

        IController keyController;




        List<ISprite> blocks;
        List<ISprite> items;
        List<ISprite> enemies;

        int blockCounter;
        int itemCounter;
        int enemyCounter;


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
            // TODO: Add your initialization logic here
            //test

            blockCounter = 0;
            itemCounter = 0;
            enemyCounter = 0;

            //keyboard = new KeyboardController(this);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            aquaSheet = Content.Load<Texture2D>("aquamentus");
            basicEnemy = Content.Load<Texture2D>("basicenemy");
            doors = Content.Load<Texture2D>("doors");
            fire = Content.Load<Texture2D>("fire");
            goriya = Content.Load<Texture2D>("goriya");
            itemSheet = Content.Load<Texture2D>("itemset");
            oldMan = Content.Load<Texture2D>("oldman");
            playerSheet = Content.Load<Texture2D>("playersheet");
            tileSet = Content.Load<Texture2D>("tileset");
            wallmaster = Content.Load<Texture2D>("wallmasters");

            blocks = new List<ISprite>();
            items = new List<ISprite>();
            enemies = new List<ISprite>();

            blocks.Add(new ClosedDoor(0, new Vector2(40, 40), doors, 0, true));
            blocks.Add(new LockedDoor(0, new Vector2(40, 40), doors, 0, true));
            blocks.Add(new OpenDoor(0, new Vector2(40, 40), doors, 0, false));
            blocks.Add(new CaveDoor(0, new Vector2(40, 40), doors));
            blocks.Add(new BlueSandBlock(tileSet, new Vector2(40, 40)));
            blocks.Add(new DragonBlock(tileSet, new Vector2(40, 40)));
            blocks.Add(new DungeonBlock(tileSet, new Vector2(40, 40)));
            blocks.Add(new FishBlock(tileSet, new Vector2(40, 40)));
            blocks.Add(new FireBlock(fire, new Vector2(40, 40))); ;

            items.Add(new BombItem(itemSheet, new Vector2(100, 100)));
            items.Add(new BowItem(itemSheet, new Vector2(100, 100)));
            items.Add(new TriforceItem(itemSheet, new Vector2(100, 100)));
            items.Add(new CompassItem(itemSheet, new Vector2(100, 100)));
            items.Add(new PermanentHeartItem(itemSheet, new Vector2(100, 100)));
            items.Add(new HeartItem(itemSheet, new Vector2(100, 100)));
            items.Add(new BoomerangItem(itemSheet, new Vector2(100, 100)));
            items.Add(new KeyItem(itemSheet, new Vector2(100, 100)));
            items.Add(new FairyItem(itemSheet, new Vector2(100, 100)));
            items.Add(new MapItem(itemSheet, new Vector2(100, 100)));
            items.Add(new RupeeItem(itemSheet, new Vector2(100, 100)));


            enemies.Add(new AquamentusEnemy(aquaSheet, new Vector2(400, 200)));
            enemies.Add(new TrapEnemy(basicEnemy, new Vector2(400, 200)));
            enemies.Add(new GelEnemy(basicEnemy, new Vector2(400, 200)));
            enemies.Add(new StalfosEnemy(basicEnemy, new Vector2(400, 200)));
            enemies.Add(new KeeseEnemy(basicEnemy, new Vector2(400, 200)));
            enemies.Add(new WallmasterEnemy(wallmaster, new Vector2(400, 200)));
            enemies.Add(new OldMan(oldMan, new Vector2(400, 200)));



            player = new Player(Content.Load<Texture2D>("playersheet"), new Vector2(500, 200), Content.Load<Texture2D>("PlayerProjectiles"));

            keyController = new KeyboardController(this);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //keyboard.Update();

            player.Update(this);

            foreach(ISprite enemyProjectile in activeEnemyProjectiles){
                enemyProjectile.Update(this);
            }
            removeProjectiles();
            addProjectiles();


            keyController.Update();

            foreach(ISprite enemy in enemies)
            {
                enemy.Update(this);
            }

            blocks[blockCounter].Update(this);
            items[itemCounter].Update(this);
            enemies[enemyCounter].Update(this);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();


            foreach (ISprite enemyProjectile in activeEnemyProjectiles)
            {
                enemyProjectile.Draw(_spriteBatch);
            }
            foreach (ISprite playerProjectile in activePlayerProjectiles)
            {
                playerProjectile.Draw(_spriteBatch);
            }


            player.Draw(_spriteBatch);

            blocks[blockCounter].Draw(_spriteBatch);
            items[itemCounter].Draw(_spriteBatch);
            enemies[enemyCounter].Draw(_spriteBatch);




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
            blockCounter+=incDec;

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
            enemyCounter+=incDec;

            if (enemyCounter == enemies.Count) enemyCounter = 0;
            else if (enemyCounter < 0) enemyCounter=enemies.Count-1;

        }
    }
}
