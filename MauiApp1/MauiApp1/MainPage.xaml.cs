using LukeMauiFilePicker;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            // Assume this method is invoked from UI thread

            var newFilePicker = App.Current.MainPage!.Handler!.MauiContext!.Services.GetRequiredService<IFilePickerService>();

            // Microsoft's file picker is broken on macos
            //FileResult? selectedFile = await FilePicker.Default.PickAsync();

            IPickFile? selectedFile = await newFilePicker.PickFileAsync("Pick a file", null);

            if(selectedFile is null) // for MacOS this is always null. Verified working on Windows.
            {
                TextToUpdate.Text = "No file was selected.";
                return;
            }

            TextToUpdate.Text = $"Selected file name: {selectedFile.FileName}";
        }
    }

}
