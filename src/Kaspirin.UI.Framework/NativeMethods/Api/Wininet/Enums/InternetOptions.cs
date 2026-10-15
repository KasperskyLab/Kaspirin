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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Wininet.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/wininet/option-flags">Learn more</seealso>.
/// </summary>
public enum InternetOptions : uint
{
    /// <summary>
    ///     The INTERNET_OPTION_CALLBACK constant.
    /// </summary>
    Callback = 1,

    /// <summary>
    ///     The INTERNET_OPTION_CONNECT_TIMEOUT constant.
    /// </summary>
    ConnectTimeout = 2,

    /// <summary>
    ///     The INTERNET_OPTION_CONNECT_RETRIES constant.
    /// </summary>
    ConnectRetries = 3,

    /// <summary>
    ///     The INTERNET_OPTION_CONNECT_BACKOFF constant.
    /// </summary>
    ConnectBackoff = 4,

    /// <summary>
    ///     The INTERNET_OPTION_SEND_TIMEOUT constant.
    /// </summary>
    SendTimeout = 5,

    /// <summary>
    ///     The INTERNET_OPTION_CONTROL_SEND_TIMEOUT constant.
    /// </summary>
    ControlSendTimeout = SendTimeout,

    /// <summary>
    ///     The INTERNET_OPTION_RECEIVE_TIMEOUT constant.
    /// </summary>
    ReceiveTimeout = 6,

    /// <summary>
    ///     The INTERNET_OPTION_CONTROL_RECEIVE_TIMEOUT constant.
    /// </summary>
    ControlReceiveTimeout = ReceiveTimeout,

    /// <summary>
    ///     The INTERNET_OPTION_DATA_SEND_TIMEOUT constant.
    /// </summary>
    DataSendTimeout = 7,

    /// <summary>
    ///     The INTERNET_OPTION_DATA_RECEIVE_TIMEOUT constant.
    /// </summary>
    DataReceiveTimeout = 8,

    /// <summary>
    ///     The INTERNET_OPTION_HANDLE_TYPE constant.
    /// </summary>
    HandleType = 9,

    /// <summary>
    ///     The INTERNET_OPTION_LISTEN_TIMEOUT constant.
    /// </summary>
    ListenTimeout = 11,

    /// <summary>
    ///     The INTERNET_OPTION_READ_BUFFER_SIZE constant.
    /// </summary>
    ReadBufferSize = 12,

    /// <summary>
    ///     The INTERNET_OPTION_WRITE_BUFFER_SIZE constant.
    /// </summary>
    WriteBufferSize = 13,

    /// <summary>
    ///     The INTERNET_OPTION_ASYNC_ID constant.
    /// </summary>
    AsyncId = 15,

    /// <summary>
    ///     The INTERNET_OPTION_ASYNC_PRIORITY constant.
    /// </summary>
    AsyncPriority = 16,

    /// <summary>
    ///     The INTERNET_OPTION_PARENT_HANDLE constant.
    /// </summary>
    ParentHandle = 21,

    /// <summary>
    ///     The INTERNET_OPTION_KEEP_CONNECTION constant.
    /// </summary>
    KeepConnection = 22,

    /// <summary>
    ///     The INTERNET_OPTION_REQUEST_FLAGS constant.
    /// </summary>
    RequestFlags = 23,

    /// <summary>
    ///     The INTERNET_OPTION_EXTENDED_ERROR constant.
    /// </summary>
    ExtendedError = 24,

    /// <summary>
    ///     The INTERNET_OPTION_OFFLINE_MODE constant.
    /// </summary>
    OfflineMode = 26,

    /// <summary>
    ///     The INTERNET_OPTION_CACHE_STREAM_HANDLE constant.
    /// </summary>
    CacheStreamHandle = 27,

    /// <summary>
    ///     The INTERNET_OPTION_USERNAME constant.
    /// </summary>
    Username = 28,

    /// <summary>
    ///     The INTERNET_OPTION_PASSWORD constant.
    /// </summary>
    Password = 29,

    /// <summary>
    ///     The INTERNET_OPTION_ASYNC constant.
    /// </summary>
    Async = 30,

    /// <summary>
    ///     The INTERNET_OPTION_SECURITY_FLAGS constant.
    /// </summary>
    SecurityFlags = 31,

    /// <summary>
    ///     The INTERNET_OPTION_SECURITY_CERTIFICATE_STRUCT constant.
    /// </summary>
    SecurityCertificateStruct = 32,

    /// <summary>
    ///     The INTERNET_OPTION_DATAFILE_NAME constant.
    /// </summary>
    DatafileName = 33,

    /// <summary>
    ///     The INTERNET_OPTION_URL constant.
    /// </summary>
    Url = 34,

