using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace AlexandriaOrg.Alexandria.Amazon
{
	public class WebServiceClient
	{
		#region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
		public WebServiceClient()
		{
		}
		#endregion
		
		#region Private Fields
		private const string wsdlPath = "http://soap.amazon.com/schemas3/AmazonWebServices.wsdl";
		private const string accessKeyId = "0NN3FG2840KM384025G2";
		#endregion
		
		#region Public Methods
		/// <summary>
		/// A Basic Amazon Search
		/// </summary>
		/// <param name="mode">The search mode to use</param>
		/// <param name="search">The search</param>
		/// <see cref="http://msdn.microsoft.com/coding4fun/web/services/article.aspx?articleid=912260"/>
		public string BasicSearch(SearchMode mode, string search)
		{
			System.Text.StringBuilder details = new StringBuilder("Search Results:\n\n");
		
			try
			{
				AmazonWebService.KeywordRequest request = new AmazonWebService.KeywordRequest();
				request.locale = "us";
				request.type = "lite";
				request.sort = "reviewrank";
				request.mode = mode.ToString();
				request.keyword = search;
				request.tag = accessKeyId;
				request.devtag = accessKeyId;
				
				AmazonWebService.AmazonSearchService searchService = new AlexandriaOrg.Alexandria.Amazon.AmazonWebService.AmazonSearchService();
				AmazonWebService.ProductInfo info = searchService.KeywordSearchRequest(request);
				foreach(AmazonWebService.Details detail in info.Details)
				{
					details.Append(detail.ProductName + " [ ASIN: " + detail.Asin);					
					if (!string.IsNullOrEmpty(detail.OurPrice))
						details.Append(" Amazon: " + detail.OurPrice);
					int usedCount;
					int.TryParse(detail.UsedCount, out usedCount);
					if (usedCount > 0 && !string.IsNullOrEmpty(detail.UsedPrice))
						details.Append(", Used: " + detail.UsedPrice);
					details.Append("]\n");
					
					System.Diagnostics.Debug.WriteLine("Product Name: " + detail.ProductName);
				}
				
				return details.ToString();
			}
			catch (Exception ex)
			{
				throw new AlexandriaException("The was an error attempting to search Amazon", ex);
			}
		}
		#endregion
	}
}
