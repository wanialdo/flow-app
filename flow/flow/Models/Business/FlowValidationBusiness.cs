using flow.Context;
using flow.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace flow.Models.Business
{
    public class FlowValidationBusiness : BaseBusiness<FlowValidation>
    {
        public FlowValidationBusiness(FlowDbContext db) : base(db)
        {
        }

        public void PerformValidation(long documentCode, long userCode, string actionName, string details)
        {
            string _complementary = "";

            //Open desired document in database and find last Status to that item
            long destinationStatus = 1; //Initial Status

            FlowValidation lastValidation = base._db.FlowValidation
                .Where(x => x.DocumentCode == documentCode)
                .OrderByDescending(x => x.ID).FirstOrDefault();

            MainFlow flow = base._db.MainFlow.Include("FlowInitStatus").Include("FlowEndStatus")
                                             .Where(x => x.ActionName.Equals(actionName))
                                             .FirstOrDefault(x => x.FlowInitStatusID == lastValidation.StatusID);

            if (lastValidation != null && flow != null)
            {
                destinationStatus = flow.FlowEndStatusID;

                #region email
                //You Can warn user about status change by e-mail.
                #endregion
            }
            else
            {
                throw new Exception("Next step not identified. Please review your flow.");
            }

            _complementary = String.Format("{1} ({2}) {0} - {3:dd/MM/yyyy}.", flow.FlowInitStatus.Description, flow.Responsible, userCode.ToString(), DateTime.Now.Date);

            SaveValidation(documentCode, destinationStatus, details, _complementary, userCode);
        }

        public void SaveValidation(long documentCode, long destinationStatusID, string details, string _complementary, long userCode)
        {
            FlowValidation newValidation = new FlowValidation()
            {
                AnalisysDate = DateTime.Now,
                Description = details,
                ComplementaryInfo = _complementary,
                DocumentCode = documentCode,
                StatusID = destinationStatusID,
                UserCode = userCode
            };
            this._db.FlowValidation.Add(newValidation);
            this._db.SaveChanges();
        }
    }
}