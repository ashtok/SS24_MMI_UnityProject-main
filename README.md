# Multimodal Point of Interest (POI) Interaction System

This project implements a multimodal Point of Interest (POI) interaction system based on the paper titled "Latent Semantic Analysis for Multimodal User Input With Speech and Gestures" by Pui-Yu Hui and Helen Meng. The system allows users to interact with POIs on a map using both speech commands and touch gestures.

## Project Overview

The Multimodal POI Interaction System is built in Unity and consists of several key components:
1. **POIManager**: Manages the POIs including places to eat, parks, and shops. Handles displaying and hiding of POIs on the map.
2. **MapTouchHandler**: Manages touch interactions on the map. Allows users to draw circles on the map and select POIs.
3. **FusionMethod**: Integrates speech recognition and touch interactions to process user commands and perform appropriate actions.

### Key Features

- **Speech Commands**: Users can interact with the system using voice commands to display, hide, and query information about POIs.
- **Touch Gestures**: Users can interact with the map using touch gestures to draw areas and select POIs.
- **Multimodal Interaction**: Combines speech and touch inputs for a more intuitive user experience.

## Speech Commands

The system recognizes the following speech commands:
- "Display places to eat" - Shows all POIs categorized as places to eat.
- "Hide places to eat" - Hides all POIs categorized as places to eat.
- "Display parks" - Shows all POIs categorized as parks.
- "Hide parks" - Hides all POIs categorized as parks.
- "Display shops" - Shows all POIs categorized as shops.
- "Hide shops" - Hides all POIs categorized as shops.
- "What is this" - Identifies and names the POI at the location of the last touch.
- "Display in area" - Displays POIs within a drawn circle area.
- "What is the distance between these points" - Calculates the distance between the last two touched points.

## Gestures

The system recognizes the following touch gestures:
- **Single Tap**: Selects a POI on the map and provides its name.
- **Circle Drawing**: Allows users to draw a circle on the map to select an area. All POIs within this circle will be displayed. **(Contains some intermitent bugs)**

## How to Use

1. **Initialization**: Ensure that all necessary components (POIManager, MapTouchHandler, and FusionMethod) are correctly linked in the Unity Editor.
2. **Starting the Application**: Run the application in the Unity Editor or build it for your target platform.
3. **Interacting with POIs**: Use the speech commands and touch gestures to interact with the POIs on the map as described above.

## Dependencies

- **Unity**: This project is built using Unity. Ensure you have the latest version of Unity installed.
- **SpeechRecognitionSystem**: A speech recognition system must be integrated with the project to process voice commands.

## Future Enhancements

- **Extended POI Categories**: Add more categories and customize POI properties.
- **Advanced Gesture Recognition**: Incorporate more complex gesture recognition for enhanced interaction.
- **Improved Speech Recognition**: Enhance the speech recognition system for better accuracy and more commands.

## Authors

- This project is based on the research paper "Latent Semantic Analysis for Multimodal User Input With Speech and Gestures" by Pui-Yu Hui and Helen Meng.

- Developed by Ashutosh Mahajan
- University of Würzburg
- **Supervisors:**
- Dr. Martin Fischbach
- Chris Zimmerer
- Ronja Heinrich



