using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Application.Common.Results
{
    public class UnauthorizedResult<T> : Result<T>
    {
        private readonly string _message;
        public override T Data => default;

        public override IEnumerable<object> Errors => Enumerable.Empty<object>();

        public override string Message => _message ?? "Vui lòng đăng nhập để tiếp tục";

        public override ResultTypes ResultType => ResultTypes.Unauthorized;

        public UnauthorizedResult(string message)
        {
            _message = message;
        }
    }
}
