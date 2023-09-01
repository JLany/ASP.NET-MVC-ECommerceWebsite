namespace ITIECommerce.Web.Utility
{
    public interface IImageWriter
    {
        /// <summary>
        /// Write an image to the wwwroot/upload/images path.
        /// </summary>
        /// <param name="image"></param>
        /// <returns>The URI for the written image or null if image is null.</returns>
        Task<string?> WriteImageToRootAsync(IFormFile? image);
    }
}