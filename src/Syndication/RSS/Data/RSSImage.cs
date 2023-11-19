namespace Bau.Libraries.LibFeeds.Syndication.RSS.Data;

/// <summary>
///		Clase con los datos de una imagen para el archivo RSS
/// </summary>
public class RSSImage
{
	/// <summary>
	///		URL de la imagen
	/// </summary>
	public string? Url { get; set; }

	/// <summary>
	///		Título de la imagen
	/// </summary>
	public string? Title { get; set; }

	/// <summary>
	///		Vínculo asociado a la imagen
	/// </summary>
	public string? Link { get; set; }
}
