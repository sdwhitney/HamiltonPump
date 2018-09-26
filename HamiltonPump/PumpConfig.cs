using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyringePump
{
    public struct PumpConfig
    {
        /// <summary>
        /// Total syringe volume in microliters
        /// </summary>
        public int totalSyringeVolumeMicroliters;

        /// <summary>
        /// Total motor steps per full pump stroke
        /// </summary>
        public int totalStepsFullStroke;

        /// <summary>
        /// Top speed in steps per second
        /// </summary>
        public int topSpeedStepsPerSec;

        /// <summary>
        /// Starting speed in steps per second
        /// </summary>
        public int startSpeedStepsPerSec;

        /// <summary>
        /// Stopping speed in steps per second
        /// </summary>
        public int stopSpeedStepsPerSec;

        /// <summary>
        /// Acceleration in steps per second^2
        /// </summary>
        public int accelerationStepsPerSecSquared;

        /// <summary>
        /// Deceleration in steps per second^2
        /// </summary>
        public int decelerationStepsPerSecSquared;

        /// <summary>
        /// Backlash in steps
        /// </summary>
        public int backlashSteps;
    }
}
