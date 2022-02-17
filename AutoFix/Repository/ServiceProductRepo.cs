using AutoFix.Data;
using AutoFix.Models.Entities;
using AutoFix.Repository.Abstracts;
using System;

namespace AutoFix.Repository
{
    public class ServiceProductRepo : BaseRepository<ServiceProduct, Guid>
    {
        public ServiceProductRepo(MyContext context) : base(context)
        {

        }
    
    }
}
