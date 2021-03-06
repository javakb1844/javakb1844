using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Reflection;
using System.Web;

public static class StringX
{
    #region Methods (48)

    public static string AddQuerystringElement(this string url,
                        string newElement, string elementValue)
    {

        string newElementEq = string.Format("{0}=", newElement);
        if (url.Contains(newElementEq))
        {
            string p = url.Substring(0, (url.IndexOf(newElementEq) + newElementEq.Length));
            string n = url.Substring(p.Length);
            if (!n.Contains('&'))
            {
                return string.Format("{0}{1}", p, elementValue);
            }
            else
            {
                return string.Format("{0}{1}{2}", p, elementValue, n.Substring(n.IndexOf('&') + 1));
            }
        }

        //if & was in the end
        if (url.EndsWith("&")
                || url.EndsWith("?"))
            return string.Format("{0}{1}={2}", url, newElement, elementValue);

        //if there was and & but not in the end
        if (url.Contains("&")
                && !url.EndsWith("&"))
        {
            return string.Format("{0}&{1}={2}",
                    url, newElement, elementValue);
        }

        if (url.Contains("?"))
            return string.Format("{0}&{1}={2}", url, newElement, elementValue);

        return string.Format("{0}?{1}={2}", url, newElement, elementValue);

    }

    public static string AddToQuerystring(this string BADIDEA,
        string url, string newElement, string elementValue)
    {
        //if & was in the end
        if (url.EndsWith("&")
            || url.EndsWith("?"))
            return string.Format("{0}{1}={2}", url, newElement, elementValue);

        //if there was and & but not in the end
        if (url.Contains("&")
            && !url.EndsWith("&"))
        {
            return string.Format("{0}&{1}={2}",
                url, newElement, elementValue);
        }

        if (url.Contains("?"))
            return string.Format("{0}&{1}={2}", url, newElement, elementValue);

        return string.Format("{0}?{1}={2}", url, newElement, elementValue);

    }

    public static string AppendCodeMember(this string str,
        string code, string memberDefinition, string textToAppendAfter)
    {
        string prev = string.Empty;
        string next = string.Empty;
        int count = -1;
        prev.TraceCodeMember(code, memberDefinition,
            out prev, out next, out count);
        return code.Insert(prev.Length + count, textToAppendAfter);
    }

    public static string BoolToYesNo(this string s, bool theBool)
    {
        return theBool ? "Yes" : "No";
    }

    /// <summary>
    /// Counts a Specific character whithin a string
    /// </summary>
    /// <param name="source">The Source String</param>
    /// <param name="TheChar">The Character to be checked</param>
    /// <returns></returns>
    public static int CountOccurance(this string gargabe, string source, char theChar)
    {
        int Ret = 0;
        foreach (char c in source)
        {
            if (c == theChar)
            {
                Ret++;
            }
        }
        return Ret;
    }

    /// <summary>
    /// Counts a Specific string whithin a string
    /// </summary>
    /// <param name="gargabe">nothing</param>
    /// <param name="source">source string</param>
    /// <param name="stringToCount">string to count</param>   
    /// <param name="indexesOfOccurance">indexes where that string occurs</param>
    /// <returns></returns>
    public static int CountOccurance(this string gargabe, string source,
        string stringToCount, out List<int> indexesOfOccurance)
    {
        int counted = 0;
        int skip = 0;
        string target = stringToCount;
        indexesOfOccurance = new List<int>();
        for (int c = 0; c <= (source.Length - stringToCount.Length); c++)
        {
            if (skip > 0)
            {
                skip--;
                continue;
            }

            string current = source.Substring(c, stringToCount.Length);

            if (current == target)
            {
                skip = current.Length;
                counted++;
                indexesOfOccurance.Add(c);
            }
        }
        return counted;
    }

    /// <summary>
    /// Counts a Specific string whithin a string
    /// </summary>
    /// <param name="gargabe">nothing</param>
    /// <param name="source">source string</param>
    /// <param name="stringToCount">string to count</param>
    /// <param name="ignoreCase">ignore case</param>
    /// <param name="indexesOfOccurance">indexes where that string occurs</param>
    /// <returns></returns>
    public static int CountOccurance(this string gargabe, string source,
        string stringToCount, bool ignoreCase, out List<int> indexesOfOccurance)
    {
        int counted = 0;
        int skip = 0;
        string target = ignoreCase ? stringToCount.ToLower() : stringToCount;
        indexesOfOccurance = new List<int>();
        for (int c = 0; c <= (source.Length - stringToCount.Length); c++)
        {
            if (skip > 0)
            {
                skip--;
                continue;
            }

            string current = ignoreCase ? source.Substring(c, stringToCount.Length).ToLower()
                : source.Substring(c, stringToCount.Length);

            if (current == target)
            {
                skip = current.Length;
                indexesOfOccurance.Add(c);
                counted++;
            }
        }
        return counted;
    }

