using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystem.DAL.Entities;
using GymSystemDAL.Data.Contexts;
using GymSystemDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace GymSystemDAL.Repositories.Classes
{
    public class GenericRepository<TEntity>(AppDbContext _context) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _context.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(int id) =>  await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);

        public async Task AddAsync(TEntity entity) => await _context.Set<TEntity>().AddAsync(entity);
        

        public async Task Delete(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
                return;

            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;

            _context.Update(entity);
        }

        public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);
        
    }
}