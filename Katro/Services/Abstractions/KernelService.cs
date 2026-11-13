using System;
using System.Collections.Generic;
using System.Text;

namespace Katro.Services.Abstractions
{
    /// <summary>
    /// A service that belongs to the kernel, started with <see cref="ServiceHost"/>.
    /// </summary>
    public abstract class KernelService
    {
        /// <summary>
        /// The user-friendly name of this service.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The identifier of this service.
        /// </summary>
        public abstract string Id { get; }

        /// <summary>
        /// The list of other service IDs that need to be started before this one can start.
        /// </summary>
        public virtual string[] DependsOn { get; } = [];

        /// <summary>
        /// Starts this service.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Cleans up certain stuff.
        /// </summary>
        public virtual void Stop() { }
    }
}
