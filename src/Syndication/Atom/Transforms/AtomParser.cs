using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibFeeds.Syndication.Atom.Data;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;

namespace Bau.Libraries.LibFeeds.Syndication.Atom.Transforms;

/// <summary>
///		Intérprete de un archivo Atom
/// </summary>
public class AtomParser
{
	/// <summary>
	///		Interpreta un archivo
	/// </summary>
	public AtomChannel? Parse(string fileName) => Parse(new XMLParser().Load(fileName));

	/// <summary>
	///		Interpreta un texto XML
	/// </summary>
	public AtomChannel? ParseText(string xml) => Parse(new XMLParser().ParseText(xml));

	/// <summary>
	///		Interpreta un archivo XML
	/// </summary>
	public AtomChannel? Parse(MLFile fileML)
	{
		AtomChannel? channel = null;

			// Recorre los nodos del documento
			if (fileML is not null)
				foreach (MLNode node in fileML.Nodes)
					if (node.Name.Equals(AtomConstTags.ChannelRoot))
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
				case AtomConstTags.ChannelId:
						channel.ID = child.Value;
					break;
				case AtomConstTags.ChannelTitle:
						ParseText(child, channel.Title);
					break;
				case AtomConstTags.ChannelTagline:
						ParseText(child, channel.TagLine);
					break;
				case AtomConstTags.cnstsubTitle:
						ParseText(child, channel.Subtitle);
					break;
				case AtomConstTags.cnsticon:
						channel.Icon = child.Value;
					break;
				case AtomConstTags.Logo:
						channel.Logo = child.Value;
					break;
				case AtomConstTags.Rights:
						ParseRights(child, channel.Rights);
					break;
				case AtomConstTags.ChannelGenerator:
						ParseGenerator(child, channel.Generator);
					break;
				case AtomConstTags.ChannelConvertLineBreaks:
						channel.ConvertLineBreaks = child.Value.GetBool(false);
					break;
				case AtomConstTags.ChannelInfo:
						ParseText(child, channel.Info);
					break;
				case AtomConstTags.ItemLink:
						channel.Links.Add(ParseLink(child));
					break;
				case AtomConstTags.ItemModified:
						channel.LastUpdated = child.Value.GetDateTime(DateTime.Now);
					break;
				case AtomConstTags.ChannelCategory:
						channel.Categories.Add(ParseCategory(child));
					break;
				case AtomConstTags.ItemRoot:
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
	private void ParseText(MLNode node, AtomText text)
	{
		text.Mode = node.Attributes[AtomConstTags.AttrMode].Value;
		text.Type = node.Attributes[AtomConstTags.AttrType].Value;
		text.Language = node.Attributes[AtomConstTags.AttrLanguage].Value;
		text.XmlBase = node.Attributes[AtomConstTags.AttrXMLBase].Value;
		text.Content = node.Value;
	}

	/// <summary>
	///		Interpreta el generador de Atom
	/// </summary>
	private void ParseGenerator(MLNode node, AtomGenerator generator)
	{
		generator.URL = node.Attributes[AtomConstTags.AttrURL].Value;
		generator.Version = node.Attributes[AtomConstTags.AttrVersion].Value;
		generator.Language = node.Attributes[AtomConstTags.AttrLanguage].Value;
		generator.Name = node.Value;
	}

	/// <summary>
	///		Interpreta un vínculo de Atom
	/// </summary>
	private AtomLink ParseLink(MLNode node)
	{
		AtomLink link = new()
							{
								Href = node.Attributes[AtomConstTags.AttrHref].Value,
								Rel = node.Attributes[AtomConstTags.AttrRel].Value,
								Title = node.Attributes[AtomConstTags.AttrTitle].Value,
								Type = node.Attributes[AtomConstTags.AttrType].Value
							};

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
		return new AtomCategory
						{
							Name = node.Attributes[AtomConstTags.AttrTerm].Value
						};
	}

	/// <summary>
	///		Interpreta una entrada de un archivo Atom
	/// </summary>
	private AtomEntry ParseEntry(MLNode entryML, AtomChannel channel)
	{
		AtomEntry entry = new();

			// Recorre los nodos
			foreach (MLNode node in entryML.Nodes)
				switch (node.Name)
				{
					case AtomConstTags.ItemID:
							entry.ID = node.Value;
						break;
					case AtomConstTags.ItemTitle:
							ParseText(node, entry.Title);
						break;
					case AtomConstTags.ItemContent:
							ParseText(node, entry.Content);
						break;
					case AtomConstTags.Summary:
							ParseText(node, entry.Summary);
						break;
					case AtomConstTags.ItemIssued:
							entry.DateIssued = node.Value.GetDateTime(DateTime.MinValue);
						break;
					case AtomConstTags.ItemModified:
							entry.DateModified = node.Value.GetDateTime(DateTime.MinValue);
						break;
					case AtomConstTags.ItemCreated:
							entry.DateCreated = node.Value.GetDateTime(DateTime.MinValue);
						break;
					case AtomConstTags.ItemUpdated:
							entry.DateUpdated = node.Value.GetDateTime(DateTime.MinValue);
						break;
					case AtomConstTags.ItemPublished:
							entry.DatePublished = node.Value.GetDateTime(DateTime.MinValue);
						break;
					case AtomConstTags.ItemLink:
							entry.Links.Add(ParseLink(node));
						break;
					case AtomConstTags.ItemAuthor:
							entry.Authors.Add(ParsePeople(node));
						break;
					case AtomConstTags.ItemContributor:
							entry.Contributors.Add(ParsePeople(node));
						break;
					case AtomConstTags.ChannelCategory:
							entry.Categories.Add(ParseCategory(node));
						break;
					case AtomConstTags.cnstsource:
							entry.Source = ParseSource(node);
						break;
					case AtomConstTags.Rights:
							ParseRights(node, entry.Rights);
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
	private void ParseRights(MLNode node, AtomRights rights)
	{
		rights.Type = node.Attributes[AtomConstTags.AttrType].Value;
		rights.Copyright = node.Value;
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
					case AtomConstTags.ChannelId:
							source.ID = node.Value;
						break;
					case AtomConstTags.ChannelTitle:
							source.Title = node.Value;
						break;
					case AtomConstTags.ItemUpdated:
							source.DateUpdated = node.Value.GetDateTime(DateTime.Now);
						break;
					case AtomConstTags.Rights:
							source.Copyright = node.Value;
						break;
				}
			// Devuelve los datos
			return source;
	}

	/// <summary>
	///		Interpreta un nodo XML para obtener los datos de una persona
	/// </summary>
	private AtomPeople ParsePeople(MLNode peopleML)
	{
		AtomPeople people = new();

			// Recorre los nodos
			foreach (MLNode node in peopleML.Nodes)
				switch (node.Name)
				{
					case AtomConstTags.ItemPeopleName:
							people.Name = node.Value;
						break;
					case AtomConstTags.ItemPeopleUrl:
							people.URL = node.Value;
						break;
					case AtomConstTags.ItemPeopleEMail:
							people.EMail = node.Value;
						break;
				}
			// Devuelve el objeto
			return people;
	}
}