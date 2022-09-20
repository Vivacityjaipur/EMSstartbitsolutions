using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRepository<T> where T : class
    {
        //IEnumerable<T> GetData();
        //T GetDataById(int id);
        //void AddData(T entity);
        //void EditData(T entity);
        //void DeleteData(T entity);
        Task<IEnumerable<T>> GetData();
        Task<T> GetDataById(int id);
        Task<T> AddData(T entity);
        Task<T> EditData(T entity);
        Task<T> DeleteData(int id);
        Task<IEnumerable<T>> AddMultipleData(IEnumerable<T> entities);
        Task<IEnumerable<T>> EditMultipleData(IEnumerable<T> entities);
        Task<IEnumerable<T>> DeleteMultipleData(IEnumerable<int> ids);
    }
}
