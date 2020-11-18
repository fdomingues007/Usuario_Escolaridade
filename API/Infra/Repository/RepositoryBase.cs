using System;
using System.Collections.Generic;
using System.Text;
using Infra.DB;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using Domain.Interface;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository
{
  public class RepositoryBase<T> : IDisposable, IRepositoryBase<T> where T : class
  {
    public readonly DB.EfContext _efContext;

    public RepositoryBase(EfContext context)
    {
      _efContext = context;
    }

    #region Dapper


    public async Task<List<T>> GetAll(T entity, DynamicParameters parameters, string procName)
    {
      using (var conn = new SqlConnection(Settings.ConnectionString))
      {
        await conn.OpenAsync();
        return SqlMapper.Query<T>(conn, procName, param: parameters, commandTimeout: 0, commandType: System.Data.CommandType.StoredProcedure).ToList();
      }
    }

    public async Task<T> GetId(T entity, DynamicParameters parameters, string procName)
    {
      using (var conn = new SqlConnection(Settings.ConnectionString))
      {
        await conn.OpenAsync();
        return SqlMapper.Query<T>(conn, procName, param: parameters, commandTimeout: 0, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
      }
    }

    public async Task<T> AddAsync(T entity, DynamicParameters parameters, string procName)
    {
      using (var conn = new SqlConnection(Settings.ConnectionString))
      {
        await conn.OpenAsync();
        entity = SqlMapper.Query<T>(conn, procName, param: parameters, commandTimeout: 0, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
        return entity;
      }
    }

    public async Task<T> UpdateAsync(T entity, DynamicParameters parameters, string procName)
    {
      using (var conn = new SqlConnection(Settings.ConnectionString))
      {
        await conn.OpenAsync();
        entity = SqlMapper.Query<T>(conn, procName, param: parameters, commandTimeout: 0, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
        return entity;
      }
    }

    public async Task<T> DeleteAsync(T entity, DynamicParameters parameters, string procName)
    {
      using (var conn = new SqlConnection(Settings.ConnectionString))
      {
        await conn.OpenAsync();
        entity = SqlMapper.Query<T>(conn, procName, param: parameters, commandTimeout: 0, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
        return entity;
      }
    }

    public async Task<List<T>> GetAll(T entity, string procName)
    {
      using (var conn = new SqlConnection(Settings.ConnectionString))
      {
        await conn.OpenAsync();
        return SqlMapper.Query<T>(conn, procName, commandTimeout: 0, commandType: System.Data.CommandType.StoredProcedure).ToList();
      }
    }

    #endregion


    #region Entity Framework

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<List<T>> GetAll()
    {
      return await _efContext.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> GetId(int id)
    {
      return await _efContext.FindAsync<T>(id);
    }

    public async Task<T> AddAsync(T model)
    {
      await _efContext.Set<T>().AddAsync(model);
      await _efContext.SaveChangesAsync();
      return model;
    }

    public async Task<T> UpdateAsync(T model)
    {
      _efContext.Entry(model).State = EntityState.Modified;
      await _efContext.SaveChangesAsync();
      return model;
    }

    public async Task<T> DeleteAsync(T model)
    {
      _efContext.Set<T>().Remove(model);
      await _efContext.SaveChangesAsync();
      return model;
    }

    public void Dispose()
    {
      _efContext?.Dispose();
    }

    public async Task<T> DeleteAsync(int id)
    {
      throw new NotImplementedException();
      //await _efContext.Set<T>().RemoveRange()
      //await _efContext.SaveChangesAsync();
      //return await _efContext.Set<T>(id).Remove();

    }



    #endregion

  }

}
