using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	/// <summary>
	/// An interface for holding metadata items so that they can be easily retrieved and mapped to other data
	/// </summary>
	public interface IMetadataMap
	{
		/// <summary>
		/// Get a denormalized dictionary of all metadata items
		/// </summary>
		/// <returns>A dictionary of string/string name-value pairs</returns>
		IDictionary<string, string> GetItems();				
		
		/// <summary>
		/// Get a generic dictionary of all metadata items of the given type
		/// </summary>
		/// <typeparam name="T">The type of metadata items to get</typeparam>
		/// <returns>A dictionary of string/MetadataItem name-value pairs</returns>
		IDictionary<string, MetadataItem<T>> GetItems<T>();

		/// <summary>
		/// Get a denormalized dictionary of all metadata items that belong to the given root
		/// </summary>
		/// <param name="root">The root that the metadata items belong to</param>
		/// <returns>A dictionary of string/string name-value pairs</returns>
		IDictionary<string, string> GetSubItems(string root);
		
		/// <summary>
		/// Get a generic dictionary of all metadata items of the given type that belong to the given root
		/// </summary>
		/// <typeparam name="T">The type of metadata items to get</typeparam>
		/// <param name="root">The root that the metadata items belong to</param>
		/// <returns>A dictionary of string/MetadataItem name-value pairs</returns>
		IDictionary<string, MetadataItem<T>> GetSubItems<T>(string root);
		
		/// <summary>
		/// Add a metadata item of the given type
		/// </summary>
		/// <typeparam name="T">The type of the metadata item being added</typeparam>
		/// <param name="item">The item to add</param>
		void AddItem<T>(MetadataItem<T> item);
		
		/// <summary>
		/// Add a root and child metadata items of the given type
		/// </summary>
		/// <typeparam name="T">The type of the root to add</typeparam>
		/// <param name="root">The root</param>
		/// <param name="data">A list of the data that will be loaded into metadata items of the given type</param>
		void AddRoot<T>(string root, IList<T> data);
		
		/// <summary>
		/// Determine whether or not a given root exists in this map
		/// </summary>
		/// <param name="root">The root</param>
		/// <returns>true if the root exists, false otherwise</returns>
		bool RootExists(string root);
		
		/// <summary>
		/// Get the type of the metadata item(s) with the given root
		/// </summary>
		/// <param name="root">The root</param>
		/// <returns>The type or null if there are no items with that root</returns>
		Type GetItemType(string root);
		
		/// <summary>
		/// Get the metadata item with the given type and name
		/// </summary>
		/// <typeparam name="T">The type of the metadata</typeparam>
		/// <param name="name">The name of the metadata item</param>
		/// <returns></returns>
		MetadataItem<T> GetItem<T>(string name);
		
		/// <summary>
		/// Get a list of the metadata items with the given type and root
		/// </summary>
		/// <typeparam name="T">The </typeparam>
		/// <param name="root"></param>
		/// <returns></returns>
		IList<MetadataItem<T>> GetRoot<T>(string root);
		
		/// <summary>
		/// Remove all items with the given root
		/// </summary>
		/// <param name="root">The root</param>
		void RemoveRoot(string root);
	}
}
