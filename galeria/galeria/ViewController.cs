using System;
using AVFoundation;
using Foundation;
using Photos;
using UIKit;

namespace galeria
{
    public partial class ViewController : UIViewController, IUIImagePickerControllerDelegate
    {

        UITapGestureRecognizer arribaTapGesture;
        UITapGestureRecognizer abajoTapGesture;

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitializeComponents();

            // Perform any additional setup after loading the view, typically from a nib.
        }



        void ShowOptions (UITapGestureRecognizer gesture){
            
            var alertController = UIAlertController.Create(null, null, UIAlertControllerStyle.ActionSheet);
            alertController.AddAction(UIAlertAction.Create("Elegir foto",UIAlertActionStyle.Default,TryOpenLibrary));
            alertController.AddAction(UIAlertAction.Create("Tomar foto", UIAlertActionStyle.Default, TryOpenCamera));
            alertController.AddAction(UIAlertAction.Create("Cancelar", UIAlertActionStyle.Cancel, null));


            PresentViewController(alertController,true,null);

            
        }

        private void TryOpenCamera(UIAlertAction obj)
        {
            if(!UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera)){
                return;
            }

            CheckCameraAuthorizationStatus(AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video));

        }

        private void TryOpenLibrary(UIAlertAction obj)
        {
            if (!UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.PhotoLibrary))
            {
                return;
            }

            CheckPhotoLibraryAutorizationStatus(PHPhotoLibrary.AuthorizationStatus);
        }

        private void CheckPhotoLibraryAutorizationStatus(PHAuthorizationStatus authorizationStatus)
        {

            switch (authorizationStatus)
            {
                case PHAuthorizationStatus.NotDetermined:
                    PHPhotoLibrary.RequestAuthorization(CheckPhotoLibraryAutorizationStatus);
                    break;
                case PHAuthorizationStatus.Restricted:
                    InvokeOnMainThread(() =>
                    {
                        ShowMessage("Error","FATAL ERROR x0008174",NavigationController);
                    });
                    break;
                case PHAuthorizationStatus.Denied:
                    InvokeOnMainThread(() =>
                    {
                        ShowMessage("Error", "FATAL ERROR x0008710", NavigationController);
                    });
                    
                    break;
                case PHAuthorizationStatus.Authorized:
                    
                    InvokeOnMainThread(()=> {
                        var imagePickerController = new UIImagePickerController
                        {
                            SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                            Delegate = this
                        };
                        PresentViewController(imagePickerController, true, null);
                        
                    });
                    break;
                default:
                    break;
            }
        }



        void CheckCameraAuthorizationStatus(AVAuthorizationStatus auth)
        {
            switch (auth)
            {
                case AVAuthorizationStatus.Authorized:
                    InvokeOnMainThread(() =>
                    {

                        var imagePicker = new UIImagePickerController
                        {
                            SourceType = UIImagePickerControllerSourceType.Camera,
                            Delegate = this
                        };
                        PresentViewController(imagePicker, true, null);

                    });
                    break;

                case AVAuthorizationStatus.Denied:
                    InvokeOnMainThread(() =>
                    {
                        ShowMessage("Error", "FATAL ERROR x0002310", NavigationController);
                    });
                    break;

                case AVAuthorizationStatus.NotDetermined:

                    AVCaptureDevice.RequestAccessForMediaType(AVMediaType.Video, (bool access) => CheckCameraAuthorizationStatus(auth));
                    break;
                case AVAuthorizationStatus.Restricted:
                    InvokeOnMainThread(() =>
                    {
                        ShowMessage("Error", "FATAL ERROR x0002710", NavigationController);
                    });
                    break;

                default:
                    break;
            }
        }

        void InitializeComponents(){

            lblOprimale.Hidden = true;
            lblModifiquele.Hidden = true;

            arribaTapGesture = new UITapGestureRecognizer(ShowOptions) { Enabled = true };
            viewArriba.AddGestureRecognizer(arribaTapGesture);

            abajoTapGesture = new UITapGestureRecognizer(ShowOptions) { Enabled = true };
            viewAbajo.AddGestureRecognizer(abajoTapGesture);
        }

        [Export("imagePickerController:didFinishPickingMediaWithInfo:")]
        public void FinishedPickingMedia(UIImagePickerController picker, Foundation.NSDictionary info)
        {
            var image = info[UIImagePickerController.OriginalImage] as UIImage;
            imgArriba.Image = image;
            picker.DismissViewController(true,null);
        }

        [Export("imagePickerControllerDidCancel:")]
        public void Canceled(UIImagePickerController picker)
        {
            picker.DismissViewController(true,null);
        }


        void ShowMessage(string title,string message,UIViewController fromViewController){
            var alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
        }




    }
}
