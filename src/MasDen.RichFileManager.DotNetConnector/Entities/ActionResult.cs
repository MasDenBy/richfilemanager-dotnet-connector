//-----------------------------------------------------------------------
// <copyright file="ActionResult.cs" author="Ihar Maiseyeu">
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
	/// Represents the result of action.
	/// </summary>
	public class ActionResult
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionResult"/> class.
		/// </summary>
		/// <param name="data">The data.</param>
		public ActionResult(ItemData data)
		{
			this.Data = data;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionResult" /> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="type">The type.</param>
		public ActionResult(string id, ItemType type, AttributesBase attributes)
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