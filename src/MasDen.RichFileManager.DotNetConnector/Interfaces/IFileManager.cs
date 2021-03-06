﻿//-----------------------------------------------------------------------
// <copyright file="IFileManager.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Interfaces
{
	#region Usings

	using System.Collections.Generic;

	using MasDen.RichFileManager.DotNetConnector.Entities;
	using Microsoft.AspNetCore.Http;

	#endregion

	/// <summary>
	/// Represents the interface of file manager component.
	/// </summary>
	public interface IFileManager
	{
		/// <summary>
		/// Gets the folder.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>
		/// The collection of <see cref="ItemData"/> from path.
		/// </returns>
		ICollection<ItemData> GetFolder(string path);

		/// <summary>
		/// Gets the file data.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>The <see cref="FileData"/> object.</returns>
		FileData GetFileData(string path);

		/// <summary>
		/// Creates the directory.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="folderName">Name of the folder.</param>
		/// <returns>The <see cref="ItemData"/> with all details of created folder.</returns>
		ItemData CreateDirectory(string path, string folderName);

		/// <summary>
		/// Deletes the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>The <see cref="ItemData"/> object with all information about deleted resource.</returns>
		ItemData Delete(string path);

		/// <summary>
		/// Uploads the specified files.
		/// </summary>
		/// <param name="files">The files.</param>
		/// <param name="path">The path.</param>
		/// <returns>The collection of <see cref="ItemData"/>.</returns>
		ICollection<ItemData> Upload(IFormFileCollection files, string path);

		/// <summary>
		/// Renames an existed file or folder.
		/// </summary>
		/// <param name="path">The path to file or folder.</param>
		/// <param name="newName">The new name.</param>
		/// <returns>The <see cref="ItemData"/> object.</returns>
		ItemData Rename(string path, string newName);

		/// <summary>
		/// Moves the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="destination">The destination.</param>
		/// <returns>The <see cref="ItemData"/> object.</returns>
		ItemData Move(string path, string destination);

		/// <summary>
		/// Copies the specified source.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="target">The target.</param>
		/// <returns>The <see cref="ItemData"/> object.</returns>
		ItemData Copy(string source, string target);

		ItemData SaveFile(string path, string content);
	}
}