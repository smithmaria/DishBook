using DishBook.Models;
using DishBook.Services;

namespace DishBook.Pages;

public partial class FavoritesPage : ContentPage
{
    private readonly DatabaseService _db;

    public FavoritesPage(DatabaseService db)
    {
        InitializeComponent();
        _db = db;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _db.InitAsync();
        FavoritesCollection.ItemsSource = await _db.GetFavoriteRecipesAsync();
    }

    private async void OnRecipeTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is not Recipe recipe) return;
        var detailPage = new RecipeDetailPage(recipe, _db);
        await Navigation.PushAsync(detailPage);
    }
}