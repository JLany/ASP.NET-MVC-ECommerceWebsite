namespace ITIECommerce.Web.Utility;

public class ImageWriter : IImageWriter
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ImageWriter(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string?> WriteImageToRootAsync(IFormFile? image)
    {
        // TODO: Implement WriteImageToRootAsync.

        return "/images/product/product5.jpg";
    }
}
