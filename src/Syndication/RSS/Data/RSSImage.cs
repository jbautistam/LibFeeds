using System;

namespace Bau.Libraries.LibFeeds.Syndication.RSS.Data
{
	/// <summary>
	///		Clase con los datos de una imagen para el archivo RSS
	/// </summary>
	public class RSSImage
	{
		public RSSImage() { }

		public RSSImage(string url) : this(url, null, null) { }

		public RSSImage(string url, string title, string strLink)
		{
			Url = url;
			Title = title;
			Link = strLink;
		}

		/// <summary>
		///		URL de la imagen
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		///		Título de la imagen
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///		Vínculo asociado a la imagen
		/// </summary>
		public string Link { get; set; }
	}
}
