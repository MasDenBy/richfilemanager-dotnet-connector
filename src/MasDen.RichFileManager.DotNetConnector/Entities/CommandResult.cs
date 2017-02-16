﻿//-----------------------------------------------------------------------
// <copyright file="CommandResult.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Entities
{
	#region Usings

	using MasDen.RichFileManager.DotNetConnector.Entities.Enumerations;

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Represents the result of command.
	/// </summary>
	public class CommandResult
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandResult" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="type">The type.</param>
		public CommandResult(string id, ItemType type, AttributesBase attributes)
		{
			this.Data = new ItemData(id, type, attributes);
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <value>
		/// The data.
		/// </value>
		[JsonProperty("data")]
		public ItemData Data { get; private set; }

		#endregion
	}
}