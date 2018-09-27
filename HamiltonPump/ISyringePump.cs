using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyringePump
{
    /// <summary>
    /// Things that every syringe pump needs to do
    /// </summary>
    public interface ISyringePump
    {
        /// <summary>
        /// Execute the command buffer starting with the first unexecuted command in the buffer
        /// </summary>
        void ExecuteCommandBuffer();

        /// <summary>
        /// Initializes the syringe pump and its valve to home position
        /// </summary>
        /// <param name="outputToLeft">
        /// If true, initialize valve with left-hand port as output,
        /// otherwise initialize valve with right-hand port as output
        /// </param>
        void InitializeSyringeAndValve(bool outputToLeft = true);

        /// <summary>
        /// Move the syringe to an absolute position
        /// </summary>
        /// <param name="steps">Step position to move to (0 = home)</param>
        void AbsolutePosition(int steps);

        /// <summary>
        /// Move the syringe down a given number of steps (aspirate)
        /// </summary>
        /// <param name="steps">Number of steps to move down</param>
        void RelativePickup(int steps);

        /// <summary>
        /// Move the syringe up a given number of steps (dispense)
        /// </summary>
        /// <param name="steps">Number of steps to move up</param>
        void RelativeDispense(int steps);

        /// <summary>
        /// Move the valve to the specified position
        /// </summary>
        /// <param name="valvePosition">Desired valve position</param>
        void MoveValve(int valvePosition);

        /// <summary>
        /// Set the pump acceleration for syringe moves in steps/sec^2
        /// </summary>
        /// <param name="acceleration">Acceleration in steps/sec^2</param>
        void SetAcceleration(int acceleration);

        /// <summary>
        /// Set the pump deceleration for syringe moves in steps/sec^2
        /// </summary>
        /// <note>
        /// May not be programmable separately on Hamilton PSD/4
        /// </note>
        /// <param name="deceleration">Acceleration in steps/sec^2</param>
        void SetDeceleration(int deceleration);

        /// <summary>
        /// Set the starting velocity for syringe moves to the specified number of steps/second
        /// </summary>
        /// <param name="stepsPerSec">Starting velocity in steps/sec</param>
        void SetStartVelocity(int stepsPerSec);

        /// <summary>
        /// Set the maximum velocity for syringe to the specified number of steps/second
        /// </summary>
        /// <param name="stepsPerSec">Maximum velocity in steps/sec</param>
        void SetSpeed(int stepsPerSec);

        /// <summary>
        /// Sets the stop velocity for syringe moves to the specified number of steps/second
        /// </summary>
        /// <param name="stepsPerSec">Stop velocity in steps/sec</param>
        void SetStopVelocity(int stepsPerSec);

        /// <summary>
        /// Set the pump backlash correction in steps
        /// </summary>
        /// <param name="steps">Backlash correction in steps</param>
        void SetBacklash(int steps);

        /// <summary>
        /// Stops execution of command buffer
        /// <note>
        /// May cause loss of syringe position.  Pump should be reinitialized.
        /// </note>
        /// </summary>
        void TerminateCommandBuffer();

        /// <summary>
        /// Query the pump status to see if the pump is busy and return the status byte
        /// </summary>
        /// <param name="status">Status byte returned from pump</param>
        /// <returns>true if pump is busy, false otherwise</returns>
        bool QueryStatus(out char status);

        /// <summary>
        /// Perform a delay of the specified number of milliseconds
        /// TODO:  Is this needed for pumps?
        /// </summary>
        /// <param name="msec">Milliseconds to delay</param>
        void Delay(int msec);

        /// <summary>
        /// Halts execution until the specified digital input(s) change state
        /// </summary>
        /// TODO:  How do we trigger pump to stop moving when signal seen at T-cell?
        /// <param name="input">Which digital input(s) to look for</param>
        void HaltCommandExecution(int digitalInput);

    }
}
