using System;
using System.IO;
using ChatSharp;
using SharpBot.Constants;
using SharpBot.AIML;
using SharpBot.Plugins;
using AIMLbot;

namespace SharpBot{
  using Conf = ConfConstants;

  class Bot{
    private static void Log(string query, string response, string bot, string user){
      DateTime time = DateTime.Now;
      string timeStamp = time.ToString("[hh:mm:ss]");
      string fileDate = time.ToString("yyyy-MM-dd");

      string logDir = Conf.LogDir;
      string logFile = Path.Combine(logDir, String.Format("{0}.log", fileDate));

      string logQuery = String.Format("{0} {1}: {2}", timeStamp, user, query);
      string logResp = String.Format("{0}: {1}{2}", bot, response, Environment.NewLine);

      using (StreamWriter file = File.AppendText(logFile)){
        file.WriteLine(logQuery);
        file.WriteLine(logResp);
      }
    }
    
    private static void Main(){
      var ircUser = new IrcUser(Conf.Nick, Conf.User);
      var client = new IrcClient(Conf.Server, ircUser);
      var ChatBot = new BotAgent();
      ChatBot.Initialise();

      client.ConnectionComplete += (s, e) => {
        foreach (string chan in Conf.Channels)
          client.JoinChannel(chan);
      };

      client.ChannelMessageRecieved += (s, e) => {
        var source = e.PrivateMessage.Source;
        var channel = client.Channels[source];
        var message = e.PrivateMessage.Message;

        var urls = URLTitle.GetURLs(message);
        if (urls.Count != 0){
          var responseTitle = URLTitle.GetTitle(urls[0]);
          channel.SendMessage(responseTitle);
        }
        else{
          var query = ChatBot.Query(message);
          var response = ChatBot.Reply(query);
          Log(message, response.Output, Conf.Nick, source);
          channel.SendMessage(response.Output);
        }
      };

      client.NetworkError += (s, e) =>
        Console.WriteLine("Error: {0}", e.SocketError);
 
      client.ConnectAsync();
      while (true);
    }
  }
}
