using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Ext.Net;

namespace Hogaf.ExtNet.UX
{
    public partial class PDatePicker
    {
        public PDatePicker()
        {
        }

        public PDatePicker(Config config)
        {
            Apply(config);
        }

        public new Builder ToBuilder()
        {
            return new BuilderFactory().PDatePicker(this);
        }

        public new class Builder : Builder<PDatePicker, Builder>
        {
            public Builder() : base(new PDatePicker()) { }

            public Builder(PDatePicker component) : base(component) { }

            public Builder(Config config) : base(new PDatePicker(config)) { }
        }

        new public class Config : DatePicker.Config
        {
            public static implicit operator Builder(Config config)
            {
                return new Builder(config);
            }
        }
    }

    public static partial class BuilderFactoryExtension
    {
        public static PDatePicker.Builder PDatePicker(this BuilderFactory factory)
        {
            return PDatePicker(factory, new PDatePicker
            {
                ViewContext = factory.HtmlHelper != null ? factory.HtmlHelper.ViewContext : null
            });
        }

        public static PDatePicker.Builder PDatePicker(this BuilderFactory factory, PDatePicker component)
        {
            component.ViewContext = factory.HtmlHelper != null ? factory.HtmlHelper.ViewContext : null;
            return new PDatePicker.Builder(component);
        }
    }
}