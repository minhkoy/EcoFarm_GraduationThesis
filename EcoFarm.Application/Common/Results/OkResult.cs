using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Application.Common.Results
{
    public class OkResult<T> : Result<T>
    {
        private readonly T _data;
        private readonly string _message;
        public OkResult(T data)
        {
            _data = data;
        }
        public OkResult(T data, string message)
        {
            _data = data;
            _message = message;
        }
        public override T Data => _data;
        public override ResultTypes ResultType => ResultTypes.Ok;
        public IEnumerable<object> Errors => Enumerable.Empty<object>();
        public override string Message => string.Empty;
    }
}
