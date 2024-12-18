﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInfoSystem.Infrastructure.Repositories
{
    public class Repository<T> where T : class
    {
        protected readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));  
            _dbSet = _context.Set<T>();  
        }

        
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();  
        }

        
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);  
        }

       
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity); 
        }

        
        public void Update(T entity)
        {
            _dbSet.Update(entity);  
        }

       
        public void Remove(T entity)
        {
            _dbSet.Remove(entity); 
        }

        // Değişiklikleri kaydetmek için
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync(); 
        }
    }
}
