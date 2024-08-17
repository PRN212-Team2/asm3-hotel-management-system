# HMS (Hotel Management System)

## Overview

HMS (Hotel Management System) is a WPF application designed for hotel room bookings and management. The system manages customer information, room assignments, and booking reservations. The user interface is crafted using the Material Design theme package, providing a modern and intuitive experience for users.

## Features

- **Room Booking:** Easily manage and book hotel rooms.
- **Customer Management:** Maintain and organize customer information.
- **Room Management:** Customize room information.
- **Reservation Management:** Handle and monitor reservations with ease.
- **Modern UI:** Built with the Material Design theme for a sleek, user-friendly interface.

## Improvements in This Version

This version of HMS is an improved iteration of a previous project. Key enhancements include:

- **Transition from ODBC to ORM:** The old repository used ODBC for database connectivity, whereas this version implements Entity Framework Core (EF Core) for more efficient and scalable data management.
  
- **Codebase Optimization:** Refined and optimized code for better performance and maintainability.

- **Enhanced UI:** Updated user interface with improved responsiveness and a more consistent design language.

## Previous Version

If you're interested in the original version of this project, you can find it in the following repository:  
[Old HMS Repository](https://github.com/PRN212-Team2/asm2-hotel-management-system)

## Getting Started

### Prerequisites

- **.NET Framework** (8.x.x)
- **SQL Server** (>= 2019)
- **Visual Studio**

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/hms
   ```
2. Open the solution in Visual Studio.
3. Restore NuGet packages.
4. Update the database connection string in `appsettings.json` or the configuration file.
5. Run the application.
