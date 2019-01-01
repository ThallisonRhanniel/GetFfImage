using Prism.Commands;
using Prism.Navigation;
using System;
using FFImageLoading.Forms;

namespace GetFfImage.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public DelegateCommand<CachedImage> ImageCommand { get; set; }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";

            ImageCommand = new DelegateCommand<CachedImage>(async cachedImage =>
            {
                var imageBase64 = Convert.ToBase64String(await cachedImage.GetImageAsJpgAsync());
            });
        }
    }
}
