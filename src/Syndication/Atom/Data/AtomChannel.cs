using System;

namespace Bau.Libraries.LibFeeds.Syndication.Atom.Data
{
	/// <summary>
	///		Clase con los datos de un canal Atom
	/// </summary>
	public class AtomChannel : FeedChannelBase<AtomEntry>
	{ 
		// Variables privadas
		private string _id;

		public AtomChannel()
		{
			Title = new AtomText();
			TagLine = new AtomText();
			Generator = new AtomGenerator();
			Info = new AtomText();
			Subtitle = new AtomText();
			Rights = new AtomRights();
			Links = new AtomLinksCollection();
			Categories = new AtomCategoriesCollection();
		}

		/// <summary>
		///		ID
		/// </summary>
		public string ID
		{
			get
			{ 
				// Asigna el ID si no existía
				if (string.IsNullOrEmpty(_id))
					_id = Guid.NewGuid().ToString();
				// Devuelve el ID
				return _id;
			}
			set { _id = value; }
		}

		/// <summary>
		///		Título del canal
		/// </summary>
		public AtomText Title { get; internal set; }

		/// <summary>
		///		Comentarios
		/// </summary>
		public AtomText TagLine { get; internal set; }

		/// <summary>
		///		Generador
		/// </summary>
		public AtomGenerator Generator { get; internal set; }

		/// <summary>
		///		Indica si se deben convertir las rupturas de línea
		/// </summary>
		public bool ConvertLineBreaks { get; set; }

		/// <summary>
		///		Información
		/// </summary>
		public AtomText Info { get; internal set; }

		/// <summary>
		///		Subtítulo
		/// </summary>
		public AtomText Subtitle { get; internal set; }

		/// <summary>
		///		Vínculos
		/// </summary>
		public AtomLinksCollection Links { get; private set; }

		/// <summary>
		///		Ultima modificación
		/// </summary>
		public DateTime LastUpdated { get; set; }

		/// <summary>
		///		Archivo de imagen para el icon
		/// </summary>
		public string Icon { get; set; }

		/// <summary>
		///		Archivo de imagen para el logo
		/// </summary>
		public string Logo { get; set; }

		/// <summary>
		///		Derechos
		/// </summary>
		public AtomRights Rights { get; internal set; }

		/// <summary>
		///		Categorías
		/// </summary>
		public AtomCategoriesCollection Categories { get; private set; }
	}
}
