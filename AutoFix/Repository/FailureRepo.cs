using AutoFix.Data;
using AutoFix.Models.Entities;
using AutoFix.Repository.Abstracts;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace AutoFix.Repository
{
    public class FailureRepo : BaseRepository<FailureLogging, Guid>
    {
        public FailureRepo(MyContext context) : base(context)
        {

        }
       
        public IQueryable<FailureLogging> GetByTechnicianId(string id)
        {
            return Table.Where(x => x.TechnicianId == id && x.FailureStatus == FailureStatus.Yönlendirildi || x.FailureStatus == FailureStatus.Beklemede
            || x.FailureStatus == FailureStatus.HizmetVeriliyor);
        }
        public IQueryable<FailureLogging> IsTech(string id)
        {
            return Table.Where(x => x.TechnicianId == id && x.FailureStatus == FailureStatus.Yönlendirildi);
        }
        public IQueryable<FailureLogging> GetStatus(FailureStatus status)
        {
            return Table.Where(x => x.FailureStatus == status);
            
        }


    }
}
