namespace Bau.Libraries.LibFeeds.Syndication.Atom.Data;

/// <summary>
///		Clase con los datos de un texto para Atom
/// </summary>
public class AtomText
{
	/// <summary>
	///		Modo (escaped, xml, ...)
	/// </summary>
	public string Mode { get; set; } = default!;

	/// <summary>
	///		Tipo (text/html ...)
	/// </summary>
	public string Type { get; set; } = default!;

	/// <summary>
	///		Idioma (en-US ...)
	/// </summary>
	public string Language { get; set; } = default!;

	/// <summary>
	///		XML base
	/// </summary>
	public string XmlBase { get; set; } = default!;

	/// <summary>
	///		Contenido
	/// </summary>
	public string Content { get; set; } = default!;
}
