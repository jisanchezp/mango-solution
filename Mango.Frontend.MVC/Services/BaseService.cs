
using Mango.Frontend.MVC.Enums;
using Mango.Frontend.MVC.Helper;
using Mango.Frontend.MVC.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Mango.Frontend.MVC.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {
                ResponseDto? responseDto = new() { IsSuccess = false };

                HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new();
                message.RequestUri = new Uri(requestDto.Url);

                if (requestDto.Data is not null)
                {
                    message.Content = new StringContent(JsonSerializer.Serialize(requestDto.Data));
                    message.Content.Headers.ContentType = new("application/json", "utf-8");
                }

                HttpResponseMessage? apiResponse = null;

                switch (requestDto.HttpVerb)
                {
                    case ApiTypeEnum.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case ApiTypeEnum.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiTypeEnum.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiTypeEnum.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        responseDto.Message = "Not Found";
                        break;
                    case HttpStatusCode.Forbidden:
                        responseDto.Message = "Access Denied";
                        break;
                    case HttpStatusCode.Unauthorized:
                        responseDto.Message = "Unauthorized";
                        break;
                    case HttpStatusCode.InternalServerError:
                        responseDto.Message = "Internal Server Error";
                        break;
                    default:
                        string content = await apiResponse.Content.ReadAsStringAsync();
                        responseDto = JsonHelper.DeserializeCaseInsensitive<ResponseDto?>(content);
                        break;
                }

                return responseDto;
            }
            catch (Exception ex)
            {
                return new() { Message = ex.Message, IsSuccess = false };
            }
        }
    }
}
