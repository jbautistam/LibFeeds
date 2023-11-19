using Bau.Libraries.LibFeeds.Syndication.RSS.Data;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;
using Bau.Libraries.LibMarkupLanguage.Tools;

namespace Bau.Libraries.LibFeeds.Syndication.RSS.Transforms;

/// <summary>
///		Clase para escritura de un objeto RSS en un archivo XML
/// </summary>
public class RSSWriter
{
	/// <summary>
	///		Obtiene el XML de un canal RSS
	/// </summary>
	public string GetXML(RSSChannel rss) => new XMLWriter().ConvertToString(GetFile(rss));

	/// <summary>
	///		Graba los datos de un objeto RSS en un archivo XML
	/// </summary>
	public void Save(RSSChannel rss, string fileName)
	{
		new XMLWriter().Save(fileName, GetFile(rss));
	}

	/// <summary>
	///		Obtiene el builder XML de un objeto RSS
	/// </summary>
	private MLFile GetFile(RSSChannel rss)
	{
		MLFile file = new MLFile();
		MLNode node = file.Nodes.Add(RSSConstTags.Root);

			// Añade los atributos de la cabecera
			node.NameSpaces.AddRange(rss.Extensions.GetNameSpaces(rss));
			node.Attributes.Add("version", "2.0");
			// Añade los datos del canal
			node = node.Nodes.Add(RSSConstTags.Channel);
			// Obtiene el XML de los datos
			node.Nodes.Add(RSSConstTags.ChannelTitle, rss.Title);
			node.Nodes.Add(RSSConstTags.ChannelLink, rss.Link);
			node.Nodes.Add(RSSConstTags.ChannelLanguage, rss.Language);
			node.Nodes.Add(RSSConstTags.ChannelCopyright, rss.Copyright);
			node.Nodes.Add(RSSConstTags.ChannelDescription, rss.Description);
			node.Nodes.Add(RSSConstTags.ChannelLastBuildDate,
							  DateTimeHelper.ToStringRfc822(rss.LastBuildDate));
			// Obtiene el XML de la imagen
			AddImage(node, rss.Logo);
			// Obtiene el XML de las extensiones
			rss.Extensions.AddNodesExtension(node);
			// Obtiene el XML de los elementos
			AddItems(node, rss.Entries);
			// Devuelve los datos
			return file;
	}

	/// <summary>
	///		Añade los datos de la imagen
	/// </summary>
	private void AddImage(MLNode parent, RSSImage image)
	{
		if (!string.IsNullOrEmpty(image.Url))
		{
			MLNode node = parent.Nodes.Add(RSSConstTags.ChannelImage);

				// Atributos
				node.Attributes.Add(RSSConstTags.ChannelImageUrl, image.Url);
				node.Attributes.Add(RSSConstTags.ChannelImageTitle, image.Title);
				node.Attributes.Add(RSSConstTags.ChannelImageLink, image.Link);
		}
	}

	/// <summary>
	///		Añade los elementos al XML
	/// </summary>
	private void AddItems(MLNode parent, FeedEntriesBaseCollection<RSSEntry> entries)
	{
		foreach (RSSEntry entry in entries)
		{
			MLNode node = parent.Nodes.Add(RSSConstTags.Item);
			MLNode objID;

				// Datos básicos
				node.Nodes.Add(RSSConstTags.ItemTitle, entry.Title);
				node.Nodes.Add(RSSConstTags.ItemLink, entry.Link);
				node.Nodes.Add(RSSConstTags.ItemDescription, entry.Content);
				AddDate(node, RSSConstTags.ItemPubDate, entry.DateCreated);
				// ID
				objID = node.Nodes.Add(RSSConstTags.ItemGuid, entry.GUID.ID);
				objID.Attributes.Add(RSSConstTags.ItemAttrPermaLink, entry.GUID.IsPermaLink);
				// Categorías
				foreach (RSSCategory category in entry.Categories)
					node.Nodes.Add(RSSConstTags.ItemCategory, category.Text);
				// Adjuntos
				AddEnclosures(node, entry.Enclosures);
				// Autores
				foreach (RSSAuthor objAuthor in entry.Authors)
					node.Nodes.Add(RSSConstTags.ItemAuthor, objAuthor.Name);
				// Obtiene el XML de las extensiones
				entry.Extensions.AddNodesExtension(node);
		}
	}

	/// <summary>
	///		Añade los adjuntos
	/// </summary>
	private void AddEnclosures(MLNode parent, RSSEnclosureCollections objColEnclosures)
	{
		foreach (RSSEnclosure objEnclosure in objColEnclosures)
		{
			MLNode node = parent.Nodes.Add(RSSConstTags.ItemEnclosure);

				// Atributos
				node.Attributes.Add(RSSConstTags.ItemAttrUrl, objEnclosure.Url);
				node.Attributes.Add(RSSConstTags.ItemAttrLength, objEnclosure.Length);
				node.Attributes.Add(RSSConstTags.ItemAttrType, objEnclosure.Type);
		}
	}

	/// <summary>
	///		Añade una fecha formateada
	/// </summary>
	private void AddDate(MLNode node, string tag, DateTime dtmValue)
	{
		node.Nodes.Add(tag, DateTimeHelper.ToStringRfc822(dtmValue));
	}
}
