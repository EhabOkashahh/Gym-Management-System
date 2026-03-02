using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GymSystem.DAL.Entities;
using GymSystemBLL.Models.TrainerModels;
using GymSystemBLL.Services.Interfaces;
using GymSystemDAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace GymSystemBLL.Services.Classes
{
    public class TrainerService(IUnitOfWork _unitOfWork, IMapper _autoMapper) : ITrainerService
    {
        public async Task<IEnumerable<TrainerModelView>> GetAllTrainersAsync()
        {
            var trainers = await GetRepo().GetAllAsync();
            return _autoMapper.Map<IEnumerable<TrainerModelView>>(trainers);
        }

        public async Task<TrainerDetailsModelView?> GetTrainerByIdAsync(int? id)
        {
            if (id is null) return null;

            var trainer = await GetRepo().GetByIdAsync(id.Value);
            if (trainer is null) return null;

            return _autoMapper.Map<TrainerDetailsModelView>(trainer);
        }
        public async Task<bool> CreateTrainerAsync(CreateTrainerModelView model)
        {
            var res = await FindByEmailOrPhone(model.Phone , model.Email);
            var trainer = _autoMapper.Map<Trainer>(model);

            await GetRepo().AddAsync(trainer);

            return await _unitOfWork.ApplyToDataBaseAsync() > 0;
        }

        public async Task<bool> UpdateTrainerAsync(int id, UpdateTrainerModelView model)
        {
            var trainer = await GetRepo().GetByIdAsync(id);
            if (trainer is null) return false;

            var updatedTrainer = _autoMapper.Map(model, trainer);

            GetRepo().Update(updatedTrainer);
            return await _unitOfWork.ApplyToDataBaseAsync() > 0;
        }

        public async Task<bool> SoftDeleteTrainerAsync(int? id)
        {
            if (id is null) return false;

            var trainer = await GetRepo().GetByIdAsync(id.Value);
            if (trainer is null) return false;

            await GetRepo().SoftDelete(id.Value);
            return await _unitOfWork.ApplyToDataBaseAsync() > 0;
        }
        
        public async Task<bool> RestoreTrainer(int? id)
        {
            if (id is null) return false;
            var trainer = await GetRepo().GetByIdAsync(id.Value);

            if(trainer is null) return false;

            trainer.IsDeleted = false;

            return await _unitOfWork.ApplyToDataBaseAsync() > 0;
            

        }
        private async Task<bool> FindByEmailOrPhone(string phone , string email)
        {
            var Member = await GetRepo().GetAllAsync();
            return Member.Any(m => m.Phone == phone || m.Email == email);
        }
        private IGenericRepository<Trainer> GetRepo()
        {
            return _unitOfWork.GenerateRepository<Trainer>();
        }

       
    }
}