using System;

namespace Bau.Libraries.LibFeeds.Syndication.RSS.Data
{
	/// <summary>
	///		Adjunto de un elemento
	/// </summary>
	public class RSSEnclosure
	{
		/// <summary>
		///		URL del adjunto
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		///		Longitud del adjunto
		/// </summary>
		public long Length { get; set; }

		/// <summary>
		///		Tipo del adjunto
		/// </summary>
		public string Type { get; set; }
	}
}
