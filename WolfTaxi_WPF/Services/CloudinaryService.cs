using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using WolfTaxi_WPF.Enums;

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
                PublicId = name,
                DisplayName = name
            };
            ImageUploadResult res;
            try { res = App.cloudinary.Upload(uploadParams); }
            catch (Exception) { return string.Empty; }
            return res.Url.ToString();
        }

        public static ProcessResult DestroyImage(string fileName,string folderPath)
        {
            if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(folderPath))
                return ProcessResult.EmptyArguments;
            var destroyParams = new DeletionParams($"{folderPath}/{fileName}");
            DeletionResult result = new();
            try { result = App.cloudinary.Destroy(destroyParams); }
            catch (Exception) { return ProcessResult.Error; }
            return ProcessResult.Success;
        }

        public static string GetSource(string name, string folderPath)
        {
            try
            {
                SearchResult result = App.cloudinary.Search()
                      .Expression($"resource_type:image AND folder={folderPath} AND filename={name}")
                      .SortBy("public_id", "desc")
                      .MaxResults(1)
                      .Execute();
                return result.Resources[0].Url;
            }
            catch (Exception) { return string.Empty; }

            return string.Empty;
        }
    }
}