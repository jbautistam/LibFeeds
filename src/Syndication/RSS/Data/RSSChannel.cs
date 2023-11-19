namespace Bau.Libraries.LibFeeds.Syndication.RSS.Data;

/// <summary>
///		Clase con los datos de un canal RSS
/// </summary>
/// <example>
/// <?xml version="1.0" encoding="UTF-8" ?>
///		<rss version="2.0">
/// 		<channel>
/// 			<title>title</title>
/// 			<description>Description</description>
/// 			<link>link</link>
/// 			<language>language</language>
/// 			<copyright>Copyright</copyright>
/// 			<lastBuildDate>LastBuildDate</lastBuildDate>
/// 			<image>
/// 				<url>http://bitacoras.com/public/images/logo.png</url>
/// 				<title>Portada de Bitacoras.com</title>
/// 				<link>http://bitacoras.com/feed/portada</link>
/// 			</image>
/// 			<item>
/// 				<guid isPermaLink="true">GUID 0</guid>
///	 				<link>Link 0</link>
/// 				<title>Title 0</title>
/// 				<description>
/// 					<![CDATA[Description 0]]>
///					</description>
///	 				<pubDate>Pubdate 0</pubDate>
/// 				<enclosure url="Enclosure 0" length="44946940" type="audio/mpeg" />
///					<category>Category 0</category>
/// 				<author>Author 0</author>
/// 			</item>
/// 		</channel>
///		</rss>
/// </example>
public class RSSChannel : FeedChannelBase<RSSEntry>
{
	/// <summary>
	///		Título
	/// </summary>
	public string? Title { get; set; }

	/// <summary>
	///		Descripción
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	///		Vínculo del canal
	/// </summary>
	public string? Link { get; set; }

	/// <summary>
	///		Lenguaje
	/// </summary>
	public string? Language { get; set; }

	/// <summary>
	///		Copyright
	/// </summary>
	public string? Copyright { get; set; }

	/// <summary>
	///		Fecha de publicación
	/// </summary>
	public DateTime PubDate { get; set; } = DateTime.UtcNow;

	/// <summary>
	///		Ultima fecha de generación
	/// </summary>
	public DateTime LastBuildDate { get; set; } = DateTime.UtcNow;

	/// <summary>
	///		Editor
	/// </summary>
	public string? Editor { get; set; }

	/// <summary>
	///		Webmaster
	/// </summary>
	public string? WebMaster { get; set; }

	/// <summary>
	///		Generador
	/// </summary>
	public string? Generator { get; set; }

	/// <summary>
	///		Autor
	/// </summary>
	public RSSAuthor Author { get; set; } = new("Unknown");

	/// <summary>
	///		Logo de la Web que emite el archivo RSS
	/// </summary>
	public RSSImage Logo { get; set; } = new();
}