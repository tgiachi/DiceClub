using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using DiceClub.Api.Data.Cards;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services
{
    public class DirectoryWatcherService : AbstractBaseService<DirectoryWatcherService>
    {
        private Dictionary<string, FileSystemWatcher> _fileSystemWatchers = new();
        private FileSystemWatcher fsw;
        public DirectoryWatcherService(IEventBusService eventBusService, ILogger<DirectoryWatcherService> logger) : base(eventBusService, logger)
        {
            // StartWatchDirectory(@"C:\temp\Scan\", "");
        }

        private void StartWatchDirectory(string directory, string tag)
        {
            fsw = new FileSystemWatcher(directory);

            fsw.NotifyFilter = NotifyFilters.Attributes
                               | NotifyFilters.CreationTime
                               | NotifyFilters.DirectoryName
                               | NotifyFilters.FileName
                               | NotifyFilters.LastAccess
                               | NotifyFilters.LastWrite
                               | NotifyFilters.Security
                               | NotifyFilters.Size;

            fsw.Filter = "*.jpg";


            fsw.Created += async (sender, args) =>
            {
                if (GetIdleFile(args.FullPath))
                {
                    await PublishEvent(new ImageCardCreatedEvent() { FileName = args.FullPath });
                }


            };
            //fsw.Changed += async (sender, args) =>
            //{
            //    await PublishEvent(new ImageCardCreatedEvent() { FileName = args.FullPath });

            //};


            fsw.Error += (sender, args) =>
            {
                Logger.LogError("Error during listen directory: {Directory} => {Error}", directory,
                    args.GetException());
            };


            fsw.IncludeSubdirectories = false;
            fsw.EnableRaisingEvents = true;

        }

        private static bool GetIdleFile(string path)
        {
            var fileIdle = false;
            const int maximumAttemptsAllowed = 30;
            var attemptsMade = 0;

            while (!fileIdle && attemptsMade <= maximumAttemptsAllowed)
            {
                try
                {
                    using (File.Open(path, FileMode.Open, FileAccess.ReadWrite))
                    {
                        fileIdle = true;
                    }
                }
                catch
                {
                    attemptsMade++;
                    Thread.Sleep(100);
                }
            }

            return fileIdle;
        }
    }
}
