using Katro.Constants.Shell;
using Katro.Enums;
using Cosmos.Kernel.System.Diagnostics;
using Katro.FileSystem;

namespace Katro.Shell.Commands.System
{
    /// <summary>
    /// Displays information about the system.
    /// </summary>
    public sealed class SysInfo : Command
    {
        /// <inheritdoc/>
        public override string Name { get; } = "sysinfo";

        /// <inheritdoc/>
        public override string Description { get; } = "Displays information about the system.";

        /// <inheritdoc/>
        public override string Usage { get; } = "sysinfo";

        /// <inheritdoc/>
        public override CommandCategory Category { get; } = CommandCategory.System;

        /// <inheritdoc/>
        public override int Execute(CommandContext ctx)
        {
            var usableMemoryFormatted = FileSizeFormatter.FormatSize(MemoryDiagnostics.RamSizeBytes);

            Terminal.Heading("Memory Info");
            Terminal.InfoLine("Usable Memory", usableMemoryFormatted);
            Terminal.InfoLine("Page Size", $"{MemoryDiagnostics.PageSizeBytes} bytes");
            Terminal.InfoLine("Total Pages", MemoryDiagnostics.TotalPages);
            Terminal.InfoLine("Free Pages", MemoryDiagnostics.FreePages);
            Terminal.InfoLine("Total Collections", MemoryDiagnostics.TotalCollections);
            Terminal.InfoLine("Total Objects Freed", MemoryDiagnostics.TotalObjectsFreed);
            Console.WriteLine();

            Terminal.Heading("Driver Info");
            Terminal.InfoLine("Started?", DriverDiagnostics.IsStarted ? "Yes" : "No");
            Terminal.InfoLine("Has Worker?", DriverDiagnostics.HasWorker ? "Yes" : "No");
            Terminal.InfoLine("Driver Count", DriverDiagnostics.DriverCount);
            Terminal.InfoLine("Node Count", DriverDiagnostics.NodeCount);
            Terminal.InfoLine("Device Count", DriverDiagnostics.DeviceCount);
            Console.WriteLine();

            if (SchedulerDiagnostics.IsInitialized)
            {
                Terminal.Heading("Scheduler Info");
                Terminal.InfoLine("Initialized?", SchedulerDiagnostics.IsInitialized ? "Yes" : "No");
                Terminal.InfoLine("Running?", SchedulerDiagnostics.IsRunning ? "Yes" : "No");
                Terminal.InfoLine("Name", SchedulerDiagnostics.SchedulerName ?? "None");
                Terminal.InfoLine("CPU Count", SchedulerDiagnostics.CpuCount);
                Terminal.InfoLine("Tick Period", $"{SchedulerDiagnostics.TickPeriodNs} nanosecond(s)");
                Terminal.InfoLine("Busy CPU Time", $"{SchedulerDiagnostics.BusyCpuTimeNs} nanosecond(s)");
                Terminal.InfoLine("Thread Count", SchedulerDiagnostics.ThreadCount);
                Terminal.InfoLine("Thread Slot Count", SchedulerDiagnostics.ThreadSlotCount);
            }

            return CommandReturnCode.Success;
        }
    }
}
