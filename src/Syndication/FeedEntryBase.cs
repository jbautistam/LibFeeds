namespace Bau.Libraries.LibFeeds.Syndication;

/// <summary>
///		Base para las entradas de un archivo 
/// </summary>
public abstract class FeedEntryBase : FeedBase
{ 
	/// <summary>
	///		ID de la entrada
	/// </summary>
	public virtual string ID { get; set; } = Guid.NewGuid().ToString();

	/// <summary>
	///		Fecha de creación de la entrada
	/// </summary>
	public virtual DateTime DateCreated { get; set; } = DateTime.UtcNow;
}
