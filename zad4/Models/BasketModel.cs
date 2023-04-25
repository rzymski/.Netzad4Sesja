using System.Text.Json.Serialization;
using System.Text.Json;
using System.Xml.Linq;

namespace zad4.Models
{
    public class BasketModel
    {
        public Dictionary<Product, int> Products { get; set; }
        public BasketModel() 
        {
            Products = new Dictionary<Product, int>();
        }
        public override string ToString()
        {
            string p = "";
            if(Products != null)
            {
                foreach (var item in Products)
                {
                    if(item.Key != null)
                        p += item.Key.ToString() + " - " + item.Value + " razy\n";
                }
            }
            else
            {
                p = "brak produktów w koszyku";
            }
            return "Products in Basket:\n"+p;
        }
    }

    class ProductDictionaryKeyConverter : JsonConverter<Dictionary<Product, int>>
    {
        public static JsonSerializerOptions jsonSerializeConventorOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new ProductDictionaryKeyConverter() }
        };

        public override Dictionary<Product, int> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var products = new Dictionary<Product, int>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException($"Expected a property name but got {reader.TokenType}");
                }

                string productName = reader.GetString();
                List<string> productValues = productName.Split("\0").ToList();
                var product = new Product(int.Parse(productValues[0]), productValues[1], decimal.Parse(productValues[2]));

                if (!reader.Read())
                {
                    throw new JsonException($"Unexpected end of JSON input");
                }

                if (reader.TokenType != JsonTokenType.Number)
                {
                    throw new JsonException($"Expected a number but got {reader.TokenType}");
                }

                int quantity = reader.GetInt32();
                products.Add(product, quantity);
            }

            return products;
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<Product, int> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            foreach (var kvp in value)
            {
                writer.WritePropertyName(kvp.Key.id + "\0" + kvp.Key.name + "\0" + kvp.Key.price);
                writer.WriteNumberValue(kvp.Value);
            }

            writer.WriteEndObject();
        }
    }
}
