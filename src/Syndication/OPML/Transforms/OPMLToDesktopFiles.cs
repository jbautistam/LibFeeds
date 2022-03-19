using System;

using Bau.Libraries.LibFeeds.Syndication.DesktopFiles.Data;

namespace Bau.Libraries.LibFeeds.Syndication.OPML.Transforms
{
	/// <summary>
	///		Conversor de <see cref="OPMLCahnnel"/ a <see cref="DesktopFilesChannel"/> >
	/// </summary>
	public class OPMLToDesktopFiles
	{
		/// <summary>
		///		Convierte un canal OPML a un canal DestopFiles
		/// </summary>
		public DesktopFilesChannel Convert(Data.OPMLChannel channel)
		{
			DesktopFilesChannel desktopChannel = new DesktopFilesChannel();

				// Asigna las propiedades al canal
				desktopChannel.DateCreated = channel.DateCreated;
				desktopChannel.DateModified = channel.DateModified;
				desktopChannel.Title = channel.Title;
				// Añade las entradas
				AddOpmlEntries(desktopChannel, null, channel.Entries);
				// Devuelve el canal
				return desktopChannel;
		}

		/// <summary>
		///		Añade las entradas OPML a las categorías
		/// </summary>
		private void AddOpmlEntries(DesktopFilesChannel channel, DesktopFilesEntry parent,
									Data.OPMLEntriesCollection opmlEntries)
		{
			foreach (Data.OPMLEntry channelEntry in opmlEntries)
			{
				DesktopFilesEntry entry = new DesktopFilesEntry();

					// Asigna las propiedades
					entry.Text = channelEntry.Title;
					if (string.IsNullOrEmpty(entry.Text))
						entry.Text = channelEntry.Text;
					entry.URL = channelEntry.URL;
					entry.NumberNotRead = 0;
					// Añade la entrada
					if (parent == null)
						channel.Entries.Add(entry);
					else
						parent.Entries.Add(entry);
					// Añade las entradas OPML hijas
					AddOpmlEntries(channel, entry, channelEntry.Entries);
			}
		}
	}
}
