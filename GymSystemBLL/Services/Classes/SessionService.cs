using AutoMapper;
using GymSystem.DAL.Entities;
using GymSystemBLL.Models.SessionModels;
using GymSystemBLL.Services.Interfaces;
using GymSystemDAL.Repositories.Interfaces;

namespace GymSystemBLL.Services.Classes
{
    public class SessionService(IUnitOfWork _unitOfWork, IMapper _mapper) : ISessionService
    {

        public async Task<IEnumerable<SessionModelView>> GetAllSessionsAsync()
        {
            var sessions = await GetRepo().GetAllAsync();
            return _mapper.Map<IEnumerable<SessionModelView>>(sessions);
        }

        public async Task<DetailsSessionModelView?> GetSessionByIdAsync(int? id)
        {
            if (id is null) return null;

            var session = await GetRepo().GetByIdAsync(id.Value);
            if (session is null) return null;

            return _mapper.Map<DetailsSessionModelView>(session);
        }

        public async Task<bool> CreateSessionAsync(CreateSessionModelView model)
        {
            var session = _mapper.Map<Session>(model);
            await GetRepo().AddAsync(session);

            return await _unitOfWork.ApplyToDataBaseAsync() > 0;
        }

        public async Task<bool> UpdateSessionAsync(int id, UpdateSessionModelView model)
        {
            var session = await GetRepo().GetByIdAsync(id);
            if (session is null) return false;

            _mapper.Map(model, session);

            GetRepo().Update(session);

            return await _unitOfWork.ApplyToDataBaseAsync() > 0;
        }

        public async Task<bool> DeleteSessionAsync(int? id)
        {
            if (id is null) return false;

            var session = await GetRepo().GetByIdAsync(id.Value);
            if (session is null) return false;

            await GetRepo().Delete(id.Value);

            return await _unitOfWork.ApplyToDataBaseAsync() > 0;
        }

        private IGenericRepository<Session> GetRepo()
        {
            return _unitOfWork.GenerateRepository<Session>();
        }
    }
}