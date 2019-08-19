using EventsExpress.Core.Extensions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using ImageProcessor;
using ImageProcessor.Imaging;
using ImageProcessor.Imaging.Formats;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;


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
                Thumb = Resize(imgData, _widthOptions.Value.Thumbnail),
                Img = Resize(imgData, _widthOptions.Value.Image),
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


        private static byte[] Resize(byte[] originalImage, int width)
        {
            using (var originalImageStream = new MemoryStream(originalImage))
            {
                using (var resultImage = new MemoryStream())
                {
                    using (var imageFactory = new ImageFactory())
                    {
                        var createdImage = imageFactory.Load(originalImageStream);

                        if (createdImage.Image.Width > width)
                        {
                            createdImage = createdImage.Resize(new ResizeLayer(new Size(width, 0), ResizeMode.Max));
                        }
                        createdImage.Format(new JpegFormat())
                            .Save(resultImage);
                    }

                    return resultImage.GetBuffer();
                }
            }
        }

    }
}