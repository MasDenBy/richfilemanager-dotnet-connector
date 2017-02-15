//-----------------------------------------------------------------------
// <copyright file="FileManagerMiddlewareTest.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Test
{
	#region Usings

	using System.Threading.Tasks;

	using MasDen.RichFileManager.DotNetConnector.Test.Infrastructure;

	using NUnit.Framework;

	#endregion

	/// <summary>
	/// Represents the tests methods for <see cref="FileManagerMiddleware"/>.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Test.Infrastructure.IntegrationTestBase" />
	[TestFixture]
	public class FileManagerMiddlewareTest : IntegrationTestBase
	{
		#region Constants

		/// <summary>
		/// The rich file manager connector URL
		/// </summary>
		private const string RichFileManagerConnectorUrl = "filemanager.dotnet";

		#endregion

		#region Tests

		/// <summary>
		/// The test checks that middleware does not process all requests.
		/// </summary>
		/// <returns></returns>
		[Test]
		public async Task Invoke_NotFileManagerRequest_InvokeNext()
		{
			var response = await this.Client.GetAsync("/default.aspx");
			string content = await response.Content.ReadAsStringAsync();

			Assert.IsTrue(content.Contains("fake"));
		}

		#endregion
	}
}