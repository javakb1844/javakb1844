using Services.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ClientCommon
{
    public class ServicesRepository
    {
        private SearchService _search = null;
        public SearchService Search
        {
            get
            {
                if (_search == null)
                {
                    _search = new SearchService();                    
                }
                return _search;
            }
        }

        private DetailService _detail = null;
        public DetailService Detail
        {
            get
            {
                if (_detail == null)
                {
                    _detail = new DetailService();
                }
                return _detail;
            }
        }
    }
}
