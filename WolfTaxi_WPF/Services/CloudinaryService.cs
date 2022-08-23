using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WolfTaxi_WPF.Services
{
    public abstract class CloudinaryService
    {

        public static string UploadImage(string path, string name, string cloudinaryFoldePath)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(path),
                Folder = cloudinaryFoldePath,
                PublicId = TextService.GetFileName(name)
            };
            App.cloudinary.Upload(uploadParams);
            return GetSource(cloudinaryFoldePath, name);
        }

        public static void DestroyImage(string publicId)
        {
            var destroyParams = new DeletionParams(publicId);
            App.cloudinary.Destroy(destroyParams);

        }

        public static string GetSource(string folderPath, string name)
            => $"https://res.cloudinary.com/kysbv/image/upload/v1661212843/{folderPath}/{name}.png";
    }
}
