using System;

namespace Bau.Libraries.LibFeeds.Syndication.Atom.Data
{
	/// <summary>
	///		Datos de una categoría para Atom
	/// </summary>
	public class AtomCategory
	{
		public AtomCategory() : this(null) { }

		public AtomCategory(string name)
		{
			Name = name;
		}

		/// <summary>
		///		Nombre de la categoría
		/// </summary>
		public string Name { get; set; }
	}
}
