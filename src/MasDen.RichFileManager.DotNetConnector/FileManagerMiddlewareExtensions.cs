//-----------------------------------------------------------------------
// <copyright file="FileManagerMiddlewareExtensions.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector
{
	#region Usings

	using System;
	using System.Reflection;

	using MasDen.RichFileManager.DotNetConnector.Components;
	using MasDen.RichFileManager.DotNetConnector.Entities.Configuration;
	using MasDen.RichFileManager.DotNetConnector.Interfaces;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// Represents the extensions methods to use RichFileManager.
	/// </summary>
	public static class FileManagerMiddlewareExtensions
	{
		#region Public Methods

		/// <summary>
		/// Adds the rich file manager.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="configuration">The configuration.</param>
		/// <returns>
		/// The <see cref="IServiceCollection" /> object.
		/// </returns>
		public static IServiceCollection AddRichFileManager(this IServiceCollection services, IConfiguration configuration)
		{
			return services.AddRichFileManager(configuration, typeof(DefaultConfigurationManager));
		}

		/// <summary>
		/// Adds the rich file manager.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="configuration">The configuration action.</param>
		/// <param name="configurationManager">The configuration manager.</param>
		/// <returns>
		/// The <see cref="IServiceCollection" /> object.
		/// </returns>
		/// <exception cref="System.ArgumentException">The configurationManager should be derived from IConfigurationManager interface</exception>
		public static IServiceCollection AddRichFileManager(
			this IServiceCollection services, 
			IConfiguration configuration,
			Type configurationManager)
		{
			if(!typeof(IConfigurationManager).IsAssignableFrom(configurationManager))
			{
				throw new ArgumentException("The configurationManager should be derived from IConfigurationManager interface");
			}

			services.AddTransient<IFileManager, FileManager>();
			services.AddTransient(typeof(IConfigurationManager), configurationManager);

			services.Configure<FileManagerConfiguration>(configuration);

			return services;
		}

		/// <summary>
		/// Uses the rich file manager.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <returns>The <see cref="IApplicationBuilder"/> object with added <see cref="FileManagerMiddleware"/></returns>
		public static IApplicationBuilder UseRichFileManager(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<FileManagerMiddleware>();
		}

		#endregion
	}
}