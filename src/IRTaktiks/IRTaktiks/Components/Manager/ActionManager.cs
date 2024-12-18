using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Menu;
using IRTaktiks.Components.Playable;
using IRTaktiks.Input.EventArgs;
using IRTaktiks.Input;
using IRTaktiks.Components.Scenario;
using IRTaktiks.Components.Interaction;
using IRTaktiks.Components.Action;

namespace IRTaktiks.Components.Manager
{
    /// <summary>
    /// Manager of unit's commands.
    /// </summary>
    public class ActionManager : DrawableGameComponent
    {
        #region Properties

        /// <summary>
        /// The unit owner of the commands.
        /// </summary>
        private Unit UnitField;

        /// <summary>
        /// The unit owner of the commands.
        /// </summary>
        public Unit Unit
        {
            get { return UnitField; }
        }

        /// <summary>
        /// The actions of the unit.
        /// </summary>
        private List<ActionMenu> ActionsField;

        /// <summary>
        /// The actions of the unit.
        /// </summary>
        public List<ActionMenu> Actions
        {
            get { return ActionsField; }
        }

        /// <summary>
        /// The mover of the unit.
        /// </summary>
        private Mover MoverField;

        /// <summary>
        /// The mover of the unit.
        /// </summary>
        public Mover Mover
        {
            get { return MoverField; }
        }
        
        /// <summary>
        /// The aim of the unit.
        /// </summary>
        private Aim AimField;

        /// <summary>
        /// The aim of the unit.
        /// </summary>
        public Aim Aim
        {
            get { return AimField; }
        }

        /// <summary>
        /// The top left position of the actions and commands.
        /// </summary>
        private Vector2 PositionField;

        /// <summary>
        /// The top left position of the actions and commands.
        /// </summary>
        public Vector2 Position
        {
            get { return PositionField; }
        }
        
        /// <summary>
        /// Indicates if the menu needs to be updated.
        /// </summary>
        private bool ChangedField;

        /// <summary>
        /// Indicates if the menu needs to be updated.
        /// </summary>
        public bool Changed
        {
            get { return ChangedField; }
        }

        /// <summary>
        /// Indicates if the menu is freezed and its selected items cannot be changed.
        /// </summary>
        private bool FreezedField;

