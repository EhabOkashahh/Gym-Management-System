using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSystem.Extension.Interfaces
{
    public interface IFileUploader
    {
        Task<String> UploadFile(IFormFile file);
    }
}