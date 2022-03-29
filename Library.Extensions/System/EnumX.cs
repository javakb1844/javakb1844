using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

public static class EnumX
    {
		#region Methods (4) 

		// Public Methods (4) 

        public static List<EnumMember> GetAllEnumMembers<T>(this Enum en)
        {
            List<EnumMember> res = new List<EnumMember>();
            StringEnum se = new StringEnum(en.GetType());
            Array arr = se.GetStringValues();
            foreach (var v in arr)
            {
                res.Add(new EnumMember
                {
                    Name = Enum.GetName(typeof(T),("".GetEnum<T>(v.ToString()))),
                     StringValue = v.ToString()
                });
            }
            return res;
        }
        public static string[] GetNames<T>(this Enum en)
        {
            var all = en.GetAllEnumMembers<T>();
            return (from a in all
                    select a.Name).ToArray<string>();
        }
        public static string[] GetValues<T>(this Enum en)
        {
            var all = en.GetAllEnumMembers<T>();
            return (from a in all
                    select a.StringValue).ToArray<string>();
        }
        public static string GetStringValue(this Enum e)
        {
            return StringEnum.GetStringValue(e);
        }
        public static string Name(this Enum e)
        {
            return e.ts();
        }
        public static string Value(this Enum e)
        {
            return e.GetStringValue();
        }

        // http://www.codeproject.com/Tips/818547/Use-of-the-enum-description-with-special-character
        /// <summary>
        /// use attribute [Description("an enum description")] on enums
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            // TODO: tokenize into separate words, based on each Capital character
            return Name(en);
            //return en.ToString();
        }

        #endregion Methods 
    }

    public class EnumMember
    {
		#region Properties (2) 

        public string Name
        {
            get;
            set;
        }

        public string StringValue 
        {
            get; set;
        }

		#endregion Properties 
    }

