namespace DishBook;

public partial class SearchPage : ContentPage
{
	public SearchPage()
	{
		InitializeComponent();
	}

    private async void ViewClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//View");
    }
}