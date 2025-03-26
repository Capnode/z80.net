using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace z80view
{
    public interface IAskUserFile
    {
        Task<string> AskFile();
    }

    public class AskUserFile : IAskUserFile
    {
        private readonly Window _parentWindow;

        public AskUserFile(Window parentWindow)
        {
            _parentWindow = parentWindow;
        }

        public async Task<string> AskFile()
        {
            var filter = new FileDialogFilter()
            {
                Name = "spectrum program files",
                Extensions = new List<string> { "z80", "tap" }
            };

            var openDialog = new OpenFileDialog();
            openDialog.Title = "Select *.z80 or *.tap file";
            openDialog.AllowMultiple = false;
            openDialog.Filters.Add(filter);

            var files = await openDialog.ShowAsync(_parentWindow);
            if (files != null && files.Length != 0)
            {
                return files[0];
            }

            return null;
        }
    }
}
