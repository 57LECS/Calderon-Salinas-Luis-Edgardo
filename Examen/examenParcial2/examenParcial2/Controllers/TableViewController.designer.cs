// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace examenParcial2.Controllers
{
	[Register ("TableViewController")]
	partial class TableViewController
	{
		[Outlet]
		UIKit.UITableView TweetsTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TweetsTableView != null) {
				TweetsTableView.Dispose ();
				TweetsTableView = null;
			}
		}
	}
}
