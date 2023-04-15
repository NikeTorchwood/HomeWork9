using System.Diagnostics;
using static System.Net.WebRequestMethods;

namespace HomeWork9
{
    public class Program
    {
        public static void Main()
        {
            var taskList = new List<Task>();
            var nameMask = "picture";
            string[] uriStrings = new[]
            {
                //"https://www.esato.com/phonephotos/cam/samsung/sm_g900f/201406011711P0ER52.jpg",
                "https://prades.net/canada2004/493T1914a.jpg",
                "http://picsdesktop.net/mountain/1024x768/PicsDesktop.net_206.jpg",
                "https://i.artfile.me/wallpaper/11-06-2013/4000x2667/priroda-reki-ozera-spirit-island-730985.jpg",
                "https://kenrockwell.com/leica/images/50mm-f35/sample-images/L1012285.jpg",
                "https://prades.net/canada2004/493T1914a.jpg",
                "http://picsdesktop.net/mountain/1024x768/PicsDesktop.net_206.jpg",
                "https://i.artfile.me/wallpaper/11-06-2013/4000x2667/priroda-reki-ozera-spirit-island-730985.jpg",
                "https://kenrockwell.com/leica/images/50mm-f35/sample-images/L1012285.jpg",
                "https://prades.net/canada2004/493T1914a.jpg",
                "http://picsdesktop.net/mountain/1024x768/PicsDesktop.net_206.jpg",
                "https://i.artfile.me/wallpaper/11-06-2013/4000x2667/priroda-reki-ozera-spirit-island-730985.jpg",
                "https://kenrockwell.com/leica/images/50mm-f35/sample-images/L1012285.jpg"
            };
            var paramsDictionary = FillDictionary(nameMask, uriStrings);
            var downloader = new ImageDownloader();
            downloader.ImageStarted += NotifyStart;
            downloader.ImageCompleted += NotifyCompleted;
            foreach (var item in paramsDictionary)
            {
                taskList.Add(Task.Run(async () =>
                {
                    await downloader.DownloadAsync(item.Key, item.Value);
                }));
            }

            var s = new string('-', 50);
            var text = "Нажмите клавишу A для выхода или любую другую клавишу для проверки статуса скачивания";
            ConsoleKeyInfo ki;
            while (true)
            {
                Console.WriteLine(text);
                ki = Console.ReadKey();
                switch (ki.Key)
                {

                    case ConsoleKey.A:
                        return;
                    default:
                        Console.WriteLine(s);
                        foreach (var item in taskList)
                        {
                            if (item.IsCompleted)
                            {
                                Console.WriteLine($"Image #{item.Id} загружена");
                            }
                            else
                            {
                                Console.WriteLine($"Image #{item.Id} загружается");
                            }
                        }
                        Console.WriteLine(s);
                        break;
                }
            }
        }
        public static Dictionary<string, string> FillDictionary(string nameMask, string[] uriStrings)
        {
            var dictionary = new Dictionary<string, string>();
            for (var i = 0; i < uriStrings.Length; i++)
            {
                dictionary.Add($"{nameMask}{i}.jpg", uriStrings[i]);
            }
            return dictionary;
        }
        public static void NotifyStart(string fileName, string remoteUri)
        {
            Console.WriteLine("Качаю \"{0}\" из \"{1}\" .......", fileName, remoteUri);
        }
        public static void NotifyCompleted(string fileName, string remoteUri)
        {
            Console.WriteLine("Успешно скачал \"{0}\" из \"{1}\"", fileName, remoteUri);
        }
    }
}
