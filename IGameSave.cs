/// <summary>
/// The common persisted data (save/load) interface for various platforms.
/// </summary>
public interface IGameSave {

	/// <summary>
	/// Binary serialize any generic data object.
	/// </summary>
	/// <param name="filename">The filename to save this data to.</param>
	/// <param name="data">The data to serialize.</param>
	void BinarySerialize(string filename, object data);
	
	/// <summary>
	/// Binary deserialize any generic data object.
	/// </summary>
	/// <typeparam name="T">The class type of the deserialized data.</typeparam>
	/// <param name="filename">The filename of the data to deserialize.</param>
	/// <returns>The deserialized data if it existed, otherwise a default object.</returns>
	T BinaryDeserialize<T>(string filename) where T : new();
		
	/// <summary>
	/// Delete a given file.
	/// </summary>
	/// <param name="filename">The name of the file to delete.</param>
	void Delete(string filename);
	
	/// <summary>
	/// Checks if a given file exists.
	/// </summary>
	/// <param name="filename">The name of the file to check.</param>
	/// <returns>True if the file exists, false otherwise.</returns>
	bool FileExists(string filename);
	
	/// <summary>
	/// Gets the full save path, including the filename.
	/// </summary>
	/// <param name="filename"></param>
	/// <returns>The full save path.</returns>
	string GetFullPath(string filename);
}
