using System;

namespace Bau.Libraries.LibFeeds.Syndication.Atom.Data
{
	/// <summary>
	///		Clase con los datos de un texto para Atom
	/// </summary>
	public class AtomText
	{
		public AtomText() : this(null, null, null, null, null) { }

		public AtomText(string strMode, string type, string content) : this(strMode, type, null, null, content)
		{
		}

		public AtomText(string strMode, string type, string language, string strXMLBase, string content)
		{
			Mode = strMode;
			Type = type;
			Language = language;
			XmlBase = strXMLBase;
			Content = content;
		}

		/// <summary>
		///		Modo (escaped, xml, ...)
		/// </summary>
		public string Mode { get; set; }

		/// <summary>
		///		Tipo (text/html ...)
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		///		Idioma (en-US ...)
		/// </summary>
		public string Language { get; set; }

		/// <summary>
		///		XML base
		/// </summary>
		public string XmlBase { get; set; }

		/// <summary>
		///		Contenido
		/// </summary>
		public string Content { get; set; }
	}
}
