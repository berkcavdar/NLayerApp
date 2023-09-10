using System.Text.Json.Serialization;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDTO<T>
    {
        public T Data { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        public List<String> Errors { get; set; }

        // Static Factory Method
        public static CustomResponseDTO<T> Success(int statusCode, T data)
        {
            return new CustomResponseDTO<T> { StatusCode = statusCode, Data = data };
        }
        // Static Factory Method
        public static CustomResponseDTO<T> Success(int statusCode)
        {
            return new CustomResponseDTO<T> { StatusCode = statusCode };
        }
        // Static Factory Method
        public static CustomResponseDTO<T> Fail(int statusCode, List<String> errors)
        {
            return new CustomResponseDTO<T> { StatusCode = statusCode, Errors = errors };
        }
        // Static Factory Method
        public static CustomResponseDTO<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDTO<T> { StatusCode = statusCode, Errors = new List<string> { error } };
        }
    }
}
