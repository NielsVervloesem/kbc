using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace KBCFoodAndGo.Shared.Interfaces.Services
{
    public interface IMediaFileService
    {
        Task<MediaFile> GetImageMediaFileFromImagePicker();
        Image GetImageFromMediaFile(MediaFile mediaFile);
        Task<string> ConvertToBase64String(MediaFile file);
        Image ConvertFromBase64String(string base64Image);
    }
}
