
namespace FeastCreator.Shared;

public class RecipeParams
{
    public string? MealTime { get; set; } = "Breakfast";
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public string? SelectedIdea { get; set; }

}
