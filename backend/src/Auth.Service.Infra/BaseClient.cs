using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Web;

namespace Auth.Service.Infra;

public class BaseClient
{
    protected readonly HttpClient _httpClient;

    public BaseClient(HttpClient client)
    {
        _httpClient = client;
    }

    protected async Task<TOut> GetAsync<TOut>(Uri uri, Dictionary<string, string> headers = null, Dictionary<string, string> parameters = null)
    {
        var response = await ExecuteJson(HttpMethod.Get, uri, null, headers, parameters);

        ValidateResponse(response);

        try
        {
            return await TryDeserialize<TOut>(response);
        }
        catch (Exception ex)
        {
            throw new Exception("Invalid response type.", ex);
        }
    }

    protected async Task<TOut> PostAsync<TIn, TOut>(Uri uri, TIn content, Dictionary<string, string> headers = null, Dictionary<string, string> parameters = null)
    {
        var serialized = content is not null ? JsonSerializer.Serialize(content, new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        }) : null;

        var response = await ExecuteJson(HttpMethod.Post, uri, serialized, headers, parameters);

        ValidateResponse(response);

        try
        {
            return await TryDeserialize<TOut>(response);
        }
        catch (Exception ex)
        {
            throw new Exception("Invalid response type.", ex);
        }
    }

    protected async Task<TOut> PostAsync<TOut>(Uri uri, Dictionary<string, string> bodyXml, Dictionary<string, string> headers = null, Dictionary<string, string> parameters = null)
    {
        var response = await ExecuteXml(HttpMethod.Post, uri, bodyXml, headers, parameters);

        ValidateResponse(response);

        try
        {
            return await TryDeserialize<TOut>(response);
        }
        catch (Exception ex)
        {
            throw new Exception("Invalid response type.", ex);
        }
    }

    private async Task<TOut> TryDeserialize<TOut>(HttpResponseMessage response)
    {
        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            return default;

        var content = await response.Content.ReadAsStringAsync();

        TOut convertedResponse = JsonSerializer.Deserialize<TOut>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        return convertedResponse;
    }

    private void ValidateResponse(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Request was unsuccessful with status {response.StatusCode} ({(int)response.StatusCode}).");
    }

    private async Task<HttpResponseMessage> ExecuteJson(HttpMethod method, Uri uri, string bodyJson = null, Dictionary<string, string> headers = null, Dictionary<string, string> parameters = null)
    {
        var restRequest = new HttpRequestMessage()
        {
            Method = method,
        };

        if (!string.IsNullOrWhiteSpace(bodyJson))
            restRequest.Content = new StringContent(bodyJson, Encoding.UTF8, "application/json");

        AddHeaders(restRequest, headers);

        restRequest.RequestUri = MountUriWithParameters(uri, parameters);

        var response = await _httpClient.SendAsync(restRequest);

        return response;
    }

    private async Task<HttpResponseMessage> ExecuteXml(HttpMethod method, Uri uri, Dictionary<string, string> bodyXml = null, Dictionary<string, string> headers = null, Dictionary<string, string> parameters = null)
    {
        var restRequest = new HttpRequestMessage()
        {
            Method = method,
        };

        if (bodyXml is not null)
            restRequest.Content = new FormUrlEncodedContent(bodyXml);

        AddHeaders(restRequest, headers);

        restRequest.RequestUri = MountUriWithParameters(uri, parameters);

        var response = await _httpClient.SendAsync(restRequest);

        return response;
    }

    private void AddHeaders(HttpRequestMessage restRequest, Dictionary<string, string> headers)
    {
        if (headers is not null && headers.Count > 0)
        {
            foreach (var header in headers.Keys)
            {
                restRequest.Headers.Add(header, headers[header]);
            }
        }
    }

    private Uri MountUriWithParameters(Uri uri, Dictionary<string, string> parameters)
    {
        var uriBuilder = new UriBuilder(uri);

        var query = HttpUtility.ParseQueryString(uriBuilder.Query);

        if (parameters is not null && parameters.Count > 0)
        {
            foreach (var parameter in parameters.Keys)
            {
                query.Add(parameter, parameters[parameter]);
            }
        }

        uriBuilder.Query = query.ToString();

        return uriBuilder.Uri;
    }
}