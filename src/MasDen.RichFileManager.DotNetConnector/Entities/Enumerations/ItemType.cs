//-----------------------------------------------------------------------
// <copyright file="ItemType.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Entities.Enumerations
{
	#region Usings

	using System.Runtime.Serialization;

	#endregion

	/// <summary>
	/// Represents the type of response item.
	/// </summary>
	public enum ItemType
	{
		[EnumMember(Value = "initiate")]
		Initiate,

		[EnumMember(Value = "folder")]
		Folder,

		[EnumMember(Value = "file")]
		File
	}
}