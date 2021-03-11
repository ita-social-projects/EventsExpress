using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace EventsExpress.Core.Services
{
    public class PhotoService : BaseService<Photo>, IPhotoService
    {
        private readonly IOptions<ImageOptionsModel> _widthOptions;
        private readonly Lazy<HttpClient> _client;

        public PhotoService(
            AppDbContext context,
            IOptions<ImageOptionsModel> opt,
            IHttpClientFactory clientFactory)
            : base(context)
        {
            _widthOptions = opt;
            _client = new Lazy<HttpClient>(() => clientFactory.CreateClient());
        }

        public async Task<Photo> AddPhoto(IFormFile uploadedFile)
        {
            var photo = GetPhotoFromIFormFile(uploadedFile);
            Insert(photo);
            await Context.SaveChangesAsync();

            return photo;
        }

        public async Task<Photo> AddPhotoByURL(string url)
        {
            if (!await IsImageUrl(url))
            {
                throw new ArgumentException("The url should be a valid image", nameof(url));
            }

            Uri uri = new Uri(url);
            byte[] imgData = _client.Value.GetByteArrayAsync(uri).Result;
            var photo = new Photo
            {
                Thumb = imgData,
                Img = imgData,
            };

            Insert(photo);
            await Context.SaveChangesAsync();

            return photo;
        }

        private async Task<bool> IsImageUrl(string url)
        {
            try
            {
                HttpResponseMessage result = await _client.Value.GetAsync(url);
                return result.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public async Task Delete(Guid id)
        {
            var photo = Context.Photos.Find(id);
            if (photo != null)
            {
                Delete(photo);
                await Context.SaveChangesAsync();
            }
        }

        private static bool IsValidImage(IFormFile file) => file != null && file.IsImage();

        private Photo GetPhotoFromIFormFile(IFormFile uploadedFile)
        {
            if (!IsValidImage(uploadedFile))
            {
                throw new ArgumentException("The upload file should be a valid image", nameof(uploadedFile));
            }

            var photo = new Photo
            {
                Thumb = GetResizedBytesFromFile(uploadedFile, _widthOptions.Value.Thumbnail),
                Img = GetResizedBytesFromFile(uploadedFile, _widthOptions.Value.Image),
            };

            return photo;
        }

        public byte[] GetResizedBytesFromFile(IFormFile file, int newWidth)
        {
            using var memoryStream = file.ToMemoryStream();
            var oldBitMap = new Bitmap(memoryStream);
            var newSize = new Size
            {
                Width = newWidth,
                Height = oldBitMap.Size.Height * newWidth / oldBitMap.Size.Width,
            };

            var newBitmap = new Bitmap(oldBitMap, newSize);

            return ImageToByteArray(newBitmap);
        }

        private byte[] ImageToByteArray(Image imageIn)
        {
            using var ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Png);

            return ms.ToArray();
        }
    }
}
