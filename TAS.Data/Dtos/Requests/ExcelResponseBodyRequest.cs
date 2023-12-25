using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace TAS.Data.Dtos.Requests
{
	public class ExcelResponseBodyRequest<T>
	{
		public HttpStatusCode code { get; set; }
		public string message { get; set; }
		public T? data { get; set; }	
		public int? maxPage { get; set; }
	}
}
