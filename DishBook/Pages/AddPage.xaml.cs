namespace DishBook;

public partial class AddPage : ContentPage
{
    public AddPage()
    {
        InitializeComponent();
        AddIngredientRow();
    }

    private void AddIngredientRow()
    {
        var grid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = new GridLength(12) },
                new ColumnDefinition { Width = new GridLength(100) }
            }
        };

        var nameEntry = new Entry { Placeholder = "e.g. Flour" };
        var amountEntry = new Entry { Placeholder = "e.g. 2 cups" };

        grid.Add(nameEntry, 0);
        grid.Add(amountEntry, 2);

        IngredientsContainer.Add(grid);
    }

    private void OnAddIngredientClicked(object sender, TappedEventArgs e)
    {
        AddIngredientRow();
    }


    private async void OnImageTapped(object sender, TappedEventArgs e)
    {
        // TODO: hook up MediaPicker for camera/gallery
        await DisplayAlert("Image", "Camera/gallery picker coming soon.", "OK");
    }


    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // TODO: validate inputs, build Recipe model, persist to SQLite
        await DisplayAlert("Save", "Save logic coming soon.", "OK");
    }
}