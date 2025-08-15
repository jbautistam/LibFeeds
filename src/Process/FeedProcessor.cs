using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibHelper.Communications;
using Bau.Libraries.LibFeeds.Syndication.Atom.Data;
using Bau.Libraries.LibFeeds.Syndication.Atom.Transforms;
using Bau.Libraries.LibFeeds.Syndication.RDF.Data;
using Bau.Libraries.LibFeeds.Syndication.RDF.Transforms;
using Bau.Libraries.LibFeeds.Syndication.RSS.Data;
using Bau.Libraries.LibFeeds.Syndication.RSS.Transforms;

namespace Bau.Libraries.LibFeeds.Process;

/// <summary>
///		Procesador para descarga de archivos RSS / Atom
/// </summary>
public class FeedProcessor
{
	/// <summary>
	///		Obtiene un canal Atom a partir de los datos de un archivo, si es un archivo RSS o RDF lo convierte a Atom
	/// </summary>
	public async Task<AtomChannel?> DownloadAsync(string url)
	{
		AtomChannel? channel;

			// Descarga el archivo
			try
			{
				channel = ParseText(await new HttpWebClient().HttpGetAsync(url));
			}
			catch (Exception exception)
			{
				throw new Exceptions.FeedException(Exceptions.FeedException.ExceptionType.DownloadError, "Error en la descarga. Url: " + url, exception);
			}
			// Si ha llegado hasta aquí es porque desconoce el tipo de archivo
			return channel;
	}

	/// <summary>
	///		Carga los datos de un archivo Atom
	/// </summary>
	public AtomChannel? Load(string fileName) => Parse(new LibMarkupLanguage.Services.XML.XMLParser().Load(fileName));

	/// <summary>
	///		Interpreta los datos de un texto XML
	/// </summary>
	public AtomChannel? ParseText(string content) => Parse(new LibMarkupLanguage.Services.XML.XMLParser().ParseText(content));

	/// <summary>
	///		Interpreta los datos de un archivo XML
	/// </summary>
	public AtomChannel? Parse(MLFile fileML)
	{
		AtomChannel? channel = ParseRSS(fileML);

			// Si no se ha cargado desde un archivo RSS, se carga desde un archivo RDF
			if (channel == null)
				channel = ParseRDF(fileML);
			// Si no se ha cargado desde un archivo RSS, se carga desde un archivo Atom
			if (channel == null)
				channel = new AtomParser().Parse(fileML);
			// Devuelve los datos
			return channel;
	}

	/// <summary>
	///		Interpreta un archivo RSS
	/// </summary>
	private AtomChannel? ParseRSS(MLFile fileML)
	{ 
		// Carga el archivo RSS
		try
		{
			RSSChannel? rss = new RSSParser().Parse(fileML);

				if (rss is not null)
				{ 
					// Obtiene el contenido codificado
					ConvertEntriesRSS(rss);
					// Convierte el archivo RSS en un archivo Atom
					return new RSSToAtom().Convert(rss);
				}
		}
		catch (Exception exception)
		{
			System.Diagnostics.Debug.WriteLine(exception.Message);
		}
		// Si ha llegado hasta aquí es porque no ha podido leerlo
		return null;
	}

	/// <summary>
	///		Convierte las entradas RSS
	/// </summary>
	private void ConvertEntriesRSS(RSSChannel rss)
	{
		foreach (RSSEntry entry in rss.Entries)
			foreach (Syndication.FeedExtensions.ExtensionBase extension in entry.Extensions)
				if (extension is Syndication.FeedExtensions.RSSContent.Data.RSSContentData)
				{
					Syndication.FeedExtensions.RSSContent.Data.RSSContentData content = extension as Syndication.FeedExtensions.RSSContent.Data.RSSContentData;

						if (content != null)
							entry.Content = content.ContentEncoded;
				}
	}

	/// <summary>
	///		Interpreta un archivo RDF
	/// </summary>
	private AtomChannel? ParseRDF(MLFile fileML)
	{ 
		// Carga el archivo RDF
		try
		{
			RDFChannel rdf = new RDFParser().Parse(fileML);

				if (rdf != null)
					return new RDFToAtom().Convert(rdf);
		}
		catch { }
		// Si ha llegado hasta aquí es porque no ha podido leerlo
		return null;
	}
}
