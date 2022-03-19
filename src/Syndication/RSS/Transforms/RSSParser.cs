using System;

using Bau.Libraries.LibHelper.Extensors;
using Bau.Libraries.LibFeeds.Syndication.RSS.Data;
using Bau.Libraries.LibMarkupLanguage;
using Bau.Libraries.LibMarkupLanguage.Services.XML;

namespace Bau.Libraries.LibFeeds.Syndication.RSS.Transforms
{
	/// <summary>
	///		Parser para un archivo RSS
	/// </summary>
	public class RSSParser
	{
		/// <summary>
		///		Interpreta un archivo
		/// </summary>
		public RSSChannel Parse(string fileName)
		{
			return Parse(new XMLParser().Load(fileName));
		}
		/// <summary>
		///		Interpreta un texto
		/// </summary>
		public RSSChannel ParseText(string text)
		{
			return Parse(new XMLParser().ParseText(text));
		}

		/// <summary>
		///		Interpreta los datos de un archivo XML
		/// </summary>
		public RSSChannel Parse(MLFile fileML)
		{
			RSSChannel rss = null;

				// Recorre los nodos del documento
				if (fileML != null)
					foreach (MLNode node in fileML.Nodes)
						if (node.Name.Equals(RSSConstTags.cnstStrRoot))
						{ 
							// Crea el objeto
							rss = new RSSChannel();
							// Lee los espacios de nombres de las extensiones
							rss.Dictionary.LoadNameSpaces(node);
							// Lee los datos
							foreach (MLNode channel in node.Nodes)
								if (channel.Name.Equals(RSSConstTags.cnstStrChannel))
									ParseChannel(channel, rss);
						}
				// Devuelve el objeto RSS
				return rss;
		}

		/// <summary>
		///		Interpreta los datos del canal
		/// </summary>
		private void ParseChannel(MLNode channel, RSSChannel rss)
		{
			foreach (MLNode node in channel.Nodes)
				switch (node.Name)
				{
					case RSSConstTags.cnstStrChannelTitle:
							rss.Title = node.Value;
						break;
					case RSSConstTags.cnstStrChannelDescription:
							rss.Description = node.Value;
						break;
					case RSSConstTags.cnstStrChannelLink:
							rss.Link = node.Value;
						break;
					case RSSConstTags.cnstStrChannelLanguage:
							rss.Language = node.Value;
						break;
					case RSSConstTags.cnstStrChannelCopyright:
							rss.Copyright = node.Value;
						break;
					case RSSConstTags.cnstStrChannelPubDate:
							rss.PubDate = node.Value.GetDateTime(DateTime.Now);
						break;
					case RSSConstTags.cnstStrChannelLastBuildDate:
							rss.LastBuildDate = node.Value.GetDateTime(DateTime.Now);
						break;
					case RSSConstTags.cnstStrChannelManagingEditor:
							rss.Editor = node.Value;
						break;
					case RSSConstTags.cnstStrChannelManagingWebMaster:
							rss.WebMaster = node.Value;
						break;
					case RSSConstTags.cnstStrChannelGenerator:
							rss.Generator = node.Value;
						break;
					case RSSConstTags.cnstStrChannelImage:
							rss.Logo = ParseImage(node);
						break;
					case RSSConstTags.cnstStrItem:
							rss.Entries.Add(ParseEntry(node, rss));
						break;
					default:
							rss.Extensions.Parse(node, rss, rss.Dictionary);
						break;
				}
		}

		/// <summary>
		///		Interpreta los nodos de una imagen
		/// </summary>
		private RSSImage ParseImage(MLNode objMLImage)
		{
			RSSImage image = new RSSImage();

				// Lee los datos de la imagen
				foreach (MLNode node in objMLImage.Nodes)
					switch (node.Name)
					{
						case RSSConstTags.cnstStrChannelImageUrl:
								image.Url = node.Value;
							break;
						case RSSConstTags.cnstStrChannelImageTitle:
								image.Title = node.Value;
							break;
						case RSSConstTags.cnstStrChannelImageLink:
								image.Link = node.Value;
							break;
					}
				// Devuelve la imagen
				return image;
		}

		/// <summary>
		///		Interpreta los nodos de un elemento
		/// </summary>
		private RSSEntry ParseEntry(MLNode objMLEntry, RSSChannel channel)
		{
			RSSEntry entry = new RSSEntry();

				// Interpreta los nodos
				foreach (MLNode node in objMLEntry.Nodes)
					switch (node.Name)
					{
						case RSSConstTags.cnstStrItemTitle:
								entry.Title = node.Value;
							break;
						case RSSConstTags.cnstStrItemLink:
								entry.Link = node.Value;
							break;
						case RSSConstTags.cnstStrItemDescription:
								entry.Content = node.Value;
							break;
						case RSSConstTags.cnstStrItemCategory:
								entry.Categories.Add(ParseCategory(node));
							break;
						case RSSConstTags.cnstStrItemPubDate:
								entry.DateCreated = node.Value.GetDateTime(DateTime.Now);
							break;
						case RSSConstTags.cnstStrItemGuid:
								entry.GUID = ParseGuid(node);
							break;
						case RSSConstTags.cnstStrItemEnclosure:
								entry.Enclosures.Add(ParseEnclosure(node));
							break;
						case RSSConstTags.cnstStrItemAuthor:
								entry.Authors.Add(ParseAuthor(node));
							break;
						default:
								entry.Extensions.Parse(node, entry, channel.Dictionary);
							break;
					}
				// Devuelve la entrada
				return entry;
		}

		/// <summary>
		///		Interpreta el ID
		/// </summary>
		private RSSGuid ParseGuid(MLNode node)
		{
			RSSGuid objGuid = new RSSGuid();

				// Interpreta el XML
				objGuid.IsPermaLink = node.Attributes[RSSConstTags.cnstStrItemAttrPermaLink].Value.GetBool(false);
				objGuid.ID = node.Value;
				// Devuelve el objeto
				return objGuid;
		}

		/// <summary>
		///		Interpreta los datos del autor
		/// </summary>
		private RSSAuthor ParseAuthor(MLNode node)
		{
			return new RSSAuthor(node.Value);
		}

		/// <summary>
		///		Interpreta la categoría
		/// </summary>
		private RSSCategory ParseCategory(MLNode node)
		{
			return new RSSCategory(node.Value);
		}

		/// <summary>
		///		Interpreta un adjunto
		/// </summary>
		private RSSEnclosure ParseEnclosure(MLNode node)
		{
			RSSEnclosure objEnclosure = new RSSEnclosure();

				// Recoge los datos
				objEnclosure.Url = node.Attributes[RSSConstTags.cnstStrItemAttrUrl].Value;
				objEnclosure.Length = node.Attributes[RSSConstTags.cnstStrItemAttrLength].Value.GetInt(0);
				objEnclosure.Type = node.Attributes[RSSConstTags.cnstStrItemAttrType].Value;
				// Devuelve el objeto
				return objEnclosure;
		}
	}
}