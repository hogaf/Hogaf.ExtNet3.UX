using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Xml.Serialization;
using Ext.Net;
using Newtonsoft.Json;

namespace Hogaf.ExtNet.UX
{
    [ToolboxItem(false)]
    [Description("")]
    [Meta]
    public partial class PDatePickerOptions : PDatePicker
    {
        /// <summary/>
        [Description("")]
        [Category("0. About")]
        public override string XType
        {
            get
            {
                return "";
            }
        }

        /// <summary/>
        [Description("")]
        [Category("0. About")]
        public override string InstanceOf
        {
            get
            {
                return "";
            }
        }

        /// <summary/>
        [Description("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public override bool AutoPostBack
        {
            get
            {
                return base.AutoPostBack;
            }
            set
            {
                base.AutoPostBack = value;
            }
        }

        /// <summary/>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [Description("")]
        public override bool CausesValidation
        {
            get
            {
                return base.CausesValidation;
            }
            set
            {
                base.CausesValidation = value;
            }
        }

        /// <summary/>
        [Description("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public override string ValidationGroup
        {
            get
            {
                return base.ValidationGroup;
            }
            set
            {
                base.ValidationGroup = value;
            }
        }

        /// <summary/>
        [Description("")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DateTime SelectedDate
        {
            get
            {
                return base.SelectedDate;
            }
            set
            {
                base.SelectedDate = value;
            }
        }

        /// <summary/>
        [ConfigOption(JsonMode.Ignore)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        protected override string ConfigIDProxy
        {
            get
            {
                return base.ConfigIDProxy;
            }
        }

        /// <summary/>
        [ConfigOption(JsonMode.Ignore)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public override string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }

        /// <summary/>
        [ConfigOption(JsonMode.Ignore)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public override string RenderTo
        {
            get
            {
                return base.RenderTo;
            }
        }

        /// <summary/>
        [ConfigOption(JsonMode.Ignore)]
        protected override string RenderToProxy
        {
            get
            {
                return "";
            }
        }

        /// <summary/>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [XmlIgnore]
        [JsonIgnore]
        public override ConfigOptionsCollection ConfigOptions
        {
            get
            {
                ConfigOptionsCollection configOptions = base.ConfigOptions;
                configOptions.Add("configIDProxy", new ConfigOption("configIDProxy", new SerializationOptions(JsonMode.Ignore), null, this.ConfigIDProxy));
                configOptions.Add("iD", new ConfigOption("iD", new SerializationOptions(JsonMode.Ignore), null, this.ID));
                configOptions.Add("renderTo", new ConfigOption("renderTo", new SerializationOptions(JsonMode.Ignore), null, this.RenderTo));
                configOptions.Add("renderToProxy", new ConfigOption("renderToProxy", new SerializationOptions(JsonMode.Ignore), null, this.RenderToProxy));
                return configOptions;
            }
        }      

        /// <summary/>
        [Description("")]
        protected override void Render(HtmlTextWriter writer)
        {
        }

        /// <summary/>
        [Description("")]
        protected override void OnPreRender(EventArgs e)
        {
        }
    }
}
