using System;

using Bau.Libraries.LibFeeds.Syndication.FeedExtensions.RSSContent.Data;
using Bau.Libraries.LibMarkupLanguage;

namespace Bau.Libraries.LibFeeds.Syndication.FeedExtensions.RSSContent.Transforms
{
	/// <summary>
	///		Escribe los datos de <see cref="RSSContent"/> sobre un archivo XML
	/// </summary>
	internal class RSSContentWriter
	{
		/// <summary>
		///		Escribe los datos de un <see cref="RSSContent"/>
		/// </summary>
		internal void AddNodesExtension(MLNode parent, RSSContentData content)
		{
			parent.Nodes.Add(RSSContentConstTags.cnstStrXMLDefaultPrefix,
							 RSSContentConstTags.cnstStrRSSContentEncoded,
							 content.ContentEncoded);
		}
	}
}
