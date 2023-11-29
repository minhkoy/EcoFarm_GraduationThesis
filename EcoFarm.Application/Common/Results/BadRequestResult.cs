using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Application.Common.Results
{
    public class BadRequestResult<T> : Result<T>
    {
        private readonly string _message;
        private readonly IEnumerable<object> _errors;
        //Generate the result quite same as the OkResult
        public override T Data => default;
        public override ResultTypes ResultType => ResultTypes.BadRequest;
        public IEnumerable<object> Errors => _errors;
        public override string Message => _message;
        public BadRequestResult() { }
        public BadRequestResult(string message, IEnumerable<object> errors)
        {
            _message = message;
            _errors = errors;
        }

    }
}
