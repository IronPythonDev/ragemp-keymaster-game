namespace Keymaster.Rage.Client
{
    public class Browser
    {
        public Browser(string folder)
        {
            Folder = folder;

            HtmlWindow = new RAGE.Ui.HtmlWindow($"package://cef_packages/{folder}/index.html");

            ExecuteJS($"window.browserId = {HtmlWindow.Id}");

            BrowserContainer.Add(this);
        }

        public string Folder { get; private set; }

        public RAGE.Ui.HtmlWindow HtmlWindow { get; private set; }

        public void Open()
        {
            HtmlWindow.Active = true;

            Focus();
        }

        public void Close()
        {
            HtmlWindow.Active = false;
            HtmlWindow.Destroy();
            UnFocus();
        }

        public void Focus() => RAGE.Ui.Cursor.Visible = true;

        public void UnFocus() => RAGE.Ui.Cursor.Visible = false;

        public void ExecuteJS(string code) => HtmlWindow?.ExecuteJs(code);
    }
}
