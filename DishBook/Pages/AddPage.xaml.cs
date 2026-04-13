using DishBook.Models;
using DishBook.Services;

namespace DishBook;

public partial class AddPage : ContentPage
{
    private readonly DatabaseService _db;
    private Recipe? _editingRecipe;

    // Raised after save so RecipeDetailPage can refresh
    public event Action<Recipe>? RecipeSaved;

    // ADD MODE
    public AddPage(DatabaseService db)
    {
        InitializeComponent();
        _db = db;
        AddIngredientRow();
    }

    // EDIT MODE
    public AddPage(DatabaseService db, Recipe recipe)
    {
        InitializeComponent();
        _db = db;
        _editingRecipe = recipe;
        Title = "Edit Recipe";
        PopulateForm(recipe);
    }

    private void PopulateForm(Recipe recipe)
    {
        NameEntry.Text = recipe.Name;
        DescriptionEditor.Text = recipe.Description;
        CookTimeEntry.Text = recipe.CookTimeMinutes > 0
            ? recipe.CookTimeMinutes.ToString() : string.Empty;
        ServingsEntry.Text = recipe.Servings > 0
            ? recipe.Servings.ToString() : string.Empty;
        DirectionsEditor.Text = recipe.Directions;
        NotesEditor.Text = recipe.Notes;

        // Rebuild ingredient rows from stored string
        IngredientsContainer.Clear();
        if (!string.IsNullOrWhiteSpace(recipe.Ingredients))
        {
            foreach (var line in recipe.Ingredients.Split('\n',
                         StringSplitOptions.RemoveEmptyEntries))
            {
                // Each stored line is "Name|Amount"
                var parts = line.Split('|');
                AddIngredientRow(
                    parts.Length > 0 ? parts[0] : string.Empty,
                    parts.Length > 1 ? parts[1] : string.Empty);
            }
        }
        else
        {
            AddIngredientRow();
        }
    }

    private void AddIngredientRow(string name = "", string amount = "")
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
        var nameEntry = new Entry { Placeholder = "e.g. Flour", Text = name };
        var amountEntry = new Entry { Placeholder = "e.g. 2 cups", Text = amount };
        grid.Add(nameEntry, 0);
        grid.Add(amountEntry, 2);
        IngredientsContainer.Add(grid);
    }

    private void OnAddIngredientClicked(object sender, TappedEventArgs e)
        => AddIngredientRow();

    private async void OnImageTapped(object sender, TappedEventArgs e)
        => await DisplayAlert("Image", "Camera/gallery picker coming soon.", "OK");

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
        else
            await Shell.Current.GoToAsync("//Home");
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            await DisplayAlert("Validation", "Recipe name is required.", "OK");
            return;
        }

        // Collect ingredients as "Name|Amount" lines
        var ingredientLines = IngredientsContainer.Children
            .OfType<Grid>()
            .Select(g =>
            {
                var n = (g.Children[0] as Entry)?.Text?.Trim() ?? string.Empty;
                var a = (g.Children[1] as Entry)?.Text?.Trim() ?? string.Empty;
                return $"{n}|{a}";
            })
            .Where(l => l != "|")
            .ToList();

        // Reuse existing record if editing, create new if adding
        var recipe = _editingRecipe ?? new Recipe();
        recipe.Name = NameEntry.Text.Trim();
        recipe.Description = DescriptionEditor.Text?.Trim() ?? string.Empty;
        recipe.CookTimeMinutes = int.TryParse(CookTimeEntry.Text, out var ct) ? ct : 0;
        recipe.Servings = int.TryParse(ServingsEntry.Text, out var sv) ? sv : 0;
        recipe.Ingredients = string.Join('\n', ingredientLines);
        recipe.Directions = DirectionsEditor.Text?.Trim() ?? string.Empty;
        recipe.Notes = NotesEditor.Text?.Trim() ?? string.Empty;

        await _db.InitAsync();
        await _db.SaveRecipeAsync(recipe);

        RecipeSaved?.Invoke(recipe);

        if (Navigation.NavigationStack.Count > 1)
            await Navigation.PopAsync();
        else
        {
            ClearForm();
            await Shell.Current.GoToAsync("//Home");
        }
    }

    private void ClearForm()
    {
        NameEntry.Text = DescriptionEditor.Text =
            CookTimeEntry.Text = ServingsEntry.Text =
            DirectionsEditor.Text = NotesEditor.Text = string.Empty;
        IngredientsContainer.Clear();
        AddIngredientRow();
    }
}