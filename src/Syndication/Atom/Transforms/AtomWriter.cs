using Bau.Libraries.LibFeeds.Syndication.Atom.Data;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;
using Bau.Libraries.LibMarkupLanguage.Tools;

namespace Bau.Libraries.LibFeeds.Syndication.Atom.Transforms;

/// <summary>
///		Clase para escribir un archivo Atom
/// </summary>
public class AtomWriter
{
	/// <summary>
	///		Obtiene el XML de un canal Atom
	/// </summary>
	public string? GetXML(AtomChannel channel) => GetFile(channel).ToString();

	/// <summary>
	///		Graba los datos de un objeto Atom en un archivo XML
	/// </summary>
	public void Save(AtomChannel channel, string fileName)
	{
		new XMLWriter().Save(fileName, GetFile(channel));
	}

	/// <summary>
	///		Obtiene el builder XML de un objeto Atom
	/// </summary>
	private MLFile GetFile(AtomChannel channel)
	{
		MLFile file = new MLFile();
		MLNode node;

			// Añade la cabecera
			node = file.Nodes.Add(AtomConstTags.ChannelRoot);
			node.Attributes.Add("xmlns", "http://purl.org/atom/ns#");
			node.NameSpaces.AddRange(channel.Extensions.GetNameSpaces(channel));
			node.Attributes.Add("version", "0.3");
			// Obtiene el XML de los datos
			node.Nodes.Add(AtomConstTags.ChannelId, channel.ID);
			AddText(node, AtomConstTags.ChannelTitle, channel.Title);
			AddText(node, AtomConstTags.ChannelTagline, channel.TagLine);
			AddText(node, AtomConstTags.cnstsubTitle, channel.Subtitle);
			node.Nodes.Add(AtomConstTags.cnsticon, channel.Icon);
			node.Nodes.Add(AtomConstTags.Logo, channel.Logo);
			AddRights(node, channel.Rights);
			AddGenerator(node, channel.Generator);
			node.Nodes.Add(AtomConstTags.ChannelConvertLineBreaks, channel.ConvertLineBreaks);
			AddText(node, AtomConstTags.ChannelInfo, channel.Info);
			AddLinks(node, channel.Links);
			AddDate(node, AtomConstTags.ItemModified, channel.LastUpdated);
			AddCategories(node, channel.Categories);
			// Obtiene el XML de las extensiones
			channel.Extensions.AddNodesExtension(node);
			// Obtiene el XML de los elementos
			AddItems(node, channel.Entries);
			// Devuelve los datos
			return file;
	}

	/// <summary>
	///		Añade al XML un nodo identificando un texto Atom
	/// </summary>
	private void AddText(MLNode parent, string tag, AtomText text)
	{
		MLNode node = parent.Nodes.Add(tag, text.Content);

			// Crea la cadena de atributos
			node.Attributes.Add(AtomConstTags.AttrMode, text.Mode);
			node.Attributes.Add(AtomConstTags.AttrType, text.Type);
			node.Attributes.Add(AtomConstTags.AttrLanguage, text.Language);
			node.Attributes.Add(AtomConstTags.AttrXMLBase, text.XmlBase);
	}

	/// <summary>
	///		Añade al XML un nodo identificando el generador del archivo Atom
	/// </summary>
	private void AddGenerator(MLNode parent, AtomGenerator generator)
	{
		MLNode node = parent.Nodes.Add(AtomConstTags.ChannelGenerator, generator.Name);

			// Crea la cadena de atributos
			node.Attributes.Add(AtomConstTags.AttrURL, generator.URL);
			node.Attributes.Add(AtomConstTags.AttrVersion, generator.Version);
			node.Attributes.Add(AtomConstTags.AttrLanguage, generator.Language);
	}

	/// <summary>
	///		Añade los datos de los derechos al XML
	/// </summary>
	private void AddRights(MLNode parent, AtomRights objRights)
	{
		MLNode node = parent.Nodes.Add(AtomConstTags.Rights, objRights.Copyright);

			// Atributos
			node.Attributes.Add(AtomConstTags.AttrType, objRights.Type);
	}

