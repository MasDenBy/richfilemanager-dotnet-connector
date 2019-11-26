//-----------------------------------------------------------------------
// <copyright file="InitiateCommandAttributesConfig.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Entities
{
	#region Usings

	using MasDen.RichFileManager.DotNetConnector.Entities.Configuration;

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Represents the config information.
	/// </summary>
	public class InitiateCommandAttributesConfig
	{
		#region Public Properties

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