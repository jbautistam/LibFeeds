using Bau.Libraries.LibFeeds.Syndication.FeedExtensions.Desktop.Data;
using Bau.Libraries.LibMarkupLanguage;

namespace Bau.Libraries.LibFeeds.Syndication.FeedExtensions.Desktop.Transforms;

/// <summary>
///		Escribe los datos de <see cref="FeedDesktop"/> sobre un archivo XML
/// </summary>
internal class FeedDesktopWriter
{
	/// <summary>
	///		Escribe los datos de un <see cref="FeedDesktop"/>
	/// </summary>
	internal void AddNodesExtension(MLNode node, FeedDesktop channel)
	{
		node.Nodes.Add(FeedDesktopConstTags.XMLPrefix, FeedDesktopConstTags.Read, channel.IsRead);
		node.Nodes.Add(FeedDesktopConstTags.XMLPrefix, FeedDesktopConstTags.Priority, channel.Priority);
		node.Nodes.Add(FeedDesktopConstTags.XMLPrefix, FeedDesktopConstTags.Enabled, channel.Enabled);
	}
}
