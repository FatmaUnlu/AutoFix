using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Threading.Tasks;

namespace AutoFix.Extensions
{
    [ModelBinder(BinderType = typeof(DataSourceLoadOptionsBinder))]
    public class DataSourceLoadOptions: DataSourceLoadOptionsBase
    {       
        public class DataSourceLoadOptionsBinder :IModelBinder
        {
            public Task BindModelAsync(ModelBindingContext bindingContext)
            {
                var LoadOptions = new DataSourceLoadOptions();
                DataSourceLoadOptionsParser.Parse(LoadOptions, key => bindingContext.ValueProvider.GetValue(key).FirstOrDefault());
                bindingContext.Result = ModelBindingResult.Success(LoadOptions);
                return Task.CompletedTask;
            }
        }
    }
}
