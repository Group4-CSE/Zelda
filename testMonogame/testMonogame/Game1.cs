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

        public void AddEnemyProjectile(ISprite projectile){activeEnemyProjectiles.Add(projectile); }
        public void RemoveEnemyProjectile(ISprite projectile) { removedEnemyProjectiles.Add(projectile); }
         void removeProjectiles()
        {
            foreach(ISprite removed in removedEnemyProjectiles)
            {
                activeEnemyProjectiles.Remove(removed);
            }
        }




        ISprite TestObject;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
