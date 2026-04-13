using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DishBook.Models;
using SQLite;

namespace DishBook.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _db;

        public async Task InitAsync()
        {
            if (_db != null) return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "dishbook.db3");
            _db = new SQLiteAsyncConnection(dbPath);
            await _db.CreateTableAsync<Recipe>();
        }

        public async Task<List<Recipe>> GetRecipesAsync() =>
            await _db.Table<Recipe>().ToListAsync();

        public async Task<int> SaveRecipeAsync(Recipe recipe) =>
            recipe.Id == 0
                ? await _db.InsertAsync(recipe)
                : await _db.UpdateAsync(recipe);

        public async Task DeleteRecipeAsync(Recipe recipe) =>
            await _db.DeleteAsync(recipe);

        public Task<List<Recipe>> GetFavoriteRecipesAsync() => 
            _db.Table<Recipe>().Where(r => r.IsFavorite).ToListAsync();
    }
}
