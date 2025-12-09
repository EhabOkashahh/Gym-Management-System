using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Threading.Tasks;
using GymSystem.Extension.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GymSystem.Extension.Classes
{
    public class ImageUploader : IFileUploader
    {
        private readonly string _Uploadpath = @"wwwroot\Images";
        public async Task<string> UploadFile(IFormFile file)
        {

            var ext = Path.GetExtension(file.FileName);
            var newFileName = $"{Guid.NewGuid()}{ext}";
            var path = Path.Combine(_Uploadpath,newFileName);

            
            using var stream = new FileStream(path , FileMode.Create);
            await file.CopyToAsync(stream);

            return newFileName;
        }
    }
}
