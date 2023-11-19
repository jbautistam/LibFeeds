namespace Bau.Libraries.LibFeeds.Syndication.OPML.Data;

/// <summary>
///		Clase con los datos de un canal de un archivo OPML
/// </summary>
public class OPMLChannel
{
	/// <summary>
	///		Título
	/// </summary>
	public string? Title { get; set; }

	/// <summary>
	///		Fecha de creación
	/// </summary>
	public DateTime DateCreated { get; set; } = DateTime.UtcNow;

	/// <summary>
	///		Fecha de modificación
	/// </summary>
	public DateTime DateModified { get; set; } = DateTime.UtcNow;

	/// <summary>
	///		Nombre del propietario
	/// </summary>
	public string? OwnerName { get; set; }

	/// <summary>
	///		Correo del propietario
	/// </summary>
	public string? OwnerEMail { get; set; }

	/// <summary>
	///		Entradas del canal
	/// </summary>
	public OPMLEntriesCollection Entries { get; } = new();
}
