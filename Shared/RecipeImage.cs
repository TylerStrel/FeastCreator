
namespace FeastCreator.Shared;

public class RecipeImage
{
    public int Created { get; set; }
    public ImageData[]? data { get; set; }

}

public class ImageData
{
    public string? Url { get; set; }
}
