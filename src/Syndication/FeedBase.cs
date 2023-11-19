namespace Bau.Libraries.LibFeeds.Syndication;

/// <summary>
///		Base de un feed
/// </summary>
public class FeedBase
{
	public FeedBase()
	{
		Extensions = new FeedExtensions.ExtensionsCollection(this);
	}

	/// <summary>
	///		Extensiones de un canal
	/// </summary>
	public FeedExtensions.ExtensionsCollection Extensions { get; private set; }
}
