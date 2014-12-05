using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ext.Net;

namespace Hogaf.ExtNet.UX
{
    public partial class PDateColumn : DateColumn
    {
        /// <summary>
        /// 
        /// </summary>
        [Category("0. About")]
        [Description("")]
        public override string XType
        {
            get
            {
                return "pdatecolumn";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("0. About")]
        [Description("")]
        public override string InstanceOf
        {
            get
            {
                return "Ext.grid.column.PDate";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        protected override List<ResourceItem> Resources
        {
            get
            {
                List<ResourceItem> baseList = base.Resources;
                baseList.Capacity += 1;

                baseList.Add(new ClientScriptItem(typeof(PDateColumn), "Hogaf.ExtNet.UX.Build.Ext.Net.extjs.PDate.grid.PDateColumn.js", "/PDate/grid/PDateColumn.js"));
                baseList.Add(new ClientScriptItem(typeof(PDateColumn), "Hogaf.ExtNet.UX.Build.Ext.Net.extjs.PDate.pdate.js", "/PDate/pdate.js"));

                return baseList;
            }
        }
    }
}
