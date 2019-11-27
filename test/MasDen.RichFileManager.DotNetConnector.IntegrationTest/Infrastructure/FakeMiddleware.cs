//-----------------------------------------------------------------------
// <copyright file="FakeMiddleware.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Test.Infrastructure
{
	#region Usings

	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Http;

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Represents the fake middleware.
	/// </summary>
	public class FakeMiddleware
	{
		#region Private Fields

		/// <summary>
		/// The next
		/// </summary>
		private readonly RequestDelegate next;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FakeMiddleware"/> class.
		/// </summary>
		/// <param name="next">The next.</param>
		public FakeMiddleware(RequestDelegate next)
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
			await context.Response.WriteAsync(JsonConvert.SerializeObject("fake"));
		}

		#endregion
	}
}