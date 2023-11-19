namespace Bau.Libraries.LibFeeds.Syndication.RSS.Data;

/// <summary>
///		Datos del GUID de un elemento RSS
/// </summary>
public class RSSGuid
{
	/// <summary>
	///		ID del elemeto
	/// </summary>
	public string ID { get; set; } = Guid.NewGuid().ToString();

	/// <summary>
	///		Atributo que indica si es permanente
	/// </summary>
	public bool IsPermaLink { get; set; }
}
