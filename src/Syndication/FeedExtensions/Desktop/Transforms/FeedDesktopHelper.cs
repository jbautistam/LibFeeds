using System;

using Bau.Libraries.LibFeeds.Syndication.FeedExtensions.Desktop.Data;

namespace Bau.Libraries.LibFeeds.Syndication.FeedExtensions.Desktop.Transforms
{
	/// <summary>
	///		Clase de ayuda para tratamiento de extensiones <see cref="FeedDesktop"/>
	/// </summary>
	public static class FeedDesktopHelper
	{
		/// <summary>
		///		Busca la entrada del tipo de extensión <see cref="FeedDesktop"/> entre las extensiones de la entrada
		/// </summary>
		private static FeedDesktop Search(FeedEntryBase entry)
		{ 
			// Recorre las extensiones buscando las del tipo FeedDesktop
			foreach (ExtensionBase extension in entry.Extensions)
				if (extension is FeedDesktop)
					return extension as FeedDesktop;
			// Si ha llegado hasta aquí es porque no ha encontrado la extensión
			return null;
		}

		/// <summary>
		///		Comprueba las extensiones de la entrada comprobando si se ha leído
		/// </summary>
		public static bool IsRead(FeedEntryBase entry)
		{
			FeedDesktop extension = Search(entry);

				return extension != null && extension.IsRead;
		}

		/// <summary>
		///		Marca una entrada como leída
		/// </summary>
		public static void MarkEntryRead(FeedEntryBase entry, bool blnIsRead)
		{
			FeedDesktop extension = Search(entry);

				// Si no se ha encontrado, añade una nueva
				if (extension == null)
				{ 
					// Crea la extensión
					extension = new FeedDesktop();
					// La añade a la colección de extensiones de la entrada
					entry.Extensions.Add(extension);
				}
				// Marca la extensión como leída o no
				extension.IsRead = blnIsRead;
		}

		/// <summary>
		///		Cuenta el número de elementos no leídos de un canal
		/// </summary>
		public static int CountNotRead<TypeData>(FeedChannelBase<TypeData> channel) where TypeData : FeedEntryBase
		{
			int intNumber = 0;

				// Recorre las entradas
				foreach (TypeData entry in channel.Entries)
					if (!IsRead(entry))
						intNumber++;
				// Devuelve el número de elementos no leídos
				return intNumber;
		}

		/// <summary>
		///		Obtiene la fecha de última modificación de un canal
		/// </summary>
		public static DateTime GetLastUpdated<TypeData>(FeedChannelBase<TypeData> channel) where TypeData : FeedEntryBase
		{
			DateTime dtmLastUpdated = DateTime.MinValue;

				// Recorre las entradas
				foreach (FeedEntryBase entry in channel.Entries)
					if (entry.DateCreated > dtmLastUpdated)
						dtmLastUpdated = entry.DateCreated;
				// Devuelve la fecha de última modificación
				return dtmLastUpdated;
		}

		/// <summary>
		///		Obtiene la prioridad de una entrada
		/// </summary>
		public static int GetPriority(FeedEntryBase entry)
		{
			FeedDesktop extension = Search(entry);

				if (extension != null)
					return extension.Priority;
				else
					return 0;
		}

		/// <summary>
		///		Asigna la prioridad a una entrada
		/// </summary>
		public static void SetPriority(FeedEntryBase entry, int intPriority)
		{
			FeedDesktop extension = Search(entry);

				if (extension != null)
				{ 
					// Marca el elemento como leído
					if (intPriority != 0)
						extension.IsRead = true;
					// Cambia la prioridad
					extension.Priority = intPriority;
				}
		}
	}
}
