using System;

namespace Bau.Libraries.LibFeeds.Syndication.RDF.Data
{
	/// <summary>
	///		Clase con los datos de un canal RDF
	/// </summary>
	/// <example>
	/// <rdf:RDF xmlns:rdf="http://www.w3.org/1999/02/22-rdf-syntax-ns#" xmlns="http://purl.org/RDF/1.0/" xmlns:taxo="http://purl.org/RDF/1.0/modules/taxonomy/" xmlns:sy="http://purl.org/RDF/1.0/modules/syndication/" xmlns:dc="http://purl.org/dc/elements/1.1/">
	///   <channel rdf:about="http://www.infoq.com">
	///     <title>InfoQ Personalized Feed for jose bautista</title>
	///     <link>http://www.infoq.com</link>
	///     <description>This RDF feed is a personalized feed, unique to your account (registered or unregistered) on InfoQ.com.  On InfoQ you can select which communities you are interested in using the 'Your Communities' box on the left side bar, and you can even turn off any content matching sub-topics or tags. The results of those selections will reflect in what news you see in this RDF feed - it should match whatever you see in the 'news' center panel on InfoQ. If you are NOT seeing this correlation then you may not be using a feed URL associated with your InfoQ account. For best accuracy, register to InfoQ and then grab a new RDF feed url from the 'Personal Feed' link on the left side menu. Enjoy!</description>
	///   </channel>
	///   <item rdf:about="http://www.infoq.com/news/2010/03/SQL-Azure-Labs">
	///     <title>SQL Azure Unveils New Features and a Prototype Lab</title>
	///     <link>http://www.infoq.com/news/2010/03/SQL-Azure-Labs</link>
	///     <description>SQL Azure will be rolling out new features over the next few months including MARS support, spatial data, and a 50 GB option. Also available is SQL Azure Labs, where previews of possible enhancements like OData Services will be showcased. &lt;i&gt;By Jonathan Allen&lt;/i&gt;</description>
	///     <dc:creator>Jonathan Allen</dc:creator>
	///     <dc:date>2010-03-31T05:19:00Z</dc:date>
	///     <dc:identifier>/news/2010/03/SQL-Azure-Labs</dc:identifier>
	///   </item>
	/// </rdf:RDF>
	/// </example>
	public class RDFChannel : FeedChannelBase<RDFEntry>
	{ 
		/// <summary>
		///		Título
		/// </summary>
		public string Title { get; set; }
		
		/// <summary>
		///		Descripción
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		///		Vínculo del canal
		/// </summary>
		public string Link { get; set; }
	}
}