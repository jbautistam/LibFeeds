using System;

namespace Bau.Libraries.LibFeeds.Syndication.RSS.Data
{
	/// <summary>
	///		Elemento del canal
	/// </summary>
	/// <example>
	///		<item>
	///			<title>Title 1</title>
	///			<link>Link 1</link>
	///			<description>
	///				<![CDATA[Description 1]]>
	///			</description>
	///			<category>Category 1 0</category>
	///			<category>Category 1 1</category>
	///			<category>Category 1 2</category>
	///			<pubDate>Pubdate 1</pubDate>
	///			<guid isPermaLink="true">GUID 1</guid>
	///			<enclosure url="Enclosure 1" length="1" type="audio/mpeg" />
	///			<author>Author 1 0</author>
	///			<author>Author 1 1</author>
	///			<author>Author 1 2</author>
	///		</item>
	///	</example>
	public class RSSEntry : FeedEntryBase
	{
		public RSSEntry()
		{
			GUID = new RSSGuid();
			Categories = new RSSCategoriesCollection();
			Authors = new RSSAuthorsCollection();
			Enclosures = new RSSEnclosureCollections();
		}

		/// <summary>
		///		ID del elemento
		/// </summary>
		public override string ID
		{
			get
			{
				if (GUID != null && !string.IsNullOrEmpty(GUID.ID))
					return GUID.ID;
				else
					return Link;
			}
			set
			{ 
				// Crea el nuevo objeto si no existe
				if (GUID == null)
					GUID = new RSSGuid();
				// Asigna el ID
				GUID.ID = value;
			}
		}

		/// <summary>
		///		Título del elemento
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///		Vínculo del elemento
		/// </summary>
		public string Link { get; set; }

		/// <summary>
		///		Contenido del elemento
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		///		Categoría
		/// </summary>
		public RSSCategoriesCollection Categories { get; set; }

		/// <summary>
		///		GUID del elemento
		/// </summary>
		public RSSGuid GUID { get; set; }

		/// <summary>
		///		Adjuntos
		/// </summary>
		public RSSEnclosureCollections Enclosures { get; set; }

		/// <summary>
		///		Autores
		/// </summary>
		public RSSAuthorsCollection Authors { get; set; }
	}
}