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
	public class DeleteCommandTest
	{
		#region Constants

		private const string Path = "path";

		#endregion

		#region Private Fields

		private HttpContext httpContext;

		private DeleteCommand command;

		private IFileManager fileManager;

		#endregion

		#region Test Context

		[SetUp]
		public void Setup()
		{
			this.fileManager = Substitute.For<IFileManager>();

			var response = Substitute.For<HttpResponse>();
			response.Body = Substitute.For<Stream>();

			this.httpContext = TestHelpers.CreateHttpContextMock(ModeNames.Delete, "GET");
			httpContext.Response.Returns(response);
			TestHelpers.AddQueryKey(this.httpContext.Request.Query, RequestKeys.Path, Path);

			this.httpContext.RequestServices.GetService(typeof(IFileManager)).Returns(this.fileManager);

			this.command = new DeleteCommand(this.httpContext);
		}

		#endregion

		#region Tests

		[Test]
		public async Task ExecuteShouldDeleteTest()
		{
			// Arrange
			var itemData = new ItemData(Path, ItemType.File, null);

			this.fileManager.Delete(Path).Returns(itemData);

			// Act
			await this.command.Execute();

			// Assert
			this.fileManager.Received().Delete(Path);
			await this.httpContext.Response.Body.Received().WriteAsync(Arg.Any<byte[]>(), Arg.Any<int>(), Arg.Any<int>());
		}

		#endregion
	}
}