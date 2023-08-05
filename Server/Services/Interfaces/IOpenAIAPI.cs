using FeastCreator.Shared;

namespace FeastCreator.Server.Services.Interfaces;

public interface IOpenAIAPI
{
    Task<List<Idea>> CreateMealIdeasAsync(string mealTime, List<string> ingrediants);
    Task<Recipe?> CreateRecipe(string title, List<string> ingrediants);
    Task<RecipeImage?> CreateRecipeImageAsync(string recipeTitle);
}
