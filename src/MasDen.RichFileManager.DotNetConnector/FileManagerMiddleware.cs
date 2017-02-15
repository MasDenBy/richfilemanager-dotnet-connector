//-----------------------------------------------------------------------
// <copyright file="FileManagerMiddleware.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector
{
	#region Usings

	using System.Threading.Tasks;

	using MasDen.RichFileManager.DotNetConnector.Components;
	using MasDen.RichFileManager.DotNetConnector.Entities.Configuration;

	using Microsoft.AspNetCore.Http;

	using Microsoft.Extensions.Options;

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Represents the middleware which catches the requests and execute commands for each request.
	/// </summary>
	public class FileManagerMiddleware
	{
		#region Constants

		/// <summary>
		/// The request path end
		/// </summary>
		private const string RequestPathEnd = "filemanager.dotnet";

		#endregion

		#region Private Fields

		/// <summary>
		/// The next
		/// </summary>
		private readonly RequestDelegate next;

		/// <summary>
		/// The configuration
		/// </summary>
		private readonly IOptions<FileManagerConfiguration> configuration;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="IgnoreRouteMiddleware" /> class.
		/// </summary>
		/// <param name="next">The next.</param>
		/// <param name="configuration">The configuration.</param>
		public FileManagerMiddleware(RequestDelegate next, IOptions<FileManagerConfiguration> configuration)
		{
			this.next = next;
			this.configuration = configuration;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Invokes the specified context.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns>Need to async/await operation</returns>
		public async Task Invoke(HttpContext context)
		{
			if (context.Request.Path.HasValue &&
				context.Request.Path.Value.EndsWith(RequestPathEnd))
			{
				var command = CommandFactory.CreateCommand(context.Request.Query, this.configuration);
				var response = command.Execute();

				JsonSerializerSettings jsonSerializerOptions = new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore
				};

				await context.Response.WriteAsync(JsonConvert.SerializeObject(response, jsonSerializerOptions));
			}
			else
			{
				await next.Invoke(context);
			}
		}

		#endregion
	}
}