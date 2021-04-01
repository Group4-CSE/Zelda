using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;


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
        Dictionary<string, Texture2D> sprites;


        IController keyController;
        IController mouseController;

        GameManager manager;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize()
        {
            //test
            sprites = new Dictionary<string, Texture2D>();
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
            Texture2D hudSheet = Content.Load<Texture2D>("HudSheet");

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
            sprites.Add("hudSheet", hudSheet);


            SpriteFont font= Content.Load<SpriteFont>("HUDfont");

            manager = new GameManager(this, sprites,font);

            keyController = new KeyboardController(manager);
            mouseController = new MouseController(manager);

            

        }

        protected override void Update(GameTime gameTime)
        {


            manager.Update();
            keyController.Update();
            mouseController.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            manager.Draw(_spriteBatch);


            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void s2reset()
        {
            //for sprint 2 reset
            this.Initialize();
        }
    }
}