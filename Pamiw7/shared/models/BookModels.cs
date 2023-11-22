using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pamiw6.Shared.Models;

namespace Pamiw6.Shared.Models
{
  public class AuthorDTO : Author
  {
    public int id { get; set; }
  }

  public class Author
  {
    public string firstName { get; set; }
    public string lastName { get; set; }

   public string fullName => $"{firstName} {lastName}";
  }

  public class BookDTO : Book
  {
    public int id { get; set; }

  }

  public class Book
  {
    public string title { get; set; }
    public AuthorDTO author { get; set; }
    public int pageCount { get; set; }
    public decimal price { get; set; }
    public string photoUrl { get; set; }
  }

  public class EditableBook
  {
    public int author_id { get; set; }
    public string title { get; set; }
    public int pageCount { get; set; }
    public decimal price { get; set; }
    public string photoUrl { get; set; }
  }

  public class BookEditData
  {
    public EditableBook Book { get; set; }
    public int? id { get; set; }
  }

}