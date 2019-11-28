using System;
using MasDen.RichFileManager.DotNetConnector.Components.Converters;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace MasDen.RichFileManager.DotNetConnector.Test.Components.Converters
{
	[TestFixture]
	public class BoolToIntConverterTest
	{
		#region Private Fields

		private BoolToIntConverter converter;

		#endregion

		#region Test Context

		[SetUp]
		public void Setup()
		{
			this.converter = new BoolToIntConverter();
		}

		#endregion

		#region Tests

		[TestCase(typeof(bool), true)]
		[TestCase(typeof(int), false)]
		[TestCase(typeof(string), false)]
		[TestCase(typeof(BoolToIntConverter), false)]
		public void CanConvertTest(Type type, bool expected)
		{
			// Act & Assert
			Assert.AreEqual(expected, this.converter.CanConvert(type));
		}

		[TestCase("1", true)]
		[TestCase("0", false)]
		[TestCase("2", false)]
		[TestCase("abc", false)]
		public void ReadJsonTest(string value, bool expectedResult)
		{
			// Arrange
			var reader = Substitute.For<JsonReader>();
			reader.Value.Returns(value);

			// Act
			var result = this.converter.ReadJson(reader, typeof(bool), null, null);

			// Assert
			Assert.AreEqual(expectedResult, result);
		}

		[TestCase(true, 1)]
		[TestCase(false, 0)]
		public void WriteJsonTest(bool value, int expected)
		{
			// Arrange
			var writer = Substitute.For<JsonWriter>();

			// Act
			this.converter.WriteJson(writer, value, null);

			// Assert
			writer.Received().WriteValue(expected);
		}

		#endregion
	}
}