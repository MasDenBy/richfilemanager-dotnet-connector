//-----------------------------------------------------------------------
// <copyright file="OptionsSection.cs" author="Ihar Maiseyeu">
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
	/// Represents the all configuration items in option section.
	/// </summary>
	public class OptionsSection
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the culture.
		/// </summary>
		/// <value>
		/// The culture.
		/// </value>
		[JsonProperty("culture")]
		public string Culture { get; set; }

		/// <summary>
		/// Gets or sets the theme.
		/// </summary>
		/// <value>
		/// The theme.
		/// </value>
		[JsonProperty("theme")]
		public string Theme { get; set; }

		/// <summary>
		/// Gets or sets the default view mode.
		/// </summary>
		/// <value>
		/// The default view mode.
		/// </value>
		[JsonProperty("defaultViewMode")]
		public string DefaultViewMode { get; set; }

		/// <summary>
		/// Gets or sets the localize GUI.
		/// </summary>
		/// <value>
		/// The localize GUI.
		/// </value>
		[JsonProperty("localizeGUI")]
		public bool? LocalizeGUI { get; set; }

		/// <summary>
		/// Gets or sets the show full path.
		/// </summary>
		/// <value>
		/// The show full path.
		/// </value>
		[JsonProperty("showFullPath")]
		public bool? showFullPath { get; set; }

		/// <summary>
		/// Gets or sets the show title attribute.
		/// </summary>
		/// <value>
		/// The show title attribute.
		/// </value>
		[JsonProperty("showTitleAttr")]
		public bool? showTitleAttr { get; set; }

		/// <summary>
		/// Gets or sets the show confirmation.
		/// </summary>
		/// <value>
		/// The show confirmation.
		/// </value>
		[JsonProperty("showConfirmation")]
		public bool? showConfirmation { get; set; }

		/// <summary>
		/// Gets or sets the browse only.
		/// </summary>
		/// <value>
		/// The browse only.
		/// </value>
		[JsonProperty("browseOnly")]
		public bool? browseOnly { get; set; }

		/// <summary>
		/// Gets or sets the clipboard.
		/// </summary>
		/// <value>
		/// The clipboard.
		/// </value>
		[JsonProperty("clipboard")]
		public bool? clipboard { get; set; }

		/// <summary>
		/// Gets or sets the search box.
		/// </summary>
		/// <value>
		/// The search box.
		/// </value>
		[JsonProperty("searchBox")]
		public bool? searchBox { get; set; }

		/// <summary>
		/// Gets or sets the list files.
		/// </summary>
		/// <value>
		/// The list files.
		/// </value>
		[JsonProperty("listFiles")]
		public bool? ListFiles { get; set; }

		/// <summary>
		/// Gets or sets the file sorting.
		/// </summary>
		/// <value>
		/// The file sorting.
		/// </value>
		[JsonProperty("fileSorting")]
		public string FileSorting { get; set; }

		/// <summary>
		/// Gets or sets the folder position.
		/// </summary>
		/// <value>
		/// The folder position.
		/// </value>
		[JsonProperty("folderPosition")]
		public string FolderPosition { get; set; }

		/// <summary>
		/// Gets or sets the quick select.
		/// </summary>
		/// <value>
		/// The quick select.
		/// </value>
		[JsonProperty("quickSelect")]
		public bool? QuickSelect { get; set; }

		/// <summary>
		/// Gets or sets the chars latin only.
		/// </summary>
		/// <value>
		/// The chars latin only.
		/// </value>
		[JsonProperty("charsLatinOnly")]
		public bool? CharsLatinOnly { get; set; }

		/// <summary>
		/// Gets or sets the width of the splitter.
		/// </summary>
		/// <value>
		/// The width of the splitter.
		/// </value>
		[JsonProperty("splitterWidth")]
		public double? SplitterWidth { get; set; }

		/// <summary>
		/// Gets or sets the minimum width of the splitter.
		/// </summary>
		/// <value>
		/// The minimum width of the splitter.
		/// </value>
		[JsonProperty("splitterMinWidth")]
		public double? SplitterMinWidth { get; set; }

		/// <summary>
		/// Gets or sets the logger.
		/// </summary>
		/// <value>
		/// The logger.
		/// </value>
		[JsonProperty("logger")]
		public bool? Logger { get; set; }

		/// <summary>
		/// Gets or sets the capabilities.
		/// </summary>
		/// <value>
		/// The capabilities.
		/// </value>
		[JsonProperty("capabilities")]
		public string Capabilities { get; set; }

		#endregion
	}
}