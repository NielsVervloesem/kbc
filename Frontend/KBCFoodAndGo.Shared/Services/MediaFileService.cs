using KBCFoodAndGo.Shared.Interfaces.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KBCFoodAndGo.Shared.Services
{
    public class MediaFileService : IMediaFileService
    {
        public async Task<MediaFile> GetImageMediaFileFromImagePicker()
        {
            var mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Small,
                CompressionQuality = 100

            });
            return mediaFile;
        }

        public Image GetImageFromMediaFile(MediaFile mediaFile)
        {
            var image = new Image
            {
                Aspect = Aspect.AspectFit,
                Source = ImageSource.FromStream(mediaFile.GetStream)
            };
            return image;
        }

        public async Task<string> ConvertToBase64String(MediaFile file)
        {
            var stream = file.GetStream();
            var bytes = new byte[stream.Length];
            await stream.ReadAsync(bytes, 0, (int)stream.Length);
            return Convert.ToBase64String(bytes);
        }

        public Image ConvertFromBase64String(string base64Image)
        {
            var imageBytes = Convert.FromBase64String(base64Image);
            var image = new Image
            {
                Aspect = Aspect.AspectFit,
                Source = ImageSource.FromStream(() => new MemoryStream(imageBytes))
            };
            return image;
        }
    }
}
