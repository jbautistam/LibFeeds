using System;
using System.Collections.Generic;

namespace Bau.Libraries.LibFeeds.Syndication
{
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
		public bool Exists(string id)
		{
			return Search(id) != null;
		}

		/// <summary>
		///		Busca un elemento
		/// </summary>
		public TypeData Search(string id)
		{ 
			// Recorre los elementos buscando el ID
			foreach (TypeData entry in this)
				if (entry.ID == id)
					return entry;
			// Si ha llegado hasta aquí es porque no ha encontrado nada
			return null;
		}
	}
}
