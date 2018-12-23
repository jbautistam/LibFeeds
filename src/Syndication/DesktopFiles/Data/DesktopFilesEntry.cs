using System;

namespace Bau.Libraries.LibFeeds.Syndication.DesktopFiles.Data
{
	/// <summary>
	///		Clase con los datos de un elemento de un archivo OPML
	/// </summary>
	public class DesktopFilesEntry : IComparable<DesktopFilesEntry>
	{ 
		// Variables privadas
		private string id, strLocalFileName;

		public DesktopFilesEntry()
		{ 
			// Inicializa las propiedades
			Enabled = true;
			// Inicializa las entradas
			Entries = new DesktopFilesEntriesCollection();
		}

		/// <summary>
		///		Compara dos entradas
		/// </summary>
		public int CompareTo(DesktopFilesEntry other)
		{
			if (other == null)
				return -1;
			else if (IsFolder && !other.IsFolder)
				return -1;
			else
				return Text.CompareTo(other.Text);
		}

		/// <summary>
		///		Cuenta el número de elementos no leídos de una entrada
		/// </summary>
		public int CountNotRead()
		{
			return NumberNotRead + Entries.CountNotRead();
		}

		/// <summary>
		///		ID del elemento
		/// </summary>
		public string ID
		{
			get
			{ 
				// Asigna el ID si no existía
				if (string.IsNullOrEmpty(id))
					id = Guid.NewGuid().ToString();
				// Devuelve el ID
				return id;
			}
			private set { id = value; }
		}

		/// <summary>
		///		Texto
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		///		Descripción
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		///		URL de la entrada
		/// </summary>
		public string URL { get; set; }

		/// <summary>
		///		Usuario de acceso a la URL
		/// </summary>
		public string User { get; set; }

		/// <summary>
		///		Contraseña de acceso
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		///		Nombre del archivo local
		/// </summary>
		public string LocalFileName
		{
			get
			{ 
				// Asigna el nombre de archivo local si no existía
				if (string.IsNullOrEmpty(strLocalFileName))
				{
					if (!string.IsNullOrEmpty(URL))
					{ 
						// Quita los caracteres raros de la URL
						strLocalFileName = URL.Replace(':', '_');
						strLocalFileName = strLocalFileName.Replace('\\', '_');
						strLocalFileName = strLocalFileName.Replace('/', '_');
						strLocalFileName = strLocalFileName.Replace('%', '_');
						strLocalFileName = strLocalFileName.Replace('?', '_');
						// Devuelve el nombre del archivo local
						strLocalFileName += ".xml";
					}
				}
				// Devuelve el nombre del archivo local
				return strLocalFileName;
			}
			set { strLocalFileName = value; }
		}

		/// <summary>
		///		Nombre del archivo local donde se almacenan los elementos descargados
		/// </summary>
		public string LocalFileNameDownload
		{
			get { return LocalFileName + ".download"; }
		}

		/// <summary>
		///		Nombre del archivo local donde se almacenan los elementos borrados
		/// </summary>
		public string LocalFileNameDeleted
		{
			get { return LocalFileName + ".deleted"; }
		}

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
		public bool Enabled { get; set; }

		/// <summary>
		///		Indica si es una carpeta
		/// </summary>
		public bool IsFolder
		{
			get { return string.IsNullOrEmpty(URL); }
		}

		/// <summary>
		///		Entradas
		/// </summary>
		public DesktopFilesEntriesCollection Entries { get; private set; }
	}
}
