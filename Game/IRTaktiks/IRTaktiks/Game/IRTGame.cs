using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using IRTaktiks.Input;
using IRTaktiks.Components.Screen;
using IRTaktiks.Components.Manager;
using IRTaktiks.Components.Playable;
using IRTaktiks.Components.Logic;
using IRTaktiks.Components.Scenario;

namespace IRTaktiks
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class IRTGame : Game
    {
        #region Constants

        /// <summary>
        /// Width of the window.
        /// </summary>
        public const int Width = 5 * 256; // 5 * 256 = 1280
        
        /// <summary>
        /// Height of the window.
        /// </summary>
        public const int Height = 4 * 256; // 4 * 256 = 1024

        /// <summary>
        /// Indicates if the game will be started at fullscreen mode.
        /// </summary>
        public const bool IsFullScreen = true;

        #endregion

        #region GameStatus

        /// <summary>
		/// The statuses of the Game.
		/// </summary>
		public enum GameStatus
		{
			TitleScreen,
			ConfigScreen,
			GameScreen,
			EndScreen
		}

		#endregion

		#region Game Properties

        /// <summary>
        /// The FPS component.
        /// </summary>
        private FPS FPSField;

        /// <summary>
        /// The FPS component.
        /// </summary>
        public FPS FPS
        {
            get { return FPSField; }
        }

        /// <summary>
        /// The player one of the game.
        /// </summary>
        private Player PlayerOneField;

        /// <summary>
        /// The player one of the game.
        /// </summary>
        public Player PlayerOne
        {
            get { return PlayerOneField; }
            set { PlayerOneField = value; }
        }

        /// <summary>
        /// The player two of the game.
        /// </summary>
        private Player PlayerTwoField;

        /// <summary>
        /// The player two of the game.
        /// </summary>
        public Player PlayerTwo
        {
            get { return PlayerTwoField; }
            set { PlayerTwoField = value; }
        }

        #endregion

        #region Graphic Properties

        /// <summary>
		/// Handles the configuration and management of the graphics device.
		/// </summary>
		private GraphicsDeviceManager GraphicsDeviceManagerField;

		/// <summary>
		/// Handles the configuration and management of the graphics device.
		/// </summary>
		public GraphicsDeviceManager GraphicsDeviceManager
		{
			get { return GraphicsDeviceManagerField; }
		}
        
        /// <summary>
        /// The instance of the camera.
        /// </summary>
        private Camera CameraField;

        /// <summary>
        /// The instance of the camera.
        /// </summary>
        public Camera Camera
        {
            get { return CameraField; }
        }

		/// <summary>
		/// Manager of sprites.
		/// </summary>
        private SpriteManager SpriteManagerField;

		/// <summary>
		/// Manager of sprites.
		/// </summary>
        public SpriteManager SpriteManager
		{
            get { return SpriteManagerField; }
        }

        /// <summary>
        /// Manager of areas.
        /// </summary>
        private AreaManager AreaManagerField;

        /// <summary>
        /// Manager of areas.
        /// </summary>
        public AreaManager AreaManager
        {
            get { return AreaManagerField; }
        }

        #endregion

        #region Screens

        /// <summary>
		/// The screen manager component of the game.
		/// </summary>
		private ScreenManager ScreenManagerField;

		/// <summary>
		/// The screen manager component of the game.
		/// </summary>
		public ScreenManager ScreenManager
		{
			get { return ScreenManagerField; }
			set { ScreenManagerField = value; }
		}

		/// <summary>
		/// The title screen of the game.
		/// </summary>
		private TitleScreen TitleScreenField;

		/// <summary>
		/// The title screen of the game.
		/// </summary>
		public TitleScreen TitleScreen
		{
			get { return TitleScreenField; }
		}
        
        /// <summary>
        /// The config screen of the game.
        /// </summary>
        private ConfigScreen ConfigScreenField;

        /// <summary>
        /// The config screen of the game.
        /// </summary>
        public ConfigScreen ConfigScreen
        {
            get { return ConfigScreenField; }
        }

		/// <summary>
		/// The game screen of the game.
		/// </summary>
		private GameScreen GameScreenField;

		/// <summary>
		/// The game screen of the game.
		/// </summary>
		public GameScreen GameScreen
		{
			get { return GameScreenField; }
        }

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor of class.
		/// </summary>
		public IRTGame()
		{
			this.GraphicsDeviceManagerField = new GraphicsDeviceManager(this);
			this.Content.RootDirectory = "Content";

			this.Window.Title = "IRTaktiks - Tactical RPG for multi-touch screens";
			this.IsMouseVisible = true;

            this.GraphicsDeviceManager.PreferredBackBufferWidth = IRTGame.Width;
            this.GraphicsDeviceManager.PreferredBackBufferHeight = IRTGame.Height;
            this.GraphicsDeviceManager.IsFullScreen = IRTGame.IsFullScreen;

			this.GraphicsDeviceManager.ApplyChanges();
		}

		#endregion

		#region Game Methods

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
            // Resources loading.
            TextureManager.Instance.Initialize(this);
            EffectManager.Instance.Initialize(this);
			FontManager.Instance.Initialize(this);

            // Components creation.
            this.CameraField = new Camera(this);
            this.SpriteManagerField = new SpriteManager(this);
            this.AreaManagerField = new AreaManager(this.GraphicsDevice, this.Camera);
            this.FPSField = new FPS(this);

			// Screen construction.
			this.TitleScreenField = new TitleScreen(this, 0);
            this.ConfigScreenField = new ConfigScreen(this, 0);
			this.GameScreenField = new GameScreen(this, 0);
			this.ScreenManagerField = new ScreenManager(this);

			// Screen addiction.
			this.ScreenManager.Screens.Add(this.TitleScreen);
            this.ScreenManager.Screens.Add(this.ConfigScreen);
			this.ScreenManager.Screens.Add(this.GameScreen);

			// Components addiction.
			this.Components.Add(this.Camera);
            this.Components.Add(this.ScreenManager);
            this.Components.Add(this.FPS);

			// Change the game to its first status.
			this.ChangeGameStatus(GameStatus.TitleScreen);

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			base.LoadContent();
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			base.UnloadContent();
		}
        
        /// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		private bool mouseIsPressed;
        protected override void Update(GameTime gameTime)
		{
            // Exits the game.
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            #warning Mouse injection enabled

			MouseState mouseState = Mouse.GetState();

            if (!mouseIsPressed)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    mouseIsPressed = true;
                    InputManager.Instance.RaiseCursorDown(0, new Vector2(mouseState.X, mouseState.Y));
                }
            }
            else
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    mouseIsPressed = true;
                    InputManager.Instance.RaiseCursorUpdate(0, new Vector2(mouseState.X, mouseState.Y));
                }
                else
                {
                    mouseIsPressed = false;
                    InputManager.Instance.RaiseCursorUp(0, new Vector2(mouseState.X, mouseState.Y));
                }
            }

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			this.GraphicsDeviceManager.GraphicsDevice.Clear(Color.DimGray);
            this.GraphicsDeviceManager.GraphicsDevice.RenderState.CullMode = CullMode.None;

            //this.AreaManager.Flush();

			base.Draw(gameTime);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Change the status of the game.
		/// </summary>
		/// <param name="newStatus">The new status of the game.</param>
		public void ChangeGameStatus(GameStatus newStatus)
		{
			// Make all screen invisibles and disabled.
			this.TitleScreen.Enabled = false;
			this.TitleScreen.Visible = false;
            
            this.ConfigScreen.Enabled = false;
            this.ConfigScreen.Visible = false;

			this.GameScreen.Enabled = false;
			this.GameScreen.Visible = false;

            // Title Screen
            if (newStatus == GameStatus.TitleScreen)
            {
                this.TitleScreen.Enabled = true;
                this.TitleScreen.Visible = true;
                this.TitleScreen.Initialize();
            }

            // Config screen
            if (newStatus == GameStatus.ConfigScreen)
            {
                this.ConfigScreen.Enabled = true;
                this.ConfigScreen.Visible = true; 
                this.ConfigScreen.Initialize();
            }

            // Game Screen
            if (newStatus == GameStatus.GameScreen)
            {
                this.GameScreen.Enabled = true;
                this.GameScreen.Visible = true;
                this.GameScreen.Initialize();
            }

            // End screen
            if (newStatus == GameStatus.EndScreen)
            {

            }
		}

		#endregion
	}
}
