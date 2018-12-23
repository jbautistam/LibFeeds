using System;

namespace Bau.Libraries.LibFeeds.Syndication.DesktopFiles.Data
{
	/// <summary>
	///		Clase con los datos de un canal de un archivo OPML
	/// </summary>
	public class DesktopFilesChannel
	{
		public DesktopFilesChannel()
		{
			Entries = new DesktopFilesEntriesCollection();
		}

		/// <summary>
		///		Elimina un nodo
		/// </summary>
		public void Remove(string id)
		{
			Entries.Remove(id);
		}

		/// <summary>
		///		Título
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///		Fecha de creación
		/// </summary>
		public DateTime DateCreated { get; set; }

		/// <summary>
		///		Fecha de modificación
		/// </summary>
		public DateTime DateModified { get; set; }

		/// <summary>
		///		Entradas
		/// </summary>
		public DesktopFilesEntriesCollection Entries { get; private set; }
	}
}
