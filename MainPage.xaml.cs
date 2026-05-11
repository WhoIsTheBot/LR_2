using System.Diagnostics; // Потрібно для класу Process [cite: 74]

namespace FigureMotionApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnStartNotepadClicked(object sender, EventArgs e)
    {
        try
        {
            [cite_start]
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "notepad.exe",
                UseShellExecute = true
            };

            [cite_start]
            Process notepadProcess = new Process
            {
                StartInfo = startInfo,
                [cite_start]EnableRaisingEvents = true 
            };

            [cite_start]
            notepadProcess.Exited += (s, args) =>
            {
                [cite_start]
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    StatusLabel.Text = "Статус: Блокнот закритий";
                    StatusLabel.TextColor = Colors.Red;
                    BtnStart.IsEnabled = true;

                    [cite_start]
                    await DisplayAlert("Процеси", "Блокнот закритий", "OK");
                });

                notepadProcess.Dispose(); 
            };

            [cite_start]
            notepadProcess.Start();

            StatusLabel.Text = "Статус: Блокнот запущено (ID: " + notepadProcess.Id + ")";
            StatusLabel.TextColor = Colors.Green;
            BtnStart.IsEnabled = false;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Помилка", "Не вдалося запустити процес: " + ex.Message, "OK");
        }
    }
}