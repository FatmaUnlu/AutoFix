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
            return Table.Where(x => x.TechnicianId == id &&x.FailureStatus== FailureStatus.Yönlendirildi.ToString()||x.FailureStatus==FailureStatus.Beklemede.ToString()
            || x.FailureStatus == FailureStatus.HizmetVeriliyor.ToString());
        }


    }
}
