namespace Bau.Libraries.LibFeeds.Syndication.Atom.Data;

/// <summary>
///		Datos de la fuente de un artículo
/// </summary>
public class AtomSource
{
	/// <summary>
	///		ID
	/// </summary>
	public string? ID { get; set; }
	
	/// <summary>
	///		Título
	/// </summary>
	public string? Title { get; set; }
	
	/// <summary>
	///		Fecha modificación
	/// </summary>
	public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
	
	/// <summary>
	///		Copyright
	/// </summary>
	public string? Copyright { get; set; }
}
