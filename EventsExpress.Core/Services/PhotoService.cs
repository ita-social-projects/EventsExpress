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

            var photo = new Photo
            {
                Thumb = GetResizedBytesFromFile(uploadedFile, _widthOptions.Value.Thumbnail),
                Img = GetResizedBytesFromFile(uploadedFile, _widthOptions.Value.Image),
            };

            Db.PhotoRepository.Insert(photo);
            await Db.SaveAsync();

            return photo;
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

        public byte[] GetResizedBytesFromFile(IFormFile file, int newWidth)
        {
            using (var memoryStream = file.OpenReadStream())
            {
                var oldBitMap = new Bitmap(memoryStream);
                var newSize = new Size
                {
                    Width = newWidth,
                    Height = (int)(oldBitMap.Size.Height * newWidth / oldBitMap.Size.Width)
                };

                var newBitmap = new Bitmap(oldBitMap, newSize);

                return ImageToByteArray(newBitmap);
            }
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