//-----------------------------------------------------------------------
// <copyright file="ItemData.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Entities
{
	#region Usings

	using MasDen.RichFileManager.DotNetConnector.Entities.Enumerations;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Converters;
	using Newtonsoft.Json.Serialization;

	#endregion

	/// <summary>
	/// Represents the data of response.
	/// </summary>
	public class ItemData
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ItemData"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="type">The type.</param>
		/// <param name="attributes">The attributes.</param>
		public ItemData(string id, ItemType type, AttributesBase attributes)
		{
			this.Id = id;
			this.ItemType = type;
			this.Attributes = attributes;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		[JsonProperty("id")]
		public string Id { get; private set; }

		/// <summary>
		/// Gets the type of the command.
		/// </summary>
		/// <value>
		/// The type of the command.
		/// </value>
		[JsonProperty("type")]
		[JsonConverter(typeof(StringEnumConverter))]
		public ItemType ItemType { get; private set; }

		/// <summary>
		/// Gets the attributes.
		/// </summary>
		/// <value>
		/// The attributes.
		/// </value>
		[JsonProperty("attributes")]
		public AttributesBase Attributes { get; private set; }

		#endregion
	}
}