using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemBLL.Models.TrainerModels;

namespace GymSystemBLL.Services.Interfaces
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerModelView>> GetAllTrainersAsync();
        Task<TrainerDetailsModelView?> GetTrainerByIdAsync(int? id);
        Task<bool> CreateTrainerAsync(CreateTrainerModelView model);
        Task<bool> UpdateTrainerAsync(int id, UpdateTrainerModelView model);

        Task<bool> RestoreTrainer(int? id);
        Task<bool> SoftDeleteTrainerAsync(int? id);
    }
}