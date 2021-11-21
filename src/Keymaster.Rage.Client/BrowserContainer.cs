using Keymaster.Rage.Client.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Keymaster.Rage.Client
{
    public static class BrowserContainer
    {
        static IList<Browser> Browsers = new List<Browser>();

        public static Browser Add(Browser browser)
        {
            Browsers.Add(browser);

            return browser;
        }

        public static Browser RemoveById(ushort id)
        {
            var browser = Browsers.FirstOrDefault(p => p.HtmlWindow.Id == id);

            if (browser == null) throw new BrowserNotFoundException($"Not found {id} browser");

            browser.Close();

            return browser;
        }
    }
}
