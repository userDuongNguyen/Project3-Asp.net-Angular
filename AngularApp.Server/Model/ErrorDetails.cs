using System.Text.Json;

namespace AngularApp.Server.Model
{
    public class ErrorDetails
    {
#nullable enable
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
