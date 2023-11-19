using Bau.Libraries.LibFeeds.Syndication.FeedExtensions.RSSContent.Data;
using Bau.Libraries.LibMarkupLanguage;

namespace Bau.Libraries.LibFeeds.Syndication.FeedExtensions.RSSContent.Transforms;

/// <summary>
///		Parser para los elementos de Yahoo
/// </summary>
internal class RSSContentParser : ExtensionParserBase
{
	internal RSSContentParser(string strXMLNameSpace, string prefix) : base(strXMLNameSpace, prefix) { }

	/// <summary>
	///		Interpreta los datos de un nodo XML
	/// </summary>
	internal override void Parse(MLNode node, FeedBase feed)
	{
		RSSContentData content = (RSSContentData) feed.Extensions.Search(RSSContentConstTags.XMLDefaultNameSpace);

			// Si no la encuentra la crea
			if (content == null)
			{ 
				// Crea la extensión
				content = new RSSContentData();
				// ... y la añade a la colección
				feed.Extensions.Add(content);
			}
			// Interpreta la extensión
			Parse(node, content);
	}

	/// <summary>
	///		Interpreta un nodo
	/// </summary>
	private void Parse(MLNode node, RSSContentData content)
	{
		if (node.Prefix.Equals(base.Prefix))
			switch (node.Name)
			{
				case RSSContentConstTags.RSSContentEncoded:
						content.ContentEncoded = node.Value;
					break;
			}
	}
}
