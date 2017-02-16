﻿//-----------------------------------------------------------------------
// <copyright file="CommandResultCollection.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Entities
{
	#region Usings

	using System.Collections.Generic;

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Represents the result of command with array of data.
	/// </summary>
	public class CommandResultCollection
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandResultCollection" /> class.
		/// </summary>
		/// <param name="data">The data.</param>
		public CommandResultCollection(ICollection<ItemData> data)
		{
			this.Data = data;
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
		public ICollection<ItemData> Data { get; private set; }

		#endregion
	}
}