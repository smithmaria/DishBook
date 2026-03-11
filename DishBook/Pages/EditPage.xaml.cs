namespace DishBook;

public partial class EditPage : ContentPage
{
	public EditPage()
	{
		InitializeComponent();
	}

    private async void ViewClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//View");
    }
}