    public static string Format(this string item, params object[] parameters)
    {
        return String.Format(item, parameters);
    }
    public static string FormatIt(this string item, params object[] parameters)
    {
        return String.Format(item, parameters);
    }
    public static string Formatt(this string empty,
        string target, params string[] paramPairs)
    {
        StringBuilder sb = new StringBuilder(target);

        for (int i = 0; i < paramPairs.Length; i += 2)
        {
            string p = paramPairs[i];
            string v = paramPairs[i + 1];

            sb.Replace(p, v);
        }

        return sb.ToString();
    }

    public static T GetEnum<T>(this string na)
    {
        try
        {
            return (T)StringEnum.Parse(typeof(T), na);
        }
        catch
        {
            return Enum.Parse(typeof(T), na, true).CastTo<T>();
        }
    }

    public static T GetEnum<T>(this string na, string value)
    {
        try
        {
            return (T)StringEnum.Parse(typeof(T), value);
        }
        catch
        {
            return Enum.Parse(typeof(T), value, true).CastTo<T>();
        }
    }
    public static string NewLine(this string garbabe)
    {
        return "\r\n";
    }

    public static T ParseToEnum<T>(this string stringValue)
    {
        try
        {
            return "".GetEnum<T>(stringValue);
        }
        catch
        {
            return Enum.Parse(typeof(T), stringValue, true).CastTo<T>();
        }
    }

    public static string Pascalize(
        this String str,
        string name)
    {
        string pascalName = string.Empty;
        string notStartingAlpha = Regex.Replace(name, "^[^a-zA-Z]+", string.Empty);
        string workingString = ToLowerExceptCamelCase(notStartingAlpha);
        pascalName = RemoveSeparatorAndCapNext(workingString);
        return pascalName;
    }
    public static string Pascalize(this String str)
    {
        string pascalName = string.Empty;
        string notStartingAlpha = Regex.Replace(str, "^[^a-zA-Z]+", string.Empty);
        string workingString = ToLowerExceptCamelCase(notStartingAlpha);
        pascalName = RemoveSeparatorAndCapNext(workingString);
        return pascalName;
    }
    //public static string Pluralize(this string x, String s)
    //{
    //    //if (string.Format("{0}_plural", s).AppSetting(string.Empty).IsNullOrEmpty().Not())
    //    //{
    //    //    return string.Format("{0}_plural", s).AppSetting(string.Empty);
    //    //}
    //    return Formatting.Pluralize(s);
    //}

    public static string RemoveCodeMember(string str,
        string code, string memberDefinition)
    {
        string prev = string.Empty;
        string next = string.Empty;
        int count = -1;
        prev.TraceCodeMember(code, memberDefinition,
            out prev, out next, out count);
        return string.Format("{0}{1}", prev, next);
    }

    public static string RemoveQuotes(this string str)
    {
        return str.Replace("'", string.Empty).Replace("\"", string.Empty);
    }

    /// <summary>
    /// Removes the separator and capitalises next character.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns></returns>
    public static string RemoveSeparatorAndCapNext(string input)
    {
        char[] splitter = new char[] { '-', '_', ' ' }; // potential chars to split on
        string workingString = input.TrimEnd(splitter);
        char[] chars = workingString.ToCharArray();

        if (chars.Length > 0)
        {
            int under = workingString.IndexOfAny(splitter);
            while (under > -1)
            {
                chars[under + 1] = Char.ToUpper(chars[under + 1], CultureInfo.InvariantCulture);
                workingString = new String(chars);
                under = workingString.IndexOfAny(splitter, under + 1);
            }

            chars[0] = Char.ToUpper(chars[0], CultureInfo.InvariantCulture);

            workingString = new string(chars);
        }
        string regexReplacer = "[" + new string(true ? new char[] { '-', '_', ' ' } : new char[] { ' ' }) + "]";

        return Regex.Replace(workingString, regexReplacer, string.Empty);
    }

    public static string RemoveXmlSection(this string instance, string xml, string sectionStart)
    {
        //presever the text to remove
        string prev = xml.Substring(0, xml.IndexOf(sectionStart));
        //remove till the section start
        xml = xml.Remove(0, xml.IndexOf(sectionStart));
        //get section name
        string endSection = string.Format("</{0}>", xml.Substring((xml.IndexOf("<") + 1),
            (xml.IndexOf(" ") - (xml.IndexOf("<") + 1))));
        xml.Remove(0, xml.IndexOf(endSection));
        return string.Format("{0}{1}", prev, xml);
    }

