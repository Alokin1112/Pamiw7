using Pamiw7.ViewModels;

namespace Pamiw7
{
    public partial class BookDetailsPage : ContentPage
    {
        public BookDetailsPage(BookDetailsViewModel bookDetailsViewModel)
        {
            InitializeComponent();
            BindingContext = bookDetailsViewModel;
        }
    }

}
