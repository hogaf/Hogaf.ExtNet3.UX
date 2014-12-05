using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Ext.Net;

namespace Hogaf.ExtNet.UX
{
    public partial class PDateField : DateField
    {
        private bool overridedFormat;
        /// <summary>
        /// The default date format string which can be overriden for localization support. The format must be valid according to Date.parseDate (defaults to 'Y/m/d').
        /// 
        /// </summary>
        public override string Format
        {
            get
            {
                if (!overridedFormat)
                    base.Format = "yyyy/MM/dd";
                return base.Format;
            }
            set
            {
                overridedFormat = true;
                base.Format = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("0. About")]
        [Description("")]
        public override string XType
        {
            get
            {
                return "pdatefield";
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
                return "Ext.form.field.PDate";
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
                baseList.Capacity += 4;

                baseList.Add(new ClientScriptItem(typeof(PDateField), "Hogaf.ExtNet.UX.Build.Ext.Net.extjs.PDate.pdate.js", "/PDate/pdate.js"));
                baseList.Add(new ClientScriptItem(typeof(PDateField), "Hogaf.ExtNet.UX.Build.Ext.Net.extjs.PDate.picker.PMonth.js", "/PDate/picker/PMonth.js"));
                baseList.Add(new ClientScriptItem(typeof(PDateField), "Hogaf.ExtNet.UX.Build.Ext.Net.extjs.PDate.picker.PDate.js", "/PDate/picker/PDate.js"));
                baseList.Add(new ClientScriptItem(typeof(PDateField), "Hogaf.ExtNet.UX.Build.Ext.Net.extjs.PDate.form.field.PDate.js", "/PDate/form/field/PDate.js"));

                return baseList;
            }
        }
    }
}
