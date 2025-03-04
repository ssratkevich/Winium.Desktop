﻿using System;
using CommandLine;

namespace Winium.Desktop.Driver
{
    internal class Program
    {
        #region Methods

        [STAThread]
        private static void Main(string[] args)
        {
            var listeningPort = 9999;

            CommandLineOptions options = null;
            var result = Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed<CommandLineOptions>(o =>
                {
                    options = o;
                });
            if (options == null)
            {
                options = new CommandLineOptions();
            }

            if (options.Port.HasValue)
            {
                listeningPort = options.Port.Value;
            }

            if (options.ElementSearchDelay.HasValue)
            {
                Cruciatus.CruciatusFactory.Settings.SearchTimeout = (int) options.ElementSearchDelay;
            }


            if (options.LogPath != null)
            {
                Logger.TargetFile(options.LogPath, options.Verbose);
            }
            else if (!options.Silent)
            {
                Logger.TargetConsole(options.Verbose);
            }
            else
            {
                Logger.TargetNull();
            }

            try
            {
                var listener = new Listener(listeningPort);
                Listener.UrnPrefix = options.UrlBase;

                Console.WriteLine("Starting Windows Desktop Driver on port {0}\n", listeningPort);

                listener.StartListening();
            }
            catch (Exception ex)
            {
                Logger.Fatal("Failed to start driver: {0}", ex);
                throw;
            }
        }

        #endregion
    }
}
