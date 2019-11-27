using System;
using System.IO;
using System.Threading.Tasks;

using MasDen.RichFileManager.DotNetConnector.Components.Actions;
using MasDen.RichFileManager.DotNetConnector.Constants;
using MasDen.RichFileManager.DotNetConnector.Entities;
using MasDen.RichFileManager.DotNetConnector.Interfaces;

using Microsoft.AspNetCore.Http;

using NSubstitute;

using NUnit.Framework;

namespace MasDen.RichFileManager.DotNetConnector.Test.Components.Actions
{
	[TestFixture]
	public class ReadFileQueryTest
	{
		#region Constants

		private const string Path = "/file1.txt";

		#endregion

		#region Private Fields

		private HttpContext httpContext;

		private ReadFileQuery query;

		private IFileManager fileManager;

		#endregion

		#region Test Context

		[SetUp]
		public void Setup()
		{
			this.fileManager = Substitute.For<IFileManager>();

			var response = Substitute.For<HttpResponse>();
			response.Body = new MemoryStream();

			this.httpContext = TestHelpers.CreateHttpContextMock(ModeNames.ReadFile, "GET");
			httpContext.Response.Returns(response);
			TestHelpers.AddQueryKey(this.httpContext.Request.Query, RequestKeys.Path, Path);

			this.httpContext.RequestServices.GetService(typeof(IFileManager)).Returns(this.fileManager);

			this.query = new ReadFileQuery(this.httpContext);
		}

		#endregion

		#region Tests

		[Test]
		public async Task ExecuteShouldGetFileTest()
		{
			// Arrange
			var itemData = new FileData(Path, $"{AppDomain.CurrentDomain.BaseDirectory}/Resources{Path}", "text/plain");

			this.fileManager.GetFileData(Path).Returns(itemData);

			// Act
			await this.query.Execute();

			// Assert
			this.fileManager.Received().GetFileData(Path);
			Assert.IsTrue(this.httpContext.Response.Body.Length > 0);
		}

		#endregion
	}
}