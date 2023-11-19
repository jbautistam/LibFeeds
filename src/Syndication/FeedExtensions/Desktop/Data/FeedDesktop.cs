namespace Bau.Libraries.LibFeeds.Syndication.FeedExtensions.Desktop.Data;

/// <summary>
///		Extensión para los programas de lectura
/// </summary>
public class FeedDesktop : ExtensionBase
{
	public FeedDesktop() : this(false, 0, true) { }

	public FeedDesktop(int intPriority) : this(false, intPriority, true) { }

	public FeedDesktop(bool blnIsRead, int intPriority) : this(blnIsRead, intPriority, true) { }

	public FeedDesktop(bool blnIsRead, int intPriority, bool enabled)
	{
		IsRead = blnIsRead;
		Priority = intPriority;
		Enabled = enabled;
	}

	/// <summary>
	///		Espacio de nombres de la extensión
	/// </summary>
	public override string NameSpace => FeedDesktopConstTags.XMLDefaultNameSpace;

	/// <summary>
	///		Prefijo de la extensión
	/// </summary>
	public override string Prefix => FeedDesktopConstTags.XMLPrefix;

	/// <summary>
	///		Indica si se ha leído la entrada
	/// </summary>
	public bool IsRead { get; set; }

	/// <summary>
	///		Prioridad de la entrada
	/// </summary>
	public int Priority { get; set; }

	/// <summary>
	///		Indica si está activo
	/// </summary>
	public bool Enabled { get; set; }
}
