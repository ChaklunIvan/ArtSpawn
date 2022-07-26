using ArtSpawn.Models.Entities;
using ArtSpawn.Models.Requests;
using ArtSpawn.Models.Responses;

namespace ArtSpawn.Tests
{
    public static class TestData
    {
        public static CancellationToken CancellationToken = new();
        public static Guid ModelId = new("61EDE4A9-748A-4E15-8043-7682F4C5147C");
        public static PagingRequest PagingRequest = new();
    }
}
