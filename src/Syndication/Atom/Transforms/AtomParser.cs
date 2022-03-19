using System;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibFeeds.Syndication.Atom.Data;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;

namespace Bau.Libraries.LibFeeds.Syndication.Atom.Transforms
{
	/// <summary>
	///		Intérprete de un archivo Atom
	/// </summary>
	public class AtomParser
	{
		/// <summary>
		///		Interpreta un archivo
		/// </summary>
		public AtomChannel Parse(string fileName)
		{
			return Parse(new XMLParser().Load(fileName));
		}

		/// <summary>
		///		Interpreta un texto XML
		/// </summary>
		public AtomChannel ParseText(string xml)
		{
			return Parse(new XMLParser().ParseText(xml));
		}

		/// <summary>
		///		Interpreta un archivo XML
		/// </summary>
		public AtomChannel Parse(MLFile fileML)
		{
			AtomChannel channel = null;

				// Recorre los nodos del documento
				if (fileML != null)
					foreach (MLNode node in fileML.Nodes)
						if (node.Name.Equals(AtomConstTags.cnstStrChannelRoot))
						{ 
							// Crea el objeto
							channel = new AtomChannel();
							// Lee los datos
							ParseChannel(node, channel);
						}
				// Devuelve el objeto Atom
				return channel;
		}

		/// <summary>
		///		Interpreta los datos del canal
		/// </summary>
		private void ParseChannel(MLNode node, AtomChannel channel)
		{
			foreach (MLNode child in node.Nodes)
				switch (child.Name)
				{
					case AtomConstTags.cnstStrChannelId:
							channel.ID = child.Value;
						break;
					case AtomConstTags.cnstStrChannelTitle:
							channel.Title = ParseText(child);
						break;
					case AtomConstTags.cnstStrChannelTagline:
							channel.TagLine = ParseText(child);
						break;
					case AtomConstTags.cnstsubTitle:
							channel.Subtitle = ParseText(child);
						break;
					case AtomConstTags.cnsticon:
							channel.Icon = child.Value;
						break;
					case AtomConstTags.cnstStrLogo:
							channel.Logo = child.Value;
						break;
					case AtomConstTags.cnstStrRights:
							channel.Rights = ParseRights(child);
						break;
					case AtomConstTags.cnstStrChannelGenerator:
							channel.Generator = ParseGenerator(child);
						break;
					case AtomConstTags.cnstStrChannelConvertLineBreaks:
							channel.ConvertLineBreaks = child.Value.GetBool(false);
						break;
					case AtomConstTags.cnstStrChannelInfo:
							channel.Info = ParseText(child);
						break;
					case AtomConstTags.cnstStrItemLink:
							channel.Links.Add(ParseLink(child));
						break;
					case AtomConstTags.cnstStrItemModified:
							channel.LastUpdated = child.Value.GetDateTime(DateTime.Now);
						break;
					case AtomConstTags.cnstStrChannelCategory:
							channel.Categories.Add(ParseCategory(child));
						break;
					case AtomConstTags.cnstStrItemRoot:
							channel.Entries.Add(ParseEntry(child, channel));
						break;
					default:
							channel.Extensions.Parse(child, channel, channel.Dictionary);
						break;
				}
		}

		/// <summary>
		///		Interpreta un texto de Atom
		/// </summary>
		private AtomText ParseText(MLNode node)
		{
			return new AtomText(node.Attributes[AtomConstTags.cnstStrAttrMode].Value,
								node.Attributes[AtomConstTags.cnstStrAttrType].Value,
								node.Attributes[AtomConstTags.cnstStrAttrLanguage].Value,
								node.Attributes[AtomConstTags.cnstStrAttrXMLBase].Value,
								node.Value);
		}

		/// <summary>
		///		Interpreta el generador de Atom
		/// </summary>
		private AtomGenerator ParseGenerator(MLNode node)
		{
			return new AtomGenerator(node.Attributes[AtomConstTags.cnstStrAttrURL].Value,
									 node.Attributes[AtomConstTags.cnstStrAttrVersion].Value,
									 node.Attributes[AtomConstTags.cnstStrAttrLanguage].Value,
									 node.Value);
		}

		/// <summary>
		///		Interpreta un vínculo de Atom
		/// </summary>
		private AtomLink ParseLink(MLNode node)
		{
			AtomLink link = new AtomLink(node.Attributes[AtomConstTags.cnstStrAttrHref].Value,
											node.Attributes[AtomConstTags.cnstStrAttrRel].Value,
											node.Attributes[AtomConstTags.cnstStrAttrTitle].Value,
											node.Attributes[AtomConstTags.cnstStrAttrType].Value);

			// Si no se ha recogido ninguna refencia mira en el InnerText
			if (string.IsNullOrEmpty(link.Href) && !string.IsNullOrEmpty(node.Value))
				link.Href = node.Value;
			// Devuelve el vínculo
			return link;
		}

