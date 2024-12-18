using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace IRTaktiks.Components.Manager
{
    /// <summary>
    /// Manager of sprite fonts.
    /// </summary>
    public class FontManager
    {
        #region Singleton

        /// <summary>
        /// The instance of the class.
        /// </summary>
        private static FontManager InstanceField;

        /// <summary>
        /// The instance of the class.
        /// </summary>
        public static FontManager Instance
        {
            get { if (InstanceField == null) InstanceField = new FontManager(); return InstanceField; }
        }

        /// <summary>
        /// Private constructor.
        /// </summary>
		private FontManager()
        { }

        #endregion

        #region Initialize

        /// <summary>
        /// Initialize the instance of the class.
        /// </summary>
        public void Initialize(Game game)
        {
			// Load the spritefonts
            this.DebugField = game.Content.Load<SpriteFont>("Fonts/Debug");

            this.Chilopod12Field = game.Content.Load<SpriteFont>("Fonts/Chilopod12");
            this.Chilopod14Field = game.Content.Load<SpriteFont>("Fonts/Chilopod14");
            this.Chilopod16Field = game.Content.Load<SpriteFont>("Fonts/Chilopod16");
            this.Chilopod18Field = game.Content.Load<SpriteFont>("Fonts/Chilopod18");
            this.Chilopod20Field = game.Content.Load<SpriteFont>("Fonts/Chilopod20");

            this.Franklin20GField = game.Content.Load<SpriteFont>("Fonts/Franklin20G");
            this.Franklin20RField = game.Content.Load<SpriteFont>("Fonts/Franklin20R");

            // Set the correct spacing.
            this.Chilopod12.Spacing = -3.5f;
            this.Chilopod14.Spacing = -3.5f;
            this.Chilopod16.Spacing = -3.5f;
            this.Chilopod18.Spacing = -3.5f;
			this.Chilopod20.Spacing = -3.5f;

            this.Franklin20G.Spacing = -3.0f;
            this.Franklin20R.Spacing = -3.0f;
        }

        #endregion

		#region Sprite Font

        /// <summary>
        /// The debug font.
        /// </summary>
        private SpriteFont DebugField;
        
        /// <summary>
        /// The debug font.
        /// </summary>
        public SpriteFont Debug
        {
            get { return DebugField; }
        }	
        
        /// <summary>
        /// The spritefont for the chilopod font type, 12pt of size.
        /// </summary>
        private SpriteFont Chilopod12Field;

        /// <summary>
        /// The spritefont for the chilopod font type, 12pt of size.
        /// </summary>
        public SpriteFont Chilopod12
        {
            get { return Chilopod12Field; }
        }

        /// <summary>
        /// The spritefont for the chilopod font type, 14pt of size.
        /// </summary>
        private SpriteFont Chilopod14Field;

        /// <summary>
        /// The spritefont for the chilopod font type, 14pt of size.
        /// </summary>
        public SpriteFont Chilopod14
        {
            get { return Chilopod14Field; }
        }

        /// <summary>
        /// The spritefont for the chilopod font type, 16pt of size.
        /// </summary>
        private SpriteFont Chilopod16Field;

        /// <summary>
        /// The spritefont for the chilopod font type, 16pt of size.
        /// </summary>
        public SpriteFont Chilopod16
        {
            get { return Chilopod16Field; }
        }
        
        /// <summary>
        /// The spritefont for the chilopod font type, 18pt of size.
        /// </summary>
        private SpriteFont Chilopod18Field;
        
        /// <summary>
        /// The spritefont for the chilopod font type, 18pt of size.
        /// </summary>
        public SpriteFont Chilopod18
        {
            get { return Chilopod18Field; }
        }

        /// <summary>
        /// The spritefont for the chilopod font type, 20pt of size.
        /// </summary>
        private SpriteFont Chilopod20Field;

        /// <summary>
        /// The spritefont for the chilopod font type, 20pt of size.
        /// </summary>
        public SpriteFont Chilopod20
        {
            get { return Chilopod20Field; }
        }

        /// <summary>
        /// The spritefont for the franklin font type, 20pt of size, green color.
        /// </summary>
        private SpriteFont Franklin20GField;

        /// <summary>
        /// The spritefont for the franklin font type, 20pt of size, green color.
        /// </summary>
        public SpriteFont Franklin20G
        {
            get { return Franklin20GField; }
        }

        /// <summary>
        /// The spritefont for the franklin font type, 20pt of size, red color.
        /// </summary>
        private SpriteFont Franklin20RField;

        /// <summary>
        /// The spritefont for the franklin font type, 20pt of size, red color.
        /// </summary>
        public SpriteFont Franklin20R
        {
            get { return Franklin20RField; }
        }

        #endregion
    }
}
