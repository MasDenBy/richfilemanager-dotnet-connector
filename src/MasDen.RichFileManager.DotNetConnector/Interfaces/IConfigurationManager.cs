//-----------------------------------------------------------------------
// <copyright file="IConfigurationManager.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Interfaces
{
	#region Usings

	using MasDen.RichFileManager.DotNetConnector.Entities.Configuration;

	#endregion

	/// <summary>
	/// Represents the interface to work with configuration of filemanager.
	/// </summary>
	public interface IConfigurationManager
	{
		/// <summary>
		/// Gets the configuration.
		/// </summary>
		/// <returns>The <see cref="FileManagerConfiguration"/> object.</returns>
		FileManagerConfiguration GetConfiguration();
	}
}