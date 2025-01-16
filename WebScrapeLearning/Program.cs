// See https://aka.ms/new-console-template for more information

using HtmlAgilityPack;

//Weather website
/*String url = "https://weather.com/en-GB/weather/today/l/UKXX0085:1:UK?Goto=Redirected";
var httpClient = new HttpClient();
var html = httpClient.GetStringAsync(url).Result;
var htmlDocument = new HtmlDocument(); //Used to parse, manipulate, and traverse HTML documents programmatically.
htmlDocument.LoadHtml(html);

var temperatureElement = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='CurrentConditions--tempValue--zUBSz']");
var temperature = temperatureElement.InnerText.Trim();
Console.WriteLine(temperature);

var airQualityElement = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='AirQualityText--severity--jiW+F']");
var airQuality = airQualityElement.InnerText.Trim();
Console.WriteLine(airQuality);*/


//Book website
string baseUrl = "https://books.toscrape.com/catalogue/page-{0}.html";
var httpClient = new HttpClient();

int pageNumber = 1;
bool hasMorePages = true;

while (hasMorePages)
{
    // Construct URL for the current page
    string url = pageNumber == 1
        ? "https://books.toscrape.com/" // First page URL is different
        : string.Format(baseUrl, pageNumber);

    Console.WriteLine($"Fetching: {url}");

    try
    {
        // Fetch and parse the HTML document
        var htmlResponseMessage = httpClient.GetAsync(url).Result;
        if(htmlResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            hasMorePages = false;
            break;
        }
        var html = htmlResponseMessage.Content.ReadAsStringAsync().Result;
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        // Select book elements on the page
        var listOfBooksElement = htmlDocument.DocumentNode.SelectNodes("//ol[@class='row']/li");
        if (listOfBooksElement == null || listOfBooksElement.Count == 0)
        {
            hasMorePages = false; // No books found, end the loop
            Console.WriteLine("No more books found. Exiting.");
            break;
        }

        // Extract and print book information
        foreach (var bookElement in listOfBooksElement)
        {
            var titleNode = bookElement.SelectSingleNode(".//h3/a");
            var title = titleNode?.GetAttributeValue("title", "No Title");

            var priceNode = bookElement.SelectSingleNode(".//p[@class='price_color']");
            var price = priceNode?.InnerText.Trim();

            var stockNode = bookElement.SelectSingleNode(".//p[contains(@class, 'instock')]");
            var stockStatus = stockNode?.InnerText.Trim();

            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Price: {price}");
            Console.WriteLine($"Stock Status: {stockStatus}");
            Console.WriteLine();
        }

        // Increment the page number for the next iteration
        pageNumber++;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
        hasMorePages = false; 
    }
}

Console.WriteLine("Scraping completed.");




Console.ReadLine();