using System;

using Bau.Libraries.LibFeeds.Syndication.RDF.Data;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;

namespace Bau.Libraries.LibFeeds.Syndication.RDF.Transforms
{
	/// <summary>
	///		Parser para un archivo RDF
	/// </summary>
	public class RDFParser
	{
		/// <summary>
		///		Interpreta un archivo
		/// </summary>
		public RDFChannel Parse(string fileName)
		{
			return Parse(new XMLParser().Load(fileName));
		}

		/// <summary>
		///		Interpreta un texto XML
		/// </summary>
		public RDFChannel ParseText(string content)
		{
			return Parse(new XMLParser().ParseText(content));
		}

		/// <summary>
		///		Interpreta un archivo XML
		/// </summary>
		public RDFChannel Parse(MLFile fileML)
		{
			RDFChannel rdf = null;

				// Carga los datos del archivo
				if (fileML != null)
				{
					MLNode node = fileML.Nodes[RDFConstTags.cnstStrRoot];

						// Carga los datos del canal
						if (node != null && node.Name == RDFConstTags.cnstStrRoot)
						{ 
							// Crea el objeto
							rdf = new RDFChannel();
							// Lee los espacios de nombres de las extensiones
							rdf.Dictionary.LoadNameSpaces(node);
							// Lee los datos del canal
							foreach (MLNode channel in node.Nodes)
								if (channel.Name.Equals(RDFConstTags.cnstStrChannel))
									ParseChannel(channel, rdf);
							// Lee los elementos
							foreach (MLNode item in node.Nodes)
								if (item.Name.Equals(RDFConstTags.cnstStrItem))
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
					case RDFConstTags.cnstStrChannelTitle:
							rdf.Title = node.Value;
						break;
					case RDFConstTags.cnstStrChannelDescription:
							rdf.Description = node.Value;
						break;
					case RDFConstTags.cnstStrChannelLink:
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
						case RDFConstTags.cnstStrItemTitle:
								entry.Title = node.Value;
							break;
						case RDFConstTags.cnstStrItemLink:
								entry.Link = node.Value;
							break;
						case RDFConstTags.cnstStrItemDescription:
								entry.Content = node.Value;
							break;
						case RDFConstTags.cnstStrItemDate:
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
}