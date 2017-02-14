//-----------------------------------------------------------------------
// <copyright file="FileManagerMiddlewareExtensions.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector
{
	#region Usings

	using Microsoft.AspNetCore.Builder;

	#endregion

	/// <summary>
	/// Represents the extensions methods to use RichFileManager.
	/// </summary>
	public static class FileManagerMiddlewareExtensions
	{
		#region Public Methods

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