    /// <summary>
    ///     The INTERNET_OPTION_SECURITY_CERTIFICATE constant.
    /// </summary>
    SecurityCertificate = 35,

    /// <summary>
    ///     The INTERNET_OPTION_SECURITY_KEY_BITNESS constant.
    /// </summary>
    SecurityKeyBitness = 36,

    /// <summary>
    ///     The INTERNET_OPTION_REFRESH constant.
    /// </summary>
    Refresh = 37,

    /// <summary>
    ///     The INTERNET_OPTION_PROXY constant.
    /// </summary>
    Proxy = 38,

    /// <summary>
    ///     The INTERNET_OPTION_SETTINGS_CHANGED constant.
    /// </summary>
    SettingsChanged = 39,

    /// <summary>
    ///     The INTERNET_OPTION_VERSION constant.
    /// </summary>
    Version = 40,

    /// <summary>
    ///     The INTERNET_OPTION_USER_AGENT constant.
    /// </summary>
    UserAgent = 41,

    /// <summary>
    ///     The INTERNET_OPTION_END_BROWSER_SESSION constant.
    /// </summary>
    EndBrowserSession = 42,

    /// <summary>
    ///     The INTERNET_OPTION_PROXY_USERNAME constant.
    /// </summary>
    ProxyUsername = 43,

    /// <summary>
    ///     The INTERNET_OPTION_PROXY_PASSWORD constant.
    /// </summary>
    ProxyPassword = 44,

    /// <summary>
    ///     The INTERNET_OPTION_CONTEXT_VALUE constant.
    /// </summary>
    ContextValue = 45,

    /// <summary>
    ///     The INTERNET_OPTION_CONNECT_LIMIT constant.
    /// </summary>
    ConnectLimit = 46,

    /// <summary>
    ///     The INTERNET_OPTION_SECURITY_SELECT_CLIENT_CERT constant.
    /// </summary>
    SecuritySelectClientCert = 47,

    /// <summary>
    ///     The INTERNET_OPTION_POLICY constant.
    /// </summary>
    Policy = 48,

    /// <summary>
    ///     The INTERNET_OPTION_DISCONNECTED_TIMEOUT constant.
    /// </summary>
    DisconnectedTimeout = 49,

    /// <summary>
    ///     The INTERNET_OPTION_CONNECTED_STATE constant.
    /// </summary>
    ConnectedState = 50,

    /// <summary>
    ///     The INTERNET_OPTION_IDLE_STATE constant.
    /// </summary>
    IdleState = 51,

    /// <summary>
    ///     The INTERNET_OPTION_OFFLINE_SEMANTICS constant.
    /// </summary>
    OfflineSemantics = 52,

    /// <summary>
    ///     The INTERNET_OPTION_SECONDARY_CACHE_KEY constant.
    /// </summary>
    SecondaryCacheKey = 53,

    /// <summary>
    ///     The INTERNET_OPTION_CALLBACK_FILTER constant.
    /// </summary>
    CallbackFilter = 54,

    /// <summary>
    ///     The INTERNET_OPTION_CONNECT_TIME constant.
    /// </summary>
    ConnectTime = 55,

    /// <summary>
    ///     The INTERNET_OPTION_SEND_THROUGHPUT constant.
    /// </summary>
    SendThroughput = 56,

    /// <summary>
    ///     The INTERNET_OPTION_RECEIVE_THROUGHPUT constant.
    /// </summary>
    ReceiveThroughput = 57,

    /// <summary>
    ///     The INTERNET_OPTION_REQUEST_PRIORITY constant.
    /// </summary>
    RequestPriority = 58,

    /// <summary>
    ///     The INTERNET_OPTION_HTTP_VERSION constant.
    /// </summary>
    HttpVersion = 59,

    /// <summary>
    ///     The INTERNET_OPTION_RESET_URLCACHE_SESSION constant.
    /// </summary>
    ResetUrlCacheSession = 60,

    /// <summary>
    ///     The INTERNET_OPTION_ERROR_MASK constant.
    /// </summary>
    ErrorMask = 62,

    /// <summary>
    ///     The INTERNET_OPTION_FROM_CACHE_TIMEOUT constant.
    /// </summary>
    FromCacheTimeout = 63,

    /// <summary>
    ///     The INTERNET_OPTION_BYPASS_EDITED_ENTRY constant.
    /// </summary>
    BypassEditedEntry = 64,

    /// <summary>
    ///     The INTERNET_OPTION_DIAGNOSTIC_SOCKET_INFO constant.
    /// </summary>
    DiagnosticSocketInfo = 67,

    /// <summary>
    ///     The INTERNET_OPTION_CODEPAGE constant.
    /// </summary>
    Codepage = 68,

