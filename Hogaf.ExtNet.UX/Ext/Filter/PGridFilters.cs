using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;

namespace Hogaf.ExtNet.UX
{
    public partial class PGridFilters : GridFilters
    {
        protected override List<ResourceItem> Resources
        {
            get
            {
                List<ResourceItem> baseList = base.Resources;
                baseList.Capacity += 1;
                baseList.RemoveAt(baseList.Count - 1);
                baseList.Add(new ClientScriptItem(typeof(PGridFilters), "Hogaf.ExtNet.UX.Build.Ext.Net.extjs.PDate.grid.gridfilters.GridFilters.js", "/PDate/grid/gridfilters/GridFilters.js"));

                return baseList;
            }
        }
    }
}
