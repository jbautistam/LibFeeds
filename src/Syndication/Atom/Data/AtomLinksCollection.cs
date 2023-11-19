namespace Bau.Libraries.LibFeeds.Syndication.Atom.Data;

/// <summary>
///		Colección de <see cref="AtomLink"/>
/// </summary>
public class AtomLinksCollection : List<AtomLink>
{
	/// <summary>
	///		Obtiene una colección con los vínculos de un tipo
	/// </summary>
	public AtomLinksCollection Search(AtomLink.AtomLinkType linkType)
	{
		AtomLinksCollection links = new();

			// Busca en la colección
			foreach (AtomLink link in this)
				if (link.LinkType == linkType)
					links.Add(link);
			// Devuelve la colección de vínculos encontrados
			return links;
	}
}
