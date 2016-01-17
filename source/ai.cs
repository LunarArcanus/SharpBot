// The AIML part of the bot^^
using AIMLbot;

namespace SharpBot.AIML{

  public class BotAgent{

    private Bot Brain;
    private User UserAgent;

    public BotAgent(){
      Brain = new Bot();
      // Everyone just lumped under a single identity
      UserAgent = new User("IRC", Brain);
    }

    public void Initialise(){
      Brain.loadSettings();
      Brain.isAcceptingUserInput = false;
      Brain.loadAIMLFromFiles();
      Brain.isAcceptingUserInput = true;
    }

    public Request Query(string input){
      return new Request(input, UserAgent, Brain);
    }

    public Result Reply(Request req){
      return Brain.Chat(req);
    }
  }
}
