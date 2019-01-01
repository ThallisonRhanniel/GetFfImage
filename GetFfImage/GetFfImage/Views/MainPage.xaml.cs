using System;
using System.IO;
using System.Threading.Tasks;
using FFImageLoading.Forms;
using PCLStorage;
using Xamarin.Forms;

namespace GetFfImage.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            XamarinCachedImage.Success += XamarinCachedImage_Success;
        }

        private async void XamarinCachedImage_Success(object sender, FFImageLoading.Forms.CachedImageEvents.SuccessEventArgs e)
        {
            //Imagem modificada 
            var modifiedImage = await XamarinCachedImage.GetImageAsPngAsync();
            //Imagem Salva no dispositivo
            await SaveImage(modifiedImage, "image.jpg");
            
            //Carregando imagem do dispositivo
            var imageSaved = await LoadImage("image.jpg");
            //carregando a imagem do dispositivo na tela
            XamarinCachedImageSaved.Source = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(imageSaved));
        }

        private static async Task SaveImage(byte[] imagem, string fileName, IFolder folderRoot = null)
        {
            IFolder folder = folderRoot ?? FileSystem.Current.LocalStorage;
            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            using (System.IO.Stream stream = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
            {
                stream.Write(imagem, 0, imagem.Length);
            }
        }

        private static async Task<byte[]> LoadImage(string fileName, IFolder folderRoot = null)
        {
            IFolder folder = folderRoot ?? FileSystem.Current.LocalStorage;
            IFile file = await folder.GetFileAsync(fileName);
            using (System.IO.Stream stream = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
            {
                long length = stream.Length;
                byte[] streamBuffer = new byte[length];
                stream.Read(streamBuffer, 0, (int)length);
                return streamBuffer;
            }
        }
        
    }
}