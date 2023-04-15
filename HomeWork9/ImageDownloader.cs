using System.Net;

namespace HomeWork9;

public class ImageDownloader
{
    public event DownloadHandler ImageStarted;
    public event DownloadHandler ImageCompleted;
    public async Task DownloadAsync(string name, string remoteUri)
    {
        using var client = new WebClient();
        await Task.Run(async () =>
        {
            ImageStarted(name, remoteUri);
            await client.DownloadFileTaskAsync(remoteUri, name);
            ImageCompleted(name, remoteUri);
        });
    }
}