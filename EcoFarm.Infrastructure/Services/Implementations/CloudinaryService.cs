using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EcoFarm.Domain.Common.Values.Options;


//using EcoFarm.Domain.Entities.Administration;
using EcoFarm.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Infrastructure.Services.Implementations
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly CloudinaryAccountOption _cloudinaryAccountOption;
        private readonly Account _account;
        public CloudinaryService(IOptions<CloudinaryAccountOption> options )
        {
            _cloudinaryAccountOption = options.Value;
            _account = new Account(
                _cloudinaryAccountOption.Cloud,
                _cloudinaryAccountOption.ApiKey,
                _cloudinaryAccountOption.ApiSecret
            );
        }
        public string UploadBase64Image(string base64Image)
        {
            Account account = new Account(
                _cloudinaryAccountOption.Cloud,
                _cloudinaryAccountOption.ApiKey,
                _cloudinaryAccountOption.ApiSecret
            );
            Cloudinary cloudinary = new Cloudinary(account);
            byte[] testBytes = Encoding.ASCII.GetBytes(base64Image);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(Guid.NewGuid().ToString(), new MemoryStream(testBytes)),

            };
            var uploadResult = cloudinary.Upload(uploadParams);
            if (uploadResult is null || uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Upload image failed");
            }
            return uploadResult.SecureUrl.AbsoluteUri;
        }

        public string UploadBlobImage(Blob blob)
        {
            throw new NotImplementedException();
        }

        public string UploadImageFromFile(IFormFile image)
        {
            byte[] fileBytes = new Span<byte>().ToArray();
            if (image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    fileBytes = ms.ToArray();
                    //s = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                }
            }
            Account account = new Account(
                _cloudinaryAccountOption.Cloud,
                _cloudinaryAccountOption.ApiKey,
                _cloudinaryAccountOption.ApiSecret
            );
            Cloudinary cloudinary = new Cloudinary(account);
            byte[] testBytes = new byte[1024];
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(Guid.NewGuid().ToString(), new MemoryStream(fileBytes)),

            };
            var uploadResult = cloudinary.Upload(uploadParams);
            if (uploadResult is null || uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return string.Empty;
            }
            return uploadResult.SecureUrl.AbsoluteUri;
        }
    }
}
