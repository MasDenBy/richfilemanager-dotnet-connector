//-----------------------------------------------------------------------
// <copyright file="CommandType.cs" author="Ihar Maiseyeu">
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
	/// Represents the types of commands.
	/// </summary>
	public enum CommandType
	{
		[EnumMember(Value = "initiate")]
		Initiate
	}
}