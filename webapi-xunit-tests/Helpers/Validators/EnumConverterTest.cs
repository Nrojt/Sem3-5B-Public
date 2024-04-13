using System.Text;
using System.Text.Json;
using JetBrains.Annotations;
using webapi_unit_tests.Helpers.Enums;
using webapi.Helpers.Validators;

namespace webapi_unit_tests.Helpers.Validators;

// Testing the EnumConverter class to ensure that it can convert a json string
// number to an enum value
[TestSubject(typeof(EnumConverter<Color>))]
public class EnumConverterTest {
  // test that the EnumConverter can convert a json string number to an enum
  // value within the range of the enum
  [Theory]
  [InlineData("0", Color.Red)]
  [InlineData("1", Color.Green)]
  [InlineData("2", Color.Blue)]
  public void String_Number_To_Enum_Inside_Range(string json, Color expected) {
    // Arrange
    // Create a reader for the JSON string
    var utf8Bytes = Encoding.UTF8.GetBytes(json);

    // Create a reader for the JSON string
    var reader = new Utf8JsonReader(utf8Bytes);
    reader.Read(); // Ensure the reader is positioned at the start of the JSON
    // content.

    // Act
    var actual = ConvertJsonToColor(ref reader);

    // Assert
    Assert.Equal(expected, actual);
  }

  // test to ensure that the EnumConverter cannot convert a json string number
  // to an enum value outside the range of the enum
  [Theory]
  [InlineData("100")]
  [InlineData("-1")]
  public void String_Number_Outside_Enum_Range(string json) {
    // Arrange
    var utf8Bytes = Encoding.UTF8.GetBytes(json);
    var reader = new Utf8JsonReader(utf8Bytes);
    reader.Read(); // Ensure the reader is positioned at the start of the JSON
    // content.

    // Act and Assert
    try {
      ConvertJsonToColor(ref reader);

      // If no exception is thrown, the test fails
      Assert.True(false, "Expected Exception");
    } catch (InvalidOperationException ignored) {
      // If an exception is thrown, the test passes
      Assert.True(true);
    }
  }

  // Helper method to call the converter with a ref parameter
  private static Color ConvertJsonToColor(ref Utf8JsonReader reader) {
    // Create a converter for the Color enum, the class we are testing. Color
    // enum is a test enum
    var converter = new EnumConverter<Color>();
    return converter.Read(ref reader, typeof(Color), null);
  }
}
