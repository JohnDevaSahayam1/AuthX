using AuthX.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthX.Infrastructure.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<T>> GetAllAsync()
            => _repository.GetAllAsync();

        public Task<T> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task<T> CreateAsync(T entity)
            => _repository.AddAsync(entity);

        public Task<T> UpdateAsync(T entity)
            => _repository.UpdateAsync(entity);

        public Task<bool> DeleteAsync(int id)
            => _repository.DeleteAsync(id);
    }
}
