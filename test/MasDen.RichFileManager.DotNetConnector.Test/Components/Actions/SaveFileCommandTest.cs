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
	public class SaveFileCommandTest
	{
		#region Constants

		private const string Path = "path";
		private const string Content = "content";

		#endregion

		#region Private Fields

		private HttpContext httpContext;

		private SaveFileCommand command;

		private IFileManager fileManager;

		#endregion

		#region Test Context

		[SetUp]
		public void Setup()
		{
			this.fileManager = Substitute.For<IFileManager>();

			var response = Substitute.For<HttpResponse>();
			response.Body = new MemoryStream();

			this.httpContext = TestHelpers.CreateHttpContextMock(ModeNames.SaveFile, "POST");
			httpContext.Response.Returns(response);
			TestHelpers.AddFormKey(this.httpContext.Request.Form, RequestKeys.Path, Path);
			TestHelpers.AddFormKey(this.httpContext.Request.Form, RequestKeys.Content, Content);

			this.httpContext.RequestServices.GetService(typeof(IFileManager)).Returns(this.fileManager);

			this.command = new SaveFileCommand(this.httpContext);
		}

		#endregion

		#region Tests

		[Test]
		public async Task ExecuteShouldSaveNewContentTest()
		{
			// Arrange
			var itemData = new ItemData(Path, ItemType.File, null);

			this.fileManager.SaveFile(Path, Content).Returns(itemData);

			// Act
			await this.command.Execute();

			// Assert
			this.fileManager.Received().SaveFile(Path, Content);
			Assert.IsTrue(this.httpContext.Response.Body.Length > 0);
		}

		#endregion
	}
}