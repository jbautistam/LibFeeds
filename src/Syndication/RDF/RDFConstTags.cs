using System;

namespace Bau.Libraries.LibFeeds.Syndication.RDF
{
	/// <summary>
	///		Etiquetas de un archivo RDF
	/// </summary>
	internal static class RDFConstTags
	{ 
		// Constantes con el nombre del nodo base
		internal const string cnstStrRoot = "RDF";
		// Constantes con el nombre de la raíz del canal
		internal const string cnstStrChannel = "channel";
		// Constantes del canal
		internal const string cnstStrChannelTitle = "title";
		internal const string cnstStrChannelDescription = "description";
		internal const string cnstStrChannelLink = "link";
		// Constantes con el nombre de la raíz del elemento
		internal const string cnstStrItem = "item";
		// Constantes con el nombre de la raíz del elemento
		internal const string cnstStrItemTitle = "title";
		internal const string cnstStrItemLink = "link";
		internal const string cnstStrItemDescription = "description";
		internal const string cnstStrItemDate = "date";
	}
}
