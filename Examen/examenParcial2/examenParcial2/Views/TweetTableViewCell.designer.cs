// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace examenParcial2.Views
{
	[Register ("TweetTableViewCell")]
	partial class TweetTableViewCell
	{
		[Outlet]
		UIKit.UIImageView imgProfile { get; set; }

		[Outlet]
		UIKit.UILabel lblRt { get; set; }

		[Outlet]
		UIKit.UILabel lblTweet { get; set; }

		[Outlet]
		UIKit.UILabel lblUserName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblRt != null) {
				lblRt.Dispose ();
				lblRt = null;
			}

			if (lblTweet != null) {
				lblTweet.Dispose ();
				lblTweet = null;
			}

			if (imgProfile != null) {
				imgProfile.Dispose ();
				imgProfile = null;
			}

			if (lblUserName != null) {
				lblUserName.Dispose ();
				lblUserName = null;
			}
		}
	}
}
