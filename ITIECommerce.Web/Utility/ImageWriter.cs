namespace ITIECommerce.Web.Utility;

public class ImageWriter : IImageWriter
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ImageWriter(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string?> WriteImageToRootAsync(IFormFile? image, string path = "/")
    {
        string? imageUri = null;

        if (image != null)
        {
            imageUri = Path.Combine(
                path,
                Guid.NewGuid().ToString() + ".jpg");

            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imageUri.Trim('/'));

            using FileStream stream = File.Create(fullPath);
            await image.CopyToAsync(stream);
        }

        return imageUri;
    }
}
