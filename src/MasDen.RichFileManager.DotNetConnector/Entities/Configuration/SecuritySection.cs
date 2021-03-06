﻿//-----------------------------------------------------------------------
// <copyright file="SecuritySection.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Entities.Configuration
{
	/// <summary>
	/// Represents the configuration options from security section.
	/// </summary>
	public class SecuritySection
	{
		#region Public Properties

		public bool ReadOnly { get; set; }

		public SecurityConfigurationExtensions Extensions { get; set; }

		#endregion
	}
}