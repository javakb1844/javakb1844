using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    public class Result<T>
    {
        public ResultType ResultType { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public List<ValidationResult> ValidationErrors { get; set; }

        public Exception Exception;

        public MethodOptions Options { get; set; }

        public Result<TData> Duplicate<TData>()
        {
            var response = new Result<TData>
            {
                Message = this.Message,
                Options = this.Options,
                ResultType = this.ResultType,
                ValidationErrors = this.ValidationErrors                
            };

            return response;
        }
    }
}
