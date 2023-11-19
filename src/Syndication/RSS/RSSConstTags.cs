namespace Bau.Libraries.LibFeeds.Syndication.RSS;

/// <summary>
///		Etiquetas de un archivo RSS
/// </summary>
internal static class RSSConstTags
{	
	// Constantes con el nombre del nodo base
	internal const string Root = "rss";
	// Constantes con el nombre de la raíz del canal
	internal const string Channel = "channel";
	// Constantes del canal
	internal const string ChannelTitle = "title";
	internal const string ChannelDescription = "description";
	internal const string ChannelLink = "link";
	internal const string ChannelLanguage = "language";
	internal const string ChannelCopyright = "copyright";
	internal const string ChannelPubDate = "pubDate";
	internal const string ChannelLastBuildDate = "lastBuildDate";
	internal const string ChannelManagingEditor = "managingEditor";
	internal const string ChannelManagingWebMaster = "webMaster";
	internal const string ChannelGenerator = "generator";
	// Constantes de la imagen del canal
	internal const string ChannelImage = "image";
	internal const string ChannelImageUrl = "url";
	internal const string ChannelImageTitle = "title";
	internal const string ChannelImageLink = "link";
	// Constantes con el nombre de la raíz del elemento
	internal const string Item = "item";
	// Constantes con el nombre de la raíz del elemento
	internal const string ItemTitle = "title";
	internal const string ItemLink = "link";
	internal const string ItemDescription = "description";
	internal const string ItemCategory = "category"; // .. puede haber más de una
	internal const string ItemPubDate = "pubDate";
	internal const string ItemGuid = "guid";
	internal const string ItemEnclosure = "enclosure"; // ... puede haber más de uno
	internal const string ItemAuthor = "author"; // ... puede haber más de uno
	// Constantes con los atributos del GUID de un elemento
	internal const string ItemAttrPermaLink = "isPermaLink"; // ...  atributo de GUID
	// Constantes con los atributos del enclosure de un elemento
	internal const string ItemAttrUrl = "url"; // ...  atributo de enclosure
	internal const string ItemAttrLength = "length"; // ...  atributo de enclosure
	internal const string ItemAttrType = "type"; // ...  atributo de enclosure
}
