using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly EMSDataContext _context;
        protected  DbSet<T> dbset;
        public Repository(EMSDataContext context)
        {
            _context = context;
            this.dbset = _context.Set<T>();
        }
        public  async Task<IEnumerable<T>> GetData()
        {
            try
            {
                return await dbset.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<T>();
            }
        }
        public async Task<T> GetDataById(int id)
        {
            try
            { var c = await dbset.FindAsync(id);
              
                return c;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<T> AddData(T entity)
        {
            try
            {
                var result = await dbset.AddAsync(entity);
                return result.Entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public async Task<T> EditData(T value)
        {
            try
            {
                if(value != null)
                {   await Task.Run(()=> {
                    dbset.Update(value);
                });
                    return value;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<T> DeleteData(int id)
        {
            try
            {
                var exist = await dbset.FindAsync(id);
                if (exist != null)
                {
                    await Task.Run(() => dbset.Remove(exist));
                    return exist;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<IEnumerable<T>> AddMultipleData(IEnumerable<T> entities)
        {
            try
            {
                List<T> list = new List<T>();
                foreach (var i in entities)
                {
                    var result = await this.AddData(i);
                    if (result != null)
                    {
                        list.Add(result);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<IEnumerable<T>> EditMultipleData(IEnumerable<T> entities)
        {
            try
            {
                List<T> list = new List<T>();
                foreach(var i in entities)
                {
                   var result =await this.EditData(i);
                    if (result != null)
                    {
                        list.Add(result);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<IEnumerable<T>> DeleteMultipleData(IEnumerable<int> ids)
        {
            try
            {
                List<T> list = new List<T>();
                foreach (var i in ids)
                {
                    var result = await this.DeleteData(i);
                    if (result != null)
                    {
                        list.Add(result);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
