using EcoFarm.Domain.Common.Values.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Common.Results
{
    public class RedirectResult<T> : Result<T>
    {
        private readonly string _url;
        private readonly T data;
        public override T Data => default;

        public override HelperEnums.ResultTypes ResultType => HelperEnums.ResultTypes.Redirect;

        public IEnumerable<object> Errors => Enumerable.Empty<object>();

        public override string Message => _url;

        public RedirectResult(string url, T data)
        {
            _url = url;
            this.data = data;
        }
    }
}
