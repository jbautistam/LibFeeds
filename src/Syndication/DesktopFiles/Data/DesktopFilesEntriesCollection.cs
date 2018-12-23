using System;

namespace Bau.Libraries.LibFeeds.Syndication.DesktopFiles.Data
{
	/// <summary>
	///		Colección de <see cref="DesktopFilesEntry"/>
	/// </summary>
	public class DesktopFilesEntriesCollection : System.Collections.Generic.List<DesktopFilesEntry>
	{
		/// <summary>
		///		Añade una serie de entradas
		/// </summary>
		public void Add(DesktopFilesEntriesCollection entries)
		{
			foreach (DesktopFilesEntry entry in entries)
				Add(entry);
		}

		/// <summary>
		///		Busca un elemento en la colección
		/// </summary>
		public DesktopFilesEntry Search(string id)
		{
			DesktopFilesEntry objFoundEntry = null;

				// Recorre la colección
				foreach (DesktopFilesEntry entry in this)
					if (objFoundEntry == null)
					{
						if (entry.ID.Equals(id))
							return entry;
						else
							objFoundEntry = entry.Entries.Search(id);
					}
				// Devuelve la entrada encontrada (si ha habido alguna)
				return objFoundEntry;
		}

		/// <summary>
		///		Comprueba si existe un elemento con este ID
		/// </summary>
		public bool Exists(string id)
		{
			return Search(id) != null;
		}

		/// <summary>
		///		Elimina un elemento de la entrada
		/// </summary>
		internal bool Remove(string id)
		{
			bool deleted = false;

			// Busca el elemento y lo elimina cuando lo encuentra		
			for (int index = Count - 1; index >= 0 && !deleted; index--)
				if (this[index].ID.Equals(id))
				{ 
					// Elimina el elemento
					RemoveAt(index);
					// Indica que lo ha borrado
					deleted = true;
				}
				else
					deleted = this[index].Entries.Remove(id);
			// Devuelve el valor que indica si lo ha borrado
			return deleted;
		}

		/// <summary>
		///		Cuenta el número de elementos no leídos de una entrada
		/// </summary>
		internal int CountNotRead()
		{
			int intNotRead = 0;

				// Acumula los elementos no leídos
				foreach (DesktopFilesEntry entry in this)
					if (entry.Enabled)
						intNotRead += entry.CountNotRead();
				// Devuelve el número de elementos no leídos
				return intNotRead;
		}

		/// <summary>
		///		Ordenación
		/// </summary>
		public new void Sort()
		{ 
			// Ordena la colección
			base.Sort();
			// Ordena las entradas
			foreach (DesktopFilesEntry entry in this)
				entry.Entries.Sort();
		}
	}
}
