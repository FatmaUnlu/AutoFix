using AutoFix.Data;
using AutoFix.Models.Entities;
using AutoFix.Repository.Abstracts;
using System;

namespace AutoFix.Repository
{
    public class FailureRepo : BaseRepository<FailureLogging, Guid>
    {
        public FailureRepo(MyContext context) : base(context)
        {

        }
        public FailureLogging GetByTechnicianId(string id)
        {
            return Table.Find(id);
        }
    }
}
