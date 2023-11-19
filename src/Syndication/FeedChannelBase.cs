namespace Bau.Libraries.LibFeeds.Syndication;

/// <summary>
///		Base para los datos de un canal de un feed
/// </summary>
public abstract class FeedChannelBase<TypeData> : FeedBase where TypeData : FeedEntryBase
{ 
	/// <summary>
	///		Diccionario de extensiones
	/// </summary>
	internal FeedExtensions.ExtensionParsersCollection Dictionary { get; } = new();

	/// <summary>
	///		Entradas
	///	</summary>
	public FeedEntriesBaseCollection<TypeData> Entries { get; } = new();
}
