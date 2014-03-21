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
        image_as_tab a;

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
            kolor = new image_RGB(sourcePixels, w, h);
            szary = new image_Gray(sourcePixels, w, h);
            
            
            
        }
        
        private async void bitmpe(byte[] tablica,image_as_tab obiekt)
        {
            WriteableBitmap writeableBitmap = new WriteableBitmap((int)obiekt.w, (int)obiekt.h);
            using (Stream stream = writeableBitmap.PixelBuffer.AsStream())
            {
                await stream.WriteAsync(tablica, 0, tablica.Length);
            }
            this.im_effect.Source = writeableBitmap;
        }

        private void _try_Click(object sender, RoutedEventArgs e)
        {

            kolor = new image_RGB(sourcePixels, w, h);
            szary = new image_Gray(sourcePixels,w,h);
            NormalizeImage normalizacja = new NormalizeImage();
            a = kolor;
            normalizacja.NormalizeAll(a);
            bitmpe(kolor.show(), kolor);
            //SwitchEvent();
            
        }

        private void SwitchEvent()
        {
            switch (Event)
            {
                case 1:

                   info.Text = "Change corolfull image to sepia colors. Sepia factor  20 ";
                    if (kolor != null)
                    {
                        kolor.sepia(20);
                        bitmpe(kolor.show(), kolor);
                        szary = null;
                    }
                        //Nunit memento visitor
                    else if (szary != null)
                    {
                        info.Text = "Gray image and sepia dont work";

                    }
                    else
                        info.Text = "image empty";
                    break;

                //moze byc warunek

                case 2:

                    info.Text = "Change corolfull image to grayscale with natural luminaces algorithm";
                    if (kolor != null)
                    {
                        szary = kolor.grey_naturalimage();
                        kolor = null;
                        bitmpe(szary.show(), szary);
                    }
                    else if (szary != null)
                    {
                        info.Text = "image is grey!";
                    }
                    else
                        info.Text = "image empty";




                    ///mozebyc warunek
                    break;
                case 3:
                    info.Text = "Histogram Eqaliztaion increases the global contrast of many images. Histogram of the color is distributed for all intensities";
                    //normalizacja na szarym!
                    if (kolor != null)
                    {
                        kolor.normalize();
                        bitmpe(kolor.show(), kolor);
                        szary = null;
                    }
                    else if (szary != null)
                    {
                        szary.normalize();
                        ///warunek
                        bitmpe(szary.show(), szary);
                        kolor = null;
                    }
                    else
                        info.Text = "image empty";




                    break;
                case 4:
                    info.Text = "Change corofull image to grayscale with avrage algorithm";
                    //moze byc warunek
                    if (kolor != null)
                    {
                        szary = kolor.greyimage();
                        kolor = null;
                        bitmpe(szary.show(), szary);
                    }
                    else if (szary != null)
                    {
                        info.Text = "image is grey!";
                        bitmpe(szary.show(), szary);
                    }
                    else
                        info.Text = "image empty";


                    break;

                case 5:
                    info.Text = "Change image to negative image";
                    if (kolor != null)
                    {
                        kolor.negative();
                        bitmpe(kolor.show(), kolor);
                    }
                    //warunek
                    else if (szary != null)
                    {
                        szary.negative();
                        bitmpe(szary.show(), szary);
                    }
                    else
                        info.Text = "image empty";

                    break;
                case 6:
                    info.Text = "Roberts cross is  differential operator, its  approximate the gradient of an image for edage detection. Sensitivity to noise";
                    if (kolor != null)
                    {
                        image_RGB temp = kolor.Roberts();
                        bitmpe(temp.show(), temp);
                    }
                    //warunek
                    else if (szary != null)
                    {
                        //szary.Roberts();
                        bitmpe(szary.show(), szary);
                    }
                    else
                        info.Text = "There is no image";


                    break;
                case 7:
                    info.Text = " Sobel is differential operator, its  approximate the gradient of an image for edage detection.Less sensitive to isolated high intensity";
                    if (kolor != null)
                    {
                        kolor.sobel();
                        bitmpe(kolor.show(), kolor);
                    }
                    //warunek
                    else if (szary != null)
                    {
                        //szary.sobel();
                        bitmpe(szary.show(), szary);
                    }
                    else
                        info.Text = "There is no image";


                    break;
                case 8:
                    info.Text = "Change corolfull image to sepia colors. Sepia factor  40";
                    if (kolor != null)
                    {
                        kolor.sepia(40);
                        bitmpe(kolor.show(), kolor);
                    }
                    //warunek
                    else if (szary != null)
                    {
                        //szary.sobel();
                        // bitmpe(szary.show(), szary);
                    }
                    else
                        info.Text = "There is no image";


                    break;


                default:
                    info.Text = "Nothing was selected";
                    //bitmpe(kolor.show());
                    break;
            }

            //sourcePixels =(byte[]) a.utab.Clone();
            //this.obrazek.Source = this.im_effect.Source;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {

            SwitchEvent();

            if (kolor != null)
            {
                this.obrazek.Source = this.im_effect.Source;
               

                sourcePixels = (byte[])kolor.utab.Clone();
                
                info.Text = "Image changed!";
                


            }
            else if (szary!=null)
                {
                this.obrazek.Source = this.im_effect.Source;
                double c = 0;
                for (int i = 0; i < szary.utab.Length; i = i + 4)
                {
                    c += (Math.Abs(szary.utab[i] - szary.utab[i + 1]) + Math.Abs(szary.utab[i] - szary.utab[i + 2]) + Math.Abs(szary.utab[i + 1] - szary.utab[i + 2]));
                }
                if (c == 0.0)
                {

                    sourcePixels =(byte[]) szary.utab.Clone();
                    info.Text = "Image changed!";
                }

     
                
                
                


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

        private void roberts_GotFocus(object sender, RoutedEventArgs e)
        {
            Event = 6;
        }

        private void sobel_GotFocus(object sender, RoutedEventArgs e)
        {
            Event = 7;
        }

        private void AppBar_Opened(object sender, object e)
        {
            if (!histapp.IsEnabled)
            {
                if (histapp.IsOpen)
                    histapp.IsOpen = false;
            }
        }

        private void buttonhist_Click(object sender, RoutedEventArgs e)
        {
            if (kolor != null)
            {
                var hist = kolor.histogram();
                bitmpe(hist.utab, hist);
            }
            else if (szary != null)
            {
                var hist = szary.histogram();
                bitmpe(hist.utab, hist);
            }
        }

        private void sepia40_GotFocus(object sender, RoutedEventArgs e)
        {
            Event = 8;
        }

        
        }
    
    }
        


            
    



