
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using Pamiw6.Shared.Configuration;
using Pamiw6.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Pamiw6.Shared.Services.AuthorService
{
  public class AuthorService
  {

    private readonly HttpClient _httpClient;
    private readonly AppSettings _appSettings;
    public AuthorService(HttpClient httpClient, AppSettings appSettings)
    {
      _httpClient = httpClient;
      _appSettings = appSettings;
    }

    public async Task<ServiceResponse<List<AuthorDTO>>> GetAuthors()
    {
      try
      {
        var response = await _httpClient.GetAsync(_appSettings.BaseAPIUrl + _appSettings.AuthorEndpoint.AllAuthorsEndpoint);
        if (!response.IsSuccessStatusCode)
          return new ServiceResponse<List<AuthorDTO>>
          {
            isSuccess = false,
            message = "HTTP request failed"
          };

        var json = await response.Content.ReadAsStringAsync();
        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<List<AuthorDTO>>>();

        return result;
      }
      catch (Exception)
      {
        return new ServiceResponse<List<AuthorDTO>>
        {
          isSuccess = false,
          message = "Network error"
        };
      }


    }

  }
}
