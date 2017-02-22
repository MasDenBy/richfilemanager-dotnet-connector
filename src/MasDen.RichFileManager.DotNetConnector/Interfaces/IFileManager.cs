//-----------------------------------------------------------------------
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
	}
}