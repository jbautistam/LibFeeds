namespace Bau.Libraries.LibFeeds.Syndication.DesktopFiles.Data;

/// <summary>
///		Clase con los datos de un elemento de un archivo OPML
/// </summary>
public class DesktopFilesEntry : IComparable<DesktopFilesEntry>
{ 
	// Variables privadas
	private string localFileName = string.Empty;

	/// <summary>
	///		Compara dos entradas
	/// </summary>
	public int CompareTo(DesktopFilesEntry? other)
	{
		if (other is null)
			return -1;
		else if (IsFolder && !other.IsFolder)
			return -1;
		else
			return Text.CompareTo(other.Text);
	}

	/// <summary>
	///		Cuenta el número de elementos no leídos de una entrada
	/// </summary>
	public int CountNotRead() => NumberNotRead + Entries.CountNotRead();

	/// <summary>
	///		ID del elemento
	/// </summary>
	public string ID { get; set; } = Guid.NewGuid().ToString();

	/// <summary>
	///		Texto
	/// </summary>
	public string? Text { get; set; }

	/// <summary>
	///		Descripción
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	///		URL de la entrada
	/// </summary>
	public string? URL { get; set; }

	/// <summary>
	///		Usuario de acceso a la URL
	/// </summary>
	public string? User { get; set; }

	/// <summary>
	///		Contraseña de acceso
	/// </summary>
	public string? Password { get; set; }

	/// <summary>
	///		Nombre del archivo local
	/// </summary>
	public string LocalFileName
	{
		get
		{ 
			// Asigna el nombre de archivo local si no existía
			if (string.IsNullOrEmpty(localFileName))
			{
				if (!string.IsNullOrEmpty(URL))
				{ 
					// Quita los caracteres raros de la URL
					localFileName = URL.Replace(':', '_');
					localFileName = localFileName.Replace('\\', '_');
					localFileName = localFileName.Replace('/', '_');
					localFileName = localFileName.Replace('%', '_');
					localFileName = localFileName.Replace('?', '_');
					// Devuelve el nombre del archivo local
					localFileName += ".xml";
				}
			}
			// Devuelve el nombre del archivo local
			return localFileName;
		}
		set { localFileName = value; }
	}

	/// <summary>
	///		Nombre del archivo local donde se almacenan los elementos descargados
	/// </summary>
	public string LocalFileNameDownload => LocalFileName + ".download";

	/// <summary>
	///		Nombre del archivo local donde se almacenan los elementos borrados
	/// </summary>
	public string LocalFileNameDeleted => LocalFileName + ".deleted";

	/// <summary>
	///		Fecha de creación
	/// </summary>
	public DateTime DateCreated { get; set; }

	/// <summary>
	///		Fecha de última lectura
	/// </summary>
	public DateTime DateLastRead { get; set; }

	/// <summary>
	///		Fecha de última modificación
	/// </summary>
	public DateTime DateLastUpdated { get; set; }

	/// <summary>
	///		Número de elementos no leídos
	/// </summary>
	public int NumberNotRead { get; set; }

	/// <summary>
	///		Indica si el archivo está activo
	/// </summary>
	public bool Enabled { get; set; } = true;

	/// <summary>
	///		Indica si es una carpeta
	/// </summary>
	public bool IsFolder => string.IsNullOrEmpty(URL);

	/// <summary>
	///		Entradas
	/// </summary>
	public DesktopFilesEntriesCollection Entries { get; } = new();
}
