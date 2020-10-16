using System.Diagnostics;
using System.IO;

namespace AndriaBot
{
    public static class Util
    {
        public static string DownloadYoutube(string url)
        {
            string directory = Directory.GetCurrentDirectory();
            directory = Path.Combine(directory, "musics");
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            int i = 0;
            while (File.Exists(Path.Combine(directory, $"test{i}.mp3")))
            {
                i++;
            }
            string file = Path.Combine(directory, $"test{i}");
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                Arguments = $"-x --audio-format mp3 {url} -o {file}.%(ext)s",
                FileName = "youtube-dl.exe",
                CreateNoWindow = true
            };
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            return Path.Combine(Path.Combine(directory, $"test{i}.mp3"));
        }
    }
}
