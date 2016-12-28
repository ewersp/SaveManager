# SaveManager
A simple, yet powerful binary serializer for persisting game data in Unity, written in C#.

We used this exact serializer for Poi to manage all saved game data with multiple save profiles and complex data storage classes. 

We implemented the IGameSave interface for Xbox One and Wii U by creating XboxOneSaveManager.cs and WiiUSaveManager.cs, respectively (tested on dev kits for both). This allowed us to ignore Unity's PlayerPrefs API entirely, as it's not an ideal solution for saving data.

This solution also supports Steam Cloud without having to write a single additional line of code (via Steam Auto-Cloud).

Example Usage
------
```C#
// A sample data class to serialize
[Serializable]
public class SaveData {
	public int ID;
	public string Name;
	
	// Lists work too, for example, to make your own "PlayerPrefs"
	public List<KeyValuePair<string, int>> Flags = new List<KeyValuePair<string, int>>();
	
	// Etc... (safe to add new fields here, even if the file has already been serialized)
}

// It's a good idea to keep a reference to the manager somewhere in your project (ie: Singleton).
// For Poi, we had a small "GameSaveManager" Singleton wrapper which managed multiple save files.
StandaloneSaveManager manager = new StandaloneSaveManager();

// If the file doesn't exist, this will simply return a default SaveData instance
MySaveData data = manager.BinaryDeserialize<SaveData>("save.dat");

// Set some arbitrary data
data.Name = "Hello, World.";

// Write to disk
saveManager.BinarySerialize("save.dat", data);
```
