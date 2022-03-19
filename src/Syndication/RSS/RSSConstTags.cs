using System;

namespace Bau.Libraries.LibFeeds.Syndication.RSS
{
	/// <summary>
	///		Etiquetas de un archivo RSS
	/// </summary>
	internal static class RSSConstTags
	{	
		// Constantes con el nombre del nodo base
		internal const string cnstStrRoot = "rss";
		// Constantes con el nombre de la raíz del canal
		internal const string cnstStrChannel = "channel";
		// Constantes del canal
		internal const string cnstStrChannelTitle = "title";
		internal const string cnstStrChannelDescription = "description";
		internal const string cnstStrChannelLink = "link";
		internal const string cnstStrChannelLanguage = "language";
		internal const string cnstStrChannelCopyright = "copyright";
		internal const string cnstStrChannelPubDate = "pubDate";
		internal const string cnstStrChannelLastBuildDate = "lastBuildDate";
		internal const string cnstStrChannelManagingEditor = "managingEditor";
		internal const string cnstStrChannelManagingWebMaster = "webMaster";
		internal const string cnstStrChannelGenerator = "generator";
		// Constantes de la imagen del canal
		internal const string cnstStrChannelImage = "image";
		internal const string cnstStrChannelImageUrl = "url";
		internal const string cnstStrChannelImageTitle = "title";
		internal const string cnstStrChannelImageLink = "link";
		// Constantes con el nombre de la raíz del elemento
		internal const string cnstStrItem = "item";
		// Constantes con el nombre de la raíz del elemento
		internal const string cnstStrItemTitle = "title";
		internal const string cnstStrItemLink = "link";
		internal const string cnstStrItemDescription = "description";
		internal const string cnstStrItemCategory = "category"; // .. puede haber más de una
		internal const string cnstStrItemPubDate = "pubDate";
		internal const string cnstStrItemGuid = "guid";
		internal const string cnstStrItemEnclosure = "enclosure"; // ... puede haber más de uno
		internal const string cnstStrItemAuthor = "author"; // ... puede haber más de uno
		// Constantes con los atributos del GUID de un elemento
		internal const string cnstStrItemAttrPermaLink = "isPermaLink"; // ...  atributo de GUID
		// Constantes con los atributos del enclosure de un elemento
		internal const string cnstStrItemAttrUrl = "url"; // ...  atributo de enclosure
		internal const string cnstStrItemAttrLength = "length"; // ...  atributo de enclosure
		internal const string cnstStrItemAttrType = "type"; // ...  atributo de enclosure
	}
}
