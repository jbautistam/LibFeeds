namespace Bau.Libraries.LibFeeds.Syndication.RDF;

/// <summary>
///		Etiquetas de un archivo RDF
/// </summary>
internal static class RDFConstTags
{ 
	// Constantes con el nombre del nodo base
	internal const string Root = "RDF";
	// Constantes con el nombre de la raíz del canal
	internal const string Channel = "channel";
	// Constantes del canal
	internal const string ChannelTitle = "title";
	internal const string ChannelDescription = "description";
	internal const string ChannelLink = "link";
	// Constantes con el nombre de la raíz del elemento
	internal const string Item = "item";
	// Constantes con el nombre de la raíz del elemento
	internal const string ItemTitle = "title";
	internal const string ItemLink = "link";
	internal const string ItemDescription = "description";
	internal const string ItemDate = "date";
}
