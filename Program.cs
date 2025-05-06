using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string filePath = "crashlogs.csv";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Crash log file not found.");
            return;
        }

        List<CrashLog> crashLogs = File.ReadAllLines(filePath)
            .Skip(1) // skip header
            .Select(line => line.Split(','))
            .Select(parts => new CrashLog
            {
                Timestamp = DateTime.ParseExact(parts[0], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                GameTitle = parts[1],
                Module = parts[2],
                ErrorCode = parts[3]
            }).ToList();

        // Group by module
        var crashCounts = crashLogs
            .GroupBy(log => log.Module)
            .Select(g => new { Module = g.Key, Count = g.Count() })
            .OrderByDescending(g => g.Count);

        Console.WriteLine("Crash Counts by Module:");
        foreach (var group in crashCounts)
        {
            Console.WriteLine($"{group.Module}: {group.Count} crashes");
        }

        // Detect anomalies (e.g. module with > 3 crashes)
        var anomalies = crashCounts.Where(x => x.Count > 3).ToList();

        Console.WriteLine("\n⚠️ Potential Crash Anomalies:");
        if (anomalies.Any())
        {
            foreach (var item in anomalies)
            {
                Console.WriteLine($"- {item.Module}: {item.Count} crashes");
            }
        }
        else
        {
            Console.WriteLine("No anomalies detected.");
        }

        // Write report to CSV
        string reportPath = "crash_report.csv";
        File.WriteAllLines(reportPath, new[] { "Module,CrashCount" }
            .Concat(crashCounts.Select(x => $"{x.Module},{x.Count}")));

        Console.WriteLine($"\nReport written to: {reportPath}");
    }
}
