//-----------------------------------------------------------------------
// <copyright file="DefaultConfigurationManager.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Components
{
	#region Usings

	using System;

	using MasDen.RichFileManager.DotNetConnector.Entities.Configuration;
	using MasDen.RichFileManager.DotNetConnector.Interfaces;

	using Microsoft.Extensions.Options;

	#endregion

	/// <summary>
	/// Represents the default configuration manager.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Interfaces.IConfigurationManager" />
	public class ConfigurationManager : IConfigurationManager
	{
		#region Private Fields

		/// <summary>
		/// The options
		/// </summary>
		private readonly IOptions<FileManagerConfiguration> options;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ConfigurationManager"/> class.
		/// </summary>
		/// <param name="options">The options.</param>
		public ConfigurationManager(IOptions<FileManagerConfiguration> options)
		{
			this.options = options;
		}

		#endregion

		#region IConfigurationManager Members

		/// <summary>
		/// Gets the configuration.
		/// </summary>
		/// <returns>
		/// The <see cref="FileManagerConfiguration" /> object.
		/// </returns>
		public FileManagerConfiguration GetConfiguration()
		{
			if(this.options == null || this.options.Value == null)
			{
				throw new InvalidOperationException("The configuration cannot be null");
			}

			return this.options.Value;
		}

		#endregion
	}
}