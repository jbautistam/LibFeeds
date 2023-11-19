using Bau.Libraries.LibFeeds.Syndication.RDF.Data;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;

namespace Bau.Libraries.LibFeeds.Syndication.RDF.Transforms;

/// <summary>
///		Parser para un archivo RDF
/// </summary>
public class RDFParser
{
	/// <summary>
	///		Interpreta un archivo
	/// </summary>
	public RDFChannel? Parse(string fileName) => Parse(new XMLParser().Load(fileName));

	/// <summary>
	///		Interpreta un texto XML
	/// </summary>
	public RDFChannel? ParseText(string content) => Parse(new XMLParser().ParseText(content));

	/// <summary>
	///		Interpreta un archivo XML
	/// </summary>
	public RDFChannel? Parse(MLFile fileML)
	{
		RDFChannel? rdf = null;

			// Carga los datos del archivo
			if (fileML != null)
			{
				MLNode node = fileML.Nodes[RDFConstTags.Root];

					// Carga los datos del canal
					if (node != null && node.Name == RDFConstTags.Root)
					{ 
						// Crea el objeto
						rdf = new RDFChannel();
						// Lee los espacios de nombres de las extensiones
						rdf.Dictionary.LoadNameSpaces(node);
						// Lee los datos del canal
						foreach (MLNode channel in node.Nodes)
							if (channel.Name.Equals(RDFConstTags.Channel))
								ParseChannel(channel, rdf);
						// Lee los elementos
						foreach (MLNode item in node.Nodes)
							if (item.Name.Equals(RDFConstTags.Item))
								rdf.Entries.Add(ParseEntry(item, rdf));
					}
			}
			// Devuelve el objeto RDF
			return rdf;
	}

	/// <summary>
	///		Interpreta los datos del canal
	/// </summary>
	private void ParseChannel(MLNode channel, RDFChannel rdf)
	{
		foreach (MLNode node in channel.Nodes)
			switch (node.Name)
			{
				case RDFConstTags.ChannelTitle:
						rdf.Title = node.Value;
					break;
				case RDFConstTags.ChannelDescription:
						rdf.Description = node.Value;
					break;
				case RDFConstTags.ChannelLink:
						rdf.Link = node.Value;
					break;
				default:
						rdf.Extensions.Parse(node, rdf, rdf.Dictionary);
					break;
			}
	}

	/// <summary>
	///		Interpreta los nodos de un elemento
	/// </summary>
	private RDFEntry ParseEntry(MLNode objMLEntry, RDFChannel channel)
	{
		RDFEntry entry = new RDFEntry();

			// Interpreta los nodos
			foreach (MLNode node in objMLEntry.Nodes)
				switch (node.Name)
				{
					case RDFConstTags.ItemTitle:
							entry.Title = node.Value;
						break;
					case RDFConstTags.ItemLink:
							entry.Link = node.Value;
						break;
					case RDFConstTags.ItemDescription:
							entry.Content = node.Value;
						break;
					case RDFConstTags.ItemDate:
							entry.DateCreated = LibMarkupLanguage.Tools.DateTimeHelper.ParseRfc(node.Value, DateTime.Now);
						break;
					default:
							entry.Extensions.Parse(node, entry, channel.Dictionary);
						break;
				}
			// Devuelve la entrada
			return entry;
	}
}