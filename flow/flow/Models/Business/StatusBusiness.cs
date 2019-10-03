using flow.Context;
using flow.Models.Entities;
using System;
using System.Linq;

namespace flow.Models.Business
{
    public class StatusBusiness : BaseBusiness<Status>
    {
        public StatusBusiness(FlowDbContext db) : base(db)
        {
        }

        public IQueryable<Status> FindBy(string description = "")
        {
            IQueryable<Status> consulta = this._db.Status;

            if (!String.IsNullOrEmpty(description))
                consulta = consulta.Where(x => x.Description.Contains(description));

            return consulta;
        }
    }
}