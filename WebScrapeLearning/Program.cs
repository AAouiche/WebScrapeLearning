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
String url = "https://books.toscrape.com/";
var httpClient = new HttpClient();
var html = httpClient.GetStringAsync(url).Result;
var htmlDocument = new HtmlDocument(); //Used to parse, manipulate, and traverse HTML documents programmatically.
htmlDocument.LoadHtml(html);

var listOfBooksElement = htmlDocument.DocumentNode.SelectNodes("//ol[@class='row']/li");
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
//Console.WriteLine(listOfBooksElement);




Console.ReadLine();