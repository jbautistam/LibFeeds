using System;

using Bau.Libraries.LibFeeds.Syndication.Atom.Data;
using Bau.Libraries.LibFeeds.Syndication.RSS.Data;

namespace Bau.Libraries.LibFeeds.Syndication.Atom.Transforms
{
	/// <summary>
	///		Clase para conversión de archivos Atom a RSS
	/// </summary>
	public class AtomToRSS
	{
		/// <summary>
		///		Convierte un archivo RSS en un archivo Atom
		/// </summary>
		public RSSChannel Convert(AtomChannel channel)
		{
			RSSChannel rss = new RSSChannel();

				// Convierte los datos del canal
				rss.Title = channel.Title.Content;
				rss.Generator = channel.Generator.Name;
				rss.Description = channel.Info.Content;
				if (channel.Links.Count > 0)
					rss.Link = channel.Links[0].Href;
				rss.LastBuildDate = channel.LastUpdated;
				// Añade las extensiones
				ConvertExtension(channel.Extensions, rss.Extensions);
				// Añade las entradas
				ConvertEntries(channel, rss);
				// Devuelve el objeto Atom
				return rss;
		}

		/// <summary>
		///		Convierte las entradas Atom en entradas RSS
		/// </summary>
		private void ConvertEntries(AtomChannel channel, RSSChannel rss)
		{
			foreach (AtomEntry channelEntry in channel.Entries)
			{
				RSSEntry rssEntry = new RSSEntry();

					// Convierte los datos de la entrada
					rssEntry.GUID.ID = channelEntry.ID;
					rssEntry.Title = channelEntry.Title.Content;
					rssEntry.Content = channelEntry.Content.Content;
					rssEntry.DateCreated = channelEntry.DatePublished;
					// Vínculos
					if (channelEntry.Links.Count > 0)
						rssEntry.Link = channelEntry.Links[0].Href;
					foreach (AtomLink channelLink in channelEntry.Links)
						if (channelLink.LinkType.Equals("enclosure"))
							rssEntry.Enclosures.Add(ConvertLink(channelLink));
					// Autores
					foreach (AtomPeople channelAuthor in channelEntry.Authors)
						rssEntry.Authors.Add(new RSSAuthor(channelAuthor.Name));
					// Categorías
					foreach (AtomCategory channelCategory in channelEntry.Categories)
						rssEntry.Categories.Add(new RSSCategory(channelCategory.Name));
					// Convierte las extensiones
					ConvertExtension(channelEntry.Extensions, rssEntry.Extensions);
					// Añade la entrada al objeto Atom
					rss.Entries.Add(rssEntry);
			}
		}

		/// <summary>
		///		Devuelve un adjunto a partir de un vínculo
		/// </summary>
		private RSSEnclosure ConvertLink(AtomLink channelLink)
		{
			RSSEnclosure objEnclosure = new RSSEnclosure();

				// Asigna las propiedades
				objEnclosure.Url = channelLink.Href;
				objEnclosure.Type = channelLink.Type;
				// Devuelve el objeto
				return objEnclosure;
		}

		/// <summary>
		///		Convierte las extensiones
		/// </summary>
		private void ConvertExtension(FeedExtensions.ExtensionsCollection rssExtensions,
									  FeedExtensions.ExtensionsCollection channelExtensions)
		{
			foreach (FeedExtensions.ExtensionBase extension in rssExtensions)
				channelExtensions.Add(extension);
		}
	}
}
