using Pamiw7.ViewModels;

namespace Pamiw7
{
    public partial class MainPage : ContentPage
    {
        public MainPage(BookViewModel bookViewModel)
        {
            InitializeComponent();
            BindingContext = bookViewModel;
        }
    }

}
