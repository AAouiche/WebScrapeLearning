using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapeLearning
{
    public class PlaywrightLearning
    {
        public async Task RunPlaywrightTestAsync()
        {
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });

            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            await page.GotoAsync("https://books.toscrape.com/");

            var books = await page.Locator("ol.row > li").AllAsync();
            foreach (var book in books)
            {
                var title = await book.Locator("h3 > a").GetAttributeAsync("title");
                var price = await book.Locator(".price_color").InnerTextAsync();
                var stockStatus = await book.Locator(".instock").InnerTextAsync();

                Console.WriteLine($"Title: {title}");
                Console.WriteLine($"Price: {price}");
                Console.WriteLine($"Stock Status: {stockStatus}");
                Console.WriteLine();
            }

            await browser.CloseAsync();
        }
    }
}
