using AutoFix.Data;
using AutoFix.Models.Entities;
using AutoFix.Repository.Abstracts;
using System;

namespace AutoFix.Repository
{
    public class CartRepo : BaseRepository<CartItem, Guid>
    {
        public CartRepo(MyContext context) : base(context)
        {

        }
    }
}
