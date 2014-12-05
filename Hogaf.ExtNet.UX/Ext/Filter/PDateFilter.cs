using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Xml.Serialization;
using Ext.Net;
using Ext.Net.Utilities;
using Newtonsoft.Json;

namespace Hogaf.ExtNet.UX
{
    public partial class PDateFilter : DateFilter
    {
        private PDatePickerOptions pickerOptions;

        [Description("")]
        [Meta]
        [ConfigOption("pickerOpts", JsonMode.Object)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [NotifyParentProperty(true)]
        public PDatePickerOptions PDatePickerOptions
        {
            get
            {
                if (this.pickerOptions == null)
                    this.pickerOptions = new PDatePickerOptions();
                return this.pickerOptions;
            }
        }

        /// <summary/>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [XmlIgnore]
        [JsonIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override ConfigOptionsCollection ConfigOptions
        {
            get
            {
                ConfigOptionsCollection configOptions = base.ConfigOptions;
                configOptions.Add("type", new ConfigOption("type", new SerializationOptions(JsonMode.ToLower), null, this.Type));
                configOptions.Add("beforeText", new ConfigOption("beforeText", null, "Before", this.BeforeText));
                configOptions.Add("afterText", new ConfigOption("afterText", null, "After", this.AfterText));
                configOptions.Add("onText", new ConfigOption("onText", null, "On", this.OnText));
                configOptions.Add("formatProxy", new ConfigOption("formatProxy", new SerializationOptions("dateFormat"), "", this.FormatProxy));
                configOptions.Add("submitFormatProxy", new ConfigOption("submitFormatProxy", new SerializationOptions("submitFormat"), "", this.SubmitFormatProxy));
                configOptions.Add("datePickerOptions", new ConfigOption("datePickerOptions", new SerializationOptions("pickerOpts", JsonMode.Object), null, this.PDatePickerOptions));
                configOptions.Add("maxDate", new ConfigOption("maxDate", new SerializationOptions(typeof(CtorDateTimeJsonConverter)), new DateTime(9999, 12, 31), this.MaxDate));
                configOptions.Add("minDate", new ConfigOption("minDate", new SerializationOptions(typeof(CtorDateTimeJsonConverter)), new DateTime(1, 1, 1), this.MinDate));
                configOptions.Add("valueProxy", new ConfigOption("valueProxy", new SerializationOptions("value", JsonMode.Raw), "", this.ValueProxy));
                return configOptions;
            }
        }
    }
}
