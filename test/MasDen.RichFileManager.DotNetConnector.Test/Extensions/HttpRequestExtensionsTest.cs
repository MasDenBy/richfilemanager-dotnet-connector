using System;

using MasDen.RichFileManager.DotNetConnector.Constants;
using MasDen.RichFileManager.DotNetConnector.Extensions;

using NUnit.Framework;

namespace MasDen.RichFileManager.DotNetConnector.Test.Extensions
{
	[TestFixture]
	public class HttpRequestExtensionsTest
	{
		#region Constants

		private const string GetMethodName = "GET";
		private const string PostMethodName = "POST";
		private const string TestValue = "value";

		#endregion

		#region Tests

		[TestCase(GetMethodName)]
		[TestCase(PostMethodName)]
		public void GetPathIfKeyDoesNotExistsShouldThrowExceptionTest(string method)
		{
			// Arrange
			var httpRequest = TestHelpers.CreateHttpRequestMock(method);

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() => httpRequest.GetPath());
		}

		[Test]
		public void GetPathIfKeyExistsInFormShouldReturnValueTest()
		{
			// Arrange
			var httpRequest = TestHelpers.CreateHttpRequestMock(PostMethodName);
			TestHelpers.AddFormKey(httpRequest.Form, RequestKeys.Path, TestValue);

			// Act & Assert
			Assert.AreEqual(TestValue, httpRequest.GetPath());
		}

		[Test]
		public void GetPathIfKeyExistsInQueryShouldReturnValueTest()
		{
			// Arrange
			var httpRequest = TestHelpers.CreateHttpRequestMock(GetMethodName);
			TestHelpers.AddQueryKey(httpRequest.Query, RequestKeys.Path, TestValue);

			// Act & Assert
			Assert.AreEqual(TestValue, httpRequest.GetPath());
		}

		[Test]
		public void GetContentIfKeyDoesNotExistsShouldThrowExceptionTest()
		{
			// Arrange
			var httpRequest = TestHelpers.CreateHttpRequestMock(PostMethodName);

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() => httpRequest.GetContent());
		}

		[Test]
		public void GetContentIfKeyExistsShouldReturnValueTest()
		{
			// Arrange
			var httpRequest = TestHelpers.CreateHttpRequestMock(PostMethodName);
			TestHelpers.AddFormKey(httpRequest.Form, RequestKeys.Content, TestValue);

			// Act & Assert
			Assert.AreEqual(TestValue, httpRequest.GetContent());
		}

		[TestCase(GetMethodName)]
		[TestCase(PostMethodName)]
		public void GetModeIfKeyDoesNotExistsShouldThrowExceptionTest(string method)
		{
			// Arrange
			var httpRequest = TestHelpers.CreateHttpRequestMock(method);

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() => httpRequest.GetMode());
		}

		[Test]
		public void GetModeIfKeyExistsInFormShouldReturnValueTest()
		{
			// Arrange
			var httpRequest = TestHelpers.CreateHttpRequestMock(PostMethodName);
			TestHelpers.AddFormKey(httpRequest.Form, RequestKeys.Mode, TestValue);

			// Act & Assert
			Assert.AreEqual(TestValue, httpRequest.GetMode());
		}

		[Test]
		public void GetModeIfKeyExistsInQueryShouldReturnValueTest()
		{
			// Arrange
			var httpRequest = TestHelpers.CreateHttpRequestMock(GetMethodName);
			TestHelpers.AddQueryKey(httpRequest.Query, RequestKeys.Mode, TestValue);

			// Act & Assert
			Assert.AreEqual(TestValue, httpRequest.GetMode());
		}

		[Test]
		public void GetNameIfKeyDoesNotExistsShouldThrowExceptionTest()
		{
			// Arrange
			var httpRequest = TestHelpers.CreateHttpRequestMock(GetMethodName);

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() => httpRequest.GetName());
		}

		[Test]
		public void GetNameIfKeyExistsShouldReturnValueTest()
		{
			// Arrange
			var httpRequest = TestHelpers.CreateHttpRequestMock(GetMethodName);
			TestHelpers.AddQueryKey(httpRequest.Query, RequestKeys.Name, TestValue);

			// Act & Assert
			Assert.AreEqual(TestValue, httpRequest.GetName());
		}

		[Test]
		public void GetOldIfKeyDoesNotExistsShouldThrowExceptionTest()
		{
			// Arrange
			var httpRequest = TestHelpers.CreateHttpRequestMock(GetMethodName);

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() => httpRequest.GetOld());
		}

		[Test]
		public void GetOldIfKeyExistsShouldReturnValueTest()
		{
			// Arrange
			var httpRequest = TestHelpers.CreateHttpRequestMock(GetMethodName);
			TestHelpers.AddQueryKey(httpRequest.Query, RequestKeys.Old, TestValue);

			// Act & Assert
			Assert.AreEqual(TestValue, httpRequest.GetOld());
		}

		[Test]
		public void GetNewIfKeyDoesNotExistsShouldThrowExceptionTest()
		{
			// Arrange
			var httpRequest = TestHelpers.CreateHttpRequestMock(GetMethodName);

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() => httpRequest.GetNew());
		}

		[Test]
		public void GetNewIfKeyExistsShouldReturnValueTest()
		{
			// Arrange
			var httpRequest = TestHelpers.CreateHttpRequestMock(GetMethodName);
			TestHelpers.AddQueryKey(httpRequest.Query, RequestKeys.New, TestValue);

			// Act & Assert
			Assert.AreEqual(TestValue, httpRequest.GetNew());
		}

		[Test]
		public void GetSourceIfKeyDoesNotExistsShouldThrowExceptionTest()
		{
			// Arrange
			var httpRequest = TestHelpers.CreateHttpRequestMock(GetMethodName);

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() => httpRequest.GetSource());
		}

		[Test]
		public void GetSourceIfKeyExistsShouldReturnValueTest()
		{
			// Arrange
			var httpRequest = TestHelpers.CreateHttpRequestMock(GetMethodName);
			TestHelpers.AddQueryKey(httpRequest.Query, RequestKeys.Source, TestValue);

			// Act & Assert
			Assert.AreEqual(TestValue, httpRequest.GetSource());
		}

		[Test]
		public void GetTargetIfKeyDoesNotExistsShouldThrowExceptionTest()
		{
			// Arrange
			var httpRequest = TestHelpers.CreateHttpRequestMock(GetMethodName);

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() => httpRequest.GetTarget());
		}

		[Test]
		public void GetTargetIfKeyExistsShouldReturnValueTest()
		{
			// Arrange
			var httpRequest = TestHelpers.CreateHttpRequestMock(GetMethodName);
			TestHelpers.AddQueryKey(httpRequest.Query, RequestKeys.Target, TestValue);

			// Act & Assert
			Assert.AreEqual(TestValue, httpRequest.GetTarget());
		}

		#endregion
	}
}