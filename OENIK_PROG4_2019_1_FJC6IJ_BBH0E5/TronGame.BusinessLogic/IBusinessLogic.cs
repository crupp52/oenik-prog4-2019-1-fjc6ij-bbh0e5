using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TronGame.BusinessLogic
{
    interface IBusinessLogic
    {
        /// <summary>
        /// Resets all variables to thier defaul values.
        /// </summary>
        void ResetToDefaultValues();

        /// <summary>
        /// Resets variables which are neccessery after round win.
        /// </summary>
        void ResetAfterRoundWin();

        /// <summary>
        /// Crates a notification message.
        /// </summary>
        void CreateNotification(int type, string message);

        /// <summary>
        /// Starts the timer.
        /// </summary>
        void StartTimer();

        /// <summary>
        /// Pauses the timer.
        /// </summary>
        void PauseTimer();

        /// <summary>
        /// Resets the timer.
        /// </summary>
        void ResetTimer();

        /// <summary>
        /// Moves the player into a direction.
        /// </summary>
        void MovePlayer(); // TODO: parameters??

        /// <summary>
        /// A player picks up an item.
        /// </summary>
        /// <param name="itemType"></param>
        void PickUp(int itemType);

        /// <summary>
        /// A player dies.
        /// </summary>
        void Die();
    }
}
