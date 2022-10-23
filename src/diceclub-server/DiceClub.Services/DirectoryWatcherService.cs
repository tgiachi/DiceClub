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
        public DirectoryWatcherService(IEventBusService eventBusService, ILogger<DirectoryWatcherService> logger) : base(eventBusService, logger)
        {

        }

        private  Task StartWatchDirectory(string directory, string tag)
        {
            var fsw = new FileSystemWatcher(directory);

            fsw.NotifyFilter = NotifyFilters.Attributes
                               | NotifyFilters.CreationTime
                               | NotifyFilters.DirectoryName
                               | NotifyFilters.FileName
                               | NotifyFilters.LastAccess
                               | NotifyFilters.LastWrite
                               | NotifyFilters.Security
                               | NotifyFilters.Size;

            fsw.Filter = "*.*";
            fsw.IncludeSubdirectories = true;
            fsw.EnableRaisingEvents = true;

            fsw.Created += async (sender, args) =>
            {
                await PublishEvent(new ImageCardCreatedEvent() {FileName = args.FullPath});

            };

            return Task.CompletedTask;
        }
    }
}
