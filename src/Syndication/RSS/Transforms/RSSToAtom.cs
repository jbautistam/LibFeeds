using Bau.Libraries.LibFeeds.Syndication.Atom.Data;
using Bau.Libraries.LibFeeds.Syndication.RSS.Data;

namespace Bau.Libraries.LibFeeds.Syndication.RSS.Transforms;

/// <summary>
///		Clase para conversión de archivos RSS a Atom
/// </summary>
public class RSSToAtom
{
	/// <summary>
	///		Convierte un archivo RSS en un archivo Atom
	/// </summary>
	public AtomChannel Convert(RSSChannel rss)
	{
		AtomChannel channel = new();

			// Convierte los datos del canal
			channel.ID = new Guid().ToString();
			channel.Title = ConvertText(rss.Title);
			channel.Generator = ConvertGenerator(rss.Generator);
			channel.ConvertLineBreaks = true;
			channel.Info = ConvertText(rss.Description);
			channel.Subtitle = ConvertText("");
			channel.Links.Add(ConvertLink(rss.Link, AtomLink.AtomLinkType.Self));
			channel.LastUpdated = rss.LastBuildDate;
			channel.Icon = rss.Logo.Url;
			channel.Logo = rss.Logo.Url;
			// Añade las extensiones
			ConvertExtension(rss.Extensions, channel.Extensions);
			// Añade las entradas
			ConvertEntries(rss, channel);
			// Devuelve el objeto Atom
			return channel;
	}

	/// <summary>
	///		Convierte un texto a Atom
	/// </summary>
	private AtomText ConvertText(string text)
	{
		return new AtomText()
						{ 
							Mode = "escaped", 
							Type = "text/html", 
							Content = text
						};
	}

	/// <summary>
	///		Convierte un generador a Atom
	/// </summary>
	private AtomGenerator ConvertGenerator(string generator)
	{
		return new AtomGenerator
					{
						Name = generator
					};
	}

	/// <summary>
	///		Convierte las entradas RSS en entradas Atom
	/// </summary>
	private void ConvertEntries(RSSChannel rss, AtomChannel channel)
	{
		foreach (RSSEntry rssEntry in rss.Entries)
		{
			AtomEntry channelEntry = new AtomEntry();

				// Convierte los datos de la entrada
				channelEntry.ID = rssEntry.ID;
				channelEntry.Title = ConvertText(rssEntry.Title);
				channelEntry.Content = ConvertText(rssEntry.Content);
				channelEntry.DateIssued = rssEntry.DateCreated;
				channelEntry.DateCreated = rssEntry.DateCreated;
				channelEntry.DateModified = rssEntry.DateCreated;
				channelEntry.DateUpdated = rssEntry.DateCreated;
				channelEntry.DatePublished = rssEntry.DateCreated;
				// Vínculos
				channelEntry.Links.Add(ConvertLink(rssEntry.Link, AtomLink.AtomLinkType.Self));
				foreach (RSSEnclosure rssEnclosure in rssEntry.Enclosures)
					channelEntry.Links.Add(ConvertLink(rssEnclosure));
				// Autores
				foreach (RSSAuthor rssAuthor in rssEntry.Authors)
					channelEntry.Authors.Add(ConvertAuthor(rssAuthor));
				// Categorías
				foreach (RSSCategory rssCategory in rssEntry.Categories)
					channelEntry.Categories.Add(ConvertCategory(rssCategory));
				// Convierte las extensiones
				ConvertExtension(rssEntry.Extensions, channelEntry.Extensions);
				// Añade la entrada al objeto Atom
				channel.Entries.Add(channelEntry);
		}
	}

	/// <summary>
	///		Convierte un vínculo a Atom
	/// </summary>
	private AtomLink ConvertLink(string url, AtomLink.AtomLinkType linkType)
	{
		return new AtomLink
						{
							Href = url,
							LinkType = linkType
						};
	}

	/// <summary>
	///		Convierte un adjunto
	/// </summary>
	private AtomLink ConvertLink(RSSEnclosure rssEnclosure)
	{
		return new AtomLink
					{
						Href = rssEnclosure.Url,
						LinkType = AtomLink.AtomLinkType.Enclosure,
						Type = rssEnclosure.Type
					};
	}

	/// <summary>
	///		Convierte los datos de una persona
	/// </summary>
	private AtomPeople ConvertAuthor(RSSAuthor rssAuthor)
	{
		AtomPeople people = new AtomPeople();

			// Pasa los datos
			people.Name = rssAuthor.Name;
			// Devuelve los datos de la persona
			return people;
	}

	/// <summary>
	///		Convierte una categoría
	/// </summary>
	private AtomCategory ConvertCategory(RSSCategory rssCategory)
	{
		return new AtomCategory
						{ 
							Name = rssCategory.Text
						};
	}

	/// <summary>
	///		Convierte las extensiones
	/// </summary>
	private void ConvertExtension(FeedExtensions.ExtensionsCollection rssExtensions,
								  FeedExtensions.ExtensionsCollection channelExtensions)
	{
		foreach (FeedExtensions.ExtensionBase extension in rssExtensions)
			channelExtensions.Add(extension);
	}
}
