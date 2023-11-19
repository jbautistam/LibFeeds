namespace Bau.Libraries.LibFeeds.Syndication;

/// <summary>
///		Colección de <see cref="FeedEntryBase"/>
/// </summary>
public class FeedEntriesBaseCollection<TypeData> : List<TypeData> where TypeData : FeedEntryBase
{
	/// <summary>
	///		Elimina una entrada a partir de su ID
	/// </summary>
	public void Remove(string id)
	{
		for (int index = Count - 1; index >= 0; index--)
			if (this[index].ID.Equals(id))
				RemoveAt(index);
	}

	/// <summary>
	///		Comprueba si existe un ID
	/// </summary>
	public bool Exists(string id) => Search(id) is not null;

	/// <summary>
	///		Busca un elemento
	/// </summary>
	public TypeData? Search(string id) => this.FirstOrDefault(item => item.ID.Equals(id, StringComparison.CurrentCultureIgnoreCase));
}
