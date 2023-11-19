using Bau.Libraries.LibFeeds.Syndication.DesktopFiles.Data;
using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;

namespace Bau.Libraries.LibFeeds.Syndication.DesktopFiles.Transforms;

/// <summary>
///		Intérprete de un archivo Desktop
/// </summary>
public class DesktopFilesParser
{
	/// <summary>
	///		Interpreta un archivo
	/// </summary>
	public DesktopFilesChannel? Parse(string fileName)
	{
		DesktopFilesChannel? channel = null;
		MLFile fileML = new XMLParser().Load(fileName);

			// Lee los datos
			if (fileML != null)
				foreach (MLNode node in fileML.Nodes)
					if (node.Name == DesktopFilesConstTags.Root)
					{ 
						// Crea el objeto
						channel = new DesktopFilesChannel();
						// Carga los datos
						foreach (MLNode objMLDesktop in node.Nodes)
							switch (objMLDesktop.Name)
							{
								case DesktopFilesConstTags.cnsttitle:
										channel.Title = objMLDesktop.Value;
									break;
								case DesktopFilesConstTags.DateCreated:
										channel.DateCreated = objMLDesktop.Value.GetDateTime(DateTime.Now);
									break;
								case DesktopFilesConstTags.DateModified:
										channel.DateModified = objMLDesktop.Value.GetDateTime(DateTime.Now);
									break;
								case DesktopFilesConstTags.Outline:
										channel.Entries.Add(ParseEntry(objMLDesktop));
									break;
							}
					}
			// Devuelve los datos del canal
			return channel;
	}

	/// <summary>
	///		Interpreta una entrada a partir de un nodo XML
	/// </summary>
	private DesktopFilesEntry ParseEntry(MLNode node)
	{
		DesktopFilesEntry entry = new DesktopFilesEntry();

			// Lee los atributos
			entry.Text = node.Attributes[DesktopFilesConstTags.cnsttext].Value;
			entry.LocalFileName = node.Attributes[DesktopFilesConstTags.cnstfileName].Value;
			entry.NumberNotRead = node.Attributes[DesktopFilesConstTags.NotRead].Value.GetInt(0);
			entry.Enabled = node.Attributes[DesktopFilesConstTags.Enabled].Value.GetBool(true);
			entry.URL = node.Attributes[DesktopFilesConstTags.cnsturl].Value;
			entry.User = node.Attributes[DesktopFilesConstTags.cnstuser].Value;
			entry.Password = node.Attributes[DesktopFilesConstTags.cnstpassword].Value;
			entry.DateCreated = node.Attributes[DesktopFilesConstTags.Created].Value.GetDateTime(DateTime.Now);
			entry.DateLastRead = node.Attributes[DesktopFilesConstTags.LastRead].Value.GetDateTime(DateTime.MinValue);
			entry.DateLastUpdated = node.Attributes[DesktopFilesConstTags.LastUpdate].Value.GetDateTime(DateTime.Now);
			// Lee las entradas
			foreach (MLNode child in node.Nodes)
				if (child.Name == DesktopFilesConstTags.Outline)
					entry.Entries.Add(ParseEntry(child));
			// Devuelve la entrada
			return entry;
	}
}
