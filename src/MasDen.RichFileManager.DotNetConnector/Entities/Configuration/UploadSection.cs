//-----------------------------------------------------------------------
// <copyright file="UploadSection.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Entities.Configuration
{
	/// <summary>
	/// Represents the configuration items for upload section.
	/// </summary>
	public class UploadSection
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the multiple.
		/// </summary>
		/// <value>
		/// The multiple.
		/// </value>
		public bool? Multiple { get; set; }

		/// <summary>
		/// Gets or sets the maximum number of files.
		/// </summary>
		/// <value>
		/// The maximum number of files.
		/// </value>
		public int? MaxNumberOfFiles { get; set; }

		/// <summary>
		/// Gets or sets the name of the parameter.
		/// </summary>
		/// <value>
		/// The name of the parameter.
		/// </value>
		public string ParamName { get; set; }

		/// <summary>
		/// Gets or sets the size of the chunk.
		/// </summary>
		/// <value>
		/// The size of the chunk.
		/// </value>
		public bool? ChunkSize { get; set; }

		/// <summary>
		/// Gets or sets the file size limit.
		/// </summary>
		/// <value>
		/// The file size limit.
		/// </value>
		public int? FileSizeLimit { get; set; }

		#endregion
	}
}