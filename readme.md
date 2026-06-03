# MyFoodTracker - Food & Drink Nutrition Tracker

A cross-platform mobile app built with .NET MAUI for the "Food and Drink" theme.

## Features

- Browse food/drink list with search by name, category or tag
- Add new food items with name, category, nutrition info, and allergy notes
- View detailed nutrition information with food image
- Delete items with confirmation dialog

## Mobile Hardware Utilized

- **Camera**: Take photos (can be extended for food recognition)
- **Location & Geocoding**: Get current address (country/city/region)
- **Text-to-Speech**: Read nutrition summary and help aloud
- **Vibration & Haptic Feedback**: Provide tactile feedback on errors or actions

## Accessibility

- Dark/Light theme switch
- Large text mode (global font scaling)
- Screen reader support via AutomationProperties
- User-friendly error messages

## Code Quality

- .NET analyzers enabled (`EnableNETAnalyzers`, `AnalysisMode=All`)
- Follows C# coding conventions and MAUI best practices
- Dependency injection for service layer

## Deployment

- Supports Android and Windows
- Tested on Android phone emulator and Android tablet emulator

## How to Run

1. Open solution in Visual Studio 2022 (17.9+).
2. Set target framework to Android or Windows.
3. Start an emulator or connect a device.
4. Press F5 to build and run.

## GitHub Usage

Regular commits with meaningful messages. See commit history.