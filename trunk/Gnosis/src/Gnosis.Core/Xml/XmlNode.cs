﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public abstract class XmlNode
        : IXmlNode
    {
        protected XmlNode()
            : this(new List<IXmlNode>())
        {
        }

        protected XmlNode(IEnumerable<IXmlNode> children)
            : this(null, children)
        {
        }

        protected XmlNode(IXmlNode parent, IEnumerable<IXmlNode> children)
        {
            this.parent = parent;
            this.children = children;

            foreach (var child in children)
                if (child.Parent == null)
                    child.Parent = this;
        }

        private IXmlNode parent;
        private readonly IEnumerable<IXmlNode> children;

        #region Protected Methods

        protected int GetDepth()
        {
            var depth = -1;

            var currentParent = Parent;
            while (currentParent != null)
            {
                depth++;
                currentParent = currentParent.Parent;
            }

            return depth;
        }

        protected string GetIndent()
        {
            const int multiplier = 2;
            var depth = GetDepth();
            return (depth > 0) ? string.Empty.PadLeft(depth * multiplier) : string.Empty;
        }

        #endregion

        #region IXmlNode Members

        public IXmlNode Parent
        {
            get { return parent; }
            set
            {
                if (parent != null)
                    throw new InvalidOperationException("Parent is already initialized");

                parent = value;
            }
        }

        public IEnumerable<IXmlNode> Children
        {
            get { return children; }
        }

        public virtual IEnumerable<T> Where<T>(Func<T, bool> predicate)
            where T : class, IXmlNode
        {
            var results = new List<T>();

            var self = this as T;
            if (self != null && predicate(self))
                results.Add(self);

            foreach (var child in children)
                results.AddRange(child.Where(predicate));

            return results;
        }

        #endregion
    }
}
