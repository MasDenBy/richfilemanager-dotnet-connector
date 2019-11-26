//-----------------------------------------------------------------------
// <copyright file="FileManagerMiddlewareTest.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Test
{
	#region Usings

	using System.Linq;
	using System.Threading.Tasks;

	using MasDen.RichFileManager.DotNetConnector.Test.Constants;
	using MasDen.RichFileManager.DotNetConnector.Test.Infrastructure;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

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

		/// <summary>
		/// The test checks that initiate command return configuration.
		/// </summary>
		[Test]
		public async Task Invoke_InitiateQuery_ReturnConfiguration()
		{
			JObject result = await this.GetAsync($"{RichFileManagerConnectorUrl}?mode={ModeNames.Initiate}");

			Assert.IsNotNull(result);
			
			Assert.IsFalse(result["data"]["attributes"]["config"]["security"]["readOnly"].Value<bool>());
			Assert.AreEqual("ALLOW_ALL", result["data"]["attributes"]["config"]["security"]["extensions"]["policy"].ToString());
			Assert.IsTrue(result["data"]["attributes"]["config"]["security"]["extensions"]["ignoreCase"].Value<bool>());
			AssertCollection(
				new[] { "jpg", "jpe", "jpeg", "gif", "png" }, 
				result["data"]["attributes"]["config"]["security"]["extensions"]["restrictions"].Values<string>().ToArray());

			Assert.IsTrue(result["data"]["attributes"]["config"]["upload"]["multiple"].Value<bool>());
			Assert.AreEqual(3, result["data"]["attributes"]["config"]["upload"]["maxNumberOfFiles"].Value<int>());
			Assert.AreEqual("files", result["data"]["attributes"]["config"]["upload"]["paramName"].ToString());
			Assert.IsTrue(result["data"]["attributes"]["config"]["upload"]["chunkSize"].Value<bool>());
			Assert.AreEqual(10, result["data"]["attributes"]["config"]["upload"]["fileSizeLimit"].Value<int>());
		}

		/// <summary>
		/// The test checks that 'getfolder' command return content of root directory.
		/// </summary>
		/// <returns></returns>
		[Test]
		public async Task Invoke_GetFolder_ReturnsRootDirectory()
		{
			JObject result = await this.GetAsync($"{RichFileManagerConnectorUrl}?mode={ModeNames.GetFolder}&path=%2F");

			Assert.IsNotNull(result);
			Assert.IsNotNull(result["data"]);

			foreach(var item in result["data"].AsJEnumerable())
			{
				Assert.IsNotNull(item["id"].Value<string>());
				Assert.IsNotNull(item["type"].Value<string>());
				Assert.IsNotNull(item["attributes"]);

				FileManagerMiddlewareTest.ValidateItemDataId(item);
			}
		}

		/// <summary>
		/// The test checks that if directory does not found, command push the error information.
		/// </summary>
		/// <returns></returns>
		[Test]
		public async Task Invoke_GetFolder_ReturnsErrorIfPathDoesNotExists()
		{
			JObject result = await this.GetAsync($"{RichFileManagerConnectorUrl}?mode={ModeNames.GetFolder}&path=%2Fnotexistspath");

			Assert.IsNotNull(result);
			Assert.IsNotNull(result["errors"]);
			Assert.AreEqual("server", result["errors"].First["id"].ToString());
			Assert.AreEqual("500", result["errors"].First["code"].ToString());
			Assert.AreEqual("Directory '/notexistspath' not found", result["errors"].First["title"].ToString());
		}

		/// <summary>
		/// The test checks that 'getfolder' command return the content of folder inside root directory.
		/// </summary>
		[Test]
		public async Task Invoke_GetFolder_ReturnsContentOfDirectoryInsideRoot()
		{
			JObject result = await this.GetAsync($"{RichFileManagerConnectorUrl}?mode={ModeNames.GetFolder}&path=%2FFolder1");

			Assert.IsNotNull(result);
			Assert.IsNotNull(result["data"]);

			foreach (var item in result["data"].AsJEnumerable())
			{
				Assert.IsNotNull(item["id"].Value<string>());
				Assert.IsTrue(item["id"].Value<string>().StartsWith("/Folder1/"));
				Assert.IsNotNull(item["type"].Value<string>());
				Assert.IsNotNull(item["attributes"]);

				FileManagerMiddlewareTest.ValidateItemDataId(item);
			}
		}

		/// <summary>
		/// The test checks that 'getimage' command return image file.
		/// </summary>
		/// <returns></returns>
		[Test]
		public async Task Invoke_GetImage_ReturnImage()
		{
			var response = await this.Client.GetAsync($"{RichFileManagerConnectorUrl}?mode={ModeNames.GetImage}&path=%2Fdotnetfoundation.png");
			Assert.IsTrue(response.IsSuccessStatusCode);
			Assert.AreEqual("image/png", response.Content.Headers.ContentType.MediaType);
			Assert.IsTrue(response.Content.Headers.ContentLength > 0);
			Assert.AreEqual("dotnetfoundation.png", response.Content.Headers.ContentDisposition.FileName);
			Assert.AreEqual("attachment", response.Content.Headers.ContentDisposition.DispositionType);
		}

		/// <summary>
		/// The test checks functionalities of adding and deleting.
		/// </summary>
		[Test]
		public async Task Should_Create_And_Delete_Folder()
		{
			var folder = await this.GetAsync($"{RichFileManagerConnectorUrl}?mode={ModeNames.AddFolder}&path=%2F&name=NewFolder");

			Assert.IsNotNull(folder);
			Assert.IsNotNull(folder["data"]);
			Assert.IsNotNull(folder["data"]["id"].Value<string>());
			Assert.IsNotNull(folder["data"]["type"].Value<string>());
			Assert.AreEqual("folder", folder["data"]["type"].Value<string>());
			Assert.IsNotNull(folder["data"]["attributes"]);
			FileManagerMiddlewareTest.ValidateItemDataId(folder["data"]);

			var deletedFolder = await this.GetAsync($"{RichFileManagerConnectorUrl}?mode={ModeNames.Delete}&path=%2FNewFolder");
			Assert.IsNotNull(deletedFolder);
			Assert.AreEqual(folder["data"]["id"].Value<string>(), deletedFolder["data"]["id"].Value<string>());
			Assert.AreEqual(folder["data"]["type"].Value<string>(), deletedFolder["data"]["type"].Value<string>());
			Assert.AreEqual(folder["data"]["attributes"]["name"].Value<string>(), deletedFolder["data"]["attributes"]["name"].Value<string>());
			Assert.AreEqual(folder["data"]["attributes"]["path"].Value<string>(), deletedFolder["data"]["attributes"]["path"].Value<string>());
		}

		/// <summary>
		/// The test checks 'getfile' command.
		/// </summary>
		[Test]
		public async Task Invoke_GetFile_ReturnFileInformation()
		{
			var fileInfo = await this.GetAsync($"{RichFileManagerConnectorUrl}?mode={ModeNames.GetFile}&path=%2Ffile4.txt");

			Assert.IsNotNull(fileInfo);
			Assert.IsNotNull(fileInfo["data"]);
			Assert.IsNotNull(fileInfo["data"]["id"].Value<string>());
			Assert.IsNotNull(fileInfo["data"]["type"].Value<string>());
			Assert.AreEqual("file", fileInfo["data"]["type"].Value<string>());
			Assert.IsNotNull(fileInfo["data"]["attributes"]);
			FileManagerMiddlewareTest.ValidateItemDataId(fileInfo["data"]);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Validates the item data identifier.
		/// </summary>
		/// <param name="token">The token.</param>
		private static void ValidateItemDataId(JToken token)
		{
			Assert.IsTrue(token["id"].Value<string>().StartsWith("/"));

			if (token["type"].Value<string>().Equals("folder"))
			{
				Assert.IsTrue(token["id"].Value<string>().EndsWith("/"));
			}
		}

		private static void AssertCollection(object[] expected, object[] actual)
		{
			for (int index = 0; index < expected.Count(); index++)
			{
				Assert.AreEqual(expected[index], actual[index]);
			}
		}

		/// <summary>
		/// Gets the asynchronous.
		/// </summary>
		/// <param name="requestUri">The request URI.</param>
		/// <returns>The response data</returns>
		private async Task<JObject> GetAsync(string requestUri)
		{
			var response = await this.Client.GetAsync(requestUri);

			Assert.IsTrue(response.IsSuccessStatusCode);
			var content = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<JObject>(content);
		}

		#endregion
	}
}