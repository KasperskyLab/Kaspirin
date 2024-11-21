// Copyright © 2024 AO Kaspersky Lab.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core
{
    public static class XamlStylerToolFacade
    {
        public static void FormatXamlFiles(string path, TaskLoggingHelper log)
        {
            if (!CheckXamlStylerToolInstalled())
            {
                log.LogMessage(MessageImportance.High, "XamlStyler dotnet tool is not installed.");

                log.LogMessage(MessageImportance.High, "Installing XamlStyler dotnet tool.");

                var hasExited = ExecuteCommand(DotnetCommand, "tool install XamlStyler.Console --global", out _, out var exitCode);
                if (hasExited && exitCode != SuccessExitCode || !CheckXamlStylerToolInstalled())
                {
                    log.LogWarning("Failed to install XamlStyler dotnet tool. Skip XAML files formatting.");
                    return;
                }

                log.LogMessage(MessageImportance.High, "XamlStyler dotnet tool was successfully installed.");
            }

            if (Directory.Exists(path))
            {
                FormatXamlFilesInDirectory(path, log);
            }
            else if (File.Exists(path))
            {
                FormatXamlFile(path, log);
            }
            else
            {
                log.LogWarning($"'{path}' doesn't correspond to existing file or directory. Skip XAML files formatting.");
            }
        }

        private static bool CheckXamlStylerToolInstalled()
        {
            var hasExited = ExecuteCommand(DotnetCommand, "tool list -g", out var output, out var exitCode);
            return hasExited && exitCode == SuccessExitCode && output.Contains(XamlStylerToolCommand);
        }

        private static bool FormatXamlFilesInDirectory(string directory, TaskLoggingHelper log)
        {
            directory = Path.GetFullPath(directory);

            for (var retryCounter = 1; retryCounter <= MaxRetriesCount; retryCounter++)
            {
                log.LogMessage(MessageImportance.Normal, $"[Attempt #{retryCounter}] Run XamlStyler dotnet tool for directory '{directory}'.");

                var hasExited = ExecuteCommand(
                    XamlStylerToolCommand,
                    $"{DirectoryParamName} {directory} {RecursiveParamName} {LogLevelParamName} None",
                    out _,
                    out var exitCode);

                if (hasExited && exitCode == SuccessExitCode)
                {
                    log.LogMessage(MessageImportance.Normal, $"[Attempt #{retryCounter}] XAML files in directory '{directory}' were successfully formatted.");
                    log.LogMessage(MessageImportance.High, $"XAML files in directory '{directory}' were successfully formatted.");
                    return true;
                }
                else
                {
                    log.LogMessage(MessageImportance.Normal, $"[Attempt #{retryCounter}] Failed to format XAML files in directory '{directory}' (exit code 0x{exitCode:X}).");
                }

                if (retryCounter != MaxRetriesCount)
                {
                    Thread.Sleep(RetryDelay);
                }
            }

            log.LogWarning($"Failed to format XAML files in directory '{directory}'.");
            return false;
        }

        private static bool FormatXamlFile(string path, TaskLoggingHelper log)
        {
            path = Path.GetFullPath(path);

            for (var retryCounter = 1; retryCounter <= MaxRetriesCount; retryCounter++)
            {

                log.LogMessage(MessageImportance.Normal, $"[Attempt #{retryCounter}] Run XamlStyler dotnet tool for file '{path}'.");

                var hasExited = ExecuteCommand(
                    XamlStylerToolCommand,
                    $"{FileParamName} {path} {LogLevelParamName} None",
                    out _,
                    out var exitCode);

                if (hasExited && exitCode == SuccessExitCode)
                {
                    log.LogMessage(MessageImportance.Normal, $"[Attempt #{retryCounter}] XAML file '{path}' was successfully formatted.");
                    log.LogMessage(MessageImportance.High, $"XAML file '{path}' was successfully formatted.");
                    return true;
                }
                else
                {
                    log.LogMessage(MessageImportance.Normal, $"[Attempt #{retryCounter}] Failed to format XAML file '{path}' (exit code 0x{exitCode:X}).");
                }

                if (retryCounter != MaxRetriesCount)
                {
                    Thread.Sleep(RetryDelay);
                }
            }

            log.LogWarning($"Failed to format XAML file '{path}'.");
            return false;
        }

        private static bool ExecuteCommand(string command, string arguments, out string output, out int exitCode)
        {
            using var process = new Process();

            process.StartInfo = new ProcessStartInfo()
            {
                Arguments = arguments,
                FileName = command,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            process.Start();
            process.WaitForExit(XamlStylerToolExitTimeout);

            output = process.StandardOutput.ReadToEnd();
            exitCode = process.HasExited ? process.ExitCode : -1;

            return process.HasExited;
        }

        private const string DotnetCommand = "dotnet";
        private const string XamlStylerToolCommand = "xstyler";
        private const string FileParamName = "--file";
        private const string DirectoryParamName = "--directory";
        private const string RecursiveParamName = "--recursive";
        private const string LogLevelParamName = "--loglevel";
        private const int MaxRetriesCount = 5;
        private const int SuccessExitCode = 0;

        private static readonly TimeSpan RetryDelay = TimeSpan.FromMilliseconds(500);
        private static readonly int XamlStylerToolExitTimeout = (int)TimeSpan.FromSeconds(30).TotalMilliseconds;
    }
}