using System;

using Bau.Libraries.LibFeeds.Syndication.DesktopFiles.Data;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;
using Bau.Libraries.LibMarkupLanguage.Tools;

namespace Bau.Libraries.LibFeeds.Syndication.DesktopFiles.Transforms
{
	/// <summary>
	///		Clase para escritura de archivos OPML
	/// </summary>
	public class DesktopFilesWriter
	{
		/// <summary>
		///		Obtiene el XML de un canal DesktopFiles
		/// </summary>
		public string GetXML(DesktopFilesChannel channel)
		{
			return GetFile(channel).ToString();
		}

		/// <summary>
		///		Graba los datos de un objeto OPML en un archivo XML
		/// </summary>
		public void Save(DesktopFilesChannel channel, string fileName)
		{
			new XMLWriter().Save(fileName, GetFile(channel));
		}

		/// <summary>
		///		Obtiene el builder XML de un objeto Atom
		/// </summary>
		private MLFile GetFile(DesktopFilesChannel channel)
		{
			MLFile file = new MLFile();
			MLNode node = file.Nodes.Add(DesktopFilesConstTags.cnstStrRoot);

				// Añade los atributos de la cabecera
				node.Attributes.Add("version", "1.0");
				// Obtiene el XML de los datos
				node.Nodes.Add(DesktopFilesConstTags.cnsttitle, channel.Title);
				node.Nodes.Add(DesktopFilesConstTags.cnstStrDateCreated,
													DateTimeHelper.ToStringRfc822(channel.DateCreated));
				node.Nodes.Add(DesktopFilesConstTags.cnstStrDateModified,
													DateTimeHelper.ToStringRfc822(channel.DateModified));
				// Obtiene el XML de los elementos
				AddEntries(node, channel.Entries);
				// Devuelve los datos
				return file;
		}

		/// <summary>
		///		Añade los elementos al XML
		/// </summary>
		private void AddEntries(MLNode parent, DesktopFilesEntriesCollection entries)
		{
			foreach (DesktopFilesEntry entry in entries)
			{
				MLNode node = parent.Nodes.Add(DesktopFilesConstTags.cnstStrOutline);

					// Atributos
					if (!string.IsNullOrEmpty(entry.Text))
						node.Attributes.Add(DesktopFilesConstTags.cnsttext, entry.Text);
					if (!string.IsNullOrEmpty(entry.LocalFileName))
						node.Attributes.Add(DesktopFilesConstTags.cnstfileName, entry.LocalFileName);
					if (entry.NumberNotRead > 0)
						node.Attributes.Add(DesktopFilesConstTags.cnstStrNotRead, entry.NumberNotRead);
					if (!entry.Enabled)
						node.Attributes.Add(DesktopFilesConstTags.cnstStrEnabled, entry.Enabled);
					if (!string.IsNullOrEmpty(entry.URL))
						node.Attributes.Add(DesktopFilesConstTags.cnsturl, entry.URL);
					if (!string.IsNullOrEmpty(entry.User))
						node.Attributes.Add(DesktopFilesConstTags.cnstuser, entry.User);
					if (!string.IsNullOrEmpty(entry.Password))
						node.Attributes.Add(DesktopFilesConstTags.cnstpassword, entry.Password);
					if (entry.DateCreated != DateTime.MinValue)
						node.Attributes.Add(DesktopFilesConstTags.cnstStrCreated,
											   DateTimeHelper.ToStringRfc822(entry.DateCreated));
					if (entry.DateLastRead != DateTime.MinValue)
						node.Attributes.Add(DesktopFilesConstTags.cnstStrLastRead,
											   DateTimeHelper.ToStringRfc822(entry.DateLastRead));
					if (entry.DateLastUpdated != DateTime.MinValue)
						node.Attributes.Add(DesktopFilesConstTags.cnstStrLastUpdate,
											   DateTimeHelper.ToStringRfc822(entry.DateLastUpdated));
					// Nodos
					AddEntries(node, entry.Entries);
			}
		}
	}
}
