//-----------------------------------------------------------------------
// <copyright file="FileManagerMiddlewareExtensions.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector
{
	#region Usings

	using MasDen.RichFileManager.DotNetConnector.Components;
	using MasDen.RichFileManager.DotNetConnector.Interfaces;

	using Microsoft.AspNetCore.Builder;
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
		/// <returns>The <see cref="IServiceCollection"/> object.</returns>
		public static IServiceCollection AddRichFileManager(this IServiceCollection services)
		{
			services.AddTransient<IFileManager, FileManager>();

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