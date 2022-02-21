using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace AutoFix.Extensions
{
    public static class AppExtensions
    {
        public static string GetUserId(this HttpContext context)
        {
            //Claimler; rollerin dışında kullanıcı hakkında bilgi tutmamızı ve bu bilgilere göre yetkilendirme yapmamızı sağlayan yapılardır
            return context.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

        }
        public static string ToFullErrorString(this ModelStateDictionary modelState)
        {
            var messages = new List<string>();

            foreach (var entry in modelState.Values)
            {
                foreach (var error in entry.Errors)
                    messages.Add(error.ErrorMessage);
            }
            return String.Join("", messages);
        }

        
    }
}
