namespace Bau.Libraries.LibFeeds.Syndication.Atom.Data;

/// <summary>
///		Generador de Atom
/// </summary>
public class AtomGenerator
{
	/// <summary>
	///		URL
	/// </summary>
	public string URL { get; set; } = default!;

	/// <summary>
	///		Versión
	/// </summary>
	public string Version { get; set; } = default!;

	/// <summary>
	///		Lenguaje
	/// </summary>
	public string Language { get; set; } = default!;

	/// <summary>
	///		Nombre
	/// </summary>
	public string Name { get; set; } = default!;
}
