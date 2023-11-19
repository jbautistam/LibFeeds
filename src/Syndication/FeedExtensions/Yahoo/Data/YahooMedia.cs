namespace Bau.Libraries.LibFeeds.Syndication.FeedExtensions.Yahoo.Data;

/// <summary>
///		Extensión Yahoo media
/// </summary>
/// <example>
/// <media:thumbnail xmlns:media="http://search.yahoo.com/mrss/" 
///									 url="http://3.bp.blogspot.com/353683.jpg" 
///									 height="72" width="72" />
///	</example>
public class YahooMedia : ExtensionBase
{
	public YahooMedia()
	{
		Thumbnail = new YahooMediaThumbnail();
	}

	/// <summary>
	///		Datos de un thumbnail
	/// </summary>
	public YahooMediaThumbnail Thumbnail { get; set; }

	/// <summary>
	///		Espacio de nombres de la extensión
	/// </summary>
	public override string NameSpace => YahooMediaConstTags.XMLDefaultNameSpace;

	/// <summary>
	///		Prefijo de la extensión
	/// </summary>
	public override string Prefix => YahooMediaConstTags.XMLDefaultPrefix;
}
