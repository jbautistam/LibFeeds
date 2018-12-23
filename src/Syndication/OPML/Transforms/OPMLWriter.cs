using System;

using Bau.Libraries.LibFeeds.Syndication.OPML.Data;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;
using Bau.Libraries.LibMarkupLanguage.Tools;

namespace Bau.Libraries.LibFeeds.Syndication.OPML.Transforms
{
	/// <summary>
	///		Clase para escritura de archivos OPML
	/// </summary>
	public class OPMLWriter
	{
		/// <summary>
		///		Obtiene el XML de un canal RSS
		/// </summary>
		public string GetXML(OPMLChannel channel)
		{
			return GetFile(channel).ToString();
		}

		/// <summary>
		///		Graba los datos de un objeto OPML en un archivo XML
		/// </summary>
		public void Save(OPMLChannel channel, string fileName)
		{
			new XMLWriter().Save(fileName, GetFile(channel));
		}

		/// <summary>
		///		Obtiene el builder XML de un objeto Atom
		/// </summary>
		private MLFile GetFile(OPMLChannel channel)
		{
			MLFile file = new MLFile();
			MLNode node = file.Nodes.Add(OPMLConstTags.cnstStrRoot);
			MLNode nodeHeader;

				// Añade la cabecera
				node.Attributes.Add("version", "1.1");
				// Cabecera
				nodeHeader = node.Nodes.Add(OPMLConstTags.cnstStrHead);
				// Obtiene el XML de los datos
				nodeHeader.Nodes.Add(OPMLConstTags.cnsttitle, channel.Title);
				nodeHeader.Nodes.Add(OPMLConstTags.cnstStrDateCreated,
									    DateTimeHelper.ToStringRfc822(channel.DateCreated));
				nodeHeader.Nodes.Add(OPMLConstTags.cnstStrDateModified,
										DateTimeHelper.ToStringRfc822(channel.DateModified));
				nodeHeader.Nodes.Add(OPMLConstTags.cnstStrOwnerName, channel.OwnerName);
				nodeHeader.Nodes.Add(OPMLConstTags.cnstStrOwnerEMail, channel.OwnerEMail);
				// Obtiene el XML de los elementos
				AddEntries(node.Nodes.Add(OPMLConstTags.cnstbody), channel.Entries);
				// Devuelve los datos
				return file;
		}

		/// <summary>
		///		Añade los elementos al XML
		/// </summary>
		private void AddEntries(MLNode parent, OPMLEntriesCollection entries)
		{
			foreach (OPMLEntry entry in entries)
			{
				MLNode node = parent.Nodes.Add(OPMLConstTags.cnstStrOutline);

					// Añade los atributos
					if (!string.IsNullOrEmpty(entry.Type))
						node.Attributes.Add(OPMLConstTags.cnsttype, entry.Type);
					if (!string.IsNullOrEmpty(entry.Text))
						node.Attributes.Add(OPMLConstTags.cnsttext, entry.Text);
					if (!string.IsNullOrEmpty(entry.URL))
						node.Attributes.Add(OPMLConstTags.cnsturl, entry.URL);
					if (entry.DateCreated != DateTime.MinValue)
						node.Attributes.Add(OPMLConstTags.cnstStrCreated,
											   DateTimeHelper.ToStringRfc822(entry.DateCreated));
					// Nodos
					AddEntries(node, entry.Entries);
			}
		}
	}
}
