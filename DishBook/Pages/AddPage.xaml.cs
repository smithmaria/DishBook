using DishBook.Models;
using DishBook.Services;

namespace DishBook;

public partial class AddPage : ContentPage
{
    private readonly DatabaseService _db;
    public AddPage(DatabaseService db)
    {
        InitializeComponent();
        _db = db;
        AddIngredientRow();
    }

    // TODO: add ingredients to sql database
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
        await Shell.Current.GoToAsync("//Home");
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            await DisplayAlert("Validation", "Recipe name is required.", "OK");
            return;
        }

        var recipe = new Recipe
        {
            Name = NameEntry.Text.Trim(),
            Description = DescriptionEditor.Text?.Trim() ?? string.Empty,
            CookTimeMinutes = int.TryParse(CookTimeEntry.Text, out var ct) ? ct : 0,
            Servings = int.TryParse(ServingsEntry.Text, out var sv) ? sv : 0,
            Directions = DirectionsEditor.Text?.Trim() ?? string.Empty,
            Notes = NotesEditor.Text?.Trim() ?? string.Empty,
        };

        await _db.InitAsync();
        await _db.SaveRecipeAsync(recipe);
        ClearForm();
        await Shell.Current.GoToAsync("//Home");
    }

    private void ClearForm()
    {
        NameEntry.Text = string.Empty;
        DescriptionEditor.Text = string.Empty;
        CookTimeEntry.Text = string.Empty;
        ServingsEntry.Text = string.Empty;
        DirectionsEditor.Text = string.Empty;
        NotesEditor.Text = string.Empty;
        IngredientsContainer.Clear();
        AddIngredientRow();
    }
}