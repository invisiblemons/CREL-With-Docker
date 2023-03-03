
namespace CREL_BE.ApiLayer.Helpers;

public static class DevelopHelper
{
    public static string log = "";
    public static void WriteLog(string st){
        log += st + "\n";
    }

    public static string ReadLog(){
        return log;
    }

    public static void ClearLog(){
        log = $"Cleared at {DateTime.Now}";
    }
}
