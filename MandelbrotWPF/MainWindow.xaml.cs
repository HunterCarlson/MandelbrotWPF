using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace MandelbrotWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Draw(Image);
        }

        private static void Draw(Image image)
        {
            var mandelbrot = new Mandelbrot();

            const int maxIterations = 100;
            var pxSize = 800;

            WriteableBitmap writeableBitmap = BitmapFactory.New(pxSize, pxSize);

            image.Source = writeableBitmap;

            int[][] iterations = mandelbrot.Generate(800, maxIterations);

            double hueStep = 36;
            double hueOffset = 0;

            using (writeableBitmap.GetBitmapContext())
            {
                for (int i = 0; i < pxSize; i++)
                {
                    for (int j = 0; j < pxSize; j++)
                    {
                        Color color;

                        if (iterations[i][j] == maxIterations || iterations[i][j] == 0)
                        {
                            color = Colors.Black;
                        }
                        else
                        {
                            double hue = (hueStep * (iterations[i][j] - 1) + hueOffset) % 360;
                            ColorConverter.HsvToRgb(hue, 1, 1, out int r, out int g, out int b);
                            color = Color.FromRgb((byte) r, (byte)g, (byte)b);
                        }

                        writeableBitmap.SetPixel(i, j, color);
                    }
                }
            }

        }
    }
}
