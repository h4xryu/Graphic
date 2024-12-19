# Graphic Application

## Overview
This application is a graphical tool developed in C# using libraries like OpenTK, ZedGraph, and System.Windows.Forms. It provides real-time visualization of 3D models, dynamic graph plotting, and serial communication data handling.

***
![nn](https://ifh.cc/g/GlRyHj.jpg)

## Features

### 1. 3D Rendering
- Utilizes OpenTK for high-quality 3D model rendering.
- Supports loading `.obj` files (e.g., Aircraft models).
- Interactive controls for rotation, zooming, and panning using the mouse.

### 2. Real-Time Graph Plotting
- Uses ZedGraph to plot dynamic data for pitch, roll, and yaw angles.
- Automatically updates graphs based on serial input.
- Offers customizable appearance and axis scaling.

### 3. Serial Communication
- Interfaces with serial ports to receive and process real-time data.
- Configurable settings for COM ports and baud rates.
- Handles incoming data to control visuals and update graphs dynamically.

### 4. User-Friendly Interface
- Built with Windows Forms for intuitive GUI interaction.
- Provides buttons for connecting/disconnecting serial ports.
- Displays live data in a debug panel for easy monitoring.

---

## Getting Started

### Prerequisites
- Windows operating system.
- .NET Framework.
- Required libraries:
  - OpenTK
  - ZedGraph
  - System.Windows.Forms

### Installation
1. Clone the repository:
   ```bash
   git clone <repository-url>
