using System;
namespace Xamarin.Forms.StateSquid
{
    public class SkeletonView : BoxView
    {
        public SkeletonView()
        {
            var smoothAnimation = new Animation();

            smoothAnimation.WithConcurrent((f) => this.Opacity = f, 0.2, 1, Xamarin.Forms.Easing.Linear);
            smoothAnimation.WithConcurrent((f) => this.Opacity = f, 1, 0.2, Xamarin.Forms.Easing.Linear);

            this.Animate("FadeInOut", smoothAnimation, 16, 1000, Easing.Linear, null, () => true);
        }
    }
}
