using System;

using Bau.Libraries.LibFeeds.Syndication.Atom.Data;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;
using Bau.Libraries.LibMarkupLanguage.Tools;

namespace Bau.Libraries.LibFeeds.Syndication.Atom.Transforms
{
	/// <summary>
	///		Clase para escribir un archivo Atom
	/// </summary>
	public class AtomWriter
	{
		/// <summary>
		///		Obtiene el XML de un canal Atom
		/// </summary>
		public string GetXML(AtomChannel channel)
		{
			return GetFile(channel).ToString();
		}

		/// <summary>
		///		Graba los datos de un objeto Atom en un archivo XML
		/// </summary>
		public void Save(AtomChannel channel, string fileName)
		{
			new XMLWriter().Save(fileName, GetFile(channel));
		}

		/// <summary>
		///		Obtiene el builder XML de un objeto Atom
		/// </summary>
		private MLFile GetFile(AtomChannel channel)
		{
			MLFile file = new MLFile();
			MLNode node;

				// Añade la cabecera
				node = file.Nodes.Add(AtomConstTags.cnstStrChannelRoot);
				node.Attributes.Add("xmlns", "http://purl.org/atom/ns#");
				node.NameSpaces.AddRange(channel.Extensions.GetNameSpaces(channel));
				node.Attributes.Add("version", "0.3");
				// Obtiene el XML de los datos
				node.Nodes.Add(AtomConstTags.cnstStrChannelId, channel.ID);
				AddText(node, AtomConstTags.cnstStrChannelTitle, channel.Title);
				AddText(node, AtomConstTags.cnstStrChannelTagline, channel.TagLine);
				AddText(node, AtomConstTags.cnstsubTitle, channel.Subtitle);
				node.Nodes.Add(AtomConstTags.cnsticon, channel.Icon);
				node.Nodes.Add(AtomConstTags.cnstStrLogo, channel.Logo);
				AddRights(node, channel.Rights);
				AddGenerator(node, channel.Generator);
				node.Nodes.Add(AtomConstTags.cnstStrChannelConvertLineBreaks, channel.ConvertLineBreaks);
				AddText(node, AtomConstTags.cnstStrChannelInfo, channel.Info);
				AddLinks(node, channel.Links);
				AddDate(node, AtomConstTags.cnstStrItemModified, channel.LastUpdated);
				AddCategories(node, channel.Categories);
				// Obtiene el XML de las extensiones
				channel.Extensions.AddNodesExtension(node);
				// Obtiene el XML de los elementos
				AddItems(node, channel.Entries);
				// Devuelve los datos
				return file;
		}

		/// <summary>
		///		Añade al XML un nodo identificando un texto Atom
		/// </summary>
		private void AddText(MLNode parent, string tag, AtomText text)
		{
			MLNode node = parent.Nodes.Add(tag, text.Content);

				// Crea la cadena de atributos
				node.Attributes.Add(AtomConstTags.cnstStrAttrMode, text.Mode);
				node.Attributes.Add(AtomConstTags.cnstStrAttrType, text.Type);
				node.Attributes.Add(AtomConstTags.cnstStrAttrLanguage, text.Language);
				node.Attributes.Add(AtomConstTags.cnstStrAttrXMLBase, text.XmlBase);
		}

		/// <summary>
		///		Añade al XML un nodo identificando el generador del archivo Atom
		/// </summary>
		private void AddGenerator(MLNode parent, AtomGenerator generator)
		{
			MLNode node = parent.Nodes.Add(AtomConstTags.cnstStrChannelGenerator, generator.Name);

				// Crea la cadena de atributos
				node.Attributes.Add(AtomConstTags.cnstStrAttrURL, generator.URL);
				node.Attributes.Add(AtomConstTags.cnstStrAttrVersion, generator.Version);
				node.Attributes.Add(AtomConstTags.cnstStrAttrLanguage, generator.Language);
		}

		/// <summary>
		///		Añade los datos de los derechos al XML
		/// </summary>
		private void AddRights(MLNode parent, AtomRights objRights)
		{
			MLNode node = parent.Nodes.Add(AtomConstTags.cnstStrRights, objRights.Copyright);

				// Atributos
				node.Attributes.Add(AtomConstTags.cnstStrAttrType, objRights.Type);
		}

