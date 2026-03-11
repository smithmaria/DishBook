namespace DishBook;

public partial class AddPage : ContentPage
{
	public AddPage()
	{
		InitializeComponent();
	}

    private async void ViewClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//View");
    }
}