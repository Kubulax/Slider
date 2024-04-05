using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Slider
{
    public partial class MainPage : TabbedPage
    {
        List<Image> images = new List<Image>();
        private bool autoSlide = true;
        private bool autoSlideTimerRunning = false;

        public MainPage()
        {
            InitializeComponent();
            StartAutoSliding();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadImages();
        }

        private void LoadImages()
        {
            CarouselView_Images.ItemsSource = null;
            CarouselView_Images.ItemsSource = images;
        }

        private void AddImage(object sender, EventArgs e)
        {
            string imageName = Entry_ImageName.Text;
            string imageSource = Entry_ImageSource.Text;

            if (String.IsNullOrEmpty(imageName) || String.IsNullOrEmpty(imageSource))
            {
                DisplayAlert("Info", "Proszę wprowadzić dane", "OK");
            }
            else
            {
                var img = new Image()
                {
                    Name = imageName,
                    Source = imageSource
                };

                images.Add(img);
                LoadImages();

                Entry_ImageName.Text = string.Empty;
                Entry_ImageSource.Text = string.Empty;
            }
        }

        private void RemoveImage(object sender, EventArgs e)
        {
            var button = sender as Button;
            var image = (Image)button.BindingContext;

            if (image != null)
            {
                images.Remove(image);
                LoadImages();
            }
        }

        private void ToggleAutoSliding(object sender, EventArgs e)
        {
            autoSlide = !autoSlide;

            if (autoSlide)
            {
                Button_ToggleAutoSliding.Text = "Stop Auto Slide";
                StartAutoSliding();
            }
            else
            {
                Button_ToggleAutoSliding.Text = "Start Auto Slide";
                autoSlideTimerRunning = false;
            }
        }

        private void StartAutoSliding()
        {
            if (!autoSlideTimerRunning && images.Count != 0)
            {
                Device.StartTimer(TimeSpan.FromSeconds(3), () =>
                {
                    if (!autoSlide)
                    {
                        autoSlideTimerRunning = false;
                        return false;
                    }

                    CarouselView_Images.Position = (CarouselView_Images.Position + 1) % images.Count;
                    autoSlideTimerRunning = true;
                    return true;
                });
            }
        }
    }
}
