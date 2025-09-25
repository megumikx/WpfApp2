using System.IO;
using System.IO.Compression;
using System.Windows;
using Microsoft.Win32;

namespace SimpleCompressor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Compress_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                await CompressAsync(dialog.FileName, dialog.FileName + ".gz");
            }
        }

        private async void Decompress_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                await DecompressAsync(dialog.FileName, dialog.FileName + ".decompressed");
            }
        }

        async Task CompressAsync(string sourceFile, string compressedFile)
        {
            using FileStream sourceStream = new FileStream(sourceFile, FileMode.Open);
            using FileStream targetStream = File.Create(compressedFile);
            using GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress);
            
            await sourceStream.CopyToAsync(compressionStream);
            pbProgress.Value = 100;
        }

        async Task DecompressAsync(string compressedFile, string targetFile)
        {
            using FileStream sourceStream = new FileStream(compressedFile, FileMode.Open);
            using FileStream targetStream = File.Create(targetFile);
            using GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress);
            
            await decompressionStream.CopyToAsync(targetStream);
            pbProgress.Value = 100;
        }
    }
}