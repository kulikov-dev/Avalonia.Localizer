using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Headless;
using Avalonia.ReactiveUI;
using Avalonia.Threading;

namespace Avalonia.Localizer.Tests
{
    public static class AvaloniaApp
    {
        public static void Stop()
        {
            var app = GetApp();

            if (app is IDisposable disposable)
            {
                Dispatcher.UIThread.Post(disposable.Dispose);
            }

            Dispatcher.UIThread.Post(() => app.Shutdown());
        }

        public static MainWindow GetMainWindow() => (MainWindow)GetApp().MainWindow;

        public static IClassicDesktopStyleApplicationLifetime GetApp()
        {
            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime result)
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public static AppBuilder BuildAvaloniaApp() =>
            AppBuilder
                .Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI()
                .UseHeadless();
    }
}
