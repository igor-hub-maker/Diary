using Diary.Models;
using System.IO;
using System.Text.Json;

namespace Diary.Services
{
    public static class LocalUserInfoService
    {
        public static bool IsUserAuth = false;
        public static bool IsUserGuest = false;
        public static string UserName;
        public static int Id;
        public static void GetUserInfo()
        {
            string filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/User.json";
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                if (!string.IsNullOrEmpty(json))
                {
                    var user = JsonSerializer.Deserialize<User>(json);
                    if (!string.IsNullOrEmpty(user.Login))
                    {
                        IsUserAuth = true;
                        UserName = user.Login;
                        Id = user.Id;
                    }
                    else
                    {
                        IsUserGuest = true;
                    }
                }
            }
        }
        public static void SaveUserInfo(User user)
        {
            string filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/User.json";
            string json = JsonSerializer.Serialize(user);
            File.WriteAllText(filePath, json);
            UserName = user.Login;
            Id = user.Id;
            if (!string.IsNullOrEmpty(UserName))
            {
                IsUserGuest = true;
            }
            IsUserAuth = true;
        }
    }
}
