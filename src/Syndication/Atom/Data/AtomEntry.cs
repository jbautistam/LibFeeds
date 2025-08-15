namespace Bau.Libraries.LibFeeds.Syndication.Atom.Data;

/// <summary>
///		Datos de una entrada Atom
/// </summary>
public class AtomEntry : FeedEntryBase
{ 
	// Variables privadas
	private string _id = string.Empty;

	/// <summary>
	///		Obtiene la primera fecha de la entrada
	/// </summary>
	public DateTime GetFirstDate()
	{
		DateTime date = DateTime.UtcNow.AddYears(-10);

			// Obtiene la fecha mínima
			//date = GetMin(date, DateCreated);
			//date = GetMin(date, DateIssued);
			//date = GetMin(date, DateModified);
			//date = GetMin(date, DateUpdated);
			date = GetMin(date, DatePublished);
			// Devuelve la fecha localizada
			return date;

		// Obtiene la fecha mínima
		DateTime GetMin(DateTime first, DateTime second)
		{
			if (first < second)
				return first;
			else
				return second;
		}
	}

	/// <summary>
	///		ID
	/// </summary>
	public override string ID
	{
		get
		{ 
			// Si no existía un ID le asigna el primer vínculo
			if (string.IsNullOrEmpty(_id))
			{
				AtomLinksCollection links = Links.Search(AtomLink.AtomLinkType.Self);

					if (links.Count > 0)
						_id = links[0].Href;
			}
			// Si no existe tampoco el primer vínculo le asigna el título
			if (string.IsNullOrEmpty(_id))
				_id = Title.Content;
			// Si no existe tampoco título, crea un nuevo ID
			if (string.IsNullOrEmpty(_id))
				_id = Guid.NewGuid().ToString();
			// Devuelve el ID
			return _id;
		}
		set { _id = value; }
	}

	/// <summary>
	///		Título
	/// </summary>
	public AtomText Title { get; internal set; } = new();

	/// <summary>
	///		Contenido
	/// </summary>
	public AtomText Content { get; internal set; } = new();

	/// <summary>
	///		Resumen
	/// </summary>
	public AtomText Summary { get; internal set; } = new();

	/// <summary>
	///		Fecha de emisión
	/// </summary>
	public DateTime DateIssued { get; set; } = DateTime.UtcNow;

	/// <summary>
	///		Fecha de modificación
	/// </summary>
	public DateTime DateModified { get; set; } = DateTime.UtcNow;

	/// <summary>
	///		Fecha de modificación
	/// </summary>
	public DateTime DateUpdated { get; set; } = DateTime.UtcNow;

	/// <summary>
	///		Fecha de publicación
	/// </summary>
	public DateTime DatePublished { get; set; } = DateTime.UtcNow;

	/// <summary>
	///		Vínculos
	/// </summary>
	public AtomLinksCollection Links { get; } = [];

	/// <summary>
	///		Autores
	/// </summary>
	public AtomPersonsCollection Authors { get; } = [];

	/// <summary>
	///		Contribuyentes
	/// </summary>
	public AtomPersonsCollection Contributors { get; } = [];

	/// <summary>
	///		Categorías
	/// </summary>
	public AtomCategoriesCollection Categories { get; } = [];

	/// <summary>
	///		Origen de la fuente
	/// </summary>
	public AtomSource Source { get; internal set; } = new();

	/// <summary>
	///		Derechos de la fuente
	/// </summary>
	public AtomRights Rights { get; internal set; } = new();
}
