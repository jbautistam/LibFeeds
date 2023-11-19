using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibFeeds.Syndication.FeedExtensions.Yahoo.Data;
using Bau.Libraries.LibMarkupLanguage;

namespace Bau.Libraries.LibFeeds.Syndication.FeedExtensions.Yahoo.Transforms;

/// <summary>
///		Parser para los elementos de Yahoo
/// </summary>
internal class YahooMediaParser : ExtensionParserBase
{
	internal YahooMediaParser(string strXMLNameSpace, string prefix) : base(strXMLNameSpace, prefix) { }

	/// <summary>
	///		Interpreta los datos de un nodo XML
	/// </summary>
	internal override void Parse(MLNode node, FeedBase feed)
	{
		YahooMedia objYahoo = (YahooMedia) feed.Extensions.Search(YahooMediaConstTags.XMLDefaultNameSpace);

			// Si no la encuentra la crea
			if (objYahoo == null)
			{ 
				// Crea la extensión
				objYahoo = new YahooMedia();
				// ... y la añade a la colección
				feed.Extensions.Add(objYahoo);
			}
			// Interpreta la extensión
			Parse(node, objYahoo);
	}

	/// <summary>
	///		Interpreta un nodo
	/// </summary>
	private void Parse(MLNode node, YahooMedia objYahoo)
	{
		if (node.Prefix.Equals(base.Prefix))
			switch (node.Name)
			{
				case YahooMediaConstTags.YahooMediaThumbnail:
						objYahoo.Thumbnail = ParseThumbnail(node);
					break;
			}
	}

	/// <summary>
	///		Interpreta los datos de un thumbnail
	/// </summary>
	private YahooMediaThumbnail ParseThumbnail(MLNode node)
	{
		YahooMediaThumbnail objThumbnail = new YahooMediaThumbnail();

			// Interpreta el objeto
			objThumbnail.Url = node.Attributes[YahooMediaConstTags.YahooMediaThumbnailAttrUrl].Value;
			objThumbnail.Width = node.Attributes[YahooMediaConstTags.YahooMediaThumbnailAttrWidth].Value.GetInt(0);
			objThumbnail.Height = node.Attributes[YahooMediaConstTags.YahooMediaThumbnailAttrHeight].Value.GetInt(0);
			// Devuelve el thumbnail
			return objThumbnail;
	}
}
