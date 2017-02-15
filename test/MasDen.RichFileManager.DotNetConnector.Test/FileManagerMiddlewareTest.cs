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
	using Entities;
	using Entities.Enumerations;
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

		[Test]
		public async Task Invoke_InitiateCommand_ReturnConfiguration()
		{
			var response = await this.Client.GetAsync($"{RichFileManagerConnectorUrl}?mode={CommandType.Initiate.ToString().ToLowerInvariant()}");
			Assert.IsTrue(response.IsSuccessStatusCode);
			var content = await response.Content.ReadAsStringAsync();

			JObject result = JsonConvert.DeserializeObject<JObject>(content);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result["data"]["attributes"]["config"]["options"]);
			Assert.AreEqual("en", result["data"]["attributes"]["config"]["options"]["culture"].ToString());
			Assert.AreEqual("flat-dark", result["data"]["attributes"]["config"]["options"]["theme"].ToString());
			Assert.AreEqual("grid", result["data"]["attributes"]["config"]["options"]["defaultViewMode"].ToString());
			Assert.IsTrue(result["data"]["attributes"]["config"]["options"]["localizeGUI"].Value<bool>());
			Assert.IsTrue(result["data"]["attributes"]["config"]["options"]["showFullPath"].Value<bool>());
			Assert.IsTrue(result["data"]["attributes"]["config"]["options"]["showTitleAttr"].Value<bool>());
			Assert.IsTrue(result["data"]["attributes"]["config"]["options"]["showConfirmation"].Value<bool>());
			Assert.IsTrue(result["data"]["attributes"]["config"]["options"]["browseOnly"].Value<bool>());
			Assert.IsTrue(result["data"]["attributes"]["config"]["options"]["clipboard"].Value<bool>());
			Assert.IsTrue(result["data"]["attributes"]["config"]["options"]["searchBox"].Value<bool>());
			Assert.IsTrue(result["data"]["attributes"]["config"]["options"]["listFiles"].Value<bool>());
			Assert.AreEqual("NAME_ASC", result["data"]["attributes"]["config"]["options"]["fileSorting"].ToString());
			Assert.AreEqual("bottom", result["data"]["attributes"]["config"]["options"]["folderPosition"].ToString());
			Assert.IsTrue(result["data"]["attributes"]["config"]["options"]["quickSelect"].Value<bool>());
			Assert.IsTrue(result["data"]["attributes"]["config"]["options"]["charsLatinOnly"].Value<bool>());
			Assert.AreEqual(100.0, result["data"]["attributes"]["config"]["options"]["splitterWidth"].Value<double>());
			Assert.AreEqual(200.0, result["data"]["attributes"]["config"]["options"]["splitterMinWidth"].Value<double>());
			Assert.IsTrue(result["data"]["attributes"]["config"]["options"]["logger"].Value<bool>());
			Assert.AreEqual("capabilities", result["data"]["attributes"]["config"]["options"]["capabilities"].ToString());

			Assert.IsTrue(result["data"]["attributes"]["config"]["security"]["allowFolderDownload"].Value<bool>());
			Assert.IsTrue(result["data"]["attributes"]["config"]["security"]["allowChangeExtensions"].Value<bool>());
			Assert.IsTrue(result["data"]["attributes"]["config"]["security"]["allowNoExtension"].Value<bool>());
			Assert.IsTrue(result["data"]["attributes"]["config"]["security"]["normalizeFilename"].Value<bool>());

			Assert.IsTrue(result["data"]["attributes"]["config"]["upload"]["multiple"].Value<bool>());
			Assert.AreEqual(3, result["data"]["attributes"]["config"]["upload"]["maxNumberOfFiles"].Value<int>());
			Assert.AreEqual("files", result["data"]["attributes"]["config"]["upload"]["paramName"].ToString());
			Assert.IsTrue(result["data"]["attributes"]["config"]["upload"]["chunkSize"].Value<bool>());
			Assert.AreEqual(10, result["data"]["attributes"]["config"]["upload"]["fileSizeLimit"].Value<int>());
			Assert.AreEqual("DISALLOW_ALL", result["data"]["attributes"]["config"]["upload"]["policy"].ToString());
			Assert.AreEqual("restrictions", result["data"]["attributes"]["config"]["upload"]["restrictions"].ToString());
		}

		#endregion
	}
}