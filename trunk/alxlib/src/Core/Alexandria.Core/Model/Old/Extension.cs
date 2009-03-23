using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Alexandria.Core.Model
{
	public class Extension : IXmlSerializable
	{
		private const string ELEMENT_NAME = "extension";
		private const string ATTRIB_APPLICATION = "application";

		public static readonly Extension Empty = default(Extension);

		public Extension(Uri application, string value)
		{
			Application = application;
			Value = value ?? string.Empty;
		}

		public Uri Application { get; private set; }
		public string Value { get; private set; }

		#region IXmlSerializable Members

		public XmlSchema GetSchema()
		{
			return new XmlSchema();
		}

		public void ReadXml(XmlReader reader)
		{
			if (reader != null)
			{
				if (reader.IsStartElement(ELEMENT_NAME))
				{
					Uri application;
					if (Uri.TryCreate(reader.GetAttribute(ATTRIB_APPLICATION), UriKind.Absolute, out application))
					{
						Application = application;
					}

					reader.ReadStartElement();

					Value = reader.ReadElementContentAsString() ?? string.Empty;

					reader.ReadEndElement();
				}
			}
		}

		public void WriteXml(XmlWriter writer)
		{
			if (writer != null)
			{
				writer.WriteStartElement(ELEMENT_NAME);
				writer.WriteAttributeString(ATTRIB_APPLICATION, (Application != null) ? Application.ToString() : string.Empty);
				writer.WriteString(Value);
				writer.WriteEndElement();
			}
		}

		#endregion
	}
}
