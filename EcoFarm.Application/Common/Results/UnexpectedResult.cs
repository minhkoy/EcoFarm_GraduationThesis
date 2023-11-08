using EcoFarm.Domain.Common.Values.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Common.Results
{
    public class UnexpectedResult<T> : Result<T>
    {
        public override T Data => default;

        public override HelperEnums.ResultTypes ResultType => HelperEnums.ResultTypes.Unexpected;

        public override IEnumerable<object> Errors => Enumerable.Empty<object>();

        public override string Message => string.Empty;
    }
}
