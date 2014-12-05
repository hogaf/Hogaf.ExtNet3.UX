using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Ext.Net;

namespace Hogaf.ExtNet.UX
{
    public partial class PGridFilters
    {
        public PGridFilters()
        {
        }

        public PGridFilters(Config config)
        {
            Apply(config);
        }

        public new Builder ToBuilder()
        {
            return new BuilderFactory().PGridFilters(this);
        }

        public new class Builder : Builder<PGridFilters, Builder>
        {
            public Builder() : base(new PGridFilters()) { }

            public Builder(PGridFilters component) : base(component) { }

            public Builder(Config config) : base(new PGridFilters(config)) { }
        }

        new public class Config : GridFilters.Config
        {
            public static implicit operator Builder(Config config)
            {
                return new Builder(config);
            }
        }
    }

    public static partial class BuilderFactoryExtension
    {
        public static PGridFilters.Builder PGridFilters(this BuilderFactory factory)
        {
            return PGridFilters(factory, new PGridFilters
            {
                ViewContext = factory.HtmlHelper != null ? factory.HtmlHelper.ViewContext : null
            });
        }

        public static PGridFilters.Builder PGridFilters(this BuilderFactory factory, PGridFilters component)
        {
            component.ViewContext = factory.HtmlHelper != null ? factory.HtmlHelper.ViewContext : null;
            return new PGridFilters.Builder(component);
        }
    }
}