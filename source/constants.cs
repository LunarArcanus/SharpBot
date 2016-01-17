using System.Configuration;

namespace SharpBot.Constants{

  public class ConfConstants{

    private static string Config(string configuration){
      return ConfigurationManager.AppSettings[configuration];
    }

    public static string Nick{
      get{ return Config("Nick"); }
    }
    public static string User{
      get{ return Config("User"); }
    }
    public static string Server{
      get{ return Config("Server"); }
    }
    public static string[] Channels{
      get{ return Config("Channels").Split(); }
    }
    public static string QuitMsg{
      get{ return Config("QuitMsg"); }
    }
    public static string LogDir{
      get{ return Config("LogDir"); }
    }
  }
}
