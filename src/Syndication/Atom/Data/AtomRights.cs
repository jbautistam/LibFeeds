namespace Bau.Libraries.LibFeeds.Syndication.Atom.Data;

/// <summary>
///		Derechos para un elemento Atom
/// </summary>
public class AtomRights
{
	/// <summary>
	///		Tipo
	/// </summary>
	public string Type { get; set; } = default!;

	/// <summary>
	///		Derechos de copia
	/// </summary>
	public string Copyright { get; set; } = default!;
}
