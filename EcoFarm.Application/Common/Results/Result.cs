using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Application.Common.Results
{
    public abstract class Result<T> : Ardalis.Result.Result<T>
    {
        public abstract T Data { get; }
        public abstract ResultTypes ResultType { get; }
        //public abstract IEnumerable<object> Errors { get; }
        public abstract string Message { get; }
    }
}
