using System;
using System.IO;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Views;

namespace Shuffle
{
    [Activity(Label = "Shuffle", MainLauncher = true,  Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        /// <summary>
        /// Called when the activity is starting
        /// </summary>
        /// <param name="savedInstanceState">The instance state</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            try
            {
                AssetManager assetManager = this.Assets;
                Shuffler game = new Shuffler(assetManager.List("Cards"));

                ImageButton cardViewer = FindViewById<ImageButton>(Resource.Id.CardViewer);
                cardViewer.Click += (o, e) =>
                {
                    string nextCard = game.NextCard();
                    if (nextCard != null)
                    {
                        UpdateImage(cardViewer, nextCard);
                    }
                    else
                    {
                        AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);
                        alertDialog.SetTitle("Shuffle");
                        alertDialog.SetMessage("No more cards. Restart?");
                        alertDialog.SetPositiveButton("OK", (senderAlert, args) => { UpdateImage(cardViewer, game.Start()); });
                        alertDialog.SetNegativeButton("Cancel", (senderAlert, args) => { });
                        alertDialog.Show();
                    }
                };
                UpdateImage(cardViewer, game.Start());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to perform setup for Kings. Exception = {0}", ex.Message);
            }
        }

        /// <summary>
        /// Updates the specified ImageButton with the specified image
        /// </summary>
        /// <param name="imageButton">ImageButton to update image for</param>
        /// <param name="image">Path to image under AssetManager</param>
        private void UpdateImage(ImageButton imageButton, string image)
        {
            AssetManager assetManager = this.Assets;
            Drawable currentImage = imageButton.Drawable;
            Drawable nextImage;
            using (Stream imageStream = assetManager.Open(Path.Combine("Cards", image)))
            {
                nextImage = Drawable.CreateFromStream(imageStream, null);
            }
            imageButton.SetImageDrawable(nextImage);
            currentImage?.Dispose();
        }
    }
}

