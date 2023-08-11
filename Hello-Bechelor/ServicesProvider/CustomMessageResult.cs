using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello_Bechelor.ServicesProvider
{
    public class CustomMessageResult : ActionResult
    {
        private readonly string _message;

        public CustomMessageResult(string message)
        {
            _message = message;
        }

        public override void ExecuteResult(ActionContext context)
        {
            var result = new ContentResult
            {
                Content = _message,
                ContentType = "text/plain",
                StatusCode = 200
            };
            result.ExecuteResult(context);
        }
    }
}
