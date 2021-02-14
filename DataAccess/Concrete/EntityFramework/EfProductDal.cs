using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            //IDisposable pattern implementation of c#
            using (NortwindContext contex=new NortwindContext())
            {
                var addedEntity = contex.Entry(entity);
                addedEntity.State = EntityState.Added;
                contex.SaveChanges();
            }
        }

        public void Delete(Product entity)
        {
            using (NortwindContext contex = new NortwindContext())
            {
                var deletedEntity = contex.Entry(entity);
               deletedEntity.State = EntityState.Deleted;
                contex.SaveChanges();
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using(NortwindContext context=new NortwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using(NortwindContext context=new NortwindContext())
            {
                return filter == null
                    ? context.Set<Product>().ToList()
                    : context.Set<Product>().Where(filter).ToList();

            }
        }

        public void Update(Product entity)
        {
            using (NortwindContext contex = new NortwindContext())
            {
                var updatedEntity = contex.Entry(entity);
               updatedEntity.State = EntityState.Modified;
                contex.SaveChanges();
            }
        }
    }
}
