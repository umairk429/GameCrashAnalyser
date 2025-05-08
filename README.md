# 🎮 Game Crash Analyzer (C# Console App)

This C# console application processes crash logs from video games and provides:
- 📊 Crash frequency reports by module
- ⚠️ Anomaly detection for modules with high crash rates
- 📁 Export to CSV for further analysis

## 🚀 Features
- Read CSV crash logs
- Parse and group crash data
- Flag crash-prone modules
- Write summary report to a new CSV

## 📦 Sample Input
```csv
Timestamp,GameTitle,Module,ErrorCode
2025-05-01 14:22:00,SuperShooter,Renderer,ERR123
...
