//-----------------------------------------------------------------------
// <copyright file="TestStartup.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Test.Infrastructure
{
	#region Usings

	using MasDen.RichFileManager.DotNetConnector.Entities.Configuration;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;

	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// Represents the startup class to register all dependencies of host builder.
	/// </summary>
	public class TestStartup
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Startup"/> class.
		/// </summary>
		/// <param name="hostingEnvironment">The hosting environment.</param>
		public TestStartup(IHostingEnvironment hostingEnvironment)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(hostingEnvironment.ContentRootPath)
				.AddJsonFile("appsettings.json");

			this.Configuration = builder.Build();
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the configuration.
		/// </summary>
		/// <value>
		/// The configuration.
		/// </value>
		public IConfigurationRoot Configuration { get; }

		#endregion

		#region Public Methods

		/// <summary>
		/// Configures the services.
		/// </summary>
		/// <param name="services">The services.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<FileManagerConfiguration>(this.Configuration.GetSection("Filemanager"));

			services.AddRichFileManager();
		}

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