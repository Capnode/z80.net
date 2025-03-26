using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using z80view.Sound;

namespace z80view
{
    public class MainWindow : Window
    {
        private EmulatorViewModel _viewModel;
        private Control _img;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            this.Focus();
            this._viewModel.KeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            this.Focus();
            this._viewModel.KeyUp(e);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            _img = ((Grid) Content).Children.First();

            var emulator = new z80emu.Emulator();
            var askfile = new AskUserFile(this);
            var soundDevice = SoundDeviceFactory.Create(emulator);

            _viewModel = new EmulatorViewModel(
                () => Dispatcher.UIThread.Invoke((Action)(() => _img.InvalidateVisual())),
                askfile,
                soundDevice,
                emulator);
            this.Closed += (s, e) => _viewModel.Stop();
        }
    }
}
