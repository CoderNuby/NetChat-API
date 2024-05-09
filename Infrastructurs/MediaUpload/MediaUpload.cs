using Application.Errors;
using Application.Interfaces;
using Application.ViewModels;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructurs.MediaUpload
{
    public class MediaUpload : IMediaUpload
    {
        private readonly Cloudinary _cloudinary;

        public MediaUpload(IOptions<CloudinarySettings> config) 
        {
            var acc = new Account(config.Value.CloudName, 
                config.Value.ApiKey, 
                config.Value.ApiSecret);

            _cloudinary = new Cloudinary(acc);
        }

        public MediaUploadResultVM UploadMedia(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if(file.Length > 0)
            {
                using(var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream)
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }

                if(uploadResult.Error != null) 
                {
                    throw new ExceptionResponse(HttpStatusCode.InternalServerError, new { error = uploadResult.Error});
                }

                var response = new MediaUploadResultVM
                {
                    PublicId = uploadResult.PublicId,
                    Url = uploadResult.SecureUrl.AbsoluteUri
                };

                return response;
            }

            throw new ExceptionResponse(HttpStatusCode.BadRequest, new { error = "No files" });
        }
    }
}
