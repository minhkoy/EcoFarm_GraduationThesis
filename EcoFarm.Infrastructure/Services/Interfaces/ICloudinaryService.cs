using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Infrastructure.Services.Interfaces
{
    public interface ICloudinaryService
    {
        string UploadBase64Image(string base64Image);
        string UploadBlobImage(Blob blob);
        string UploadImageFromFile(IFormFile image);
    }
}
