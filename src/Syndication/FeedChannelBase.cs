using System;

namespace Bau.Libraries.LibFeeds.Syndication
{
	/// <summary>
	///		Base para los datos de un canal de un feed
	/// </summary>
	public abstract class FeedChannelBase<TypeData> : FeedBase where TypeData : FeedEntryBase
	{ 
		// Variables privadas
		private FeedEntriesBaseCollection<TypeData> objEntries;

		public FeedChannelBase()
		{
			Dictionary = new FeedExtensions.ExtensionParsersCollection();
		}

		/// <summary>
		///		Diccionario de extensiones
		/// </summary>
		internal FeedExtensions.ExtensionParsersCollection Dictionary { get; private set; }

		/// <summary>
		///		Entradas
		///	</summary>
		public FeedEntriesBaseCollection<TypeData> Entries
		{
			get
			{ 
				// Crea la colección de entradas
				if (objEntries == null)
					objEntries = new FeedEntriesBaseCollection<TypeData>();
				// Devuelve la colección
				return objEntries;
			}
		}
	}
}
