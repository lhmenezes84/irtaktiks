using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace IRTaktiks.Components.Managers
{
    public class TextureManager
    {
        #region Singleton

        /// <summary>
        /// The instance of the class.
        /// </summary>
        private static TextureManager InstanceField;

        /// <summary>
        /// The instance of the class.
        /// </summary>
        public static TextureManager Instance
        {
            get { if (InstanceField == null) InstanceField = new TextureManager(); return InstanceField; }
        }

        /// <summary>
        /// Private constructor.
        /// </summary>
        private TextureManager()
        { }

        #endregion

        #region Initialize

        /// <summary>
        /// Initialize the instance of the class.
        /// </summary>
        public void Initialize(Game game)
        {
            this.TitleScreenField = game.Content.Load<Texture2D>("Sprites/TitleScreen");
            
            this.TerrainHeightmapField = game.Content.Load<Texture2D>("Terrains/Terrain");

            this.GrassTerrainField = game.Content.Load<Texture2D>("Textures/Grass");
            this.RockTerrainField = game.Content.Load<Texture2D>("Textures/Rock");
            this.SandTerrainField = game.Content.Load<Texture2D>("Textures/Sand");
            this.SnowTerrainField = game.Content.Load<Texture2D>("Textures/Snow");

            this.BackgroundMenuField = game.Content.Load<Texture2D>("Sprites/BackgroundMenu");
            this.PlayerOneStatusField = game.Content.Load<Texture2D>("Sprites/PlayerStatusBlue");
            this.PlayerTwoStatusField = game.Content.Load<Texture2D>("Sprites/PlayerStatusRed");

            this.UnitStatusReadyField = game.Content.Load<Texture2D>("Sprites/UnitStatusGreen");
            this.UnitStatusDeadField = game.Content.Load<Texture2D>("Sprites/UnitStatusRed");
            this.UnitStatusWaitingField = game.Content.Load<Texture2D>("Sprites/UnitStatusYellow");

            this.DefaultMenuItemField = game.Content.Load<Texture2D>("Sprites/DefaultMenuItem");
            this.SelectedMenuItemField = game.Content.Load<Texture2D>("Sprites/SelectedMenuItem");
        }

        #endregion

        #region Textures

        #region Title Screen

        /// <summary>
        /// The title screen texture.
        /// </summary>
        private Texture2D TitleScreenField;

        /// <summary>
        /// The title screen texture.
        /// </summary>
        public Texture2D TitleScreen
        {
            get { return TitleScreenField; }
        }

        #endregion

        #region Terrain

        /// <summary>
        /// The terrain heightmap of the game.
        /// </summary>
        private Texture2D TerrainHeightmapField;

        /// <summary>
        /// The terrain heightmap of the game.
        /// </summary>
        public Texture2D TerrainHeightMap
        {
            get { return TerrainHeightmapField; }
            set { TerrainHeightmapField = value; }
        }
        
        /// <summary>
        /// The grass texture of the terrain.
        /// </summary>
        private Texture2D GrassTerrainField;

        /// <summary>
        /// The grass texture of the terrain.
        /// </summary>
        public Texture2D GrassTerrain
        {
            get { return GrassTerrainField; }
        }

        /// <summary>
        /// The rock texture of the terrain.
        /// </summary>
        private Texture2D RockTerrainField;

        /// <summary>
        /// The rock texture of the terrain.
        /// </summary>
        public Texture2D RockTerrain
        {
            get { return RockTerrainField; }
        }

        /// <summary>
        /// The sand texture of the terrain.
        /// </summary>
        private Texture2D SandTerrainField;

        /// <summary>
        /// The sand texture of the terrain.
        /// </summary>
        public Texture2D SandTerrain
        {
            get { return SandTerrainField; }
        }

        /// <summary>
        /// The snow texture of the terrain.
        /// </summary>
        private Texture2D SnowTerrainField;

        /// <summary>
        /// The snow texture of the terrain.
        /// </summary>
        public Texture2D SnowTerrain
        {
            get { return SnowTerrainField; }
        }

        #endregion

        #region Player Menu

        /// <summary>
        /// The background of the player's menu.
        /// </summary>
        private Texture2D BackgroundMenuField;

        /// <summary>
        /// The background of the player's menu.
        /// </summary>
        public Texture2D BackgroundMenu
        {
            get { return BackgroundMenuField; }
        }

        /// <summary>
        /// The status texture for the player one.
        /// </summary>
        private Texture2D PlayerOneStatusField;

        /// <summary>
        /// The status texture for the player one.
        /// </summary>
        public Texture2D PlayerOneStatus
        {
            get { return PlayerOneStatusField; }
        }

        /// <summary>
        /// The status texture for the player two.
        /// </summary>
        private Texture2D PlayerTwoStatusField;

        /// <summary>
        /// The status texture for the player two.
        /// </summary>
        public Texture2D PlayerTwoStatus
        {
            get { return PlayerTwoStatusField; }
        }

        #endregion

        #region Unit Menu

        /// <summary>
        /// The status texture for the unit when is ready.
        /// </summary>
        private Texture2D UnitStatusReadyField;

        /// <summary>
        /// The status texture for the unit when is ready.
        /// </summary>
        public Texture2D UnitStatusReady
        {
            get { return UnitStatusReadyField; }
        }

        /// <summary>
        /// The status texture for the unit when is dead.
        /// </summary>
        private Texture2D UnitStatusDeadField;

        /// <summary>
        /// The status texture for the unit when is dead.
        /// </summary>
        public Texture2D UnitStatusDead
        {
            get { return UnitStatusDeadField; }
        }

        /// <summary>
        /// The status texture for the unit when is waiting.
        /// </summary>
        private Texture2D UnitStatusWaitingField;

        /// <summary>
        /// The status texture for the unit when is waiting.
        /// </summary>
        public Texture2D UnitStatusWaiting
        {
            get { return UnitStatusWaitingField; }
        }

        #endregion

        #region Unit Submenu

        /// <summary>
        /// The default texture for the menu item.
        /// </summary>
        private Texture2D DefaultMenuItemField;

        /// <summary>
        /// The default texture for the menu item.
        /// </summary>
        public Texture2D DefaultMenuItem
        {
            get { return DefaultMenuItemField; }
        }

        /// <summary>
        /// The default texture for the menu item.
        /// </summary>
        private Texture2D SelectedMenuItemField;

        /// <summary>
        /// The default texture for the menu item.
        /// </summary>
        public Texture2D SelectedMenuItem
        {
            get { return SelectedMenuItemField; }
        }

        #endregion

        #endregion
    }
}
