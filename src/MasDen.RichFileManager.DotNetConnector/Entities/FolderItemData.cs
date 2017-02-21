//-----------------------------------------------------------------------
// <copyright file="FolderItemData.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Entities
{
	#region Usings

	using MasDen.RichFileManager.DotNetConnector.Entities.Enumerations;

	#endregion

	/// <summary>
	/// Represents the data of folder.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Entities.ItemData" />
	public class FolderItemData : ItemData
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FolderItemData"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="attributes">The attributes.</param>
		public FolderItemData(string id, AttributesBase attributes) 
			: base($"{id}/", ItemType.Folder, attributes)
		{
		}

		#endregion
	}
}