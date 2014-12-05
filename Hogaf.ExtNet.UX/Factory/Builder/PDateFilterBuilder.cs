using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Ext.Net;

namespace Hogaf.ExtNet.UX
{
    public partial class PDateFilter
    {
        public PDateFilter()
        {
        }

        public PDateFilter(Config config)
        {
            Apply(config);
        }

        public new Builder ToBuilder()
        {
            return new BuilderFactory().PDateFilter(this);
        }

        public new class Builder : Builder<PDateFilter, Builder>
        {
            public Builder() : base(new PDateFilter()) { }

            public Builder(PDateFilter component) : base(component) { }

            public Builder(Config config) : base(new PDateFilter(config)) { }
        }

        new public class Config : DateFilter.Config
        {
            public static implicit operator Builder(Config config)
            {
                return new Builder(config);
            }
        }
    }

    public static partial class BuilderFactoryExtension
    {
        public static PDateFilter.Builder PDateFilter(this BuilderFactory factory)
        {
            return PDateFilter(factory, new PDateFilter
            {
                OnText = "برابر با",
                BeforeText = "پیش از",
                AfterText = "پس از"
            }.SetViewContext(factory));
        }

        public static PDateFilter.Builder PDateFilter(this BuilderFactory factory, PDateFilter component)
        {
            return new PDateFilter.Builder(component.SetViewContext(factory));
        }

        private static PDateFilter SetViewContext(this PDateFilter component, BuilderFactory factory)
        {
            component.GetType().GetProperty("ViewContext", System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.NonPublic).SetValue(component, factory.HtmlHelper != null ? factory.HtmlHelper.ViewContext : null, null);
            return component;
        }
    }
}