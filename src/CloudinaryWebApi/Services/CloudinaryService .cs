using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace CloudinaryWebApi.Services;

public interface ICloudinaryService
{
    Task<string> UploadImageAsync(IFormFile file);
    Task<ImageResult> GetImageInfoAsync(string publicId);
    Task<ListResourcesResult> GetImagesAsync();
    Task<DeletionResult> DeleteAsync(DeletionParams deletionParams);
    string Resize(string publicId, int width, int height);
    string GetTemporaryUrlForPublic(string publicId = "my_folder/tree-1", string format = "jpg", double expiresInMinutes = 0.2);
    string GetTemporaryUrl(string publicId = "my_folder/tree-736885__480", string format = "jpg", double expiresInMinutes = 0.2);
}

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IConfiguration configuration)
    {
        Account account = new Account(
            configuration["Cloudinary:CloudName"],
            configuration["Cloudinary:ApiKey"],
            configuration["Cloudinary:ApiSecret"]);

        _cloudinary = new Cloudinary(account);
    }
    public string GetTemporaryUrlForPublic(string publicId = "my_folder/tree-1", string format = "jpg", double expiresInMinutes = -0.2)
    {
        /*
        publicId = publicId.Replace("%2F", "/");
        var expirationTime = DateTime.Now.ToUniversalTime().AddMinutes(expiresInMinutes);
        var timestamp = (int)(expirationTime - new DateTime(1970, 1, 1)).TotalSeconds;
        var signature = _cloudinary.Api.SignParameters(new SortedDictionary<string, object>
        {
            { "public_id", publicId },
            { "timestamp", timestamp },
            { "invalidate", true }
        });*/
        /*
        long expiresAt = (long)DateTime.UtcNow.AddMinutes(1).Subtract(new DateTime(1970, 1, 1)).TotalSeconds; // 1 minute from now
        string resourceType = "image";
        string type = "authenticated";

        string url = _cloudinary.Api.UrlImgUp
            .BuildUrl(publicId, new Dictionary<string, object>{
                { "resource_type", resourceType },
                { "type", type },
                { "expires_at", expiresAt }
            });
        */
        /*
        var res = _cloudinary.Api.UrlImgUp/*.Signed(true)*/
        /*.Secure().ResourceType("image").Format("jpg")
            //.Transform(new Transformation().Width(500).Crop("scale"))
            .BuildUrl(publicId) + "?signature=" + signature + "&timestamp=" + timestamp;
        */


        return null;
    }

    public string GetTemporaryUrl(string publicId = "my_folder/tree-736885__480", string format = "jpg", double expiresInMinutes = 0.2)
    {
        publicId = "my_folder/tree-1";

        //publicId = "my_folder/download";
        //publicId = "my_folder/tree-736885__480";
        publicId = publicId.Replace("%2F", "/");
        var expirationTime = DateTime.Now.ToUniversalTime().AddSeconds(15);
        var timestamp = (int)(expirationTime - new DateTime(1970, 1, 1)).TotalSeconds;

        var info = _cloudinary.GetResourceAsync(publicId).Result;

        if (info.Type != "private")
        {
            //throw new Exception("Not public");
            var rr = 1;
        }

        var res = _cloudinary.DownloadPrivate(publicId, expiresAt: timestamp, format: format);

        return res;
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return null;

        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
        var extension = Path.GetExtension(file.FileName);
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(fileName, file.OpenReadStream()),
            Folder = "nnimage/",
            PublicId = fileName,
            Overwrite = true,
            //ReturnDeleteToken = true,

            //AccessMode = "private",
            //Type = "private"
            //Transformation = new Transformation().Width(800).Height(800).Crop("limit")
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        return uploadResult?.SecureUri!.AbsoluteUri!;
    }

    public async Task<ImageResult> GetImageInfoAsync(string publicId)
    {
        publicId = publicId.Replace("%2F", "/");

        var result = await _cloudinary.GetResourceAsync(publicId);

        if (result.Error != null)
        {
            throw new Exception($"Failed to get image info from Cloudinary: {result.Error.Message}");
        }

        var imageInfo = new ImageResult
        {
            PublicId = result.PublicId,
            Format = result.Format,
            Width = result.Width,
            Height = result.Height,
            Url = result.Url.ToString()
        };

        return imageInfo;
    }

    public async Task<ListResourcesResult> GetImagesAsync()
    {
        return await _cloudinary.ListResourcesAsync();
    }

    public async Task<DeletionResult> DeleteAsync(DeletionParams deletionParams)
    {
        return await _cloudinary.DestroyAsync(deletionParams);
    }

    public string Resize(string publicId, int width, int height)
    {
        var transformation = new Transformation().Width(width).Height(height).Crop("scale");
        return _cloudinary.Api.UrlImgUp.Transform(transformation).BuildUrl(publicId);
    }

    /*
    private string BuildUrl(string publicId, Transformation transformation = null)
    {
        var imageBuilder = new ImageTagBuilder().PublicId(publicId);

        if (transformation != null)
        {
            imageBuilder.Transformation(transformation);
        }

        return _cloudinary.Api.UrlImgUp.BuildUrl(imageBuilder);
    }*/
}
