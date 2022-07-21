using ArtSpawn.Helpers.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ArtSpawn.Extensions
{
    public static class ResultExtension
    {
        public static ActionResult WithHeaders(this ActionResult receiver, IHeaderDictionary headers)
          => new ActionResultWithHeaders(receiver, headers);

    }
}
