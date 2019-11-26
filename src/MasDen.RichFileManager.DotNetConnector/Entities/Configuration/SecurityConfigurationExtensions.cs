//-----------------------------------------------------------------------
// <copyright file="SecurityConfigurationExtensions.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Entities.Configuration
{
	#region Usings

	using System.Collections.Generic;

	#endregion

	/// <summary>
	/// The security configuration extensions class.
	/// </summary>
	public class SecurityConfigurationExtensions
	{
		#region Public Properties

		public string Policy { get; set; } = "ALLOW_LIST";

		public bool IgnoreCase { get; set; }

		public ICollection<string> Restrictions { get; set; }

		#endregion
	}
}