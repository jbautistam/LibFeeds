namespace Bau.Libraries.LibFeeds.Syndication.FeedExtensions.RSSContent.Data;

/// <summary>
///		Extensión Yahoo media
/// </summary>
/// <example>
/// <rss xmlns:content="http://purl.org/rss/1.0/modules/content/" >
///		<content:encoded>Contenido</content:encoded>
/// </rss>
///	</example>
public class RSSContentData : ExtensionBase
{
	/// <summary>
	///		Contenido
	/// </summary>
	public string? ContentEncoded { get; set; }

	/// <summary>
	///		Espacio de nombres de la extensión
	/// </summary>
	public override string NameSpace => RSSContentConstTags.XMLDefaultNameSpace;

	/// <summary>
	///		Prefijo de la extensión
	/// </summary>
	public override string Prefix => RSSContentConstTags.XMLDefaultPrefix;
}
