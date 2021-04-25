using DigiRega.Shared.Dto.Entry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DigiRega.Shared
{
    /// <summary>
    /// Converts <see cref="GetEntryDto"/> or any of its subclasses to JSON.
    /// Polmorphism is supported and properties of derived types are kept.
    /// </summary>
    // See https://stackoverflow.com/questions/58074304/is-polymorphic-deserialization-possible-in-system-text-json
    public class GetEntryDtoJsonConverter : JsonConverter<GetEntryDto>
    {
        private static readonly JsonSerializerOptions jsonOptions = new(JsonSerializerDefaults.Web);
        
        // Can convert any class that is an GetEntryDto including derived types.
        public override bool CanConvert(Type typeToConvert) =>
            typeof(GetEntryDto).IsAssignableFrom(typeToConvert);

        public override GetEntryDto? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {            
            // Make sure we are reading an object and not a list or whatever.
            if (reader.TokenType != JsonTokenType.StartObject)
            { throw new JsonException(); }

            // Save the current depth in the JSON tree. This is used to determine if we are done.
            var startingDepth = reader.CurrentDepth;

            // Read the next part, make sure it is a property and it is the type discriminator.
            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName
                || reader.GetString() != "type")
            { throw new JsonException("No type discriminator found."); }

            // Read the property's value and make sure it is a string.
            reader.Read();
            if (reader.TokenType != JsonTokenType.String)
            { throw new JsonException("Type discriminator not specified."); }

            // Read the discriminator value and find the corresponding target type.
            Type type = reader.GetString() switch
            {
                nameof(GetCrewChangeDto) => typeof(GetCrewChangeDto),
                nameof(GetLateEntryDto) => typeof(GetLateEntryDto),
                nameof(GetWithdrawalDto) => typeof(GetWithdrawalDto),
                nameof(GetEntryDto) => typeof(GetEntryDto),
                _ => throw new JsonException("Unknown type discriminator.")
            };

            // Read the next part and make sure it is the entry property.
            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName
                || reader.GetString() != "entry")
            { throw new JsonException("No entry data found."); }

            // Read the next part and make sure it is the start of an object.
            // The entry data is contained in a nested object.
            reader.Read();
            if (reader.TokenType != JsonTokenType.StartObject)
            { throw new JsonException("Unexpected entry data format."); }

            // Use the default JSON deserializer to create an object of the target type from the remaining reader data.
            // Save it boxed as GetEntryDto.
            var entry = JsonSerializer.Deserialize(ref reader, type, jsonOptions) as GetEntryDto;

            // The reader must read the whole JSON tree to finish successfully.
            while (reader.Read())
            {
                // We are done if we found the end of the JSON object at the depth we started from.
                if (reader.TokenType == JsonTokenType.EndObject
                    && reader.CurrentDepth == startingDepth)
                {
                    return entry;
                }
            }

            // If we hit this, the reader has stopped due to missing data without a proper ending.
            throw new JsonException("Truncated data.");
        }

        public override void Write(Utf8JsonWriter writer, GetEntryDto value, JsonSerializerOptions options)
        {
            // Start writing a JSON object.
            writer.WriteStartObject();

            // Put a type discriminator into the JSON object.
            // Note that this is the first property in the object and it is a string.
            // Deserialization relies on finding this property in first position and as string.
            writer.WriteString("type", value.GetType().Name);

            // Write the actual object using the default JSON serializer.
            // Since the serializer will produce a complete object, we need to wrap it into a JSON property.
            // TODO: Would be nice if this did not require another level of nesting.
            writer.WritePropertyName("entry");
            // GetType() will return the actual type even if it is boxed in a more general one.
            JsonSerializer.Serialize(writer, value, value.GetType(), jsonOptions);
            
            // End writing the current JSON object.
            writer.WriteEndObject();
        }
    }
}
