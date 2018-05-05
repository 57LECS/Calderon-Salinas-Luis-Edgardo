// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace galeria
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIBarButtonItem btnEdit { get; set; }

		[Outlet]
		UIKit.UIImageView imgAbajo { get; set; }

		[Outlet]
		UIKit.UIImageView imgArriba { get; set; }

		[Outlet]
		UIKit.UILabel lblModifiquele { get; set; }

		[Outlet]
		UIKit.UILabel lblOprimale { get; set; }

		[Outlet]
		UIKit.UIView viewAbajo { get; set; }

		[Outlet]
		UIKit.UIView viewArriba { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblModifiquele != null) {
				lblModifiquele.Dispose ();
				lblModifiquele = null;
			}

			if (lblOprimale != null) {
				lblOprimale.Dispose ();
				lblOprimale = null;
			}

			if (imgArriba != null) {
				imgArriba.Dispose ();
				imgArriba = null;
			}

			if (imgAbajo != null) {
				imgAbajo.Dispose ();
				imgAbajo = null;
			}

			if (btnEdit != null) {
				btnEdit.Dispose ();
				btnEdit = null;
			}

			if (viewArriba != null) {
				viewArriba.Dispose ();
				viewArriba = null;
			}

			if (viewAbajo != null) {
				viewAbajo.Dispose ();
				viewAbajo = null;
			}
		}
	}
}
