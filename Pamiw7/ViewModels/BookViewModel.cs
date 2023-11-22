using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pamiw6.Shared.Models;
using Pamiw6.Shared.Services.BookService;
using Pamiw7.shared.interfaces;

namespace Pamiw7.ViewModels
{
    public partial class BookViewModel: ObservableObject
    {
        private readonly IConnectivity _connectivity;
        private readonly BookService _bookService;
        private readonly IMessageDialogService _messageDialogService;
        public ObservableCollection<BookDTO> Books { get; set; }
        [ObservableProperty]
        private BookDTO selectedBook;
        private readonly BookDetailsViewModel _bookDetailsViewModel;

        public 
            BookViewModel(BookService bookService, IMessageDialogService messageDialogService,
    IConnectivity connectivity, BookDetailsViewModel bookDetailsViewModel)
        {
            _messageDialogService = messageDialogService;
            _bookService = bookService;
            _bookDetailsViewModel = bookDetailsViewModel;
            _connectivity = connectivity; 
            Books = new ObservableCollection<BookDTO>();
            GetBooks();
        }

        public async Task GetBooks()
        {
            Books.Clear();
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }
            var pagination = new Pagination()
            {
                page = 0,
                take = 999,
            };
            var bookResult = await _bookService.GetBooks(pagination);
            if (bookResult.isSuccess)
            {
                foreach (var p in bookResult.data.data)
                {
                    Books.Add(p);
                }
            }
            else
            {
                _messageDialogService.ShowMessage(bookResult.message);
            }
        }


        [RelayCommand]
        public async Task ShowDetails(BookDTO book)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            await Shell.Current.GoToAsync(nameof(BookDetailsPage), true, new Dictionary<string, object>
            {
                {nameof(BookDTO), book },
                {nameof(BookViewModel), this }
            });
            SelectedBook = book;
        }
        
        [RelayCommand]
        public async Task New()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            SelectedBook = new BookDTO();
            await Shell.Current.GoToAsync(nameof(BookDetailsPage), true, new Dictionary<string, object>
            {
                {nameof(BookDTO), SelectedBook },
                {nameof(BookViewModel), this }
            });
        }
        
    }
}
