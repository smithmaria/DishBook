using DishBook.Models;
using DishBook.Services;

namespace DishBook;

public partial class ViewPage : ContentPage
{
    private readonly DatabaseService _db;
    private List<Recipe> _allRecipes = new();

    public ViewPage(DatabaseService db)
    {
        InitializeComponent();
        _db = db;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _db.InitAsync();
        _allRecipes = await _db.GetRecipesAsync();
        RecipesCollection.ItemsSource = _allRecipes;
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var query = e.NewTextValue?.ToLowerInvariant() ?? string.Empty;
        RecipesCollection.ItemsSource = string.IsNullOrWhiteSpace(query)
            ? _allRecipes
            : _allRecipes.Where(r => r.Name.ToLowerInvariant().Contains(query)).ToList();
    }

    private async void OnRecipeTapped(object sender, TappedEventArgs e)
    {
        // TODO: Open recipe page
        if (e.Parameter is not Recipe recipe) return;
        await DisplayAlert("Recipe", $"Tapped: {recipe.Name}", "OK");
    }
}