using System;

namespace Bau.Libraries.LibFeeds.Syndication.OPML.Data
{
	/// <summary>
	///		Clase con los datos de un elemento de un archivo OPML
	/// </summary>
	public class OPMLEntry : FeedEntryBase
	{
		public OPMLEntry()
		{
			Entries = new OPMLEntriesCollection();
		}

		/// <summary>
		///		Tipo de la entrada
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		///		Título
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///		Texto
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		///		URL de la entrada
		/// </summary>
		public string URL { get; set; }

		/// <summary>
		///		Entradas
		/// </summary>
		public OPMLEntriesCollection Entries { get; private set; }
	}
}
