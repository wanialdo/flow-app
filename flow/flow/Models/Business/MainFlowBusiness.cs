using flow.Context;
using flow.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace flow.Models.Business
{
    public class MainFlowBusiness : BaseBusiness<MainFlow>
    {
        public MainFlowBusiness(FlowDbContext db) : base(db)
        {
        }

        public IQueryable<MainFlow> FindBy(string responsible, long status)
        {
            IQueryable<MainFlow> consulta = this._db.MainFlow;

            if (!String.IsNullOrEmpty(responsible))
                consulta = consulta.Where(x => x.Responsible.Equals(responsible))
                                   .Where(x => x.FlowInitStatusID == status);

            return consulta;
        }

    }
}