using System;
using System.Collections.Generic;
using System.Text;

namespace IRTaktiks.Components.Logic
{
    /// <summary>
    /// The logic representation of the unit's atributes.
    /// </summary>
    public class UnitAttributes
    {
        #region Properties

        /// <summary>
        /// The strength of the unit. Influences the attack power.
        /// </summary>
        private int StrengthField;

        /// <summary>
        /// The strength of the unit. Influences the attack power.
        /// </summary>
        public int Strength
        {
            get { return StrengthField; }
        }

        /// <summary>
        /// The agility of the unit. Influences the time.
        /// </summary>
        private int AgilityField;

        /// <summary>
        /// The agility of the unit. Influences the time.
        /// </summary>
        public int Agility
        {
            get { return AgilityField; }
        }

        /// <summary>
        /// The vitality of the unit. Influences the deffense.
        /// </summary>
        private int VitalityField;
        
        /// <summary>
        /// The vitality of the unit. Influences the deffense.
        /// </summary>
        public int Vitality
        {
            get { return VitalityField; }
        }

        /// <summary>
        /// The magic of the unit. Influences the magic attack and deffense.
        /// </summary>
        private int MagicField;

        /// <summary>
        /// The magic of the unit. Influences the magic attack and deffense.
        /// </summary>
        public int Magic
        {
            get { return MagicField; }
        }

        /// <summary>
        /// The dexteriry of unit. Influences the range of attacks.
        /// </summary>
        private int DexterityField;

        /// <summary>
        /// The dexteriry of unit. Influences the range of attacks.
        /// </summary>
        public int Dexterity
        {
            get { return DexterityField; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="strength">The strenght value.</param>
        /// <param name="agility">The agility value.</param>
        /// <param name="vitality">The vitality value.</param>
        /// <param name="magic">The magic value.</param>
        /// <param name="dexterity">The dexterity value.</param>
        public UnitAttributes(int strength, int agility, int vitality, int magic, int dexterity)
        {
            this.StrengthField = strength;
            this.AgilityField = agility;
            this.VitalityField = vitality;
            this.MagicField = magic;
            this.DexterityField = dexterity;
        }

        #endregion
    }
}