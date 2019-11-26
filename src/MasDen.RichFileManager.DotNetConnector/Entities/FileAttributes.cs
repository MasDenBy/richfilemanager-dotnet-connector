//-----------------------------------------------------------------------
// <copyright file="FileAttributes.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Entities
{
	#region Usings

	using System;

	using MasDen.RichFileManager.DotNetConnector.Components.Converters;

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Represents the attributes of file.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Entities.AttributesBase" />
	public class FileAttributes : AttributesBase
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the path.
		/// </summary>
		/// <value>
		/// The path.
		/// </value>
		[JsonProperty("path")]
		public string Path { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="FileAttributes"/> is readable.
		/// </summary>
		/// <value>
		///   <c>true</c> if readable; otherwise, <c>false</c>.
		/// </value>
		[JsonConverter(typeof(BoolToIntConverter))]
		[JsonProperty("readable")]
		public bool Readable { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="FileAttributes"/> is writable.
		/// </summary>
		/// <value>
		///   <c>true</c> if writable; otherwise, <c>false</c>.
		/// </value>
		[JsonConverter(typeof(BoolToIntConverter))]
		[JsonProperty("writable")]
		public bool Writable { get; set; }

		/// <summary>
		/// Gets or sets the created.
		/// </summary>
		/// <value>
		/// The created.
		/// </value>
		[JsonProperty("created")]
		public DateTime Created { get; set; }

		/// <summary>
		/// Gets or sets the modified.
		/// </summary>
		/// <value>
		/// The modified.
		/// </value>
		[JsonProperty("modified")]
		public DateTime Modified { get; set; }

		/// <summary>
		/// Gets or sets the height.
		/// </summary>
		/// <value>
		/// The height.
		/// </value>
		[JsonProperty("height")]
		public int Height { get; set; }

		/// <summary>
		/// Gets or sets the width.
		/// </summary>
		/// <value>
		/// The width.
		/// </value>
		[JsonProperty("width")]
		public int Width { get; set; }

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>
		/// The size.
		/// </value>
		[JsonProperty("size")]
		public long Size { get; set; }

		#endregion
	}
}