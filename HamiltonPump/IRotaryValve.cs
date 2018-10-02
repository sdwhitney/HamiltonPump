using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyringePump
{
    /// <summary>
    /// Things that every rotary valve needs to do
    /// </summary>
    public interface IRotaryValve
    {
        /// <summary>
        /// Moves the rotary valve to its home position
        /// </summary>
        void MoveToHome();

        /// <summary>
        /// Queries the status of the rotary valve
        /// </summary>
        /// <returns>Rotary valve status - position or error code</returns>
        string QueryStatus();

        /// <summary>
        /// Move the rotary valve to the specified position
        /// </summary>
        /// <param name="position">Position to move rotary valve to</param>
        void MoveToPosition(int position);
    }
}
