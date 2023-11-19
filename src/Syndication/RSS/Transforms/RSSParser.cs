using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibFeeds.Syndication.RSS.Data;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;

namespace Bau.Libraries.LibFeeds.Syndication.RSS.Transforms;

/// <summary>
///		Parser para un archivo RSS
/// </summary>
public class RSSParser
{
	/// <summary>
	///		Interpreta un archivo
	/// </summary>
	public RSSChannel? Parse(string fileName) => Parse(new XMLParser().Load(fileName));

	/// <summary>
	///		Interpreta un texto
	/// </summary>
	public RSSChannel? ParseText(string text) => Parse(new XMLParser().ParseText(text));

	/// <summary>
	///		Interpreta los datos de un archivo XML
	/// </summary>
	public RSSChannel? Parse(MLFile fileML)
	{
		RSSChannel? rss = null;

			// Recorre los nodos del documento
			if (fileML != null)
				foreach (MLNode node in fileML.Nodes)
					if (node.Name.Equals(RSSConstTags.Root))
					{ 
						// Crea el objeto
						rss = new RSSChannel();
						// Lee los espacios de nombres de las extensiones
						rss.Dictionary.LoadNameSpaces(node);
						// Lee los datos
						foreach (MLNode channel in node.Nodes)
							if (channel.Name.Equals(RSSConstTags.Channel))
								ParseChannel(channel, rss);
					}
			// Devuelve el objeto RSS
			return rss;
	}

	/// <summary>
	///		Interpreta los datos del canal
	/// </summary>
	private void ParseChannel(MLNode channel, RSSChannel rss)
	{
		foreach (MLNode node in channel.Nodes)
			switch (node.Name)
			{
				case RSSConstTags.ChannelTitle:
						rss.Title = node.Value;
					break;
				case RSSConstTags.ChannelDescription:
						rss.Description = node.Value;
					break;
				case RSSConstTags.ChannelLink:
						rss.Link = node.Value;
					break;
				case RSSConstTags.ChannelLanguage:
						rss.Language = node.Value;
					break;
				case RSSConstTags.ChannelCopyright:
						rss.Copyright = node.Value;
					break;
				case RSSConstTags.ChannelPubDate:
						rss.PubDate = node.Value.GetDateTime(DateTime.Now);
					break;
				case RSSConstTags.ChannelLastBuildDate:
						rss.LastBuildDate = node.Value.GetDateTime(DateTime.Now);
					break;
				case RSSConstTags.ChannelManagingEditor:
						rss.Editor = node.Value;
					break;
				case RSSConstTags.ChannelManagingWebMaster:
						rss.WebMaster = node.Value;
					break;
				case RSSConstTags.ChannelGenerator:
						rss.Generator = node.Value;
					break;
				case RSSConstTags.ChannelImage:
						rss.Logo = ParseImage(node);
					break;
				case RSSConstTags.Item:
						rss.Entries.Add(ParseEntry(node, rss));
					break;
				default:
						rss.Extensions.Parse(node, rss, rss.Dictionary);
					break;
			}
	}

	/// <summary>
	///		Interpreta los nodos de una imagen
	/// </summary>
	private RSSImage ParseImage(MLNode imageML)
	{
		RSSImage image = new();

			// Lee los datos de la imagen
			foreach (MLNode node in imageML.Nodes)
				switch (node.Name)
				{
					case RSSConstTags.ChannelImageUrl:
							image.Url = node.Value;
						break;
					case RSSConstTags.ChannelImageTitle:
							image.Title = node.Value;
						break;
					case RSSConstTags.ChannelImageLink:
							image.Link = node.Value;
						break;
				}
			// Devuelve la imagen
			return image;
	}

	/// <summary>
	///		Interpreta los nodos de un elemento
	/// </summary>
	private RSSEntry ParseEntry(MLNode objMLEntry, RSSChannel channel)
	{
		RSSEntry entry = new();

			// Interpreta los nodos
			foreach (MLNode node in objMLEntry.Nodes)
				switch (node.Name)
				{
					case RSSConstTags.ItemTitle:
							entry.Title = node.Value;
						break;
					case RSSConstTags.ItemLink:
							entry.Link = node.Value;
						break;
					case RSSConstTags.ItemDescription:
							entry.Content = node.Value;
						break;
					case RSSConstTags.ItemCategory:
							entry.Categories.Add(ParseCategory(node));
						break;
					case RSSConstTags.ItemPubDate:
							entry.DateCreated = node.Value.GetDateTime(DateTime.Now);
						break;
					case RSSConstTags.ItemGuid:
							entry.GUID = ParseGuid(node);
						break;
					case RSSConstTags.ItemEnclosure:
							entry.Enclosures.Add(ParseEnclosure(node));
						break;
					case RSSConstTags.ItemAuthor:
							entry.Authors.Add(ParseAuthor(node));
						break;
					default:
							entry.Extensions.Parse(node, entry, channel.Dictionary);
						break;
				}
			// Devuelve la entrada
			return entry;
	}

	/// <summary>
	///		Interpreta el ID
	/// </summary>
	private RSSGuid ParseGuid(MLNode node)
	{
		return new RSSGuid()
						{
							IsPermaLink = node.Attributes[RSSConstTags.ItemAttrPermaLink].Value.GetBool(false),
							ID = node.Value
						};
	}

	/// <summary>
	///		Interpreta los datos del autor
	/// </summary>
	private RSSAuthor ParseAuthor(MLNode node) => new RSSAuthor(node.Value);

	/// <summary>
	///		Interpreta la categoría
	/// </summary>
	private RSSCategory ParseCategory(MLNode node) => new RSSCategory(node.Value);

	/// <summary>
	///		Interpreta un adjunto
	/// </summary>
	private RSSEnclosure ParseEnclosure(MLNode node)
	{
		RSSEnclosure enclosure = new RSSEnclosure();

			// Recoge los datos
			enclosure.Url = node.Attributes[RSSConstTags.ItemAttrUrl].Value;
			enclosure.Length = node.Attributes[RSSConstTags.ItemAttrLength].Value.GetInt(0);
			enclosure.Type = node.Attributes[RSSConstTags.ItemAttrType].Value;
			// Devuelve el objeto
			return enclosure;
	}
}