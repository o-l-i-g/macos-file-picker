using LukeMauiFilePicker;
using MauiApp1.Extensions;
using System.Diagnostics;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Loaded += (sender, args) =>
            {
                DropArea.RegisterDrop(Handler?.MauiContext, HandleCollectionViewDrop);
            };

        }

        private async Task HandleCollectionViewDrop(List<string> list)
        {
            await Console.Out.WriteLineAsync("Drop detected");

            foreach (var item in list)
            {
                Debug.WriteLine(item);
                await Console.Out.WriteLineAsync(item);
            }
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            // Assume this method is invoked from UI thread

            var newFilePicker = App.Current.MainPage!.Handler!.MauiContext!.Services.GetRequiredService<IFilePickerService>();

            // Microsoft's file picker is broken on macos
            //FileResult? selectedFile = await FilePicker.Default.PickAsync();

            IPickFile? selectedFile = await newFilePicker.PickFileAsync("Pick a file", null);

            if (selectedFile is null) // for MacOS this is always null. Verified working on Windows.
            {
                TextToUpdate.Text = "No file was selected.";
                return;
            }

            if (selectedFile.FileResult is null)
            {
                TextToUpdate.Text = $"Selected file name: {selectedFile.FileName} but file result was null";
                return;
            }

            // this library works to this point -- but doesn't return the full path which makes it useless
            TextToUpdate.Text = $"Selected file path: {selectedFile.FileResult.FullPath}";
        }

        private void DropGestureRecognizer_Drop(object sender, DropEventArgs e)
        {
            Console.WriteLine($"Drop recognised");
            Console.WriteLine($"Sender: {sender.ToString()}");
            Console.WriteLine($"Properties count: {e.Data.Properties.Count} ");
        }

        private void DropGestureRecognizer_DragOver(object sender, DragEventArgs e)
        {
            Console.WriteLine($"Drag over");
            //PerformWindowsDrop(e);
            //Console.WriteLine($"{JsonSerializer.Serialize(e.Data)}");
            //if (e.Data.View.Contains(StandardDataFormats.StorageItems))
            //{
            //    var deferral = e.GetDeferral();
            //    var extensions = new List<string> { ".json" };
            //    var isAllowed = false;
            //    var items = await e.DataView.GetStorageItemsAsync();
            //    foreach (var item in items)
            //    {
            //        if (item is StorageFile file && extensions.Contains(file.FileType))
            //        {
            //            isAllowed = true;
            //            break;
            //        }
            //    }

            //    e.AcceptedOperation = isAllowed ? DataPackageOperation.Copy : DataPackageOperation.None;
            //    deferral.Complete();
            //}

            //e.AcceptedOperation = DataPackageOperation.None;
        }

    }

}
