using ArtSpawn.Helpers.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace ArtSpawn.Extensions
{
    public static class ResultExtension
    {
        public static ActionResult WithHeaders(this ActionResult receiver, IHeaderDictionary headers, IEnumerable items)
          => new ActionResultWithHeaders(receiver, headers, items);

    }
}
