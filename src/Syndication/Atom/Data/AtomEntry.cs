using System;

namespace Bau.Libraries.LibFeeds.Syndication.Atom.Data
{
	/// <summary>
	///		Datos de una entrada Atom
	/// </summary>
	public class AtomEntry : FeedEntryBase
	{ 
		// Variables privadas
		private string _id;

		public AtomEntry()
		{
			Title = new AtomText();
			Content = new AtomText();
			Summary = new AtomText();
			Links = new AtomLinksCollection();
			Authors = new AtomPersonsCollection();
			Contributors = new AtomPersonsCollection();
			Categories = new AtomCategoriesCollection();
			Source = new AtomSource();
			Rights = new AtomRights();
		}

		/// <summary>
		///		ID
		/// </summary>
		public override string ID
		{
			get
			{ 
				// Si no existía un ID le asigna el primer vínculo
				if (string.IsNullOrEmpty(_id))
				{
					AtomLinksCollection objColLinks = Links.Search(AtomLink.AtomLinkType.Self);

						if (objColLinks.Count > 0)
							_id = objColLinks[0].Href;
				}
				// Si no existe tampoco el primer vínculo le asigna el título
				if (string.IsNullOrEmpty(_id))
					_id = Title.Content;
				// Si no existe tampoco título, crea un nuevo ID
				if (string.IsNullOrEmpty(_id))
					_id = Guid.NewGuid().ToString();
				// Devuelve el ID
				return _id;
			}
			set { _id = value; }
		}

		/// <summary>
		///		Título
		/// </summary>
		public AtomText Title { get; internal set; }

		/// <summary>
		///		Contenido
		/// </summary>
		public AtomText Content { get; internal set; }

		/// <summary>
		///		Resumen
		/// </summary>
		public AtomText Summary { get; internal set; }

		/// <summary>
		///		Fecha de emisión
		/// </summary>
		public DateTime DateIssued { get; set; }

		/// <summary>
		///		Fecha de modificación
		/// </summary>
		public DateTime DateModified { get; set; }

		/// <summary>
		///		Fecha de modificación
		/// </summary>
		public DateTime DateUpdated { get; set; }

		/// <summary>
		///		Fecha de publicación
		/// </summary>
		public DateTime DatePublished { get; set; }

		/// <summary>
		///		Vínculos
		/// </summary>
		public AtomLinksCollection Links { get; private set; }

		/// <summary>
		///		Autores
		/// </summary>
		public AtomPersonsCollection Authors { get; private set; }

		/// <summary>
		///		Contribuyentes
		/// </summary>
		public AtomPersonsCollection Contributors { get; private set; }

		/// <summary>
		///		Categorías
		/// </summary>
		public AtomCategoriesCollection Categories { get; private set; }

		/// <summary>
		///		Origen de la fuente
		/// </summary>
		public AtomSource Source { get; internal set; }

		/// <summary>
		///		Derechos de la fuente
		/// </summary>
		public AtomRights Rights { get; internal set; }
	}
}
