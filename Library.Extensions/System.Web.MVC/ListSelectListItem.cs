using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

public static class ListSelectListItem
{
    public static List<SelectListItem> InsertFirst(this List<SelectListItem> items, string text, string value,
        bool selected, bool disabled = false, SelectListGroup group = null)
    {
        if (items.Count.IsPositive())
        {
            var first = items.FirstOrDefault();
            if (first.Text.MatchByString(text))
            {
                return items;
            }
        }

        items.Insert(0, new SelectListItem
        {
            Text = text,
            Value = value,
            Selected = selected,
            Disabled = disabled,
            Group = group
        });

        if (selected)
        {
            for (int i = 1; i < items.Count; i++)
            {
                items[i].Selected = false;
            }
        }
        return items;
    }

    public static List<SelectListItem> InsertLast(this List<SelectListItem> items, string text, string value,
        bool selected, bool disabled = false, SelectListGroup group = null)
    {
        if (items.Count.IsPositive())
        {
            var last = items[items.Count - 1];
            if (last.Text.MatchByString(text))
            {
                return items;
            }
        }

        items.Add(new SelectListItem
        {
            Text = text,
            Value = value,
            Selected = selected,
            Disabled = disabled,
            Group = group
        });

        if (selected)
        {
            for (int i = 1; i < items.Count; i++)
            {
                items[i].Selected = false;
            }
        }
        return items;
    }

    public static List<SelectListItem> CloneSelectList(this List<SelectListItem> items)
    {
        var list = new List<SelectListItem>();

        for (int i = 0; i < items.Count; i++)
        {
            var current = items[i];
            list.Add(new SelectListItem 
            {
                Disabled = current.Disabled,
                Group = current.Group,
                Selected = current.Selected,
                Text = current.Text,
                Value = current.Value
            });
        }

        return list;
    }
}