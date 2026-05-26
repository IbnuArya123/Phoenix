using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Phoenix.DTO.Validation;

namespace Phoenix.Web.UI.Controllers
{
    public abstract class BaseController : Controller {
        protected IEnumerable<ValidationMessageDTO> GetValidationErrorMessage(ModelStateDictionary modelState) {
            var dto = new List<ValidationMessageDTO>();

            foreach (var property in modelState) { 
                if (property.Value.Errors.Count < 1) {
                    continue;
                } else { 
                    string errorMessage = property.Value.Errors[0].ErrorMessage;
                    dto.Add(new ValidationMessageDTO { 
                        PropertyName = property.Key,
                        ErrorMessage = errorMessage,
                    });
                }
            }
            return dto;
        }
    }
}
