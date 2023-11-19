namespace Bau.Libraries.LibFeeds.Syndication.RDF.Data;

/// <summary>
///		Elemento del canal
/// </summary>
/// <example>
///		<item>
///			<title>Title 1</title>
///			<link>Link 1</link>
///			<description>
///				<![CDATA[Description 1]]>
///			</description>
///			<category>Category 1 0</category>
///			<category>Category 1 1</category>
///			<category>Category 1 2</category>
///			<pubDate>Pubdate 1</pubDate>
///			<guid isPermaLink="true">GUID 1</guid>
///			<enclosure url="Enclosure 1" length="1" type="audio/mpeg" />
///			<author>Author 1 0</author>
///			<author>Author 1 1</author>
///			<author>Author 1 2</author>
///		</item>
///	</example>
public class RDFEntry : FeedEntryBase
{
	/// <summary>
	///		Título del elemento
	/// </summary>
	public string? Title { get; set; }

	/// <summary>
	///		Vínculo del elemento
	/// </summary>
	public string? Link { get; set; }
	
	/// <summary>
	///		Contenido del elemento
	/// </summary>
	public string? Content { get; set; }
}