//-----------------------------------------------------------------------
// <copyright file="TestStartup.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Test.Infrastructure
{
	#region Usings

	using Microsoft.AspNetCore.Builder;

	#endregion

	/// <summary>
	/// Represents the startup class to register all dependencies of host builder.
	/// </summary>
	public class TestStartup
	{
		#region Public Methods

		/// <summary>
		/// Configures the specified application.
		/// </summary>
		/// <param name="app">The application.</param>
		public void Configure(IApplicationBuilder app)
		{
			app.UseRichFileManager();
			app.UseMiddleware<FakeMiddleware>();
		}

		#endregion
	}
}