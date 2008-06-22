using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Config;

namespace IRTaktiks.Components.Manager
{
    /// <summary>
    /// Manager of the configuration.
    /// </summary>
    public class ConfigurationManager : DrawableGameComponent
    {
        #region Event

        /// <summary>
        /// The delegate of methods that will handle the touched event.
        /// </summary>
        public delegate void ConfiguratedEventHandler();

        /// <summary>
        /// Event fired when a player end the configuration of the items.
        /// </summary>
        public event ConfiguratedEventHandler Configurated;

        #endregion

        #region Properties

        /// <summary>
        /// The index of the player.
        /// </summary>
        private PlayerIndex PlayerIndexField;

        /// <summary>
        /// The index of the player.
        /// </summary>
        public PlayerIndex PlayerIndex
        {
            get { return PlayerIndexField; }
        }

        /// <summary>
        /// The config keyboard.
        /// </summary>
        private Keyboard KeyboardField;

        /// <summary>
        /// The config keyboard.
        /// </summary>
        public Keyboard Keyboard
        {
            get { return KeyboardField; }
        }
                
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="game">The instance of game that is running.</param>
        public ConfigurationManager(Game game, PlayerIndex playerIndex)
            : base(game)
        {
            this.PlayerIndexField = playerIndex;

            // Items creation
            this.KeyboardField = new Keyboard(game, playerIndex);

            // Event handling
            this.Keyboard.Touched += new Keyboard.TouchedEventHandler(Keyboard_Touched);
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
            base.Draw(gameTime);
        }

        #endregion

        #region Event Handling
        
        /// <summary>
        /// Handle the pressed event of the keyboard.
        /// </summary>
        /// <param name="letter">The letther that was touched on the keyboard.</param>
        private void Keyboard_Touched(string letter)
        {
        }

        #endregion
    }
}