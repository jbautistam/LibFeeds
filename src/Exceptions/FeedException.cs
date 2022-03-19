using System;

namespace Bau.Libraries.LibFeeds.Exceptions
{
	/// <summary>
	///		Clase de excepción
	/// </summary>
	public class FeedException : Exception
	{   

		// Enumerados
		/// <summary>
		///		Tipo de excepción
		/// </summary>
		public enum ExceptionType
		{
			/// <summary>No se reconoce el tipo de datos</summary>
			DataNotRecognized,
			/// <summary>Error de descarga</summary>
			DownloadError
		}

		public FeedException(ExceptionType type, string message, Exception innerException = null) : base(message, innerException)
		{
			Type = type;
		}

		public FeedException() : base()
		{
		}

		protected FeedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
		{
		}

		public FeedException(string message) : base(message)
		{
		}

		public FeedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>
		///		Tipo de excepción
		/// </summary>
		public ExceptionType Type { get; }
	}
}