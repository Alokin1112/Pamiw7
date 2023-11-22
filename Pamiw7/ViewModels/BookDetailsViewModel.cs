using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Networking;
using Pamiw6.Shared.Models;
using Pamiw6.Shared.Services.AuthorService;
using Pamiw6.Shared.Services.BookService;
using Pamiw7.shared.interfaces;

namespace Pamiw7.ViewModels
{
    [QueryProperty(nameof(BookDTO), nameof(BookDTO))]
    [QueryProperty(nameof(BookViewModel), nameof(BookViewModel))]
    public partial class BookDetailsViewModel : ObservableObject
    {

        private readonly IConnectivity _connectivity;
        private readonly BookService _bookService;
        private readonly AuthorService _authorService;
        private readonly IMessageDialogService _messageDialogService;
        private BookViewModel _bookViewModel;
        public ObservableCollection<Author> Authors { get; set; }

        public BookDetailsViewModel(BookService bookService, IMessageDialogService messageDialogService,AuthorService authorService, IConnectivity connectivity)
        {
            _bookService = bookService;
            _messageDialogService = messageDialogService;
            _authorService = authorService;
            _connectivity = connectivity;
            Authors = new ObservableCollection<Author>();
            GetAuthors();
        }

        private async Task GetAuthors()
        {
            Authors.Clear();
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }
            var authorsResult = await _authorService.GetAuthors();
            if (authorsResult.isSuccess)
            {
                foreach (var p in authorsResult.data)
                {
                    Authors.Add(p);
                }
            }
            else
            {
                _messageDialogService.ShowMessage(authorsResult.message);
            }
        }

        public BookViewModel BookViewModel
        {
            get
            {
                return _bookViewModel;
            }
            set
            {
                _bookViewModel = value;
            }
        }

        [ObservableProperty]
        BookDTO bookDTO;

        public async Task DeleteBook()
        {
            await _bookService.deleteBook(bookDTO.id);
            await BookViewModel.GetBooks();
        }


        public async Task CreateBook()
        {

            var newBook = new EditableBook()
            {
                title = bookDTO.title,
                author_id=bookDTO.author.id,
                pageCount= bookDTO.pageCount,
                photoUrl=bookDTO.photoUrl,
                price=bookDTO.price,
            };

            var result = await _bookService.AddBook(newBook);
            if (result.isSuccess)
            {
                await BookViewModel.GetBooks();
            }
           
            else
                _messageDialogService.ShowMessage(result.message);
        }


        public async Task UpdateBook()
        {
            var newBook = new EditableBook()
            {
                title = bookDTO.title,
                author_id = bookDTO.author.id,
                pageCount = bookDTO.pageCount,
                photoUrl = bookDTO.photoUrl,
                price = bookDTO.price,
            };

            await _bookService.putBook(newBook,bookDTO.id);
            await BookViewModel.GetBooks();
        }



        [RelayCommand]
        public async Task Save()
        {
            if (bookDTO.id == 0)
            {
                CreateBook();
                await Shell.Current.GoToAsync("../", true);

            }
            else
            {
                UpdateBook();
                await Shell.Current.GoToAsync("../", true);
            }

        }

        [RelayCommand]
        public async Task Delete()
        {
            DeleteBook();
            await Shell.Current.GoToAsync("../", true);
        }

    }
}
