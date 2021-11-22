using Keymaster.Rage.Client.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Keymaster.Rage.Client
{
    public static class BrowserContainer
    {
        // Сам список браузеров
        static IList<Browser> Browsers = new List<Browser>();

        //Добавляем в контейнер новій браузер и возвращаем єтот браузер обратно пользователю
        public static Browser Add(Browser browser)
        {
            Browsers.Add(browser);

            return browser;
        }

        // Удаление браузера по его ID
        public static Browser RemoveById(ushort id)
        {
            // Ищем браузер
            var browser = Browsers.FirstOrDefault(p => p.HtmlWindow.Id == id);

            // Если не нашли, то выбрасываем исключение(Мы его создали немного ниже)
            if (browser == null) throw new BrowserNotFoundException($"Not found {id} browser");

            // Закрываем браузер
            browser.Close();

            // Возвращаем закрытый браузер
            return browser;
        }
    }
}
