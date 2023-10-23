namespace WebScraper;

public class ScrapeBase
{
    public static async Task<string> GetHTMLAsync(string url)
    {
        using HttpClient client = new();                            //create http client and managing unnecessary resources 
        HttpRequestMessage request = new(HttpMethod.Get, url);      // send request method:get to url
                
        HttpResponseMessage response=await client.SendAsync(request); //get response 

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();      // get content from response
        
        }

        throw new Exception($"request failed {url}");

    }
}