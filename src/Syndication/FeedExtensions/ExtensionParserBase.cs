using System;

using Bau.Libraries.LibMarkupLanguage;

namespace Bau.Libraries.LibFeeds.Syndication.FeedExtensions
{
	/// <summary>
	///		Base para las clases de interpretación de un nodo de una extensión
	/// </summary>
	internal abstract class ExtensionParserBase
	{
		internal ExtensionParserBase(string nameSpace, string prefix)
		{
			NameSpace = nameSpace;
			Prefix = prefix;
		}

		/// <summary>
		///		Función para interpretar un nodo
		/// </summary>
		internal abstract void Parse(MLNode node, FeedBase objFeed);

		/// <summary>
		///		Espacio de nombres de la extensión
		/// </summary>
		internal string NameSpace { get; set; }

		/// <summary>
		///		Prefijo de la extensión
		/// </summary>
		internal string Prefix { get; set; }
	}
}