	/// <summary>
	///		Añade los elementos al XML
	/// </summary>
	private void AddItems(MLNode parent, FeedEntriesBaseCollection<AtomEntry> entries)
	{
		foreach (AtomEntry entry in entries)
		{
			MLNode node = parent.Nodes.Add(AtomConstTags.ItemRoot);

			// Datos básicos
			node.Nodes.Add(AtomConstTags.ItemID, entry.ID);
			AddText(node, AtomConstTags.ItemTitle, entry.Title);
			AddText(node, AtomConstTags.ItemContent, entry.Content);
			AddText(node, AtomConstTags.Summary, entry.Summary);
			// Fechas
			AddDate(node, AtomConstTags.ItemIssued, entry.DateIssued);
			AddDate(node, AtomConstTags.ItemModified, entry.DateModified);
			AddDate(node, AtomConstTags.ItemCreated, entry.DateCreated);
			AddDate(node, AtomConstTags.ItemUpdated, entry.DateUpdated);
			AddDate(node, AtomConstTags.ItemPublished, entry.DatePublished);
			// Vínculos
			AddLinks(node, entry.Links);
			// Autores
			AddPeople(node, AtomConstTags.ItemAuthor, entry.Authors);
			AddPeople(node, AtomConstTags.ItemContributor, entry.Contributors);
			// Categorías
			AddCategories(node, entry.Categories);
			// Origen
			AddSource(node, entry.Source);
			// Derechos
			AddRights(node, entry.Rights);
			// Extensiones
			entry.Extensions.AddNodesExtension(node);
		}
	}

	/// <summary>
	///		Añade una fecha formateada
	/// </summary>
	private void AddDate(MLNode node, string tag, DateTime dtmValue)
	{
		node.Nodes.Add(tag, DateTimeHelper.ToStringRfc822(dtmValue));
	}

	/// <summary>
	///		Añade los adjuntos
	/// </summary>
	private void AddLinks(MLNode parent, AtomLinksCollection objColLinks)
	{
		foreach (AtomLink link in objColLinks)
		{
			MLNode node = parent.Nodes.Add(AtomConstTags.ItemLink);

				// Añade los atributos
				if (!string.IsNullOrEmpty(link.Href))
					node.Attributes.Add(AtomConstTags.AttrHref, link.Href);
				if (!string.IsNullOrEmpty(link.Rel))
					node.Attributes.Add(AtomConstTags.AttrRel, link.Rel);
				if (!string.IsNullOrEmpty(link.Type))
					node.Attributes.Add(AtomConstTags.AttrType, link.Type);
				if (!string.IsNullOrEmpty(link.Title))
					node.Attributes.Add(AtomConstTags.AttrTitle, link.Title);
		}
	}

	/// <summary>
	///		Añade los nodos de las categorías
	/// </summary>
	private void AddCategories(MLNode parent, AtomCategoriesCollection objColCategories)
	{
		foreach (AtomCategory category in objColCategories)
		{
			MLNode node = parent.Nodes.Add(AtomConstTags.ChannelCategory);

				// Atributos
				node.Attributes.Add(AtomConstTags.AttrTerm, category.Name);
		}
	}

	/// <summary>
	///		Añade los nodos de autores
	/// </summary>
	private void AddPeople(MLNode parent, string tag, AtomPersonsCollection objColPersons)
	{
		foreach (AtomPeople objPeople in objColPersons)
		{
			MLNode node = parent.Nodes.Add(tag);

				// Cuerpo
				node.Nodes.Add(AtomConstTags.ItemPeopleName, objPeople.Name);
				node.Nodes.Add(AtomConstTags.ItemPeopleUrl, objPeople.URL);
				node.Nodes.Add(AtomConstTags.ItemPeopleEMail, objPeople.EMail);
		}
	}

	/// <summary>
	///		Añade los datos del origen
	/// </summary>
	private void AddSource(MLNode parent, AtomSource source)
	{
		MLNode node = parent.Nodes.Add(AtomConstTags.cnstsource);

			// Cuerpo
			node.Nodes.Add(AtomConstTags.ChannelId, source.ID);
			node.Nodes.Add(AtomConstTags.ChannelTitle, source.Title);
			AddDate(node, AtomConstTags.ItemUpdated, source.DateUpdated);
			node.Nodes.Add(AtomConstTags.Rights, source.Copyright);
	}
}