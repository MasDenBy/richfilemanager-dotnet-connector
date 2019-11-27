using System.IO;
using System.Threading.Tasks;

using MasDen.RichFileManager.DotNetConnector.Components.Actions;
using MasDen.RichFileManager.DotNetConnector.Constants;
using MasDen.RichFileManager.DotNetConnector.Entities;
using MasDen.RichFileManager.DotNetConnector.Entities.Enumerations;
using MasDen.RichFileManager.DotNetConnector.Interfaces;

using Microsoft.AspNetCore.Http;

using NSubstitute;

using NUnit.Framework;

namespace MasDen.RichFileManager.DotNetConnector.Test.Components.Actions
{
	[TestFixture]
	public class UploadCommandTest
	{
		#region Constants

		private const string Path = "path";

		#endregion

		#region Private Fields

		private HttpContext httpContext;

		private UploadCommand command;

		private IFileManager fileManager;

		#endregion

		#region Test Context

		[SetUp]
		public void Setup()
		{
			this.fileManager = Substitute.For<IFileManager>();

			var response = Substitute.For<HttpResponse>();
			response.Body = new MemoryStream();

			this.httpContext = TestHelpers.CreateHttpContextMock(ModeNames.Upload, "POST");
			httpContext.Response.Returns(response);
			TestHelpers.AddFormKey(this.httpContext.Request.Form, RequestKeys.Path, Path);
			this.httpContext.Request.Form.Files.Returns(Substitute.For<IFormFileCollection>());

			this.httpContext.RequestServices.GetService(typeof(IFileManager)).Returns(this.fileManager);

			this.command = new UploadCommand(this.httpContext);
		}

		#endregion

		#region Tests

		[Test]
		public async Task ExecuteShouldUploadFilesTest()
		{
			// Arrange
			var items = new[] { new ItemData("", ItemType.File, null) };

			this.fileManager.Upload(this.httpContext.Request.Form.Files, Path).Returns(items);

			// Act
			await this.command.Execute();

			// Assert
			this.fileManager.Received().Upload(this.httpContext.Request.Form.Files, Path);
			Assert.IsTrue(this.httpContext.Response.Body.Length > 0);
		}

		#endregion
	}
}