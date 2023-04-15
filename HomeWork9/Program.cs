using System.Diagnostics;
using System.Net;

namespace HomeWork9
{

    public class Program
    {

        public static void Main()
        {
            void PrintStart(string name, string adress)
            {
                Console.WriteLine("Start download \"{0}\" image from \"{1}\"", name, adress);
            }

            void PrintEnd(string name, string adress)
            {
                Console.WriteLine("Download \"{0}\" image from \"{1}\" is comleted", name, adress);
            }

            ImageDownloader.ImageStarted += PrintStart;
            ImageDownloader.ImageCompleted += PrintEnd;
            List<Task> tasks = new List<Task>();
            string[] imagesUrl = new[]
            {
                "https://img1.akspic.ru/crops/4/2/7/2/7/172724/172724-nacionalnyj_park_banf-nacionalnyj_park_joho_v_kanade-luk_river_trejl-oblako-rastenie-7680x4320.jpg",
                "https://img3.akspic.ru/previews/0/2/7/2/7/172720/172720-avalskaya_skala-rodaliya_silvestr-afrikanskij_poezd-mister_ruki-tyuremnyj_plyazhnyj_klub-x750.jpg",
                "https://img3.akspic.ru/previews/9/7/6/2/7/172679/172679-nagore-barencburg-gora-voda-oblako-x750.jpg",
                "https://img2.akspic.ru/previews/0/2/6/2/7/172620/172620-dolomit-cerkov_svyatogo_ioanna-gora-oblako-rastenie-x750.jpg",
                "https://img1.akspic.ru/previews/9/9/5/2/7/172599/172599-nacionalnyj_park_banf-morennoe_ozero-priroda-nacionalnyj_park-banff-x750.jpg",
                "https://img3.akspic.ru/previews/9/0/5/2/7/172509/172509-rozovaya_milost_art-Overwatch-milost-art-teni-x750.jpg",
                "https://img2.akspic.ru/previews/5/0/4/2/7/172405/172405-priroda-zamok_balmoral-loh_muik-pomeste_mar_lodzh-voda-x750.jpg",
                "https://img2.akspic.ru/previews/4/0/4/2/7/172404/172404-vashe_imya-miyamizu_mitsuha-taki_tachibana-anime-atmosfera-x750.jpg",
                "https://img1.akspic.ru/previews/5/8/3/2/7/172385/172385-toskana-ekoregion-oblako-zelenyj-priroda-x750.jpg",
                "https://img1.akspic.ru/previews/2/6/3/2/7/172362/172362-germaniya-art-soundcloud-zvukovoj_element-spotifaj-x750.jpg"
            };
            var i1 = new ImageDownloader();
            var i2 = new ImageDownloader();
            var i3 = new ImageDownloader();
            var i4 = new ImageDownloader();
            var t = i1.DownloadAsync("b1", imagesUrl[0]);
            tasks.Add(t);
            var t1 = i2.DownloadAsync("b2", imagesUrl[1]);
            tasks.Add(t1);
            var t2 = i3.DownloadAsync("b3", imagesUrl[2]);
            tasks.Add(t2);
            var t3 = i4.DownloadAsync("b4", imagesUrl[3]);
            tasks.Add(t3);
            Console.WriteLine("end");
            while (true)
            {
                if (tasks.Count == 0)
                {
                    break;
                }
                else
                {
                    foreach (var item in tasks)
                    {

                        if (item.IsCompleted)
                        {
                            Console.WriteLine($"completed {item.Id}");
                            continue;
                        }
                        else
                        {
                            Console.WriteLine($"not completed {item.Id}");
                        }
                    }
                }
            }
            Console.ReadKey();
        }


    }


    public class ImageDownloader
    {
        public static event Action<string, string> ImageStarted;
        public static event Action<string, string> ImageCompleted;
        List<Task> taskList = new();

        public async Task DownloadAsync(string fileNameMask, string imagesUrl)
        {
            var webClient = new WebClient();
            Console.WriteLine($"Start {fileNameMask}, from {imagesUrl}");
            await webClient.DownloadFileTaskAsync(imagesUrl, fileNameMask);
            Console.WriteLine($"End {fileNameMask}, from {imagesUrl}");
        }
    }
}

//public void PrintStatusTask()
//{
//    foreach (var item in taskList)
//    {
//        Console.WriteLine(item.Status.ToString());
//    }
//}

//        var taskList = new List<Task>(10);
//        var imageDownloader = new ImageDownloader();
//        var starter = new DownloadStartNotifier();
//        var ender = new DownloadEndNotifier();
//        starter.Subscribe(imageDownloader);
//        ender.Subscribe(imageDownloader);
//        imageDownloader.DownloadAsync();
//        var whiteLine = new string(' ', 30);
//        ConsoleKeyInfo ki;
//        Console.WriteLine("Press \"A\" to close program\nPress any key to print status download");
//        int[] cursor = new[]
//        {
//                Console.GetCursorPosition().Left,
//                Console.GetCursorPosition().Top
//         };
//        do
//        {
//            ki = Console.ReadKey();
//            SetCursor(cursor, whiteLine);
//            switch (ki.Key)
//            {
//                case ConsoleKey.A:
//                    Console.WriteLine("end program");
//                    break;
//                default:
//                    imageDownloader.PrintStatusTask();
//                    break;
//            }
//        } while (ConsoleKey.A != ki.Key);
//    }

//    private static void SetCursor(int[] cursor, string whiteLine)
//    {
//        Console.SetCursorPosition(cursor[0], cursor[1]);
//        Console.WriteLine(whiteLine);
//        Console.SetCursorPosition(cursor[0], cursor[1]);
//    }
//}

//public class ImageDownloader
//{
//    public event Action<string, string> ImageStarted;
//    public event Action<string, string> ImageCompleted;
//    public string DownloadUrl { get; set; }
//    public string FileName { get; set; }
//    public Task Task { get; set; }
//    public async void DownloadAsync()
//    {
//        // Откуда будем качать
//        DownloadUrl = "https://kenrockwell.com/leica/images/50mm-f35/sample-images/L1012285.jpg";
//        // Как назовем файл на диске
//        FileName = "bigimage.jpg";
//        // Качаем картинку в текущую директорию
//        var myWebClient = new WebClient();
//        ImageStarted?.Invoke(DownloadUrl, FileName);
//        Task = myWebClient.DownloadFileTaskAsync(DownloadUrl, FileName);
//        ImageCompleted?.Invoke(DownloadUrl, FileName);
//        await Task;
//    }

//    public void PrintStatusTask()
//    {
//        if (Task.IsCompleted)
//        {

//            Console.WriteLine("Task is completed");
//        }
//        else
//        {
//            Console.WriteLine("Task in process...");
//        }
//    }
//}

//public class DownloadStartNotifier
//{
//    public void ImageStarted(string downloadUrl, string fileName)
//    {
//        Console.WriteLine("Качаю \\\"{0}\\\" из \\\"{1}\\\" .......\\n\\n\"", downloadUrl, fileName);
//    }


//    public void Subscribe(ImageDownloader downloader)
//    {
//        downloader.ImageStarted += ImageStarted;
//    }
//}

//public class DownloadEndNotifier
//{
//    public void ImageCompleted(string downloadUrl, string fileName)
//    {
//        Console.WriteLine("Успешно скачал \"{0}\" из \"{1}\"", downloadUrl, fileName);
//    }
//    public void Subscribe(ImageDownloader downloader)
//    {
//        downloader.ImageCompleted += ImageCompleted;
//    }