		/// <summary>
		///		Añade los elementos al XML
		/// </summary>
		private void AddItems(MLNode parent, FeedEntriesBaseCollection<AtomEntry> entries)
		{
			foreach (AtomEntry entry in entries)
			{
				MLNode node = parent.Nodes.Add(AtomConstTags.cnstStrItemRoot);

				// Datos básicos
				node.Nodes.Add(AtomConstTags.cnstStrItemID, entry.ID);
				AddText(node, AtomConstTags.cnstStrItemTitle, entry.Title);
				AddText(node, AtomConstTags.cnstStrItemContent, entry.Content);
				AddText(node, AtomConstTags.cnstStrSummary, entry.Summary);
				// Fechas
				AddDate(node, AtomConstTags.cnstStrItemIssued, entry.DateIssued);
				AddDate(node, AtomConstTags.cnstStrItemModified, entry.DateModified);
				AddDate(node, AtomConstTags.cnstStrItemCreated, entry.DateCreated);
				AddDate(node, AtomConstTags.cnstStrItemUpdated, entry.DateUpdated);
				AddDate(node, AtomConstTags.cnstStrItemPublished, entry.DatePublished);
				// Vínculos
				AddLinks(node, entry.Links);
				// Autores
				AddPeople(node, AtomConstTags.cnstStrItemAuthor, entry.Authors);
				AddPeople(node, AtomConstTags.cnstStrItemContributor, entry.Contributors);
				// Categorías
				AddCategories(node, entry.Categories);
				// Origen
				AddSource(node, entry.Source);
				// Derechos
				AddRights(node, entry.Rights);
				// Extensiones
				entry.Extensions.AddNodesExtension(node);
			}
		}

		/// <summary>
		///		Añade una fecha formateada
		/// </summary>
		private void AddDate(MLNode node, string tag, DateTime dtmValue)
		{
			node.Nodes.Add(tag, DateTimeHelper.ToStringRfc822(dtmValue));
		}

		/// <summary>
		///		Añade los adjuntos
		/// </summary>
		private void AddLinks(MLNode parent, AtomLinksCollection objColLinks)
		{
			foreach (AtomLink link in objColLinks)
			{
				MLNode node = parent.Nodes.Add(AtomConstTags.cnstStrItemLink);

					// Añade los atributos
					if (!string.IsNullOrEmpty(link.Href))
						node.Attributes.Add(AtomConstTags.cnstStrAttrHref, link.Href);
					if (!string.IsNullOrEmpty(link.Rel))
						node.Attributes.Add(AtomConstTags.cnstStrAttrRel, link.Rel);
					if (!string.IsNullOrEmpty(link.Type))
						node.Attributes.Add(AtomConstTags.cnstStrAttrType, link.Type);
					if (!string.IsNullOrEmpty(link.Title))
						node.Attributes.Add(AtomConstTags.cnstStrAttrTitle, link.Title);
			}
		}

		/// <summary>
		///		Añade los nodos de las categorías
		/// </summary>
		private void AddCategories(MLNode parent, AtomCategoriesCollection objColCategories)
		{
			foreach (AtomCategory category in objColCategories)
			{
				MLNode node = parent.Nodes.Add(AtomConstTags.cnstStrChannelCategory);

					// Atributos
					node.Attributes.Add(AtomConstTags.cnstStrAttrTerm, category.Name);
			}
		}

		/// <summary>
		///		Añade los nodos de autores
		/// </summary>
		private void AddPeople(MLNode parent, string tag, AtomPersonsCollection objColPersons)
		{
			foreach (AtomPeople objPeople in objColPersons)
			{
				MLNode node = parent.Nodes.Add(tag);

					// Cuerpo
					node.Nodes.Add(AtomConstTags.cnstStrItemPeopleName, objPeople.Name);
					node.Nodes.Add(AtomConstTags.cnstStrItemPeopleUrl, objPeople.URL);
					node.Nodes.Add(AtomConstTags.cnstStrItemPeopleEMail, objPeople.EMail);
			}
		}

		/// <summary>
		///		Añade los datos del origen
		/// </summary>
		private void AddSource(MLNode parent, AtomSource source)
		{
			MLNode node = parent.Nodes.Add(AtomConstTags.cnstsource);

				// Cuerpo
				node.Nodes.Add(AtomConstTags.cnstStrChannelId, source.ID);
				node.Nodes.Add(AtomConstTags.cnstStrChannelTitle, source.Title);
				AddDate(node, AtomConstTags.cnstStrItemUpdated, source.DateUpdated);
				node.Nodes.Add(AtomConstTags.cnstStrRights, source.Copyright);
		}
	}
}