    /// <summary>
    ///     The INTERNET_OPTION_CACHE_TIMESTAMPS constant.
    /// </summary>
    CacheTimestamps = 69,

    /// <summary>
    ///     The INTERNET_OPTION_DISABLE_AUTODIAL constant.
    /// </summary>
    DisableAutodial = 70,

    /// <summary>
    ///     The INTERNET_OPTION_MAX_CONNS_PER_SERVER constant.
    /// </summary>
    MaxConnsPerServer = 73,

    /// <summary>
    ///     The INTERNET_OPTION_MAX_CONNS_PER_1_0_SERVER constant.
    /// </summary>
    MaxConnsPer10Server = 74,

    /// <summary>
    ///     The INTERNET_OPTION_PER_CONNECTION_OPTION constant.
    /// </summary>
    PerConnectionOption = 75,

    /// <summary>
    ///     The INTERNET_OPTION_DIGEST_AUTH_UNLOAD constant.
    /// </summary>
    DigestAuthUnload = 76,

    /// <summary>
    ///     The INTERNET_OPTION_IGNORE_OFFLINE constant.
    /// </summary>
    IgnoreOffline = 77,

    /// <summary>
    ///     The INTERNET_OPTION_IDENTITY constant.
    /// </summary>
    Identity = 78,

    /// <summary>
    ///     The INTERNET_OPTION_REMOVE_IDENTITY constant.
    /// </summary>
    RemoveIdentity = 79,

    /// <summary>
    ///     The INTERNET_OPTION_ALTER_IDENTITY constant.
    /// </summary>
    AlterIdentity = 80,

    /// <summary>
    ///     The INTERNET_OPTION_SUPPRESS_BEHAVIOR constant.
    /// </summary>
    SuppressBehavior = 81,

    /// <summary>
    ///     The INTERNET_OPTION_AUTODIAL_MODE constant.
    /// </summary>
    AutodialMode = 82,

    /// <summary>
    ///     The INTERNET_OPTION_AUTODIAL_CONNECTION constant.
    /// </summary>
    AutodialConnection = 83,

    /// <summary>
    ///     The INTERNET_OPTION_CLIENT_CERT_CONTEXT constant.
    /// </summary>
    ClientCertContext = 84,

    /// <summary>
    ///     The INTERNET_OPTION_AUTH_FLAGS constant.
    /// </summary>
    AuthFlags = 85,

    /// <summary>
    ///     The INTERNET_OPTION_COOKIES_3RD_PARTY constant.
    /// </summary>
    Cookies3RdParty = 86,

    /// <summary>
    ///     The INTERNET_OPTION_DISABLE_PASSPORT_AUTH constant.
    /// </summary>
    DisablePassportAuth = 87,

    /// <summary>
    ///     The INTERNET_OPTION_SEND_UTF8_SERVERNAME_TO_PROXY constant.
    /// </summary>
    SendUtf8ServerNameToProxy = 88,

    /// <summary>
    ///     The INTERNET_OPTION_EXEMPT_CONNECTION_LIMIT constant.
    /// </summary>
    ExemptConnectionLimit = 89,

    /// <summary>
    ///     The INTERNET_OPTION_ENABLE_PASSPORT_AUTH constant.
    /// </summary>
    EnablePassportAuth = 90,

    /// <summary>
    ///     The INTERNET_OPTION_HIBERNATE_INACTIVE_WORKER_THREADS constant.
    /// </summary>
    HibernateInactiveWorkerThreads = 91,

    /// <summary>
    ///     The INTERNET_OPTION_ACTIVATE_WORKER_THREADS constant.
    /// </summary>
    ActivateWorkerThreads = 92,

    /// <summary>
    ///     The INTERNET_OPTION_RESTORE_WORKER_THREAD_DEFAULTS constant.
    /// </summary>
    RestoreWorkerThreadDefaults = 93,

    /// <summary>
    ///     The INTERNET_OPTION_SOCKET_SEND_BUFFER_LENGTH constant.
    /// </summary>
    SocketSendBufferLength = 94,

    /// <summary>
    ///     The INTERNET_OPTION_PROXY_SETTINGS_CHANGED constant.
    /// </summary>
    ProxySettingsChanged = 95,

    /// <summary>
    ///     The INTERNET_OPTION_DATAFILE_EXT constant.
    /// </summary>
    DatafileExt = 96,

    /// <summary>
    ///     The INTERNET_FIRST_OPTION constant.
    /// </summary>
    InternetFirstOption = Callback,

    /// <summary>
    ///     The INTERNET_LAST_OPTION constant.
    /// </summary>
    InternetLastOption = DatafileExt
}