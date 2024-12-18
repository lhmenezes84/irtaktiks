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

namespace IRTaktiks.Components.Scenario
{
    /// <summary>
    /// Component that represents a particle effect.
    /// </summary>
    public class ParticleEffect
    {
        #region Effect Type

        /// <summary>
        /// Representation of a type of effects.
        /// </summary>
        public enum EffectType
        {
            /// <summary>
            /// Firework effect.
            /// </summary>
            Firework,

            /// <summary>
            /// Ring effect.
            /// </summary>
            Ring,

            /// <summary>
            /// Losangle Effect.
            /// </summary>
            Losangle,

            /// <summary>
            /// Flash effect at 0°.
            /// </summary>
            Flash0,

            /// <summary>
            /// Flash effect at 45°.
            /// </summary>
            Flash45,

            /// <summary>
            /// Flash effect at 90°.
            /// </summary>
            Flash90,

            /// <summary>
            /// Flash effect at 135°.
            /// </summary>
            Flash135,

            /// <summary>
            /// Pillar effect.
            /// </summary>
            Pillar,

            /// <summary>
            /// Sphere effect.
            /// </summary>
            Sphere,

            /// <summary>
            /// Slice Effect.
            /// </summary>
            Slice
        }

        #endregion

        #region Particle Struct

        /// <summary>
        /// Structure of one particle.
        /// </summary>
        public struct Particle
        {
            #region Properties

            /// <summary>
            /// The vertex elements of the particle.
            /// </summary>
            private static VertexElement[] VertexElementsField = new VertexElement[] {
                    new VertexElement(0, 0, VertexElementFormat.Vector3, VertexElementMethod.Default, VertexElementUsage.Position, 0),
                    new VertexElement(0, sizeof(float) * 3, VertexElementFormat.Color, VertexElementMethod.Default, VertexElementUsage.Color, 0),
                    new VertexElement(0, sizeof(float) * 7, VertexElementFormat.Vector4, VertexElementMethod.Default, VertexElementUsage.TextureCoordinate, 0),                
                };

            /// <summary>
            /// The vertex elements of the particle.
            /// </summary>
            public static VertexElement[] VertexElements
            {
                get { return VertexElementsField; }
            }

            /// <summary>
            /// The actual position of the particle.
            /// </summary>
            private Vector3 PositionField;

            /// <summary>
            /// The actual position of the particle.
            /// </summary>
            public Vector3 Position
            {
                get { return PositionField; }
                set { PositionField = value; }
            }

            /// <summary>
            /// The color of the particle.
            /// </summary>
            private Color ColorField;

            /// <summary>
            /// The color of the particle.
            /// </summary>
            public Color Color
            {
                get { return ColorField; }
                set { ColorField = value; }
            }

            /// <summary>
            /// The data of the particle.
            /// </summary>
            private Vector4 DataField;

            /// <summary>
            /// The data of the particle.
            /// </summary>
            public Vector4 Data
            {
                get { return DataField; }
                set { DataField = value; }
            }

            /// <summary>
            /// Generator of random numbers.
            /// </summary>
            private static Random random = new Random();

            #endregion

            #region Constructor

            /// <summary>
            /// Constructor of the structure.
            /// </summary>
            /// <param name="position">The position of the particle.</param>
            /// <param name="color">The color of the particle.</param>
            public Particle(Vector3 position, Color color)
            {
                this.PositionField = position;
                this.ColorField = color;

                this.DataField = new Vector4(random.Next(0, 360), random.Next(0, 360), 0, 0);
            }

            #endregion
        }

        #endregion

        #region Properties

        /// <summary>
        /// The list of particles.
        /// </summary>
        private Particle[] ParticlesField;

        /// <summary>
        /// The list of particles.
        /// </summary>
        public Particle[] Particles
        {
            get { return ParticlesField; }
        }

        /// <summary>
        /// The position of the effect.
        /// </summary>
        private Vector3 PositionField;

        /// <summary>
        /// The position of the effect.
        /// </summary>
        public Vector3 Position
        {
            get { return PositionField; }
        }

        /// <summary>
        /// The type of the effect.
        /// </summary>
        private EffectType TypeField;

        /// <summary>
        /// The type of the effect.
        /// </summary>
        public EffectType Type
        {
            get { return TypeField; }
        }

