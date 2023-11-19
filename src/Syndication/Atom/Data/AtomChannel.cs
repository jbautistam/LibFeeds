namespace Bau.Libraries.LibFeeds.Syndication.Atom.Data;

/// <summary>
///		Clase con los datos de un canal Atom
/// </summary>
public class AtomChannel : FeedChannelBase<AtomEntry>
{ 
	/// <summary>
	///		ID
	/// </summary>
	public string ID { get; set; } = Guid.NewGuid().ToString();

	/// <summary>
	///		Título del canal
	/// </summary>
	public AtomText Title { get; set; } = new();

	/// <summary>
	///		Comentarios
	/// </summary>
	public AtomText TagLine { get; set; } = new();

	/// <summary>
	///		Generador
	/// </summary>
	public AtomGenerator Generator { get; set; } = new();

	/// <summary>
	///		Indica si se deben convertir las rupturas de línea
	/// </summary>
	public bool ConvertLineBreaks { get; set; }

	/// <summary>
	///		Información
	/// </summary>
	public AtomText Info { get; set; } = new();

	/// <summary>
	///		Subtítulo
	/// </summary>
	public AtomText Subtitle { get; set; } = new();

	/// <summary>
	///		Vínculos
	/// </summary>
	public AtomLinksCollection Links { get; } = new();

	/// <summary>
	///		Ultima modificación
	/// </summary>
	public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

	/// <summary>
	///		Archivo de imagen para el icon
	/// </summary>
	public string Icon { get; set; } = default!;

	/// <summary>
	///		Archivo de imagen para el logo
	/// </summary>
	public string Logo { get; set; } = default!;

	/// <summary>
	///		Derechos
	/// </summary>
	public AtomRights Rights { get; } = new();

	/// <summary>
	///		Categorías
	/// </summary>
	public AtomCategoriesCollection Categories { get; } = new();
}
