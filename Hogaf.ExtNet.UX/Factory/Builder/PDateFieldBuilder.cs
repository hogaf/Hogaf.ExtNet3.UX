using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using Ext.Net;

namespace Hogaf.ExtNet.UX
{
    public partial class PDateField
    {
        public PDateField()
        {
        }

        public PDateField(Config config)
        {
            Apply(config);
        }

        public Builder ToBuilder()
        {
            return new BuilderFactory().PDateField(this);
        }

        public class Builder : Builder<PDateField, Builder>
        {
            public Builder() : base(new PDateField()) { }

            public Builder(PDateField component) : base(component) { }

            public Builder(Config config) : base(new PDateField(config)) { }
        }

        new public class Config : DateField.Config
        {
            public static implicit operator Builder(Config config)
            {
                return new Builder(config);
            }
        }
    }

    public static partial class BuilderFactoryExtension
    {
        public static PDateField.Builder PDateField(this BuilderFactory factory)
        {
            return PDateField(factory, new PDateField
            {
                ViewContext = factory.HtmlHelper != null ? factory.HtmlHelper.ViewContext : null
            });
        }

        public static PDateField.Builder PDateField(this BuilderFactory factory, PDateField component)
        {
            component.ViewContext = factory.HtmlHelper != null ? factory.HtmlHelper.ViewContext : null;
            return new PDateField.Builder(component);
        }

        public static PDateField.Builder PDateFieldFor<TModel, TProperty>(this BuilderFactory<TModel> factory, 
            Expression<Func<TModel, TProperty>> expression, bool setId = false, Func<object, object> convert = null, 
            string format = null)
        {
            return factory.InitFieldForBuilder<PDateField, PDateField.Builder, TProperty>(expression, setId, convert, format);
        }
    }
}