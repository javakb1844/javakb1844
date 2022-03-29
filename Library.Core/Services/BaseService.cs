using Library.Core.Logging;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    public class BaseService
    {
        #region Validation
        public List<ValidationResult> Validate(object model)
        {
            var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }
        #endregion

        #region Returns
        public Result<T> ReturnException<T>(Exception error)
        {
            string message = "Service Error";
            if (error != null)
            {
                if (error.InnerException != null && error.InnerException.Message.IsNotNullOrEmpty())
                {
                    message = error.InnerException.Message;
                }
                else
                {
                    if (error.Message.IsNotNullOrEmpty())
                    {
                        message = error.Message;
                    }
                }

                try
                {
                    var logger = GetLogger("ReturnException", true);
                    if (logger != null)
                    {
                        logger.AddErrorString("ReturnException: Exception Message", message);
                        logger.AddErrorString("ReturnException: Exception Stack Trace", error.StackTrace.ToString());

                        if (error.InnerException != null)
                        {
                            logger.AddErrorString("ReturnException: Inner Exception Stack Trace", error.InnerException.StackTrace.ToString());
                        }

                        logger.Save();
                    }
                }
                catch (Exception)
                { } 

            }

            return new Result<T>
            { 
                Exception = error,
                Message = message,
                ResultType = ResultType.Exception 
            };
        }

        public Result<T> ReturnSuccess<T>(T data)
        {
            return new Result<T>
            {                
                ResultType = ResultType.Success,
                Data = data,
                ValidationErrors = null
            };
        }

        public Result<T> ReturnErrorMessage<T>(string errorMessage)
        {
            return new Result<T>
            {
                ResultType = ResultType.Failure,
                Message = errorMessage
            };
        }

        public Result<T> ReturnEmpty<T>(T data)
        {
            return new Result<T>
            {
                ResultType = ResultType.Empty,
                Data = default(T),
                ValidationErrors = null
            };
        }

        public Result<T> ReturnFailure<T>(T data)
        {
            return new Result<T>
            {
                ResultType = ResultType.Failure,
                Data = default(T),
                ValidationErrors = null
            };
        }

        public Result<T> ReturnValidationErrors<T>(List<ValidationResult> errors)
        {
            return new Result<T>
            {
                ResultType = ResultType.ValidationErrors,
                ValidationErrors = errors
            };
        }

        public Result<T> ReturnValidationErrors<T>(params ValidationResult[] errors)
        {
            return new Result<T>
            {
                ResultType = ResultType.ValidationErrors,
                ValidationErrors = errors.ToList()
            };
        }
        #endregion

        protected LogBuilder GetLogger(string functionName, bool enabled) 
        {
            return new LogBuilder(functionName, enabled, "LogContext".Connectionstring());
        }
    }
}
