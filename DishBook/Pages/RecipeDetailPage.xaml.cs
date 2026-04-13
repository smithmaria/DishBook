using DishBook.Models;
using DishBook.Services;

namespace DishBook.Pages;

public partial class RecipeDetailPage : ContentPage
{
    private readonly DatabaseService _db;
    private Recipe _recipe;

    public RecipeDetailPage(Recipe recipe, DatabaseService db)
    {
        InitializeComponent();
        _db = db;
        _recipe = recipe;
        PopulateView();
    }

    private void PopulateView()
    {
        TitleLabel.Text = _recipe.Name;
        UpdateFavoriteIcon();

        CookTimeLabel.Text = _recipe.CookTimeMinutes > 0
            ? $"{_recipe.CookTimeMinutes} min" : "—";
        ServingsLabel.Text = _recipe.Servings > 0
            ? $"{_recipe.Servings}" : "—";

        // Description
        DescriptionSection.IsVisible = !string.IsNullOrWhiteSpace(_recipe.Description);
        DescriptionLabel.Text = _recipe.Description;

        // Ingredients
        IngredientsContainer.Clear();
        if (!string.IsNullOrWhiteSpace(_recipe.Ingredients))
        {
            var lines = _recipe.Ingredients.Split('\n',
                StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                IngredientsContainer.Add(new Label
                {
                    Text = $"• {line.Trim()}",
                    TextColor = (Color)Application.Current!.Resources["Gray600"],
                    LineBreakMode = LineBreakMode.WordWrap
                });
            }
        }

        // Directions
        DirectionsSection.IsVisible = !string.IsNullOrWhiteSpace(_recipe.Directions);
        DirectionsLabel.Text = _recipe.Directions;

        // Notes
        NotesSection.IsVisible = !string.IsNullOrWhiteSpace(_recipe.Notes);
        NotesLabel.Text = _recipe.Notes;
    }

    private void UpdateFavoriteIcon()
    {
        FavoriteIcon.Source = _recipe.IsFavorite ? "heart_filled.png" : "heart_outline.png";
    }

    private async void OnFavoriteTapped(object sender, TappedEventArgs e)
    {
        _recipe.IsFavorite = !_recipe.IsFavorite;
        UpdateFavoriteIcon();
        await _db.SaveRecipeAsync(_recipe);
    }

    private async void OnEditTapped(object sender, TappedEventArgs e)
    {
        var editPage = new AddPage(_db, _recipe);
        editPage.RecipeSaved += OnRecipeUpdated;
        await Navigation.PushAsync(editPage);
    }

    private void OnRecipeUpdated(Recipe updated)
    {
        _recipe = updated;
        PopulateView();
    }
}