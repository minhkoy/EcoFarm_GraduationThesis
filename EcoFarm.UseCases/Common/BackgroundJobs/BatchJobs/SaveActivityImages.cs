using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Entities;
using EcoFarm.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.Common.BackgroundJobs.BatchJobs
{
    public class SaveActivityImages
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;
        public SaveActivityImages(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
        }
        public async Task Run(string activityId, List<IFormFile> files )
        {
            files.ForEach(file =>
            {
                var url = _cloudinaryService.UploadImageFromFile(file);
                if (!string.IsNullOrEmpty(url))
                {
                    _unitOfWork.ActivityMedias.Add(new ActivityMedia
                    {
                        ACTIVITY_ID = activityId,
                        MEDIA_URL = url,
                        
                    });
                }
            });
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
