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
            IRandomAccessStream fileStream = null; // Wczytanie pliku do strumienia
            Guid decoderId;
            // Uruchomienie wybierania pliku pojedynczego
            if (file != null)
            {
             // Dekoder będzie potrzebny później przy pracy na obrazie
            BitmapImage bitmapImage = new BitmapImage(); // Stworzenie obiektu obrazu do wyświetlenia
            bitmapImage.SetSource(fileStream); // Przepisanie obrazu ze strumienia do obiektu obrazu przez wartosc
            this.obrazek.Source = bitmapImage; // Przypisanie obiektu obrazu do elementu interfejsu typu "Image" o nazwie "Oryginał"
            // Poniżej znajduje się zapamiętanie dekodera
            
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
            return;
            }
            }
                    }
                }
}
