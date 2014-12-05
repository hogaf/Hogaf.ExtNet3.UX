using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Ext.Net;

namespace Hogaf.ExtNet.UX
{
    public partial class PDatePickerOptions
    {
        public PDatePickerOptions()
        {
        }

        public PDatePickerOptions(Config config)
        {
            Apply(config);
        }

        public new Builder ToBuilder()
        {
            return new BuilderFactory().PDatePickerOptions(this);
        }

        public new class Builder : Builder<PDatePickerOptions, Builder>
        {
            public Builder() : base(new PDatePickerOptions()) { }

            public Builder(PDatePickerOptions component) : base(component) { }

            public Builder(Config config) : base(new PDatePickerOptions(config)) { }
        }

        new public class Config : PDatePicker.Config
        {
            public static implicit operator Builder(Config config)
            {
                return new Builder(config);
            }
        }
    }

    public static partial class BuilderFactoryExtension
    {
        public static PDatePickerOptions.Builder PDatePickerOptions(this BuilderFactory factory)
        {
            return PDatePickerOptions(factory, new PDatePickerOptions
            {
                ViewContext = factory.HtmlHelper != null ? factory.HtmlHelper.ViewContext : null
            });
        }

        public static PDatePickerOptions.Builder PDatePickerOptions(this BuilderFactory factory, PDatePickerOptions component)
        {
            component.ViewContext = factory.HtmlHelper != null ? factory.HtmlHelper.ViewContext : null;
            return new PDatePickerOptions.Builder(component);
        }
    }
}