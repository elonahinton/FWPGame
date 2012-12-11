//Camco
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FWPGame.Engine;
using FWPGame.Powers;
using FWPGame.Items;
using FWPGame.Events;
using System.Collections;


namespace FWPGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public partial class FWPGame : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont chiF;
        private Vector2 FontPos;
        public Map map;

        public Player player;
        private Cursor cursor;
        private Vector2 tempMapSize = new Vector2(1200, 1200);
        private Apocalypse myApocalypse;
        protected internal ArrayList powers = new ArrayList();
        public Vector2 worldScale;

        private SoundEffect mainMusic;
        private SoundEffectInstance mainMusicInstance;

        public FWPGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;//1280;  
            graphics.PreferredBackBufferHeight = 768;//1024; 
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;//true;
        }  

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            chiF = Content.Load<SpriteFont>("ChillerFont");
            
            //Kill the game with Escape
            GameAction closeGame = new GameAction(this, this.GetType().GetMethod("ExitGame"), new object[0]);
            InputManager.AddToKeyboardMap(Keys.Escape, closeGame);

            GrowGrass grassPower = new GrowGrass(Content.Load<Texture2D>("UI/sprouts"), this, new Vector2(0, 0), new Vector2(0, 0));
            powers.Add(grassPower);
            Rain rainPower = new Rain(Content.Load<Texture2D>("UI/raindrop"), this, new Vector2(0, 0), new Vector2(0, 0));
            powers.Add(rainPower);
            sproutTree = new SproutTree(Content.Load<Texture2D>("UI/treeicon"), this, new Vector2(0, 0), new Vector2(0, 0));
            powers.Add(sproutTree);
            BuildRoad buildRoad = new BuildRoad(Content.Load<Texture2D>("UI/roadicon"), this, new Vector2(0, 0), new Vector2(0, 0));
            powers.Add(buildRoad);
            Fire fire = new Fire(Content.Load<Texture2D>("UI/fireicon"), this, new Vector2(0, 0), new Vector2(0, 0));
            powers.Add(fire);
            BuildHouse housePower = new BuildHouse(Content.Load<Texture2D>("UI/house_icon"), this, new Vector2(0, 0), new Vector2(0, 0));
            powers.Add(housePower);
            Wind windPower = new Wind(Content.Load<Texture2D>("UI/windicon"), this, new Vector2(0,0), new Vector2(0,0));
            powers.Add(windPower);
            //Protect protect = new Protect(Content.Load<Texture2D>("UI/protect"), this, new Vector2(0, 0), new Vector2(0, 0));

            Dictionary<string, Power> myPowers = new Dictionary<string, Power>();
            myPowers.Add("grass", grassPower);
            myPowers.Add("sprout", sproutTree);
            myPowers.Add("fire", fire);
            myPowers.Add("house", housePower);
            
            List<Power> availablePowers = new List<Power>();
            //availablePowers.Add(protect);
            

            cursor = new Cursor(Content.Load<Texture2D>("cursor"), new Vector2(0,0));



            map = new Map(Content.Load<Texture2D>("Maps/Mars/marsorbit"),
                new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
                new Vector2(0, 0));

            player = new Player(Content, map,
    new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
    cursor, powers);

            // TODO: use this.Content to load your game content here
            LoadObjects();

            FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
                graphics.GraphicsDevice.Viewport.Height / 2);

            myApocalypse = new Apocalypse(this, myPowers);
            //myApocalypse.BuildVillage();


            mainMusic = Content.Load<SoundEffect>("Sound/mainMusic");
            mainMusicInstance = mainMusic.CreateInstance();
            mainMusicInstance.IsLooped = true;
            mainMusicInstance.Play();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            HandleInput();
            Transmorg();

            player.Update(gameTime);
            //myApocalypse.Update(gameTime.TotalGameTime.Seconds, player.myMapPosition);

            base.Update(gameTime);
        }

        private void HandleInput()
        {

            if (this.IsActive == true)
            {
                InputManager.ActKeyboard(Keyboard.GetState());
                InputManager.ActMouse(Mouse.GetState());
            }
        }

        public void ExitGame()
        {
            this.Exit();
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here

            player.Draw(spriteBatch);
            //myApocalypse.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
