using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using ImageProcessor;
using ImageProcessor.Imaging;
using ImageProcessor.Imaging.Formats;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace EventsExpress.Core.Services
{
    public class PhotoService : IPhotoService
    {
        private IUnitOfWork Db;
        private WidthsConfig _widthsConfig;


        public PhotoService(
            IUnitOfWork uow,
            IHostingEnvironment appEnvironment
            )
        {
            Db = uow;
            _widthsConfig = new WidthsConfig() { thumbnail = 400, image = 1200};
        }

        public async Task<Photo> AddPhoto(IFormFile uploadedFile)
        {
            byte[] imgData;
            using (var reader = new BinaryReader(uploadedFile.OpenReadStream()))
            {
                imgData = reader.ReadBytes((int)uploadedFile.Length);
            }

            Photo photo = new Photo
            {
                Thumb = Resize(imgData, _widthsConfig.thumbnail),
                Img = Resize(imgData, _widthsConfig.image),
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

        #region UploadHelpers...

        private bool IsValidImage(IFormFile file) => (file != null || file.IsImage());



        private byte[] Resize(byte[] originalImage, int width)
        {
            using (var originalImageStream = new MemoryStream(originalImage))
            {
                using (var resultImage = new MemoryStream())
                {
                    using (var imageFactory = new ImageFactory())
                    {
                        var createdImage = imageFactory
                                .Load(originalImageStream);

                        if (createdImage.Image.Width > width)
                        {
                            createdImage = createdImage
                                .Resize(new ResizeLayer(new Size(width, 0), ResizeMode.Max));
                        }

                        createdImage
                            .Format(new JpegFormat { })
                            .Save(resultImage);
                    }

                    return resultImage.GetBuffer();
                }
            }
        }

        #endregion

    }
}

class WidthsConfig
{
    public int thumbnail { get; set; } 
    public int image { get; set; }
}