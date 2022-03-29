using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace Library.Core.Validations
{

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DateGreaterThanEqualTodayDateAttribute : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "{0} must be on or after today.";
        public string HiddenInitialDatePropertyName;

        public DateGreaterThanEqualTodayDateAttribute(string hiddenInitialDatePropertyName) 
            : base(DefaultErrorMessage)
        {
            this.HiddenInitialDatePropertyName = hiddenInitialDatePropertyName;
        }

        public override string FormatErrorMessage(string propertyName)
        {
            if (ErrorMessage.IsNullOrEmpty())
            {
                return string.Format(DefaultErrorMessage, propertyName);
            }
            else
            {
                return ErrorMessage;
            }
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            throw new NotImplementedException();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Using reflection we can get a reference to the other date property
            var hiddenInitialDatePropertyNameInfo = validationContext.ObjectType.GetProperty(this.HiddenInitialDatePropertyName);
             
            DateTime? initialDate = null;

            if (hiddenInitialDatePropertyNameInfo.PropertyType == typeof (DateTime))
            {
                initialDate = (DateTime)hiddenInitialDatePropertyNameInfo.GetValue(validationContext.ObjectInstance, null); 
            }
            else if (hiddenInitialDatePropertyNameInfo.PropertyType == typeof (DateTime?))
            {
                initialDate = (DateTime?)hiddenInitialDatePropertyNameInfo.GetValue(validationContext.ObjectInstance, null); 
            }
            else if (hiddenInitialDatePropertyNameInfo.PropertyType == typeof(string))
            {
                string initialDateStr = (string)hiddenInitialDatePropertyNameInfo.GetValue(validationContext.ObjectInstance, null);
                if (initialDateStr.IsNotNullOrEmpty())
                {
                    DateTime dateEntered;
                    bool isValidDate = DateTime.TryParse(initialDateStr, CultureInfo.CreateSpecificCulture("en-AU"), DateTimeStyles.None, out dateEntered);
                    if (isValidDate)
                    {
                        initialDate = dateEntered;
                    }
                }
                
            }

            if (initialDate.HasValue)
            {
                DateTime dateEntered;
                if (value != null)
                {
                    bool isValidDate = DateTime.TryParse(value.ToString(), CultureInfo.CreateSpecificCulture("en-AU"), DateTimeStyles.None, out dateEntered);
                    if (isValidDate)
                    {
                        if (initialDate != dateEntered)
                        {
                            if (dateEntered < DateTime.Today)
                            {
                                var message = FormatErrorMessage(validationContext.DisplayName);
                                return new ValidationResult(message);
                            }
                        }
                    }
                }
            } 
            
            return null;
        }

        //public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        //{
        //    var rule = new ModelClientCustomDateValidationRule(FormatErrorMessage(metadata.DisplayName), "dategreaterthanequaltodaydate");
        //    yield return rule;
        //}
    }

    
}