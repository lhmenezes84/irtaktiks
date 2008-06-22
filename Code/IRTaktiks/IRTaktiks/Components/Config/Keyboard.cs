using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Manager;
using IRTaktiks.Input;
using IRTaktiks.Input.EventArgs;

namespace IRTaktiks.Components.Config
{
    /// <summary>
    /// Representation of the config keyboard.
    /// </summary>
    public class Keyboard : Configurable
    {
        #region Event

        /// <summary>
        /// The delegate of methods that will handle the touched event.
        /// </summary>
        /// <param name="letter">The letter pressed on the keyboard.</param>
        public delegate void TouchedEventHandler(string letter);

        /// <summary>
        /// Event fired when a letter on the keyboard is touched.
        /// </summary>
        public event TouchedEventHandler Touched;

        #endregion

        #region Properties

        /// <summary>
        /// Indicate that the item is configurated.
        /// </summary>
        public override bool Configurated
        {
            get { return true; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="game">The instance of game that is running.</param>
        /// <param name="playerIndex">The index of the player owner of the keyboard.</param>
        public Keyboard(Game game, PlayerIndex playerIndex)
            : base(game, playerIndex)
        {
            switch (playerIndex)
            {
                case PlayerIndex.One:
                    this.PositionField = new Vector2(0, 0);
                    this.TextureField = TextureManager.Instance.Sprites.Config.KeyboardPlayerOne;
                    break;

                case PlayerIndex.Two:
                    this.PositionField = new Vector2(640, 0);
                    this.TextureField = TextureManager.Instance.Sprites.Config.KeyboardPlayerTwo;
                    break;
            }

            InputManager.Instance.CursorDown += new EventHandler<CursorDownArgs>(CursorDown);
        }

        #endregion

        #region Component Methods

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run. This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Called when the DrawableGameComponent needs to be drawn. Override this method
        /// with component-specific drawing code.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            IRTGame game = this.Game as IRTGame;

            game.SpriteManager.Draw(this.Texture, this.Position, Color.White, 20);
            
            base.Draw(gameTime);
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Deactivate the item.
        /// </summary>
        protected override void Deactivate()
        {
            InputManager.Instance.CursorDown -= CursorDown;
        }

        #endregion

        #region Input Handling

        /// <summary>
        /// Input Handling for Cursor Down event.
        /// </summary>
        private void CursorDown(object sender, CursorDownArgs e)
        {
            // 1st Line
            // 2nd Line
            // 3rd Line
            // 4th Line
            // 5th Line
        }

        #endregion
    }
}