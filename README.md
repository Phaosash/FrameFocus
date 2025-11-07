# Tenpin Bowling Score & Statistics Tracker

A **cross-platform Tenpin Bowling score and statistics tracking app** built with **ASP.NET MAUI Blazor Hybrid** and **C# class libraries**.  
Track your performance over time, visualize your averages, and analyze your strike, spare, and open frame percentages — all from one intuitive interface.

---

## Overview

This project combines the flexibility of **Blazor Web** and the power of **.NET MAUI Hybrid** to deliver both web and desktop/mobile experiences.  
Users can log their game scores and view detailed analytics through interactive charts and dashboards.

---

## Features

- **Comprehensive Score Tracking**
  - Record per-frame and per-game results
  - Auto-calculates totals, averages, and frame outcomes

- **Statistical Insights**
  - Calculates:
    - Average score over time
    - Strike percentage
    - Spare percentage
    - Open frame percentage

- **Data Visualization**
  - Line chart displaying **average score trends**
  - Bar or radar chart comparing **strike%, spare%, open frame%**

-  **Persistent Data Storage**
  - Local storage via SQLite or file-based data
  - Extensible for cloud sync in future updates

-  **Cross-Platform**
  - Runs as a **web app** or **native hybrid app** via .NET MAUI

---

## Project Structure

```

TenpinBowlingTracker/
│
├── TenpinBowlingTracker.App/           # ASP.NET MAUI Blazor Hybrid front-end
├── TenpinBowlingTracker.Web/           # Blazor Web front-end
├── TenpinBowlingTracker.Core/          # Shared C# class library (business logic, models)
├── TenpinBowlingTracker.Data/          # Data access layer (repositories, services)
├── TenpinBowlingTracker.Tests/         # Unit tests
└── README.md

````

---

## Technologies Used

- **.NET 8 / C# 12**
- **ASP.NET Core Blazor Hybrid (MAUI)**
- **Blazor WebAssembly**
- **Entity Framework Core / SQLite**
- **Chart.js or Blazorise.Charts** for visualization
- **xUnit / MSTest** for testing

---

## Getting Started

### Prerequisites

- [Visual Studio 2022 (17.8+)](https://visualstudio.microsoft.com/)
- **.NET SDK 8.0+**
- **.NET MAUI workloads** installed:
  ```bash
  dotnet workload install maui
````
## Planned Roadmap

* [ ] Add user authentication and profiles
* [ ] Data persistence through database and API
* [ ] Support for multiple bowlers
* [ ] Dark / Light mode theme toggle


## Contributing

Contributions, feedback, and feature requests are welcome!

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/your-feature`)
3. Commit your changes (`git commit -m "Add your feature"`)
4. Push the branch (`git push origin feature/your-feature`)
5. Open a Pull Request

---

## License

This project is licensed under the [MIT License](LICENSE).

---
