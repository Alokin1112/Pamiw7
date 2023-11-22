using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pamiw6.Shared.Models;

namespace Pamiw6.Shared.Models
{

  public class ServiceResponse<T>
  {
    public T data { get; set; }
    public bool isSuccess { get; set; }
    public string message { get; set; }
  }

  public class Pagination
  {
    public int page { get; set; }
    public int take { get; set; }
  }

  public class PaginableResponse<T>
  {
    public T data { get; set; }
    public int pageCount { get; set; }
  }

}