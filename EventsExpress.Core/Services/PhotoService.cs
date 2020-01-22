using EventsExpress.Core.Extensions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;



namespace EventsExpress.Core.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork Db;
        private readonly IOptions<ImageOptionsModel> _widthOptions;

        public PhotoService(
            IUnitOfWork uow,
            IOptions<ImageOptionsModel> opt
            )
        {
            Db = uow;
            _widthOptions = opt;
        }

        public async Task<Photo> AddPhoto(IFormFile uploadedFile)
        {
            if (!IsValidImage(uploadedFile))
            {
                throw new ArgumentException();
            }

            byte[] imgData;
            using (var reader = new BinaryReader(uploadedFile.OpenReadStream()))
            {
                imgData = reader.ReadBytes((int)uploadedFile.Length);
            }

            var img = GetImageFromFile(uploadedFile);

            var photo = new Photo
            {
                Thumb = ImageToByteArray(Resize(img, _widthOptions.Value.Thumbnail)),
                Img = ImageToByteArray(Resize(img, _widthOptions.Value.Image)),
            };

            Db.PhotoRepository.Insert(photo);
            await Db.SaveAsync();
                
            return photo;
        }

        public Image GetImageFromFile(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);

                using (var img = Image.FromStream(memoryStream))
                {
                    return img;
                }
            }
        }

        public async Task Delete(Guid id)
        {
            var photo = Db.PhotoRepository.Get(id);
            if (photo != null)
            {
                Db.PhotoRepository.Delete(photo);
                await Db.SaveAsync();
            }
        }


        private static bool IsValidImage(IFormFile file) => (file != null && file.IsImage());


        private Image Resize(Image image, int width)
        {
            int height = (int)(image.Height * image.Width / width);
            var res = new Bitmap(width, height);
            using (var graphic = Graphics.FromImage(res))
            {
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.DrawImage(image, 0, 0, width, height);
            }
            return res;
        }


        private byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
        
    }
}