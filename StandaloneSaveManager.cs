using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// The common persisted data (save/load) interface for various platforms.
/// </summary>
public interface IGameSave {
	T BinaryDeserialize<T>(string filename) where T : new();
	void BinarySerialize(string filename, object data);
	void Delete(string filename);
	bool FileExists(string filename);
	string GetFullPath(string filename);
}

/// <summary>
/// The game save manager for all standalone platforms (Windows, Mac and Linux). Capable of 
/// binary serialization and deserialization of generic data objects.
///
/// This manager supports adding new data to your [Serializable] classes, even if you've already
/// saved it to disk. For example, adding new persisted data to a save file post-launch.
/// 
/// Example Usage:
///		[Serializable]
///		public class SaveData {
///			public int ID;
///			public string Name;
///			// Etc... (safe to add new fields here, even if the file has already been serialized)
///		}
///
///		StandaloneSaveManager manager = new StandaloneSaveManager();
///		MySaveData data = manager.BinaryDeserialize<SaveData>("save.dat");
///		data.Name = "Hello, World.";
///		saveManager.BinarySerialize("save.dat", data);
/// </summary>
public class StandaloneSaveManager : MonoBehaviour, IGameSave {

	/// <summary>
	/// Binary serialize any generic data object.
	/// </summary>
	/// <param name="filename">The filename to save this data to.</param>
	/// <param name="data">The data to serialize.</param>
	public void BinarySerialize(string filename, object data) {
		FileStream file = File.Open(GetFullPath(filename), FileMode.OpenOrCreate);
		BinaryFormatter bf = new BinaryFormatter();

		try {
			bf.Serialize(file, data);
		} catch (SerializationException e) {
			Debug.Log("BinarySerialize failed: " + e.Message);
		} finally {
			file.Close();
		}
	}

	/// <summary>
	/// Binary deserialize any generic data object.
	/// </summary>
	/// <typeparam name="T">The class type of the deserialized data.</typeparam>
	/// <param name="filename">The filename of the data to deserialize.</param>
	/// <returns>The deserialized data if it existed, otherwise a default object.</returns>
	public T BinaryDeserialize<T>(string filename) where T : new() {
		T result = new T();

		// If the file doesn't exist, return a default object
		if (!FileExists(filename)) {
			return result;
		}

		FileStream file = File.Open(GetFullPath(filename), FileMode.Open);
		BinaryFormatter bf = new BinaryFormatter();

		try {
			result = (T)bf.Deserialize(file);
		} catch (SerializationException e) {
			Debug.Log("BinaryDeserialize failed: " + e.Message);
		} finally {
			file.Close();
		}
		return result;
	}

	/// <summary>
	/// Checks if a given file exists.
	/// </summary>
	/// <param name="filename">The name of the file to check.</param>
	/// <returns>True if the file exists, false otherwise.</returns>
	public bool FileExists(string filename) {
		return File.Exists(GetFullPath(filename));
	}

	/// <summary>
	/// Delete a given file.
	/// </summary>
	/// <param name="filename">The name of the file to delete.</param>
	public void Delete(string filename) {
		File.Delete(GetFullPath(filename));
	}

	/// <summary>
	/// Gets the full save path, including the filename.
	/// </summary>
	/// <param name="filename"></param>
	/// <returns>The full save path.</returns>
	public string GetFullPath(string filename) {
		return Path.Combine(Application.persistentDataPath, filename);
	}
}
