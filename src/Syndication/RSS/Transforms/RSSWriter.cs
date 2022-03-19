using System;

using Bau.Libraries.LibFeeds.Syndication.RSS.Data;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;
using Bau.Libraries.LibMarkupLanguage.Tools;

namespace Bau.Libraries.LibFeeds.Syndication.RSS.Transforms
{
	/// <summary>
	///		Clase para escritura de un objeto RSS en un archivo XML
	/// </summary>
	public class RSSWriter
	{
		/// <summary>
		///		Obtiene el XML de un canal RSS
		/// </summary>
		public string GetXML(RSSChannel rss)
		{
			return new XMLWriter().ConvertToString(GetFile(rss));
		}

		/// <summary>
		///		Graba los datos de un objeto RSS en un archivo XML
		/// </summary>
		public void Save(RSSChannel rss, string fileName)
		{
			new XMLWriter().Save(fileName, GetFile(rss));
		}

		/// <summary>
		///		Obtiene el builder XML de un objeto RSS
		/// </summary>
		private MLFile GetFile(RSSChannel rss)
		{
			MLFile file = new MLFile();
			MLNode node = file.Nodes.Add(RSSConstTags.cnstStrRoot);

				// Añade los atributos de la cabecera
				node.NameSpaces.AddRange(rss.Extensions.GetNameSpaces(rss));
				node.Attributes.Add("version", "2.0");
				// Añade los datos del canal
				node = node.Nodes.Add(RSSConstTags.cnstStrChannel);
				// Obtiene el XML de los datos
				node.Nodes.Add(RSSConstTags.cnstStrChannelTitle, rss.Title);
				node.Nodes.Add(RSSConstTags.cnstStrChannelLink, rss.Link);
				node.Nodes.Add(RSSConstTags.cnstStrChannelLanguage, rss.Language);
				node.Nodes.Add(RSSConstTags.cnstStrChannelCopyright, rss.Copyright);
				node.Nodes.Add(RSSConstTags.cnstStrChannelDescription, rss.Description);
				node.Nodes.Add(RSSConstTags.cnstStrChannelLastBuildDate,
								  DateTimeHelper.ToStringRfc822(rss.LastBuildDate));
				// Obtiene el XML de la imagen
				AddImage(node, rss.Logo);
				// Obtiene el XML de las extensiones
				rss.Extensions.AddNodesExtension(node);
				// Obtiene el XML de los elementos
				AddItems(node, rss.Entries);
				// Devuelve los datos
				return file;
		}

		/// <summary>
		///		Añade los datos de la imagen
		/// </summary>
		private void AddImage(MLNode parent, RSSImage image)
		{
			if (!string.IsNullOrEmpty(image.Url))
			{
				MLNode node = parent.Nodes.Add(RSSConstTags.cnstStrChannelImage);

					// Atributos
					node.Attributes.Add(RSSConstTags.cnstStrChannelImageUrl, image.Url);
					node.Attributes.Add(RSSConstTags.cnstStrChannelImageTitle, image.Title);
					node.Attributes.Add(RSSConstTags.cnstStrChannelImageLink, image.Link);
			}
		}

		/// <summary>
		///		Añade los elementos al XML
		/// </summary>
		private void AddItems(MLNode parent, FeedEntriesBaseCollection<RSSEntry> entries)
		{
			foreach (RSSEntry entry in entries)
			{
				MLNode node = parent.Nodes.Add(RSSConstTags.cnstStrItem);
				MLNode objID;

					// Datos básicos
					node.Nodes.Add(RSSConstTags.cnstStrItemTitle, entry.Title);
					node.Nodes.Add(RSSConstTags.cnstStrItemLink, entry.Link);
					node.Nodes.Add(RSSConstTags.cnstStrItemDescription, entry.Content);
					AddDate(node, RSSConstTags.cnstStrItemPubDate, entry.DateCreated);
					// ID
					objID = node.Nodes.Add(RSSConstTags.cnstStrItemGuid, entry.GUID.ID);
					objID.Attributes.Add(RSSConstTags.cnstStrItemAttrPermaLink, entry.GUID.IsPermaLink);
					// Categorías
					foreach (RSSCategory category in entry.Categories)
						node.Nodes.Add(RSSConstTags.cnstStrItemCategory, category.Text);
					// Adjuntos
					AddEnclosures(node, entry.Enclosures);
					// Autores
					foreach (RSSAuthor objAuthor in entry.Authors)
						node.Nodes.Add(RSSConstTags.cnstStrItemAuthor, objAuthor.Name);
					// Obtiene el XML de las extensiones
					entry.Extensions.AddNodesExtension(node);
			}
		}

		/// <summary>
		///		Añade los adjuntos
		/// </summary>
		private void AddEnclosures(MLNode parent, RSSEnclosureCollections objColEnclosures)
		{
			foreach (RSSEnclosure objEnclosure in objColEnclosures)
			{
				MLNode node = parent.Nodes.Add(RSSConstTags.cnstStrItemEnclosure);

					// Atributos
					node.Attributes.Add(RSSConstTags.cnstStrItemAttrUrl, objEnclosure.Url);
					node.Attributes.Add(RSSConstTags.cnstStrItemAttrLength, objEnclosure.Length);
					node.Attributes.Add(RSSConstTags.cnstStrItemAttrType, objEnclosure.Type);
			}
		}

		/// <summary>
		///		Añade una fecha formateada
		/// </summary>
		private void AddDate(MLNode node, string tag, DateTime dtmValue)
		{
			node.Nodes.Add(tag, DateTimeHelper.ToStringRfc822(dtmValue));
		}
	}
}
