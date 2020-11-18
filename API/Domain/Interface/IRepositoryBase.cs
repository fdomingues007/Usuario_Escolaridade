using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
  public interface IRepositoryBase<T> where T : class
  {
    // Mapeamento Entity Framework

    // Método responsável em trazer uma Lista 
    Task<List<T>> GetAll();

    // Método responsável em trazer uma Lista
    Task<T> GetId(int id);

    // Método responsável em trazer uma Lista
    Task<T> AddAsync(T model);

    // Método para 
    Task<T> UpdateAsync(T model);

    // Método para deletar
    Task<T> DeleteAsync(T model);

    // Método para deletar
    Task<T> DeleteAsync(int id);


    // Mapeamento Dapper

    // Método responsável em trazer uma Lista 
    Task<List<T>> GetAll(T entity, DynamicParameters parameters, string procName);

    //Task<List<Object>> GetAllDinamico(Object entity, DynamicParameters parameters, string procName);

    Task<List<T>> GetAll(T entity, string procName);

    // Método responsável em trazer uma Lista
    Task<T> GetId(T entity, DynamicParameters parameters, string procName);

    // Método responsável em trazer uma Lista
    Task<T> AddAsync(T entity, DynamicParameters parameters, string procName);

    // Método para 
    Task<T> UpdateAsync(T entity, DynamicParameters parameters, string procName);

    // Método para deletar
    Task<T> DeleteAsync(T entity, DynamicParameters parameters, string procName);
  }

}
