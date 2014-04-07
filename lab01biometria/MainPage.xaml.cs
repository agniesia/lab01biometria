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
using lab01biometria.Memento;



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
            przyciskiEnabled();
            info.Text = "load image to work";

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e){}
        private void przyciskiEnabled()
        {

            color.IsEnabled = false;
            noise.IsEnabled = false;
            Binoperation.IsEnabled = false;
            resize.IsEnabled = false;
            filters.IsEnabled = false;
            binaryzation.IsEnabled = false;
            Aply.IsEnabled = false;
            OK.IsEnabled = false;
            histogram.IsEnabled = false;
            



        }
        private void przyciskiVisible()
        {
            OK.IsEnabled = true;
            Aply.IsEnabled = true;
            color.IsEnabled = true;
            noise.IsEnabled = true;
            resize.IsEnabled = true;
            filters.IsEnabled = true;
            histogram.IsEnabled = true;
        }

        IRandomAccessStream fileStream; // Wczytanie pliku do strumienia
        Guid decoderId;
        byte[] sourcePixels;
        BitmapDecoder decoder;
        int w = 0;
        int h = 0;
        Visitor operatio;
        imageoperation.RGBtoGrey x;
        imageoperation.RGBtoNaturalGrey y;
        image_as_tab imagetowork;
        List<int> Lista = new List<int>();
        string opis = "";

        private async void wczytajimage(object sender, RoutedEventArgs e)
        {
            przyciskiEnabled();
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
                przyciskiVisible();

                fileStream = await file.OpenAsync(FileAccessMode.Read);
                // Dekoder będzie potrzebny później przy pracy na obrazie
                BitmapImage bitmapImage = new BitmapImage(); // Stworzenie obiektu obrazu do wyświetlenia
                bitmapImage.SetSource(fileStream); // Przepisanie obrazu ze strumienia do obiektu obrazu przez wartosc
                this.Show.Source = bitmapImage; // Przypisanie obiektu obrazu do elementu interfejsu typu "Image" o nazwie "Oryginał"
                // Poniżej znajduje się zapamiętanie dekodera
                w = bitmapImage.PixelWidth;
                h = bitmapImage.PixelHeight;

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

                decoder = await BitmapDecoder.CreateAsync(decoderId, fileStream); // Dekodowanie strumienia za pomocą dekodera
                // Dekodowanie strumienia do klasy z informacjami o jego parametrach
                PixelDataProvider pixelData = await decoder.GetPixelDataAsync(
                BitmapPixelFormat.Bgra8,// Warto tu zwrócić uwagę jak przechowywane są kolory!!!
                BitmapAlphaMode.Straight,
                new BitmapTransform(),
                ExifOrientationMode.IgnoreExifOrientation,
                ColorManagementMode.DoNotColorManage
                );

                sourcePixels = pixelData.DetachPixelData();
                byte[] RCanal = sourcePixels.Where((t, i) => i % 4 == 0).ToArray();
                byte[] BCanal = sourcePixels.Where((t, i) => i % 4 == 1).ToArray();
                byte[] GCanal = sourcePixels.Where((t, i) => i % 4 == 2).ToArray();
                var suma = 0;
                for (int i = 0; i < RCanal.Length; i++)
                {

                    suma += Math.Abs(RCanal[i] - BCanal[i]) + Math.Abs(RCanal[i] - RCanal[i]) + Math.Abs(BCanal[i] - RCanal[i]);


                }
                if (suma != 0)
                {
                    imagetowork = new image_RGB(sourcePixels, w, h);

                }
                else
                {
                    imagetowork = new image_Gray(sourcePixels, w, h);
                }
                change.Source =null;
                y = null;
                operatio = null;
                x = null;
            }
        }
       

        
        private async void bitmpe(image_as_tab obiekt)
        {
            WriteableBitmap writeableBitmap = new WriteableBitmap((int)obiekt.w, (int)obiekt.h);
            using (Stream stream = writeableBitmap.PixelBuffer.AsStream())
            {
                await stream.WriteAsync(obiekt.show(), 0, obiekt.show().Length);
            }
            this.change.Source = writeableBitmap;
            
        }

        private async void _try_Click(object sender, RoutedEventArgs e)
        {

            Originator org = new Originator();
            Caretaker caretaker = new Caretaker();

            org.State = imagetowork;
            caretaker.Memento = org.SaveMemento();

            try
            {
                if (operatio.GetType() == grey.GetType())
                {
                    await ThreadPool.RunAsync(new WorkItemHandler((IAsyncAction action) =>
                    {
                        grey.rob(imagetowork);
                    }));
                    
                    imagetowork = grey.GreyElement.copy();
                }

                else if (operatio.GetType() == greyn.GetType())
                {
                    await ThreadPool.RunAsync(new WorkItemHandler((IAsyncAction action) =>{
                    greyn.rob(imagetowork);}));
                    
                    imagetowork = greyn.GreyElement.copy();
                }
                else
                {
                    await ThreadPool.RunAsync(new WorkItemHandler((IAsyncAction action) =>{
                    operatio.rob(imagetowork);}));
                    
                    
                }
                info.Text = opis;
                bitmpe(imagetowork);
                org.RestoreMemento(caretaker.Memento);
                imagetowork = org.State;
            }
            catch (System.NullReferenceException t)
            {
                info.Text = "The operation faild, check image operation again and confirm";
            }

        }
        
        imageoperation.RGBtoGrey grey = new imageoperation.RGBtoGrey();
        imageoperation.RGBtoNaturalGrey greyn = new imageoperation.RGBtoNaturalGrey();
        private async void OK_Click(object sender, RoutedEventArgs e)
        {



            try
            {
                if (operatio.GetType() == grey.GetType())
                {
                    await ThreadPool.RunAsync(new WorkItemHandler((IAsyncAction action) =>
                    {
                        grey.rob(imagetowork);
                    }));
                    
                    imagetowork = grey.GreyElement.copy();
                    binaryzation.IsEnabled = true;
                }

                else if (operatio.GetType() == greyn.GetType())
                {
                    await ThreadPool.RunAsync(new WorkItemHandler((IAsyncAction action) =>
                    {
                        greyn.rob(imagetowork);
                    }));
                    
                    imagetowork = greyn.GreyElement.copy();
                    binaryzation.IsEnabled = true;
                }
                else
                {
                    await ThreadPool.RunAsync(new WorkItemHandler((IAsyncAction action) =>
                    {
                        operatio.rob(imagetowork);
                    }));
                }
                info.Text = opis;
                bitmpe(imagetowork);
                Show.Source = change.Source;
                operatio = null;
            }
            catch (System.NullReferenceException t)
            {
                info.Text = "The operation faild";
                info.Text = "check image operation again and confirm";
            }

            

           

        }

        private void oki_Click(object sender, RoutedEventArgs e)
        {

            int a=docoloroperation.SelectedIndex;
            
            switch (a)
            {
                case 0:
                    operatio = new imageoperation.NormalizeImage();
                    opis = "Histogram Eqaliztaion increases the global contrast of many images. Histogram of the color is distributed for all intensities";
                    break;
                case 1:
                     
                    operatio = new imageoperation.RGBtoGrey();
                    opis = "Change corofull image to grayscale with avrage algorithm";
                    break;
                case 2:
                    operatio= new imageoperation.RGBtoNaturalGrey();
                    opis="Change corolfull image to grayscale with natural luminaces algorithm";
                    break;
                case 3:
                    operatio = new imageoperation.Negative();
                    opis="Change image to negative image";
                    break;
                case 4:
                    int waga = (int)wagasepia.Value;
                    operatio = new imageoperation.Sepia(waga);
                    opis = "Change corolfull image to sepia colors. Sepia factor  20 ";
                    break;
            }

            docoloroperation.SelectedIndex = -1;
            color.Flyout.Hide();
            
        }

        private void noise_Click(object sender, RoutedEventArgs e)
        {
            int a = noiselista.SelectedIndex;
            int chance =(int) power.Value;
            byte xa = (byte)Intensity.Value;
            byte ya = (byte)(xa / 2);
            switch (a)
            {
                case 0:
                    operatio = new imageoperation.NoiseGeneratorSaltPeper(chance);
                    opis = "Generate artificial noise type of Salt and paper with power correspond to probability ";
                    break;
                case 1:
                    operatio = new imageoperation.NoiseGeneratorUniformOneCalanl(chance,ya ,xa);
                    opis = "Generate artificial noise type of Unitary noise with power correspond to probability and range correspond to change of pixel value. The noise id the same for every canal in RGB images. ";
                    break;
                case 2:
                    operatio = new imageoperation.NoiseGeneratorUniformDiffCanal(chance,ya ,xa);
                    opis = "Generate artificial noise type of Unitary noise with power correspond to probability and range correspond to change of pixel value. Noise is different for every canal in RGB images. Noise only for RGB images. ";
                    break;
            }

            noiselista.SelectedIndex = -1;
            noise.Flyout.Hide();
        }

        private void okeadgetetction_Click(object sender, RoutedEventArgs e)
        {
            int a = edgelist.SelectedIndex;
            switch(a){
                case 0:
                    operatio = new imageoperation.Roberts();
                    opis = "Roberts cross is  differential operator, its  approximate the gradient of an image for edage detection. Sensitivity to noise";
                    break;
                case 1:
                    operatio = new imageoperation.Sobel();
                    opis=" Sobel is differential operator, its  approximate the gradient of an image for edage detection.Less sensitive to isolated high intensity";
                    break;

            }
            egdedetection.Flyout.Hide();
            edgelist.SelectedIndex = -1;
        }

        private void okfilter_Click(object sender, RoutedEventArgs e)
        {
            var a = filterslist.SelectedIndex;
            var Sigma = sigma.Value;
            var Rozm = (int)rozmiarfilter.Value+1;
            
            switch (a)
            {
                case 0:
                    operatio = new imageoperation.MedianFilter(Rozm);
                    opis = " The median filter  often used to remove noise.";
                    break;
                case 1:
                    operatio = new imageoperation.MedianFilterBetter(Rozm);
                    opis = " The median filter  often used to remove noise, with extra pixel value control.";
                    break;
                case 2:
                    operatio = new imageoperation.KuwaharaFilter(Rozm);
                    opis=" Kuwahara filter is able to apply smoothing on the image while preserving the edges.";
                    break;
                case 3:
                    operatio = new imageoperation.GaussFilter(Rozm, Sigma);
                    opis = "Gaussian filter is able to apply smoothing with Gaussian probablity. ";
                    break;
            }

            advence.Hide();

            filterslist.SelectedIndex = -1;

        }

        private void oksharpen_Click(object sender, RoutedEventArgs e)
        {
            var a = Listasharpensmooth.SelectedIndex;
            int rozmiar = (int)rozmmaski.Value+1;
            
            switch (a)
            {
                case 0:
                    operatio = new MeanFiltersharpen5();
                    opis = "Sharpen I filter is able to apply sharpen images with small power ";
                    break;
                case 1:
                    operatio = new MeanFilterSharpen9();
                    opis = "Sharpen I filter is able to apply sharpen images with medium power ";
                    break;
                case 2:
                    operatio = new MeanFilteSharpen5and2();
                    opis = "Sharpen I filter is able to apply sharpen images with big power. ";
                    break;
                case 3:
                    operatio = new MeanFilterSmooth2();
                    opis = "Smooth I filter is able to apply smoothing images with small power ";
                    break;
                case 4:
                    operatio = new MeanFilterSmooth4();
                    opis = "Smooth I filter is able to apply smoothing images with big power ";
                    break;
                case 5:
                    operatio = new MeanFilterSmooth1(rozmiar);
                    opis = "Smooth I filter is able to apply smoothing images with the biggest power. The size corresponde to power of smoothing. ";
                    break;

            }
            Listasharpensmooth.SelectedIndex = -1;
            sharpenflyout.Hide();

        }

        private void okbinary_Click(object sender, RoutedEventArgs e)
        {
            var a = binarylist.SelectedIndex;
            var roz = (int)powerslider.Value+1;
            var odchylenie = (int)Softsider.Value;
            switch (a)
            {
                case 0:
                    operatio = new imageoperation.ThresholdingGlobal();
                    opis = "Global binarization is thresolding with global mean.It is good for one element image";
                    break;
                case 1:
                    operatio=new imageoperation.BinaryLocalMean(roz);
                    opis = "Local binarization is thresolding with local mean.It is good for few big elements image";

                    break;
                case 2:
                    operatio = new imageoperation.BinaryLocalGlobal(roz, odchylenie);
                    opis = "Mix binarization is thresolding with global mean.It is good for few elements image with global control.";
                    break;
                case 3:
                    operatio = new imageoperation.Bernsen(roz,odchylenie);
                    opis = "Bersen binarization is thresolding with local intensity mean.It is good for few elements image with better control for object range.";
                    break;
                case 4:
                    operatio = new imageoperation.Otsu();
                    opis = "Otsu binarization is thresolding with histogram  level";
                    break;
            }

            binaryflyout.Hide();
            binarylist.SelectedIndex = -1;
            Binoperation.IsEnabled = true;
        }

        private void okzoom_Click(object sender, RoutedEventArgs e)
        {
            var Zoom = powerzoom.Value;
            int a = skalowanielist.SelectedIndex;
            int kat =(int) rarte.Value;
            switch (a)
            {
                case 0:
                    operatio = new imageoperation.Skalowanie(Zoom);
                    opis = " Fast and inaccurate zooming";
                    break;
                case 1:
                    operatio =  new imageoperation.Skalowanie(Zoom);
                    opis = "Zooming with colors interpolation";
                    break;
                case 2:
                    Zoom = 1 / Zoom;
                    operatio = new imageoperation.Skalowanie(Zoom);
                    opis = " Fast and inaccurate unzooming";
                    break;
                case 3:
                    Zoom =1/Zoom;
                    operatio = new imageoperation.ScaleMean(Zoom);
                    opis = " Fast and inaccurate zooming with smoothig";
                    break;
                case 4:
                    operatio = new imageoperation.Roate(kat);
                    opis = " Rotate of image with angle beetwen 0-90 deegres";
                    
                    break;

            }
            resizeflyout.Hide();
            skalowanielist.SelectedIndex = -1;
        }

        private void binoperationok_Click(object sender, RoutedEventArgs e)
        {
            var a=binoperationlist.SelectedIndex;
            switch (a)
            {
                case 0:
                    operatio=new  Binaryoperation.Skeleton();
                    opis = " One pixel thinning";
                    break;
                case 1:
                    operatio = new Binaryoperation.Segmentation();
                    opis = "Sgementation finde object at image";
                    break;

            }
            binoperatinflyout.Hide();
            binoperationlist.SelectedIndex = -1;

        }

        private void histogram_Click(object sender, RoutedEventArgs e)
        {   imageoperation.Histogram operacja = new imageoperation.Histogram();
            operacja.rob(imagetowork);
            bitmpe(operacja.HistogramObject);
        }

        

        

        

       
        
       

        

       
        
        }
    
    }
        


            
    



