﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml
{
    public class AttributeFactory
        : IAttributeFactory
    {
        public AttributeFactory(string attributeName, Func<IAttribute, bool> predicate, Func<INode, IQualifiedName, string, IAttribute> create)
        {
            this.attributeName = attributeName;
            this.predicate = predicate;
            this.create = create;
        }

        private readonly string attributeName;
        private readonly Func<IAttribute, bool> predicate;
        private readonly Func<INode, IQualifiedName, string, IAttribute> create;

        #region IXmlAttributeFactory Members

        public string AttributeName
        {
            get { return attributeName; }
        }

        public bool IsValidFor(IAttribute attribute)
        {
            return predicate(attribute);
        }

        public IAttribute Create(INode parent, IQualifiedName name, string value)
        {
            return create(parent, name, value);
        }

        #endregion
    }
}
