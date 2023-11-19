using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibFeeds.Syndication.OPML.Data;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;

namespace Bau.Libraries.LibFeeds.Syndication.OPML.Transforms;

/// <summary>
///		Intérprete de un archivo OPML
/// </summary>
public class OPMLParser
{
	/// <summary>
	///		Interpreta un archivo
	/// </summary>
	public OPMLChannel? Parse(string fileName)
	{
		OPMLChannel? channel = null;
		MLFile fileML = new XMLParser().Load(fileName);

			// Lee los datos
			if (fileML != null)
				foreach (MLNode node in fileML.Nodes)
					if (node.Name == OPMLConstTags.Root)
					{ 
						// Crea el objeto
						channel = new OPMLChannel();
						// Carga los datos
						foreach (MLNode channelML in node.Nodes)
							switch (channelML.Name)
							{
								case OPMLConstTags.Head:
										ParseHead(channelML.Nodes, channel);
									break;
								case OPMLConstTags.cnstbody:
										ParseEntries(channelML.Nodes, channel.Entries);
									break;
							}
					}
			// Devuelve los datos del canal
			return channel;
	}

	/// <summary>
	///		Interpreta las cabeceras de un archivo OPML
	/// </summary>
	private void ParseHead(MLNodesCollection nodes, OPMLChannel channel)
	{
		foreach (MLNode node in nodes)
			switch (node.Name)
			{
				case OPMLConstTags.cnsttitle:
						channel.Title = node.Value;
					break;
				case OPMLConstTags.DateCreated:
						channel.DateCreated = node.Value.GetDateTime(DateTime.Now);
					break;
				case OPMLConstTags.DateModified:
						channel.DateModified = node.Value.GetDateTime(DateTime.Now);
					break;
				case OPMLConstTags.OwnerName:
						channel.OwnerName = node.Value;
					break;
				case OPMLConstTags.OwnerEMail:
						channel.OwnerEMail = node.Value;
					break;
			}
	}

	/// <summary>
	///		Interpreta las entradas de un archivo XML
	/// </summary>
	private void ParseEntries(MLNodesCollection nodes, OPMLEntriesCollection entries)
	{
		foreach (MLNode node in nodes)
			switch (node.Name)
			{
				case OPMLConstTags.Outline:
						entries.Add(ParseEntry(node));
					break;
			}
	}

	/// <summary>
	///		Interpreta una entrada a partir de un nodo XML
	/// </summary>
	private OPMLEntry ParseEntry(MLNode node)
	{
		OPMLEntry entry = new OPMLEntry();

			// Lee los atributos
			entry.Type = node.Attributes[OPMLConstTags.cnsttype].Value;
			entry.Title = node.Attributes[OPMLConstTags.cnsttitleEntry].Value;
			entry.Text = node.Attributes[OPMLConstTags.cnsttext].Value;
			entry.URL = node.Attributes[OPMLConstTags.cnsturl].Value;
			if (string.IsNullOrEmpty(entry.URL))
				entry.URL = node.Attributes[OPMLConstTags.XMLUrl].Value;
			entry.DateCreated = node.Attributes[OPMLConstTags.Created].Value.GetDateTime(DateTime.Now);
			// Lee las entradas
			ParseEntries(node.Nodes, entry.Entries);
			// Devuelve la entrada
			return entry;
	}
}
