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
        byte Event = 0;
        int w = 0;
        int h = 0;
        image_RGB kolor;
        image_Gray szary;
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

        private void _try_Click(object sender, RoutedEventArgs e)
        {

            kolor = new image_RGB(sourcePixels, w, h);
            szary = new image_Gray(sourcePixels);
            
            switch (Event)
            {
                case 1:
                    info.Text = "Change corolfull image to sepia colors. Sepia factor belongs to the interval from 20 to 40";
                    kolor.sepia(20);
                    bitmpe(kolor.show());
                    break;
                    //moze byc warunek

                case 2:

                    info.Text = "Change corolfull image to grayscale with natural luminaces algorithm";
                    szary = kolor.grey_naturalimage();
                    bitmpe(szary.show());
                    ///mozebyc warunek
                    break;
                case 3:
                    info.Text = "Histogram Eqaliztaion increases the global contrast of many images. Histogram of the color is distributed for all intensities";
                    //normalizacja na szarym!
                    kolor.normalize();
                    bitmpe(kolor.show());
                    szary.normalize();
                    ///warunek
                    //bitmpe(szary.show());

                    break;
                case 4:
                    info.Text = "Change corofull image to grayscale with avrage algorithm";
                    //moze byc warunek
                    szary = kolor.greyimage();
                    bitmpe(szary.show());

                    break;

                case 5:
                    info.Text = "Change image to negative image";
                    kolor.negative();
                    bitmpe(kolor.show());
                    //warunek
                    szary.negative();
                    //bitmpe(szary.show());

                    break;


                default:
                    info.Text = "Nothing was selected";
                    bitmpe(kolor.show());
                    break;
            }
            
            //sourcePixels =(byte[]) a.utab.Clone();
            //this.obrazek.Source = this.im_effect.Source;
            
        }

           

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            _try_Click(sender, e);

            if (kolor != null)
            {
                this.obrazek.Source = this.im_effect.Source;
                double c = 0;
                for (int i = 0; i < kolor.utab.Length; i = i + 4)
                {
                    c += (Math.Abs(kolor.utab[i] - kolor.utab[i + 1]) + Math.Abs(kolor.utab[i] - kolor.utab[i + 2]) + Math.Abs(kolor.utab[i + 1] - kolor.utab[i + 2]));
                }
                if (c == 0.0)
                {

                    sourcePixels = szary.utab;
                    return;
                }

                sourcePixels = kolor.utab;
            }
            else
                info.Text = "object is null exeption needed";






            
                
        }

        private void sepia_GotFocus(object sender, RoutedEventArgs e)
        {
            Event = 1;
        }

        private void natural_grey_GotFocus(object sender, RoutedEventArgs e)
        {
            Event = 2;
        }

        private void normalize_GotFocus(object sender, RoutedEventArgs e)
        {
            Event = 3;
        }

        private void grey_GotFocus(object sender, RoutedEventArgs e)
        {
            Event = 4;
        }

        private void negative_GotFocus(object sender, RoutedEventArgs e)
        {
            Event = 5;
        }

        
        }
    
    }
        


            
    



