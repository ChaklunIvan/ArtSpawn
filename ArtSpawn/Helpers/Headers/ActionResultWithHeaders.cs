using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Threading.Tasks;

namespace ArtSpawn.Helpers.Headers
{
    public class ActionResultWithHeaders : ActionResult
    {
        private readonly IHeaderDictionary _headers;
        private readonly ActionResult _result;
        public IEnumerable Items { get; set; }

        public ActionResultWithHeaders(ActionResult receiver, IHeaderDictionary headers, IEnumerable items)
        {
            _result = receiver;
            _headers = headers;
            Items = items;
        }

        private void AddHeaders(HttpResponse response)
        {
            foreach (var (name, value) in _headers) response.Headers.Add(name, value);
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            AddHeaders(context.HttpContext.Response);
            return _result.ExecuteResultAsync(context);
        }

        public override void ExecuteResult(ActionContext context)
        {
            AddHeaders(context.HttpContext.Response);
            _result.ExecuteResult(context);
        }
    }
}
