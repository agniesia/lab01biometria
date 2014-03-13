using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.System.Threading;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace lab01biometria
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        IRandomAccessStream fileStream; // Wczytanie pliku do strumienia
        Guid decoderId;
        
        byte[] sourcePixels;
        BitmapDecoder decoder;
        int w = 0;
        int h = 0;
        private async void wczytajimage(object sender, RoutedEventArgs e)
        {
            FileOpenPicker FOP = new FileOpenPicker(); // Klasa okna wybierania pliku
            FOP.ViewMode = PickerViewMode.Thumbnail; // Rodzaj podglądu plików w oknie - tu jako małe obrazy
            FOP.SuggestedStartLocation = PickerLocationId.PicturesLibrary; // Od jakiego katalogu okno powinno zacząć wyświetlanie
            FOP.FileTypeFilter.Add(".bmp"); // filtry, które informują jakie rozszerzenia można wybrać
            FOP.FileTypeFilter.Add(".jpg");
            FOP.FileTypeFilter.Add(".jpeg");
            FOP.FileTypeFilter.Add(".png");
            FOP.FileTypeFilter.Add(".gif");
            StorageFile file = await FOP.PickSingleFileAsync();
            // Uruchomienie wybierania pliku pojedynczego
            
            if (file != null)
            {
                    fileStream = await file.OpenAsync(FileAccessMode.Read);
                     // Dekoder będzie potrzebny później przy pracy na obrazie
                    BitmapImage bitmapImage = new BitmapImage(); // Stworzenie obiektu obrazu do wyświetlenia
                    bitmapImage.SetSource(fileStream); // Przepisanie obrazu ze strumienia do obiektu obrazu przez wartosc
                    this.obrazek.Source = bitmapImage; // Przypisanie obiektu obrazu do elementu interfejsu typu "Image" o nazwie "Oryginał"
                    // Poniżej znajduje się zapamiętanie dekodera
                    w=bitmapImage.PixelWidth;
                    h=bitmapImage.PixelHeight;
                    
                    switch (file.FileType.ToLower())
                    {
                    case ".jpg":
                    case ".jpeg":
                    decoderId = BitmapDecoder.JpegDecoderId;
                    break;
                    case ".bmp":
                    decoderId = BitmapDecoder.BmpDecoderId;
                    break;
                    case ".png":
                    decoderId = BitmapDecoder.PngDecoderId;
                    break;
                    case ".gif":
                    decoderId = BitmapDecoder.GifDecoderId;
                    break;
                    default:
                    return;}
            }
            decoder = await BitmapDecoder.CreateAsync(decoderId, fileStream); // Dekodowanie strumienia za pomocą dekodera
            // Dekodowanie strumienia do klasy z informacjami o jego parametrach
            PixelDataProvider pixelData = await decoder.GetPixelDataAsync(
            BitmapPixelFormat.Bgra8,// Warto tu zwrócić uwagę jak przechowywane są kolory!!!
            BitmapAlphaMode.Straight,
            new BitmapTransform(),
            ExifOrientationMode.IgnoreExifOrientation,
            ColorManagementMode.DoNotColorManage
            );
            
            sourcePixels=pixelData.DetachPixelData();
            
            
            
        }

        private async void bitmpe(byte[] tablica)
        {
            WriteableBitmap writeableBitmap = new WriteableBitmap((int)decoder.PixelWidth, (int)decoder.PixelHeight);
            using (Stream stream = writeableBitmap.PixelBuffer.AsStream())
            {
                await stream.WriteAsync(tablica, 0, tablica.Length);
            }
            this.im_effect.Source = writeableBitmap;
        }

        private async void _try_Click(object sender, RoutedEventArgs e)
        {
            
            
            image_RGB a = new image_RGB(sourcePixels,w,h);
            image_Gray temp = new image_Gray();


            await ThreadPool.RunAsync(
new WorkItemHandler(
(IAsyncAction action) =>
{   a.Sobel();
    
}
)
); 
            //temp = a.grey_naturalimage();
            //bitmpe(temp.utab);
            bitmpe(a.imagearray3Dto1D());
             
            if (normalize.IsSelected)
                info.Text = "1";
            else if (grey.IsSelected)
                info.Text = "3";
            else if (natural_grey.IsSelected)
                info.Text = "4";
            else if (sepia.IsSelected)
                info.Text = "5";
            else if (roberts.IsSelected)
                info.Text = "6";
            else if (sobel.IsSelected)
                info.Text = "7";
            else if (negative.IsSelected)
                info.Text = w.ToString();
                 
                 
            else
                info.Text = "nothing selected";
            //bitmpe(temp.utab);
            
            //sourcePixels =(byte[]) a.utab.Clone();
            //this.obrazek.Source = this.im_effect.Source;
            
        }

           

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            _try_Click(sender, e);

            //this.obrazek.Source = this.im_effect.Source;




            
                
        }
        }
    
    }
        


            
    



