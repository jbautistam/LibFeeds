using Bau.Libraries.LibFeeds.Syndication.OPML.Data;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;
using Bau.Libraries.LibMarkupLanguage.Tools;

namespace Bau.Libraries.LibFeeds.Syndication.OPML.Transforms;

/// <summary>
///		Clase para escritura de archivos OPML
/// </summary>
public class OPMLWriter
{
	/// <summary>
	///		Obtiene el XML de un canal RSS
	/// </summary>
	public string? GetXML(OPMLChannel channel) => GetFile(channel).ToString();

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
		MLFile file = new();
		MLNode node = file.Nodes.Add(OPMLConstTags.Root);
		MLNode nodeHeader;

			// Añade la cabecera
			node.Attributes.Add("version", "1.1");
			// Cabecera
			nodeHeader = node.Nodes.Add(OPMLConstTags.Head);
			// Obtiene el XML de los datos
			nodeHeader.Nodes.Add(OPMLConstTags.cnsttitle, channel.Title);
			nodeHeader.Nodes.Add(OPMLConstTags.DateCreated,
								    DateTimeHelper.ToStringRfc822(channel.DateCreated));
			nodeHeader.Nodes.Add(OPMLConstTags.DateModified,
									DateTimeHelper.ToStringRfc822(channel.DateModified));
			nodeHeader.Nodes.Add(OPMLConstTags.OwnerName, channel.OwnerName);
			nodeHeader.Nodes.Add(OPMLConstTags.OwnerEMail, channel.OwnerEMail);
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
			MLNode node = parent.Nodes.Add(OPMLConstTags.Outline);

				// Añade los atributos
				if (!string.IsNullOrEmpty(entry.Type))
					node.Attributes.Add(OPMLConstTags.cnsttype, entry.Type);
				if (!string.IsNullOrEmpty(entry.Text))
					node.Attributes.Add(OPMLConstTags.cnsttext, entry.Text);
				if (!string.IsNullOrEmpty(entry.URL))
					node.Attributes.Add(OPMLConstTags.cnsturl, entry.URL);
				if (entry.DateCreated != DateTime.MinValue)
					node.Attributes.Add(OPMLConstTags.Created,
										   DateTimeHelper.ToStringRfc822(entry.DateCreated));
				// Nodos
				AddEntries(node, entry.Entries);
		}
	}
}
