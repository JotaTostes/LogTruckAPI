using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogTruck.Shared.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public T? Content { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        public ApiResponse()
        {
        }

        public ApiResponse(bool success, int statusCode, T? content = default, IEnumerable<string>? errors = null)
        {
            Success = success;
            StatusCode = statusCode;
            Content = content;
            Errors = errors;
        }

        public static ApiResponse<T> SuccessResponse(T? content, int statusCode = 200)
        {
            return new ApiResponse<T>
            {
                Success = true,
                StatusCode = statusCode,
                Content = content
            };
        }

        public static ApiResponse<T> ErrorResponse(IEnumerable<string> errors, int statusCode = 400)
        {
            return new ApiResponse<T>
            {
                Success = false,
                StatusCode = statusCode,
                Errors = errors
            };
        }
    }

    public class ApiResponse : ApiResponse<object>
    {
        public static ApiResponse SuccessNoContent(int statusCode = 204)
        {
            return new ApiResponse
            {
                Success = true,
                StatusCode = statusCode
            };
        }
    }
}
