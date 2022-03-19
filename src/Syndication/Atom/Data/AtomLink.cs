using System;

namespace Bau.Libraries.LibFeeds.Syndication.Atom.Data
{
	/// <summary>
	///		Datos de un vínculo para Atom
	/// </summary>
	public class AtomLink
	{ 
		// Enumerados públicos
		/// <summary>
		///		Tipo de vínculo
		/// </summary>
		public enum AtomLinkType
		{
			/// <summary>Desconocido. No se debería utilizar</summary>
			Unknown,
			/// <summary>La propia entrada</summary>
			Self,
			/// <summary>Una URL alternativa</summary>
			Alternate,
			/// <summary>Una URL a un recurso relacionado</summary>
			Enclosure,
			/// <summary>Un documento relacionado con la entrada</summary>
			Related,
			/// <summary>La fuente de la información proporcionada en la entrada</summary>
			Via
		}

		public AtomLink() : this(null, null, null, null) { }

		public AtomLink(string strHref, string strRel, string title, string type)
								: this(strHref, GetType(strRel), title, type) { }

		public AtomLink(string strHref, AtomLinkType intLinkType, string title, string type)
		{
			Href = strHref;
			LinkType = intLinkType;
			Title = title;
			Type = type;
		}

		/// <summary>
		///		Obtiene el tipo de vínculo a partir de la entrada
		/// </summary>
		private static AtomLinkType GetType(string strRel)
		{
			switch (strRel)
			{
				case "self":
					return AtomLinkType.Self;
				case "alternate":
					return AtomLinkType.Alternate;
				case "enclosure":
					return AtomLinkType.Enclosure;
				case "related":
					return AtomLinkType.Related;
				case "via":
					return AtomLinkType.Via;
				default:
					return AtomLinkType.Self;
			}
		}

		/// <summary>
		///		URL
		/// </summary>
		public string Href { get; set; }

		/// <summary>
		///		Tipo del vínculo
		/// </summary>
		public AtomLinkType LinkType { get; set; }

		/// <summary>
		///		Cadena con el tipo del vínculo
		/// </summary>
		public string Rel
		{
			get
			{
				switch (LinkType)
				{
					case AtomLinkType.Self:
						return "self";
					case AtomLinkType.Alternate:
						return "alternate";
					case AtomLinkType.Enclosure:
						return "enclosure";
					case AtomLinkType.Related:
						return "related";
					case AtomLinkType.Via:
						return "via";
					default:
						return null;
				}
			}
		}

		/// <summary>
		///		Título
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///		Tipo
		/// </summary>
		public string Type { get; set; }
	}
}