		/// <summary>
		///		Interpreta una categoría de Atom
		/// </summary>
		private AtomCategory ParseCategory(MLNode node)
		{
			return new AtomCategory(node.Attributes[AtomConstTags.cnstStrAttrTerm].Value);
		}

		/// <summary>
		///		Interpreta una entrada de un archivo Atom
		/// </summary>
		private AtomEntry ParseEntry(MLNode objMLEntry, AtomChannel channel)
		{
			AtomEntry entry = new AtomEntry();

				// Recorre los nodos
				foreach (MLNode node in objMLEntry.Nodes)
					switch (node.Name)
					{
						case AtomConstTags.cnstStrItemID:
								entry.ID = node.Value;
							break;
						case AtomConstTags.cnstStrItemTitle:
								entry.Title = ParseText(node);
							break;
						case AtomConstTags.cnstStrItemContent:
								entry.Content = ParseText(node);
							break;
						case AtomConstTags.cnstStrSummary:
								entry.Summary = ParseText(node);
							break;
						case AtomConstTags.cnstStrItemIssued:
								entry.DateIssued = node.Value.GetDateTime(DateTime.MinValue);
							break;
						case AtomConstTags.cnstStrItemModified:
								entry.DateModified = node.Value.GetDateTime(DateTime.MinValue);
							break;
						case AtomConstTags.cnstStrItemCreated:
								entry.DateCreated = node.Value.GetDateTime(DateTime.MinValue);
							break;
						case AtomConstTags.cnstStrItemUpdated:
								entry.DateUpdated = node.Value.GetDateTime(DateTime.MinValue);
							break;
						case AtomConstTags.cnstStrItemPublished:
								entry.DatePublished = node.Value.GetDateTime(DateTime.MinValue);
							break;
						case AtomConstTags.cnstStrItemLink:
								entry.Links.Add(ParseLink(node));
							break;
						case AtomConstTags.cnstStrItemAuthor:
								entry.Authors.Add(ParsePeople(node));
							break;
						case AtomConstTags.cnstStrItemContributor:
								entry.Contributors.Add(ParsePeople(node));
							break;
						case AtomConstTags.cnstStrChannelCategory:
								entry.Categories.Add(ParseCategory(node));
							break;
						case AtomConstTags.cnstsource:
								entry.Source = ParseSource(node);
							break;
						case AtomConstTags.cnstStrRights:
								entry.Rights = ParseRights(node);
							break;
						default:
								entry.Extensions.Parse(node, entry, channel.Dictionary);
							break;
					}
				// Actualiza la fecha de creación
				if (entry.DateCreated.Date == DateTime.MinValue.Date)
				{
					entry.DateCreated = entry.DatePublished;
					if (entry.DateCreated.Date == DateTime.MinValue.Date)
						entry.DateCreated = DateTime.Now;
				}
				// Devuelve la entrada
				return entry;
		}

		/// <summary>
		///		Interpreta un nodo con derechos
		/// </summary>
		private AtomRights ParseRights(MLNode node)
		{
			return new AtomRights(node.Attributes[AtomConstTags.cnstStrAttrType].Value, node.Value);
		}

		/// <summary>
		///		Interpreta el origen de un nodo
		/// </summary>
		private AtomSource ParseSource(MLNode objMLSource)
		{
			AtomSource source = new AtomSource();

				// Interpreta los datos del nodo
				foreach (MLNode node in objMLSource.Nodes)
					switch (node.Name)
					{
						case AtomConstTags.cnstStrChannelId:
								source.ID = node.Value;
							break;
						case AtomConstTags.cnstStrChannelTitle:
								source.Title = node.Value;
							break;
						case AtomConstTags.cnstStrItemUpdated:
								source.DateUpdated = node.Value.GetDateTime(DateTime.Now);
							break;
						case AtomConstTags.cnstStrRights:
								source.Copyright = node.Value;
							break;
					}
				// Devuelve los datos
				return source;
		}

		/// <summary>
		///		Interpreta un nodo XML para obtener los datos de una persona
		/// </summary>
		private AtomPeople ParsePeople(MLNode objMLPeople)
		{
			AtomPeople objPeople = new AtomPeople();

				// Recorre los nodos
				foreach (MLNode node in objMLPeople.Nodes)
					switch (node.Name)
					{
						case AtomConstTags.cnstStrItemPeopleName:
								objPeople.Name = node.Value;
							break;
						case AtomConstTags.cnstStrItemPeopleUrl:
								objPeople.URL = node.Value;
							break;
						case AtomConstTags.cnstStrItemPeopleEMail:
								objPeople.EMail = node.Value;
							break;
					}
				// Devuelve el objeto
				return objPeople;
		}
	}
}