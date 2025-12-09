using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystem.DAL.Entities;
using GymSystemDAL.Data.Contexts;
using GymSystemDAL.Repositories.Interfaces;
using Microsoft.VisualBasic;

namespace GymSystemDAL.Repositories.Classes
{
    public class UnitOfWork(AppDbContext _context) : IUnitOfWork
    {
        public ConcurrentDictionary<Type,object> ReposContainer { get; set; } = new ConcurrentDictionary<Type,object>();
        public IGenericRepository<T> GenerateRepository<T>() where T : BaseEntity
        {
            var repo = (IGenericRepository<T>)ReposContainer.GetOrAdd(typeof(T),(Type) => new GenericRepository<T>(_context) );
            return repo;
        }

        public async Task<int> ApplyToDataBaseAsync() => await _context.SaveChangesAsync();

    }
}