using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyringePump
{
    public interface ISyringePumpHAL
    {
        /// <summary>
        /// Configures all pumps in system
        /// </summary>
        /// <param name="pumpConfigs">Array of pump configuration structures</param>
        void ConfigureSyringeMotion(ref PumpConfig[] pumpConfigs);

        /// <summary>
        /// Aspirate fluid volume
        /// </summary>
        /// <param name="addr">Pump address</param>
        /// <param name="uL">Number of microliters to aspirate</param>
        /// <param name="executeNow">true to execute immediately, false otherwise</param>
        void AspirateVolume(PumpAddress addr, double uL, bool executeNow = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addr">Pump address</param>
        /// <param name="uL">Number of microliters to dispense</param>
        /// <param name="executeNow">true to execute immediately, false otherwise</param>
        void DispenseVolume(PumpAddress addr, double uL, bool executeNow = false);

        /// <summary>
        /// Return pump to home position (0 steps)
        /// </summary>
        /// <param name="addr">Pump address</param>
        /// <param name="executeNow">true to execute immediately, false otherwise</param>
        void Home(PumpAddress addr, bool executeNow = false);

        /// <summary>
        /// Merge sample with assay by doing a series of small dispenses
        /// </summary>
        /// <param name="addr">Pump address</param>
        /// <param name="dispenseStepsPerIteration">Steps to dispense each iteration</param>
        /// <param name="repeatLoopCount">Number of iterations to perform</param>
        /// <param name="waitAfterDispenseMsec">Number of milliseconds to wait after each dispense iteration</param>
        void Merge(PumpAddress addr, int dispenseStepsPerIteration, int repeatLoopCount, int waitAfterDispenseMsec);

        /// <summary>
        /// Move pump valve to its initialization position
        /// </summary>
        /// <param name="addr">Pump address</param>
        /// <param name="executeNow">true to execute immediately, false otherwise</param>
        void MoveToInitialPort(PumpAddress addr, bool executeNow = false);

        /// <summary>
        /// Do nothing.  Pump performs no action while other pumps may run
        /// </summary>
        /// <param name="addr">Pump address</param>
        void Null(PumpAddress addr);

        /// <summary>
        /// Sets the initial valve position on reset
        /// </summary>
        /// <note>
        /// May not be possible for non-Kloehn pumps
        /// </note>
        /// <param name="addr">Pump address</param>
        /// <param name="valvePosition">Valve position, 1:3</param>
        void SetInitialPort(PumpAddress addr, int valvePosition);

        /// <summary>
        /// Set the pump speed in steps per second
        /// </summary>
        /// <note>
        /// Raw steps/sec used in current scripts - OK?
        /// </note>
        /// <param name="addr"></param>
        /// <param name="stepsPerSec">Pump speed in steps/sec</param>
        void SetSpeed(PumpAddress addr, int stepsPerSec);

        /// <summary>
        /// Switch to the specified pump valve port
        /// </summary>
        /// <param name="addr">Pump address</param>
        /// <param name="valvePosition">Valve position, 1:3</param>
        /// <param name="executeNow">true to execute immediately, false otherwise</param>
        void SwitchToPort(PumpAddress addr, int valvePosition, bool executeNow = false);

        /// <summary>
        /// Execute the command(s) stored in the pump command buffer(s)
        /// </summary>
        /// <param name="addr">Pump address</param>
        void Execute(PumpAddress addr);
    }
}
