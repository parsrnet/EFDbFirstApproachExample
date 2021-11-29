using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFDbFirstApproachExample.HtmlHelpers
{
	public static class FileHelper
	{
		public static MvcHtmlString File(this HtmlHelper htmlHelper, string cssClassName)
		{
			TagBuilder tag = new TagBuilder("input");
			tag.MergeAttributes(new Dictionary<string,string> () {
				{ "type", "file" },
				{ "id", "Image" },
				{ "name", "Img" },
				{ "class", cssClassName },
			});
			return new MvcHtmlString(tag.ToString(TagRenderMode.SelfClosing));
		}
	}
}