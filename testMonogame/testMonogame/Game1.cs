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






        ISprite TestObject;
        IPlayer player;

        IController keyController;



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


            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here


            TestObject = new AquamentusEnemy(Content.Load<Texture2D>("aquamentus"), new Vector2(200, 200));
            player = new Player(Content.Load<Texture2D>("playersheet"), new Vector2(500, 200), Content.Load<Texture2D>("PlayerProjectiles"));

            keyController = new KeyboardController(this);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            TestObject.Update(this);
            foreach(ISprite enemyProjectile in activeEnemyProjectiles){
                enemyProjectile.Update(this);
            }
            removeProjectiles();
            addProjectiles();

            player.Update(this);


            keyController.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            TestObject.Draw(_spriteBatch);
            foreach (ISprite enemyProjectile in activeEnemyProjectiles)
            {
                enemyProjectile.Draw(_spriteBatch);
            }
            foreach (ISprite playerProjectile in activePlayerProjectiles)
            {
                playerProjectile.Draw(_spriteBatch);
            }


            player.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
