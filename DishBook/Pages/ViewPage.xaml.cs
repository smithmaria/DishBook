namespace DishBook;

public partial class ViewPage : ContentPage
{
    // TODO: Implement actual Recipe entity
    private record DummyRecipe(string Name);

    private readonly List<DummyRecipe> _allRecipes = new()
    {
        new DummyRecipe("Spaghetti Carbonara"),
        new DummyRecipe("Chicken Stir Fry"),
        new DummyRecipe("Banana Bread"),
        new DummyRecipe("Caesar Salad"),
        new DummyRecipe("Beef Tacos"),
        new DummyRecipe("Tomato Soup"),
    };

    public ViewPage()
    {
        InitializeComponent();
        RecipesCollection.ItemsSource = _allRecipes;
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        // TODO: Search functionality
    }

    private async void OnRecipeTapped(object sender, TappedEventArgs e)
    {
        // TODO: Open recipe page
        if (e.Parameter is not DummyRecipe recipe) return;
        await DisplayAlert("Recipe", $"Tapped: {recipe.Name}", "OK");
    }
}