using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Application.Common.Results
{
    public class ForbiddenResult<T> : Result<T>
    {
        private readonly string _message;
        public override T Data => default;

        public override IEnumerable<object> Errors => Enumerable.Empty<object>();

        public override string Message => _message ?? "Bạn không có quyền truy cập. Vui lòng liên hệ quản trị viên nếu nghi ngờ có sự nhầm lẫn";

        public override ResultTypes ResultType => ResultTypes.Forbidden;
        public ForbiddenResult(string message)
        {
            _message = message;
        }
    }
}
