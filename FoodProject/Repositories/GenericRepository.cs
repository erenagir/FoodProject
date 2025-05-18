using ProductProject.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ProductProject.Repositories
{
    public class GenericRepository<T> where T: class, new() // T mutlaka bir class olmalı ve new sözcüğünü barındırabilmeli.
    {
        public readonly Context _context;

        public GenericRepository(Context context)
        {
            _context = context;
        }

        public List<T> TList()
        {
            return _context.Set<T>().ToList();
        }
        public void TAdd(T p)
        {
            _context.Set<T>().Add(p);
            _context.SaveChanges();
        }
        public void TDelete(T p)
        {
            _context.Set<T>().Remove(p);
            _context.SaveChanges();
        }
        public void TUpdate(T p)
        {
            _context.Set<T>().Update(p);
            _context.SaveChanges();
        }
        public T TGet(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public List<T> TList(string p) // İlgili yiyeceğin kategori adını getirebilmek için
        {
            return _context.Set<T>().Include(p).ToList();
        }
        public List<T> List(Expression<Func<T,bool>> filter) // istediğimiz herhangi sütuna göre listeleme işlemi 
        {
            return _context.Set<T>().Where(filter).ToList();
        }
    }
}
