// See https://aka.ms/new-console-template for more information

using HtmlAgilityPack;

String url = "https://weather.com/en-GB/weather/today/l/UKXX0085:1:UK?Goto=Redirected";
var httpClient = new HttpClient();
var html = httpClient.GetStringAsync(url).Result;
var htmlDocument = new HtmlDocument(); //Used to parse, manipulate, and traverse HTML documents programmatically.
htmlDocument.LoadHtml(html);

var temperatureElement = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='CurrentConditions--tempValue--zUBSz']");
var temperature = temperatureElement.InnerText.Trim();
Console.WriteLine(temperature);



Console.ReadKey();