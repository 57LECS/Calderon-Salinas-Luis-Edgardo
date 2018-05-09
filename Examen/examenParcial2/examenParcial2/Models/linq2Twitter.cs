using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using LinqToTwitter;
using System.Net;
using System.Collections.Generic;

namespace examenParcial2.Models
{
    public class linq2Twitter
    {

        static Lazy<linq2Twitter> lazy = new Lazy<linq2Twitter>(() => new linq2Twitter());
        public static linq2Twitter SharedInstance { get => lazy.Value; }



        public event EventHandler<TweetsFetchedArgs> TweetsFetchedArgs;
        public event EventHandler<FailedTweetsFetchedArgs> FailedTweetsFetched;








        SingleUserAuthorizer authorizer;
        TwitterContext twitterContext;
        CancellationTokenSource cancellationTokenSource;

        public linq2Twitter()
        {
            authorizer = new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey = "",
                    ConsumerSecret = "",
                    AccessToken = "",
                    AccessTokenSecret = ""
                }
            };

            twitterContext = new TwitterContext(authorizer);
            cancellationTokenSource = new CancellationTokenSource();
        }



        public void SearchTweets(string query)
        {
            if (cancellationTokenSource.IsCancellationRequested)
                cancellationTokenSource.Cancel();

            var cancellationToken = cancellationTokenSource.Token;

            Task.Factory.StartNew(async () =>
            {
                try
                {
                    var tweets = await SearcTweetsAsync(query, cancellationToken);
                    var e = new TweetsFetchedArgs(tweets);
                    TweetsFetchedArgs?.Invoke(this, e);

                }
                catch (Exception ex)
                {
                    var e = new FailedTweetsFetchedArgs(ex.Message);
                    FailedTweetsFetched?.Invoke(this, e);

                }
            }, cancellationToken);

        }


        async Task<List<Status>> SearcTweetsAsync(string query, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<Status>();

            Search searchResponse =
                await
                (from search in twitterContext.Search
                 where search.Type == SearchType.Search &&
                 search.Query == query &&
                       search.IncludeEntities == true &&
                       search.TweetMode == TweetMode.Extended
                 select search)
                .SingleOrDefaultAsync();

            cancellationToken.ThrowIfCancellationRequested();

            return searchResponse?.Statuses;

        }
    }




    public class TweetsFetchedArgs : EventArgs
    {
        public List<Status> Tweets { get; private set; }


        public TweetsFetchedArgs(List<Status> tweets)
        {

            Tweets = tweets;
        }

    }

    public class FailedTweetsFetchedArgs : EventArgs
    {
        public string Error { get; private set; }

        public FailedTweetsFetchedArgs(string error)
        {
            Error = error;
        }

    }
}
