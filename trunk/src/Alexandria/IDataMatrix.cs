using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	/// <summary>
	/// An interface for holding data items so that they can be easily retrieved and mapped to other data structures
	/// </summary>
	public interface IDataMatrix
	{		
		/// <summary>
		/// Get a denormalized dictionary of the names and values of all data nodes
		/// </summary>
		/// <returns>A dictionary of name-value pairs</returns>
		IDictionary<string, string> GetNameValuePairs();

		/// <summary>
		/// Get a denormalized dictionary of the names and values of all child data nodes with a parent with the given name
		/// </summary>
		/// <param name="name">The name of the parent node</param>
		/// <returns>A dictionary of name-value pairs</returns>
		IDictionary<string, string> GetNameValuePairs(string name);
		
		/// <summary>
		/// Get a list of all data nodes of the given type
		/// </summary>
		/// <typeparam name="T">The type of data nodes to get</typeparam>
		/// <returns>A list of data nodes or null if there are no nodes of this type in this map</returns>
		IList<DataNode<T>> GetNodes<T>();

		
		/// <summary>
		/// Get a list of all data nodes of the given type with a parent with the given name
		/// </summary>
		/// <typeparam name="T">The type of data nodes to get</typeparam>
		/// <param name="name">The name of the parent node</param>
		/// <returns>A list of data nodes or null if there is no parent with the given name and type in this map</returns>
		IList<DataNode<T>> GetNodes<T>(string name);
		
		/// <summary>
		/// Add a data node of the given type
		/// </summary>
		/// <typeparam name="T">The type of the data node being added</typeparam>
		/// <param name="item">The node to add</param>
		void AddNode<T>(DataNode<T> node);
				
		/// <summary>
		/// Determine whether or not this matrix contains a data node with the given name
		/// </summary>
		/// <param name="name">The name of the data node to search for</param>
		/// <returns>true if the node exists, false otherwise</returns>
		bool Contains(string name);
		
		/// <summary>
		/// Determine whether or not this matrix contains a data node with the given number and a parent with the given name
		/// </summary>
		/// <param name="name">The name of the parent data node</param>
		/// <param name="number">The number of the child data node to search for</param>
		/// <returns>true if the child node exists, false otherwise</returns>
		bool Contains(string name, int number);
		
		/// <summary>
		/// Get the type of the data node with the given name
		/// </summary>
		/// <param name="name">The name of the node to get the type for</param>
		/// <returns>The type or null if no node with the given name exists in this map</returns>
		Type GetNodeType(string name);
		
		/// <summary>
		/// Get the data node with the given type and name
		/// </summary>
		/// <typeparam name="T">The type of the data node</typeparam>
		/// <param name="name">The name of the data node</param>
		/// <returns>The node with the given type and name</returns>
		DataNode<T> GetNode<T>(string name);
		
		/// <summary>
		/// Get the child data node with the given number and type that belongs to a parent of the given name
		/// </summary>
		/// <typeparam name="T">The type of the child date node</typeparam>
		/// <param name="name">The name of the child data node's parent</param>
		/// <param name="number">The number of the child data node</param>
		/// <returns>A child data node</returns>
		DataNode<T> GetNode<T>(string name, int number);
		
		/// <summary>
		/// Remove the data node with the given name, if it has children they are removed as well
		/// </summary>
		/// <param name="name">The name of the data node to remove</param>
		void RemoveNode(string name);
		
		/// <summary>
		/// Remove the child data node with the given number and a parent with the given name
		/// </summary>
		/// <param name="name">The name of the parent data node</param>
		/// <param name="number">The number of the child data node to remove</param>
		void RemoveNode(string name, int number);
		
		/// <summary>
		/// Get the union of the two data matrices
		/// </summary>
		/// <param name="matrix">The other matrix</param>
		/// <returns>A DataMatrix that represents the union of the two matrices</returns>
		IDataMatrix Union(IDataMatrix matrix);
		
		/// <summary>
		/// Get the difference of the two data matrices
		/// </summary>
		/// <param name="matrix">The other matrix</param>
		/// <returns>A DataMatrix that represents the difference of the two matrices</returns>
		IDataMatrix Difference(IDataMatrix matrix);
	}
}
