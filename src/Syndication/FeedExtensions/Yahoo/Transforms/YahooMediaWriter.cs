using Bau.Libraries.LibFeeds.Syndication.FeedExtensions.Yahoo.Data;
using Bau.Libraries.LibMarkupLanguage;

namespace Bau.Libraries.LibFeeds.Syndication.FeedExtensions.Yahoo.Transforms;

/// <summary>
///		Escribe los datos de <see cref="YahooMedia"/> sobre un archivo XML
/// </summary>
internal class YahooMediaWriter
{
	/// <summary>
	///		Escribe los datos de un <see cref="YahooMedia"/>
	/// </summary>
	internal void AddNodesExtension(MLNode parent, YahooMedia objYahoo)
	{
		MLNode node = parent.Nodes.Add(YahooMediaConstTags.XMLDefaultPrefix,
									   YahooMediaConstTags.YahooMediaThumbnail, string.Empty);

			// Atributos
			node.Attributes.Add(YahooMediaConstTags.YahooMediaThumbnailAttrUrl, objYahoo.Thumbnail.Url);
			node.Attributes.Add(YahooMediaConstTags.YahooMediaThumbnailAttrWidth, objYahoo.Thumbnail.Width);
			node.Attributes.Add(YahooMediaConstTags.YahooMediaThumbnailAttrHeight, objYahoo.Thumbnail.Height);
	}
}
