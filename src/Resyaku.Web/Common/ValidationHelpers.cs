using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Resyaku.Web.Common
{
    public static class ValidationHelpers
    {
        public static bool HasError(ViewDataDictionary viewData, string propertyName)
        {
            return viewData.ModelState[propertyName]?.Errors.Count > 0;
        }

        public static string ErrorClass(this ViewDataDictionary viewData, string propertyName, string errorClass)
        {
            return HasError(viewData, propertyName) ? errorClass : "";
        }
    }
}
