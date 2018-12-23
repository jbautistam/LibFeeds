using System;
using System.Net;

using Bau.Libraries.LibCommonHelper.Communications;

namespace Bau.Libraries.LibFeeds.Process
{
	/// <summary>
	///		Clase para descarga de archivos
	/// </summary>
	internal static class WebDownload
	{
		/// <summary>
		///		Descarga los datos
		/// </summary>
		internal static void Download(string url, string fileName)
		{
			LibCommonHelper.Files.HelperFiles.SaveTextFile(fileName,new HttpWebClient().HttpGet(url));
		}

		///// <summary>
		/////		Descarga un archivo
		///// </summary>
		//internal static void Download(string url, string fileName)
		//{
		//	Download(url, fileName, null, 0, null, null);
		//}

		///// <summary>
		/////		Descarga los datos
		///// </summary>
		//internal static void Download(string url, string fileName, string proxyName, int proxyPort,
		//							  string user, string password)
		//{
		//	using (WebResponse webResponse = GetWebRequest(url, proxyName, proxyPort, user, password).GetResponse())
		//	{ 
		//		// Abre un stream de lectura sobre la respuesta HTPP
		//		using (System.IO.Stream response = webResponse.GetResponseStream())
		//		{   
		//			// Escribe la respuesta HTPP en un archivo
		//			using (System.IO.FileStream file = new System.IO.FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write))
		//			{
		//				byte[] buffer = new byte[2048]; // Buffer de 2KB
		//				int size;

		//					// Recorre el stream de entrada grabando en el de salida
		//					while ((size = response.Read(buffer, 0, buffer.Length)) > 0)
		//						file.Write(buffer, 0, size);
		//					// Cierra el archivo de salida
		//					file.Flush();
		//					file.Close();
		//			}
		//			// Cierra el stream de lectura
		//			response.Close();
		//		}
		//		// Cierra el stream HTTP
		//		webResponse.Close();
		//	}
		//}

		///// <summary>
		/////		Obtiene un WebRequest configurado para la tarea
		///// </summary>
		//private static WebRequest GetWebRequest(string url, string proxyName, int proxyPort, string user, string password)
		//{
		//	WebRequest webRequest = HttpWebRequest.Create(url);

		//		// Asigna las credenciales si es necesario
		//		if (!string.IsNullOrEmpty(user))
		//			webRequest.Credentials = new NetworkCredential(user, password);
		//		// Asigna el proxy si es necesario
		//		if (!string.IsNullOrEmpty(proxyName))
		//		{
		//			webRequest.Proxy = new WebProxy(proxyName, proxyPort);

		//			if (!string.IsNullOrEmpty(user))
		//				webRequest.Proxy.Credentials = new NetworkCredential(user, password);
		//		}
		//		//else
		//		//  objWebRequest.Proxy = WebProxy.GetDefaultProxy();
		//		// Devuelve el WebRequest configurado
		//		return webRequest;
		//}
	}
}