        /// <summary>
        /// The color of the effect. If null, random color will be used.
        /// </summary>
        private Color? ColorField;

        /// <summary>
        /// The color of the effect. If null, random color will be used.
        /// </summary>
        public Color? Color
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
        /// The life increment at each update.
        /// </summary>
        private float LifeIncrement;

        /// <summary>
        /// The life time of the effect, before its destruction
        /// </summary>
        private float LifeTime;

        /// <summary>
        /// Generator of random numbers.
        /// </summary>
        private static Random random = new Random();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="position">The position of the effect.</param>
        /// <param name="particlesCount">The number of particles used to draw the effect.</param>
        /// <param name="type">The type of effect.</param>
        /// <param name="lifeIncrement">The life increment at each update.</param>
        /// <param name="lifeTime">The life time of the effect, before its destruction</param>
        /// <param name="color">The color of the effect. If null, random color will be used.</param>
        public ParticleEffect(Vector2 position, int particlesCount, EffectType type, float lifeIncrement, float lifeTime, Color? color)
        {
            // Scale the position and convert into 3D position.
            position.Y = IRTSettings.Default.Height - position.Y;
            float x = 69 * (2 * (position.X / (float)IRTSettings.Default.Width) - 1);
            float z = 55 * (2 * (position.Y / (float)IRTSettings.Default.Height) - 1);
            Vector3 scaledPosition = new Vector3(x, 1, z);

            this.PositionField = scaledPosition;
            this.TypeField = type;
            this.LifeIncrement = lifeIncrement;
            this.LifeTime = lifeTime;
            this.ColorField = color;

            this.AliveField = true;

            this.ParticlesField = new Particle[particlesCount];

            // Create the particles with the specified color.
            if (color.HasValue)
            {
                for (int index = 0; index < particlesCount; index++)
                {
                    this.Particles[index] = new Particle(scaledPosition, color.Value);
                }
            }

            // Create the particles with random colors.
            else
            {
                for (int index = 0; index < particlesCount; index++)
                {
                    this.Particles[index] = new Particle(scaledPosition, new Color((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255), 1));
                }
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Updates the particles of the effect.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            switch (this.Type)
            {
                case EffectType.Firework:
                    this.Firework();
                    break;

                case EffectType.Ring:
                    this.Ring();
                    break;

                case EffectType.Losangle:
                    this.Losangle();
                    break;

                case EffectType.Flash0:
                    this.Flash0();
                    break;

                case EffectType.Flash45:
                    this.Flash45();
                    break;

                case EffectType.Flash90:
                    this.Flash90();
                    break;

                case EffectType.Flash135:
                    this.Flash135();
                    break;

                case EffectType.Pillar:
                    this.Pillar();
                    break;

                case EffectType.Sphere:
                    this.Sphere();
                    break;

                case EffectType.Slice:
                    this.Slice();
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Firework

        /// <summary>
        /// Update the particles using the firework algorithm.
        /// </summary>
        private void Firework()
        {
            for (int index = 0; index < this.Particles.Length; index++)
            {
                // Get the particle data.
                float angleX = this.Particles[index].Data.X;
                float angleY = this.Particles[index].Data.Y;
                float life = this.Particles[index].Data.Z;

                // Comment to disable rotation.
                angleX += 0.01f;
                angleY += 0.01f;

                // Mantain the angleX between 0 and 360.
                if (angleX > 360)
                {
                    angleX = 0;
                }

                // Mantain the angleY between 0 and 360.
                if (angleY > 360)
                {
                    angleY = 0;
                }

                // Increase the life time of the particle.
                life += 0.1f;

                // Calculate the new position of the particle.
                float cosX = life * (float)Math.Cos(angleX);
                float cosY = life * (float)Math.Cos(angleY);
                float sinX = life * (float)Math.Sin(angleX);
                float sinY = life * life * (float)Math.Sin(angleY);

                Vector3 distanceFromStart = new Vector3(cosX * cosY, sinX * cosY, sinY);

                distanceFromStart.X += this.Position.X;
                distanceFromStart.Y += this.Position.Y;
                distanceFromStart.Z += this.Position.Z;

                this.Particles[index].Position = distanceFromStart;

                // Calculate the new color of the particle.
                this.Particles[index].Color = new Color(new Vector4(this.Particles[index].Color.ToVector3(), 1.0f - (life / (float)this.LifeTime)));

                // Kill the firework if the life time has trepassed the limit.
                if (life > this.LifeTime)
                {
                    this.AliveField = false;
                }

                // Store the particle data.
                this.Particles[index].Data = new Vector4(angleX, angleY, life, 0);
            }
        }

        #endregion

        #region Ring

        /// <summary>
        /// Update the particles using the ring algorithm.
        /// </summary>
        private void Ring()
        {
            for (int index = 0; index < this.Particles.Length; index++)
            {
                // Get the particle data.
                float angleX = this.Particles[index].Data.X;
                float angleY = this.Particles[index].Data.Y;
                float life = this.Particles[index].Data.Z;

                // Comment to disable rotation.
                angleX += 0.02f;
                angleY += 0.02f;

                // Mantain the angleX between 0 and 360.
                if (angleX > 360)
                {
                    angleX = 0;
                }

                // Mantain the angleY between 0 and 360.
                if (angleY > 360)
                {
                    angleY = 0;
                }

                // Increase the life time of the particle.
                life += this.LifeIncrement;

                // Calculate the new position of the particle.
                float cosX = life * (float)Math.Cos(angleX);
                float cosY = life * (float)Math.Cos(angleY);
                float sinX = life * (float)Math.Sin(angleX);
                float sinY = life * (float)Math.Sin(angleY);

                Vector3 distanceFromStart = new Vector3(cosX, 0, sinX);

                distanceFromStart.X += this.Position.X;
                distanceFromStart.Y += this.Position.Y;
                distanceFromStart.Z += this.Position.Z;

                this.Particles[index].Position = distanceFromStart;

                // Calculate the new color of the particle.
                this.Particles[index].Color = new Color(new Vector4(this.Particles[index].Color.ToVector3(), 1.0f - (life / (float)this.LifeTime)));

                // Kill the firework if the life time has trepassed the limit.
                if (life > this.LifeTime)
                {
                    this.AliveField = false;
                }

                // Store the particle data.
                this.Particles[index].Data = new Vector4(angleX, angleY, life, 0);
            }
        }

        #endregion

        #region Losangle

        /// <summary>
        /// Update the particles using the losangle algorithm.
        /// </summary>
        private void Losangle()
        {
            for (int index = 0; index < this.Particles.Length; index++)
            {
                // Get the particle data.
                float angleX = this.Particles[index].Data.X;
                float angleY = this.Particles[index].Data.Y;
                float life = this.Particles[index].Data.Z;

                // Comment to disable rotation.
                angleX += 0.02f;
                angleY += 0.02f;

                // Mantain the angleX between 0 and 360.
                if (angleX > 360)
                {
                    angleX = 0;
                }

                // Mantain the angleY between 0 and 360.
                if (angleY > 360)
                {
                    angleY = 0;
                }

                // Increase the life time of the particle.
                life += this.LifeIncrement;

                // Calculate the new position of the particle.
                float cosX = life * (float)Math.Cos(angleX);
                float cosY = life * (float)Math.Cos(angleY);
                float sinX = life * (float)Math.Sin(angleX);
                float sinY = life * (float)Math.Sin(angleY);

                Vector3 distanceFromStart = new Vector3(cosX * cosY, 1, sinX * sinY);

                distanceFromStart.X += this.Position.X;
                distanceFromStart.Y = this.Position.Y;
                distanceFromStart.Z += this.Position.Z;

                this.Particles[index].Position = distanceFromStart;

                // Calculate the new color of the particle.
                this.Particles[index].Color = new Color(new Vector4(this.Particles[index].Color.ToVector3(), 1.0f - (life / (float)this.LifeTime)));

                // Kill the firework if the life time has trepassed the limit.
                if (life > this.LifeTime)
                {
                    this.AliveField = false;
                }

                // Store the particle data.
                this.Particles[index].Data = new Vector4(angleX, angleY, life, 0);
            }
        }

        #endregion

        #region Flash 0°

        /// <summary>
        /// Update the particles using the flash algorithm at 0°.
        /// </summary>
        private void Flash0()
        {
            for (int index = 0; index < this.Particles.Length; index++)
            {
                // Get the particle data.
                float angleX = this.Particles[index].Data.X;
                float angleY = this.Particles[index].Data.Y;
                float life = this.Particles[index].Data.Z;

                // Comment to disable rotation.
                //angleX += 0.02f;
                //angleY += 0.02f;

                // Mantain the angleX between 0 and 360.
                if (angleX > 360)
                {
                    angleX = 0;
                }

                // Mantain the angleY between 0 and 360.
                if (angleY > 360)
                {
                    angleY = 0;
                }

                // Increase the life time of the particle.
                life += this.LifeIncrement;

                // Calculate the new position of the particle.
                float cosX = life * (float)Math.Cos(angleX);
                float cosY = life * (float)Math.Cos(angleY);
                float sinX = life * (float)Math.Sin(angleX);
                float sinY = life * (float)Math.Sin(angleY);

                Vector3 distanceFromStart = new Vector3(cosX * sinY, 0, 0);

                distanceFromStart.X += this.Position.X;
                distanceFromStart.Y += this.Position.Y;
                distanceFromStart.Z += this.Position.Z;

                this.Particles[index].Position = distanceFromStart;

                // Calculate the new color of the particle.
                this.Particles[index].Color = new Color(new Vector4(this.Particles[index].Color.ToVector3(), 1.0f - (life / (float)this.LifeTime)));

                // Kill the firework if the life time has trepassed the limit.
                if (life > this.LifeTime)
                {
                    this.AliveField = false;
                }

                // Store the particle data.
                this.Particles[index].Data = new Vector4(angleX, angleY, life, 0);
            }
        }

        #endregion

        #region Flash 45°

        /// <summary>
        /// Update the particles using the flash algorithm at 45°.
        /// </summary>
        private void Flash45()
        {
            for (int index = 0; index < this.Particles.Length; index++)
            {
                // Get the particle data.
                float angleX = this.Particles[index].Data.X;
                float angleY = this.Particles[index].Data.Y;
                float life = this.Particles[index].Data.Z;

                // Comment to disable rotation.
                //angleX += 0.02f;
                //angleY += 0.02f;

                // Mantain the angleX between 0 and 360.
                if (angleX > 360)
                {
                    angleX = 0;
                }

                // Mantain the angleY between 0 and 360.
                if (angleY > 360)
                {
                    angleY = 0;
                }

                // Increase the life time of the particle.
                life += this.LifeIncrement;

                // Calculate the new position of the particle.
                float cosX = life * (float)Math.Cos(angleX);
                float cosY = life * (float)Math.Cos(angleY);
                float sinX = life * (float)Math.Sin(angleX);
                float sinY = life * (float)Math.Sin(angleY);

                Vector3 distanceFromStart = new Vector3(cosX * sinY, 0, cosX * sinY);

                distanceFromStart.X += this.Position.X;
                distanceFromStart.Y += this.Position.Y;
                distanceFromStart.Z += this.Position.Z;

                this.Particles[index].Position = distanceFromStart;

                // Calculate the new color of the particle.
                this.Particles[index].Color = new Color(new Vector4(this.Particles[index].Color.ToVector3(), 1.0f - (life / (float)this.LifeTime)));

                // Kill the firework if the life time has trepassed the limit.
                if (life > this.LifeTime)
                {
                    this.AliveField = false;
                }

                // Store the particle data.
                this.Particles[index].Data = new Vector4(angleX, angleY, life, 0);
            }
        }

        #endregion

        #region Flash 90°

        /// <summary>
        /// Update the particles using the flash algorithm at 90°.
        /// </summary>
        private void Flash90()
        {
            for (int index = 0; index < this.Particles.Length; index++)
            {
                // Get the particle data.
                float angleX = this.Particles[index].Data.X;
                float angleY = this.Particles[index].Data.Y;
                float life = this.Particles[index].Data.Z;

                // Comment to disable rotation.
                //angleX += 0.02f;
                //angleY += 0.02f;

                // Mantain the angleX between 0 and 360.
                if (angleX > 360)
                {
                    angleX = 0;
                }

                // Mantain the angleY between 0 and 360.
                if (angleY > 360)
                {
                    angleY = 0;
                }

                // Increase the life time of the particle.
                life += this.LifeIncrement;

                // Calculate the new position of the particle.
                float cosX = life * (float)Math.Cos(angleX);
                float cosY = life * (float)Math.Cos(angleY);
                float sinX = life * (float)Math.Sin(angleX);
                float sinY = life * (float)Math.Sin(angleY);

                Vector3 distanceFromStart = new Vector3(0, 0, cosX * sinY);

                distanceFromStart.X += this.Position.X;
                distanceFromStart.Y += this.Position.Y;
                distanceFromStart.Z += this.Position.Z;

                this.Particles[index].Position = distanceFromStart;

                // Calculate the new color of the particle.
                this.Particles[index].Color = new Color(new Vector4(this.Particles[index].Color.ToVector3(), 1.0f - (life / (float)this.LifeTime)));

                // Kill the firework if the life time has trepassed the limit.
                if (life > this.LifeTime)
                {
                    this.AliveField = false;
                }

                // Store the particle data.
                this.Particles[index].Data = new Vector4(angleX, angleY, life, 0);
            }
        }

        #endregion

        #region Flash 135°

        /// <summary>
        /// Update the particles using the flash algorithm at 135°.
        /// </summary>
        private void Flash135()
        {
            for (int index = 0; index < this.Particles.Length; index++)
            {
                // Get the particle data.
                float angleX = this.Particles[index].Data.X;
                float angleY = this.Particles[index].Data.Y;
                float life = this.Particles[index].Data.Z;

                // Comment to disable rotation.
                //angleX += 0.02f;
                //angleY += 0.02f;

                // Mantain the angleX between 0 and 360.
                if (angleX > 360)
                {
                    angleX = 0;
                }

                // Mantain the angleY between 0 and 360.
                if (angleY > 360)
                {
                    angleY = 0;
                }

                // Increase the life time of the particle.
                life += this.LifeIncrement;

                // Calculate the new position of the particle.
                float cosX = life * (float)Math.Cos(angleX);
                float cosY = life * (float)Math.Cos(angleY);
                float sinX = life * (float)Math.Sin(angleX);
                float sinY = life * (float)Math.Sin(angleY);

                Vector3 distanceFromStart = new Vector3(-cosX * sinY, 0, cosX * sinY);

                distanceFromStart.X += this.Position.X;
                distanceFromStart.Y += this.Position.Y;
                distanceFromStart.Z += this.Position.Z;

                this.Particles[index].Position = distanceFromStart;

                // Calculate the new color of the particle.
                this.Particles[index].Color = new Color(new Vector4(this.Particles[index].Color.ToVector3(), 1.0f - (life / (float)this.LifeTime)));

                // Kill the firework if the life time has trepassed the limit.
                if (life > this.LifeTime)
                {
                    this.AliveField = false;
                }

                // Store the particle data.
                this.Particles[index].Data = new Vector4(angleX, angleY, life, 0);
            }
        }

        #endregion

        #region Pillar

        /// <summary>
        /// Update the particles using the pillar algorithm.
        /// </summary>
        private void Pillar()
        {
            for (int index = 0; index < this.Particles.Length; index++)
            {
                // Get the particle data.
                float angleX = this.Particles[index].Data.X;
                float angleY = this.Particles[index].Data.Y;
                float life = this.Particles[index].Data.Z;

                // Comment to disable rotation.
                //angleX += 0.02f;
                angleY += 0.03f;

                // Mantain the angleX between 0 and 360.
                if (angleX > 360)
                {
                    angleX = 0;
                }

                // Mantain the angleY between 0 and 360.
                if (angleY > 360)
                {
                    angleY = 0;
                }

                // Increase the life time of the particle.
                life += this.LifeIncrement;

                // Calculate the new position of the particle.
                float cosX = life * (float)Math.Cos(angleX);
                float cosY = (2.5f - 2.5f * (life / (float)this.LifeTime)) * (float)Math.Cos(angleY);
                float sinX = life * (float)Math.Sin(angleX);
                float sinY = (2.5f - 2.5f * (life / (float)this.LifeTime)) * (float)Math.Sin(angleY);

                Vector3 distanceFromStart = new Vector3(cosY * sinY, 0, Math.Abs(cosX * sinX));

                distanceFromStart.X += this.Position.X;
                distanceFromStart.Y += this.Position.Y;
                distanceFromStart.Z += this.Position.Z;

                this.Particles[index].Position = distanceFromStart;

                // Calculate the new color of the particle.
                this.Particles[index].Color = new Color(new Vector4(this.Particles[index].Color.ToVector3(), 1.0f - (life / (float)this.LifeTime)));

                // Kill the firework if the life time has trepassed the limit.
                if (life > this.LifeTime)
                {
                    this.AliveField = false;
                }

                // Store the particle data.
                this.Particles[index].Data = new Vector4(angleX, angleY, life, 0);
            }
        }

        #endregion

        #region Sphere

        /// <summary>
        /// Update the particles using the sphere algorithm.
        /// </summary>
        private void Sphere()
        {
            for (int index = 0; index < this.Particles.Length; index++)
            {
                // Get the particle data.
                float angleX = this.Particles[index].Data.X;
                float angleY = this.Particles[index].Data.Y;
                float life = this.Particles[index].Data.Z;

                // Comment to disable rotation.
                angleX += 0.02f;
                angleY += 0.01f;

                // Mantain the angleX between 0 and 360.
                if (angleX > 360)
                {
                    angleX = 0;
                }

                // Mantain the angleY between 0 and 360.
                if (angleY > 360)
                {
                    angleY = 0;
                }

                // Increase the life time of the particle.
                life += this.LifeIncrement;

                // Calculate the new position of the particle.
                float cosX = 2.5f * 1.0f * (float)Math.Cos(angleX);
                float cosY = 2.5f * 1.0f * (float)Math.Cos(angleY);
                float sinX = 2.5f * 1.0f * (float)Math.Sin(angleX);
                float sinY = 2.5f * 2.5f * (float)Math.Sin(angleY);

                Vector3 distanceFromStart = new Vector3(cosX * cosY, sinX * cosY, sinY);

                distanceFromStart.X += this.Position.X;
                distanceFromStart.Y += this.Position.Y;
                distanceFromStart.Z += this.Position.Z;

                this.Particles[index].Position = distanceFromStart;

                // Calculate the new color of the particle.
                this.Particles[index].Color = new Color(new Vector4(this.Particles[index].Color.ToVector3(), 1.0f - (life / (float)this.LifeTime)));

                // Kill the firework if the life time has trepassed the limit.
                if (life > this.LifeTime)
                {
                    this.AliveField = false;
                }

                // Store the particle data.
                this.Particles[index].Data = new Vector4(angleX, angleY, life, 0);
            }
        }

        #endregion

        #region Slice

        /// <summary>
        /// Update the particles using the Slice algorithm.
        /// </summary>
        private void Slice()
        {
            for (int index = 0; index < this.Particles.Length; index++)
            {
                // Get the particle data.
                float angleX = this.Particles[index].Data.X;
                float angleY = this.Particles[index].Data.Y;
                float life = this.Particles[index].Data.Z;

                // Comment to disable rotation.
                //angleX += 0.02f;
                //angleY += 0.02f;

                // Mantain the angleX between 0 and 360.
                if (angleX > 360)
                {
                    angleX = 0;
                }

                // Mantain the angleY between 0 and 360.
                if (angleY > 360)
                {
                    angleY = 0;
                }

                // Increase the life time of the particle.
                life += this.LifeIncrement;

                // Calculate the new position of the particle.
                float cosX = life * (float)Math.Cos(angleX);
                float cosY = life * (float)Math.Cos(angleY);
                float sinX = life * (float)Math.Sin(angleX);
                float sinY = life * (float)Math.Sin(angleY);

                Vector3 distanceFromStart = new Vector3((float)Math.Abs(cosX * sinY), 0, (float)Math.Abs(cosX * sinY));

                distanceFromStart.X += this.Position.X;
                distanceFromStart.Y += this.Position.Y;
                distanceFromStart.Z += this.Position.Z;

                this.Particles[index].Position = distanceFromStart;

                // Calculate the new color of the particle.
                this.Particles[index].Color = new Color(new Vector4(this.Particles[index].Color.ToVector3(), 1.0f - (life / (float)this.LifeTime)));

                // Kill the firework if the life time has trepassed the limit.
                if (life > this.LifeTime)
                {
                    this.AliveField = false;
                }

                // Store the particle data.
                this.Particles[index].Data = new Vector4(angleX, angleY, life, 0);
            }
        }

        #endregion
    }
}
