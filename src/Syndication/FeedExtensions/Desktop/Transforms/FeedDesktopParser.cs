using System;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibFeeds.Syndication.FeedExtensions.Desktop.Data;
using Bau.Libraries.LibMarkupLanguage;

namespace Bau.Libraries.LibFeeds.Syndication.FeedExtensions.Desktop.Transforms
{
	/// <summary>
	///		Parser para los elementos de <see cref="FeedDesktop"/>
	/// </summary>
	internal class FeedDesktopParser : ExtensionParserBase
	{
		internal FeedDesktopParser(string strXMLNameSpace, string prefix) : base(strXMLNameSpace, prefix) { }

		/// <summary>
		///		Interpreta los datos de un nodo XML
		/// </summary>
		internal override void Parse(MLNode node, FeedBase objFeed)
		{
			FeedDesktop channel = (FeedDesktop) objFeed.Extensions.Search(FeedDesktopConstTags.cnstStrXMLDefaultNameSpace);

			// Si no la encuentra la crea
			if (channel == null)
			{ 
				// Crea la extensión
				channel = new FeedDesktop();
				// ... y la añade a la colección
				objFeed.Extensions.Add(channel);
			}
			// Interpreta la extensión
			Parse(node, channel);
		}

		/// <summary>
		///		Interpreta un nodo
		/// </summary>
		private void Parse(MLNode node, FeedDesktop channel)
		{
			if (node.Prefix.Equals(base.Prefix))
				switch (node.Name)
				{
					case FeedDesktopConstTags.cnstStrRead:
							channel.IsRead = node.Value.GetBool(false);
						break;
					case FeedDesktopConstTags.cnstStrPriority:
							channel.Priority = node.Value.GetInt(0);
						break;
					case FeedDesktopConstTags.cnstStrEnabled:
							channel.Enabled = node.Value.GetBool(true);
						break;
				}
		}
	}
}