    //public static string Singularize(this string x, String s)
    //{
    //    if (string.Format("{0}_singular", x).AppSetting(string.Empty).IsNullOrEmpty().Not())
    //    {
    //        return string.Format("{0}_singular", x).AppSetting(string.Empty);
    //    }

    //    return Singularize(s);
    //}

    //public static string Singularize(this string x)
    //{
    //    if (string.Format("{0}_singular", x).AppSetting(string.Empty).IsNullOrEmpty().Not())
    //    {
    //        return string.Format("{0}_singular", x).AppSetting(string.Empty);
    //    }

    //    return Pluralizer.ToSingular(x);
    //}

    public static List<string> Split(this string instance,
        string stringToSplit, string splitter)
    {
        return stringToSplit.Split(new string[] { splitter }, StringSplitOptions.None).ToList<string>();
    }

    public static List<T> Split<T>(this string instance, string delimiter)
    {
        var v = instance.Split(new string[] { delimiter }, StringSplitOptions.None).ToListSafely<String>();
        List<T> resultList = new List<T>();
        foreach (string s in v)
        {
            resultList.Add(s.CastTo<T>());
        }
        return resultList;
    }


    public static List<int> SplitToInt32List(this string instance, string delimiter = ",")
    {
        if (instance.IsNullOrEmpty()) return new List<int>();

        var v = instance.Split(new string[] { delimiter }, StringSplitOptions.None).ToListSafely<String>();
        List<int> resultList = new List<int>();
        foreach (string s in v)
        {
            resultList.Add(Convert.ToInt32(s));
        }
        return resultList;
    }


    public static List<long> SplitToInt64List(this string instance, string delimiter = ",")
    {
        if (instance.IsNullOrEmpty()) return new List<long>();

        var v = instance.Split(new string[] { delimiter }, StringSplitOptions.None)
            .ToListSafely<String>();
        List<long> resultList = new List<long>();
        foreach (string s in v)
        {
            resultList.Add(Convert.ToInt64(s));
        }
        return resultList;
    }


    public static List<string> SplitToStringList(this string instance, string delimiter)
    {
        return instance.Split(new string[] { delimiter }, StringSplitOptions.None).ToListSafely<String>();
    }
    //public static string ToSentence(this string stringWithUpperCharacters)
    //{
    //    StringBuilder result = new StringBuilder();
    //    bool first = true;
    //    foreach (char c in stringWithUpperCharacters)
    //    {
    //        if (Char.IsUpper(c) && !first)
    //        {
    //            result.Append(string.Format(" {0}"));
    //        }
    //        else
    //        {
    //            result.Append(c.ToString());
    //        }

    //            first = false;
    //    }
    //    return result.ToString();
    //}
    /// <summary>
    /// replace space with empty
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string SpaceToEmpty(this string str)
    {
        return str.Replace(" ", string.Empty);
    }

    /// <summary>
    /// replace space with underscore
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string SpaceToUnderScore(this string str)
    {
        return str.Replace(" ", "_");
    }

    /// <summary>
    /// Trancate long strings with ... in the end.
    /// </summary>
    /// <param name="maxChars"></param>
    /// <returns></returns>
    public static string Truncate(this string value, int maxChars)
    {
        if (value.IsNullOrEmpty())
            return "";


        return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
    }



    /// <summary>
    /// Converts to lower except camel case.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns></returns>
    public static string ToLowerExceptCamelCase(string input)
    {
        char[] chars = input.ToCharArray();
        char[] origChars = input.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            int left = (i > 0 ? i - 1 : i);
            int right = (i < chars.Length - 1 ? i + 1 : i);

            if (i != left &&
                    i != right)
            {
                if (Char.IsUpper(chars[i]) &&
                        Char.IsLetter(chars[left]) &&
                        Char.IsUpper(chars[left]))
                {
                    chars[i] = Char.ToLower(chars[i], CultureInfo.InvariantCulture);
                }
                else if (Char.IsUpper(chars[i]) &&
                            Char.IsLetter(chars[right]) &&
                            Char.IsUpper(chars[right]) &&
                            Char.IsUpper(origChars[left]))
                {
                    chars[i] = Char.ToLower(chars[i], CultureInfo.InvariantCulture);
                }
                else if (Char.IsUpper(chars[i]) &&
                            !Char.IsLetter(chars[right]))
                {
                    chars[i] = Char.ToLower(chars[i], CultureInfo.InvariantCulture);
                }
            }

            string x = new string(chars);
        }

        if (chars.Length > 0)
        {
            chars[chars.Length - 1] = Char.ToLower(chars[chars.Length - 1], CultureInfo.InvariantCulture);
        }

