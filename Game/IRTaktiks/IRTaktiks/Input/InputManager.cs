using System;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using IRTaktiks.Input.EventArgs;

namespace IRTaktiks.Input
{
    /// <summary>
    /// Responsible by input events through the Tuio listener.
    /// </summary>
    public class InputManager
    {
        #region Singleton

        /// <summary>
        /// Class instance.
        /// </summary>
        private static InputManager InstanceField;

        /// <summary>
        /// Class instance.
        /// </summary>
        public static InputManager Instance
        {
            get { if (InstanceField == null) InstanceField = new InputManager(); return InstanceField; }
        }

        /// <summary>
        /// Private constructor.
        /// </summary>
        private InputManager()
        {
			this.cursorStatus = new Dictionary<int, bool>();
		}

        #endregion

		#region Properties

		/// <summary>
		/// Hold the status (pressed or released) of all cursors present in the game.
		/// </summary>
		private Dictionary<int, bool> cursorStatus;
		
		/// <summary>
		/// Hold the status (pressed or released) of all cursors present in the game.
		/// </summary>
		public Dictionary<int, bool> CursorStatus
		{
			get { return cursorStatus; }
		}

		#endregion
		
        #region Events

        /// <summary>
        /// Dispatched when a fiducial is added to the table.
        /// </summary>
        public event EventHandler<ObjectAddedArgs> ObjectAdded;

        /// <summary>
        /// Dispatched when a fiducial over the table changes its properties.
        /// </summary>
        public event EventHandler<ObjectUpdatedArgs> ObjectUpdated;
        
        /// <summary>
        /// Dispatched when a fiducial is removed from the table.
        /// </summary>
        public event EventHandler<ObjectRemovedArgs> ObjectRemoved;

        /// <summary>
        /// Dispatched when a finger touches the table.
        /// </summary>
        public event EventHandler<CursorDownArgs> CursorDown;
        
        /// <summary>
        /// Dispatched when a finger touching the table leaves.
        /// </summary>
        public event EventHandler<CursorUpArgs> CursorUp;

        #endregion

        #region RaiseEvents

        /// <summary>
        /// Queue the AddObject event for dispatch.
        /// </summary>
        /// <param name="identifier">Id of the fiducial.</param>
        /// <param name="position">Center position of the fiducial.</param>
        /// <param name="orientation">Orientation in radians.</param>
        public void RaiseObjectAdded(int identifier, Vector2 position, float orientation)
        {
            ThreadPool.QueueUserWorkItem(AddObject, new ObjectAddedArgs(identifier, position, orientation));
        }

		/// <summary>
		/// Queue the UpdateObject event for dispatch.
		/// </summary>
		/// <param name="identifier">Id of the fiducial.</param>
		/// <param name="position">Center position of the fiducial.</param>
		/// <param name="orientation">Orientation in radians.</param>
        public void RaiseObjectUpdated(int identifier, Vector2 position, float orientation)
        {
            ThreadPool.QueueUserWorkItem(UpdateObject, new ObjectUpdatedArgs(identifier, position, orientation));
        }

		/// <summary>
		/// Queue the RemoveObject event for dispatch.
		/// </summary>
		/// <param name="identifier">Id of the fiducial.</param>
        public void RaiseObjectRemoved(int identifier)
        {
            ThreadPool.QueueUserWorkItem(RemoveObject, new ObjectRemovedArgs(identifier));
        }

		/// <summary>
		/// Queue the PressCursor event for dispatch.
		/// </summary>
		/// <param name="identifier">Id of the cursor created by a finger touch.</param>
		/// <param name="position">Center position of the finger.</param>
        public void RaiseCursorDown(int identifier, Vector2 position)
        {
            ThreadPool.QueueUserWorkItem(PressCursor, new CursorDownArgs(identifier, position));
            
			// Update the cursor status.
			this.cursorStatus[identifier] = true;
        }

		/// <summary>
		/// Queue the ReleaseCursor event for dispatch.
		/// </summary>
		/// <param name="identifier">Id of the cursor created by a finger touch.</param>
		/// <param name="position">Center position of the finger.</param>
        public void RaiseCursorUp(int identifier, Vector2 position)
        {
            if (this.cursorStatus.ContainsKey(identifier) && this.cursorStatus[identifier])
			{
                ThreadPool.QueueUserWorkItem(ReleaseCursor, new CursorUpArgs(identifier, position));

				// Removes the cursor status from collection.
				this.cursorStatus[identifier] = false;
            }
        }

        #endregion

        #region Event Dispatcher

        /// <summary>
        /// Dispatch the AddObject event.
        /// </summary>
        /// <param name="data">Event data.</param>
        private void AddObject(object data)
        {
            if (this.ObjectAdded != null)
                this.ObjectAdded(null, data as ObjectAddedArgs);
        }

		/// <summary>
		/// Dispatch the ObjectUpdated event.
		/// </summary>
		/// <param name="data">Event data.</param>
        private void UpdateObject(object data)
        {
            if (this.ObjectUpdated != null)
                this.ObjectUpdated(null, data as ObjectUpdatedArgs);
        }

		/// <summary>
		/// Dispatch the ObjectRemoved event.
		/// </summary>
		/// <param name="data">Event data.</param>
        private void RemoveObject(object data)
        {
            if (this.ObjectRemoved != null)
                this.ObjectRemoved(null, data as ObjectRemovedArgs);
        }

		/// <summary>
		/// Dispatch the CursorDown event.
		/// </summary>
		/// <param name="data">Event data.</param>
        private void PressCursor(object data)
        {
            if (this.CursorDown != null)
                this.CursorDown(null, data as CursorDownArgs);
        }

		/// <summary>
		/// Dispatch the ReleaseCursor event.
		/// </summary>
		/// <param name="data">Event data.</param>
        private void ReleaseCursor(object data)
        {
            if (this.CursorUp != null)
                this.CursorUp(null, data as CursorUpArgs);
        }

        #endregion
    }
}