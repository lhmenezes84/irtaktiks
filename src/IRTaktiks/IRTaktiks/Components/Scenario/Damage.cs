using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Manager;

namespace IRTaktiks.Components.Scenario
{
    /// <summary>
    /// Component that represents a particle effect.
    /// </summary>
    public class Damage
    {
        #region Effect Type

        /// <summary>
        /// Representation of a type of damages.
        /// </summary>
        public enum DamageType
        {
            /// <summary>
            /// Beneful damage.
            /// </summary>
            Benefit,

            /// <summary>
            /// Harmful damage.
            /// </summary>
            Harmful,
        }

        #endregion

        #region Properties

        /// <summary>
        /// The text representation of the damage.
        /// </summary>
        private string TextField;

        /// <summary>
        /// The text representation of the damage.
        /// </summary>
        public string Text
	    {
		    get { return TextField;}
	    }

        /// <summary>
        /// The position of the damage.
        /// </summary>
        private Vector2 PositionField;

        /// <summary>
        /// The position of the damage.
        /// </summary>
        public Vector2 Position
        {
            get { return PositionField; }
        }

        /// <summary>
        /// The type of the damage.
        /// </summary>
        private DamageType TypeField;

        /// <summary>
        /// The type of the damage.
        /// </summary>
        public DamageType Type
        {
            get { return TypeField; }
        }

        /// <summary>
        /// The color of the damage.
        /// </summary>
        private Color ColorField;

        /// <summary>
        /// The color of the damage.
        /// </summary>
        public Color Color
        {
            get { return ColorField; }
        }
        
        /// <summary>
        /// Indicates that the effect is alive.
        /// </summary>
        private bool AliveField;

        /// <summary>
        /// Indicates that the effect is alive.
        /// </summary>
        public bool Alive
        {
            get { return AliveField; }
        }

        /// <summary>
        /// The font that will be used to draw the damage.
        /// </summary>
        private SpriteFont FontField;

        /// <summary>
        /// The font that will be used to draw the damage.
        /// </summary>
        public SpriteFont Font
        {
            get { return FontField; }
        }
        
        /// <summary>
        /// The actual life of the damage.
        /// </summary>
        private float Life;

        /// <summary>
        /// The life increment at each update.
        /// </summary>
        private float LifeIncrement;

        /// <summary>
        /// The life time of the damage, before its destruction
        /// </summary>
        private float LifeTime;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="value">The value of the damage.</param>
        /// <param name="suffix">Suffix for damage identification.</param>
        /// <param name="position">The position of the damage.</param>
        /// <param name="type">The type of the damage.</param>
        public Damage(int value, string suffix, Vector2 position, DamageType type) 
            : this(value, suffix, position, type, 0.1f, 10.0f)
        { }

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="text">The text of the damage.</param>
        /// <param name="position">The position of the damage.</param>
        /// <param name="type">The type of the damage.</param>
        /// <param name="lifeIncrement">The life increment at each update.</param>
        /// <param name="lifeTime">The life time of the damage, before its destruction.</param>
        public Damage(string text, Vector2 position, DamageType type)
            : this(text, position, type, 0.1f, 10.0f)
        { }
        
        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="value">The value of the damage.</param>
        /// <param name="suffix">Suffix for damage identification.</param>
        /// <param name="position">The position of the damage.</param>
        /// <param name="type">The type of the damage.</param>
        /// <param name="lifeIncrement">The life increment at each update.</param>
        /// <param name="lifeTime">The life time of the damage, before its destruction.</param>
        public Damage(int value, string suffix, Vector2 position, DamageType type, float lifeIncrement, float lifeTime)
            : this(String.Format("{0}{1}", value.ToString(), suffix), position, type, lifeIncrement, lifeTime)
        { }

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="text">The text of the damage.</param>
        /// <param name="position">The position of the damage.</param>
        /// <param name="type">The type of the damage.</param>
        /// <param name="lifeIncrement">The life increment at each update.</param>
        /// <param name="lifeTime">The life time of the damage, before its destruction.</param>
        public Damage(string text, Vector2 position, DamageType type, float lifeIncrement, float lifeTime)
        {
            this.TextField = text;
            this.PositionField = position;
            this.TypeField = type;
            this.AliveField = true;

            this.LifeIncrement = lifeIncrement;
            this.Life = 0;
            this.LifeTime = lifeTime;

            this.ColorField = Color.White;

            switch (this.Type)
            {
                case DamageType.Benefit:
                    this.FontField = FontManager.Instance.Franklin20G;
                    break;

                case DamageType.Harmful:
                    this.FontField = FontManager.Instance.Franklin20R;
                    break;
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Update the damage position.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            if (this.Life > this.LifeTime)
            {
                this.AliveField = false;
            }
            else
            {
                float x = this.Position.X + 0.15f;
                float y = this.Position.Y - this.Life * this.Life / 50;

                this.PositionField = new Vector2(x, y);
                
                this.Life += this.LifeIncrement;
                this.ColorField = new Color(new Vector4(this.Color.ToVector3(), 1.0f - (this.Life / this.LifeTime)));
            }
        }

        #endregion
    }
}
