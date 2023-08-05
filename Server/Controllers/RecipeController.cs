using FeastCreator.Server.Services.Interfaces;
using FeastCreator.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FeastCreator.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class RecipeController : ControllerBase
{
    private readonly IOpenAIAPI _openAIService;

    public RecipeController(IOpenAIAPI openAIService)
    {
        _openAIService = openAIService;
    }


    [HttpPost, Route("GetMealIdeas")]
    public async Task<ActionResult<List<Idea>>> GetFeastIdeas(RecipeParams recipeParams)
    {
        string mealTime = recipeParams.MealTime!;
        List<string> ingredients = recipeParams.Ingredients.Where(i => !string.IsNullOrEmpty(i.Description)).Select(i => i.Description).ToList()!;

        if (string.IsNullOrEmpty(mealTime)) mealTime = "Breakfast";

        List<Idea> ideas = new List<Idea>();
        int attempts = 0;

        while (ideas.Count == 0 && attempts < 10)
        {
            ideas = await _openAIService.CreateMealIdeasAsync(mealTime, ingredients);
            Console.WriteLine($"Trying to obtain ideas attempt # {attempts + 1}");
            attempts++;
        }

        Console.WriteLine($"Ideas : {ideas.Count}");

        if (ideas.Count == 0) return BadRequest();

        return ideas;
    }

    [HttpPost, Route("GetRecipe")]
    public async Task<ActionResult<Recipe>> GetRecipe(RecipeParams recipeParams)
    {
        List<string> ingredients = recipeParams.Ingredients.Where(i => !string.IsNullOrEmpty(i.Description)).Select(i => i.Description).ToList()!;

        string title = recipeParams.SelectedIdea!;

        if (string.IsNullOrEmpty(title)) return BadRequest();


        Recipe? recipe = null;
        int attempts = 0;

        while (recipe is null && attempts < 10)
        {
            recipe = await _openAIService.CreateRecipe(title, ingredients);
            Console.WriteLine($"Trying to obtain recipe attempt # {attempts + 1}");
            attempts++;
        }

        return recipe!;
    }

    [HttpGet, Route("GetRecipeImage")]
    public async Task<RecipeImage> GetRecipeImage(string recipeTitle)
    {
        RecipeImage? recipeImage = null;
        int attempts = 0;

        while (recipeImage is null && attempts < 10)
        {
            recipeImage = await _openAIService.CreateRecipeImageAsync(recipeTitle);
            Console.WriteLine($"Trying to obtain recipe image attempt # {attempts + 1}");
            attempts++;
        }

        return recipeImage!;
    }
}