        return new string(chars);
    }

    public static string ToParagraph(this string s, List<string> words)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < words.Count; i++)
        {
            sb.Append(words[i]);
            if (i < words.Count - 1)
                sb.Append(" ");
        }
        return sb.ToString();
    }

    public static string ToParagraph(this string s, string[] words)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < words.Length; i++)
        {
            sb.Append(words[i]);
            if (i < words.Length - 1)
                sb.Append(" ");
        }
        return sb.ToString();
    }

    public static void TraceCodeMember(this string str,
        string code, string memberStartText,
        out string previousText,
        out string nextText,
        out int indexOfEndBrace)
    {
        string prev = code.Substring(0, code.IndexOf(memberStartText));
        string next = code.Substring(code.IndexOf(memberStartText));
        int starts = 0;
        int count = 0;
        string toRemove = string.Empty;
        bool firstEncount = false;
        foreach (char c in next)
        {
            count++;
            toRemove += c.ToString();
            if (c == '{')
            {
                starts++;
            }
            if (c == '}')
            {
                if (c != 0)
                {
                    starts--;
                }
            }
            if (firstEncount && starts == 0)
            {
                break;
            }
            if (starts == 1)
            {
                firstEncount = true;
            }
        }

        previousText = prev;
        nextText = code.Substring(prev.Length + count);
        indexOfEndBrace = count;
    }

    /// <summary>
    /// Trims spaces from the end
    /// </summary>
    /// <param name="bogus"></param>
    /// <param name="target"></param>
    /// <param name="removeThis"></param>
    /// <returns></returns>
    public static string TrimEnd(this string bogus, string source)
    {
        //trim space in the start
        if (source.EndsWith(" "))
        {
            bool spaceTrimed = false;
            while (!spaceTrimed)
            {
                source = source.TrimEnd(' ');
                if (!source.EndsWith(" "))
                    spaceTrimed = true;
            }
        }
        return source;
    }

    /// <summary>
    /// Trims a string from the end of a string
    /// </summary>
    /// <param name="bogus"></param>
    /// <param name="target"></param>
    /// <param name="removeThis"></param>
    /// <returns></returns>
    public static string TrimEnd(this string bogus, string source, string removeThis, bool allOccurances)
    {
        source = "".TrimEnd(source);
        removeThis = "".TrimEnd(removeThis);
        if (source.EndsWith(removeThis))
        {
            if (allOccurances)
            {
                while (source.EndsWith(removeThis))
                {
                    source = source.Remove(source.Length - removeThis.Length, removeThis.Length);
                }
            }
            else
            {
                source = source.Remove(source.Length - removeThis.Length, removeThis.Length);
            }
        }
        return source;
    }

    public static string TrimLeft(this string source, string toTrim)
    {
        return "".TrimStart(source, toTrim, false);
    }

    public static string TrimRight(this string source, string toTrim)
    {
        return "".TrimEnd(source, toTrim, false);
    }

    /// <summary>
    /// Trims spaces from the start
    /// </summary>
    /// <param name="bogus"></param>
    /// <param name="target"></param>
    /// <param name="removeThis"></param>
    /// <returns></returns>
    public static string TrimStart(this string bogus, string source)
    {
        //trim space in the start
        if (source.StartsWith(" "))
        {
            bool spaceTrimed = false;
            while (!spaceTrimed)
            {
                source = source.TrimStart(' ');
                if (!source.StartsWith(" "))
                    spaceTrimed = true;
            }
        }
        return source;
    }

    /// <summary>
    /// Trims a string from the start of a string
    /// </summary>
    /// <param name="bogus"></param>
    /// <param name="target"></param>
    /// <param name="removeThis"></param>
    /// <returns></returns>
    public static string TrimStart(this string bogus, string source, string removeThis, bool allOccurances)
    {
        source = "".TrimStart(source);
        removeThis = "".TrimStart(removeThis);
        if (source.StartsWith(removeThis))
        {
            if (allOccurances)
            {
                while (source.StartsWith(removeThis))
                {
                    source = source.Remove(0, removeThis.Length);
                }
            }
            else
            {
                source = source.Remove(0, removeThis.Length);
            }
        }
        return source;
    }

    public static string WithFirstCharLower(this string word)
    {
        return "".WithFirstCharLower(word);
    }

    public static string WithFirstCharLower(this string s, string word)
    {
        return string.Format("{0}{1}", word.Substring(0, 1).ToLower(), word.Substring(1));
    }

    public static string WithFirstCharUpper(this string word)
    {
        return "".WithFirstCharUpper(word);
    }

    public static string WithFirstCharUpper(this string s, string word)
    {
        return string.Format("{0}{1}", word.Substring(0, 1).ToUpper(), word.Substring(1));
    }

    public static bool IsInQuery(this string element)
    {
        return HttpContext.Current.Request[element] != null;
    }

    public static bool CompareWithoutCase(this string element, string strCompareWith)
    {
        if (string.Compare(element, strCompareWith, true) == 0)
            return true;

        return false;
    }

    public static bool CompareWithCase(this string element, string strCompareWith)
    {
        if (string.Compare(element, strCompareWith, false) == 0)
            return true;

        return false;
    }
    
    public static string FromQuery(this string element)
    {
        return HttpContext.Current.Request[element];
    }

    public static T FromQueryTyped<T>(this string elementName)
    {
        return HttpContext.Current.Request[elementName].ts().ConvertToCommonType<T>();
    }

    public static bool IsNotInQuery(this string element)
    {
        return HttpContext.Current.Request[element] == null;
    }

    //url creation
    public static string WithQuery_ExcludeArray(this string baseUrl, params string[] dropQueryElements)
    {
        string url = baseUrl;
        List<string> qKeys = HttpContext.Current.Request.QueryString.AllKeys.ToList<string>();
        qKeys.ToLower();
        List<string> dKeys = dropQueryElements.ToList<string>();
        dKeys.ToLower();
        foreach (string e in qKeys)
        {
            if (dKeys.Contains(e)) continue;
            url = url.AddQuerystringElement(e, e.FromQuery());
        }
        return url;
    }

    public static string WithQuery_IncludeArray(this string baseUrl, params string[] include)
    {
        string url = baseUrl;

        List<string> qKeys = HttpContext.Current.Request.QueryString.AllKeys.ToList<string>();
        qKeys.ToLower();
        List<string> inKeys = include.ToList<string>();
        inKeys.ToLower();
        foreach (string e in qKeys)
        {
            if (inKeys.Contains(e))
                url = url.AddQuerystringElement(e, e.FromQuery());
        }
        return url;
    }
    /// <summary>
    /// Key: Content inside token, Value: complete token i.e, Start of token + Key + End of token
    /// </summary>
    public static Dictionary<string, string> GetTokenDictionary(this string tokenFullString, string start, string end)
    {
        Dictionary<string, string> tokens = new Dictionary<string, string>();
        while (tokenFullString.Contains(start))
        {
            tokenFullString = tokenFullString.Substring(tokenFullString.IndexOf(start) + start.Length);
            string keyInside = tokenFullString.Substring(0, tokenFullString.IndexOf(end));
            tokens.Add(keyInside, string.Format("{0}{1}{2}", start, keyInside, end));
            tokenFullString = tokenFullString.Substring(end.Length);
        }
        return tokens;
    }
    #endregion Methods
    
    public static string ToSentenceCase(this string str)
    {
        return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
    }

    public static string XincludedShortcut(this string shortcut)
    {
        if (shortcut.Length > 10)
        {
            shortcut = shortcut.Substring(0, 10);
        }
        if (shortcut.Substring(0, 1).ToLower() == "x")
        {
                
                return shortcut = shortcut.Substring(0,1).ToUpper() + shortcut.Substring(1);
        }
        else
        {
            return shortcut = "X" + shortcut.Substring(0,9);
            
        }
    }

    public static string DictionaryTextLength(this string dicText)
    {
        if (dicText.Length > 500)
        {
            dicText = dicText.Substring(0, 499);
        }
        return dicText;
    }

    public static string FindInHttpContext(string key)
    {
        HttpContext context = HttpContext.Current;
        if (context.Request.Headers[key].IsNotNullOrEmpty())
            return context.Request.Headers[key];
        if (context.Request.Form[key].IsNotNullOrEmpty())
            return context.Request.Form[key];
        if (context.Request.QueryString[key].IsNotNullOrEmpty())
            return context.Request.QueryString[key];
        return null;
    }

    public static object GetStaticPropertyValue<T>(this string propertyName)
    {
        Type t = typeof(T);
        PropertyInfo pi = t.GetProperty(propertyName);
        if (null != pi)
        {
            return pi.GetValue(t, new object[] { });
        }
        else
        {
            FieldInfo fi = t.GetField(propertyName);
            if (null != fi)
            {
                return fi.GetValue(t);
            }
            else
            {
                throw new Exception("No property or field with name " + propertyName + " could not be found");
            }
        }
    }
    /// <summary>
    /// Get from app setting generic
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static T AppSetting<T>(this string key, T defaultValue)
    {
        return ConfigurationManager.AppSettings[key].IsNullOrEmpty() ?
        defaultValue : (T)Convert.ChangeType((object)ConfigurationManager.AppSettings[key], typeof(T));
    }

    /// <summary>
    /// Get from session generic
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static object Session(this string key)
    {
        return HttpContext.Current.Session[key];
    }
    public static object Session(this string key, object objectToStore)
    {
        return HttpContext.Current.Session[key] = objectToStore;
    }

    public static void Redirect(this string url)
    {
        HttpContext.Current.Response.Redirect(url, true);
    }

    public static string Connectionstring(this string connectionStringName)
    {
        if (connectionStringName.IsNullOrEmpty())
        {
            throw new Exception("conncectionstring name is empty or null");
        }

        if (ConfigurationManager.ConnectionStrings[connectionStringName].IsNullOrEmpty())
        {
            throw new Exception("connectionstring with name " + connectionStringName + " does not exist");
        }

        if (ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString.IsNullOrEmpty())
        {
            throw new Exception("connectionstring with name " + connectionStringName + " does not exist");
        }

        return ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
    }

    public static T ConvertToCommonType<T>(this string toCovert)
    {
        if (typeof(T) == typeof(int))
        {
            return Convert.ToInt32(toCovert).CastTo<T>();
        }

        throw new Exception("method: ConvertToCommonType, type casting failed.");
    }

    public static string RemoveIllegalFileFolderNameChars(this string target)
    {
        return target
        .Replace("/", string.Empty)
        .Replace(@"\", string.Empty)
        .Replace(":", string.Empty)
        .Replace("|", string.Empty)
        .Replace("*", string.Empty)
        .Replace("?", string.Empty)
        .Replace("\"", string.Empty)
        .Replace("<", string.Empty)
        .Replace(">", string.Empty)
        .Replace(@"''", string.Empty)
        .Replace(@"'", string.Empty);
    }
    public static string AlphabetsOnly(this string target)
    {
        return target
        .Replace("/", string.Empty)
        .Replace(@"\", string.Empty)
        .Replace(":", string.Empty)
        .Replace("|", string.Empty)
        .Replace("*", string.Empty)
        .Replace("?", string.Empty)
        .Replace("\"", string.Empty)
        .Replace("<", string.Empty)
        .Replace(">", string.Empty)
        .Replace(@"''", string.Empty)
        .Replace(@"'", string.Empty)
        .Replace("-", string.Empty);
    }
    public static bool MatchCaseSensitive(this string a, string b)
    {
        if (String.IsNullOrEmpty(a) && String.IsNullOrEmpty(b))
        {
            return true;
        }
        return CultureInfo.CurrentCulture.CompareInfo.Compare(a, b) == 0;
    }
    public static string LoadAsUrl(this string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        // Set some reasonable limits on resources used by this request
        // request.MaximumAutomaticRedirections = 4;
        //request.MaximumResponseHeadersLength = 4;
        // Set credentials to use for this request.
        //request.Credentials = CredentialCache.DefaultCredentials;
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            //Console.WriteLine("Content length is {0}", response.ContentLength);
            //Console.WriteLine("Content type is {0}", response.ContentType);

            // Get the stream associated with the response.
            Stream receiveStream = response.GetResponseStream();
            // Pipes the stream to a higher level stream reader with the required encoding format. 
            using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
            {
                return readStream.ReadToEnd();
            }
        }
        //Console.WriteLine("Response stream received.");
        //Console.WriteLine(readStream.ReadToEnd());
    }

    public static char[] SpecialCharacters = new char[] { '`', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '\\', '.', '/', '', ':', '|', ';', '\'', '"', ',', '?', '+', '=', '_' };

    //
    public static string ToId(this string label)
    {
        foreach (var s in SpecialCharacters)
        {
            if (label.IndexOf(s.ToString()) > -1)
            {
                label = label.Replace(s.ToString(), string.Empty);
            }

        }

        while (label.IndexOf("  ") > -1)
        {
            label = label.Replace("  ", " ");
        }

        return label.Pascalize();
    }

    public static string ToLabel(this string id)
    {
        id = id.Trim();
        var sb = new StringBuilder();
        foreach (var c in id.Select((str, ind) => new { Item = str, Index = ind }))
        {
            if (c.Index > 0 && Char.IsUpper(c.Item))
            {
                sb.Append(string.Format(" {0}", c.Item.ToString()));
            }
            else
            {
                sb.Append(c.Item.ToString());
            }
        }
        return sb.ToString();
    }

    public static List<string> SplitMultilineString(this string multilineString)
    {
        return multilineString.SplitToStringList("\r\n");
    }
    /// <summary>
    /// with web slash in the end
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string EnsureLocalDriveSlashInTheEnd(
                this string path
                )
    {
        return "".EnsureLocalDriveSlashInTheEnd(path);
    }

    /// <summary>
    /// with local slash in the end
    /// </summary>
    /// <param name="s"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string EnsureLocalDriveSlashInTheEnd(
        this string s,
        string path
        )
    {
        if (path.EndsWith(@"\"))
            return path;
        else
            return string.Format(@"{0}\", path);

    }

    public static string EnsureWebSlashInTheEnd(
             this string s
             )
    {
        return "".EnsureWebSlashInTheEnd(s);

    }

    public static string EnsureWebSlashInTheEnd(
        this string s,
        string path
        )
    {
        if (path.EndsWith(@"/"))
            return path;
        else
            return string.Format(@"{0}/", path);

    }

    public static DateTime? ToDateTimeByLocalRules(this string dateString)
    {
        if (dateString.IsNullOrEmpty())
        {
            return null;
        }

        return DateTime.SpecifyKind(
        DateTime.Parse(dateString),
        DateTimeKind.Local);
    }


    public static DateTime? ToDateTimeByUTCRules(this string dateString)
    {
        if (dateString.IsNullOrEmpty())
        {
            return null;
        }

        return DateTime.SpecifyKind(
        DateTime.Parse(dateString),
        DateTimeKind.Utc);
    }

    public static DateTime? ToDateTimeByNoRules(this string dateString)
    {
        if (dateString.IsNullOrEmpty())
        {
            return null;
        }

        return DateTime.SpecifyKind(
        DateTime.Parse(dateString),
        DateTimeKind.Unspecified);
    }

    public static string FormatByteSize(long bytes)
    {
        string[] suffix = { "b", "kb", "mb", "gb", "tb" , "pb", "eb", "zb", "yb"};
        int index = 0;
        do
        {
            bytes /= 1024; index++;
        }
        while (bytes >= 1024);
        return string.Format("{0:0.00}{1}", bytes, suffix[index]);
    }

    public static string ToEncrypt(this string clearText)
    {
        string EncryptionKey = "abc123";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    public static string ToDecrypt(this string cipherText)
    {
        string EncryptionKey = "abc123";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    public static string ReverseString(this string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }


    public static string ConvertToFriendlyUrl(this string text)
    {
        var decomposed = text.Normalize(NormalizationForm.FormKD);
        var builder = new StringBuilder();
        foreach (var ch in decomposed)
        {
            var charInfo = CharUnicodeInfo.GetUnicodeCategory(ch);
            switch (charInfo)
            {
                // Keep these as they are
                case UnicodeCategory.DecimalDigitNumber:
                case UnicodeCategory.LetterNumber:
                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.CurrencySymbol:
                case UnicodeCategory.OtherLetter:
                case UnicodeCategory.OtherNumber:
                    builder.Append(ch);
                    break;

                // Convert these to dashes
                case UnicodeCategory.DashPunctuation:
                case UnicodeCategory.MathSymbol:
                case UnicodeCategory.ModifierSymbol:
                case UnicodeCategory.OtherPunctuation:
                case UnicodeCategory.OtherSymbol:
                case UnicodeCategory.SpaceSeparator:
                    builder.Append('-');
                    break;

                // Convert to lower-case
                case UnicodeCategory.TitlecaseLetter:
                case UnicodeCategory.UppercaseLetter:
                    builder.Append(char.ToLowerInvariant(ch));
                    break;

                // Ignore certain types of characters
                case UnicodeCategory.OpenPunctuation:
                case UnicodeCategory.ClosePunctuation:
                case UnicodeCategory.ConnectorPunctuation:
                case UnicodeCategory.Control:
                case UnicodeCategory.EnclosingMark:
                case UnicodeCategory.FinalQuotePunctuation:
                case UnicodeCategory.Format:
                case UnicodeCategory.InitialQuotePunctuation:
                case UnicodeCategory.LineSeparator:
                case UnicodeCategory.ModifierLetter:
                case UnicodeCategory.NonSpacingMark:
                case UnicodeCategory.OtherNotAssigned:
                case UnicodeCategory.ParagraphSeparator:
                case UnicodeCategory.PrivateUse:
                case UnicodeCategory.SpacingCombiningMark:
                case UnicodeCategory.Surrogate:
                    break;
            }
        }

        var built = builder.ToString();
        while (built.Contains("--"))
            built = built.Replace("--", "-");
        while (built.EndsWith("-"))
        {
            built = built.Substring(0, built.Length - 1);
        }
        while (built.StartsWith("-"))
        {
            built = built.Substring(1, built.Length - 1);
        }
        return built;
    }

    public static string ToLandlineNoFormat(this string phoneNo)
    {
        try
        {
            if (phoneNo.IsNotNullOrEmpty())
            {
                phoneNo = phoneNo.Trim().Replace(" ", string.Empty);

                if (phoneNo.Length == 10)
                {
                    string twoDigit = phoneNo.Substring(0, 2);
                    phoneNo = "(" + twoDigit + ") " + phoneNo.Substring(2, 4) + " " + phoneNo.Substring(6);
                    return phoneNo;
                }
            }
        }
        catch
        {
            return phoneNo;
        }

        return phoneNo;
    }

    public static string GetVideoHost(this string videoLink)
    {
        string host;
        if( videoLink.IsNotNullOrEmpty())
        {
            if (videoLink.Contains("youtube") || videoLink.Contains("youtu.be"))
            {
                host = "youtube";
            }
            else
            {
                host = "vimeo";
            }
            return host;
        }
        return null;
    }

    public static string DecodeUrlString(this string url) {
        string newUrl;
        while ((newUrl = Uri.UnescapeDataString(url)) != url)
            url = newUrl;
        return newUrl;
    }

    public static string GetVideoLink(this string videoLink)
    {
        var result = videoLink;
        string videoId = "";

        if (videoLink.IsNotNullOrEmpty())
        {
            videoLink = videoLink.DecodeUrlString();
            string hostName = videoLink.GetVideoHost();

            if (hostName.IsNotNullOrEmpty())
            {

                if (videoLink.Contains("player.vimeo.com"))
                {
                    result = videoLink;
                }
                else
                {
                    Regex VimeoVideoRegex = new Regex(@"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    Regex YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);

                    Match youtubeMatch = YoutubeVideoRegex.Match(videoLink);
                    Match vimeoMatch = VimeoVideoRegex.Match(videoLink);

                    if (youtubeMatch.Success)
                        videoId = youtubeMatch.Groups[1].Value;

                    if (vimeoMatch.Success)
                        videoId = vimeoMatch.Groups[1].Value;

                    if (videoId.IsNotNullOrEmpty())
                    {
                        if (hostName == "vimeo")
                        {
                            result = "https://player.vimeo.com/video/" + videoId;
                        }
                        else if (hostName == "youtube")
                        {
                            result = "http://www.youtube.com/embed/" + videoId +
                                     "?version=3&enablejsapi=1&playerapiid=ytplayer";
                        }
                    } 
                }
            }
        }
        
        return result;
    }

    /// <summary>
    /// Pass date in ddmmyyyy format.
    /// </summary>
    /// <param name="strDate"></param>
    /// <returns></returns>
    // TODO: Move this method in base
    public static DateTime GetParsedDateFromString(this string strDate)
    {
        return new DateTime(Convert.ToInt32(strDate.Substring(4, 4)), Convert.ToInt32(strDate.Substring(2, 2)), Convert.ToInt32(strDate.Substring(0, 2)));//year, month, day
    }


    /// <summary>
    /// Pass date in ddmmyyyyHHmmss format.
    /// </summary>
    /// <param name="strDate"></param>
    /// <returns></returns>
    // TODO: Move this method in base
    public static DateTime GetParsedDateTimeFromString(this string strDate)
    {
        return new DateTime(Convert.ToInt32(strDate.Substring(4, 4)), Convert.ToInt32(strDate.Substring(2, 2)), Convert.ToInt32(strDate.Substring(0, 2)), Convert.ToInt32(strDate.Substring(8,2)), Convert.ToInt32(strDate.Substring(10,2)), Convert.ToInt32(strDate.Substring(12,2)));//year, month, day, hour, minute, sec
    }


    public static string MergeStrings(this string separater, string[] tokens) {
        var s = "";
        if (tokens.CountedPositive()) {
            for (var i = 0; i < tokens.Length; i++) {
                var token = tokens[i];

                if (token.IsNotNullOrEmpty())
                    s += token + separater + " ";
            }

            if (s.IsNotNullOrEmpty() && s.Length > 1)
            {
                s = s.Trim().Substring(0, s.Length - 2);
            }
        }
        return s;
    }

    public static string ToBase64(this string value)
    {

        byte[] toEncodeAsBytes
                = System.Text.ASCIIEncoding.ASCII.GetBytes(value);
        string returnValue
              = System.Convert.ToBase64String(toEncodeAsBytes);
        return returnValue;
    
    }

    public static string CapitalizeWords(this string value)
    {
        if (value == null)
            return "";
        if (value.Length == 0)
            return value;

        StringBuilder result = new StringBuilder(value);
        result[0] = char.ToUpper(result[0]);
        for (int i = 1; i < result.Length; ++i)
        {
            if (char.IsWhiteSpace(result[i - 1]))
                result[i] = char.ToUpper(result[i]);
            else
                result[i] = char.ToLower(result[i]);
        }
        return result.ToString();
    }
}
