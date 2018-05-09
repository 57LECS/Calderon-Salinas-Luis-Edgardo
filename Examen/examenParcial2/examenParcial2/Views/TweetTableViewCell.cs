using System;

using Foundation;
using UIKit;

namespace examenParcial2.Views
{
    public partial class TweetTableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("TweetTableViewCell");

        public string Tweet
        {
			get => lblTweet.Text;
            set => lblTweet.Text = value;
        }

        public UIImage Img
        {
            get => imgProfile.Image;
            set => imgProfile.Image = value;
        }

        public string User
        {
            get => lblUserName.Text;
            set => lblUserName.Text = value;
        }

		public NSIndexPath IndexPath{
			get;
			set;
		}
        protected TweetTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
