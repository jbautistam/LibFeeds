using System;

using Bau.Libraries.LibFeeds.Syndication.Atom.Data;
using Bau.Libraries.LibFeeds.Syndication.RDF.Data;

namespace Bau.Libraries.LibFeeds.Syndication.RDF.Transforms
{
	/// <summary>
	///		Clase para conversión de archivos RDF a Atom
	/// </summary>
	public class RDFToAtom
	{
		/// <summary>
		///		Convierte un archivo RDF en un archivo Atom
		/// </summary>
		public AtomChannel Convert(RDFChannel rdf)
		{
			AtomChannel channel = new AtomChannel();

				// Convierte los datos del canal
				channel.ID = new Guid().ToString();
				channel.Title = ConvertText(rdf.Title);
				channel.Info = ConvertText(rdf.Description);
				channel.Subtitle = ConvertText("");
				channel.Links.Add(ConvertLink(rdf.Link, AtomLink.AtomLinkType.Self));
				// Añade las extensiones
				ConvertExtension(rdf.Extensions, channel.Extensions);
				// Añade las entradas
				ConvertEntries(rdf, channel);
				// Devuelve el objeto Atom
				return channel;
		}

		/// <summary>
		///		Convierte un texto a Atom
		/// </summary>
		private AtomText ConvertText(string text)
		{
			return new AtomText("escaped", "text/html", text);
		}

		/// <summary>
		///		Convierte un generador a Atom
		/// </summary>
		private AtomGenerator ConvertGenerator(string strGenerator)
		{
			return new AtomGenerator(null, null, strGenerator);
		}

		/// <summary>
		///		Convierte las entradas RDF en entradas Atom
		/// </summary>
		private void ConvertEntries(RDFChannel rdf, AtomChannel channel)
		{
			foreach (RDFEntry rdfEntry in rdf.Entries)
			{
				AtomEntry channelEntry = new AtomEntry();

					// Convierte los datos de la entrada
					channelEntry.ID = rdfEntry.ID;
					channelEntry.Title = ConvertText(rdfEntry.Title);
					channelEntry.Content = ConvertText(rdfEntry.Content);
					channelEntry.DateIssued = rdfEntry.DateCreated;
					channelEntry.DateCreated = rdfEntry.DateCreated;
					channelEntry.DateModified = rdfEntry.DateCreated;
					channelEntry.DateUpdated = rdfEntry.DateCreated;
					channelEntry.DatePublished = rdfEntry.DateCreated;
					// Vínculos
					channelEntry.Links.Add(ConvertLink(rdfEntry.Link, AtomLink.AtomLinkType.Self));
					// Convierte las extensiones
					ConvertExtension(rdfEntry.Extensions, channelEntry.Extensions);
					// Añade la entrada al objeto Atom
					channel.Entries.Add(channelEntry);
			}
		}

		/// <summary>
		///		Convierte un vínculo a Atom
		/// </summary>
		private AtomLink ConvertLink(string url, AtomLink.AtomLinkType intLinkType)
		{
			return new AtomLink(url, intLinkType, null, null);
		}

		/// <summary>
		///		Convierte las extensiones
		/// </summary>
		private void ConvertExtension(FeedExtensions.ExtensionsCollection rdfExtensions,
									  FeedExtensions.ExtensionsCollection channelExtensions)
		{
			foreach (FeedExtensions.ExtensionBase extension in rdfExtensions)
				channelExtensions.Add(extension);
		}
	}
}
