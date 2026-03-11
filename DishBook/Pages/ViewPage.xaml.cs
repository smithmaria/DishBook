namespace DishBook;

public partial class ViewPage : ContentPage
{
	public ViewPage()
	{
		InitializeComponent();
	}

    private async void EditClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Edit");
    }

    private async void AddClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Add");
    }

    private async void SearchClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Search");
    }
}