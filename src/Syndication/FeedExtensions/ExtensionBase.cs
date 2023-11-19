namespace Bau.Libraries.LibFeeds.Syndication.FeedExtensions;

/// <summary>
///		Base para la raíz de una extensión
/// </summary>
public abstract class ExtensionBase
{
	/// <summary>
	///		Nombre del espacio de nombres
	/// </summary>
	public abstract string NameSpace { get; }

	/// <summary>
	///		Prefijo de la extensión
	/// </summary>
	public abstract string Prefix { get; }
}
