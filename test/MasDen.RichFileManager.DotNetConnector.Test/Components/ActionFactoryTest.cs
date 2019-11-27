using System;

using MasDen.RichFileManager.DotNetConnector.Components;
using MasDen.RichFileManager.DotNetConnector.Components.Actions;
using MasDen.RichFileManager.DotNetConnector.Constants;

using Microsoft.AspNetCore.Http;

using NUnit.Framework;

namespace MasDen.RichFileManager.DotNetConnector.Test.Components
{
	[TestFixture]
	public class ActionFactoryTest
	{
		#region Tests

		[TestCase(ModeNames.Initiate, "GET", typeof(InitiateQuery))]
		[TestCase(ModeNames.ReadFolder, "GET", typeof(ReadFolderQuery))]
		[TestCase(ModeNames.GetImage, "GET", typeof(GetImageQuery))]
		[TestCase(ModeNames.AddFolder, "GET", typeof(AddFolderCommand))]
		[TestCase(ModeNames.Delete, "GET", typeof(DeleteCommand))]
		[TestCase(ModeNames.ReadFile, "GET", typeof(ReadFileQuery))]
		[TestCase(ModeNames.Upload, "POST", typeof(UploadCommand))]
		[TestCase(ModeNames.Rename, "GET", typeof(RenameCommand))]
		[TestCase(ModeNames.Move, "GET", typeof(MoveCommand))]
		[TestCase(ModeNames.Copy, "GET", typeof(CopyCommand))]
		[TestCase(ModeNames.SaveFile, "POST", typeof(SaveFileCommand))]
		public void CreateActionTest(string mode, string method, Type expectedAction)
		{
			// Arrange
			var httpContext = CreateHttpContextMock(mode, method);

			// Act
			var action = ActionFactory.CreateAction(httpContext);

			// Assert
			Assert.AreEqual(expectedAction, action.GetType());
		}

		[Test]
		public void CreateActionIfNotExistsModeShouldThrowException()
		{
			// Arrange
			var httpContext = CreateHttpContextMock("invalid", "GET");

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() => ActionFactory.CreateAction(httpContext));
		}

		#endregion

		#region Private Methods

		private static HttpContext CreateHttpContextMock(string mode, string method)
		{
			var httpContext = TestHelpers.CreateHttpContextMock(mode, method);

			TestHelpers.AddQueryKey(httpContext.Request.Query, RequestKeys.Mode, mode);
			TestHelpers.AddQueryKey(httpContext.Request.Query, RequestKeys.Path, "path");
			TestHelpers.AddQueryKey(httpContext.Request.Query, RequestKeys.Name, "name");
			TestHelpers.AddQueryKey(httpContext.Request.Query, RequestKeys.Source, "source");
			TestHelpers.AddQueryKey(httpContext.Request.Query, RequestKeys.Target, "target");
			TestHelpers.AddQueryKey(httpContext.Request.Query, RequestKeys.Old, "old");
			TestHelpers.AddQueryKey(httpContext.Request.Query, RequestKeys.New, "new");
			TestHelpers.AddFormKey(httpContext.Request.Form, RequestKeys.Mode, mode);
			TestHelpers.AddFormKey(httpContext.Request.Form, RequestKeys.Path, "path");
			TestHelpers.AddFormKey(httpContext.Request.Form, RequestKeys.Content, "content");

			return httpContext;
		}

		#endregion
	}
}