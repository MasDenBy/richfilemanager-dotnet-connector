//-----------------------------------------------------------------------
// <copyright file="FileManagerConfiguration.cs" author="Ihar Maiseyeu">
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
	/// Represents the configuration for file manager
	/// </summary>
	public class FileManagerConfiguration
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the root path.
		/// </summary>
		/// <value>
		/// The root path.
		/// </value>
		[JsonProperty("rootPath")]
		public string RootPath { get; set; }

		/// <summary>
		/// Gets or sets the icon directory.
		/// </summary>
		/// <value>
		/// The icon directory.
		/// </value>
		[JsonProperty("iconDirectory")]
		public string IconDirectory { get; set; }

		/// <summary>
		/// Gets or sets the options.
		/// </summary>
		/// <value>
		/// The options.
		/// </value>
		[JsonProperty("options")]
		public OptionsSection Options { get; set; }

		/// <summary>
		/// Gets or sets the security.
		/// </summary>
		/// <value>
		/// The security.
		/// </value>
		[JsonProperty("security")]
		public SecuritySection Security { get; set; }

		/// <summary>
		/// Gets or sets the upload.
		/// </summary>
		/// <value>
		/// The upload.
		/// </value>
		[JsonProperty("upload")]
		public UploadSection Upload { get; set; }

		#endregion
	}
}