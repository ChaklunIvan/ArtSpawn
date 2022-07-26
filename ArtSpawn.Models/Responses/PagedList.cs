using ArtSpawn.Models.Entities;
using System;
using System.Collections.Generic;

namespace ArtSpawn.Models.Responses
{
    public class PagedList<TEntity> : List<TEntity> where TEntity : class
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public IEnumerable<TEntity> Items { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
