using System;
using System.Collections.Generic;

using Bau.Libraries.LibMarkupLanguage;

namespace Bau.Libraries.LibFeeds.Syndication.FeedExtensions
{
	/// <summary>
	///		Colección de <see cref="ExtensionParserBase"/>
	/// </summary>
	internal class ExtensionParsersCollection : List<ExtensionParserBase>
	{
		/// <summary>
		///		Añade un elemento al diccionario
		/// </summary>
		internal void Add(string strXMLNameSpace, string prefix)
		{
			if (!Exists(strXMLNameSpace))
			{
				ExtensionParserBase parser = GetParser(prefix);

					if (parser != null)
						Add(GetParser(prefix));
			}
		}

		/// <summary>
		///		Obtiene el parser del espacio de nombres
		/// </summary>
		private ExtensionParserBase GetParser(string prefix)
		{ 
			switch (prefix)
			{
				case Yahoo.YahooMediaConstTags.cnstStrXMLDefaultPrefix:
					return new Yahoo.Transforms.YahooMediaParser(Yahoo.YahooMediaConstTags.cnstStrXMLDefaultNameSpace, prefix);
				case Desktop.FeedDesktopConstTags.cnstStrXMLPrefix:
					return new Desktop.Transforms.FeedDesktopParser(Desktop.FeedDesktopConstTags.cnstStrXMLDefaultNameSpace,
																	prefix);
				case RSSContent.RSSContentConstTags.cnstStrXMLDefaultPrefix:
					return new RSSContent.Transforms.RSSContentParser(RSSContent.RSSContentConstTags.cnstStrXMLDefaultNameSpace,
																	  prefix);
				default:
					return null;
			}
		}

		/// <summary>
		///		Obtiene el parser asociado a un nodo XML
		/// </summary>
		internal ExtensionParserBase GetParser(MLNode node)
		{
			ExtensionParserBase parser;

				// Añade el elemento de extensión a la colección (siempre, para que se añada el espacio de nombres si no existía)
				LoadNameSpaces(node);
				// Obtiene el parser
				parser = GetParser(node.Prefix);
				// Devuelve el parser
				return parser;
		}

		/// <summary>
		///		Inicializa el diccionario a partir de un nodo raíz
		/// </summary>
		internal void LoadNameSpaces(MLNode node)
		{
			foreach (MLNameSpace objNameSpace in node.NameSpaces)
				Add(objNameSpace.NameSpace, objNameSpace.Prefix);
		}


		/// <summary>
		///		Obtiene el parser asociado a un espacio de nombres
		/// </summary>
		private ExtensionParserBase Search(string strXMLNameSpace)
		{   
			// Recorre las entradas del diccionario
			foreach (ExtensionParserBase item in this)
				if (item.NameSpace.Equals(strXMLNameSpace, StringComparison.CurrentCultureIgnoreCase))
					return item;
			// Si ha llegado hasta aquí es porque no ha encontrado nada
			return null;
		}

		/// <summary>
		///		Comprueba si existe un espacio de nombre en el diccionario
		/// </summary>
		private bool Exists(string strXMLNameSpace)
		{
			return Search(strXMLNameSpace) != null;
		}
	}
}
