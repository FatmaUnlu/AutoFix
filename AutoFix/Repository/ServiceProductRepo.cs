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
        //public static string GetServiceProductId()
        //{
        //    //Claimler; rollerin dışında kullanıcı hakkında bilgi tutmamızı ve bu bilgilere göre yetkilendirme yapmamızı sağlayan yapılardır
        //    return

        //}

    }
}
