# AITree

## Overview
This project is a riff on the classic min-max AI challenge you were probably given in CompSci 101: Simulating the lifecycle of a tree in a 2D grid. This project scales that up into the 3D, and lays the groundwork for extension by a variety of tree growth engines.  

## Relationship to the Display Project
AITree serves as the data and behavior counterpart to the Unity-based [AITreeDisplay](https://github.com/DrJaul/AITreeDisplay) project. The two repositories are designed to be used together, with this project providing the tree state and growth mechanics that the display project visualizes in real time.

## Architectural Approach
The codebase follows a Model-View-Controller-style separation of concerns. The logic here acts as both the **model** and the **controller**:

- **Model**: Encapsulates the data structures representing the tree, its growth rules, and the supporting grid.
- **Controller**: Provides the functions that query and mutate that state, enabling consumers to drive the growth simulation.

The Unity project supplies the **view**, handling the rendering pipeline, visual effects, and user-facing visualization loop. Together, the two projects create a modular system where the simulation logic and presentation layer can evolve independently.
