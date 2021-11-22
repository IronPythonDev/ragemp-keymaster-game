namespace Keymaster.Rage.Client
{
    public class Browser
    {
        // folder это имя папки в которой лежит index.html нового браузера
        public Browser(string folder)
        {
            Folder = folder;

            HtmlWindow = new RAGE.Ui.HtmlWindow($"package://cef_packages/{folder}/index.html");

            // Вбрасываем в window нового браузера его уникальный ид чтобы если вдруг у нас будет много браузеров можно было как-то их модерировать
            ExecuteJS($"window.browserId = {HtmlWindow.Id}");

            BrowserContainer.Add(this); // Добавляем этот браузер в контейнер браузеров
        }

        public string Folder { get; private set; }

        public RAGE.Ui.HtmlWindow HtmlWindow { get; private set; }

        // Метод открывает браузер и показывает курсор
        public void Open()
        {
            HtmlWindow.Active = true;

            Focus();
        }

        // Удаляет браузер и скрывает курсор
        public void Close()
        {
            HtmlWindow.Active = false;
            HtmlWindow.Destroy();
            UnFocus();
        }

        // Отображаем курсор
        public void Focus() => RAGE.Ui.Cursor.Visible = true;

        // Скрываем курсор
        public void UnFocus() => RAGE.Ui.Cursor.Visible = false;

        // Инжектим какой-то JS код внутри текущего браузера
        public void ExecuteJS(string code) => HtmlWindow?.ExecuteJs(code);
    }
}
