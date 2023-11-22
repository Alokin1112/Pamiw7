
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

namespace Pamiw6.Shared.Services.BookService
{
  public class BookService
  {

    private readonly HttpClient _httpClient;
    private readonly AppSettings _appSettings;
    public BookService(HttpClient httpClient, AppSettings appSettings)
    {
      _httpClient = httpClient;
      _appSettings = appSettings;
    }

    public async Task<ServiceResponse<PaginableResponse<List<BookDTO>>>> GetBooks(Pagination pagination)
    {
      try
      {
        var response = await _httpClient.GetAsync(_appSettings.BaseAPIUrl + _appSettings.BookEndpoint.AllBooksEndpoint + "?page=" + pagination.page + "&take=" + pagination.take);
        if (!response.IsSuccessStatusCode)
          return new ServiceResponse<PaginableResponse<List<BookDTO>>>
          {
            isSuccess = false,
            message = "HTTP request failed"
          };

        var json = await response.Content.ReadAsStringAsync();
        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<PaginableResponse<List<BookDTO>>>>();

        return result;
      }
      catch (Exception)
      {
        return new ServiceResponse<PaginableResponse<List<BookDTO>>>
        {
          isSuccess = false,
          message = "Network error"
        };
      }
    }

    public async Task<ServiceResponse<BookDTO>> GetBookById(int id)
    {
      try
      {
        var response = await _httpClient.GetAsync(_appSettings.BaseAPIUrl + _appSettings.BookEndpoint.SpecificBookEndpoint + id);
        if (!response.IsSuccessStatusCode)
          return new ServiceResponse<BookDTO>()
          {
            isSuccess = false,
            message = "HTTP request failed"
          };

        var json = await response.Content.ReadAsStringAsync();
        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<BookDTO>>();

        return result;
      }
      catch (Exception)
      {
        return new ServiceResponse<BookDTO>
        {
          isSuccess = false,
          message = "Network error"
        };
      }
    }

    public async Task<ServiceResponse<BookDTO>> AddBook(EditableBook book)
    {
      try
      {
        var response = await _httpClient.PostAsJsonAsync(_appSettings.BaseAPIUrl + _appSettings.BookEndpoint.AllBooksEndpoint, book);
        if (!response.IsSuccessStatusCode)
          return new ServiceResponse<BookDTO>
          {
            isSuccess = false,
            message = "HTTP request failed"
          };

        var json = await response.Content.ReadAsStringAsync();
        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<BookDTO>>();

        return result;
      }
      catch (Exception)
      {
        return new ServiceResponse<BookDTO>
        {
          isSuccess = false,
          message = "Network error"
        };
      }
    }

    public async Task<ServiceResponse<BookDTO>> deleteBook(int id)
    {
      try
      {
        var response = await _httpClient.DeleteAsync(_appSettings.BaseAPIUrl + _appSettings.BookEndpoint.SpecificBookEndpoint + id);
        if (!response.IsSuccessStatusCode)
          return new ServiceResponse<BookDTO>
          {
            isSuccess = false,
            message = "HTTP request failed"
          };

        var json = await response.Content.ReadAsStringAsync();
        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<BookDTO>>();

        return result;
      }
      catch (Exception)
      {
        return new ServiceResponse<BookDTO>
        {
          isSuccess = false,
          message = "Network error"
        };
      }
    }

    public async Task<ServiceResponse<BookDTO>> putBook(EditableBook book, int id)
    {
      try
      {
        var response = await _httpClient.PutAsJsonAsync(_appSettings.BaseAPIUrl + _appSettings.BookEndpoint.SpecificBookEndpoint + id, book);
        if (!response.IsSuccessStatusCode)
          return new ServiceResponse<BookDTO>
          {
            isSuccess = false,
            message = "HTTP request failed"
          };

        var json = await response.Content.ReadAsStringAsync();
        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<BookDTO>>();

        return result;
      }
      catch (Exception)
      {
        return new ServiceResponse<BookDTO>
        {
          isSuccess = false,
          message = "Network error"
        };
      }
    }

  }
}