        /// <summary>
        /// Indicates if the menu is freezed and its selected items cannot be changed.
        /// </summary>
        public bool Freezed
        {
            get { return FreezedField; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="game">The instance of game that is running.</param>
        /// <param name="unit">The unit owner of the commands.</param>
        public ActionManager(Game game, Unit unit)
            : base(game)
        {
            // Set the unit owner of the commands.
            this.UnitField = unit;

            this.ChangedField = true;
            this.FreezedField = false;

            // Create the actions.
            this.ActionsField = new List<ActionMenu>();

            // Create the mover.
            this.MoverField = new Mover(this.Unit);
            this.Mover.Started += new Mover.StartedMoverEventHandler(Mover_Started);
            this.Mover.Moved += new Mover.MovedEventHandler(Mover_Moved);

            // Create the aim.
            this.AimField = new Aim(this.Unit);
            this.Aim.Started += new Aim.StartedAimEventHandler(Aim_Started);
            this.Aim.Aimed += new Aim.AimedEventHandler(Aim_Aimed);

            // Set the top left position for the player one.
            if (this.Unit.Player.PlayerIndex == PlayerIndex.One)
            {
                this.PositionField = new Vector2(0, 323);
            }

            // Set the top left position for the player two
            if (this.Unit.Player.PlayerIndex == PlayerIndex.Two)
            {
                this.PositionField = new Vector2(IRTSettings.Default.Width - TextureManager.Instance.Sprites.Menu.ItemPlayerOne.Width, 323);
            }

            // Construct the menu.
            this.Construct();

            // Input
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
            // Check if the menu needs updates
            if (this.Changed)
            {
                // Set the positions of the items.
                for (int index = 0, positionIndex = 0; index < this.Actions.Count; index++)
                {
                    this.Actions[index].Position = new Vector2(this.Position.X, this.Position.Y + (positionIndex++ * TextureManager.Instance.Sprites.Menu.ItemPlayerOne.Height));

                    // If the menu item is selected, must set the position for its subitems.
                    if (this.Actions[index].Selected)
                    {
                        // Set the position for all the subitems.
                        for (int subindex = 0; subindex < this.Actions[index].Commands.Count; subindex++)
                        {
                            this.Actions[index].Commands[subindex].Position = new Vector2(this.Position.X, this.Position.Y + (positionIndex++ * TextureManager.Instance.Sprites.Menu.ItemPlayerOne.Height));
                        }
                    }
                }

                this.ChangedField = false;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Called when the DrawableGameComponent needs to be drawn. Override this method
        //  with component-specific drawing code.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            IRTGame game = this.Game as IRTGame;

            int items = 0;

            // Draw the items.
            for (int index = 0; index < this.Actions.Count; index++, items++)
            {
                this.Actions[index].Draw(game.SpriteManager);

                // If the item is selected, draws the subitems.
                if (this.Actions[index].Selected)
                {
                    // Draws all the subitems.
                    for (int subindex = 0; subindex < this.Actions[index].Commands.Count; subindex++, items++)
                    {
                        this.Actions[index].Commands[subindex].Draw(game.SpriteManager);
                    }
                }
            }

            // Draws the background of menu.
            game.SpriteManager.Draw(TextureManager.Instance.Sprites.Menu.Background, new Vector2(this.Position.X, 292), new Rectangle(0, 292, TextureManager.Instance.Sprites.Menu.ItemPlayerOne.Width, items * TextureManager.Instance.Sprites.Menu.ItemPlayerOne.Height + ((int)this.Position.Y) - 292), Color.White, 40);

            // Draw the mover.
            this.Mover.Draw(game.SpriteManager, game.AreaManager, gameTime);

            // Draw the aim.
            this.Aim.Draw(game.SpriteManager, game.AreaManager, gameTime);

            base.Draw(gameTime);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Construct the menu.
        /// </summary>
        private void Construct()
        {
            // Create the actions.
            ActionMenu moveAction = new ActionMenu(this.Unit, "Move", ActionMenu.ActionMenuType.Move);
            ActionMenu attackAction = new ActionMenu(this.Unit, "Attack", ActionMenu.ActionMenuType.Attack);
            ActionMenu skillsAction = new ActionMenu(this.Unit, "Skills", ActionMenu.ActionMenuType.Skills);
            ActionMenu itemsAction = new ActionMenu(this.Unit, "Items", ActionMenu.ActionMenuType.Items);

            // Handle the Execute events.
            moveAction.Execute += new ActionMenu.ExecuteEventHandler(Action_Execute);
            attackAction.Execute += new ActionMenu.ExecuteEventHandler(Action_Execute);
            skillsAction.Execute += new ActionMenu.ExecuteEventHandler(Action_Execute);
            itemsAction.Execute += new ActionMenu.ExecuteEventHandler(Action_Execute);

            // Add the attacks.
            foreach (Attack attack in this.Unit.Attacks)
            {
                CommandMenu menu = new CommandMenu(attack, ActionMenu.ActionMenuType.Attack);
                menu.Execute += new ActionMenu.ExecuteEventHandler(Attack_Execute);
                attackAction.Commands.Add(menu);
            }

            // Add the skills.
            foreach (Skill skill in this.Unit.Skills)
            {
                CommandMenu menu = new CommandMenu(skill, ActionMenu.ActionMenuType.Skills);
                menu.Execute += new ActionMenu.ExecuteEventHandler(Skill_Execute);
                skillsAction.Commands.Add(menu);
            }

            // Add the items.
            foreach (Item item in this.Unit.Items)
            {
                CommandMenu menu = new CommandMenu(item, ActionMenu.ActionMenuType.Items);
                menu.Execute += new ActionMenu.ExecuteEventHandler(Item_Execute);
                itemsAction.Commands.Add(menu);
            }

            // Add the actions.
            this.Actions.Add(moveAction);
            this.Actions.Add(attackAction);
            this.Actions.Add(skillsAction);
            this.Actions.Add(itemsAction);
        }

        /// <summary>
        /// Reset the logic properties of the menu.
        /// </summary>
        private void Reset()
        {
            this.UnselectMenu();

            this.ChangedField = true;
            this.FreezedField = false;

            this.Mover.Deactivate();
            this.Aim.Deactivate();
        }

        /// <summary>
        /// Unselect all items in the menu.
        /// </summary>
        private void UnselectMenu()
        {
            for (int index = 0; index < this.Actions.Count; index++)
            {
                this.UnselectAction(this.Actions[index]);
            }
        }
        
        /// <summary>
        /// Unselect one action menu and its childs.
        /// </summary>
        /// <param name="actionMenu">The action menu to unselect.</param>
        private void UnselectAction(ActionMenu actionMenu)
        {
            for (int index = 0; index < actionMenu.Commands.Count; index++)
            {
                actionMenu.Commands[index].Selected = false;
            }

            actionMenu.Selected = false;
        }

        /// <summary>
        /// Unselect all the commands in the menu.
        /// </summary>
        private void UnselectCommands()
        {
            for (int index = 0; index < this.Actions.Count; index++)
            {
                for (int subindex = 0; subindex < this.Actions[index].Commands.Count; subindex++)
                {
                    this.Actions[index].Commands[subindex].Selected = false;
                }
            }
        }

        #endregion

        #region Input Handling

        /// <summary>
        /// Input Handling for Cursor Down event.
        /// </summary>
        private void CursorDown(object sender, CursorDownArgs e)
        {
            // Check if the items can be selected or executed.
            if (this.Enabled && this.Visible && !this.Freezed)
            {
                // Define if the touch was inside the menu x area.
                float limitX = this.Position.X + TextureManager.Instance.Sprites.Menu.ItemPlayerOne.Width;
                if (e.Position.X < limitX && e.Position.X > this.Position.X)
                {
                    // Define if the touch was inside the menu y area.
                    if (e.Position.Y > this.Position.Y && e.Position.Y < IRTSettings.Default.Height)
                    {
                        // Updates the menu.
                        this.ChangedField = true;                        
                        
                        // Deactivate the aim.
                        if (this.Aim.Enabled)
                        {
                            this.Aim.Deactivate();
                        }
                        
                        // Deactivate the mover.
                        if (this.Mover.Enabled)
                        {
                            this.Mover.Deactivate();
                        }

                        // Check if the touch was inside some action item.
                        for (int index = 0; index < this.Actions.Count; index++)
                        {
                            // If action touched.
                            float limitY = this.Actions[index].Position.Y + TextureManager.Instance.Sprites.Menu.ItemPlayerOne.Height;
                            if (e.Position.Y < limitY && e.Position.Y > this.Actions[index].Position.Y)
                            {
                                // Check if its an unselection.
                                if (this.Actions[index].Selected)
                                {
                                    this.UnselectAction(this.Actions[index]);
                                }
                                else
                                {
                                    this.UnselectMenu();
                                    
                                    this.Actions[index].Selected = true;
                                    this.Actions[index].RaiseExecute();
                                }
                                
                                // Action menu was un/selected.
                                return;
                            }
                        }

                        // Check if the touch was inside some command item.
                        for (int index = 0; index < this.Actions.Count; index++)
                        {
                            // Check the childs only if the action menu is selected.
                            if (this.Actions[index].Selected)
                            {
                                // Check all commands.
                                for (int subindex = 0; subindex < this.Actions[index].Commands.Count; subindex++)
                                {
                                    // If command touched.
                                    float limitY = this.Actions[index].Commands[subindex].Position.Y + TextureManager.Instance.Sprites.Menu.ItemPlayerOne.Height;
                                    if (e.Position.Y < limitY && e.Position.Y > this.Actions[index].Commands[subindex].Position.Y)
                                    {
                                        // Check if the command can be executed.
                                        if (this.Actions[index].Commands[subindex].Command.Enabled)
                                        {
                                            // Check if its an unselection
                                            if (this.Actions[index].Commands[subindex].Selected)
                                            {
                                                this.Actions[index].Commands[subindex].Selected = false;
                                            }
                                            else
                                            {
                                                // Unselect all commands.
                                                this.UnselectCommands();

                                                this.Actions[index].Commands[subindex].Selected = true;
                                                this.Actions[index].Commands[subindex].RaiseExecute();
                                            }
                                        }

                                        // Command menu was un/selected.
                                        return;
                                    }
                                }
                            }
                        }

                        // Touch outside the area of the menu.
                        return;
                    }
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handle the Execute event for actions.
        /// </summary>
        /// <param name="actionMenu">The menu that dispatched the event.</param>
        private void Action_Execute(ActionMenu actionMenu)
        {
            switch (actionMenu.Type)
            {
                case ActionMenu.ActionMenuType.Attack:
                    break;

                case ActionMenu.ActionMenuType.Items:
                    break;

                case ActionMenu.ActionMenuType.Move:
                    
                    this.Mover.Activate(actionMenu, this.Unit.Attributes.Range);
                    break;

                case ActionMenu.ActionMenuType.Skills:
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Handle the Execute event for attack commands.
        /// </summary>
        /// <param name="actionMenu">The menu that dispatched the event.</param>
        private void Attack_Execute(ActionMenu actionMenu)
        {
            CommandMenu commandMenu = actionMenu as CommandMenu;

            switch ((commandMenu.Command as Attack).Type)
            {
                case Attack.AttackType.Long:

                    this.Aim.Activate(actionMenu, this.Unit.Attributes.LongAttackRange);
                    break;

                case Attack.AttackType.Short:

                    this.Aim.Activate(actionMenu, this.Unit.Attributes.ShortAttackRange);
                    break;
            }
        }

        /// <summary>
        /// Handle the Execute event for skill commands.
        /// </summary>
        /// <param name="actionMenu">The menu that dispatched the event.</param>
        private void Skill_Execute(ActionMenu actionMenu)
        {
            CommandMenu commandMenu = actionMenu as CommandMenu;

            switch ((commandMenu.Command as Skill).Type)
            {
                case Skill.SkillType.Self:
                    
                    commandMenu.Command.Execute(this.Unit, this.Unit.Position);
                    this.Reset();
                    break;

                case Skill.SkillType.Target:

                    this.Aim.Activate(actionMenu, this.Unit.Attributes.SkillRange);
                    break;
            }
        }

        /// <summary>
        /// Handle the Execute event for item commands.
        /// </summary>
        /// <param name="actionMenu">The menu that dispatched the event.</param>
        private void Item_Execute(ActionMenu actionMenu)
        {
            CommandMenu commandMenu = actionMenu as CommandMenu;

            switch ((commandMenu.Command as Item).Type)
            {
                case Item.ItemType.Self: 
                    
                    commandMenu.Command.Execute(this.Unit, this.Unit.Position);
                    this.Reset();
                    break;

                case Item.ItemType.Target:

                    this.Aim.Activate(actionMenu, this.Unit.Attributes.SkillRange);
                    break;
            }
        }

        /// <summary>
        /// Handle the Started event from the mover.
        /// </summary>
        private void Mover_Started()
        {
            this.FreezedField = true;
        }
        
        /// <summary>
        /// Handle the Started event from the aim.
        /// </summary>
        private void Aim_Started()
        {
            this.FreezedField = true;
        }

        /// <summary>
        /// Handle the Moved event from the mover.
        /// </summary>
        /// <param name="actionMenu">The menu that requested the mover.</param>
        private void Mover_Moved(ActionMenu actionMenu)
        {
            this.Reset();
            
            // End the movement.
            this.Unit.Time = 0;
        }

        /// <summary>
        /// Called when the aim of the Heal magic is released.
        /// </summary>
        /// <param name="actionMenu">The menu that requested the mover.</param>
        /// <param name="target">The unit targeted by the aim. Null if the aim is over anything.</param>
        /// <param name="position">The last valid position of the aim.</param>
        private void Aim_Aimed(ActionMenu actionMenu, Unit target, Vector2 position)
        {
            this.Reset();
            
            // Execute the command.
            CommandMenu commandMenu = actionMenu as CommandMenu;
            commandMenu.Command.Execute(target, position);
        }

        #endregion
    }
}