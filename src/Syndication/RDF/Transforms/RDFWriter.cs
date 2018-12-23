using System;

using Bau.Libraries.LibFeeds.Syndication.RDF.Data;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;

namespace Bau.Libraries.LibFeeds.Syndication.RDF.Transforms
{
	/// <summary>
	///		Clase para escritura de un objeto RDF en un archivo XML
	/// </summary>
	public class RDFWriter
	{
		/// <summary>
		///		Obtiene el XML de un canal RDF
		/// </summary>
		public string GetXML(RDFChannel rdf)
		{
			return GetFile(rdf).ToString();
		}

		/// <summary>
		///		Graba los datos de un objeto RDF en un archivo XML
		/// </summary>
		public void Save(RDFChannel rdf, string fileName)
		{
			new XMLWriter().Save(fileName, GetFile(rdf));
		}

		/// <summary>
		///		Obtiene el builder XML de un objeto RDF
		/// </summary>
		private MLFile GetFile(RDFChannel rdf)
		{
			MLFile file = new MLFile();
			MLNode node = file.Nodes.Add(RDFConstTags.cnstStrRoot);

				// Añade los atributos de la cabecera
				node.NameSpaces.AddRange(rdf.Extensions.GetNameSpaces(rdf));
				// Añade los datos del canal
				node = node.Nodes.Add(RDFConstTags.cnstStrChannel);
				// Obtiene el XML de los datos del canal
				node.Nodes.Add(RDFConstTags.cnstStrChannelTitle, rdf.Title);
				node.Nodes.Add(RDFConstTags.cnstStrChannelLink, rdf.Link);
				node.Nodes.Add(RDFConstTags.cnstStrChannelDescription, rdf.Description);
				// Obtiene el XML de las extensiones
				rdf.Extensions.AddNodesExtension(node);
				// Obtiene el XML de los elementos
				AddItems(node, rdf.Entries);
				// Devuelve los datos
				return file;
		}

		/// <summary>
		///		Añade los elementos al XML
		/// </summary>
		private void AddItems(MLNode parent, FeedEntriesBaseCollection<RDFEntry> entries)
		{
			foreach (RDFEntry entry in entries)
			{
				MLNode node = parent.Nodes.Add(RDFConstTags.cnstStrItem);

					// Datos básicos
					node.Nodes.Add(RDFConstTags.cnstStrItemTitle, entry.Title);
					node.Nodes.Add(RDFConstTags.cnstStrItemLink, entry.Link);
					node.Nodes.Add(RDFConstTags.cnstStrItemDescription, entry.Content);
					node.Nodes.Add(RDFConstTags.cnstStrItemDate, entry.DateCreated);
					// Obtiene el XML de las extensiones
					entry.Extensions.AddNodesExtension(node);
			}
		}
	}
}
