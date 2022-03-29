using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Services.HRMS
{

    public class SelectListItemCustom : SelectListItem
    {
        public IDictionary<string, object> itemsHtmlAttributes { get; set; }
    }

    public static class FormHelper
    {
        public static MvcHtmlString DropDownListForCustom(this HtmlHelper htmlHelper, string id, List<SelectListItemCustom> selectListItems)
        {
            var selectListHtml = "";

            foreach (var item in selectListItems)
            {
                var attributes = new List<string>();
                foreach (KeyValuePair<string, object> dictItem in item.itemsHtmlAttributes)
                {
                    attributes.Add(string.Format("{0}='{1}'", dictItem.Key, dictItem.Value));
                }
                // do this or some better way of tag building
                selectListHtml += string.Format(
                    "<option value='{0}' {1} {2}>{3}</option>", item.Value, item.Selected ? "selected" : string.Empty, string.Join(" ", attributes.ToArray()), item.Text);
            }
            // do this or some better way of tag building
            var html = string.Format("<select id='{0}' name='{0}'>{1}</select>", id, selectListHtml);

            return new MvcHtmlString(html);
        }
    }
  public  class HtmlHelper
    {
    }
}
