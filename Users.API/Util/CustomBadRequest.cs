using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Users.API.Util
{
    public class CustomBadRequest    
    {
        public string Message { get; set; }
        public int ErrorCode { get; set; }

        public CustomBadRequest(ActionContext context)
        {
            ErrorCode = 400;
            this.ConstructErrorMessages(context);
        }

        private void ConstructErrorMessages(ActionContext context)
        {
            foreach (var keyModelStatePair in context.ModelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    Message = errors[0].ErrorMessage; 
                }
            }
        }
    }

}