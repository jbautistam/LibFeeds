using System;

namespace Bau.Libraries.LibFeeds.Syndication.Atom.Data
{
	/// <summary>
	///		Generador de Atom
	/// </summary>
	public class AtomGenerator
	{
		public AtomGenerator() : this(null, null, null, null) { }

		public AtomGenerator(string url, string version, string name) : this(url, version, null, name)
		{
		}

		public AtomGenerator(string url, string version, string language, string name)
		{
			URL = url;
			Version = version;
			Language = language;
			Name = name;
		}

		/// <summary>
		///		URL
		/// </summary>
		public string URL { get; set; }

		/// <summary>
		///		Versión
		/// </summary>
		public string Version { get; set; }

		/// <summary>
		///		Lenguaje
		/// </summary>
		public string Language { get; set; }

		/// <summary>
		///		Nombre
		/// </summary>
		public string Name { get; set; }
	}
}
