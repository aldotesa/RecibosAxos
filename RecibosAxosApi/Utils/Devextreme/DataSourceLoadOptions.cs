using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace RecibosAxosApi.Utils.Devextreme
{
    [ModelBinder(typeof(DataSourceLoadOptionsHttpBinder))]
    public class DataSourceLoadOptions : DataSourceLoadOptionsBase { }

    class DataSourceLoadOptionsHttpBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var loadOptions = new DataSourceLoadOptions();
            DataSourceLoadOptionsParser.Parse(loadOptions, key => bindingContext.ValueProvider.GetValue(key)?.AttemptedValue);
            bindingContext.Model = loadOptions;
            return true;
        }

        //public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        //{
        //    var loadOptions = new DataSourceLoadOptions();
        //    DataSourceLoadOptionsParser.Parse(loadOptions, key => bindingContext.ValueProvider.GetValue(key)?.AttemptedValue);
        //    bindingContext.Model = loadOptions;
        //    return true;
        //}
    }
}