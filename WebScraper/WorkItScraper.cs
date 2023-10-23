using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using NUglify;
namespace WebScraper;

public class WorkItScraper : ScrapeBase
{
    public async Task<List<Announcement>> Scrape()
    {
        
        string url = "https://kaz.tgstat.com/channel/@workitkz";
    
        List<Announcement> list = new();


        string htmlContent =await GetHTMLAsync(url);

        HtmlDocument htmlDocument = new();  
        htmlDocument.LoadHtml(htmlContent);

        HtmlNodeCollection nodeCollections = htmlDocument.DocumentNode.SelectNodes("//div[@class='posts-list lm-list-container']/div");
        foreach (HtmlNode node in nodeCollections)
        {
            if (Regex.IsMatch(node.SelectSingleNode(".//div[@class='post-text']").InnerHtml, "#вакансия"))
            {
                if(Regex.IsMatch(node.SelectSingleNode(".//div[@class='post-text']").InnerHtml, ("#dotnet|#csharp|dotnetcore|java")))
                {
                    
                    var content = node.SelectSingleNode(".//div[@class='post-text']").InnerHtml;
                    var time = node.SelectSingleNode(".//p[@class='text-muted m-0']/small").InnerText;
                    var result = Uglify.HtmlToText(content); //
                    list.Add(new Announcement()
                    {
                        Content = result.Code,
                        UploadDate = time
                    });

                    //Console.WriteLine(link);
                }
            }
            
        }

        return list;
    }
}