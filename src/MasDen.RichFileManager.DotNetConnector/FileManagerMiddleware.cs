//-----------------------------------------------------------------------
// <copyright file="FileManagerMiddleware.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector
{
	#region Usings

	using System;
	using System.Threading.Tasks;

	using MasDen.RichFileManager.DotNetConnector.Components;
	using MasDen.RichFileManager.DotNetConnector.Entities;

	using Microsoft.AspNetCore.Http;

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

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="IgnoreRouteMiddleware" /> class.
		/// </summary>
		/// <param name="next">The next.</param>
		public FileManagerMiddleware(RequestDelegate next)
		{
			this.next = next;
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
				try
				{
					var action = ActionFactory.CreateAction(context);

					await action.Execute();
				}
				catch (Exception ex)
				{
					var data = new ErrorData() { Code = "500", Id = "server", Title = ex.Message };
					var response = "{\"errors\":[" + JsonConvert.SerializeObject(data) + "]}";

					await context.Response.WriteAsync(response);
				}
			}
			else
			{
				await next.Invoke(context);
			}
		}

		#endregion
	}
}