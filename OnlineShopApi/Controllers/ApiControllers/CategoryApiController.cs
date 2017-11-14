using OnlineShopApi.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShopApi.Controllers.ApiControllers
{
	[RoutePrefix("category")]
	public class CategoryApiController : ApiController
	{
		[HttpPost]
		[Route("edit_category")]
		public IHttpActionResult EditCategory([FromBody]AddProductToBasketRequest request)
		{
			return Ok();
		}
	}
}