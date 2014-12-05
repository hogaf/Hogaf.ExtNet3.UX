using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Ext.Net;

namespace Hogaf.ExtNet.UX
{
    public partial class PDateColumn
    {
        public PDateColumn()
        {
        }

        public PDateColumn(Config config)
        {
            Apply(config);
        }

        public Builder ToBuilder()
        {
            return new BuilderFactory().PDateColumn(this);
        }

        public class Builder : Builder<PDateColumn, Builder>
        {
            public Builder() : base(new PDateColumn()) { }

            public Builder(PDateColumn component) : base(component) { }

            public Builder(Config config) : base(new PDateColumn(config)) { }
        }

        new public class Config : DateColumn.Config
        {
            public static implicit operator Builder(Config config)
            {
                return new Builder(config);
            }
        }
    }

    public static partial class BuilderFactoryExtension
    {
        public static PDateColumn.Builder PDateColumn(this BuilderFactory factory)
        {
            return PDateColumn(factory, new PDateColumn
            {
                Renderer = new Renderer("return Ext.PDate.format(value, 'Y/m/d')")
            });
        }

        public static PDateColumn.Builder PDateColumn(this BuilderFactory factory, PDateColumn component)
        {
            return new PDateColumn.Builder(component);
        }
    }
}
