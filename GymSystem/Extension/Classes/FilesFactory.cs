using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystem.Extension.Interfaces;

namespace GymSystem.Extension.Classes
{
    public class FilesFactory(IServiceProvider _Service)
    {
        public IFileUploader GetFileUploader(String Type)
        {
            return Type switch
            {
                "image/png" => _Service.GetRequiredService<ImageUploader>(),
                "image/jpeg" => _Service.GetRequiredService<ImageUploader>(),
                _ => throw new NotSupportedException("This File Type not Supported")
            };
        }
    }
}