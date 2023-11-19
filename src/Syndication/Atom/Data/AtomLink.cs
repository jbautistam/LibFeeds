namespace Bau.Libraries.LibFeeds.Syndication.Atom.Data;

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

	/// <summary>
	///		URL
	/// </summary>
	public string Href { get; set; } = default!;

	/// <summary>
	///		Tipo del vínculo
	/// </summary>
	public AtomLinkType LinkType { get; set; } = AtomLinkType.Unknown;

	/// <summary>
	///		Cadena con el tipo del vínculo
	/// </summary>
	public string Rel
	{
		get { return LinkType.ToString().ToLower(); }
		set
		{
			switch (value)
			{
				case "self":
						LinkType = AtomLinkType.Self;
					break;
				case "alternate":
						LinkType = AtomLinkType.Alternate;
					break;
				case "enclosure":
						LinkType = AtomLinkType.Enclosure;
					break;
				case "related":
						LinkType = AtomLinkType.Related;
					break;
				case "via":
						LinkType = AtomLinkType.Via;
					break;
				default:
						LinkType = AtomLinkType.Unknown;
					break;
			}

		}
	}

	/// <summary>
	///		Título
	/// </summary>
	public string Title { get; set; } = default!;

	/// <summary>
	///		Tipo
	/// </summary>
	public string Type { get; set; } = default!;
}
