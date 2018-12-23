using System;
using System.Collections.Generic;

using Bau.Libraries.LibMarkupLanguage;

namespace Bau.Libraries.LibFeeds.Syndication.FeedExtensions
{
	/// <summary>
	///		Colección de <see cref="ExtensionRootBase"/>
	/// </summary>
	public class ExtensionsCollection : List<ExtensionBase>
	{
		public ExtensionsCollection(FeedBase parent)
		{
			Parent = parent;
		}

		/// <summary>
		///		Añade una colección de extensiones
		/// </summary>
		public void Add(ExtensionsCollection objColExtensions)
		{
			foreach (ExtensionBase extension in objColExtensions)
				Add(extension);
		}

		/// <summary>
		///		Interpreta un nodo
		/// </summary>
		internal void Parse(MLNode node, FeedBase objFeed, ExtensionParsersCollection objDictionary)
		{
			if (objDictionary != null)
			{
				ExtensionParserBase parser = objDictionary.GetParser(node);

					if (parser != null)
						parser.Parse(node, objFeed);
			}
		}

		/// <summary>
		///		Añade los nodos de extensión
		/// </summary>
		internal void AddNodesExtension(MLNode node)
		{
			foreach (ExtensionBase extension in this)
				if (extension is Yahoo.Data.YahooMedia)
					new Yahoo.Transforms.YahooMediaWriter().AddNodesExtension(node, (extension as Yahoo.Data.YahooMedia));
				else if (extension is Desktop.Data.FeedDesktop)
					new Desktop.Transforms.FeedDesktopWriter().AddNodesExtension(node, (extension as Desktop.Data.FeedDesktop));
		}

		/// <summary>
		///		Busca una extensión
		/// </summary>
		internal ExtensionBase Search(string strXMLNameSpace)
		{ 
			// Recorre las extensiones buscando el espacio de nombres
			foreach (ExtensionBase extension in this)
				if (extension.NameSpace.Equals(strXMLNameSpace, StringComparison.CurrentCultureIgnoreCase))
					return extension;
			// Si ha llegado hasta aquí es porque no ha encontrado nada
			return null;
		}

		/// <summary>
		///		Obtiene los espacios de nombres
		/// </summary>
		internal MLNameSpacesCollection GetNameSpaces<TypeData>(FeedChannelBase<TypeData> channel) where TypeData : FeedEntryBase
		{
			MLNameSpacesCollection objColNameSpaces = new MLNameSpacesCollection();

				// Añade los espacios de nombres de las extensiones del canal
				foreach (ExtensionBase extension in channel.Extensions)
					objColNameSpaces.Add(extension.Prefix, extension.NameSpace);
				// Añade los espacios de nombres de las extensiones de las entradas
				foreach (TypeData data in channel.Entries)
					foreach (ExtensionBase extension in data.Extensions)
						objColNameSpaces.Add(extension.Prefix, extension.NameSpace);
				// Devuelve la colección de espacios de nombres
				return objColNameSpaces;
		}

		/// <summary>
		///		Elemento padre de las extensiones
		/// </summary>
		public FeedBase Parent { get; }
	}
}