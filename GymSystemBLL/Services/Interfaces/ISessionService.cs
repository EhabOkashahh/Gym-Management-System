using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemBLL.Models.SessionModels;

namespace GymSystemBLL.Services.Interfaces
{
    public interface ISessionService
    {
        Task<IEnumerable<SessionModelView>> GetAllSessionsAsync();
        Task<DetailsSessionModelView?> GetSessionByIdAsync(int? id);

        Task<bool> CreateSessionAsync(CreateSessionModelView model);
        Task<bool> UpdateSessionAsync(int id, UpdateSessionModelView model);
        Task<bool> DeleteSessionAsync(int? id);
    }
}