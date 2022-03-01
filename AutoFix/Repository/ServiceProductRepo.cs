using AutoFix.Data;
using AutoFix.Models.Entities;
using AutoFix.Repository.Abstracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFix.Repository
{
    public class ServiceProductRepo : BaseRepository<ServiceProduct, Guid>
    {
      
        public ServiceProductRepo(MyContext context) : base(context)
        {

        }

        //Claimler; rollerin dışında kullanıcı hakkında bilgi tutmamızı ve bu bilgilere göre yetkilendirme yapmamızı sağlayan yapılardır

        //public virtual ValueTask<TEntity> FindAsync([JetBrains.Annotations.CanBeNull] params object[] keyValues)
        //{
        //    throw new NotImplementedException();
        //}
        


    }
}
