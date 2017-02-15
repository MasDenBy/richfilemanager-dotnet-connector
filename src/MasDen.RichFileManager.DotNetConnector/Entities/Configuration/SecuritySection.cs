//-----------------------------------------------------------------------
// <copyright file="SecuritySection.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Entities.Configuration
{
	#region Usings

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Represents the configuration options from security section.
	/// </summary>
	public class SecuritySection
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the allow folder download.
		/// </summary>
		/// <value>
		/// The allow folder download.
		/// </value>
		[JsonProperty("allowFolderDownload")]
		public bool? AllowFolderDownload { get; set; }

		/// <summary>
		/// Gets or sets the allow change extensions.
		/// </summary>
		/// <value>
		/// The allow change extensions.
		/// </value>
		[JsonProperty("allowChangeExtensions")]
		public bool? AllowChangeExtensions { get; set; }

		/// <summary>
		/// Gets or sets the allow no extension.
		/// </summary>
		/// <value>
		/// The allow no extension.
		/// </value>
		[JsonProperty("allowNoExtension")]
		public bool? AllowNoExtension { get; set; }

		/// <summary>
		/// Gets or sets the normalize filename.
		/// </summary>
		/// <value>
		/// The normalize filename.
		/// </value>
		[JsonProperty("normalizeFilename")]
		public bool? NormalizeFilename { get; set; }

		#endregion
	}
}