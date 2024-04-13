using System.Text.Json;
using System.Text.Json.Serialization;

namespace webapi.Helpers.Validators;

public class EnumConverter<T> : JsonConverter<T>
    where T : Enum {
  public override T Read(ref Utf8JsonReader reader, Type typeToConvert,
                         JsonSerializerOptions? options) {
    if (reader.TokenType == JsonTokenType.Number) {
      // If the value is a number, try to parse it as an enum value
      if (reader.TryGetInt32(out int value) &&
          Enum.IsDefined(typeof(T), value)) {
        return (T)Enum.ToObject(typeof(T), value);
      }
    } else if (reader.TokenType == JsonTokenType.String) {
      // If the value is a string, try to parse it as an enum name
      var name = reader.GetString();
            if (Enum.TryParse(typeof(T), name, out object? result))
            {
              return (T)result;
            }
    }
    // If the value is not a valid enum int value or name, throw an exception
    throw new JsonException(
        $"Invalid value for {typeof(T).Name}: {reader.GetString()}");
  }

  // This method is not used but is required by the JsonConverter base class
  public override void Write(Utf8JsonWriter writer, T value,
                             JsonSerializerOptions options) {
    // Write the enum value as a string
    writer.WriteStringValue(value.ToString());
  }
}
