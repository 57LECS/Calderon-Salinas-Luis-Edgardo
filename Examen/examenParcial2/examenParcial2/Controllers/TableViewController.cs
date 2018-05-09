using System;
using System.Collections.Generic;
using examenParcial2.Models;
using examenParcial2.Views;
using Foundation;
using LinqToTwitter;
using UIKit;

namespace examenParcial2.Controllers
{
    public partial class TableViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate, IUISearchResultsUpdating
    {

        UISearchController searchController;
		List<Status> tweets;


        public TableViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitializeComponents();
            // Perform any additional setup after loading the view, typically from a nib.
        }


        void InitializeComponents(){

			tweets = new List<Status>();




            linq2Twitter.SharedInstance.TweetsFetchedArgs += SharedInstance_TweetsFetched;
            linq2Twitter.SharedInstance.FailedTweetsFetched += SharedInstance_FailedTweetsFetched;



            searchController = new UISearchController(searchResultsController: null)
            {
                SearchResultsUpdater = this,
                DimsBackgroundDuringPresentation = false
            };
            TweetsTableView.DataSource = this;
            TweetsTableView.Delegate = this;
            TweetsTableView.TableHeaderView = searchController.SearchBar;
            TweetsTableView.RowHeight = UITableView.AutomaticDimension;
            TweetsTableView.EstimatedRowHeight = 50;

        }



        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }


        [Export("numberOfSectionsInTableView:")]
        public nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
			return tweets.Count;
			//return 10;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
			var tweet = tweets[indexPath.Row];
			var cell = tableView.DequeueReusableCell(TweetTableViewCell.Key, indexPath) as TweetTableViewCell;
			cell.Tweet = tweet.FullText;
            cell.User = tweet.User.Name;
            cell.Img = FromUrl(tweet.User.ProfileImageUrlHttps);
            return cell;
        }
        

        

        public void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            linq2Twitter.SharedInstance.SearchTweets(searchController.SearchBar.Text);
        }




		void SharedInstance_TweetsFetched(object sender, TweetsFetchedArgs e)
		{
			tweets = e.Tweets;
			InvokeOnMainThread(() => TweetsTableView.ReloadData());
		}

        void SharedInstance_FailedTweetsFetched(object sender, FailedTweetsFetchedArgs e)
        {
			Console.WriteLine(e.Error);

            InvokeOnMainThread(()=>{
                searchController.DismissViewController(true,null);

                UIAlertController alerta = UIAlertController.Create("Error", "Lo sentimos por alguna razon no encontramos tweets", UIAlertControllerStyle.Alert);
                alerta.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel,null));
                PresentViewController(alerta,true,null);

            });
        }

        static UIImage FromUrl(string uri)
        {
            try
            {
                using (var url = new NSUrl(uri))
                using (var data = NSData.FromUrl(url))
                    return UIImage.LoadFromData(data);
            }
            catch (ArgumentNullException ex)
            {
                return new UIImage();
            }

        }
    }
}

