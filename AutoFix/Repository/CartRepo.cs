using AutoFix.Data;
using AutoFix.Models.Entities;
using AutoFix.Repository.Abstracts;
using System;
using System.Linq;

namespace AutoFix.Repository
{
    public class CartRepo : BaseRepository<CartItem, Guid>
    {
        public CartRepo(MyContext context) : base(context)
        {

        }
        //satılan ürünleri getirme
        //public IQueryable<CartItem> SoldProducts()
        //{
        //    return Table.Where(x => x.OrderStatus == OrderStatus.Odendi.ToString()).Select(x => x.ServiceProductId);
        //}
        //public IQueryable<CartItem> SoldProducts()
        //{
            

        //    var query = Context.ShoppingCarts.Where(x => x.OrderStatus == OrderStatus.Odendi.ToString()).

        //}
    }
}
