using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Core.Validations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DateGreaterThanAttribute : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "{0} must be equal or greather than {1}.";
        public string OtherPropertyName;

        public DateGreaterThanAttribute(string otherPropertyName)
            : base(DefaultErrorMessage)
        {
            this.OtherPropertyName = otherPropertyName;
        }

        public override string FormatErrorMessage(string propertyName)
        {
            if (ErrorMessage.IsNullOrEmpty())
            {
                return string.Format(DefaultErrorMessage, propertyName, this.OtherPropertyName);
            }
            else
            {
                return ErrorMessage;
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = ValidationResult.Success;
            try
            {
                // Using reflection we can get a reference to the other date property
                var otherPropertyInfo = validationContext.ObjectType.GetProperty(this.OtherPropertyName);

                // Let's check that otherProperty is of type DateTime or DateTime? as we expect it to be
                if (otherPropertyInfo.PropertyType == typeof(DateTime) || otherPropertyInfo.PropertyType == typeof(DateTime?))
                {
                    DateTime? toValidate = null;

                    if (otherPropertyInfo.PropertyType == typeof (DateTime))
                    {
                        toValidate = (DateTime)value;
                    }
                    else if(otherPropertyInfo.PropertyType == typeof(DateTime?))
                    {
                        toValidate = (DateTime?) value;
                    }

                    if (toValidate.HasValue)
                    {
                        DateTime referenceProperty = (DateTime)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null); 
                        if (toValidate.Value < referenceProperty)
                        {
                            var message = FormatErrorMessage(validationContext.DisplayName);
                            validationResult = new ValidationResult(message);
                        }
                    }
                    
                }
                else
                {
                    validationResult = new ValidationResult("An error occurred while validating the property. " + this.OtherPropertyName + " is not of type DateTime.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return validationResult;
        }
        
        //IClientValidatable.GetClientValidationRules Method
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientCustomDateValidationRule(FormatErrorMessage(metadata.DisplayName), "dategreaterthan");
            
            //"otherpropertyname" is the name of the jQuery parameter for the adapter, must be LOWERCASE!
            rule.ValidationParameters.Add("otherpropertyname", this.OtherPropertyName);

            yield return rule;
        }
    }

    

}