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
	public class MoveCommandTest
	{
		#region Constants

		private const string OldPath = "old";
		private const string NewPath = "new";

		#endregion

		#region Private Fields

		private HttpContext httpContext;

		private MoveCommand command;

		private IFileManager fileManager;

		#endregion

		#region Test Context

		[SetUp]
		public void Setup()
		{
			this.fileManager = Substitute.For<IFileManager>();

			var response = Substitute.For<HttpResponse>();
			response.Body = new MemoryStream();

			this.httpContext = TestHelpers.CreateHttpContextMock(ModeNames.Move, "GET");
			httpContext.Response.Returns(response);
			TestHelpers.AddQueryKey(this.httpContext.Request.Query, RequestKeys.Old, OldPath);
			TestHelpers.AddQueryKey(this.httpContext.Request.Query, RequestKeys.New, NewPath);

			this.httpContext.RequestServices.GetService(typeof(IFileManager)).Returns(this.fileManager);

			this.command = new MoveCommand(this.httpContext);
		}

		#endregion

		#region Tests

		[Test]
		public async Task ExecuteShouldMoveItemTest()
		{
			// Arrange
			var itemData = new ItemData(NewPath, ItemType.File, null);

			this.fileManager.Move(OldPath, NewPath).Returns(itemData);

			// Act
			await this.command.Execute();

			// Assert
			this.fileManager.Received().Move(OldPath, NewPath);
			Assert.IsTrue(this.httpContext.Response.Body.Length > 0);
		}

		#endregion
	}
}