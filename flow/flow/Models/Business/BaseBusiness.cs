using flow.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace flow.Models.Business
{
    public class BaseBusiness<T> : IBusiness<T> where T : class
    {
        protected FlowDbContext _db;

        public BaseBusiness(FlowDbContext db)
        {
            this._db = db;
        }

        public void Delete(T obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(T obj)
        {
            _db.Entry(obj).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public long Save(T obj)
        {
            long _id = 0;

            _db.Entry(obj).State = EntityState.Added;
            _db.SaveChanges();
            _id = Convert.ToInt64(obj.GetType().GetProperty("ID").GetValue(obj));

            return _id;
        }

        public IList<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public IList<T> FindBy(int id)
        {
            throw new NotImplementedException();
        }

    }
}