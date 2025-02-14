using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Resyaku.Web.Extensions
{
    public static class Extensions
    {
        public static void AddToModelState(
            this ValidationResult validationResult,
            ModelStateDictionary modelState)
        {
            modelState.Clear();

            foreach (var error in validationResult.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}
