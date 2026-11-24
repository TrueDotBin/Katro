using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Network
{
    /// <summary>
    /// The result of a ping.
    /// </summary>
    public class PingResult(int time)
    {
        /// <summary>
        /// The amount of milliseconds it took for the server to respond.
        /// </summary>
        public int Time { get; } = time;
    }
}
