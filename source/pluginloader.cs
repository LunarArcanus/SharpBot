using System.Collections.Generic;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace SharpBot.Plugins{

  public class URLTitle{
    public static string GetTitle(string webpage){
      HtmlDocument doc = new HtmlWeb().Load(webpage);
      HtmlNode titleNode = doc.DocumentNode.SelectSingleNode("//title");
      string title = titleNode.InnerText;
      return title;
    }

    public static List<string> GetURLs(string text){
      Regex linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
      var matches = linkParser.Matches(text);
      var urls = new List<string>();
      foreach (Match m in matches) urls.Add(m.Value);
      return urls;
    }
  }
}
