using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBook.Client.Static
{
    public static class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string PostsEndpoint = $"{Prefix}/posts";
        public static readonly string CommentsEndpoint = $"{Prefix}/comments";
        public static readonly string AccountsEndpoint = $"{Prefix}/accounts";
        public static readonly string LikesEndpoint = $"{Prefix}/likes";
        public static readonly string LocationsEndpoint = $"{Prefix}/locations";
    }
}
