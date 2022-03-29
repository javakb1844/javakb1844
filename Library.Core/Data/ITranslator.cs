using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Data
{
    /// <summary>
    /// Translation Interface
    /// </summary>
    interface ITranslator
    {
        string Get(string word);
        string GetById(string id);
    }

    //NOTE: Implement it in respective project(s)


    ///// <summary>
    ///// Database Translator
    ///// </summary>
    //public class Translator : ITranslator
    //{
    //    public string Get(string word)
    //    {
    //        return word;
    //    }


    //    public string GetById(string id)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}



    ///// <summary>
    ///// Microsoft Translator Service
    ///// </summary>
    //public class MicrosoftTranslator : ITranslator
    //{
    //    public string Get(string word)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public string GetById(string id)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
