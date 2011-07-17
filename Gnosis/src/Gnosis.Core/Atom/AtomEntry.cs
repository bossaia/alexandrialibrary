﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public class AtomEntry
        : AtomCommon, IAtomEntry
    {
        public AtomEntry(Uri baseId, ILanguageTag lang, IEnumerable<IXmlExtension> extensions, IEnumerable<IXmlNamespace> namespaces, IXmlNamespace primaryNamespace, IEnumerable<IAtomPerson> authors, IAtomContent content, IAtomId id, IEnumerable<IAtomLink> links, IAtomSummary summary, IAtomTitle title, IAtomUpdated updated, IEnumerable<IAtomCategory> categories, IEnumerable<IAtomPerson> contributors, IAtomPublished published, IAtomRights rights, IAtomSource source)
            : base(baseId, lang, extensions, namespaces, primaryNamespace)
        {
            this.authors = authors;
            this.content = content;
            this.id = id;
            this.links = links;
            this.summary = summary;
            this.title = title;
            this.updated = updated;
            this.categories = categories;
            this.contributors = contributors;
            this.published = published;
            this.rights = rights;
            this.source = source;
        }

        private readonly IEnumerable<IAtomPerson> authors;
        private readonly IAtomContent content;
        private readonly IAtomId id;
        private readonly IEnumerable<IAtomLink> links;
        private readonly IAtomSummary summary;
        private readonly IAtomTitle title;
        private readonly IAtomUpdated updated;
        private readonly IEnumerable<IAtomCategory> categories;
        private readonly IEnumerable<IAtomPerson> contributors;
        private readonly IAtomPublished published;
        private readonly IAtomRights rights;
        private readonly IAtomSource source;

        #region IAtomEntry Members

        public IEnumerable<IAtomPerson> Authors
        {
            get { return authors; }
        }

        public IAtomContent Content
        {
            get { return content; }
        }

        public IAtomId Id
        {
            get { return id; }
        }

        public IEnumerable<IAtomLink> Links
        {
            get { return links; }
        }

        public IAtomSummary Summary
        {
            get { return summary; }
        }

        public IAtomTitle Title
        {
            get { return title; }
        }

        public IAtomUpdated Updated
        {
            get { return updated; }
        }

        public IEnumerable<IAtomCategory> Categories
        {
            get { return categories; }
        }

        public IEnumerable<IAtomPerson> Contributors
        {
            get { return contributors; }
        }

        public IAtomPublished Published
        {
            get { return published; }
        }

        public IAtomRights Rights
        {
            get { return rights; }
        }

        public IAtomSource Source
        {
            get { return source; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();

            AppendStartTag(xml, "entry");



            foreach (var author in authors)
                xml.AppendLine(author.ToString());

            if (content != null)
                xml.AppendLine(content.ToString());

            xml.AppendLine(id.ToString());
            xml.AppendLine(title.ToString());
            xml.AppendLine(updated.ToString());

            foreach (var link in links)
                xml.AppendLine(link.ToString());

            if (summary != null)
                xml.AppendLine(summary.ToString());

            foreach (var category in categories)
                xml.AppendLine(category.ToString());

            foreach (var contributor in contributors)
                xml.AppendLine(contributor.ToString());

            if (published != null)
                xml.AppendLine(published.ToString());

            if (rights != null)
                xml.AppendLine(rights.ToString());

            if (source != null)
                xml.AppendLine(source.ToString());

            AppendEndTag(xml, "entry");

            return xml.ToString();
        }

    }
}