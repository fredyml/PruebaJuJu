﻿using Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Infrastructure.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {

        /// <summary>
        /// Contexto
        /// </summary>
        JujuTestContext _context;
        /// <summary>
        /// Entidad
        /// </summary>
        protected DbSet<TEntity> _dbSet;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public Repository(JujuTestContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }


        /// <summary>
        /// Consulta todas las entidades
        /// </summary>
        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }


        /// <summary>
        /// Consulta una entidad por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity FindById(object id)
        {
            return _dbSet.Find(id);
        }



        /// <summary>
        /// Crea un entidad (Guarda)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Create(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();

            return entity;
        }



        /// <summary>
        /// Actualiza la entidad (GUARDA)
        /// </summary>
        /// <param name="editedEntity">Entidad editada</param>
        /// <param name="originalEntity">Entidad Original sin cambios</param>
        /// <param name="changed">Indica si se modifico la entidad</param>
        /// <returns></returns>
        public virtual TEntity Update(TEntity editedEntity)
        {

            if (editedEntity == null)
            {
                throw new ArgumentNullException(nameof(editedEntity));
            }

            _dbSet.Update(editedEntity);
            _context.SaveChanges();

            return editedEntity;
        }



        /// <summary>
        /// Elimina una entidad (Guarda)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);

            _context.SaveChangesAsync();

            return entity;
        }


        /// <summary>
        /// Guardar cambios
        /// </summary>
        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
