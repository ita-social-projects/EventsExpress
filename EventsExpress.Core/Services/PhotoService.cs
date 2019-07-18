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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace EventsExpress.Core.Services
{
    public class PhotoService : IPhotoService
    {
        private IUnitOfWork Db;
        private IHostingEnvironment _appEnvironment;

        #region configure storing
        // 

        private enum OwnType
        {
            UserAvatar,
            EventPhoto
        }

        private Dictionary<OwnType, string> _pathOptions = new Dictionary<OwnType, string>()
        {
            {OwnType.UserAvatar, "/files/images/avatars/"},
            {OwnType.EventPhoto, "/files/images/events/"}
        };
        private Dictionary<OwnType, int[]> _widths = new Dictionary<OwnType, int[]>
        {
            {OwnType.UserAvatar, new int[] { 400 } },
            {OwnType.EventPhoto, new int[] { 400, 1200 } }
        };
        #endregion

        public PhotoService(
            IUnitOfWork uow,
            IHostingEnvironment appEnvironment
            )
        {
            Db = uow;
            _appEnvironment = appEnvironment;
        }

        public async Task<Photo> AddUserPhoto(IFormFile uploadedFile)
        {
           
            var path = SaveImage(uploadedFile, OwnType.UserAvatar);

            // Create new Photo Object for DataBase:
            Photo photo = new Photo
            {
                Path = path,
                Extension = Path.GetExtension(uploadedFile.FileName).Substring(0)
            };
            Db.PhotoRepository.Insert(photo);

            await Db.SaveAsync();

            return photo;
        }

        public async Task<Photo> AddEventPhoto(IFormFile uploadedFile)
        {
            var path = SaveImage(uploadedFile, OwnType.EventPhoto);

            // Create new Photo Object for DataBase:
            Photo photo = new Photo { Path = path };
            Db.PhotoRepository.Insert(photo);

            await Db.SaveAsync();

            return photo;
        }


        public async Task Delete(Guid id)
        {
            var photo = Db.PhotoRepository.Get(id);
            if (photo != null)
            {
                try
                {
                    File.Delete(_appEnvironment + photo.Path);
                }
                finally
                {
                    Db.PhotoRepository.Delete(photo);
                    await Db.SaveAsync();
                }
            }

        }

        #region UploadHelpers...

        private bool IsValidImage(IFormFile file) => (file != null || file.IsImage());

        private string SaveImage(IFormFile uploadedFile, OwnType type)
        {
            if (!IsValidImage(uploadedFile))
            {
                throw (new Exception("Bad file!"));
            }
            // Check for Directory exist
            CreateFolder(_appEnvironment.WebRootPath + _pathOptions[type]);

            string fileExt = Path.GetExtension(uploadedFile.FileName).Substring(0);
            return ResizeAndSaveImage(uploadedFile, _widths[type], _pathOptions[type] + uploadedFile.FileName, fileExt);
        }

        private string ResizeAndSaveImage(IFormFile originalImage, int[] widths, string originalImageFilePath, string extension)
        {
            byte[] imgData;
            using (var reader = new BinaryReader(originalImage.OpenReadStream()))
            {
                imgData = reader.ReadBytes((int)originalImage.Length);
            }

            var filePath = originalImageFilePath.Substring(0, originalImageFilePath.Length - extension.Length);

            foreach (var width in widths)
            {
                var resizedImageFilePath = filePath + "_" + width + extension;

                byte[] resizedImageBytes = this.Resize(imgData, width);

                File.WriteAllBytes(_appEnvironment.ContentRootPath + resizedImageFilePath, resizedImageBytes);

                //var q = _appEnvironment.ContentRootPath + resizedImageFilePath;

                MemoryStream ms = new MemoryStream(resizedImageBytes);
                Image resizedImage = Image.FromStream(ms);

                //resizedImage.Save(resizedImageFilePath);
                using (var fileStream = new FileStream(_appEnvironment.ContentRootPath + resizedImageFilePath, FileMode.Create))
                {
                    resizedImage.Save(fileStream, ImageFormat.Jpeg);
                }
            }

            return filePath;
        }

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
                            .Format(new JpegFormat {  })
                            .Save(resultImage);
                    }

                    return resultImage.GetBuffer();
                }
            }
        }

        #endregion

        #region FoldersHelpers...

        private bool CreateFolder(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    return false;
                }
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private bool DeleteFolders(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    return false;
                }
                DirectoryInfo di = Directory.CreateDirectory(path);
                foreach (var file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (var dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
                di.Delete();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private int GetFilesCount(string path)
        {
            var count = 0;
            try
            {
                if (!Directory.Exists(path))
                {
                    return 0;
                }
                DirectoryInfo di = Directory.CreateDirectory(path);
                count = di.GetFiles().Length;
            }
            catch (Exception e)
            {
                return 0;
            }
            return count;
        }
        #endregion

    }
}

