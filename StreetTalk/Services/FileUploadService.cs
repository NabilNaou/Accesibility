using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using StreetTalk.Exceptions;
using StreetTalk.Models;

namespace StreetTalk.Services
{
    public interface IFileUploadService
    {
        PostPhoto HandlePostPhotoUpload(IFormFile uploadedPhoto, bool sensitive);
    }
    
    public class FileUploadService : IFileUploadService
    {
        private readonly List<string> permittedUploadExtensions = new List<string> { ".png", ".jpg", ".jpeg" };
        private readonly IConfiguration config;
        private readonly IWebHostEnvironment environment;
        
        public FileUploadService(IConfiguration config, IWebHostEnvironment environment)
        {
            this.config = config;
            this.environment = environment;
        }
        
        public PostPhoto HandlePostPhotoUpload(IFormFile uploadedPhoto, bool sensitive)
        {
            var extenstion = Path.GetExtension(uploadedPhoto.FileName);
            
            if (extenstion == null || !permittedUploadExtensions.Contains(extenstion))
                throw new InvalidFileFormatException();

            var newFilename = Path.GetRandomFileName() + extenstion;
            var filePath = Path.Combine(config["StoredFilesPath"], newFilename);
            var stream = File.Create(Path.Combine(environment.WebRootPath, filePath));
            
            uploadedPhoto.CopyToAsync(stream).Wait();
            
            stream.Close();
            
            return new PostPhoto
            {
                Sensitive = sensitive,
                Photo = new Photo
                {
                    Filename = "/" + filePath
                }
            };
        }

    }
}