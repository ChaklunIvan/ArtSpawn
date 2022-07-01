using ArtSpawn.Models.Constants;
using ArtSpawn.Models.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArtSpawn.Infrastructure.Helpers
{
    public static class PaginationHelper<TEnity> where TEnity : class
    {
        public static PagedList<TEnity> GetPagedModel(IEnumerable<TEnity> items, int totalItems, int currentPage, int pageSize)
        {

            var pagedModel = new PagedList<TEnity>
            {
                Items = items,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
            };

            return pagedModel;
        }

        public static (IEnumerable<TEnity>, int) ToPagedList(IQueryable<TEnity> source, int pageNumber, int pageSize)
        {
            var count =  source.Count();
            var items =  source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return (items, count);
        }

        public static IHeaderDictionary GetPagingHeaders(PagedList<TEnity> pagedList)
        {

            IHeaderDictionary headers = new HeaderDictionary();
            var metadata = new
            {
                pagedList.TotalCount,
                pagedList.PageSize,
                pagedList.CurrentPage,
                pagedList.TotalPages,
                pagedList.HasNext,
                pagedList.HasPrevious
            };

            headers.Add(PaginationConstans.XTotalCountHeaderName, pagedList.TotalCount.ToString());
            headers.Add(PaginationConstans.XPaginationHeaderName, JsonConvert.SerializeObject(metadata));

            return headers;
        }
    }
}
