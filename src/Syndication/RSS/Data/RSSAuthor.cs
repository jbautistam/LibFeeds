namespace Bau.Libraries.LibFeeds.Syndication.RSS.Data;

/// <summary>
///		Autor del archivo RSS
/// </summary>
public class RSSAuthor
{
	public RSSAuthor(string name)
	{
		Name = name;
	}

	/// <summary>
	///		Nombre del autor
	/// </summary>
	public string Name { get; set; }
}