namespace Bau.Libraries.LibFeeds.Exceptions;

/// <summary>
///		Clase de excepci�n
/// </summary>
public class FeedException : Exception
{
	/// <summary>
	///		Tipo de excepci�n
	/// </summary>
	public enum ExceptionType
	{
		/// <summary>No se reconoce el tipo de datos</summary>
		DataNotRecognized,
		/// <summary>Error de descarga</summary>
		DownloadError
	}

	public FeedException(ExceptionType type, string message, Exception? innerException = null) : base(message, innerException)
	{
		Type = type;
	}

	public FeedException() : base()
	{
	}

	public FeedException(string message) : base(message)
	{
	}

	public FeedException(string message, Exception innerException) : base(message, innerException)
	{
	}

	/// <summary>
	///		Tipo de excepci�n
	/// </summary>
	public ExceptionType Type { get; }
}