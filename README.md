# SaveManager
A simple, yet powerful binary serializer for persisting game data in Unity, written in C#.

We used this exact serializer for Poi to manage all saved game data with multiple save profiles and complex data storage classes. 

We implemented the IGameSave interface for Xbox One and Wii U by creating XboxOneSaveManager.cs and WiiUSaveManager.cs, respectively (tested on dev kits for both). This allowed us to ignore Unity's PlayerPrefs API entirely, as it's not an ideal solution for saving data.
