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

#pragma warning disable IDE0005 // Using directive is unnecessary.

global using Kaspirin.UI.Framework.Cache;
global using Kaspirin.UI.Framework.Cryptography;
global using Kaspirin.UI.Framework.Disposables;
global using Kaspirin.UI.Framework.Extensions;
global using Kaspirin.UI.Framework.Extensions.Collections;
global using Kaspirin.UI.Framework.Extensions.CultureInfos;
global using Kaspirin.UI.Framework.Extensions.DateTimes;
global using Kaspirin.UI.Framework.Extensions.Dictionaries;
global using Kaspirin.UI.Framework.Extensions.Doubles;
global using Kaspirin.UI.Framework.Extensions.Enumerables;
global using Kaspirin.UI.Framework.Extensions.Enums;
global using Kaspirin.UI.Framework.Extensions.Exceptions;
global using Kaspirin.UI.Framework.Extensions.Objects;
global using Kaspirin.UI.Framework.Extensions.SecureStrings;
global using Kaspirin.UI.Framework.Extensions.Strings;
global using Kaspirin.UI.Framework.Extensions.TimeSpans;
global using Kaspirin.UI.Framework.Guards;
global using Kaspirin.UI.Framework.Guards.InterpolatedStringHandlers;
global using Kaspirin.UI.Framework.IoC;
global using Kaspirin.UI.Framework.IoC.Injection;
global using Kaspirin.UI.Framework.IoC.Lifetime;
global using Kaspirin.UI.Framework.IoC.Overrides;
global using Kaspirin.UI.Framework.Log;
global using Kaspirin.UI.Framework.Log.InterpolatedStringHandlers;
global using Kaspirin.UI.Framework.Mvvm;
global using Kaspirin.UI.Framework.NativeMethods;
global using Kaspirin.UI.Framework.NativeMethods.Api.Advapi32;
global using Kaspirin.UI.Framework.NativeMethods.Api.Advapi32.Enums;
global using Kaspirin.UI.Framework.NativeMethods.Api.Advapi32.Structs;
global using Kaspirin.UI.Framework.NativeMethods.Api.Comctl32;
global using Kaspirin.UI.Framework.NativeMethods.Api.Comctl32.Constants;
global using Kaspirin.UI.Framework.NativeMethods.Api.Comctl32.Enums;
global using Kaspirin.UI.Framework.NativeMethods.Api.Comctl32.Interfaces;
global using Kaspirin.UI.Framework.NativeMethods.Api.Comctl32.Structs;
global using Kaspirin.UI.Framework.NativeMethods.Api.Cryptui;
global using Kaspirin.UI.Framework.NativeMethods.Api.Cryptui.Enums;
global using Kaspirin.UI.Framework.NativeMethods.Api.Cryptui.Structs;
global using Kaspirin.UI.Framework.NativeMethods.Api.Dwmapi;
global using Kaspirin.UI.Framework.NativeMethods.Api.Dwmapi.Enums;
global using Kaspirin.UI.Framework.NativeMethods.Api.Gdi32;
global using Kaspirin.UI.Framework.NativeMethods.Api.Gdi32.Enums;
global using Kaspirin.UI.Framework.NativeMethods.Api.IeFrame;
global using Kaspirin.UI.Framework.NativeMethods.Api.Kernel32;
global using Kaspirin.UI.Framework.NativeMethods.Api.Kernel32.Delegates;
global using Kaspirin.UI.Framework.NativeMethods.Api.Kernel32.Enums;
global using Kaspirin.UI.Framework.NativeMethods.Api.Kernel32.Structs;
global using Kaspirin.UI.Framework.NativeMethods.Api.Mpr;
global using Kaspirin.UI.Framework.NativeMethods.Api.Mpr.Enums;
global using Kaspirin.UI.Framework.NativeMethods.Api.Ntdll;
global using Kaspirin.UI.Framework.NativeMethods.Api.Ntdll.Enums;
global using Kaspirin.UI.Framework.NativeMethods.Api.Ntdll.Structs;
global using Kaspirin.UI.Framework.NativeMethods.Api.Shcore;
global using Kaspirin.UI.Framework.NativeMethods.Api.Shcore.Enums;
global using Kaspirin.UI.Framework.NativeMethods.Api.Shell32;
global using Kaspirin.UI.Framework.NativeMethods.Api.Shell32.Classes;
global using Kaspirin.UI.Framework.NativeMethods.Api.Shell32.Constants;
global using Kaspirin.UI.Framework.NativeMethods.Api.Shell32.Enums;
global using Kaspirin.UI.Framework.NativeMethods.Api.Shell32.Interfaces;
global using Kaspirin.UI.Framework.NativeMethods.Api.Shell32.Structs;
global using Kaspirin.UI.Framework.NativeMethods.Api.User32;
global using Kaspirin.UI.Framework.NativeMethods.Api.User32.Constants;
global using Kaspirin.UI.Framework.NativeMethods.Api.User32.Delegates;
global using Kaspirin.UI.Framework.NativeMethods.Api.User32.Enums;
global using Kaspirin.UI.Framework.NativeMethods.Api.User32.Structs;
global using Kaspirin.UI.Framework.NativeMethods.Api.Version;
global using Kaspirin.UI.Framework.NativeMethods.Api.Wininet;
global using Kaspirin.UI.Framework.NativeMethods.Api.Wininet.Enums;
global using Kaspirin.UI.Framework.NativeMethods.Api.Wininet.Structs;
global using Kaspirin.UI.Framework.NativeMethods.Api.Winspool;
global using Kaspirin.UI.Framework.NativeMethods.Api.Winspool.Enums;
global using Kaspirin.UI.Framework.NativeMethods.Common;
global using Kaspirin.UI.Framework.NativeMethods.SafeHandles;
global using Kaspirin.UI.Framework.NativeMethods.Utils;
global using Kaspirin.UI.Framework.Services;
global using Kaspirin.UI.Framework.Storage;
global using Kaspirin.UI.Framework.Storage.KeyValue;
global using Kaspirin.UI.Framework.SystemInfo;
global using Kaspirin.UI.Framework.Threading;
global using Kaspirin.UI.Framework.Weak;
global using Kaspirin.UI.Framework.WinRT;

#if IMPLICIT_USINGS_ENABLE

global using global::System;
global using global::System.Collections.Generic;
global using global::System.IO;
global using global::System.Linq;
global using global::System.Net.Http;
global using global::System.Threading;
global using global::System.Threading.Tasks;

#endif
