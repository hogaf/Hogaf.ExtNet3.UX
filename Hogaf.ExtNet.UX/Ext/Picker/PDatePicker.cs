using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ext.Net;

namespace Hogaf.ExtNet.UX
{
    public partial class PDatePicker : DatePicker
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
                return "pdatepicker";
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
                return "Ext.picker.PDate";
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
                baseList.Capacity += 3;

                baseList.Add(new ClientScriptItem(typeof(PDatePicker), "Hogaf.ExtNet.UX.Build.Ext.Net.extjs.PDate.pdate.js", ""));
                baseList.Add(new ClientScriptItem(typeof(PDatePicker), "Hogaf.ExtNet.UX.Build.Ext.Net.extjs.PDate.picker.PMonth.js", ""));
                baseList.Add(new ClientScriptItem(typeof(PDatePicker), "Hogaf.ExtNet.UX.Build.Ext.Net.extjs.PDate.picker.PDate.js", ""));

                return baseList;
            }
        }
    }
}
