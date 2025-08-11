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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Ntdll.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/596a1078-e883-4972-9bbc-49e60bebca55">Learn more</seealso>.
/// </summary>
public enum NtStatus : uint
{

    /// <summary>
    ///     The STATUS_SUCCESS constant.
    /// </summary>
    StatusSuccess = 0x00000000,

    /// <summary>
    ///     The STATUS_WAIT_0 constant.
    /// </summary>
    StatusWait0 = 0x00000000,

    /// <summary>
    ///     The STATUS_WAIT_1 constant.
    /// </summary>
    StatusWait1 = 0x00000001,

    /// <summary>
    ///     The STATUS_WAIT_2 constant.
    /// </summary>
    StatusWait2 = 0x00000002,

    /// <summary>
    ///     The STATUS_WAIT_3 constant.
    /// </summary>
    StatusWait3 = 0x00000003,

    /// <summary>
    ///     The STATUS_WAIT_63 constant.
    /// </summary>
    StatusWait63 = 0x0000003F,

    /// <summary>
    ///     The STATUS_ABANDONED constant.
    /// </summary>
    StatusAbandoned = 0x00000080,

    /// <summary>
    ///     The STATUS_ABANDONED_WAIT_0 constant.
    /// </summary>
    StatusAbandonedWait0 = 0x00000080,

    /// <summary>
    ///     The STATUS_ABANDONED_WAIT_63 constant.
    /// </summary>
    StatusAbandonedWait63 = 0x000000BF,

    /// <summary>
    ///     The STATUS_USER_APC constant.
    /// </summary>
    StatusUserApc = 0x000000C0,

    /// <summary>
    ///     The STATUS_ALERTED constant.
    /// </summary>
    StatusAlerted = 0x00000101,

    /// <summary>
    ///     The STATUS_TIMEOUT constant.
    /// </summary>
    StatusTimeout = 0x00000102,

    /// <summary>
    ///     The STATUS_PENDING constant.
    /// </summary>
    StatusPending = 0x00000103,

    /// <summary>
    ///     The STATUS_REPARSE constant.
    /// </summary>
    StatusReparse = 0x00000104,

    /// <summary>
    ///     The STATUS_MORE_ENTRIES constant.
    /// </summary>
    StatusMoreEntries = 0x00000105,

    /// <summary>
    ///     The STATUS_NOT_ALL_ASSIGNED constant.
    /// </summary>
    StatusNotAllAssigned = 0x00000106,

    /// <summary>
    ///     The STATUS_SOME_NOT_MAPPED constant.
    /// </summary>
    StatusSomeNotMapped = 0x00000107,

    /// <summary>
    ///     The STATUS_OPLOCK_BREAK_IN_PROGRESS constant.
    /// </summary>
    StatusOplockBreakInProgress = 0x00000108,

    /// <summary>
    ///     The STATUS_VOLUME_MOUNTED constant.
    /// </summary>
    StatusVolumeMounted = 0x00000109,

    /// <summary>
    ///     The STATUS_RXACT_COMMITTED constant.
    /// </summary>
    StatusRxActCommitted = 0x0000010A,

    /// <summary>
    ///     The STATUS_NOTIFY_CLEANUP constant.
    /// </summary>
    StatusNotifyCleanup = 0x0000010B,

    /// <summary>
    ///     The STATUS_NOTIFY_ENUM_DIR constant.
    /// </summary>
    StatusNotifyEnumDir = 0x0000010C,

    /// <summary>
    ///     The STATUS_NO_QUOTAS_FOR_ACCOUNT constant.
    /// </summary>
    StatusNoQuotasForAccount = 0x0000010D,

    /// <summary>
    ///     The STATUS_PRIMARY_TRANSPORT_CONNECT_FAILED constant.
    /// </summary>
    StatusPrimaryTransportConnectFailed = 0x0000010E,

    /// <summary>
    ///     The STATUS_PAGE_FAULT_TRANSITION constant.
    /// </summary>
    StatusPageFaultTransition = 0x00000110,

    /// <summary>
    ///     The STATUS_PAGE_FAULT_DEMAND_ZERO constant.
    /// </summary>
    StatusPageFaultDemandZero = 0x00000111,

    /// <summary>
    ///     The STATUS_PAGE_FAULT_COPY_ON_WRITE constant.
    /// </summary>
    StatusPageFaultCopyOnWrite = 0x00000112,

    /// <summary>
    ///     The STATUS_PAGE_FAULT_GUARD_PAGE constant.
    /// </summary>
    StatusPageFaultGuardPage = 0x00000113,

    /// <summary>
    ///     The STATUS_PAGE_FAULT_PAGING_FILE constant.
    /// </summary>
    StatusPageFaultPagingFile = 0x00000114,

    /// <summary>
    ///     The STATUS_CACHE_PAGE_LOCKED constant.
    /// </summary>
    StatusCachePageLocked = 0x00000115,

    /// <summary>
    ///     The STATUS_CRASH_DUMP constant.
    /// </summary>
    StatusCrashDump = 0x00000116,

    /// <summary>
    ///     The STATUS_BUFFER_ALL_ZEROS constant.
    /// </summary>
    StatusBufferAllZeros = 0x00000117,

    /// <summary>
    ///     The STATUS_REPARSE_OBJECT constant.
    /// </summary>
    StatusReparseObject = 0x00000118,

    /// <summary>
    ///     The STATUS_RESOURCE_REQUIREMENTS_CHANGED constant.
    /// </summary>
    StatusResourceRequirementsChanged = 0x00000119,

    /// <summary>
    ///     The STATUS_TRANSLATION_COMPLETE constant.
    /// </summary>
    StatusTranslationComplete = 0x00000120,

    /// <summary>
    ///     The STATUS_DS_MEMBERSHIP_EVALUATED_LOCALLY constant.
    /// </summary>
    StatusDsMembershipEvaluatedLocally = 0x00000121,

    /// <summary>
    ///     The STATUS_NOTHING_TO_TERMINATE constant.
    /// </summary>
    StatusNothingToTerminate = 0x00000122,

    /// <summary>
    ///     The STATUS_PROCESS_NOT_IN_JOB constant.
    /// </summary>
    StatusProcessNotInJob = 0x00000123,

    /// <summary>
    ///     The STATUS_PROCESS_IN_JOB constant.
    /// </summary>
    StatusProcessInJob = 0x00000124,

    /// <summary>
    ///     The STATUS_VOLSNAP_HIBERNATE_READY constant.
    /// </summary>
    StatusVolSnapHibernateReady = 0x00000125,

    /// <summary>
    ///     The STATUS_FSFILTER_OP_COMPLETED_SUCCESSFULLY constant.
    /// </summary>
    StatusFsFilterOpCompletedSuccessfully = 0x00000126,

    /// <summary>
    ///     The STATUS_INTERRUPT_VECTOR_ALREADY_CONNECTED constant.
    /// </summary>
    StatusInterruptVectorAlreadyConnected = 0x00000127,

    /// <summary>
    ///     The STATUS_INTERRUPT_STILL_CONNECTED constant.
    /// </summary>
    StatusInterruptStillConnected = 0x00000128,

    /// <summary>
    ///     The STATUS_PROCESS_CLONED constant.
    /// </summary>
    StatusProcessCloned = 0x00000129,

    /// <summary>
    ///     The STATUS_FILE_LOCKED_WITH_ONLY_READERS constant.
    /// </summary>
    StatusFileLockedWithOnlyReaders = 0x0000012A,

    /// <summary>
    ///     The STATUS_FILE_LOCKED_WITH_WRITERS constant.
    /// </summary>
    StatusFileLockedWithWriters = 0x0000012B,

    /// <summary>
    ///     The STATUS_RESOURCEMANAGER_READ_ONLY constant.
    /// </summary>
    StatusResourceManagerReadOnly = 0x00000202,

    /// <summary>
    ///     The STATUS_WAIT_FOR_OPLOCK constant.
    /// </summary>
    StatusWaitForOplock = 0x00000367,

    /// <summary>
    ///     The DBG_EXCEPTION_HANDLED constant.
    /// </summary>
    DbgExceptionHandled = 0x00010001,

    /// <summary>
    ///     The DBG_CONTINUE constant.
    /// </summary>
    DbgContinue = 0x00010002,

    /// <summary>
    ///     The STATUS_FLT_IO_COMPLETE constant.
    /// </summary>
    StatusFltIoComplete = 0x001C0001,

    /// <summary>
    ///     The STATUS_FILE_NOT_AVAILABLE constant.
    /// </summary>
    StatusFileNotAvailable = 0xC0000467,

    /// <summary>
    ///     The STATUS_SHARE_UNAVAILABLE constant.
    /// </summary>
    StatusShareUnavailable = 0xC0000480,

    /// <summary>
    ///     The STATUS_CALLBACK_RETURNED_THREAD_AFFINITY constant.
    /// </summary>
    StatusCallbackReturnedThreadAffinity = 0xC0000721,

    /// <summary>
    ///     The STATUS_OBJECT_NAME_EXISTS constant.
    /// </summary>
    StatusObjectNameExists = 0x40000000,

    /// <summary>
    ///     The STATUS_THREAD_WAS_SUSPENDED constant.
    /// </summary>
    StatusThreadWasSuspended = 0x40000001,

    /// <summary>
    ///     The STATUS_WORKING_SET_LIMIT_RANGE constant.
    /// </summary>
    StatusWorkingSetLimitRange = 0x40000002,

    /// <summary>
    ///     The STATUS_IMAGE_NOT_AT_BASE constant.
    /// </summary>
    StatusImageNotAtBase = 0x40000003,

    /// <summary>
    ///     The STATUS_RXACT_STATE_CREATED constant.
    /// </summary>
    StatusRxActStateCreated = 0x40000004,

    /// <summary>
    ///     The STATUS_SEGMENT_NOTIFICATION constant.
    /// </summary>
    StatusSegmentNotification = 0x40000005,

    /// <summary>
    ///     The STATUS_LOCAL_USER_SESSION_KEY constant.
    /// </summary>
    StatusLocalUserSessionKey = 0x40000006,

    /// <summary>
    ///     The STATUS_BAD_CURRENT_DIRECTORY constant.
    /// </summary>
    StatusBadCurrentDirectory = 0x40000007,

    /// <summary>
    ///     The STATUS_SERIAL_MORE_WRITES constant.
    /// </summary>
    StatusSerialMoreWrites = 0x40000008,

    /// <summary>
    ///     The STATUS_REGISTRY_RECOVERED constant.
    /// </summary>
    StatusRegistryRecovered = 0x40000009,

    /// <summary>
    ///     The STATUS_FT_READ_RECOVERY_FROM_BACKUP constant.
    /// </summary>
    StatusFtReadRecoveryFromBackup = 0x4000000A,

    /// <summary>
    ///     The STATUS_FT_WRITE_RECOVERY constant.
    /// </summary>
    StatusFtWriteRecovery = 0x4000000B,

    /// <summary>
    ///     The STATUS_SERIAL_COUNTER_TIMEOUT constant.
    /// </summary>
    StatusSerialCounterTimeout = 0x4000000C,

    /// <summary>
    ///     The STATUS_NULL_LM_PASSWORD constant.
    /// </summary>
    StatusNullLmPassword = 0x4000000D,

    /// <summary>
    ///     The STATUS_IMAGE_MACHINE_TYPE_MISMATCH constant.
    /// </summary>
    StatusImageMachineTypeMismatch = 0x4000000E,

    /// <summary>
    ///     The STATUS_RECEIVE_PARTIAL constant.
    /// </summary>
    StatusReceivePartial = 0x4000000F,

    /// <summary>
    ///     The STATUS_RECEIVE_EXPEDITED constant.
    /// </summary>
    StatusReceiveExpedited = 0x40000010,

    /// <summary>
    ///     The STATUS_RECEIVE_PARTIAL_EXPEDITED constant.
    /// </summary>
    StatusReceivePartialExpedited = 0x40000011,

    /// <summary>
    ///     The STATUS_EVENT_DONE constant.
    /// </summary>
    StatusEventDone = 0x40000012,

    /// <summary>
    ///     The STATUS_EVENT_PENDING constant.
    /// </summary>
    StatusEventPending = 0x40000013,

    /// <summary>
    ///     The STATUS_CHECKING_FILE_SYSTEM constant.
    /// </summary>
    StatusCheckingFileSystem = 0x40000014,

    /// <summary>
    ///     The STATUS_FATAL_APP_EXIT constant.
    /// </summary>
    StatusFatalAppExit = 0x40000015,

    /// <summary>
    ///     The STATUS_PREDEFINED_HANDLE constant.
    /// </summary>
    StatusPredefinedHandle = 0x40000016,

    /// <summary>
    ///     The STATUS_WAS_UNLOCKED constant.
    /// </summary>
    StatusWasUnlocked = 0x40000017,

    /// <summary>
    ///     The STATUS_SERVICE_NOTIFICATION constant.
    /// </summary>
    StatusServiceNotification = 0x40000018,

    /// <summary>
    ///     The STATUS_WAS_LOCKED constant.
    /// </summary>
    StatusWasLocked = 0x40000019,

    /// <summary>
    ///     The STATUS_LOG_HARD_ERROR constant.
    /// </summary>
    StatusLogHardError = 0x4000001A,

    /// <summary>
    ///     The STATUS_ALREADY_WIN32 constant.
    /// </summary>
    StatusAlreadyWin32 = 0x4000001B,

    /// <summary>
    ///     The STATUS_WX86_UNSIMULATE constant.
    /// </summary>
    StatusWx86UnSimulate = 0x4000001C,

    /// <summary>
    ///     The STATUS_WX86_CONTINUE constant.
    /// </summary>
    StatusWx86Continue = 0x4000001D,

    /// <summary>
    ///     The STATUS_WX86_SINGLE_STEP constant.
    /// </summary>
    StatusWx86SingleStep = 0x4000001E,

    /// <summary>
    ///     The STATUS_WX86_BREAKPOINT constant.
    /// </summary>
    StatusWx86Breakpoint = 0x4000001F,

    /// <summary>
    ///     The STATUS_WX86_EXCEPTION_CONTINUE constant.
    /// </summary>
    StatusWx86ExceptionContinue = 0x40000020,

    /// <summary>
    ///     The STATUS_WX86_EXCEPTION_LASTCHANCE constant.
    /// </summary>
    StatusWx86ExceptionLastChance = 0x40000021,

    /// <summary>
    ///     The STATUS_WX86_EXCEPTION_CHAIN constant.
    /// </summary>
    StatusWx86ExceptionChain = 0x40000022,

    /// <summary>
    ///     The STATUS_IMAGE_MACHINE_TYPE_MISMATCH_EXE constant.
    /// </summary>
    StatusImageMachineTypeMismatchExe = 0x40000023,

    /// <summary>
    ///     The STATUS_NO_YIELD_PERFORMED constant.
    /// </summary>
    StatusNoYieldPerformed = 0x40000024,

    /// <summary>
    ///     The STATUS_TIMER_RESUME_IGNORED constant.
    /// </summary>
    StatusTimerResumeIgnored = 0x40000025,

    /// <summary>
    ///     The STATUS_ARBITRATION_UNHANDLED constant.
    /// </summary>
    StatusArbitrationUnhandled = 0x40000026,

    /// <summary>
    ///     The STATUS_CARDBUS_NOT_SUPPORTED constant.
    /// </summary>
    StatusCardBusNotSupported = 0x40000027,

    /// <summary>
    ///     The STATUS_WX86_CREATEWX86TIB constant.
    /// </summary>
    StatusWx86CreateWx86tib = 0x40000028,

    /// <summary>
    ///     The STATUS_MP_PROCESSOR_MISMATCH constant.
    /// </summary>
    StatusMpProcessorMismatch = 0x40000029,

    /// <summary>
    ///     The STATUS_HIBERNATED constant.
    /// </summary>
    StatusHibernated = 0x4000002A,

    /// <summary>
    ///     The STATUS_RESUME_HIBERNATION constant.
    /// </summary>
    StatusResumeHibernation = 0x4000002B,

    /// <summary>
    ///     The STATUS_FIRMWARE_UPDATED constant.
    /// </summary>
    StatusFirmwareUpdated = 0x4000002C,

    /// <summary>
    ///     The STATUS_DRIVERS_LEAKING_LOCKED_PAGES constant.
    /// </summary>
    StatusDriversLeakingLockedPages = 0x4000002D,

    /// <summary>
    ///     The STATUS_MESSAGE_RETRIEVED constant.
    /// </summary>
    StatusMessageRetrieved = 0x4000002E,

    /// <summary>
    ///     The STATUS_SYSTEM_POWERSTATE_TRANSITION constant.
    /// </summary>
    StatusSystemPowerStateTransition = 0x4000002F,

    /// <summary>
    ///     The STATUS_ALPC_CHECK_COMPLETION_LIST constant.
    /// </summary>
    StatusAlpcCheckCompletionList = 0x40000030,

    /// <summary>
    ///     The STATUS_SYSTEM_POWERSTATE_COMPLEX_TRANSITION constant.
    /// </summary>
    StatusSystemPowerStateComplexTransition = 0x40000031,

    /// <summary>
    ///     The STATUS_ACCESS_AUDIT_BY_POLICY constant.
    /// </summary>
    StatusAccessAuditByPolicy = 0x40000032,

    /// <summary>
    ///     The STATUS_ABANDON_HIBERFILE constant.
    /// </summary>
    StatusAbandonHiberFile = 0x40000033,

    /// <summary>
    ///     The STATUS_BIZRULES_NOT_ENABLED constant.
    /// </summary>
    StatusBizRulesNotEnabled = 0x40000034,

    /// <summary>
    ///     The STATUS_WAKE_SYSTEM constant.
    /// </summary>
    StatusWakeSystem = 0x40000294,

    /// <summary>
    ///     The STATUS_DS_SHUTTING_DOWN constant.
    /// </summary>
    StatusDsShuttingDown = 0x40000370,

    /// <summary>
    ///     The DBG_REPLY_LATER constant.
    /// </summary>
    DbgReplyLater = 0x40010001,

    /// <summary>
    ///     The DBG_UNABLE_TO_PROVIDE_HANDLE constant.
    /// </summary>
    DbgUnableToProvideHandle = 0x40010002,

    /// <summary>
    ///     The DBG_TERMINATE_THREAD constant.
    /// </summary>
    DbgTerminateThread = 0x40010003,

    /// <summary>
    ///     The DBG_TERMINATE_PROCESS constant.
    /// </summary>
    DbgTerminateProcess = 0x40010004,

    /// <summary>
    ///     The DBG_CONTROL_C constant.
    /// </summary>
    DbgControlC = 0x40010005,

    /// <summary>
    ///     The DBG_PRINTEXCEPTION_C constant.
    /// </summary>
    DbgPrintExceptionC = 0x40010006,

    /// <summary>
    ///     The DBG_RIPEXCEPTION constant.
    /// </summary>
    DbgRipException = 0x40010007,

    /// <summary>
    ///     The DBG_CONTROL_BREAK constant.
    /// </summary>
    DbgControlBreak = 0x40010008,

    /// <summary>
    ///     The DBG_COMMAND_EXCEPTION constant.
    /// </summary>
    DbgCommandException = 0x40010009,

    /// <summary>
    ///     The RPC_NT_UUID_LOCAL_ONLY constant.
    /// </summary>
    RpcNtUuidLocalOnly = 0x40020056,

    /// <summary>
    ///     The RPC_NT_SEND_INCOMPLETE constant.
    /// </summary>
    RpcNtSendIncomplete = 0x400200AF,

    /// <summary>
    ///     The STATUS_CTX_CDM_CONNECT constant.
    /// </summary>
    StatusCtxCdmConnect = 0x400A0004,

    /// <summary>
    ///     The STATUS_CTX_CDM_DISCONNECT constant.
    /// </summary>
    StatusCtxCdmDisconnect = 0x400A0005,

    /// <summary>
    ///     The STATUS_SXS_RELEASE_ACTIVATION_CONTEXT constant.
    /// </summary>
    StatusSxsReleaseActivationContext = 0x4015000D,

    /// <summary>
    ///     The STATUS_RECOVERY_NOT_NEEDED constant.
    /// </summary>
    StatusRecoveryNotNeeded = 0x40190034,

    /// <summary>
    ///     The STATUS_RM_ALREADY_STARTED constant.
    /// </summary>
    StatusRmAlreadyStarted = 0x40190035,

    /// <summary>
    ///     The STATUS_LOG_NO_RESTART constant.
    /// </summary>
    StatusLogNoRestart = 0x401A000C,

    /// <summary>
    ///     The STATUS_VIDEO_DRIVER_DEBUG_REPORT_REQUEST constant.
    /// </summary>
    StatusVideoDriverDebugReportRequest = 0x401B00EC,

    /// <summary>
    ///     The STATUS_GRAPHICS_PARTIAL_DATA_POPULATED constant.
    /// </summary>
    StatusGraphicsPartialDataPopulated = 0x401E000A,

    /// <summary>
    ///     The STATUS_GRAPHICS_DRIVER_MISMATCH constant.
    /// </summary>
    StatusGraphicsDriverMismatch = 0x401E0117,

    /// <summary>
    ///     The STATUS_GRAPHICS_MODE_NOT_PINNED constant.
    /// </summary>
    StatusGraphicsModeNotPinned = 0x401E0307,

    /// <summary>
    ///     The STATUS_GRAPHICS_NO_PREFERRED_MODE constant.
    /// </summary>
    StatusGraphicsNoPreferredMode = 0x401E031E,

    /// <summary>
    ///     The STATUS_GRAPHICS_DATASET_IS_EMPTY constant.
    /// </summary>
    StatusGraphicsDatasetIsEmpty = 0x401E034B,

    /// <summary>
    ///     The STATUS_GRAPHICS_NO_MORE_ELEMENTS_IN_DATASET constant.
    /// </summary>
    StatusGraphicsNoMoreElementsInDataset = 0x401E034C,

    /// <summary>
    ///     The STATUS_GRAPHICS_PATH_CONTENT_GEOMETRY_TRANSFORMATION_NOT_PINNED constant.
    /// </summary>
    StatusGraphicsPathContentGeometryTransformationNotPinned = 0x401E0351,

    /// <summary>
    ///     The STATUS_GRAPHICS_UNKNOWN_CHILD_STATUS constant.
    /// </summary>
    StatusGraphicsUnknownChildStatus = 0x401E042F,

    /// <summary>
    ///     The STATUS_GRAPHICS_LEADLINK_START_DEFERRED constant.
    /// </summary>
    StatusGraphicsLeadLinkStartDeferred = 0x401E0437,

    /// <summary>
    ///     The STATUS_GRAPHICS_POLLING_TOO_FREQUENTLY constant.
    /// </summary>
    StatusGraphicsPollingTooFrequently = 0x401E0439,

    /// <summary>
    ///     The STATUS_GRAPHICS_START_DEFERRED constant.
    /// </summary>
    StatusGraphicsStartDeferred = 0x401E043A,

    /// <summary>
    ///     The STATUS_NDIS_INDICATION_REQUIRED constant.
    /// </summary>
    StatusNdisIndicationRequired = 0x40230001,

    /// <summary>
    ///     The STATUS_GUARD_PAGE_VIOLATION constant.
    /// </summary>
    StatusGuardPageViolation = 0x80000001,

    /// <summary>
    ///     The STATUS_DATATYPE_MISALIGNMENT constant.
    /// </summary>
    StatusDatatypeMisalignment = 0x80000002,

    /// <summary>
    ///     The STATUS_BREAKPOINT constant.
    /// </summary>
    StatusBreakpoint = 0x80000003,

    /// <summary>
    ///     The STATUS_SINGLE_STEP constant.
    /// </summary>
    StatusSingleStep = 0x80000004,

    /// <summary>
    ///     The STATUS_BUFFER_OVERFLOW constant.
    /// </summary>
    StatusBufferOverflow = 0x80000005,

    /// <summary>
    ///     The STATUS_NO_MORE_FILES constant.
    /// </summary>
    StatusNoMoreFiles = 0x80000006,

    /// <summary>
    ///     The STATUS_WAKE_SYSTEM_DEBUGGER constant.
    /// </summary>
    StatusWakeSystemDebugger = 0x80000007,

    /// <summary>
    ///     The STATUS_HANDLES_CLOSED constant.
    /// </summary>
    StatusHandlesClosed = 0x8000000A,

    /// <summary>
    ///     The STATUS_NO_INHERITANCE constant.
    /// </summary>
    StatusNoInheritance = 0x8000000B,

    /// <summary>
    ///     The STATUS_GUID_SUBSTITUTION_MADE constant.
    /// </summary>
    StatusGuidSubstitutionMade = 0x8000000C,

    /// <summary>
    ///     The STATUS_PARTIAL_COPY constant.
    /// </summary>
    StatusPartialCopy = 0x8000000D,

    /// <summary>
    ///     The STATUS_DEVICE_PAPER_EMPTY constant.
    /// </summary>
    StatusDevicePaperEmpty = 0x8000000E,

    /// <summary>
    ///     The STATUS_DEVICE_POWERED_OFF constant.
    /// </summary>
    StatusDevicePoweredOff = 0x8000000F,

    /// <summary>
    ///     The STATUS_DEVICE_OFF_LINE constant.
    /// </summary>
    StatusDeviceOffLine = 0x80000010,

    /// <summary>
    ///     The STATUS_DEVICE_BUSY constant.
    /// </summary>
    StatusDeviceBusy = 0x80000011,

    /// <summary>
    ///     The STATUS_NO_MORE_EAS constant.
    /// </summary>
    StatusNoMoreEas = 0x80000012,

    /// <summary>
    ///     The STATUS_INVALID_EA_NAME constant.
    /// </summary>
    StatusInvalidEaName = 0x80000013,

    /// <summary>
    ///     The STATUS_EA_LIST_INCONSISTENT constant.
    /// </summary>
    StatusEaListInconsistent = 0x80000014,

    /// <summary>
    ///     The STATUS_INVALID_EA_FLAG constant.
    /// </summary>
    StatusInvalidEaFlag = 0x80000015,

    /// <summary>
    ///     The STATUS_VERIFY_REQUIRED constant.
    /// </summary>
    StatusVerifyRequired = 0x80000016,

    /// <summary>
    ///     The STATUS_EXTRANEOUS_INFORMATION constant.
    /// </summary>
    StatusExtraneousInformation = 0x80000017,

    /// <summary>
    ///     The STATUS_RXACT_COMMIT_NECESSARY constant.
    /// </summary>
    StatusRxActCommitNecessary = 0x80000018,

    /// <summary>
    ///     The STATUS_NO_MORE_ENTRIES constant.
    /// </summary>
    StatusNoMoreEntries = 0x8000001A,

    /// <summary>
    ///     The STATUS_FILEMARK_DETECTED constant.
    /// </summary>
    StatusFileMarkDetected = 0x8000001B,

    /// <summary>
    ///     The STATUS_MEDIA_CHANGED constant.
    /// </summary>
    StatusMediaChanged = 0x8000001C,

    /// <summary>
    ///     The STATUS_BUS_RESET constant.
    /// </summary>
    StatusBusReset = 0x8000001D,

    /// <summary>
    ///     The STATUS_END_OF_MEDIA constant.
    /// </summary>
    StatusEndOfMedia = 0x8000001E,

    /// <summary>
    ///     The STATUS_BEGINNING_OF_MEDIA constant.
    /// </summary>
    StatusBeginningOfMedia = 0x8000001F,

    /// <summary>
    ///     The STATUS_MEDIA_CHECK constant.
    /// </summary>
    StatusMediaCheck = 0x80000020,

    /// <summary>
    ///     The STATUS_SETMARK_DETECTED constant.
    /// </summary>
    StatusSetMarkDetected = 0x80000021,

    /// <summary>
    ///     The STATUS_NO_DATA_DETECTED constant.
    /// </summary>
    StatusNoDataDetected = 0x80000022,

    /// <summary>
    ///     The STATUS_REDIRECTOR_HAS_OPEN_HANDLES constant.
    /// </summary>
    StatusRedirectorHasOpenHandles = 0x80000023,

    /// <summary>
    ///     The STATUS_SERVER_HAS_OPEN_HANDLES constant.
    /// </summary>
    StatusServerHasOpenHandles = 0x80000024,

    /// <summary>
    ///     The STATUS_ALREADY_DISCONNECTED constant.
    /// </summary>
    StatusAlreadyDisconnected = 0x80000025,

    /// <summary>
    ///     The STATUS_LONGJUMP constant.
    /// </summary>
    StatusLongJump = 0x80000026,

    /// <summary>
    ///     The STATUS_CLEANER_CARTRIDGE_INSTALLED constant.
    /// </summary>
    StatusCleanerCartridgeInstalled = 0x80000027,

    /// <summary>
    ///     The STATUS_PLUGPLAY_QUERY_VETOED constant.
    /// </summary>
    StatusPlugPlayQueryVetoed = 0x80000028,

    /// <summary>
    ///     The STATUS_UNWIND_CONSOLIDATE constant.
    /// </summary>
    StatusUnwindConsolidate = 0x80000029,

    /// <summary>
    ///     The STATUS_REGISTRY_HIVE_RECOVERED constant.
    /// </summary>
    StatusRegistryHiveRecovered = 0x8000002A,

    /// <summary>
    ///     The STATUS_DLL_MIGHT_BE_INSECURE constant.
    /// </summary>
    StatusDllMightBeInsecure = 0x8000002B,

    /// <summary>
    ///     The STATUS_DLL_MIGHT_BE_INCOMPATIBLE constant.
    /// </summary>
    StatusDllMightBeIncompatible = 0x8000002C,

    /// <summary>
    ///     The STATUS_STOPPED_ON_SYMLINK constant.
    /// </summary>
    StatusStoppedOnSymLink = 0x8000002D,

    /// <summary>
    ///     The STATUS_DEVICE_REQUIRES_CLEANING constant.
    /// </summary>
    StatusDeviceRequiresCleaning = 0x80000288,

    /// <summary>
    ///     The STATUS_DEVICE_DOOR_OPEN constant.
    /// </summary>
    StatusDeviceDoorOpen = 0x80000289,

    /// <summary>
    ///     The STATUS_DATA_LOST_REPAIR constant.
    /// </summary>
    StatusDataLostRepair = 0x80000803,

    /// <summary>
    ///     The DBG_EXCEPTION_NOT_HANDLED constant.
    /// </summary>
    DbgExceptionNotHandled = 0x80010001,

    /// <summary>
    ///     The STATUS_CLUSTER_NODE_ALREADY_UP constant.
    /// </summary>
    StatusClusterNodeAlreadyUp = 0x80130001,

    /// <summary>
    ///     The STATUS_CLUSTER_NODE_ALREADY_DOWN constant.
    /// </summary>
    StatusClusterNodeAlreadyDown = 0x80130002,

    /// <summary>
    ///     The STATUS_CLUSTER_NETWORK_ALREADY_ONLINE constant.
    /// </summary>
    StatusClusterNetworkAlreadyOnline = 0x80130003,

    /// <summary>
    ///     The STATUS_CLUSTER_NETWORK_ALREADY_OFFLINE constant.
    /// </summary>
    StatusClusterNetworkAlreadyOffline = 0x80130004,

    /// <summary>
    ///     The STATUS_CLUSTER_NODE_ALREADY_MEMBER constant.
    /// </summary>
    StatusClusterNodeAlreadyMember = 0x80130005,

    /// <summary>
    ///     The STATUS_COULD_NOT_RESIZE_LOG constant.
    /// </summary>
    StatusCouldNotResizeLog = 0x80190009,

    /// <summary>
    ///     The STATUS_NO_TXF_METADATA constant.
    /// </summary>
    StatusNoTxfMetadata = 0x80190029,

    /// <summary>
    ///     The STATUS_CANT_RECOVER_WITH_HANDLE_OPEN constant.
    /// </summary>
    StatusCantRecoverWithHandleOpen = 0x80190031,

    /// <summary>
    ///     The STATUS_TXF_METADATA_ALREADY_PRESENT constant.
    /// </summary>
    StatusTxfMetadataAlreadyPresent = 0x80190041,

    /// <summary>
    ///     The STATUS_TRANSACTION_SCOPE_CALLBACKS_NOT_SET constant.
    /// </summary>
    StatusTransactionScopeCallbacksNotSet = 0x80190042,

    /// <summary>
    ///     The STATUS_VIDEO_HUNG_DISPLAY_DRIVER_THREAD_RECOVERED constant.
    /// </summary>
    StatusVideoHungDisplayDriverThreadRecovered = 0x801B00EB,

    /// <summary>
    ///     The STATUS_FLT_BUFFER_TOO_SMALL constant.
    /// </summary>
    StatusFltBufferTooSmall = 0x801C0001,

    /// <summary>
    ///     The STATUS_FVE_PARTIAL_METADATA constant.
    /// </summary>
    StatusFvePartialMetadata = 0x80210001,

    /// <summary>
    ///     The STATUS_FVE_TRANSIENT_STATE constant.
    /// </summary>
    StatusFveTransientState = 0x80210002,

    /// <summary>
    ///     The STATUS_UNSUCCESSFUL constant.
    /// </summary>
    StatusUnsuccessful = 0xC0000001,

    /// <summary>
    ///     The STATUS_NOT_IMPLEMENTED constant.
    /// </summary>
    StatusNotImplemented = 0xC0000002,

    /// <summary>
    ///     The STATUS_INVALID_INFO_CLASS constant.
    /// </summary>
    StatusInvalidInfoClass = 0xC0000003,

    /// <summary>
    ///     The STATUS_INFO_LENGTH_MISMATCH constant.
    /// </summary>
    StatusInfoLengthMismatch = 0xC0000004,

    /// <summary>
    ///     The STATUS_ACCESS_VIOLATION constant.
    /// </summary>
    StatusAccessViolation = 0xC0000005,

    /// <summary>
    ///     The STATUS_IN_PAGE_ERROR constant.
    /// </summary>
    StatusInPageError = 0xC0000006,

    /// <summary>
    ///     The STATUS_PAGEFILE_QUOTA constant.
    /// </summary>
    StatusPageFileQuota = 0xC0000007,

    /// <summary>
    ///     The STATUS_INVALID_HANDLE constant.
    /// </summary>
    StatusInvalidHandle = 0xC0000008,

    /// <summary>
    ///     The STATUS_BAD_INITIAL_STACK constant.
    /// </summary>
    StatusBadInitialStack = 0xC0000009,

    /// <summary>
    ///     The STATUS_BAD_INITIAL_PC constant.
    /// </summary>
    StatusBadInitialPc = 0xC000000A,

    /// <summary>
    ///     The STATUS_INVALID_CID constant.
    /// </summary>
    StatusInvalidCid = 0xC000000B,

    /// <summary>
    ///     The STATUS_TIMER_NOT_CANCELED constant.
    /// </summary>
    StatusTimerNotCanceled = 0xC000000C,

    /// <summary>
    ///     The STATUS_INVALID_PARAMETER constant.
    /// </summary>
    StatusInvalidParameter = 0xC000000D,

    /// <summary>
    ///     The STATUS_NO_SUCH_DEVICE constant.
    /// </summary>
    StatusNoSuchDevice = 0xC000000E,

    /// <summary>
    ///     The STATUS_NO_SUCH_FILE constant.
    /// </summary>
    StatusNoSuchFile = 0xC000000F,

    /// <summary>
    ///     The STATUS_INVALID_DEVICE_REQUEST constant.
    /// </summary>
    StatusInvalidDeviceRequest = 0xC0000010,

    /// <summary>
    ///     The STATUS_END_OF_FILE constant.
    /// </summary>
    StatusEndOfFile = 0xC0000011,

    /// <summary>
    ///     The STATUS_WRONG_VOLUME constant.
    /// </summary>
    StatusWrongVolume = 0xC0000012,

    /// <summary>
    ///     The STATUS_NO_MEDIA_IN_DEVICE constant.
    /// </summary>
    StatusNoMediaInDevice = 0xC0000013,

    /// <summary>
    ///     The STATUS_UNRECOGNIZED_MEDIA constant.
    /// </summary>
    StatusUnrecognizedMedia = 0xC0000014,

    /// <summary>
    ///     The STATUS_NONEXISTENT_SECTOR constant.
    /// </summary>
    StatusNonexistentSector = 0xC0000015,

    /// <summary>
    ///     The STATUS_MORE_PROCESSING_REQUIRED constant.
    /// </summary>
    StatusMoreProcessingRequired = 0xC0000016,

    /// <summary>
    ///     The STATUS_NO_MEMORY constant.
    /// </summary>
    StatusNoMemory = 0xC0000017,

    /// <summary>
    ///     The STATUS_CONFLICTING_ADDRESSES constant.
    /// </summary>
    StatusConflictingAddresses = 0xC0000018,

    /// <summary>
    ///     The STATUS_NOT_MAPPED_VIEW constant.
    /// </summary>
    StatusNotMappedView = 0xC0000019,

    /// <summary>
    ///     The STATUS_UNABLE_TO_FREE_VM constant.
    /// </summary>
    StatusUnableToFreeVm = 0xC000001A,

    /// <summary>
    ///     The STATUS_UNABLE_TO_DELETE_SECTION constant.
    /// </summary>
    StatusUnableToDeleteSection = 0xC000001B,

    /// <summary>
    ///     The STATUS_INVALID_SYSTEM_SERVICE constant.
    /// </summary>
    StatusInvalidSystemService = 0xC000001C,

    /// <summary>
    ///     The STATUS_ILLEGAL_INSTRUCTION constant.
    /// </summary>
    StatusIllegalInstruction = 0xC000001D,

    /// <summary>
    ///     The STATUS_INVALID_LOCK_SEQUENCE constant.
    /// </summary>
    StatusInvalidLockSequence = 0xC000001E,

    /// <summary>
    ///     The STATUS_INVALID_VIEW_SIZE constant.
    /// </summary>
    StatusInvalidViewSize = 0xC000001F,

    /// <summary>
    ///     The STATUS_INVALID_FILE_FOR_SECTION constant.
    /// </summary>
    StatusInvalidFileForSection = 0xC0000020,

    /// <summary>
    ///     The STATUS_ALREADY_COMMITTED constant.
    /// </summary>
    StatusAlreadyCommitted = 0xC0000021,

    /// <summary>
    ///     The STATUS_ACCESS_DENIED constant.
    /// </summary>
    StatusAccessDenied = 0xC0000022,

    /// <summary>
    ///     The STATUS_BUFFER_TOO_SMALL constant.
    /// </summary>
    StatusBufferTooSmall = 0xC0000023,

    /// <summary>
    ///     The STATUS_OBJECT_TYPE_MISMATCH constant.
    /// </summary>
    StatusObjectTypeMismatch = 0xC0000024,

    /// <summary>
    ///     The STATUS_NONCONTINUABLE_EXCEPTION constant.
    /// </summary>
    StatusNoncontinuableException = 0xC0000025,

    /// <summary>
    ///     The STATUS_INVALID_DISPOSITION constant.
    /// </summary>
    StatusInvalidDisposition = 0xC0000026,

    /// <summary>
    ///     The STATUS_UNWIND constant.
    /// </summary>
    StatusUnwind = 0xC0000027,

    /// <summary>
    ///     The STATUS_BAD_STACK constant.
    /// </summary>
    StatusBadStack = 0xC0000028,

    /// <summary>
    ///     The STATUS_INVALID_UNWIND_TARGET constant.
    /// </summary>
    StatusInvalidUnwindTarget = 0xC0000029,

    /// <summary>
    ///     The STATUS_NOT_LOCKED constant.
    /// </summary>
    StatusNotLocked = 0xC000002A,

    /// <summary>
    ///     The STATUS_PARITY_ERROR constant.
    /// </summary>
    StatusParityError = 0xC000002B,

    /// <summary>
    ///     The STATUS_UNABLE_TO_DECOMMIT_VM constant.
    /// </summary>
    StatusUnableToDecommitVm = 0xC000002C,

    /// <summary>
    ///     The STATUS_NOT_COMMITTED constant.
    /// </summary>
    StatusNotCommitted = 0xC000002D,

    /// <summary>
    ///     The STATUS_INVALID_PORT_ATTRIBUTES constant.
    /// </summary>
    StatusInvalidPortAttributes = 0xC000002E,

    /// <summary>
    ///     The STATUS_PORT_MESSAGE_TOO_LONG constant.
    /// </summary>
    StatusPortMessageTooLong = 0xC000002F,

    /// <summary>
    ///     The STATUS_INVALID_PARAMETER_MIX constant.
    /// </summary>
    StatusInvalidParameterMix = 0xC0000030,

    /// <summary>
    ///     The STATUS_INVALID_QUOTA_LOWER constant.
    /// </summary>
    StatusInvalidQuotaLower = 0xC0000031,

    /// <summary>
    ///     The STATUS_DISK_CORRUPT_ERROR constant.
    /// </summary>
    StatusDiskCorruptError = 0xC0000032,

    /// <summary>
    ///     The STATUS_OBJECT_NAME_INVALID constant.
    /// </summary>
    StatusObjectNameInvalid = 0xC0000033,

    /// <summary>
    ///     The STATUS_OBJECT_NAME_NOT_FOUND constant.
    /// </summary>
    StatusObjectNameNotFound = 0xC0000034,

    /// <summary>
    ///     The STATUS_OBJECT_NAME_COLLISION constant.
    /// </summary>
    StatusObjectNameCollision = 0xC0000035,

    /// <summary>
    ///     The STATUS_PORT_DISCONNECTED constant.
    /// </summary>
    StatusPortDisconnected = 0xC0000037,

    /// <summary>
    ///     The STATUS_DEVICE_ALREADY_ATTACHED constant.
    /// </summary>
    StatusDeviceAlreadyAttached = 0xC0000038,

    /// <summary>
    ///     The STATUS_OBJECT_PATH_INVALID constant.
    /// </summary>
    StatusObjectPathInvalid = 0xC0000039,

    /// <summary>
    ///     The STATUS_OBJECT_PATH_NOT_FOUND constant.
    /// </summary>
    StatusObjectPathNotFound = 0xC000003A,

    /// <summary>
    ///     The STATUS_OBJECT_PATH_SYNTAX_BAD constant.
    /// </summary>
    StatusObjectPathSyntaxBad = 0xC000003B,

    /// <summary>
    ///     The STATUS_DATA_OVERRUN constant.
    /// </summary>
    StatusDataOverrun = 0xC000003C,

    /// <summary>
    ///     The STATUS_DATA_LATE_ERROR constant.
    /// </summary>
    StatusDataLateError = 0xC000003D,

    /// <summary>
    ///     The STATUS_DATA_ERROR constant.
    /// </summary>
    StatusDataError = 0xC000003E,

    /// <summary>
    ///     The STATUS_CRC_ERROR constant.
    /// </summary>
    StatusCrcError = 0xC000003F,

    /// <summary>
    ///     The STATUS_SECTION_TOO_BIG constant.
    /// </summary>
    StatusSectionTooBig = 0xC0000040,

    /// <summary>
    ///     The STATUS_PORT_CONNECTION_REFUSED constant.
    /// </summary>
    StatusPortConnectionRefused = 0xC0000041,

    /// <summary>
    ///     The STATUS_INVALID_PORT_HANDLE constant.
    /// </summary>
    StatusInvalidPortHandle = 0xC0000042,

    /// <summary>
    ///     The STATUS_SHARING_VIOLATION constant.
    /// </summary>
    StatusSharingViolation = 0xC0000043,

    /// <summary>
    ///     The STATUS_QUOTA_EXCEEDED constant.
    /// </summary>
    StatusQuotaExceeded = 0xC0000044,

    /// <summary>
    ///     The STATUS_INVALID_PAGE_PROTECTION constant.
    /// </summary>
    StatusInvalidPageProtection = 0xC0000045,

    /// <summary>
    ///     The STATUS_MUTANT_NOT_OWNED constant.
    /// </summary>
    StatusMutantNotOwned = 0xC0000046,

    /// <summary>
    ///     The STATUS_SEMAPHORE_LIMIT_EXCEEDED constant.
    /// </summary>
    StatusSemaphoreLimitExceeded = 0xC0000047,

    /// <summary>
    ///     The STATUS_PORT_ALREADY_SET constant.
    /// </summary>
    StatusPortAlreadySet = 0xC0000048,

    /// <summary>
    ///     The STATUS_SECTION_NOT_IMAGE constant.
    /// </summary>
    StatusSectionNotImage = 0xC0000049,

    /// <summary>
    ///     The STATUS_SUSPEND_COUNT_EXCEEDED constant.
    /// </summary>
    StatusSuspendCountExceeded = 0xC000004A,

    /// <summary>
    ///     The STATUS_THREAD_IS_TERMINATING constant.
    /// </summary>
    StatusThreadIsTerminating = 0xC000004B,

    /// <summary>
    ///     The STATUS_BAD_WORKING_SET_LIMIT constant.
    /// </summary>
    StatusBadWorkingSetLimit = 0xC000004C,

    /// <summary>
    ///     The STATUS_INCOMPATIBLE_FILE_MAP constant.
    /// </summary>
    StatusIncompatibleFileMap = 0xC000004D,

    /// <summary>
    ///     The STATUS_SECTION_PROTECTION constant.
    /// </summary>
    StatusSectionProtection = 0xC000004E,

    /// <summary>
    ///     The STATUS_EAS_NOT_SUPPORTED constant.
    /// </summary>
    StatusEasNotSupported = 0xC000004F,

    /// <summary>
    ///     The STATUS_EA_TOO_LARGE constant.
    /// </summary>
    StatusEaTooLarge = 0xC0000050,

    /// <summary>
    ///     The STATUS_NONEXISTENT_EA_ENTRY constant.
    /// </summary>
    StatusNonexistentEaEntry = 0xC0000051,

    /// <summary>
    ///     The STATUS_NO_EAS_ON_FILE constant.
    /// </summary>
    StatusNoEasOnFile = 0xC0000052,

    /// <summary>
    ///     The STATUS_EA_CORRUPT_ERROR constant.
    /// </summary>
    StatusEaCorruptError = 0xC0000053,

    /// <summary>
    ///     The STATUS_FILE_LOCK_CONFLICT constant.
    /// </summary>
    StatusFileLockConflict = 0xC0000054,

    /// <summary>
    ///     The STATUS_LOCK_NOT_GRANTED constant.
    /// </summary>
    StatusLockNotGranted = 0xC0000055,

    /// <summary>
    ///     The STATUS_DELETE_PENDING constant.
    /// </summary>
    StatusDeletePending = 0xC0000056,

    /// <summary>
    ///     The STATUS_CTL_FILE_NOT_SUPPORTED constant.
    /// </summary>
    StatusCtlFileNotSupported = 0xC0000057,

    /// <summary>
    ///     The STATUS_UNKNOWN_REVISION constant.
    /// </summary>
    StatusUnknownRevision = 0xC0000058,

    /// <summary>
    ///     The STATUS_REVISION_MISMATCH constant.
    /// </summary>
    StatusRevisionMismatch = 0xC0000059,

    /// <summary>
    ///     The STATUS_INVALID_OWNER constant.
    /// </summary>
    StatusInvalidOwner = 0xC000005A,

    /// <summary>
    ///     The STATUS_INVALID_PRIMARY_GROUP constant.
    /// </summary>
    StatusInvalidPrimaryGroup = 0xC000005B,

    /// <summary>
    ///     The STATUS_NO_IMPERSONATION_TOKEN constant.
    /// </summary>
    StatusNoImpersonationToken = 0xC000005C,

    /// <summary>
    ///     The STATUS_CANT_DISABLE_MANDATORY constant.
    /// </summary>
    StatusCantDisableMandatory = 0xC000005D,

    /// <summary>
    ///     The STATUS_NO_LOGON_SERVERS constant.
    /// </summary>
    StatusNoLogonServers = 0xC000005E,

    /// <summary>
    ///     The STATUS_NO_SUCH_LOGON_SESSION constant.
    /// </summary>
    StatusNoSuchLogonSession = 0xC000005F,

    /// <summary>
    ///     The STATUS_NO_SUCH_PRIVILEGE constant.
    /// </summary>
    StatusNoSuchPrivilege = 0xC0000060,

    /// <summary>
    ///     The STATUS_PRIVILEGE_NOT_HELD constant.
    /// </summary>
    StatusPrivilegeNotHeld = 0xC0000061,

    /// <summary>
    ///     The STATUS_INVALID_ACCOUNT_NAME constant.
    /// </summary>
    StatusInvalidAccountName = 0xC0000062,

    /// <summary>
    ///     The STATUS_USER_EXISTS constant.
    /// </summary>
    StatusUserExists = 0xC0000063,

    /// <summary>
    ///     The STATUS_NO_SUCH_USER constant.
    /// </summary>
    StatusNoSuchUser = 0xC0000064,

    /// <summary>
    ///     The STATUS_GROUP_EXISTS constant.
    /// </summary>
    StatusGroupExists = 0xC0000065,

    /// <summary>
    ///     The STATUS_NO_SUCH_GROUP constant.
    /// </summary>
    StatusNoSuchGroup = 0xC0000066,

    /// <summary>
    ///     The STATUS_MEMBER_IN_GROUP constant.
    /// </summary>
    StatusMemberInGroup = 0xC0000067,

    /// <summary>
    ///     The STATUS_MEMBER_NOT_IN_GROUP constant.
    /// </summary>
    StatusMemberNotInGroup = 0xC0000068,

    /// <summary>
    ///     The STATUS_LAST_ADMIN constant.
    /// </summary>
    StatusLastAdmin = 0xC0000069,

    /// <summary>
    ///     The STATUS_WRONG_PASSWORD constant.
    /// </summary>
    StatusWrongPassword = 0xC000006A,

    /// <summary>
    ///     The STATUS_ILL_FORMED_PASSWORD constant.
    /// </summary>
    StatusIllFormedPassword = 0xC000006B,

    /// <summary>
    ///     The STATUS_PASSWORD_RESTRICTION constant.
    /// </summary>
    StatusPasswordRestriction = 0xC000006C,

    /// <summary>
    ///     The STATUS_LOGON_FAILURE constant.
    /// </summary>
    StatusLogonFailure = 0xC000006D,

    /// <summary>
    ///     The STATUS_ACCOUNT_RESTRICTION constant.
    /// </summary>
    StatusAccountRestriction = 0xC000006E,

    /// <summary>
    ///     The STATUS_INVALID_LOGON_HOURS constant.
    /// </summary>
    StatusInvalidLogonHours = 0xC000006F,

    /// <summary>
    ///     The STATUS_INVALID_WORKSTATION constant.
    /// </summary>
    StatusInvalidWorkstation = 0xC0000070,

    /// <summary>
    ///     The STATUS_PASSWORD_EXPIRED constant.
    /// </summary>
    StatusPasswordExpired = 0xC0000071,

    /// <summary>
    ///     The STATUS_ACCOUNT_DISABLED constant.
    /// </summary>
    StatusAccountDisabled = 0xC0000072,

    /// <summary>
    ///     The STATUS_NONE_MAPPED constant.
    /// </summary>
    StatusNoneMapped = 0xC0000073,

    /// <summary>
    ///     The STATUS_TOO_MANY_LUIDS_REQUESTED constant.
    /// </summary>
    StatusTooManyLuidsRequested = 0xC0000074,

    /// <summary>
    ///     The STATUS_LUIDS_EXHAUSTED constant.
    /// </summary>
    StatusLuidsExhausted = 0xC0000075,

    /// <summary>
    ///     The STATUS_INVALID_SUB_AUTHORITY constant.
    /// </summary>
    StatusInvalidSubAuthority = 0xC0000076,

    /// <summary>
    ///     The STATUS_INVALID_ACL constant.
    /// </summary>
    StatusInvalidAcl = 0xC0000077,

    /// <summary>
    ///     The STATUS_INVALID_SID constant.
    /// </summary>
    StatusInvalidSid = 0xC0000078,

    /// <summary>
    ///     The STATUS_INVALID_SECURITY_DESCR constant.
    /// </summary>
    StatusInvalidSecurityDescr = 0xC0000079,

    /// <summary>
    ///     The STATUS_PROCEDURE_NOT_FOUND constant.
    /// </summary>
    StatusProcedureNotFound = 0xC000007A,

    /// <summary>
    ///     The STATUS_INVALID_IMAGE_FORMAT constant.
    /// </summary>
    StatusInvalidImageFormat = 0xC000007B,

    /// <summary>
    ///     The STATUS_NO_TOKEN constant.
    /// </summary>
    StatusNoToken = 0xC000007C,

    /// <summary>
    ///     The STATUS_BAD_INHERITANCE_ACL constant.
    /// </summary>
    StatusBadInheritanceAcl = 0xC000007D,

    /// <summary>
    ///     The STATUS_RANGE_NOT_LOCKED constant.
    /// </summary>
    StatusRangeNotLocked = 0xC000007E,

    /// <summary>
    ///     The STATUS_DISK_FULL constant.
    /// </summary>
    StatusDiskFull = 0xC000007F,

    /// <summary>
    ///     The STATUS_SERVER_DISABLED constant.
    /// </summary>
    StatusServerDisabled = 0xC0000080,

    /// <summary>
    ///     The STATUS_SERVER_NOT_DISABLED constant.
    /// </summary>
    StatusServerNotDisabled = 0xC0000081,

    /// <summary>
    ///     The STATUS_TOO_MANY_GUIDS_REQUESTED constant.
    /// </summary>
    StatusTooManyGuidsRequested = 0xC0000082,

    /// <summary>
    ///     The STATUS_GUIDS_EXHAUSTED constant.
    /// </summary>
    StatusGuidsExhausted = 0xC0000083,

    /// <summary>
    ///     The STATUS_INVALID_ID_AUTHORITY constant.
    /// </summary>
    StatusInvalidIdAuthority = 0xC0000084,

    /// <summary>
    ///     The STATUS_AGENTS_EXHAUSTED constant.
    /// </summary>
    StatusAgentsExhausted = 0xC0000085,

    /// <summary>
    ///     The STATUS_INVALID_VOLUME_LABEL constant.
    /// </summary>
    StatusInvalidVolumeLabel = 0xC0000086,

    /// <summary>
    ///     The STATUS_SECTION_NOT_EXTENDED constant.
    /// </summary>
    StatusSectionNotExtended = 0xC0000087,

    /// <summary>
    ///     The STATUS_NOT_MAPPED_DATA constant.
    /// </summary>
    StatusNotMappedData = 0xC0000088,

    /// <summary>
    ///     The STATUS_RESOURCE_DATA_NOT_FOUND constant.
    /// </summary>
    StatusResourceDataNotFound = 0xC0000089,

    /// <summary>
    ///     The STATUS_RESOURCE_TYPE_NOT_FOUND constant.
    /// </summary>
    StatusResourceTypeNotFound = 0xC000008A,

    /// <summary>
    ///     The STATUS_RESOURCE_NAME_NOT_FOUND constant.
    /// </summary>
    StatusResourceNameNotFound = 0xC000008B,

    /// <summary>
    ///     The STATUS_ARRAY_BOUNDS_EXCEEDED constant.
    /// </summary>
    StatusArrayBoundsExceeded = 0xC000008C,

    /// <summary>
    ///     The STATUS_FLOAT_DENORMAL_OPERAND constant.
    /// </summary>
    StatusFloatDenormalOperand = 0xC000008D,

    /// <summary>
    ///     The STATUS_FLOAT_DIVIDE_BY_ZERO constant.
    /// </summary>
    StatusFloatDivideByZero = 0xC000008E,

    /// <summary>
    ///     The STATUS_FLOAT_INEXACT_RESULT constant.
    /// </summary>
    StatusFloatInexactResult = 0xC000008F,

    /// <summary>
    ///     The STATUS_FLOAT_INVALID_OPERATION constant.
    /// </summary>
    StatusFloatInvalidOperation = 0xC0000090,

    /// <summary>
    ///     The STATUS_FLOAT_OVERFLOW constant.
    /// </summary>
    StatusFloatOverflow = 0xC0000091,

    /// <summary>
    ///     The STATUS_FLOAT_STACK_CHECK constant.
    /// </summary>
    StatusFloatStackCheck = 0xC0000092,

    /// <summary>
    ///     The STATUS_FLOAT_UNDERFLOW constant.
    /// </summary>
    StatusFloatUnderflow = 0xC0000093,

    /// <summary>
    ///     The STATUS_INTEGER_DIVIDE_BY_ZERO constant.
    /// </summary>
    StatusIntegerDivideByZero = 0xC0000094,

    /// <summary>
    ///     The STATUS_INTEGER_OVERFLOW constant.
    /// </summary>
    StatusIntegerOverflow = 0xC0000095,

    /// <summary>
    ///     The STATUS_PRIVILEGED_INSTRUCTION constant.
    /// </summary>
    StatusPrivilegedInstruction = 0xC0000096,

    /// <summary>
    ///     The STATUS_TOO_MANY_PAGING_FILES constant.
    /// </summary>
    StatusTooManyPagingFiles = 0xC0000097,

    /// <summary>
    ///     The STATUS_FILE_INVALID constant.
    /// </summary>
    StatusFileInvalid = 0xC0000098,

    /// <summary>
    ///     The STATUS_ALLOTTED_SPACE_EXCEEDED constant.
    /// </summary>
    StatusAllottedSpaceExceeded = 0xC0000099,

    /// <summary>
    ///     The STATUS_INSUFFICIENT_RESOURCES constant.
    /// </summary>
    StatusInsufficientResources = 0xC000009A,

    /// <summary>
    ///     The STATUS_DFS_EXIT_PATH_FOUND constant.
    /// </summary>
    StatusDfsExitPathFound = 0xC000009B,

    /// <summary>
    ///     The STATUS_DEVICE_DATA_ERROR constant.
    /// </summary>
    StatusDeviceDataError = 0xC000009C,

    /// <summary>
    ///     The STATUS_DEVICE_NOT_CONNECTED constant.
    /// </summary>
    StatusDeviceNotConnected = 0xC000009D,

    /// <summary>
    ///     The STATUS_FREE_VM_NOT_AT_BASE constant.
    /// </summary>
    StatusFreeVmNotAtBase = 0xC000009F,

    /// <summary>
    ///     The STATUS_MEMORY_NOT_ALLOCATED constant.
    /// </summary>
    StatusMemoryNotAllocated = 0xC00000A0,

    /// <summary>
    ///     The STATUS_WORKING_SET_QUOTA constant.
    /// </summary>
    StatusWorkingSetQuota = 0xC00000A1,

    /// <summary>
    ///     The STATUS_MEDIA_WRITE_PROTECTED constant.
    /// </summary>
    StatusMediaWriteProtected = 0xC00000A2,

    /// <summary>
    ///     The STATUS_DEVICE_NOT_READY constant.
    /// </summary>
    StatusDeviceNotReady = 0xC00000A3,

    /// <summary>
    ///     The STATUS_INVALID_GROUP_ATTRIBUTES constant.
    /// </summary>
    StatusInvalidGroupAttributes = 0xC00000A4,

    /// <summary>
    ///     The STATUS_BAD_IMPERSONATION_LEVEL constant.
    /// </summary>
    StatusBadImpersonationLevel = 0xC00000A5,

    /// <summary>
    ///     The STATUS_CANT_OPEN_ANONYMOUS constant.
    /// </summary>
    StatusCantOpenAnonymous = 0xC00000A6,

    /// <summary>
    ///     The STATUS_BAD_VALIDATION_CLASS constant.
    /// </summary>
    StatusBadValidationClass = 0xC00000A7,

    /// <summary>
    ///     The STATUS_BAD_TOKEN_TYPE constant.
    /// </summary>
    StatusBadTokenType = 0xC00000A8,

    /// <summary>
    ///     The STATUS_BAD_MASTER_BOOT_RECORD constant.
    /// </summary>
    StatusBadMasterBootRecord = 0xC00000A9,

    /// <summary>
    ///     The STATUS_INSTRUCTION_MISALIGNMENT constant.
    /// </summary>
    StatusInstructionMisalignment = 0xC00000AA,

    /// <summary>
    ///     The STATUS_INSTANCE_NOT_AVAILABLE constant.
    /// </summary>
    StatusInstanceNotAvailable = 0xC00000AB,

    /// <summary>
    ///     The STATUS_PIPE_NOT_AVAILABLE constant.
    /// </summary>
    StatusPipeNotAvailable = 0xC00000AC,

    /// <summary>
    ///     The STATUS_INVALID_PIPE_STATE constant.
    /// </summary>
    StatusInvalidPipeState = 0xC00000AD,

    /// <summary>
    ///     The STATUS_PIPE_BUSY constant.
    /// </summary>
    StatusPipeBusy = 0xC00000AE,

    /// <summary>
    ///     The STATUS_ILLEGAL_FUNCTION constant.
    /// </summary>
    StatusIllegalFunction = 0xC00000AF,

    /// <summary>
    ///     The STATUS_PIPE_DISCONNECTED constant.
    /// </summary>
    StatusPipeDisconnected = 0xC00000B0,

    /// <summary>
    ///     The STATUS_PIPE_CLOSING constant.
    /// </summary>
    StatusPipeClosing = 0xC00000B1,

    /// <summary>
    ///     The STATUS_PIPE_CONNECTED constant.
    /// </summary>
    StatusPipeConnected = 0xC00000B2,

    /// <summary>
    ///     The STATUS_PIPE_LISTENING constant.
    /// </summary>
    StatusPipeListening = 0xC00000B3,

    /// <summary>
    ///     The STATUS_INVALID_READ_MODE constant.
    /// </summary>
    StatusInvalidReadMode = 0xC00000B4,

    /// <summary>
    ///     The STATUS_IO_TIMEOUT constant.
    /// </summary>
    StatusIoTimeout = 0xC00000B5,

    /// <summary>
    ///     The STATUS_FILE_FORCED_CLOSED constant.
    /// </summary>
    StatusFileForcedClosed = 0xC00000B6,

    /// <summary>
    ///     The STATUS_PROFILING_NOT_STARTED constant.
    /// </summary>
    StatusProfilingNotStarted = 0xC00000B7,

    /// <summary>
    ///     The STATUS_PROFILING_NOT_STOPPED constant.
    /// </summary>
    StatusProfilingNotStopped = 0xC00000B8,

    /// <summary>
    ///     The STATUS_COULD_NOT_INTERPRET constant.
    /// </summary>
    StatusCouldNotInterpret = 0xC00000B9,

    /// <summary>
    ///     The STATUS_FILE_IS_A_DIRECTORY constant.
    /// </summary>
    StatusFileIsADirectory = 0xC00000BA,

    /// <summary>
    ///     The STATUS_NOT_SUPPORTED constant.
    /// </summary>
    StatusNotSupported = 0xC00000BB,

    /// <summary>
    ///     The STATUS_REMOTE_NOT_LISTENING constant.
    /// </summary>
    StatusRemoteNotListening = 0xC00000BC,

    /// <summary>
    ///     The STATUS_DUPLICATE_NAME constant.
    /// </summary>
    StatusDuplicateName = 0xC00000BD,

    /// <summary>
    ///     The STATUS_BAD_NETWORK_PATH constant.
    /// </summary>
    StatusBadNetworkPath = 0xC00000BE,

    /// <summary>
    ///     The STATUS_NETWORK_BUSY constant.
    /// </summary>
    StatusNetworkBusy = 0xC00000BF,

    /// <summary>
    ///     The STATUS_DEVICE_DOES_NOT_EXIST constant.
    /// </summary>
    StatusDeviceDoesNotExist = 0xC00000C0,

    /// <summary>
    ///     The STATUS_TOO_MANY_COMMANDS constant.
    /// </summary>
    StatusTooManyCommands = 0xC00000C1,

    /// <summary>
    ///     The STATUS_ADAPTER_HARDWARE_ERROR constant.
    /// </summary>
    StatusAdapterHardwareError = 0xC00000C2,

    /// <summary>
    ///     The STATUS_INVALID_NETWORK_RESPONSE constant.
    /// </summary>
    StatusInvalidNetworkResponse = 0xC00000C3,

    /// <summary>
    ///     The STATUS_UNEXPECTED_NETWORK_ERROR constant.
    /// </summary>
    StatusUnexpectedNetworkError = 0xC00000C4,

    /// <summary>
    ///     The STATUS_BAD_REMOTE_ADAPTER constant.
    /// </summary>
    StatusBadRemoteAdapter = 0xC00000C5,

    /// <summary>
    ///     The STATUS_PRINT_QUEUE_FULL constant.
    /// </summary>
    StatusPrintQueueFull = 0xC00000C6,

    /// <summary>
    ///     The STATUS_NO_SPOOL_SPACE constant.
    /// </summary>
    StatusNoSpoolSpace = 0xC00000C7,

    /// <summary>
    ///     The STATUS_PRINT_CANCELLED constant.
    /// </summary>
    StatusPrintCancelled = 0xC00000C8,

    /// <summary>
    ///     The STATUS_NETWORK_NAME_DELETED constant.
    /// </summary>
    StatusNetworkNameDeleted = 0xC00000C9,

    /// <summary>
    ///     The STATUS_NETWORK_ACCESS_DENIED constant.
    /// </summary>
    StatusNetworkAccessDenied = 0xC00000CA,

    /// <summary>
    ///     The STATUS_BAD_DEVICE_TYPE constant.
    /// </summary>
    StatusBadDeviceType = 0xC00000CB,

    /// <summary>
    ///     The STATUS_BAD_NETWORK_NAME constant.
    /// </summary>
    StatusBadNetworkName = 0xC00000CC,

    /// <summary>
    ///     The STATUS_TOO_MANY_NAMES constant.
    /// </summary>
    StatusTooManyNames = 0xC00000CD,

    /// <summary>
    ///     The STATUS_TOO_MANY_SESSIONS constant.
    /// </summary>
    StatusTooManySessions = 0xC00000CE,

    /// <summary>
    ///     The STATUS_SHARING_PAUSED constant.
    /// </summary>
    StatusSharingPaused = 0xC00000CF,

    /// <summary>
    ///     The STATUS_REQUEST_NOT_ACCEPTED constant.
    /// </summary>
    StatusRequestNotAccepted = 0xC00000D0,

    /// <summary>
    ///     The STATUS_REDIRECTOR_PAUSED constant.
    /// </summary>
    StatusRedirectorPaused = 0xC00000D1,

    /// <summary>
    ///     The STATUS_NET_WRITE_FAULT constant.
    /// </summary>
    StatusNetWriteFault = 0xC00000D2,

    /// <summary>
    ///     The STATUS_PROFILING_AT_LIMIT constant.
    /// </summary>
    StatusProfilingAtLimit = 0xC00000D3,

    /// <summary>
    ///     The STATUS_NOT_SAME_DEVICE constant.
    /// </summary>
    StatusNotSameDevice = 0xC00000D4,

    /// <summary>
    ///     The STATUS_FILE_RENAMED constant.
    /// </summary>
    StatusFileRenamed = 0xC00000D5,

    /// <summary>
    ///     The STATUS_VIRTUAL_CIRCUIT_CLOSED constant.
    /// </summary>
    StatusVirtualCircuitClosed = 0xC00000D6,

    /// <summary>
    ///     The STATUS_NO_SECURITY_ON_OBJECT constant.
    /// </summary>
    StatusNoSecurityOnObject = 0xC00000D7,

    /// <summary>
    ///     The STATUS_CANT_WAIT constant.
    /// </summary>
    StatusCantWait = 0xC00000D8,

    /// <summary>
    ///     The STATUS_PIPE_EMPTY constant.
    /// </summary>
    StatusPipeEmpty = 0xC00000D9,

    /// <summary>
    ///     The STATUS_CANT_ACCESS_DOMAIN_INFO constant.
    /// </summary>
    StatusCantAccessDomainInfo = 0xC00000DA,

    /// <summary>
    ///     The STATUS_CANT_TERMINATE_SELF constant.
    /// </summary>
    StatusCantTerminateSelf = 0xC00000DB,

    /// <summary>
    ///     The STATUS_INVALID_SERVER_STATE constant.
    /// </summary>
    StatusInvalidServerState = 0xC00000DC,

    /// <summary>
    ///     The STATUS_INVALID_DOMAIN_STATE constant.
    /// </summary>
    StatusInvalidDomainState = 0xC00000DD,

    /// <summary>
    ///     The STATUS_INVALID_DOMAIN_ROLE constant.
    /// </summary>
    StatusInvalidDomainRole = 0xC00000DE,

    /// <summary>
    ///     The STATUS_NO_SUCH_DOMAIN constant.
    /// </summary>
    StatusNoSuchDomain = 0xC00000DF,

    /// <summary>
    ///     The STATUS_DOMAIN_EXISTS constant.
    /// </summary>
    StatusDomainExists = 0xC00000E0,

    /// <summary>
    ///     The STATUS_DOMAIN_LIMIT_EXCEEDED constant.
    /// </summary>
    StatusDomainLimitExceeded = 0xC00000E1,

    /// <summary>
    ///     The STATUS_OPLOCK_NOT_GRANTED constant.
    /// </summary>
    StatusOplockNotGranted = 0xC00000E2,

    /// <summary>
    ///     The STATUS_INVALID_OPLOCK_PROTOCOL constant.
    /// </summary>
    StatusInvalidOplockProtocol = 0xC00000E3,

    /// <summary>
    ///     The STATUS_INTERNAL_DB_CORRUPTION constant.
    /// </summary>
    StatusInternalDbCorruption = 0xC00000E4,

    /// <summary>
    ///     The STATUS_INTERNAL_ERROR constant.
    /// </summary>
    StatusInternalError = 0xC00000E5,

    /// <summary>
    ///     The STATUS_GENERIC_NOT_MAPPED constant.
    /// </summary>
    StatusGenericNotMapped = 0xC00000E6,

    /// <summary>
    ///     The STATUS_BAD_DESCRIPTOR_FORMAT constant.
    /// </summary>
    StatusBadDescriptorFormat = 0xC00000E7,

    /// <summary>
    ///     The STATUS_INVALID_USER_BUFFER constant.
    /// </summary>
    StatusInvalidUserBuffer = 0xC00000E8,

    /// <summary>
    ///     The STATUS_UNEXPECTED_IO_ERROR constant.
    /// </summary>
    StatusUnexpectedIoError = 0xC00000E9,

    /// <summary>
    ///     The STATUS_UNEXPECTED_MM_CREATE_ERR constant.
    /// </summary>
    StatusUnexpectedMmCreateErr = 0xC00000EA,

    /// <summary>
    ///     The STATUS_UNEXPECTED_MM_MAP_ERROR constant.
    /// </summary>
    StatusUnexpectedMmMapError = 0xC00000EB,

    /// <summary>
    ///     The STATUS_UNEXPECTED_MM_EXTEND_ERR constant.
    /// </summary>
    StatusUnexpectedMmExtendErr = 0xC00000EC,

    /// <summary>
    ///     The STATUS_NOT_LOGON_PROCESS constant.
    /// </summary>
    StatusNotLogonProcess = 0xC00000ED,

    /// <summary>
    ///     The STATUS_LOGON_SESSION_EXISTS constant.
    /// </summary>
    StatusLogonSessionExists = 0xC00000EE,

    /// <summary>
    ///     The STATUS_INVALID_PARAMETER_1 constant.
    /// </summary>
    StatusInvalidParameter1 = 0xC00000EF,

    /// <summary>
    ///     The STATUS_INVALID_PARAMETER_2 constant.
    /// </summary>
    StatusInvalidParameter2 = 0xC00000F0,

    /// <summary>
    ///     The STATUS_INVALID_PARAMETER_3 constant.
    /// </summary>
    StatusInvalidParameter3 = 0xC00000F1,

    /// <summary>
    ///     The STATUS_INVALID_PARAMETER_4 constant.
    /// </summary>
    StatusInvalidParameter4 = 0xC00000F2,

    /// <summary>
    ///     The STATUS_INVALID_PARAMETER_5 constant.
    /// </summary>
    StatusInvalidParameter5 = 0xC00000F3,

    /// <summary>
    ///     The STATUS_INVALID_PARAMETER_6 constant.
    /// </summary>
    StatusInvalidParameter6 = 0xC00000F4,

    /// <summary>
    ///     The STATUS_INVALID_PARAMETER_7 constant.
    /// </summary>
    StatusInvalidParameter7 = 0xC00000F5,

    /// <summary>
    ///     The STATUS_INVALID_PARAMETER_8 constant.
    /// </summary>
    StatusInvalidParameter8 = 0xC00000F6,

    /// <summary>
    ///     The STATUS_INVALID_PARAMETER_9 constant.
    /// </summary>
    StatusInvalidParameter9 = 0xC00000F7,

    /// <summary>
    ///     The STATUS_INVALID_PARAMETER_10 constant.
    /// </summary>
    StatusInvalidParameter10 = 0xC00000F8,

    /// <summary>
    ///     The STATUS_INVALID_PARAMETER_11 constant.
    /// </summary>
    StatusInvalidParameter11 = 0xC00000F9,

    /// <summary>
    ///     The STATUS_INVALID_PARAMETER_12 constant.
    /// </summary>
    StatusInvalidParameter12 = 0xC00000FA,

    /// <summary>
    ///     The STATUS_REDIRECTOR_NOT_STARTED constant.
    /// </summary>
    StatusRedirectorNotStarted = 0xC00000FB,

    /// <summary>
    ///     The STATUS_REDIRECTOR_STARTED constant.
    /// </summary>
    StatusRedirectorStarted = 0xC00000FC,

    /// <summary>
    ///     The STATUS_STACK_OVERFLOW constant.
    /// </summary>
    StatusStackOverflow = 0xC00000FD,

    /// <summary>
    ///     The STATUS_NO_SUCH_PACKAGE constant.
    /// </summary>
    StatusNoSuchPackage = 0xC00000FE,

    /// <summary>
    ///     The STATUS_BAD_FUNCTION_TABLE constant.
    /// </summary>
    StatusBadFunctionTable = 0xC00000FF,

    /// <summary>
    ///     The STATUS_VARIABLE_NOT_FOUND constant.
    /// </summary>
    StatusVariableNotFound = 0xC0000100,

    /// <summary>
    ///     The STATUS_DIRECTORY_NOT_EMPTY constant.
    /// </summary>
    StatusDirectoryNotEmpty = 0xC0000101,

    /// <summary>
    ///     The STATUS_FILE_CORRUPT_ERROR constant.
    /// </summary>
    StatusFileCorruptError = 0xC0000102,

    /// <summary>
    ///     The STATUS_NOT_A_DIRECTORY constant.
    /// </summary>
    StatusNotADirectory = 0xC0000103,

    /// <summary>
    ///     The STATUS_BAD_LOGON_SESSION_STATE constant.
    /// </summary>
    StatusBadLogonSessionState = 0xC0000104,

    /// <summary>
    ///     The STATUS_LOGON_SESSION_COLLISION constant.
    /// </summary>
    StatusLogonSessionCollision = 0xC0000105,

    /// <summary>
    ///     The STATUS_NAME_TOO_LONG constant.
    /// </summary>
    StatusNameTooLong = 0xC0000106,

    /// <summary>
    ///     The STATUS_FILES_OPEN constant.
    /// </summary>
    StatusFilesOpen = 0xC0000107,

    /// <summary>
    ///     The STATUS_CONNECTION_IN_USE constant.
    /// </summary>
    StatusConnectionInUse = 0xC0000108,

    /// <summary>
    ///     The STATUS_MESSAGE_NOT_FOUND constant.
    /// </summary>
    StatusMessageNotFound = 0xC0000109,

    /// <summary>
    ///     The STATUS_PROCESS_IS_TERMINATING constant.
    /// </summary>
    StatusProcessIsTerminating = 0xC000010A,

    /// <summary>
    ///     The STATUS_INVALID_LOGON_TYPE constant.
    /// </summary>
    StatusInvalidLogonType = 0xC000010B,

    /// <summary>
    ///     The STATUS_NO_GUID_TRANSLATION constant.
    /// </summary>
    StatusNoGuidTranslation = 0xC000010C,

    /// <summary>
    ///     The STATUS_CANNOT_IMPERSONATE constant.
    /// </summary>
    StatusCannotImpersonate = 0xC000010D,

    /// <summary>
    ///     The STATUS_IMAGE_ALREADY_LOADED constant.
    /// </summary>
    StatusImageAlreadyLoaded = 0xC000010E,

    /// <summary>
    ///     The STATUS_NO_LDT constant.
    /// </summary>
    StatusNoLdt = 0xC0000117,

    /// <summary>
    ///     The STATUS_INVALID_LDT_SIZE constant.
    /// </summary>
    StatusInvalidLdtSize = 0xC0000118,

    /// <summary>
    ///     The STATUS_INVALID_LDT_OFFSET constant.
    /// </summary>
    StatusInvalidLdtOffset = 0xC0000119,

    /// <summary>
    ///     The STATUS_INVALID_LDT_DESCRIPTOR constant.
    /// </summary>
    StatusInvalidLdtDescriptor = 0xC000011A,

    /// <summary>
    ///     The STATUS_INVALID_IMAGE_NE_FORMAT constant.
    /// </summary>
    StatusInvalidImageNeFormat = 0xC000011B,

    /// <summary>
    ///     The STATUS_RXACT_INVALID_STATE constant.
    /// </summary>
    StatusRxActInvalidState = 0xC000011C,

    /// <summary>
    ///     The STATUS_RXACT_COMMIT_FAILURE constant.
    /// </summary>
    StatusRxActCommitFailure = 0xC000011D,

    /// <summary>
    ///     The STATUS_MAPPED_FILE_SIZE_ZERO constant.
    /// </summary>
    StatusMappedFileSizeZero = 0xC000011E,

    /// <summary>
    ///     The STATUS_TOO_MANY_OPENED_FILES constant.
    /// </summary>
    StatusTooManyOpenedFiles = 0xC000011F,

    /// <summary>
    ///     The STATUS_CANCELLED constant.
    /// </summary>
    StatusCancelled = 0xC0000120,

    /// <summary>
    ///     The STATUS_CANNOT_DELETE constant.
    /// </summary>
    StatusCannotDelete = 0xC0000121,

    /// <summary>
    ///     The STATUS_INVALID_COMPUTER_NAME constant.
    /// </summary>
    StatusInvalidComputerName = 0xC0000122,

    /// <summary>
    ///     The STATUS_FILE_DELETED constant.
    /// </summary>
    StatusFileDeleted = 0xC0000123,

    /// <summary>
    ///     The STATUS_SPECIAL_ACCOUNT constant.
    /// </summary>
    StatusSpecialAccount = 0xC0000124,

    /// <summary>
    ///     The STATUS_SPECIAL_GROUP constant.
    /// </summary>
    StatusSpecialGroup = 0xC0000125,

    /// <summary>
    ///     The STATUS_SPECIAL_USER constant.
    /// </summary>
    StatusSpecialUser = 0xC0000126,

    /// <summary>
    ///     The STATUS_MEMBERS_PRIMARY_GROUP constant.
    /// </summary>
    StatusMembersPrimaryGroup = 0xC0000127,

    /// <summary>
    ///     The STATUS_FILE_CLOSED constant.
    /// </summary>
    StatusFileClosed = 0xC0000128,

    /// <summary>
    ///     The STATUS_TOO_MANY_THREADS constant.
    /// </summary>
    StatusTooManyThreads = 0xC0000129,

    /// <summary>
    ///     The STATUS_THREAD_NOT_IN_PROCESS constant.
    /// </summary>
    StatusThreadNotInProcess = 0xC000012A,

    /// <summary>
    ///     The STATUS_TOKEN_ALREADY_IN_USE constant.
    /// </summary>
    StatusTokenAlreadyInUse = 0xC000012B,

    /// <summary>
    ///     The STATUS_PAGEFILE_QUOTA_EXCEEDED constant.
    /// </summary>
    StatusPageFileQuotaExceeded = 0xC000012C,

    /// <summary>
    ///     The STATUS_COMMITMENT_LIMIT constant.
    /// </summary>
    StatusCommitmentLimit = 0xC000012D,

    /// <summary>
    ///     The STATUS_INVALID_IMAGE_LE_FORMAT constant.
    /// </summary>
    StatusInvalidImageLeFormat = 0xC000012E,

    /// <summary>
    ///     The STATUS_INVALID_IMAGE_NOT_MZ constant.
    /// </summary>
    StatusInvalidImageNotMz = 0xC000012F,

    /// <summary>
    ///     The STATUS_INVALID_IMAGE_PROTECT constant.
    /// </summary>
    StatusInvalidImageProtect = 0xC0000130,

    /// <summary>
    ///     The STATUS_INVALID_IMAGE_WIN_16 constant.
    /// </summary>
    StatusInvalidImageWin16 = 0xC0000131,

    /// <summary>
    ///     The STATUS_LOGON_SERVER_CONFLICT constant.
    /// </summary>
    StatusLogonServerConflict = 0xC0000132,

    /// <summary>
    ///     The STATUS_TIME_DIFFERENCE_AT_DC constant.
    /// </summary>
    StatusTimeDifferenceAtDc = 0xC0000133,

    /// <summary>
    ///     The STATUS_SYNCHRONIZATION_REQUIRED constant.
    /// </summary>
    StatusSynchronizationRequired = 0xC0000134,

    /// <summary>
    ///     The STATUS_DLL_NOT_FOUND constant.
    /// </summary>
    StatusDllNotFound = 0xC0000135,

    /// <summary>
    ///     The STATUS_OPEN_FAILED constant.
    /// </summary>
    StatusOpenFailed = 0xC0000136,

    /// <summary>
    ///     The STATUS_IO_PRIVILEGE_FAILED constant.
    /// </summary>
    StatusIoPrivilegeFailed = 0xC0000137,

    /// <summary>
    ///     The STATUS_ORDINAL_NOT_FOUND constant.
    /// </summary>
    StatusOrdinalNotFound = 0xC0000138,

    /// <summary>
    ///     The STATUS_ENTRYPOINT_NOT_FOUND constant.
    /// </summary>
    StatusEntryPointNotFound = 0xC0000139,

    /// <summary>
    ///     The STATUS_CONTROL_C_EXIT constant.
    /// </summary>
    StatusControlCExit = 0xC000013A,

    /// <summary>
    ///     The STATUS_LOCAL_DISCONNECT constant.
    /// </summary>
    StatusLocalDisconnect = 0xC000013B,

    /// <summary>
    ///     The STATUS_REMOTE_DISCONNECT constant.
    /// </summary>
    StatusRemoteDisconnect = 0xC000013C,

    /// <summary>
    ///     The STATUS_REMOTE_RESOURCES constant.
    /// </summary>
    StatusRemoteResources = 0xC000013D,

    /// <summary>
    ///     The STATUS_LINK_FAILED constant.
    /// </summary>
    StatusLinkFailed = 0xC000013E,

    /// <summary>
    ///     The STATUS_LINK_TIMEOUT constant.
    /// </summary>
    StatusLinkTimeout = 0xC000013F,

    /// <summary>
    ///     The STATUS_INVALID_CONNECTION constant.
    /// </summary>
    StatusInvalidConnection = 0xC0000140,

    /// <summary>
    ///     The STATUS_INVALID_ADDRESS constant.
    /// </summary>
    StatusInvalidAddress = 0xC0000141,

    /// <summary>
    ///     The STATUS_DLL_INIT_FAILED constant.
    /// </summary>
    StatusDllInitFailed = 0xC0000142,

    /// <summary>
    ///     The STATUS_MISSING_SYSTEMFILE constant.
    /// </summary>
    StatusMissingSystemFile = 0xC0000143,

    /// <summary>
    ///     The STATUS_UNHANDLED_EXCEPTION constant.
    /// </summary>
    StatusUnhandledException = 0xC0000144,

    /// <summary>
    ///     The STATUS_APP_INIT_FAILURE constant.
    /// </summary>
    StatusAppInitFailure = 0xC0000145,

    /// <summary>
    ///     The STATUS_PAGEFILE_CREATE_FAILED constant.
    /// </summary>
    StatusPageFileCreateFailed = 0xC0000146,

    /// <summary>
    ///     The STATUS_NO_PAGEFILE constant.
    /// </summary>
    StatusNoPageFile = 0xC0000147,

    /// <summary>
    ///     The STATUS_INVALID_LEVEL constant.
    /// </summary>
    StatusInvalidLevel = 0xC0000148,

    /// <summary>
    ///     The STATUS_WRONG_PASSWORD_CORE constant.
    /// </summary>
    StatusWrongPasswordCore = 0xC0000149,

    /// <summary>
    ///     The STATUS_ILLEGAL_FLOAT_CONTEXT constant.
    /// </summary>
    StatusIllegalFloatContext = 0xC000014A,

    /// <summary>
    ///     The STATUS_PIPE_BROKEN constant.
    /// </summary>
    StatusPipeBroken = 0xC000014B,

    /// <summary>
    ///     The STATUS_REGISTRY_CORRUPT constant.
    /// </summary>
    StatusRegistryCorrupt = 0xC000014C,

    /// <summary>
    ///     The STATUS_REGISTRY_IO_FAILED constant.
    /// </summary>
    StatusRegistryIoFailed = 0xC000014D,

    /// <summary>
    ///     The STATUS_NO_EVENT_PAIR constant.
    /// </summary>
    StatusNoEventPair = 0xC000014E,

    /// <summary>
    ///     The STATUS_UNRECOGNIZED_VOLUME constant.
    /// </summary>
    StatusUnrecognizedVolume = 0xC000014F,

    /// <summary>
    ///     The STATUS_SERIAL_NO_DEVICE_INITED constant.
    /// </summary>
    StatusSerialNoDeviceInited = 0xC0000150,

    /// <summary>
    ///     The STATUS_NO_SUCH_ALIAS constant.
    /// </summary>
    StatusNoSuchAlias = 0xC0000151,

    /// <summary>
    ///     The STATUS_MEMBER_NOT_IN_ALIAS constant.
    /// </summary>
    StatusMemberNotInAlias = 0xC0000152,

    /// <summary>
    ///     The STATUS_MEMBER_IN_ALIAS constant.
    /// </summary>
    StatusMemberInAlias = 0xC0000153,

    /// <summary>
    ///     The STATUS_ALIAS_EXISTS constant.
    /// </summary>
    StatusAliasExists = 0xC0000154,

    /// <summary>
    ///     The STATUS_LOGON_NOT_GRANTED constant.
    /// </summary>
    StatusLogonNotGranted = 0xC0000155,

    /// <summary>
    ///     The STATUS_TOO_MANY_SECRETS constant.
    /// </summary>
    StatusTooManySecrets = 0xC0000156,

    /// <summary>
    ///     The STATUS_SECRET_TOO_LONG constant.
    /// </summary>
    StatusSecretTooLong = 0xC0000157,

    /// <summary>
    ///     The STATUS_INTERNAL_DB_ERROR constant.
    /// </summary>
    StatusInternalDbError = 0xC0000158,

    /// <summary>
    ///     The STATUS_FULLSCREEN_MODE constant.
    /// </summary>
    StatusFullscreenMode = 0xC0000159,

    /// <summary>
    ///     The STATUS_TOO_MANY_CONTEXT_IDS constant.
    /// </summary>
    StatusTooManyContextIds = 0xC000015A,

    /// <summary>
    ///     The STATUS_LOGON_TYPE_NOT_GRANTED constant.
    /// </summary>
    StatusLogonTypeNotGranted = 0xC000015B,

    /// <summary>
    ///     The STATUS_NOT_REGISTRY_FILE constant.
    /// </summary>
    StatusNotRegistryFile = 0xC000015C,

    /// <summary>
    ///     The STATUS_NT_CROSS_ENCRYPTION_REQUIRED constant.
    /// </summary>
    StatusNtCrossEncryptionRequired = 0xC000015D,

    /// <summary>
    ///     The STATUS_DOMAIN_CTRLR_CONFIG_ERROR constant.
    /// </summary>
    StatusDomainCtrlRConfigError = 0xC000015E,

    /// <summary>
    ///     The STATUS_FT_MISSING_MEMBER constant.
    /// </summary>
    StatusFtMissingMember = 0xC000015F,

    /// <summary>
    ///     The STATUS_ILL_FORMED_SERVICE_ENTRY constant.
    /// </summary>
    StatusIllFormedServiceEntry = 0xC0000160,

    /// <summary>
    ///     The STATUS_ILLEGAL_CHARACTER constant.
    /// </summary>
    StatusIllegalCharacter = 0xC0000161,

    /// <summary>
    ///     The STATUS_UNMAPPABLE_CHARACTER constant.
    /// </summary>
    StatusUnmappableCharacter = 0xC0000162,

    /// <summary>
    ///     The STATUS_UNDEFINED_CHARACTER constant.
    /// </summary>
    StatusUndefinedCharacter = 0xC0000163,

    /// <summary>
    ///     The STATUS_FLOPPY_VOLUME constant.
    /// </summary>
    StatusFloppyVolume = 0xC0000164,

    /// <summary>
    ///     The STATUS_FLOPPY_ID_MARK_NOT_FOUND constant.
    /// </summary>
    StatusFloppyIdMarkNotFound = 0xC0000165,

    /// <summary>
    ///     The STATUS_FLOPPY_WRONG_CYLINDER constant.
    /// </summary>
    StatusFloppyWrongCylinder = 0xC0000166,

    /// <summary>
    ///     The STATUS_FLOPPY_UNKNOWN_ERROR constant.
    /// </summary>
    StatusFloppyUnknownError = 0xC0000167,

    /// <summary>
    ///     The STATUS_FLOPPY_BAD_REGISTERS constant.
    /// </summary>
    StatusFloppyBadRegisters = 0xC0000168,

    /// <summary>
    ///     The STATUS_DISK_RECALIBRATE_FAILED constant.
    /// </summary>
    StatusDiskRecalibrateFailed = 0xC0000169,

    /// <summary>
    ///     The STATUS_DISK_OPERATION_FAILED constant.
    /// </summary>
    StatusDiskOperationFailed = 0xC000016A,

    /// <summary>
    ///     The STATUS_DISK_RESET_FAILED constant.
    /// </summary>
    StatusDiskResetFailed = 0xC000016B,

    /// <summary>
    ///     The STATUS_SHARED_IRQ_BUSY constant.
    /// </summary>
    StatusSharedIrqBusy = 0xC000016C,

    /// <summary>
    ///     The STATUS_FT_ORPHANING constant.
    /// </summary>
    StatusFtOrphaning = 0xC000016D,

    /// <summary>
    ///     The STATUS_BIOS_FAILED_TO_CONNECT_INTERRUPT constant.
    /// </summary>
    StatusBiosFailedToConnectInterrupt = 0xC000016E,

    /// <summary>
    ///     The STATUS_PARTITION_FAILURE constant.
    /// </summary>
    StatusPartitionFailure = 0xC0000172,

    /// <summary>
    ///     The STATUS_INVALID_BLOCK_LENGTH constant.
    /// </summary>
    StatusInvalidBlockLength = 0xC0000173,

    /// <summary>
    ///     The STATUS_DEVICE_NOT_PARTITIONED constant.
    /// </summary>
    StatusDeviceNotPartitioned = 0xC0000174,

    /// <summary>
    ///     The STATUS_UNABLE_TO_LOCK_MEDIA constant.
    /// </summary>
    StatusUnableToLockMedia = 0xC0000175,

    /// <summary>
    ///     The STATUS_UNABLE_TO_UNLOAD_MEDIA constant.
    /// </summary>
    StatusUnableToUnloadMedia = 0xC0000176,

    /// <summary>
    ///     The STATUS_EOM_OVERFLOW constant.
    /// </summary>
    StatusEomOverflow = 0xC0000177,

    /// <summary>
    ///     The STATUS_NO_MEDIA constant.
    /// </summary>
    StatusNoMedia = 0xC0000178,

    /// <summary>
    ///     The STATUS_NO_SUCH_MEMBER constant.
    /// </summary>
    StatusNoSuchMember = 0xC000017A,

    /// <summary>
    ///     The STATUS_INVALID_MEMBER constant.
    /// </summary>
    StatusInvalidMember = 0xC000017B,

    /// <summary>
    ///     The STATUS_KEY_DELETED constant.
    /// </summary>
    StatusKeyDeleted = 0xC000017C,

    /// <summary>
    ///     The STATUS_NO_LOG_SPACE constant.
    /// </summary>
    StatusNoLogSpace = 0xC000017D,

    /// <summary>
    ///     The STATUS_TOO_MANY_SIDS constant.
    /// </summary>
    StatusTooManySids = 0xC000017E,

    /// <summary>
    ///     The STATUS_LM_CROSS_ENCRYPTION_REQUIRED constant.
    /// </summary>
    StatusLmCrossEncryptionRequired = 0xC000017F,

    /// <summary>
    ///     The STATUS_KEY_HAS_CHILDREN constant.
    /// </summary>
    StatusKeyHasChildren = 0xC0000180,

    /// <summary>
    ///     The STATUS_CHILD_MUST_BE_VOLATILE constant.
    /// </summary>
    StatusChildMustBeVolatile = 0xC0000181,

    /// <summary>
    ///     The STATUS_DEVICE_CONFIGURATION_ERROR constant.
    /// </summary>
    StatusDeviceConfigurationError = 0xC0000182,

    /// <summary>
    ///     The STATUS_DRIVER_INTERNAL_ERROR constant.
    /// </summary>
    StatusDriverInternalError = 0xC0000183,

    /// <summary>
    ///     The STATUS_INVALID_DEVICE_STATE constant.
    /// </summary>
    StatusInvalidDeviceState = 0xC0000184,

    /// <summary>
    ///     The STATUS_IO_DEVICE_ERROR constant.
    /// </summary>
    StatusIoDeviceError = 0xC0000185,

    /// <summary>
    ///     The STATUS_DEVICE_PROTOCOL_ERROR constant.
    /// </summary>
    StatusDeviceProtocolError = 0xC0000186,

    /// <summary>
    ///     The STATUS_BACKUP_CONTROLLER constant.
    /// </summary>
    StatusBackupController = 0xC0000187,

    /// <summary>
    ///     The STATUS_LOG_FILE_FULL constant.
    /// </summary>
    StatusLogFileFull = 0xC0000188,

    /// <summary>
    ///     The STATUS_TOO_LATE constant.
    /// </summary>
    StatusTooLate = 0xC0000189,

    /// <summary>
    ///     The STATUS_NO_TRUST_LSA_SECRET constant.
    /// </summary>
    StatusNoTrustLsaSecret = 0xC000018A,

    /// <summary>
    ///     The STATUS_NO_TRUST_SAM_ACCOUNT constant.
    /// </summary>
    StatusNoTrustSamAccount = 0xC000018B,

    /// <summary>
    ///     The STATUS_TRUSTED_DOMAIN_FAILURE constant.
    /// </summary>
    StatusTrustedDomainFailure = 0xC000018C,

    /// <summary>
    ///     The STATUS_TRUSTED_RELATIONSHIP_FAILURE constant.
    /// </summary>
    StatusTrustedRelationshipFailure = 0xC000018D,

    /// <summary>
    ///     The STATUS_EVENTLOG_FILE_CORRUPT constant.
    /// </summary>
    StatusEventLogFileCorrupt = 0xC000018E,

    /// <summary>
    ///     The STATUS_EVENTLOG_CANT_START constant.
    /// </summary>
    StatusEventLogCantStart = 0xC000018F,

    /// <summary>
    ///     The STATUS_TRUST_FAILURE constant.
    /// </summary>
    StatusTrustFailure = 0xC0000190,

    /// <summary>
    ///     The STATUS_MUTANT_LIMIT_EXCEEDED constant.
    /// </summary>
    StatusMutantLimitExceeded = 0xC0000191,

    /// <summary>
    ///     The STATUS_NETLOGON_NOT_STARTED constant.
    /// </summary>
    StatusNetLogonNotStarted = 0xC0000192,

    /// <summary>
    ///     The STATUS_ACCOUNT_EXPIRED constant.
    /// </summary>
    StatusAccountExpired = 0xC0000193,

    /// <summary>
    ///     The STATUS_POSSIBLE_DEADLOCK constant.
    /// </summary>
    StatusPossibleDeadlock = 0xC0000194,

    /// <summary>
    ///     The STATUS_NETWORK_CREDENTIAL_CONFLICT constant.
    /// </summary>
    StatusNetworkCredentialConflict = 0xC0000195,

    /// <summary>
    ///     The STATUS_REMOTE_SESSION_LIMIT constant.
    /// </summary>
    StatusRemoteSessionLimit = 0xC0000196,

    /// <summary>
    ///     The STATUS_EVENTLOG_FILE_CHANGED constant.
    /// </summary>
    StatusEventLogFileChanged = 0xC0000197,

    /// <summary>
    ///     The STATUS_NOLOGON_INTERDOMAIN_TRUST_ACCOUNT constant.
    /// </summary>
    StatusNoLogonInterdomainTrustAccount = 0xC0000198,

    /// <summary>
    ///     The STATUS_NOLOGON_WORKSTATION_TRUST_ACCOUNT constant.
    /// </summary>
    StatusNoLogonWorkstationTrustAccount = 0xC0000199,

    /// <summary>
    ///     The STATUS_NOLOGON_SERVER_TRUST_ACCOUNT constant.
    /// </summary>
    StatusNoLogonServerTrustAccount = 0xC000019A,

    /// <summary>
    ///     The STATUS_DOMAIN_TRUST_INCONSISTENT constant.
    /// </summary>
    StatusDomainTrustInconsistent = 0xC000019B,

    /// <summary>
    ///     The STATUS_FS_DRIVER_REQUIRED constant.
    /// </summary>
    StatusFsDriverRequired = 0xC000019C,

    /// <summary>
    ///     The STATUS_IMAGE_ALREADY_LOADED_AS_DLL constant.
    /// </summary>
    StatusImageAlreadyLoadedAsDll = 0xC000019D,

    /// <summary>
    ///     The STATUS_INCOMPATIBLE_WITH_GLOBAL_SHORT_NAME_REGISTRY_SETTING constant.
    /// </summary>
    StatusIncompatibleWithGlobalShortNameRegistrySetting = 0xC000019E,

    /// <summary>
    ///     The STATUS_SHORT_NAMES_NOT_ENABLED_ON_VOLUME constant.
    /// </summary>
    StatusShortNamesNotEnabledOnVolume = 0xC000019F,

    /// <summary>
    ///     The STATUS_SECURITY_STREAM_IS_INCONSISTENT constant.
    /// </summary>
    StatusSecurityStreamIsInconsistent = 0xC00001A0,

    /// <summary>
    ///     The STATUS_INVALID_LOCK_RANGE constant.
    /// </summary>
    StatusInvalidLockRange = 0xC00001A1,

    /// <summary>
    ///     The STATUS_INVALID_ACE_CONDITION constant.
    /// </summary>
    StatusInvalidAceCondition = 0xC00001A2,

    /// <summary>
    ///     The STATUS_IMAGE_SUBSYSTEM_NOT_PRESENT constant.
    /// </summary>
    StatusImageSubsystemNotPresent = 0xC00001A3,

    /// <summary>
    ///     The STATUS_NOTIFICATION_GUID_ALREADY_DEFINED constant.
    /// </summary>
    StatusNotificationGuidAlreadyDefined = 0xC00001A4,

    /// <summary>
    ///     The STATUS_NETWORK_OPEN_RESTRICTION constant.
    /// </summary>
    StatusNetworkOpenRestriction = 0xC0000201,

    /// <summary>
    ///     The STATUS_NO_USER_SESSION_KEY constant.
    /// </summary>
    StatusNoUserSessionKey = 0xC0000202,

    /// <summary>
    ///     The STATUS_USER_SESSION_DELETED constant.
    /// </summary>
    StatusUserSessionDeleted = 0xC0000203,

    /// <summary>
    ///     The STATUS_RESOURCE_LANG_NOT_FOUND constant.
    /// </summary>
    StatusResourceLangNotFound = 0xC0000204,

    /// <summary>
    ///     The STATUS_INSUFF_SERVER_RESOURCES constant.
    /// </summary>
    StatusInsuffServerResources = 0xC0000205,

    /// <summary>
    ///     The STATUS_INVALID_BUFFER_SIZE constant.
    /// </summary>
    StatusInvalidBufferSize = 0xC0000206,

    /// <summary>
    ///     The STATUS_INVALID_ADDRESS_COMPONENT constant.
    /// </summary>
    StatusInvalidAddressComponent = 0xC0000207,

    /// <summary>
    ///     The STATUS_INVALID_ADDRESS_WILDCARD constant.
    /// </summary>
    StatusInvalidAddressWildcard = 0xC0000208,

    /// <summary>
    ///     The STATUS_TOO_MANY_ADDRESSES constant.
    /// </summary>
    StatusTooManyAddresses = 0xC0000209,

    /// <summary>
    ///     The STATUS_ADDRESS_ALREADY_EXISTS constant.
    /// </summary>
    StatusAddressAlreadyExists = 0xC000020A,

    /// <summary>
    ///     The STATUS_ADDRESS_CLOSED constant.
    /// </summary>
    StatusAddressClosed = 0xC000020B,

    /// <summary>
    ///     The STATUS_CONNECTION_DISCONNECTED constant.
    /// </summary>
    StatusConnectionDisconnected = 0xC000020C,

    /// <summary>
    ///     The STATUS_CONNECTION_RESET constant.
    /// </summary>
    StatusConnectionReset = 0xC000020D,

    /// <summary>
    ///     The STATUS_TOO_MANY_NODES constant.
    /// </summary>
    StatusTooManyNodes = 0xC000020E,

    /// <summary>
    ///     The STATUS_TRANSACTION_ABORTED constant.
    /// </summary>
    StatusTransactionAborted = 0xC000020F,

    /// <summary>
    ///     The STATUS_TRANSACTION_TIMED_OUT constant.
    /// </summary>
    StatusTransactionTimedOut = 0xC0000210,

    /// <summary>
    ///     The STATUS_TRANSACTION_NO_RELEASE constant.
    /// </summary>
    StatusTransactionNoRelease = 0xC0000211,

    /// <summary>
    ///     The STATUS_TRANSACTION_NO_MATCH constant.
    /// </summary>
    StatusTransactionNoMatch = 0xC0000212,

    /// <summary>
    ///     The STATUS_TRANSACTION_RESPONDED constant.
    /// </summary>
    StatusTransactionResponded = 0xC0000213,

    /// <summary>
    ///     The STATUS_TRANSACTION_INVALID_ID constant.
    /// </summary>
    StatusTransactionInvalidId = 0xC0000214,

    /// <summary>
    ///     The STATUS_TRANSACTION_INVALID_TYPE constant.
    /// </summary>
    StatusTransactionInvalidType = 0xC0000215,

    /// <summary>
    ///     The STATUS_NOT_SERVER_SESSION constant.
    /// </summary>
    StatusNotServerSession = 0xC0000216,

    /// <summary>
    ///     The STATUS_NOT_CLIENT_SESSION constant.
    /// </summary>
    StatusNotClientSession = 0xC0000217,

    /// <summary>
    ///     The STATUS_CANNOT_LOAD_REGISTRY_FILE constant.
    /// </summary>
    StatusCannotLoadRegistryFile = 0xC0000218,

    /// <summary>
    ///     The STATUS_DEBUG_ATTACH_FAILED constant.
    /// </summary>
    StatusDebugAttachFailed = 0xC0000219,

    /// <summary>
    ///     The STATUS_SYSTEM_PROCESS_TERMINATED constant.
    /// </summary>
    StatusSystemProcessTerminated = 0xC000021A,

    /// <summary>
    ///     The STATUS_DATA_NOT_ACCEPTED constant.
    /// </summary>
    StatusDataNotAccepted = 0xC000021B,

    /// <summary>
    ///     The STATUS_NO_BROWSER_SERVERS_FOUND constant.
    /// </summary>
    StatusNoBrowserServersFound = 0xC000021C,

    /// <summary>
    ///     The STATUS_VDM_HARD_ERROR constant.
    /// </summary>
    StatusVdmHardError = 0xC000021D,

    /// <summary>
    ///     The STATUS_DRIVER_CANCEL_TIMEOUT constant.
    /// </summary>
    StatusDriverCancelTimeout = 0xC000021E,

    /// <summary>
    ///     The STATUS_REPLY_MESSAGE_MISMATCH constant.
    /// </summary>
    StatusReplyMessageMismatch = 0xC000021F,

    /// <summary>
    ///     The STATUS_MAPPED_ALIGNMENT constant.
    /// </summary>
    StatusMappedAlignment = 0xC0000220,

    /// <summary>
    ///     The STATUS_IMAGE_CHECKSUM_MISMATCH constant.
    /// </summary>
    StatusImageChecksumMismatch = 0xC0000221,

    /// <summary>
    ///     The STATUS_LOST_WRITEBEHIND_DATA constant.
    /// </summary>
    StatusLostWriteBehindData = 0xC0000222,

    /// <summary>
    ///     The STATUS_CLIENT_SERVER_PARAMETERS_INVALID constant.
    /// </summary>
    StatusClientServerParametersInvalid = 0xC0000223,

    /// <summary>
    ///     The STATUS_PASSWORD_MUST_CHANGE constant.
    /// </summary>
    StatusPasswordMustChange = 0xC0000224,

    /// <summary>
    ///     The STATUS_NOT_FOUND constant.
    /// </summary>
    StatusNotFound = 0xC0000225,

    /// <summary>
    ///     The STATUS_NOT_TINY_STREAM constant.
    /// </summary>
    StatusNotTinyStream = 0xC0000226,

    /// <summary>
    ///     The STATUS_RECOVERY_FAILURE constant.
    /// </summary>
    StatusRecoveryFailure = 0xC0000227,

    /// <summary>
    ///     The STATUS_STACK_OVERFLOW_READ constant.
    /// </summary>
    StatusStackOverflowRead = 0xC0000228,

    /// <summary>
    ///     The STATUS_FAIL_CHECK constant.
    /// </summary>
    StatusFailCheck = 0xC0000229,

    /// <summary>
    ///     The STATUS_DUPLICATE_OBJECTID constant.
    /// </summary>
    StatusDuplicateObjectId = 0xC000022A,

    /// <summary>
    ///     The STATUS_OBJECTID_EXISTS constant.
    /// </summary>
    StatusObjectIdExists = 0xC000022B,

    /// <summary>
    ///     The STATUS_CONVERT_TO_LARGE constant.
    /// </summary>
    StatusConvertToLarge = 0xC000022C,

    /// <summary>
    ///     The STATUS_RETRY constant.
    /// </summary>
    StatusRetry = 0xC000022D,

    /// <summary>
    ///     The STATUS_FOUND_OUT_OF_SCOPE constant.
    /// </summary>
    StatusFoundOutOfScope = 0xC000022E,

    /// <summary>
    ///     The STATUS_ALLOCATE_BUCKET constant.
    /// </summary>
    StatusAllocateBucket = 0xC000022F,

    /// <summary>
    ///     The STATUS_PROPSET_NOT_FOUND constant.
    /// </summary>
    StatusPropSetNotFound = 0xC0000230,

    /// <summary>
    ///     The STATUS_MARSHALL_OVERFLOW constant.
    /// </summary>
    StatusMarshallOverflow = 0xC0000231,

    /// <summary>
    ///     The STATUS_INVALID_VARIANT constant.
    /// </summary>
    StatusInvalidVariant = 0xC0000232,

    /// <summary>
    ///     The STATUS_DOMAIN_CONTROLLER_NOT_FOUND constant.
    /// </summary>
    StatusDomainControllerNotFound = 0xC0000233,

    /// <summary>
    ///     The STATUS_ACCOUNT_LOCKED_OUT constant.
    /// </summary>
    StatusAccountLockedOut = 0xC0000234,

    /// <summary>
    ///     The STATUS_HANDLE_NOT_CLOSABLE constant.
    /// </summary>
    StatusHandleNotClosable = 0xC0000235,

    /// <summary>
    ///     The STATUS_CONNECTION_REFUSED constant.
    /// </summary>
    StatusConnectionRefused = 0xC0000236,

    /// <summary>
    ///     The STATUS_GRACEFUL_DISCONNECT constant.
    /// </summary>
    StatusGracefulDisconnect = 0xC0000237,

    /// <summary>
    ///     The STATUS_ADDRESS_ALREADY_ASSOCIATED constant.
    /// </summary>
    StatusAddressAlreadyAssociated = 0xC0000238,

    /// <summary>
    ///     The STATUS_ADDRESS_NOT_ASSOCIATED constant.
    /// </summary>
    StatusAddressNotAssociated = 0xC0000239,

    /// <summary>
    ///     The STATUS_CONNECTION_INVALID constant.
    /// </summary>
    StatusConnectionInvalid = 0xC000023A,

    /// <summary>
    ///     The STATUS_CONNECTION_ACTIVE constant.
    /// </summary>
    StatusConnectionActive = 0xC000023B,

    /// <summary>
    ///     The STATUS_NETWORK_UNREACHABLE constant.
    /// </summary>
    StatusNetworkUnreachable = 0xC000023C,

    /// <summary>
    ///     The STATUS_HOST_UNREACHABLE constant.
    /// </summary>
    StatusHostUnreachable = 0xC000023D,

    /// <summary>
    ///     The STATUS_PROTOCOL_UNREACHABLE constant.
    /// </summary>
    StatusProtocolUnreachable = 0xC000023E,

    /// <summary>
    ///     The STATUS_PORT_UNREACHABLE constant.
    /// </summary>
    StatusPortUnreachable = 0xC000023F,

    /// <summary>
    ///     The STATUS_REQUEST_ABORTED constant.
    /// </summary>
    StatusRequestAborted = 0xC0000240,

    /// <summary>
    ///     The STATUS_CONNECTION_ABORTED constant.
    /// </summary>
    StatusConnectionAborted = 0xC0000241,

    /// <summary>
    ///     The STATUS_BAD_COMPRESSION_BUFFER constant.
    /// </summary>
    StatusBadCompressionBuffer = 0xC0000242,

    /// <summary>
    ///     The STATUS_USER_MAPPED_FILE constant.
    /// </summary>
    StatusUserMappedFile = 0xC0000243,

    /// <summary>
    ///     The STATUS_AUDIT_FAILED constant.
    /// </summary>
    StatusAuditFailed = 0xC0000244,

    /// <summary>
    ///     The STATUS_TIMER_RESOLUTION_NOT_SET constant.
    /// </summary>
    StatusTimerResolutionNotSet = 0xC0000245,

    /// <summary>
    ///     The STATUS_CONNECTION_COUNT_LIMIT constant.
    /// </summary>
    StatusConnectionCountLimit = 0xC0000246,

    /// <summary>
    ///     The STATUS_LOGIN_TIME_RESTRICTION constant.
    /// </summary>
    StatusLoginTimeRestriction = 0xC0000247,

    /// <summary>
    ///     The STATUS_LOGIN_WKSTA_RESTRICTION constant.
    /// </summary>
    StatusLoginWkstaRestriction = 0xC0000248,

    /// <summary>
    ///     The STATUS_IMAGE_MP_UP_MISMATCH constant.
    /// </summary>
    StatusImageMpUpMismatch = 0xC0000249,

    /// <summary>
    ///     The STATUS_INSUFFICIENT_LOGON_INFO constant.
    /// </summary>
    StatusInsufficientLogonInfo = 0xC0000250,

    /// <summary>
    ///     The STATUS_BAD_DLL_ENTRYPOINT constant.
    /// </summary>
    StatusBadDllEntryPoint = 0xC0000251,

    /// <summary>
    ///     The STATUS_BAD_SERVICE_ENTRYPOINT constant.
    /// </summary>
    StatusBadServiceEntryPoint = 0xC0000252,

    /// <summary>
    ///     The STATUS_LPC_REPLY_LOST constant.
    /// </summary>
    StatusLpcReplyLost = 0xC0000253,

    /// <summary>
    ///     The STATUS_IP_ADDRESS_CONFLICT1 constant.
    /// </summary>
    StatusIpAddressConflict1 = 0xC0000254,

    /// <summary>
    ///     The STATUS_IP_ADDRESS_CONFLICT2 constant.
    /// </summary>
    StatusIpAddressConflict2 = 0xC0000255,

    /// <summary>
    ///     The STATUS_REGISTRY_QUOTA_LIMIT constant.
    /// </summary>
    StatusRegistryQuotaLimit = 0xC0000256,

    /// <summary>
    ///     The STATUS_PATH_NOT_COVERED constant.
    /// </summary>
    StatusPathNotCovered = 0xC0000257,

    /// <summary>
    ///     The STATUS_NO_CALLBACK_ACTIVE constant.
    /// </summary>
    StatusNoCallbackActive = 0xC0000258,

    /// <summary>
    ///     The STATUS_LICENSE_QUOTA_EXCEEDED constant.
    /// </summary>
    StatusLicenseQuotaExceeded = 0xC0000259,

    /// <summary>
    ///     The STATUS_PWD_TOO_SHORT constant.
    /// </summary>
    StatusPwdTooShort = 0xC000025A,

    /// <summary>
    ///     The STATUS_PWD_TOO_RECENT constant.
    /// </summary>
    StatusPwdTooRecent = 0xC000025B,

    /// <summary>
    ///     The STATUS_PWD_HISTORY_CONFLICT constant.
    /// </summary>
    StatusPwdHistoryConflict = 0xC000025C,

    /// <summary>
    ///     The STATUS_PLUGPLAY_NO_DEVICE constant.
    /// </summary>
    StatusPlugPlayNoDevice = 0xC000025E,

    /// <summary>
    ///     The STATUS_UNSUPPORTED_COMPRESSION constant.
    /// </summary>
    StatusUnsupportedCompression = 0xC000025F,

    /// <summary>
    ///     The STATUS_INVALID_HW_PROFILE constant.
    /// </summary>
    StatusInvalidHwProfile = 0xC0000260,

    /// <summary>
    ///     The STATUS_INVALID_PLUGPLAY_DEVICE_PATH constant.
    /// </summary>
    StatusInvalidPlugPlayDevicePath = 0xC0000261,

    /// <summary>
    ///     The STATUS_DRIVER_ORDINAL_NOT_FOUND constant.
    /// </summary>
    StatusDriverOrdinalNotFound = 0xC0000262,

    /// <summary>
    ///     The STATUS_DRIVER_ENTRYPOINT_NOT_FOUND constant.
    /// </summary>
    StatusDriverEntryPointNotFound = 0xC0000263,

    /// <summary>
    ///     The STATUS_RESOURCE_NOT_OWNED constant.
    /// </summary>
    StatusResourceNotOwned = 0xC0000264,

    /// <summary>
    ///     The STATUS_TOO_MANY_LINKS constant.
    /// </summary>
    StatusTooManyLinks = 0xC0000265,

    /// <summary>
    ///     The STATUS_QUOTA_LIST_INCONSISTENT constant.
    /// </summary>
    StatusQuotaListInconsistent = 0xC0000266,

    /// <summary>
    ///     The STATUS_FILE_IS_OFFLINE constant.
    /// </summary>
    StatusFileIsOffline = 0xC0000267,

    /// <summary>
    ///     The STATUS_EVALUATION_EXPIRATION constant.
    /// </summary>
    StatusEvaluationExpiration = 0xC0000268,

    /// <summary>
    ///     The STATUS_ILLEGAL_DLL_RELOCATION constant.
    /// </summary>
    StatusIllegalDllRelocation = 0xC0000269,

    /// <summary>
    ///     The STATUS_LICENSE_VIOLATION constant.
    /// </summary>
    StatusLicenseViolation = 0xC000026A,

    /// <summary>
    ///     The STATUS_DLL_INIT_FAILED_LOGOFF constant.
    /// </summary>
    StatusDllInitFailedLogoff = 0xC000026B,

    /// <summary>
    ///     The STATUS_DRIVER_UNABLE_TO_LOAD constant.
    /// </summary>
    StatusDriverUnableToLoad = 0xC000026C,

    /// <summary>
    ///     The STATUS_DFS_UNAVAILABLE constant.
    /// </summary>
    StatusDfsUnavailable = 0xC000026D,

    /// <summary>
    ///     The STATUS_VOLUME_DISMOUNTED constant.
    /// </summary>
    StatusVolumeDismounted = 0xC000026E,

    /// <summary>
    ///     The STATUS_WX86_INTERNAL_ERROR constant.
    /// </summary>
    StatusWx86InternalError = 0xC000026F,

    /// <summary>
    ///     The STATUS_WX86_FLOAT_STACK_CHECK constant.
    /// </summary>
    StatusWx86FloatStackCheck = 0xC0000270,

    /// <summary>
    ///     The STATUS_VALIDATE_CONTINUE constant.
    /// </summary>
    StatusValidateContinue = 0xC0000271,

    /// <summary>
    ///     The STATUS_NO_MATCH constant.
    /// </summary>
    StatusNoMatch = 0xC0000272,

    /// <summary>
    ///     The STATUS_NO_MORE_MATCHES constant.
    /// </summary>
    StatusNoMoreMatches = 0xC0000273,

    /// <summary>
    ///     The STATUS_NOT_A_REPARSE_POINT constant.
    /// </summary>
    StatusNotAReparsePoint = 0xC0000275,

    /// <summary>
    ///     The STATUS_IO_REPARSE_TAG_INVALID constant.
    /// </summary>
    StatusIoReparseTagInvalid = 0xC0000276,

    /// <summary>
    ///     The STATUS_IO_REPARSE_TAG_MISMATCH constant.
    /// </summary>
    StatusIoReparseTagMismatch = 0xC0000277,

    /// <summary>
    ///     The STATUS_IO_REPARSE_DATA_INVALID constant.
    /// </summary>
    StatusIoReparseDataInvalid = 0xC0000278,

    /// <summary>
    ///     The STATUS_IO_REPARSE_TAG_NOT_HANDLED constant.
    /// </summary>
    StatusIoReparseTagNotHandled = 0xC0000279,

    /// <summary>
    ///     The STATUS_REPARSE_POINT_NOT_RESOLVED constant.
    /// </summary>
    StatusReparsePointNotResolved = 0xC0000280,

    /// <summary>
    ///     The STATUS_DIRECTORY_IS_A_REPARSE_POINT constant.
    /// </summary>
    StatusDirectoryIsAReparsePoint = 0xC0000281,

    /// <summary>
    ///     The STATUS_RANGE_LIST_CONFLICT constant.
    /// </summary>
    StatusRangeListConflict = 0xC0000282,

    /// <summary>
    ///     The STATUS_SOURCE_ELEMENT_EMPTY constant.
    /// </summary>
    StatusSourceElementEmpty = 0xC0000283,

    /// <summary>
    ///     The STATUS_DESTINATION_ELEMENT_FULL constant.
    /// </summary>
    StatusDestinationElementFull = 0xC0000284,

    /// <summary>
    ///     The STATUS_ILLEGAL_ELEMENT_ADDRESS constant.
    /// </summary>
    StatusIllegalElementAddress = 0xC0000285,

    /// <summary>
    ///     The STATUS_MAGAZINE_NOT_PRESENT constant.
    /// </summary>
    StatusMagazineNotPresent = 0xC0000286,

    /// <summary>
    ///     The STATUS_REINITIALIZATION_NEEDED constant.
    /// </summary>
    StatusReinitializationNeeded = 0xC0000287,

    /// <summary>
    ///     The STATUS_ENCRYPTION_FAILED constant.
    /// </summary>
    StatusEncryptionFailed = 0xC000028A,

    /// <summary>
    ///     The STATUS_DECRYPTION_FAILED constant.
    /// </summary>
    StatusDecryptionFailed = 0xC000028B,

    /// <summary>
    ///     The STATUS_RANGE_NOT_FOUND constant.
    /// </summary>
    StatusRangeNotFound = 0xC000028C,

    /// <summary>
    ///     The STATUS_NO_RECOVERY_POLICY constant.
    /// </summary>
    StatusNoRecoveryPolicy = 0xC000028D,

    /// <summary>
    ///     The STATUS_NO_EFS constant.
    /// </summary>
    StatusNoEfs = 0xC000028E,

    /// <summary>
    ///     The STATUS_WRONG_EFS constant.
    /// </summary>
    StatusWrongEfs = 0xC000028F,

    /// <summary>
    ///     The STATUS_NO_USER_KEYS constant.
    /// </summary>
    StatusNoUserKeys = 0xC0000290,

    /// <summary>
    ///     The STATUS_FILE_NOT_ENCRYPTED constant.
    /// </summary>
    StatusFileNotEncrypted = 0xC0000291,

    /// <summary>
    ///     The STATUS_NOT_EXPORT_FORMAT constant.
    /// </summary>
    StatusNotExportFormat = 0xC0000292,

    /// <summary>
    ///     The STATUS_FILE_ENCRYPTED constant.
    /// </summary>
    StatusFileEncrypted = 0xC0000293,

    /// <summary>
    ///     The STATUS_WMI_GUID_NOT_FOUND constant.
    /// </summary>
    StatusWmiGuidNotFound = 0xC0000295,

    /// <summary>
    ///     The STATUS_WMI_INSTANCE_NOT_FOUND constant.
    /// </summary>
    StatusWmiInstanceNotFound = 0xC0000296,

    /// <summary>
    ///     The STATUS_WMI_ITEMID_NOT_FOUND constant.
    /// </summary>
    StatusWmiItemIdNotFound = 0xC0000297,

    /// <summary>
    ///     The STATUS_WMI_TRY_AGAIN constant.
    /// </summary>
    StatusWmiTryAgain = 0xC0000298,

    /// <summary>
    ///     The STATUS_SHARED_POLICY constant.
    /// </summary>
    StatusSharedPolicy = 0xC0000299,

    /// <summary>
    ///     The STATUS_POLICY_OBJECT_NOT_FOUND constant.
    /// </summary>
    StatusPolicyObjectNotFound = 0xC000029A,

    /// <summary>
    ///     The STATUS_POLICY_ONLY_IN_DS constant.
    /// </summary>
    StatusPolicyOnlyInDs = 0xC000029B,

    /// <summary>
    ///     The STATUS_VOLUME_NOT_UPGRADED constant.
    /// </summary>
    StatusVolumeNotUpgraded = 0xC000029C,

    /// <summary>
    ///     The STATUS_REMOTE_STORAGE_NOT_ACTIVE constant.
    /// </summary>
    StatusRemoteStorageNotActive = 0xC000029D,

    /// <summary>
    ///     The STATUS_REMOTE_STORAGE_MEDIA_ERROR constant.
    /// </summary>
    StatusRemoteStorageMediaError = 0xC000029E,

    /// <summary>
    ///     The STATUS_NO_TRACKING_SERVICE constant.
    /// </summary>
    StatusNoTrackingService = 0xC000029F,

    /// <summary>
    ///     The STATUS_SERVER_SID_MISMATCH constant.
    /// </summary>
    StatusServerSidMismatch = 0xC00002A0,

    /// <summary>
    ///     The STATUS_DS_NO_ATTRIBUTE_OR_VALUE constant.
    /// </summary>
    StatusDsNoAttributeOrValue = 0xC00002A1,

    /// <summary>
    ///     The STATUS_DS_INVALID_ATTRIBUTE_SYNTAX constant.
    /// </summary>
    StatusDsInvalidAttributeSyntax = 0xC00002A2,

    /// <summary>
    ///     The STATUS_DS_ATTRIBUTE_TYPE_UNDEFINED constant.
    /// </summary>
    StatusDsAttributeTypeUndefined = 0xC00002A3,

    /// <summary>
    ///     The STATUS_DS_ATTRIBUTE_OR_VALUE_EXISTS constant.
    /// </summary>
    StatusDsAttributeOrValueExists = 0xC00002A4,

    /// <summary>
    ///     The STATUS_DS_BUSY constant.
    /// </summary>
    StatusDsBusy = 0xC00002A5,

    /// <summary>
    ///     The STATUS_DS_UNAVAILABLE constant.
    /// </summary>
    StatusDsUnavailable = 0xC00002A6,

    /// <summary>
    ///     The STATUS_DS_NO_RIDS_ALLOCATED constant.
    /// </summary>
    StatusDsNoRidsAllocated = 0xC00002A7,

    /// <summary>
    ///     The STATUS_DS_NO_MORE_RIDS constant.
    /// </summary>
    StatusDsNoMoreRids = 0xC00002A8,

    /// <summary>
    ///     The STATUS_DS_INCORRECT_ROLE_OWNER constant.
    /// </summary>
    StatusDsIncorrectRoleOwner = 0xC00002A9,

    /// <summary>
    ///     The STATUS_DS_RIDMGR_INIT_ERROR constant.
    /// </summary>
    StatusDsRidMgrInitError = 0xC00002AA,

    /// <summary>
    ///     The STATUS_DS_OBJ_CLASS_VIOLATION constant.
    /// </summary>
    StatusDsObjClassViolation = 0xC00002AB,

    /// <summary>
    ///     The STATUS_DS_CANT_ON_NON_LEAF constant.
    /// </summary>
    StatusDsCantOnNonLeaf = 0xC00002AC,

    /// <summary>
    ///     The STATUS_DS_CANT_ON_RDN constant.
    /// </summary>
    StatusDsCantOnRdn = 0xC00002AD,

    /// <summary>
    ///     The STATUS_DS_CANT_MOD_OBJ_CLASS constant.
    /// </summary>
    StatusDsCantModObjClass = 0xC00002AE,

    /// <summary>
    ///     The STATUS_DS_CROSS_DOM_MOVE_FAILED constant.
    /// </summary>
    StatusDsCrossDomMoveFailed = 0xC00002AF,

    /// <summary>
    ///     The STATUS_DS_GC_NOT_AVAILABLE constant.
    /// </summary>
    StatusDsGcNotAvailable = 0xC00002B0,

    /// <summary>
    ///     The STATUS_DIRECTORY_SERVICE_REQUIRED constant.
    /// </summary>
    StatusDirectoryServiceRequired = 0xC00002B1,

    /// <summary>
    ///     The STATUS_REPARSE_ATTRIBUTE_CONFLICT constant.
    /// </summary>
    StatusReparseAttributeConflict = 0xC00002B2,

    /// <summary>
    ///     The STATUS_CANT_ENABLE_DENY_ONLY constant.
    /// </summary>
    StatusCantEnableDenyOnly = 0xC00002B3,

    /// <summary>
    ///     The STATUS_FLOAT_MULTIPLE_FAULTS constant.
    /// </summary>
    StatusFloatMultipleFaults = 0xC00002B4,

    /// <summary>
    ///     The STATUS_FLOAT_MULTIPLE_TRAPS constant.
    /// </summary>
    StatusFloatMultipleTraps = 0xC00002B5,

    /// <summary>
    ///     The STATUS_DEVICE_REMOVED constant.
    /// </summary>
    StatusDeviceRemoved = 0xC00002B6,

    /// <summary>
    ///     The STATUS_JOURNAL_DELETE_IN_PROGRESS constant.
    /// </summary>
    StatusJournalDeleteInProgress = 0xC00002B7,

    /// <summary>
    ///     The STATUS_JOURNAL_NOT_ACTIVE constant.
    /// </summary>
    StatusJournalNotActive = 0xC00002B8,

    /// <summary>
    ///     The STATUS_NOINTERFACE constant.
    /// </summary>
    StatusNoInterface = 0xC00002B9,

    /// <summary>
    ///     The STATUS_DS_ADMIN_LIMIT_EXCEEDED constant.
    /// </summary>
    StatusDsAdminLimitExceeded = 0xC00002C1,

    /// <summary>
    ///     The STATUS_DRIVER_FAILED_SLEEP constant.
    /// </summary>
    StatusDriverFailedSleep = 0xC00002C2,

    /// <summary>
    ///     The STATUS_MUTUAL_AUTHENTICATION_FAILED constant.
    /// </summary>
    StatusMutualAuthenticationFailed = 0xC00002C3,

    /// <summary>
    ///     The STATUS_CORRUPT_SYSTEM_FILE constant.
    /// </summary>
    StatusCorruptSystemFile = 0xC00002C4,

    /// <summary>
    ///     The STATUS_DATATYPE_MISALIGNMENT_ERROR constant.
    /// </summary>
    StatusDatatypeMisalignmentError = 0xC00002C5,

    /// <summary>
    ///     The STATUS_WMI_READ_ONLY constant.
    /// </summary>
    StatusWmiReadOnly = 0xC00002C6,

    /// <summary>
    ///     The STATUS_WMI_SET_FAILURE constant.
    /// </summary>
    StatusWmiSetFailure = 0xC00002C7,

    /// <summary>
    ///     The STATUS_COMMITMENT_MINIMUM constant.
    /// </summary>
    StatusCommitmentMinimum = 0xC00002C8,

    /// <summary>
    ///     The STATUS_REG_NAT_CONSUMPTION constant.
    /// </summary>
    StatusRegNatConsumption = 0xC00002C9,

    /// <summary>
    ///     The STATUS_TRANSPORT_FULL constant.
    /// </summary>
    StatusTransportFull = 0xC00002CA,

    /// <summary>
    ///     The STATUS_DS_SAM_INIT_FAILURE constant.
    /// </summary>
    StatusDsSamInitFailure = 0xC00002CB,

    /// <summary>
    ///     The STATUS_ONLY_IF_CONNECTED constant.
    /// </summary>
    StatusOnlyIfConnected = 0xC00002CC,

    /// <summary>
    ///     The STATUS_DS_SENSITIVE_GROUP_VIOLATION constant.
    /// </summary>
    StatusDsSensitiveGroupViolation = 0xC00002CD,

    /// <summary>
    ///     The STATUS_PNP_RESTART_ENUMERATION constant.
    /// </summary>
    StatusPnpRestartEnumeration = 0xC00002CE,

    /// <summary>
    ///     The STATUS_JOURNAL_ENTRY_DELETED constant.
    /// </summary>
    StatusJournalEntryDeleted = 0xC00002CF,

    /// <summary>
    ///     The STATUS_DS_CANT_MOD_PRIMARYGROUPID constant.
    /// </summary>
    StatusDsCantModPrimaryGroupId = 0xC00002D0,

    /// <summary>
    ///     The STATUS_SYSTEM_IMAGE_BAD_SIGNATURE constant.
    /// </summary>
    StatusSystemImageBadSignature = 0xC00002D1,

    /// <summary>
    ///     The STATUS_PNP_REBOOT_REQUIRED constant.
    /// </summary>
    StatusPnpRebootRequired = 0xC00002D2,

    /// <summary>
    ///     The STATUS_POWER_STATE_INVALID constant.
    /// </summary>
    StatusPowerStateInvalid = 0xC00002D3,

    /// <summary>
    ///     The STATUS_DS_INVALID_GROUP_TYPE constant.
    /// </summary>
    StatusDsInvalidGroupType = 0xC00002D4,

    /// <summary>
    ///     The STATUS_DS_NO_NEST_GLOBALGROUP_IN_MIXEDDOMAIN constant.
    /// </summary>
    StatusDsNoNestGlobalGroupInMixedDomain = 0xC00002D5,

    /// <summary>
    ///     The STATUS_DS_NO_NEST_LOCALGROUP_IN_MIXEDDOMAIN constant.
    /// </summary>
    StatusDsNoNestLocalGroupInMixedDomain = 0xC00002D6,

    /// <summary>
    ///     The STATUS_DS_GLOBAL_CANT_HAVE_LOCAL_MEMBER constant.
    /// </summary>
    StatusDsGlobalCantHaveLocalMember = 0xC00002D7,

    /// <summary>
    ///     The STATUS_DS_GLOBAL_CANT_HAVE_UNIVERSAL_MEMBER constant.
    /// </summary>
    StatusDsGlobalCantHaveUniversalMember = 0xC00002D8,

    /// <summary>
    ///     The STATUS_DS_UNIVERSAL_CANT_HAVE_LOCAL_MEMBER constant.
    /// </summary>
    StatusDsUniversalCantHaveLocalMember = 0xC00002D9,

    /// <summary>
    ///     The STATUS_DS_GLOBAL_CANT_HAVE_CROSSDOMAIN_MEMBER constant.
    /// </summary>
    StatusDsGlobalCantHaveCrossDomainMember = 0xC00002DA,

    /// <summary>
    ///     The STATUS_DS_LOCAL_CANT_HAVE_CROSSDOMAIN_LOCAL_MEMBER constant.
    /// </summary>
    StatusDsLocalCantHaveCrossDomainLocalMember = 0xC00002DB,

    /// <summary>
    ///     The STATUS_DS_HAVE_PRIMARY_MEMBERS constant.
    /// </summary>
    StatusDsHavePrimaryMembers = 0xC00002DC,

    /// <summary>
    ///     The STATUS_WMI_NOT_SUPPORTED constant.
    /// </summary>
    StatusWmiNotSupported = 0xC00002DD,

    /// <summary>
    ///     The STATUS_INSUFFICIENT_POWER constant.
    /// </summary>
    StatusInsufficientPower = 0xC00002DE,

    /// <summary>
    ///     The STATUS_SAM_NEED_BOOTKEY_PASSWORD constant.
    /// </summary>
    StatusSamNeedBootKeyPassword = 0xC00002DF,

    /// <summary>
    ///     The STATUS_SAM_NEED_BOOTKEY_FLOPPY constant.
    /// </summary>
    StatusSamNeedBootKeyFloppy = 0xC00002E0,

    /// <summary>
    ///     The STATUS_DS_CANT_START constant.
    /// </summary>
    StatusDsCantStart = 0xC00002E1,

    /// <summary>
    ///     The STATUS_DS_INIT_FAILURE constant.
    /// </summary>
    StatusDsInitFailure = 0xC00002E2,

    /// <summary>
    ///     The STATUS_SAM_INIT_FAILURE constant.
    /// </summary>
    StatusSamInitFailure = 0xC00002E3,

    /// <summary>
    ///     The STATUS_DS_GC_REQUIRED constant.
    /// </summary>
    StatusDsGcRequired = 0xC00002E4,

    /// <summary>
    ///     The STATUS_DS_LOCAL_MEMBER_OF_LOCAL_ONLY constant.
    /// </summary>
    StatusDsLocalMemberOfLocalOnly = 0xC00002E5,

    /// <summary>
    ///     The STATUS_DS_NO_FPO_IN_UNIVERSAL_GROUPS constant.
    /// </summary>
    StatusDsNoFpoInUniversalGroups = 0xC00002E6,

    /// <summary>
    ///     The STATUS_DS_MACHINE_ACCOUNT_QUOTA_EXCEEDED constant.
    /// </summary>
    StatusDsMachineAccountQuotaExceeded = 0xC00002E7,

    /// <summary>
    ///     The STATUS_CURRENT_DOMAIN_NOT_ALLOWED constant.
    /// </summary>
    StatusCurrentDomainNotAllowed = 0xC00002E9,

    /// <summary>
    ///     The STATUS_CANNOT_MAKE constant.
    /// </summary>
    StatusCannotMake = 0xC00002EA,

    /// <summary>
    ///     The STATUS_SYSTEM_SHUTDOWN constant.
    /// </summary>
    StatusSystemShutdown = 0xC00002EB,

    /// <summary>
    ///     The STATUS_DS_INIT_FAILURE_CONSOLE constant.
    /// </summary>
    StatusDsInitFailureConsole = 0xC00002EC,

    /// <summary>
    ///     The STATUS_DS_SAM_INIT_FAILURE_CONSOLE constant.
    /// </summary>
    StatusDsSamInitFailureConsole = 0xC00002ED,

    /// <summary>
    ///     The STATUS_UNFINISHED_CONTEXT_DELETED constant.
    /// </summary>
    StatusUnfinishedContextDeleted = 0xC00002EE,

    /// <summary>
    ///     The STATUS_NO_TGT_REPLY constant.
    /// </summary>
    StatusNoTgtReply = 0xC00002EF,

    /// <summary>
    ///     The STATUS_OBJECTID_NOT_FOUND constant.
    /// </summary>
    StatusObjectIdNotFound = 0xC00002F0,

    /// <summary>
    ///     The STATUS_NO_IP_ADDRESSES constant.
    /// </summary>
    StatusNoIpAddresses = 0xC00002F1,

    /// <summary>
    ///     The STATUS_WRONG_CREDENTIAL_HANDLE constant.
    /// </summary>
    StatusWrongCredentialHandle = 0xC00002F2,

    /// <summary>
    ///     The STATUS_CRYPTO_SYSTEM_INVALID constant.
    /// </summary>
    StatusCryptoSystemInvalid = 0xC00002F3,

    /// <summary>
    ///     The STATUS_MAX_REFERRALS_EXCEEDED constant.
    /// </summary>
    StatusMaxReferralsExceeded = 0xC00002F4,

    /// <summary>
    ///     The STATUS_MUST_BE_KDC constant.
    /// </summary>
    StatusMustBeKdc = 0xC00002F5,

    /// <summary>
    ///     The STATUS_STRONG_CRYPTO_NOT_SUPPORTED constant.
    /// </summary>
    StatusStrongCryptoNotSupported = 0xC00002F6,

    /// <summary>
    ///     The STATUS_TOO_MANY_PRINCIPALS constant.
    /// </summary>
    StatusTooManyPrincipals = 0xC00002F7,

    /// <summary>
    ///     The STATUS_NO_PA_DATA constant.
    /// </summary>
    StatusNoPaData = 0xC00002F8,

    /// <summary>
    ///     The STATUS_PKINIT_NAME_MISMATCH constant.
    /// </summary>
    StatusPkInitNameMismatch = 0xC00002F9,

    /// <summary>
    ///     The STATUS_SMARTCARD_LOGON_REQUIRED constant.
    /// </summary>
    StatusSmartcardLogonRequired = 0xC00002FA,

    /// <summary>
    ///     The STATUS_KDC_INVALID_REQUEST constant.
    /// </summary>
    StatusKdcInvalidRequest = 0xC00002FB,

    /// <summary>
    ///     The STATUS_KDC_UNABLE_TO_REFER constant.
    /// </summary>
    StatusKdcUnableToRefer = 0xC00002FC,

    /// <summary>
    ///     The STATUS_KDC_UNKNOWN_ETYPE constant.
    /// </summary>
    StatusKdcUnknownEType = 0xC00002FD,

    /// <summary>
    ///     The STATUS_SHUTDOWN_IN_PROGRESS constant.
    /// </summary>
    StatusShutdownInProgress = 0xC00002FE,

    /// <summary>
    ///     The STATUS_SERVER_SHUTDOWN_IN_PROGRESS constant.
    /// </summary>
    StatusServerShutdownInProgress = 0xC00002FF,

    /// <summary>
    ///     The STATUS_NOT_SUPPORTED_ON_SBS constant.
    /// </summary>
    StatusNotSupportedOnSbs = 0xC0000300,

    /// <summary>
    ///     The STATUS_WMI_GUID_DISCONNECTED constant.
    /// </summary>
    StatusWmiGuidDisconnected = 0xC0000301,

    /// <summary>
    ///     The STATUS_WMI_ALREADY_DISABLED constant.
    /// </summary>
    StatusWmiAlreadyDisabled = 0xC0000302,

    /// <summary>
    ///     The STATUS_WMI_ALREADY_ENABLED constant.
    /// </summary>
    StatusWmiAlreadyEnabled = 0xC0000303,

    /// <summary>
    ///     The STATUS_MFT_TOO_FRAGMENTED constant.
    /// </summary>
    StatusMftTooFragmented = 0xC0000304,

    /// <summary>
    ///     The STATUS_COPY_PROTECTION_FAILURE constant.
    /// </summary>
    StatusCopyProtectionFailure = 0xC0000305,

    /// <summary>
    ///     The STATUS_CSS_AUTHENTICATION_FAILURE constant.
    /// </summary>
    StatusCssAuthenticationFailure = 0xC0000306,

    /// <summary>
    ///     The STATUS_CSS_KEY_NOT_PRESENT constant.
    /// </summary>
    StatusCssKeyNotPresent = 0xC0000307,

    /// <summary>
    ///     The STATUS_CSS_KEY_NOT_ESTABLISHED constant.
    /// </summary>
    StatusCssKeyNotEstablished = 0xC0000308,

    /// <summary>
    ///     The STATUS_CSS_SCRAMBLED_SECTOR constant.
    /// </summary>
    StatusCssScrambledSector = 0xC0000309,

    /// <summary>
    ///     The STATUS_CSS_REGION_MISMATCH constant.
    /// </summary>
    StatusCssRegionMismatch = 0xC000030A,

    /// <summary>
    ///     The STATUS_CSS_RESETS_EXHAUSTED constant.
    /// </summary>
    StatusCssResetsExhausted = 0xC000030B,

    /// <summary>
    ///     The STATUS_PKINIT_FAILURE constant.
    /// </summary>
    StatusPkInitFailure = 0xC0000320,

    /// <summary>
    ///     The STATUS_SMARTCARD_SUBSYSTEM_FAILURE constant.
    /// </summary>
    StatusSmartcardSubsystemFailure = 0xC0000321,

    /// <summary>
    ///     The STATUS_NO_KERB_KEY constant.
    /// </summary>
    StatusNoKerbKey = 0xC0000322,

    /// <summary>
    ///     The STATUS_HOST_DOWN constant.
    /// </summary>
    StatusHostDown = 0xC0000350,

    /// <summary>
    ///     The STATUS_UNSUPPORTED_PREAUTH constant.
    /// </summary>
    StatusUnsupportedPreAuth = 0xC0000351,

    /// <summary>
    ///     The STATUS_EFS_ALG_BLOB_TOO_BIG constant.
    /// </summary>
    StatusEfsAlgBlobTooBig = 0xC0000352,

    /// <summary>
    ///     The STATUS_PORT_NOT_SET constant.
    /// </summary>
    StatusPortNotSet = 0xC0000353,

    /// <summary>
    ///     The STATUS_DEBUGGER_INACTIVE constant.
    /// </summary>
    StatusDebuggerInactive = 0xC0000354,

    /// <summary>
    ///     The STATUS_DS_VERSION_CHECK_FAILURE constant.
    /// </summary>
    StatusDsVersionCheckFailure = 0xC0000355,

    /// <summary>
    ///     The STATUS_AUDITING_DISABLED constant.
    /// </summary>
    StatusAuditingDisabled = 0xC0000356,

    /// <summary>
    ///     The STATUS_PRENT4_MACHINE_ACCOUNT constant.
    /// </summary>
    StatusPreNt4MachineAccount = 0xC0000357,

    /// <summary>
    ///     The STATUS_DS_AG_CANT_HAVE_UNIVERSAL_MEMBER constant.
    /// </summary>
    StatusDsAgCantHaveUniversalMember = 0xC0000358,

    /// <summary>
    ///     The STATUS_INVALID_IMAGE_WIN_32 constant.
    /// </summary>
    StatusInvalidImageWin32 = 0xC0000359,

    /// <summary>
    ///     The STATUS_INVALID_IMAGE_WIN_64 constant.
    /// </summary>
    StatusInvalidImageWin64 = 0xC000035A,

    /// <summary>
    ///     The STATUS_BAD_BINDINGS constant.
    /// </summary>
    StatusBadBindings = 0xC000035B,

    /// <summary>
    ///     The STATUS_NETWORK_SESSION_EXPIRED constant.
    /// </summary>
    StatusNetworkSessionExpired = 0xC000035C,

    /// <summary>
    ///     The STATUS_APPHELP_BLOCK constant.
    /// </summary>
    StatusAppHelpBlock = 0xC000035D,

    /// <summary>
    ///     The STATUS_ALL_SIDS_FILTERED constant.
    /// </summary>
    StatusAllSidsFiltered = 0xC000035E,

    /// <summary>
    ///     The STATUS_NOT_SAFE_MODE_DRIVER constant.
    /// </summary>
    StatusNotSafeModeDriver = 0xC000035F,

    /// <summary>
    ///     The STATUS_ACCESS_DISABLED_BY_POLICY_DEFAULT constant.
    /// </summary>
    StatusAccessDisabledByPolicyDefault = 0xC0000361,

    /// <summary>
    ///     The STATUS_ACCESS_DISABLED_BY_POLICY_PATH constant.
    /// </summary>
    StatusAccessDisabledByPolicyPath = 0xC0000362,

    /// <summary>
    ///     The STATUS_ACCESS_DISABLED_BY_POLICY_PUBLISHER constant.
    /// </summary>
    StatusAccessDisabledByPolicyPublisher = 0xC0000363,

    /// <summary>
    ///     The STATUS_ACCESS_DISABLED_BY_POLICY_OTHER constant.
    /// </summary>
    StatusAccessDisabledByPolicyOther = 0xC0000364,

    /// <summary>
    ///     The STATUS_FAILED_DRIVER_ENTRY constant.
    /// </summary>
    StatusFailedDriverEntry = 0xC0000365,

    /// <summary>
    ///     The STATUS_DEVICE_ENUMERATION_ERROR constant.
    /// </summary>
    StatusDeviceEnumerationError = 0xC0000366,

    /// <summary>
    ///     The STATUS_MOUNT_POINT_NOT_RESOLVED constant.
    /// </summary>
    StatusMountPointNotResolved = 0xC0000368,

    /// <summary>
    ///     The STATUS_INVALID_DEVICE_OBJECT_PARAMETER constant.
    /// </summary>
    StatusInvalidDeviceObjectParameter = 0xC0000369,

    /// <summary>
    ///     The STATUS_MCA_OCCURED constant.
    /// </summary>
    StatusMcaOccurred = 0xC000036A,

    /// <summary>
    ///     The STATUS_DRIVER_BLOCKED_CRITICAL constant.
    /// </summary>
    StatusDriverBlockedCritical = 0xC000036B,

    /// <summary>
    ///     The STATUS_DRIVER_BLOCKED constant.
    /// </summary>
    StatusDriverBlocked = 0xC000036C,

    /// <summary>
    ///     The STATUS_DRIVER_DATABASE_ERROR constant.
    /// </summary>
    StatusDriverDatabaseError = 0xC000036D,

    /// <summary>
    ///     The STATUS_SYSTEM_HIVE_TOO_LARGE constant.
    /// </summary>
    StatusSystemHiveTooLarge = 0xC000036E,

    /// <summary>
    ///     The STATUS_INVALID_IMPORT_OF_NON_DLL constant.
    /// </summary>
    StatusInvalidImportOfNonDll = 0xC000036F,

    /// <summary>
    ///     The STATUS_NO_SECRETS constant.
    /// </summary>
    StatusNoSecrets = 0xC0000371,

    /// <summary>
    ///     The STATUS_ACCESS_DISABLED_NO_SAFER_UI_BY_POLICY constant.
    /// </summary>
    StatusAccessDisabledNoSaferUiByPolicy = 0xC0000372,

    /// <summary>
    ///     The STATUS_FAILED_STACK_SWITCH constant.
    /// </summary>
    StatusFailedStackSwitch = 0xC0000373,

    /// <summary>
    ///     The STATUS_HEAP_CORRUPTION constant.
    /// </summary>
    StatusHeapCorruption = 0xC0000374,

    /// <summary>
    ///     The STATUS_SMARTCARD_WRONG_PIN constant.
    /// </summary>
    StatusSmartcardWrongPin = 0xC0000380,

    /// <summary>
    ///     The STATUS_SMARTCARD_CARD_BLOCKED constant.
    /// </summary>
    StatusSmartcardCardBlocked = 0xC0000381,

    /// <summary>
    ///     The STATUS_SMARTCARD_CARD_NOT_AUTHENTICATED constant.
    /// </summary>
    StatusSmartcardCardNotAuthenticated = 0xC0000382,

    /// <summary>
    ///     The STATUS_SMARTCARD_NO_CARD constant.
    /// </summary>
    StatusSmartcardNoCard = 0xC0000383,

    /// <summary>
    ///     The STATUS_SMARTCARD_NO_KEY_CONTAINER constant.
    /// </summary>
    StatusSmartcardNoKeyContainer = 0xC0000384,

    /// <summary>
    ///     The STATUS_SMARTCARD_NO_CERTIFICATE constant.
    /// </summary>
    StatusSmartcardNoCertificate = 0xC0000385,

    /// <summary>
    ///     The STATUS_SMARTCARD_NO_KEYSET constant.
    /// </summary>
    StatusSmartcardNoKeyset = 0xC0000386,

    /// <summary>
    ///     The STATUS_SMARTCARD_IO_ERROR constant.
    /// </summary>
    StatusSmartcardIoError = 0xC0000387,

    /// <summary>
    ///     The STATUS_DOWNGRADE_DETECTED constant.
    /// </summary>
    StatusDowngradeDetected = 0xC0000388,

    /// <summary>
    ///     The STATUS_SMARTCARD_CERT_REVOKED constant.
    /// </summary>
    StatusSmartcardCertRevoked = 0xC0000389,

    /// <summary>
    ///     The STATUS_ISSUING_CA_UNTRUSTED constant.
    /// </summary>
    StatusIssuingCaUntrusted = 0xC000038A,

    /// <summary>
    ///     The STATUS_REVOCATION_OFFLINE_C constant.
    /// </summary>
    StatusRevocationOfflineC = 0xC000038B,

    /// <summary>
    ///     The STATUS_PKINIT_CLIENT_FAILURE constant.
    /// </summary>
    StatusPkInitClientFailure = 0xC000038C,

    /// <summary>
    ///     The STATUS_SMARTCARD_CERT_EXPIRED constant.
    /// </summary>
    StatusSmartcardCertExpired = 0xC000038D,

    /// <summary>
    ///     The STATUS_DRIVER_FAILED_PRIOR_UNLOAD constant.
    /// </summary>
    StatusDriverFailedPriorUnload = 0xC000038E,

    /// <summary>
    ///     The STATUS_SMARTCARD_SILENT_CONTEXT constant.
    /// </summary>
    StatusSmartcardSilentContext = 0xC000038F,

    /// <summary>
    ///     The STATUS_PER_USER_TRUST_QUOTA_EXCEEDED constant.
    /// </summary>
    StatusPerUserTrustQuotaExceeded = 0xC0000401,

    /// <summary>
    ///     The STATUS_ALL_USER_TRUST_QUOTA_EXCEEDED constant.
    /// </summary>
    StatusAllUserTrustQuotaExceeded = 0xC0000402,

    /// <summary>
    ///     The STATUS_USER_DELETE_TRUST_QUOTA_EXCEEDED constant.
    /// </summary>
    StatusUserDeleteTrustQuotaExceeded = 0xC0000403,

    /// <summary>
    ///     The STATUS_DS_NAME_NOT_UNIQUE constant.
    /// </summary>
    StatusDsNameNotUnique = 0xC0000404,

    /// <summary>
    ///     The STATUS_DS_DUPLICATE_ID_FOUND constant.
    /// </summary>
    StatusDsDuplicateIdFound = 0xC0000405,

    /// <summary>
    ///     The STATUS_DS_GROUP_CONVERSION_ERROR constant.
    /// </summary>
    StatusDsGroupConversionError = 0xC0000406,

    /// <summary>
    ///     The STATUS_VOLSNAP_PREPARE_HIBERNATE constant.
    /// </summary>
    StatusVolSnapPrepareHibernate = 0xC0000407,

    /// <summary>
    ///     The STATUS_USER2USER_REQUIRED constant.
    /// </summary>
    StatusUser2userRequired = 0xC0000408,

    /// <summary>
    ///     The STATUS_STACK_BUFFER_OVERRUN constant.
    /// </summary>
    StatusStackBufferOverrun = 0xC0000409,

    /// <summary>
    ///     The STATUS_NO_S4U_PROT_SUPPORT constant.
    /// </summary>
    StatusNoS4uProtSupport = 0xC000040A,

    /// <summary>
    ///     The STATUS_CROSSREALM_DELEGATION_FAILURE constant.
    /// </summary>
    StatusCrossRealmDelegationFailure = 0xC000040B,

    /// <summary>
    ///     The STATUS_REVOCATION_OFFLINE_KDC constant.
    /// </summary>
    StatusRevocationOfflineKdc = 0xC000040C,

    /// <summary>
    ///     The STATUS_ISSUING_CA_UNTRUSTED_KDC constant.
    /// </summary>
    StatusIssuingCaUntrustedKdc = 0xC000040D,

    /// <summary>
    ///     The STATUS_KDC_CERT_EXPIRED constant.
    /// </summary>
    StatusKdcCertExpired = 0xC000040E,

    /// <summary>
    ///     The STATUS_KDC_CERT_REVOKED constant.
    /// </summary>
    StatusKdcCertRevoked = 0xC000040F,

    /// <summary>
    ///     The STATUS_PARAMETER_QUOTA_EXCEEDED constant.
    /// </summary>
    StatusParameterQuotaExceeded = 0xC0000410,

    /// <summary>
    ///     The STATUS_HIBERNATION_FAILURE constant.
    /// </summary>
    StatusHibernationFailure = 0xC0000411,

    /// <summary>
    ///     The STATUS_DELAY_LOAD_FAILED constant.
    /// </summary>
    StatusDelayLoadFailed = 0xC0000412,

    /// <summary>
    ///     The STATUS_AUTHENTICATION_FIREWALL_FAILED constant.
    /// </summary>
    StatusAuthenticationFirewallFailed = 0xC0000413,

    /// <summary>
    ///     The STATUS_VDM_DISALLOWED constant.
    /// </summary>
    StatusVdmDisallowed = 0xC0000414,

    /// <summary>
    ///     The STATUS_HUNG_DISPLAY_DRIVER_THREAD constant.
    /// </summary>
    StatusHungDisplayDriverThread = 0xC0000415,

    /// <summary>
    ///     The STATUS_INSUFFICIENT_RESOURCE_FOR_SPECIFIED_SHARED_SECTION_SIZE constant.
    /// </summary>
    StatusInsufficientResourceForSpecifiedSharedSectionSize = 0xC0000416,

    /// <summary>
    ///     The STATUS_INVALID_CRUNTIME_PARAMETER constant.
    /// </summary>
    StatusInvalidCRuntimeParameter = 0xC0000417,

    /// <summary>
    ///     The STATUS_NTLM_BLOCKED constant.
    /// </summary>
    StatusNtlmBlocked = 0xC0000418,

    /// <summary>
    ///     The STATUS_DS_SRC_SID_EXISTS_IN_FOREST constant.
    /// </summary>
    StatusDsSrcSidExistsInForest = 0xC0000419,

    /// <summary>
    ///     The STATUS_DS_DOMAIN_NAME_EXISTS_IN_FOREST constant.
    /// </summary>
    StatusDsDomainNameExistsInForest = 0xC000041A,

    /// <summary>
    ///     The STATUS_DS_FLAT_NAME_EXISTS_IN_FOREST constant.
    /// </summary>
    StatusDsFlatNameExistsInForest = 0xC000041B,

    /// <summary>
    ///     The STATUS_INVALID_USER_PRINCIPAL_NAME constant.
    /// </summary>
    StatusInvalidUserPrincipalName = 0xC000041C,

    /// <summary>
    ///     The STATUS_ASSERTION_FAILURE constant.
    /// </summary>
    StatusAssertionFailure = 0xC0000420,

    /// <summary>
    ///     The STATUS_VERIFIER_STOP constant.
    /// </summary>
    StatusVerifierStop = 0xC0000421,

    /// <summary>
    ///     The STATUS_CALLBACK_POP_STACK constant.
    /// </summary>
    StatusCallbackPopStack = 0xC0000423,

    /// <summary>
    ///     The STATUS_INCOMPATIBLE_DRIVER_BLOCKED constant.
    /// </summary>
    StatusIncompatibleDriverBlocked = 0xC0000424,

    /// <summary>
    ///     The STATUS_HIVE_UNLOADED constant.
    /// </summary>
    StatusHiveUnloaded = 0xC0000425,

    /// <summary>
    ///     The STATUS_COMPRESSION_DISABLED constant.
    /// </summary>
    StatusCompressionDisabled = 0xC0000426,

    /// <summary>
    ///     The STATUS_FILE_SYSTEM_LIMITATION constant.
    /// </summary>
    StatusFileSystemLimitation = 0xC0000427,

    /// <summary>
    ///     The STATUS_INVALID_IMAGE_HASH constant.
    /// </summary>
    StatusInvalidImageHash = 0xC0000428,

    /// <summary>
    ///     The STATUS_NOT_CAPABLE constant.
    /// </summary>
    StatusNotCapable = 0xC0000429,

    /// <summary>
    ///     The STATUS_REQUEST_OUT_OF_SEQUENCE constant.
    /// </summary>
    StatusRequestOutOfSequence = 0xC000042A,

    /// <summary>
    ///     The STATUS_IMPLEMENTATION_LIMIT constant.
    /// </summary>
    StatusImplementationLimit = 0xC000042B,

    /// <summary>
    ///     The STATUS_ELEVATION_REQUIRED constant.
    /// </summary>
    StatusElevationRequired = 0xC000042C,

    /// <summary>
    ///     The STATUS_NO_SECURITY_CONTEXT constant.
    /// </summary>
    StatusNoSecurityContext = 0xC000042D,

    /// <summary>
    ///     The STATUS_PKU2U_CERT_FAILURE constant.
    /// </summary>
    StatusPku2uCertFailure = 0xC000042E,

    /// <summary>
    ///     The STATUS_BEYOND_VDL constant.
    /// </summary>
    StatusBeyondVdl = 0xC0000432,

    /// <summary>
    ///     The STATUS_ENCOUNTERED_WRITE_IN_PROGRESS constant.
    /// </summary>
    StatusEncounteredWriteInProgress = 0xC0000433,

    /// <summary>
    ///     The STATUS_PTE_CHANGED constant.
    /// </summary>
    StatusPteChanged = 0xC0000434,

    /// <summary>
    ///     The STATUS_PURGE_FAILED constant.
    /// </summary>
    StatusPurgeFailed = 0xC0000435,

    /// <summary>
    ///     The STATUS_CRED_REQUIRES_CONFIRMATION constant.
    /// </summary>
    StatusCredRequiresConfirmation = 0xC0000440,

    /// <summary>
    ///     The STATUS_CS_ENCRYPTION_INVALID_SERVER_RESPONSE constant.
    /// </summary>
    StatusCsEncryptionInvalidServerResponse = 0xC0000441,

    /// <summary>
    ///     The STATUS_CS_ENCRYPTION_UNSUPPORTED_SERVER constant.
    /// </summary>
    StatusCsEncryptionUnsupportedServer = 0xC0000442,

    /// <summary>
    ///     The STATUS_CS_ENCRYPTION_EXISTING_ENCRYPTED_FILE constant.
    /// </summary>
    StatusCsEncryptionExistingEncryptedFile = 0xC0000443,

    /// <summary>
    ///     The STATUS_CS_ENCRYPTION_NEW_ENCRYPTED_FILE constant.
    /// </summary>
    StatusCsEncryptionNewEncryptedFile = 0xC0000444,

    /// <summary>
    ///     The STATUS_CS_ENCRYPTION_FILE_NOT_CSE constant.
    /// </summary>
    StatusCsEncryptionFileNotCse = 0xC0000445,

    /// <summary>
    ///     The STATUS_INVALID_LABEL constant.
    /// </summary>
    StatusInvalidLabel = 0xC0000446,

    /// <summary>
    ///     The STATUS_DRIVER_PROCESS_TERMINATED constant.
    /// </summary>
    StatusDriverProcessTerminated = 0xC0000450,

    /// <summary>
    ///     The STATUS_AMBIGUOUS_SYSTEM_DEVICE constant.
    /// </summary>
    StatusAmbiguousSystemDevice = 0xC0000451,

    /// <summary>
    ///     The STATUS_SYSTEM_DEVICE_NOT_FOUND constant.
    /// </summary>
    StatusSystemDeviceNotFound = 0xC0000452,

    /// <summary>
    ///     The STATUS_RESTART_BOOT_APPLICATION constant.
    /// </summary>
    StatusRestartBootApplication = 0xC0000453,

    /// <summary>
    ///     The STATUS_INSUFFICIENT_NVRAM_RESOURCES constant.
    /// </summary>
    StatusInsufficientNvramResources = 0xC0000454,

    /// <summary>
    ///     The STATUS_NO_RANGES_PROCESSED constant.
    /// </summary>
    StatusNoRangesProcessed = 0xC0000460,

    /// <summary>
    ///     The STATUS_DEVICE_FEATURE_NOT_SUPPORTED constant.
    /// </summary>
    StatusDeviceFeatureNotSupported = 0xC0000463,

    /// <summary>
    ///     The STATUS_DEVICE_UNREACHABLE constant.
    /// </summary>
    StatusDeviceUnreachable = 0xC0000464,

    /// <summary>
    ///     The STATUS_INVALID_TOKEN constant.
    /// </summary>
    StatusInvalidToken = 0xC0000465,

    /// <summary>
    ///     The STATUS_SERVER_UNAVAILABLE constant.
    /// </summary>
    StatusServerUnavailable = 0xC0000466,

    /// <summary>
    ///     The STATUS_INVALID_TASK_NAME constant.
    /// </summary>
    StatusInvalidTaskName = 0xC0000500,

    /// <summary>
    ///     The STATUS_INVALID_TASK_INDEX constant.
    /// </summary>
    StatusInvalidTaskIndex = 0xC0000501,

    /// <summary>
    ///     The STATUS_THREAD_ALREADY_IN_TASK constant.
    /// </summary>
    StatusThreadAlreadyInTask = 0xC0000502,

    /// <summary>
    ///     The STATUS_CALLBACK_BYPASS constant.
    /// </summary>
    StatusCallbackBypass = 0xC0000503,

    /// <summary>
    ///     The STATUS_FAIL_FAST_EXCEPTION constant.
    /// </summary>
    StatusFailFastException = 0xC0000602,

    /// <summary>
    ///     The STATUS_IMAGE_CERT_REVOKED constant.
    /// </summary>
    StatusImageCertRevoked = 0xC0000603,

    /// <summary>
    ///     The STATUS_PORT_CLOSED constant.
    /// </summary>
    StatusPortClosed = 0xC0000700,

    /// <summary>
    ///     The STATUS_MESSAGE_LOST constant.
    /// </summary>
    StatusMessageLost = 0xC0000701,

    /// <summary>
    ///     The STATUS_INVALID_MESSAGE constant.
    /// </summary>
    StatusInvalidMessage = 0xC0000702,

    /// <summary>
    ///     The STATUS_REQUEST_CANCELED constant.
    /// </summary>
    StatusRequestCanceled = 0xC0000703,

    /// <summary>
    ///     The STATUS_RECURSIVE_DISPATCH constant.
    /// </summary>
    StatusRecursiveDispatch = 0xC0000704,

    /// <summary>
    ///     The STATUS_LPC_RECEIVE_BUFFER_EXPECTED constant.
    /// </summary>
    StatusLpcReceiveBufferExpected = 0xC0000705,

    /// <summary>
    ///     The STATUS_LPC_INVALID_CONNECTION_USAGE constant.
    /// </summary>
    StatusLpcInvalidConnectionUsage = 0xC0000706,

    /// <summary>
    ///     The STATUS_LPC_REQUESTS_NOT_ALLOWED constant.
    /// </summary>
    StatusLpcRequestsNotAllowed = 0xC0000707,

    /// <summary>
    ///     The STATUS_RESOURCE_IN_USE constant.
    /// </summary>
    StatusResourceInUse = 0xC0000708,

    /// <summary>
    ///     The STATUS_HARDWARE_MEMORY_ERROR constant.
    /// </summary>
    StatusHardwareMemoryError = 0xC0000709,

    /// <summary>
    ///     The STATUS_THREADPOOL_HANDLE_EXCEPTION constant.
    /// </summary>
    StatusThreadPoolHandleException = 0xC000070A,

    /// <summary>
    ///     The STATUS_THREADPOOL_SET_EVENT_ON_COMPLETION_FAILED constant.
    /// </summary>
    StatusThreadPoolSetEventOnCompletionFailed = 0xC000070B,

    /// <summary>
    ///     The STATUS_THREADPOOL_RELEASE_SEMAPHORE_ON_COMPLETION_FAILED constant.
    /// </summary>
    StatusThreadPoolReleaseSemaphoreOnCompletionFailed = 0xC000070C,

    /// <summary>
    ///     The STATUS_THREADPOOL_RELEASE_MUTEX_ON_COMPLETION_FAILED constant.
    /// </summary>
    StatusThreadPoolReleaseMutexOnCompletionFailed = 0xC000070D,

    /// <summary>
    ///     The STATUS_THREADPOOL_FREE_LIBRARY_ON_COMPLETION_FAILED constant.
    /// </summary>
    StatusThreadPoolFreeLibraryOnCompletionFailed = 0xC000070E,

    /// <summary>
    ///     The STATUS_THREADPOOL_RELEASED_DURING_OPERATION constant.
    /// </summary>
    StatusThreadPoolReleasedDuringOperation = 0xC000070F,

    /// <summary>
    ///     The STATUS_CALLBACK_RETURNED_WHILE_IMPERSONATING constant.
    /// </summary>
    StatusCallbackReturnedWhileImpersonating = 0xC0000710,

    /// <summary>
    ///     The STATUS_APC_RETURNED_WHILE_IMPERSONATING constant.
    /// </summary>
    StatusApcReturnedWhileImpersonating = 0xC0000711,

    /// <summary>
    ///     The STATUS_PROCESS_IS_PROTECTED constant.
    /// </summary>
    StatusProcessIsProtected = 0xC0000712,

    /// <summary>
    ///     The STATUS_MCA_EXCEPTION constant.
    /// </summary>
    StatusMcaException = 0xC0000713,

    /// <summary>
    ///     The STATUS_CERTIFICATE_MAPPING_NOT_UNIQUE constant.
    /// </summary>
    StatusCertificateMappingNotUnique = 0xC0000714,

    /// <summary>
    ///     The STATUS_SYMLINK_CLASS_DISABLED constant.
    /// </summary>
    StatusSymLinkClassDisabled = 0xC0000715,

    /// <summary>
    ///     The STATUS_INVALID_IDN_NORMALIZATION constant.
    /// </summary>
    StatusInvalidIdnNormalization = 0xC0000716,

    /// <summary>
    ///     The STATUS_NO_UNICODE_TRANSLATION constant.
    /// </summary>
    StatusNoUnicodeTranslation = 0xC0000717,

    /// <summary>
    ///     The STATUS_ALREADY_REGISTERED constant.
    /// </summary>
    StatusAlreadyRegistered = 0xC0000718,

    /// <summary>
    ///     The STATUS_CONTEXT_MISMATCH constant.
    /// </summary>
    StatusContextMismatch = 0xC0000719,

    /// <summary>
    ///     The STATUS_PORT_ALREADY_HAS_COMPLETION_LIST constant.
    /// </summary>
    StatusPortAlreadyHasCompletionList = 0xC000071A,

    /// <summary>
    ///     The STATUS_CALLBACK_RETURNED_THREAD_PRIORITY constant.
    /// </summary>
    StatusCallbackReturnedThreadPriority = 0xC000071B,

    /// <summary>
    ///     The STATUS_INVALID_THREAD constant.
    /// </summary>
    StatusInvalidThread = 0xC000071C,

    /// <summary>
    ///     The STATUS_CALLBACK_RETURNED_TRANSACTION constant.
    /// </summary>
    StatusCallbackReturnedTransaction = 0xC000071D,

    /// <summary>
    ///     The STATUS_CALLBACK_RETURNED_LDR_LOCK constant.
    /// </summary>
    StatusCallbackReturnedLdrLock = 0xC000071E,

    /// <summary>
    ///     The STATUS_CALLBACK_RETURNED_LANG constant.
    /// </summary>
    StatusCallbackReturnedLang = 0xC000071F,

    /// <summary>
    ///     The STATUS_CALLBACK_RETURNED_PRI_BACK constant.
    /// </summary>
    StatusCallbackReturnedPriBack = 0xC0000720,

    /// <summary>
    ///     The STATUS_DISK_REPAIR_DISABLED constant.
    /// </summary>
    StatusDiskRepairDisabled = 0xC0000800,

    /// <summary>
    ///     The STATUS_DS_DOMAIN_RENAME_IN_PROGRESS constant.
    /// </summary>
    StatusDsDomainRenameInProgress = 0xC0000801,

    /// <summary>
    ///     The STATUS_DISK_QUOTA_EXCEEDED constant.
    /// </summary>
    StatusDiskQuotaExceeded = 0xC0000802,

    /// <summary>
    ///     The STATUS_CONTENT_BLOCKED constant.
    /// </summary>
    StatusContentBlocked = 0xC0000804,

    /// <summary>
    ///     The STATUS_BAD_CLUSTERS constant.
    /// </summary>
    StatusBadClusters = 0xC0000805,

    /// <summary>
    ///     The STATUS_VOLUME_DIRTY constant.
    /// </summary>
    StatusVolumeDirty = 0xC0000806,

    /// <summary>
    ///     The STATUS_FILE_CHECKED_OUT constant.
    /// </summary>
    StatusFileCheckedOut = 0xC0000901,

    /// <summary>
    ///     The STATUS_CHECKOUT_REQUIRED constant.
    /// </summary>
    StatusCheckoutRequired = 0xC0000902,

    /// <summary>
    ///     The STATUS_BAD_FILE_TYPE constant.
    /// </summary>
    StatusBadFileType = 0xC0000903,

    /// <summary>
    ///     The STATUS_FILE_TOO_LARGE constant.
    /// </summary>
    StatusFileTooLarge = 0xC0000904,

    /// <summary>
    ///     The STATUS_FORMS_AUTH_REQUIRED constant.
    /// </summary>
    StatusFormsAuthRequired = 0xC0000905,

    /// <summary>
    ///     The STATUS_VIRUS_INFECTED constant.
    /// </summary>
    StatusVirusInfected = 0xC0000906,

    /// <summary>
    ///     The STATUS_VIRUS_DELETED constant.
    /// </summary>
    StatusVirusDeleted = 0xC0000907,

    /// <summary>
    ///     The STATUS_BAD_MCFG_TABLE constant.
    /// </summary>
    StatusBadMcfgTable = 0xC0000908,

    /// <summary>
    ///     The STATUS_CANNOT_BREAK_OPLOCK constant.
    /// </summary>
    StatusCannotBreakOplock = 0xC0000909,

    /// <summary>
    ///     The STATUS_WOW_ASSERTION constant.
    /// </summary>
    StatusWowAssertion = 0xC0009898,

    /// <summary>
    ///     The STATUS_INVALID_SIGNATURE constant.
    /// </summary>
    StatusInvalidSignature = 0xC000A000,

    /// <summary>
    ///     The STATUS_HMAC_NOT_SUPPORTED constant.
    /// </summary>
    StatusHmacNotSupported = 0xC000A001,

    /// <summary>
    ///     The STATUS_IPSEC_QUEUE_OVERFLOW constant.
    /// </summary>
    StatusIpSecQueueOverflow = 0xC000A010,

    /// <summary>
    ///     The STATUS_ND_QUEUE_OVERFLOW constant.
    /// </summary>
    StatusNdQueueOverflow = 0xC000A011,

    /// <summary>
    ///     The STATUS_HOPLIMIT_EXCEEDED constant.
    /// </summary>
    StatusHopLimitExceeded = 0xC000A012,

    /// <summary>
    ///     The STATUS_PROTOCOL_NOT_SUPPORTED constant.
    /// </summary>
    StatusProtocolNotSupported = 0xC000A013,

    /// <summary>
    ///     The STATUS_LOST_WRITEBEHIND_DATA_NETWORK_DISCONNECTED constant.
    /// </summary>
    StatusLostWriteBehindDataNetworkDisconnected = 0xC000A080,

    /// <summary>
    ///     The STATUS_LOST_WRITEBEHIND_DATA_NETWORK_SERVER_ERROR constant.
    /// </summary>
    StatusLostWriteBehindDataNetworkServerError = 0xC000A081,

    /// <summary>
    ///     The STATUS_LOST_WRITEBEHIND_DATA_LOCAL_DISK_ERROR constant.
    /// </summary>
    StatusLostWriteBehindDataLocalDiskError = 0xC000A082,

    /// <summary>
    ///     The STATUS_XML_PARSE_ERROR constant.
    /// </summary>
    StatusXmlParseError = 0xC000A083,

    /// <summary>
    ///     The STATUS_XMLDSIG_ERROR constant.
    /// </summary>
    StatusXmldsigError = 0xC000A084,

    /// <summary>
    ///     The STATUS_WRONG_COMPARTMENT constant.
    /// </summary>
    StatusWrongCompartment = 0xC000A085,

    /// <summary>
    ///     The STATUS_AUTHIP_FAILURE constant.
    /// </summary>
    StatusAuthIpFailure = 0xC000A086,

    /// <summary>
    ///     The STATUS_DS_OID_MAPPED_GROUP_CANT_HAVE_MEMBERS constant.
    /// </summary>
    StatusDsOidMappedGroupCantHaveMembers = 0xC000A087,

    /// <summary>
    ///     The STATUS_DS_OID_NOT_FOUND constant.
    /// </summary>
    StatusDsOidNotFound = 0xC000A088,

    /// <summary>
    ///     The STATUS_HASH_NOT_SUPPORTED constant.
    /// </summary>
    StatusHashNotSupported = 0xC000A100,

    /// <summary>
    ///     The STATUS_HASH_NOT_PRESENT constant.
    /// </summary>
    StatusHashNotPresent = 0xC000A101,

    /// <summary>
    ///     The STATUS_OFFLOAD_READ_FLT_NOT_SUPPORTED constant.
    /// </summary>
    StatusOffloadReadFltNotSupported = 0xC000A2A1,

    /// <summary>
    ///     The STATUS_OFFLOAD_WRITE_FLT_NOT_SUPPORTED constant.
    /// </summary>
    StatusOffloadWriteFltNotSupported = 0xC000A2A2,

    /// <summary>
    ///     The STATUS_OFFLOAD_READ_FILE_NOT_SUPPORTED constant.
    /// </summary>
    StatusOffloadReadFileNotSupported = 0xC000A2A3,

    /// <summary>
    ///     The STATUS_OFFLOAD_WRITE_FILE_NOT_SUPPORTED constant.
    /// </summary>
    StatusOffloadWriteFileNotSupported = 0xC000A2A4,

    /// <summary>
    ///     The DBG_NO_STATE_CHANGE constant.
    /// </summary>
    DbgNoStateChange = 0xC0010001,

    /// <summary>
    ///     The DBG_APP_NOT_IDLE constant.
    /// </summary>
    DbgAppNotIdle = 0xC0010002,

    /// <summary>
    ///     The RPC_NT_INVALID_STRING_BINDING constant.
    /// </summary>
    RpcNtInvalidStringBinding = 0xC0020001,

    /// <summary>
    ///     The RPC_NT_WRONG_KIND_OF_BINDING constant.
    /// </summary>
    RpcNtWrongKindOfBinding = 0xC0020002,

    /// <summary>
    ///     The RPC_NT_INVALID_BINDING constant.
    /// </summary>
    RpcNtInvalidBinding = 0xC0020003,

    /// <summary>
    ///     The RPC_NT_PROTSEQ_NOT_SUPPORTED constant.
    /// </summary>
    RpcNtProtseqNotSupported = 0xC0020004,

    /// <summary>
    ///     The RPC_NT_INVALID_RPC_PROTSEQ constant.
    /// </summary>
    RpcNtInvalidRpcProtseq = 0xC0020005,

    /// <summary>
    ///     The RPC_NT_INVALID_STRING_UUID constant.
    /// </summary>
    RpcNtInvalidStringUuid = 0xC0020006,

    /// <summary>
    ///     The RPC_NT_INVALID_ENDPOINT_FORMAT constant.
    /// </summary>
    RpcNtInvalidEndpointFormat = 0xC0020007,

    /// <summary>
    ///     The RPC_NT_INVALID_NET_ADDR constant.
    /// </summary>
    RpcNtInvalidNetAddr = 0xC0020008,

    /// <summary>
    ///     The RPC_NT_NO_ENDPOINT_FOUND constant.
    /// </summary>
    RpcNtNoEndpointFound = 0xC0020009,

    /// <summary>
    ///     The RPC_NT_INVALID_TIMEOUT constant.
    /// </summary>
    RpcNtInvalidTimeout = 0xC002000A,

    /// <summary>
    ///     The RPC_NT_OBJECT_NOT_FOUND constant.
    /// </summary>
    RpcNtObjectNotFound = 0xC002000B,

    /// <summary>
    ///     The RPC_NT_ALREADY_REGISTERED constant.
    /// </summary>
    RpcNtAlreadyRegistered = 0xC002000C,

    /// <summary>
    ///     The RPC_NT_TYPE_ALREADY_REGISTERED constant.
    /// </summary>
    RpcNtTypeAlreadyRegistered = 0xC002000D,

    /// <summary>
    ///     The RPC_NT_ALREADY_LISTENING constant.
    /// </summary>
    RpcNtAlreadyListening = 0xC002000E,

    /// <summary>
    ///     The RPC_NT_NO_PROTSEQS_REGISTERED constant.
    /// </summary>
    RpcNtNoProtseqsRegistered = 0xC002000F,

    /// <summary>
    ///     The RPC_NT_NOT_LISTENING constant.
    /// </summary>
    RpcNtNotListening = 0xC0020010,

    /// <summary>
    ///     The RPC_NT_UNKNOWN_MGR_TYPE constant.
    /// </summary>
    RpcNtUnknownMgrType = 0xC0020011,

    /// <summary>
    ///     The RPC_NT_UNKNOWN_IF constant.
    /// </summary>
    RpcNtUnknownIf = 0xC0020012,

    /// <summary>
    ///     The RPC_NT_NO_BINDINGS constant.
    /// </summary>
    RpcNtNoBindings = 0xC0020013,

    /// <summary>
    ///     The RPC_NT_NO_PROTSEQS constant.
    /// </summary>
    RpcNtNoProtseqs = 0xC0020014,

    /// <summary>
    ///     The RPC_NT_CANT_CREATE_ENDPOINT constant.
    /// </summary>
    RpcNtCantCreateEndpoint = 0xC0020015,

    /// <summary>
    ///     The RPC_NT_OUT_OF_RESOURCES constant.
    /// </summary>
    RpcNtOutOfResources = 0xC0020016,

    /// <summary>
    ///     The RPC_NT_SERVER_UNAVAILABLE constant.
    /// </summary>
    RpcNtServerUnavailable = 0xC0020017,

    /// <summary>
    ///     The RPC_NT_SERVER_TOO_BUSY constant.
    /// </summary>
    RpcNtServerTooBusy = 0xC0020018,

    /// <summary>
    ///     The RPC_NT_INVALID_NETWORK_OPTIONS constant.
    /// </summary>
    RpcNtInvalidNetworkOptions = 0xC0020019,

    /// <summary>
    ///     The RPC_NT_NO_CALL_ACTIVE constant.
    /// </summary>
    RpcNtNoCallActive = 0xC002001A,

    /// <summary>
    ///     The RPC_NT_CALL_FAILED constant.
    /// </summary>
    RpcNtCallFailed = 0xC002001B,

    /// <summary>
    ///     The RPC_NT_CALL_FAILED_DNE constant.
    /// </summary>
    RpcNtCallFailedDne = 0xC002001C,

    /// <summary>
    ///     The RPC_NT_PROTOCOL_ERROR constant.
    /// </summary>
    RpcNtProtocolError = 0xC002001D,

    /// <summary>
    ///     The RPC_NT_UNSUPPORTED_TRANS_SYN constant.
    /// </summary>
    RpcNtUnsupportedTransSyn = 0xC002001F,

    /// <summary>
    ///     The RPC_NT_UNSUPPORTED_TYPE constant.
    /// </summary>
    RpcNtUnsupportedType = 0xC0020021,

    /// <summary>
    ///     The RPC_NT_INVALID_TAG constant.
    /// </summary>
    RpcNtInvalidTag = 0xC0020022,

    /// <summary>
    ///     The RPC_NT_INVALID_BOUND constant.
    /// </summary>
    RpcNtInvalidBound = 0xC0020023,

    /// <summary>
    ///     The RPC_NT_NO_ENTRY_NAME constant.
    /// </summary>
    RpcNtNoEntryName = 0xC0020024,

    /// <summary>
    ///     The RPC_NT_INVALID_NAME_SYNTAX constant.
    /// </summary>
    RpcNtInvalidNameSyntax = 0xC0020025,

    /// <summary>
    ///     The RPC_NT_UNSUPPORTED_NAME_SYNTAX constant.
    /// </summary>
    RpcNtUnsupportedNameSyntax = 0xC0020026,

    /// <summary>
    ///     The RPC_NT_UUID_NO_ADDRESS constant.
    /// </summary>
    RpcNtUuidNoAddress = 0xC0020028,

    /// <summary>
    ///     The RPC_NT_DUPLICATE_ENDPOINT constant.
    /// </summary>
    RpcNtDuplicateEndpoint = 0xC0020029,

    /// <summary>
    ///     The RPC_NT_UNKNOWN_AUTHN_TYPE constant.
    /// </summary>
    RpcNtUnknownAuthNType = 0xC002002A,

    /// <summary>
    ///     The RPC_NT_MAX_CALLS_TOO_SMALL constant.
    /// </summary>
    RpcNtMaxCallsTooSmall = 0xC002002B,

    /// <summary>
    ///     The RPC_NT_STRING_TOO_LONG constant.
    /// </summary>
    RpcNtStringTooLong = 0xC002002C,

    /// <summary>
    ///     The RPC_NT_PROTSEQ_NOT_FOUND constant.
    /// </summary>
    RpcNtProtseqNotFound = 0xC002002D,

    /// <summary>
    ///     The RPC_NT_PROCNUM_OUT_OF_RANGE constant.
    /// </summary>
    RpcNtProcNumOutOfRange = 0xC002002E,

    /// <summary>
    ///     The RPC_NT_BINDING_HAS_NO_AUTH constant.
    /// </summary>
    RpcNtBindingHasNoAuth = 0xC002002F,

    /// <summary>
    ///     The RPC_NT_UNKNOWN_AUTHN_SERVICE constant.
    /// </summary>
    RpcNtUnknownAuthNService = 0xC0020030,

    /// <summary>
    ///     The RPC_NT_UNKNOWN_AUTHN_LEVEL constant.
    /// </summary>
    RpcNtUnknownAuthNLevel = 0xC0020031,

    /// <summary>
    ///     The RPC_NT_INVALID_AUTH_IDENTITY constant.
    /// </summary>
    RpcNtInvalidAuthIdentity = 0xC0020032,

    /// <summary>
    ///     The RPC_NT_UNKNOWN_AUTHZ_SERVICE constant.
    /// </summary>
    RpcNtUnknownAuthZService = 0xC0020033,

    /// <summary>
    ///     The EPT_NT_INVALID_ENTRY constant.
    /// </summary>
    EptNtInvalidEntry = 0xC0020034,

    /// <summary>
    ///     The EPT_NT_CANT_PERFORM_OP constant.
    /// </summary>
    EptNtCantPerformOp = 0xC0020035,

    /// <summary>
    ///     The EPT_NT_NOT_REGISTERED constant.
    /// </summary>
    EptNtNotRegistered = 0xC0020036,

    /// <summary>
    ///     The RPC_NT_NOTHING_TO_EXPORT constant.
    /// </summary>
    RpcNtNothingToExport = 0xC0020037,

    /// <summary>
    ///     The RPC_NT_INCOMPLETE_NAME constant.
    /// </summary>
    RpcNtIncompleteName = 0xC0020038,

    /// <summary>
    ///     The RPC_NT_INVALID_VERS_OPTION constant.
    /// </summary>
    RpcNtInvalidVersOption = 0xC0020039,

    /// <summary>
    ///     The RPC_NT_NO_MORE_MEMBERS constant.
    /// </summary>
    RpcNtNoMoreMembers = 0xC002003A,

    /// <summary>
    ///     The RPC_NT_NOT_ALL_OBJS_UNEXPORTED constant.
    /// </summary>
    RpcNtNotAllObjsUnexported = 0xC002003B,

    /// <summary>
    ///     The RPC_NT_INTERFACE_NOT_FOUND constant.
    /// </summary>
    RpcNtInterfaceNotFound = 0xC002003C,

    /// <summary>
    ///     The RPC_NT_ENTRY_ALREADY_EXISTS constant.
    /// </summary>
    RpcNtEntryAlreadyExists = 0xC002003D,

    /// <summary>
    ///     The RPC_NT_ENTRY_NOT_FOUND constant.
    /// </summary>
    RpcNtEntryNotFound = 0xC002003E,

    /// <summary>
    ///     The RPC_NT_NAME_SERVICE_UNAVAILABLE constant.
    /// </summary>
    RpcNtNameServiceUnavailable = 0xC002003F,

    /// <summary>
    ///     The RPC_NT_INVALID_NAF_ID constant.
    /// </summary>
    RpcNtInvalidNafId = 0xC0020040,

    /// <summary>
    ///     The RPC_NT_CANNOT_SUPPORT constant.
    /// </summary>
    RpcNtCannotSupport = 0xC0020041,

    /// <summary>
    ///     The RPC_NT_NO_CONTEXT_AVAILABLE constant.
    /// </summary>
    RpcNtNoContextAvailable = 0xC0020042,

    /// <summary>
    ///     The RPC_NT_INTERNAL_ERROR constant.
    /// </summary>
    RpcNtInternalError = 0xC0020043,

    /// <summary>
    ///     The RPC_NT_ZERO_DIVIDE constant.
    /// </summary>
    RpcNtZeroDivide = 0xC0020044,

    /// <summary>
    ///     The RPC_NT_ADDRESS_ERROR constant.
    /// </summary>
    RpcNtAddressError = 0xC0020045,

    /// <summary>
    ///     The RPC_NT_FP_DIV_ZERO constant.
    /// </summary>
    RpcNtFpDivZero = 0xC0020046,

    /// <summary>
    ///     The RPC_NT_FP_UNDERFLOW constant.
    /// </summary>
    RpcNtFpUnderflow = 0xC0020047,

    /// <summary>
    ///     The RPC_NT_FP_OVERFLOW constant.
    /// </summary>
    RpcNtFpOverflow = 0xC0020048,

    /// <summary>
    ///     The RPC_NT_CALL_IN_PROGRESS constant.
    /// </summary>
    RpcNtCallInProgress = 0xC0020049,

    /// <summary>
    ///     The RPC_NT_NO_MORE_BINDINGS constant.
    /// </summary>
    RpcNtNoMoreBindings = 0xC002004A,

    /// <summary>
    ///     The RPC_NT_GROUP_MEMBER_NOT_FOUND constant.
    /// </summary>
    RpcNtGroupMemberNotFound = 0xC002004B,

    /// <summary>
    ///     The EPT_NT_CANT_CREATE constant.
    /// </summary>
    EptNtCantCreate = 0xC002004C,

    /// <summary>
    ///     The RPC_NT_INVALID_OBJECT constant.
    /// </summary>
    RpcNtInvalidObject = 0xC002004D,

    /// <summary>
    ///     The RPC_NT_NO_INTERFACES constant.
    /// </summary>
    RpcNtNoInterfaces = 0xC002004F,

    /// <summary>
    ///     The RPC_NT_CALL_CANCELLED constant.
    /// </summary>
    RpcNtCallCancelled = 0xC0020050,

    /// <summary>
    ///     The RPC_NT_BINDING_INCOMPLETE constant.
    /// </summary>
    RpcNtBindingIncomplete = 0xC0020051,

    /// <summary>
    ///     The RPC_NT_COMM_FAILURE constant.
    /// </summary>
    RpcNtCommFailure = 0xC0020052,

    /// <summary>
    ///     The RPC_NT_UNSUPPORTED_AUTHN_LEVEL constant.
    /// </summary>
    RpcNtUnsupportedAuthNLevel = 0xC0020053,

    /// <summary>
    ///     The RPC_NT_NO_PRINC_NAME constant.
    /// </summary>
    RpcNtNoPrincName = 0xC0020054,

    /// <summary>
    ///     The RPC_NT_NOT_RPC_ERROR constant.
    /// </summary>
    RpcNtNotRpcError = 0xC0020055,

    /// <summary>
    ///     The RPC_NT_SEC_PKG_ERROR constant.
    /// </summary>
    RpcNtSecPkgError = 0xC0020057,

    /// <summary>
    ///     The RPC_NT_NOT_CANCELLED constant.
    /// </summary>
    RpcNtNotCancelled = 0xC0020058,

    /// <summary>
    ///     The RPC_NT_INVALID_ASYNC_HANDLE constant.
    /// </summary>
    RpcNtInvalidAsyncHandle = 0xC0020062,

    /// <summary>
    ///     The RPC_NT_INVALID_ASYNC_CALL constant.
    /// </summary>
    RpcNtInvalidAsyncCall = 0xC0020063,

    /// <summary>
    ///     The RPC_NT_PROXY_ACCESS_DENIED constant.
    /// </summary>
    RpcNtProxyAccessDenied = 0xC0020064,

    /// <summary>
    ///     The RPC_NT_NO_MORE_ENTRIES constant.
    /// </summary>
    RpcNtNoMoreEntries = 0xC0030001,

    /// <summary>
    ///     The RPC_NT_SS_CHAR_TRANS_OPEN_FAIL constant.
    /// </summary>
    RpcNtSsCharTransOpenFail = 0xC0030002,

    /// <summary>
    ///     The RPC_NT_SS_CHAR_TRANS_SHORT_FILE constant.
    /// </summary>
    RpcNtSsCharTransShortFile = 0xC0030003,

    /// <summary>
    ///     The RPC_NT_SS_IN_NULL_CONTEXT constant.
    /// </summary>
    RpcNtSsInNullContext = 0xC0030004,

    /// <summary>
    ///     The RPC_NT_SS_CONTEXT_MISMATCH constant.
    /// </summary>
    RpcNtSsContextMismatch = 0xC0030005,

    /// <summary>
    ///     The RPC_NT_SS_CONTEXT_DAMAGED constant.
    /// </summary>
    RpcNtSsContextDamaged = 0xC0030006,

    /// <summary>
    ///     The RPC_NT_SS_HANDLES_MISMATCH constant.
    /// </summary>
    RpcNtSsHandlesMismatch = 0xC0030007,

    /// <summary>
    ///     The RPC_NT_SS_CANNOT_GET_CALL_HANDLE constant.
    /// </summary>
    RpcNtSsCannotGetCallHandle = 0xC0030008,

    /// <summary>
    ///     The RPC_NT_NULL_REF_POINTER constant.
    /// </summary>
    RpcNtNullRefPointer = 0xC0030009,

    /// <summary>
    ///     The RPC_NT_ENUM_VALUE_OUT_OF_RANGE constant.
    /// </summary>
    RpcNtEnumValueOutOfRange = 0xC003000A,

    /// <summary>
    ///     The RPC_NT_BYTE_COUNT_TOO_SMALL constant.
    /// </summary>
    RpcNtByteCountTooSmall = 0xC003000B,

    /// <summary>
    ///     The RPC_NT_BAD_STUB_DATA constant.
    /// </summary>
    RpcNtBadStubData = 0xC003000C,

    /// <summary>
    ///     The RPC_NT_INVALID_ES_ACTION constant.
    /// </summary>
    RpcNtInvalidEsAction = 0xC0030059,

    /// <summary>
    ///     The RPC_NT_WRONG_ES_VERSION constant.
    /// </summary>
    RpcNtWrongEsVersion = 0xC003005A,

    /// <summary>
    ///     The RPC_NT_WRONG_STUB_VERSION constant.
    /// </summary>
    RpcNtWrongStubVersion = 0xC003005B,

    /// <summary>
    ///     The RPC_NT_INVALID_PIPE_OBJECT constant.
    /// </summary>
    RpcNtInvalidPipeObject = 0xC003005C,

    /// <summary>
    ///     The RPC_NT_INVALID_PIPE_OPERATION constant.
    /// </summary>
    RpcNtInvalidPipeOperation = 0xC003005D,

    /// <summary>
    ///     The RPC_NT_WRONG_PIPE_VERSION constant.
    /// </summary>
    RpcNtWrongPipeVersion = 0xC003005E,

    /// <summary>
    ///     The RPC_NT_PIPE_CLOSED constant.
    /// </summary>
    RpcNtPipeClosed = 0xC003005F,

    /// <summary>
    ///     The RPC_NT_PIPE_DISCIPLINE_ERROR constant.
    /// </summary>
    RpcNtPipeDisciplineError = 0xC0030060,

    /// <summary>
    ///     The RPC_NT_PIPE_EMPTY constant.
    /// </summary>
    RpcNtPipeEmpty = 0xC0030061,

    /// <summary>
    ///     The STATUS_PNP_BAD_MPS_TABLE constant.
    /// </summary>
    StatusPnpBadMpsTable = 0xC0040035,

    /// <summary>
    ///     The STATUS_PNP_TRANSLATION_FAILED constant.
    /// </summary>
    StatusPnpTranslationFailed = 0xC0040036,

    /// <summary>
    ///     The STATUS_PNP_IRQ_TRANSLATION_FAILED constant.
    /// </summary>
    StatusPnpIrqTranslationFailed = 0xC0040037,

    /// <summary>
    ///     The STATUS_PNP_INVALID_ID constant.
    /// </summary>
    StatusPnpInvalidId = 0xC0040038,

    /// <summary>
    ///     The STATUS_IO_REISSUE_AS_CACHED constant.
    /// </summary>
    StatusIoReissueAsCached = 0xC0040039,

    /// <summary>
    ///     The STATUS_CTX_WINSTATION_NAME_INVALID constant.
    /// </summary>
    StatusCtxWinStationNameInvalid = 0xC00A0001,

    /// <summary>
    ///     The STATUS_CTX_INVALID_PD constant.
    /// </summary>
    StatusCtxInvalidPd = 0xC00A0002,

    /// <summary>
    ///     The STATUS_CTX_PD_NOT_FOUND constant.
    /// </summary>
    StatusCtxPdNotFound = 0xC00A0003,

    /// <summary>
    ///     The STATUS_CTX_CLOSE_PENDING constant.
    /// </summary>
    StatusCtxClosePending = 0xC00A0006,

    /// <summary>
    ///     The STATUS_CTX_NO_OUTBUF constant.
    /// </summary>
    StatusCtxNoOutBuf = 0xC00A0007,

    /// <summary>
    ///     The STATUS_CTX_MODEM_INF_NOT_FOUND constant.
    /// </summary>
    StatusCtxModemInfNotFound = 0xC00A0008,

    /// <summary>
    ///     The STATUS_CTX_INVALID_MODEMNAME constant.
    /// </summary>
    StatusCtxInvalidModemName = 0xC00A0009,

    /// <summary>
    ///     The STATUS_CTX_RESPONSE_ERROR constant.
    /// </summary>
    StatusCtxResponseError = 0xC00A000A,

    /// <summary>
    ///     The STATUS_CTX_MODEM_RESPONSE_TIMEOUT constant.
    /// </summary>
    StatusCtxModemResponseTimeout = 0xC00A000B,

    /// <summary>
    ///     The STATUS_CTX_MODEM_RESPONSE_NO_CARRIER constant.
    /// </summary>
    StatusCtxModemResponseNoCarrier = 0xC00A000C,

    /// <summary>
    ///     The STATUS_CTX_MODEM_RESPONSE_NO_DIALTONE constant.
    /// </summary>
    StatusCtxModemResponseNoDialTone = 0xC00A000D,

    /// <summary>
    ///     The STATUS_CTX_MODEM_RESPONSE_BUSY constant.
    /// </summary>
    StatusCtxModemResponseBusy = 0xC00A000E,

    /// <summary>
    ///     The STATUS_CTX_MODEM_RESPONSE_VOICE constant.
    /// </summary>
    StatusCtxModemResponseVoice = 0xC00A000F,

    /// <summary>
    ///     The STATUS_CTX_TD_ERROR constant.
    /// </summary>
    StatusCtxTdError = 0xC00A0010,

    /// <summary>
    ///     The STATUS_CTX_LICENSE_CLIENT_INVALID constant.
    /// </summary>
    StatusCtxLicenseClientInvalid = 0xC00A0012,

    /// <summary>
    ///     The STATUS_CTX_LICENSE_NOT_AVAILABLE constant.
    /// </summary>
    StatusCtxLicenseNotAvailable = 0xC00A0013,

    /// <summary>
    ///     The STATUS_CTX_LICENSE_EXPIRED constant.
    /// </summary>
    StatusCtxLicenseExpired = 0xC00A0014,

    /// <summary>
    ///     The STATUS_CTX_WINSTATION_NOT_FOUND constant.
    /// </summary>
    StatusCtxWinStationNotFound = 0xC00A0015,

    /// <summary>
    ///     The STATUS_CTX_WINSTATION_NAME_COLLISION constant.
    /// </summary>
    StatusCtxWinStationNameCollision = 0xC00A0016,

    /// <summary>
    ///     The STATUS_CTX_WINSTATION_BUSY constant.
    /// </summary>
    StatusCtxWinStationBusy = 0xC00A0017,

    /// <summary>
    ///     The STATUS_CTX_BAD_VIDEO_MODE constant.
    /// </summary>
    StatusCtxBadVideoMode = 0xC00A0018,

    /// <summary>
    ///     The STATUS_CTX_GRAPHICS_INVALID constant.
    /// </summary>
    StatusCtxGraphicsInvalid = 0xC00A0022,

    /// <summary>
    ///     The STATUS_CTX_NOT_CONSOLE constant.
    /// </summary>
    StatusCtxNotConsole = 0xC00A0024,

    /// <summary>
    ///     The STATUS_CTX_CLIENT_QUERY_TIMEOUT constant.
    /// </summary>
    StatusCtxClientQueryTimeout = 0xC00A0026,

    /// <summary>
    ///     The STATUS_CTX_CONSOLE_DISCONNECT constant.
    /// </summary>
    StatusCtxConsoleDisconnect = 0xC00A0027,

    /// <summary>
    ///     The STATUS_CTX_CONSOLE_CONNECT constant.
    /// </summary>
    StatusCtxConsoleConnect = 0xC00A0028,

    /// <summary>
    ///     The STATUS_CTX_SHADOW_DENIED constant.
    /// </summary>
    StatusCtxShadowDenied = 0xC00A002A,

    /// <summary>
    ///     The STATUS_CTX_WINSTATION_ACCESS_DENIED constant.
    /// </summary>
    StatusCtxWinStationAccessDenied = 0xC00A002B,

    /// <summary>
    ///     The STATUS_CTX_INVALID_WD constant.
    /// </summary>
    StatusCtxInvalidWd = 0xC00A002E,

    /// <summary>
    ///     The STATUS_CTX_WD_NOT_FOUND constant.
    /// </summary>
    StatusCtxWdNotFound = 0xC00A002F,

    /// <summary>
    ///     The STATUS_CTX_SHADOW_INVALID constant.
    /// </summary>
    StatusCtxShadowInvalid = 0xC00A0030,

    /// <summary>
    ///     The STATUS_CTX_SHADOW_DISABLED constant.
    /// </summary>
    StatusCtxShadowDisabled = 0xC00A0031,

    /// <summary>
    ///     The STATUS_RDP_PROTOCOL_ERROR constant.
    /// </summary>
    StatusRdpProtocolError = 0xC00A0032,

    /// <summary>
    ///     The STATUS_CTX_CLIENT_LICENSE_NOT_SET constant.
    /// </summary>
    StatusCtxClientLicenseNotSet = 0xC00A0033,

    /// <summary>
    ///     The STATUS_CTX_CLIENT_LICENSE_IN_USE constant.
    /// </summary>
    StatusCtxClientLicenseInUse = 0xC00A0034,

    /// <summary>
    ///     The STATUS_CTX_SHADOW_ENDED_BY_MODE_CHANGE constant.
    /// </summary>
    StatusCtxShadowEndedByModeChange = 0xC00A0035,

    /// <summary>
    ///     The STATUS_CTX_SHADOW_NOT_RUNNING constant.
    /// </summary>
    StatusCtxShadowNotRunning = 0xC00A0036,

    /// <summary>
    ///     The STATUS_CTX_LOGON_DISABLED constant.
    /// </summary>
    StatusCtxLogonDisabled = 0xC00A0037,

    /// <summary>
    ///     The STATUS_CTX_SECURITY_LAYER_ERROR constant.
    /// </summary>
    StatusCtxSecurityLayerError = 0xC00A0038,

    /// <summary>
    ///     The STATUS_TS_INCOMPATIBLE_SESSIONS constant.
    /// </summary>
    StatusTsIncompatibleSessions = 0xC00A0039,

    /// <summary>
    ///     The STATUS_MUI_FILE_NOT_FOUND constant.
    /// </summary>
    StatusMuiFileNotFound = 0xC00B0001,

    /// <summary>
    ///     The STATUS_MUI_INVALID_FILE constant.
    /// </summary>
    StatusMuiInvalidFile = 0xC00B0002,

    /// <summary>
    ///     The STATUS_MUI_INVALID_RC_CONFIG constant.
    /// </summary>
    StatusMuiInvalidRcConfig = 0xC00B0003,

    /// <summary>
    ///     The STATUS_MUI_INVALID_LOCALE_NAME constant.
    /// </summary>
    StatusMuiInvalidLocaleName = 0xC00B0004,

    /// <summary>
    ///     The STATUS_MUI_INVALID_ULTIMATEFALLBACK_NAME constant.
    /// </summary>
    StatusMuiInvalidUltimateFallbackName = 0xC00B0005,

    /// <summary>
    ///     The STATUS_MUI_FILE_NOT_LOADED constant.
    /// </summary>
    StatusMuiFileNotLoaded = 0xC00B0006,

    /// <summary>
    ///     The STATUS_RESOURCE_ENUM_USER_STOP constant.
    /// </summary>
    StatusResourceEnumUserStop = 0xC00B0007,

    /// <summary>
    ///     The STATUS_CLUSTER_INVALID_NODE constant.
    /// </summary>
    StatusClusterInvalidNode = 0xC0130001,

    /// <summary>
    ///     The STATUS_CLUSTER_NODE_EXISTS constant.
    /// </summary>
    StatusClusterNodeExists = 0xC0130002,

    /// <summary>
    ///     The STATUS_CLUSTER_JOIN_IN_PROGRESS constant.
    /// </summary>
    StatusClusterJoinInProgress = 0xC0130003,

    /// <summary>
    ///     The STATUS_CLUSTER_NODE_NOT_FOUND constant.
    /// </summary>
    StatusClusterNodeNotFound = 0xC0130004,

    /// <summary>
    ///     The STATUS_CLUSTER_LOCAL_NODE_NOT_FOUND constant.
    /// </summary>
    StatusClusterLocalNodeNotFound = 0xC0130005,

    /// <summary>
    ///     The STATUS_CLUSTER_NETWORK_EXISTS constant.
    /// </summary>
    StatusClusterNetworkExists = 0xC0130006,

    /// <summary>
    ///     The STATUS_CLUSTER_NETWORK_NOT_FOUND constant.
    /// </summary>
    StatusClusterNetworkNotFound = 0xC0130007,

    /// <summary>
    ///     The STATUS_CLUSTER_NETINTERFACE_EXISTS constant.
    /// </summary>
    StatusClusterNetInterfaceExists = 0xC0130008,

    /// <summary>
    ///     The STATUS_CLUSTER_NETINTERFACE_NOT_FOUND constant.
    /// </summary>
    StatusClusterNetInterfaceNotFound = 0xC0130009,

    /// <summary>
    ///     The STATUS_CLUSTER_INVALID_REQUEST constant.
    /// </summary>
    StatusClusterInvalidRequest = 0xC013000A,

    /// <summary>
    ///     The STATUS_CLUSTER_INVALID_NETWORK_PROVIDER constant.
    /// </summary>
    StatusClusterInvalidNetworkProvider = 0xC013000B,

    /// <summary>
    ///     The STATUS_CLUSTER_NODE_DOWN constant.
    /// </summary>
    StatusClusterNodeDown = 0xC013000C,

    /// <summary>
    ///     The STATUS_CLUSTER_NODE_UNREACHABLE constant.
    /// </summary>
    StatusClusterNodeUnreachable = 0xC013000D,

    /// <summary>
    ///     The STATUS_CLUSTER_NODE_NOT_MEMBER constant.
    /// </summary>
    StatusClusterNodeNotMember = 0xC013000E,

    /// <summary>
    ///     The STATUS_CLUSTER_JOIN_NOT_IN_PROGRESS constant.
    /// </summary>
    StatusClusterJoinNotInProgress = 0xC013000F,

    /// <summary>
    ///     The STATUS_CLUSTER_INVALID_NETWORK constant.
    /// </summary>
    StatusClusterInvalidNetwork = 0xC0130010,

    /// <summary>
    ///     The STATUS_CLUSTER_NO_NET_ADAPTERS constant.
    /// </summary>
    StatusClusterNoNetAdapters = 0xC0130011,

    /// <summary>
    ///     The STATUS_CLUSTER_NODE_UP constant.
    /// </summary>
    StatusClusterNodeUp = 0xC0130012,

    /// <summary>
    ///     The STATUS_CLUSTER_NODE_PAUSED constant.
    /// </summary>
    StatusClusterNodePaused = 0xC0130013,

    /// <summary>
    ///     The STATUS_CLUSTER_NODE_NOT_PAUSED constant.
    /// </summary>
    StatusClusterNodeNotPaused = 0xC0130014,

    /// <summary>
    ///     The STATUS_CLUSTER_NO_SECURITY_CONTEXT constant.
    /// </summary>
    StatusClusterNoSecurityContext = 0xC0130015,

    /// <summary>
    ///     The STATUS_CLUSTER_NETWORK_NOT_INTERNAL constant.
    /// </summary>
    StatusClusterNetworkNotInternal = 0xC0130016,

    /// <summary>
    ///     The STATUS_CLUSTER_POISONED constant.
    /// </summary>
    StatusClusterPoisoned = 0xC0130017,

    /// <summary>
    ///     The STATUS_ACPI_INVALID_OPCODE constant.
    /// </summary>
    StatusAcpiInvalidOpcode = 0xC0140001,

    /// <summary>
    ///     The STATUS_ACPI_STACK_OVERFLOW constant.
    /// </summary>
    StatusAcpiStackOverflow = 0xC0140002,

    /// <summary>
    ///     The STATUS_ACPI_ASSERT_FAILED constant.
    /// </summary>
    StatusAcpiAssertFailed = 0xC0140003,

    /// <summary>
    ///     The STATUS_ACPI_INVALID_INDEX constant.
    /// </summary>
    StatusAcpiInvalidIndex = 0xC0140004,

    /// <summary>
    ///     The STATUS_ACPI_INVALID_ARGUMENT constant.
    /// </summary>
    StatusAcpiInvalidArgument = 0xC0140005,

    /// <summary>
    ///     The STATUS_ACPI_FATAL constant.
    /// </summary>
    StatusAcpiFatal = 0xC0140006,

    /// <summary>
    ///     The STATUS_ACPI_INVALID_SUPERNAME constant.
    /// </summary>
    StatusAcpiInvalidSuperName = 0xC0140007,

    /// <summary>
    ///     The STATUS_ACPI_INVALID_ARGTYPE constant.
    /// </summary>
    StatusAcpiInvalidArgType = 0xC0140008,

    /// <summary>
    ///     The STATUS_ACPI_INVALID_OBJTYPE constant.
    /// </summary>
    StatusAcpiInvalidObjType = 0xC0140009,

    /// <summary>
    ///     The STATUS_ACPI_INVALID_TARGETTYPE constant.
    /// </summary>
    StatusAcpiInvalidTargetType = 0xC014000A,

    /// <summary>
    ///     The STATUS_ACPI_INCORRECT_ARGUMENT_COUNT constant.
    /// </summary>
    StatusAcpiIncorrectArgumentCount = 0xC014000B,

    /// <summary>
    ///     The STATUS_ACPI_ADDRESS_NOT_MAPPED constant.
    /// </summary>
    StatusAcpiAddressNotMapped = 0xC014000C,

    /// <summary>
    ///     The STATUS_ACPI_INVALID_EVENTTYPE constant.
    /// </summary>
    StatusAcpiInvalidEventType = 0xC014000D,

    /// <summary>
    ///     The STATUS_ACPI_HANDLER_COLLISION constant.
    /// </summary>
    StatusAcpiHandlerCollision = 0xC014000E,

    /// <summary>
    ///     The STATUS_ACPI_INVALID_DATA constant.
    /// </summary>
    StatusAcpiInvalidData = 0xC014000F,

    /// <summary>
    ///     The STATUS_ACPI_INVALID_REGION constant.
    /// </summary>
    StatusAcpiInvalidRegion = 0xC0140010,

    /// <summary>
    ///     The STATUS_ACPI_INVALID_ACCESS_SIZE constant.
    /// </summary>
    StatusAcpiInvalidAccessSize = 0xC0140011,

    /// <summary>
    ///     The STATUS_ACPI_ACQUIRE_GLOBAL_LOCK constant.
    /// </summary>
    StatusAcpiAcquireGlobalLock = 0xC0140012,

    /// <summary>
    ///     The STATUS_ACPI_ALREADY_INITIALIZED constant.
    /// </summary>
    StatusAcpiAlreadyInitialized = 0xC0140013,

    /// <summary>
    ///     The STATUS_ACPI_NOT_INITIALIZED constant.
    /// </summary>
    StatusAcpiNotInitialized = 0xC0140014,

    /// <summary>
    ///     The STATUS_ACPI_INVALID_MUTEX_LEVEL constant.
    /// </summary>
    StatusAcpiInvalidMutexLevel = 0xC0140015,

    /// <summary>
    ///     The STATUS_ACPI_MUTEX_NOT_OWNED constant.
    /// </summary>
    StatusAcpiMutexNotOwned = 0xC0140016,

    /// <summary>
    ///     The STATUS_ACPI_MUTEX_NOT_OWNER constant.
    /// </summary>
    StatusAcpiMutexNotOwner = 0xC0140017,

    /// <summary>
    ///     The STATUS_ACPI_RS_ACCESS constant.
    /// </summary>
    StatusAcpiRsAccess = 0xC0140018,

    /// <summary>
    ///     The STATUS_ACPI_INVALID_TABLE constant.
    /// </summary>
    StatusAcpiInvalidTable = 0xC0140019,

    /// <summary>
    ///     The STATUS_ACPI_REG_HANDLER_FAILED constant.
    /// </summary>
    StatusAcpiRegHandlerFailed = 0xC0140020,

    /// <summary>
    ///     The STATUS_ACPI_POWER_REQUEST_FAILED constant.
    /// </summary>
    StatusAcpiPowerRequestFailed = 0xC0140021,

    /// <summary>
    ///     The STATUS_SXS_SECTION_NOT_FOUND constant.
    /// </summary>
    StatusSxsSectionNotFound = 0xC0150001,

    /// <summary>
    ///     The STATUS_SXS_CANT_GEN_ACTCTX constant.
    /// </summary>
    StatusSxsCantGenActCtx = 0xC0150002,

    /// <summary>
    ///     The STATUS_SXS_INVALID_ACTCTXDATA_FORMAT constant.
    /// </summary>
    StatusSxsInvalidActCtxDataFormat = 0xC0150003,

    /// <summary>
    ///     The STATUS_SXS_ASSEMBLY_NOT_FOUND constant.
    /// </summary>
    StatusSxsAssemblyNotFound = 0xC0150004,

    /// <summary>
    ///     The STATUS_SXS_MANIFEST_FORMAT_ERROR constant.
    /// </summary>
    StatusSxsManifestFormatError = 0xC0150005,

    /// <summary>
    ///     The STATUS_SXS_MANIFEST_PARSE_ERROR constant.
    /// </summary>
    StatusSxsManifestParseError = 0xC0150006,

    /// <summary>
    ///     The STATUS_SXS_ACTIVATION_CONTEXT_DISABLED constant.
    /// </summary>
    StatusSxsActivationContextDisabled = 0xC0150007,

    /// <summary>
    ///     The STATUS_SXS_KEY_NOT_FOUND constant.
    /// </summary>
    StatusSxsKeyNotFound = 0xC0150008,

    /// <summary>
    ///     The STATUS_SXS_VERSION_CONFLICT constant.
    /// </summary>
    StatusSxsVersionConflict = 0xC0150009,

    /// <summary>
    ///     The STATUS_SXS_WRONG_SECTION_TYPE constant.
    /// </summary>
    StatusSxsWrongSectionType = 0xC015000A,

    /// <summary>
    ///     The STATUS_SXS_THREAD_QUERIES_DISABLED constant.
    /// </summary>
    StatusSxsThreadQueriesDisabled = 0xC015000B,

    /// <summary>
    ///     The STATUS_SXS_ASSEMBLY_MISSING constant.
    /// </summary>
    StatusSxsAssemblyMissing = 0xC015000C,

    /// <summary>
    ///     The STATUS_SXS_PROCESS_DEFAULT_ALREADY_SET constant.
    /// </summary>
    StatusSxsProcessDefaultAlreadySet = 0xC015000E,

    /// <summary>
    ///     The STATUS_SXS_EARLY_DEACTIVATION constant.
    /// </summary>
    StatusSxsEarlyDeactivation = 0xC015000F,

    /// <summary>
    ///     The STATUS_SXS_INVALID_DEACTIVATION constant.
    /// </summary>
    StatusSxsInvalidDeactivation = 0xC0150010,

    /// <summary>
    ///     The STATUS_SXS_MULTIPLE_DEACTIVATION constant.
    /// </summary>
    StatusSxsMultipleDeactivation = 0xC0150011,

    /// <summary>
    ///     The STATUS_SXS_SYSTEM_DEFAULT_ACTIVATION_CONTEXT_EMPTY constant.
    /// </summary>
    StatusSxsSystemDefaultActivationContextEmpty = 0xC0150012,

    /// <summary>
    ///     The STATUS_SXS_PROCESS_TERMINATION_REQUESTED constant.
    /// </summary>
    StatusSxsProcessTerminationRequested = 0xC0150013,

    /// <summary>
    ///     The STATUS_SXS_CORRUPT_ACTIVATION_STACK constant.
    /// </summary>
    StatusSxsCorruptActivationStack = 0xC0150014,

    /// <summary>
    ///     The STATUS_SXS_CORRUPTION constant.
    /// </summary>
    StatusSxsCorruption = 0xC0150015,

    /// <summary>
    ///     The STATUS_SXS_INVALID_IDENTITY_ATTRIBUTE_VALUE constant.
    /// </summary>
    StatusSxsInvalidIdentityAttributeValue = 0xC0150016,

    /// <summary>
    ///     The STATUS_SXS_INVALID_IDENTITY_ATTRIBUTE_NAME constant.
    /// </summary>
    StatusSxsInvalidIdentityAttributeName = 0xC0150017,

    /// <summary>
    ///     The STATUS_SXS_IDENTITY_DUPLICATE_ATTRIBUTE constant.
    /// </summary>
    StatusSxsIdentityDuplicateAttribute = 0xC0150018,

    /// <summary>
    ///     The STATUS_SXS_IDENTITY_PARSE_ERROR constant.
    /// </summary>
    StatusSxsIdentityParseError = 0xC0150019,

    /// <summary>
    ///     The STATUS_SXS_COMPONENT_STORE_CORRUPT constant.
    /// </summary>
    StatusSxsComponentStoreCorrupt = 0xC015001A,

    /// <summary>
    ///     The STATUS_SXS_FILE_HASH_MISMATCH constant.
    /// </summary>
    StatusSxsFileHashMismatch = 0xC015001B,

    /// <summary>
    ///     The STATUS_SXS_MANIFEST_IDENTITY_SAME_BUT_CONTENTS_DIFFERENT constant.
    /// </summary>
    StatusSxsManifestIdentitySameButContentsDifferent = 0xC015001C,

    /// <summary>
    ///     The STATUS_SXS_IDENTITIES_DIFFERENT constant.
    /// </summary>
    StatusSxsIdentitiesDifferent = 0xC015001D,

    /// <summary>
    ///     The STATUS_SXS_ASSEMBLY_IS_NOT_A_DEPLOYMENT constant.
    /// </summary>
    StatusSxsAssemblyIsNotADeployment = 0xC015001E,

    /// <summary>
    ///     The STATUS_SXS_FILE_NOT_PART_OF_ASSEMBLY constant.
    /// </summary>
    StatusSxsFileNotPartOfAssembly = 0xC015001F,

    /// <summary>
    ///     The STATUS_ADVANCED_INSTALLER_FAILED constant.
    /// </summary>
    StatusAdvancedInstallerFailed = 0xC0150020,

    /// <summary>
    ///     The STATUS_XML_ENCODING_MISMATCH constant.
    /// </summary>
    StatusXmlEncodingMismatch = 0xC0150021,

    /// <summary>
    ///     The STATUS_SXS_MANIFEST_TOO_BIG constant.
    /// </summary>
    StatusSxsManifestTooBig = 0xC0150022,

    /// <summary>
    ///     The STATUS_SXS_SETTING_NOT_REGISTERED constant.
    /// </summary>
    StatusSxsSettingNotRegistered = 0xC0150023,

    /// <summary>
    ///     The STATUS_SXS_TRANSACTION_CLOSURE_INCOMPLETE constant.
    /// </summary>
    StatusSxsTransactionClosureIncomplete = 0xC0150024,

    /// <summary>
    ///     The STATUS_SMI_PRIMITIVE_INSTALLER_FAILED constant.
    /// </summary>
    StatusSmiPrimitiveInstallerFailed = 0xC0150025,

    /// <summary>
    ///     The STATUS_GENERIC_COMMAND_FAILED constant.
    /// </summary>
    StatusGenericCommandFailed = 0xC0150026,

    /// <summary>
    ///     The STATUS_SXS_FILE_HASH_MISSING constant.
    /// </summary>
    StatusSxsFileHashMissing = 0xC0150027,

    /// <summary>
    ///     The STATUS_TRANSACTIONAL_CONFLICT constant.
    /// </summary>
    StatusTransactionalConflict = 0xC0190001,

    /// <summary>
    ///     The STATUS_INVALID_TRANSACTION constant.
    /// </summary>
    StatusInvalidTransaction = 0xC0190002,

    /// <summary>
    ///     The STATUS_TRANSACTION_NOT_ACTIVE constant.
    /// </summary>
    StatusTransactionNotActive = 0xC0190003,

    /// <summary>
    ///     The STATUS_TM_INITIALIZATION_FAILED constant.
    /// </summary>
    StatusTmInitializationFailed = 0xC0190004,

    /// <summary>
    ///     The STATUS_RM_NOT_ACTIVE constant.
    /// </summary>
    StatusRmNotActive = 0xC0190005,

    /// <summary>
    ///     The STATUS_RM_METADATA_CORRUPT constant.
    /// </summary>
    StatusRmMetadataCorrupt = 0xC0190006,

    /// <summary>
    ///     The STATUS_TRANSACTION_NOT_JOINED constant.
    /// </summary>
    StatusTransactionNotJoined = 0xC0190007,

    /// <summary>
    ///     The STATUS_DIRECTORY_NOT_RM constant.
    /// </summary>
    StatusDirectoryNotRm = 0xC0190008,

    /// <summary>
    ///     The STATUS_TRANSACTIONS_UNSUPPORTED_REMOTE constant.
    /// </summary>
    StatusTransactionsUnsupportedRemote = 0xC019000A,

    /// <summary>
    ///     The STATUS_LOG_RESIZE_INVALID_SIZE constant.
    /// </summary>
    StatusLogResizeInvalidSize = 0xC019000B,

    /// <summary>
    ///     The STATUS_REMOTE_FILE_VERSION_MISMATCH constant.
    /// </summary>
    StatusRemoteFileVersionMismatch = 0xC019000C,

    /// <summary>
    ///     The STATUS_CRM_PROTOCOL_ALREADY_EXISTS constant.
    /// </summary>
    StatusCrmProtocolAlreadyExists = 0xC019000F,

    /// <summary>
    ///     The STATUS_TRANSACTION_PROPAGATION_FAILED constant.
    /// </summary>
    StatusTransactionPropagationFailed = 0xC0190010,

    /// <summary>
    ///     The STATUS_CRM_PROTOCOL_NOT_FOUND constant.
    /// </summary>
    StatusCrmProtocolNotFound = 0xC0190011,

    /// <summary>
    ///     The STATUS_TRANSACTION_SUPERIOR_EXISTS constant.
    /// </summary>
    StatusTransactionSuperiorExists = 0xC0190012,

    /// <summary>
    ///     The STATUS_TRANSACTION_REQUEST_NOT_VALID constant.
    /// </summary>
    StatusTransactionRequestNotValid = 0xC0190013,

    /// <summary>
    ///     The STATUS_TRANSACTION_NOT_REQUESTED constant.
    /// </summary>
    StatusTransactionNotRequested = 0xC0190014,

    /// <summary>
    ///     The STATUS_TRANSACTION_ALREADY_ABORTED constant.
    /// </summary>
    StatusTransactionAlreadyAborted = 0xC0190015,

    /// <summary>
    ///     The STATUS_TRANSACTION_ALREADY_COMMITTED constant.
    /// </summary>
    StatusTransactionAlreadyCommitted = 0xC0190016,

    /// <summary>
    ///     The STATUS_TRANSACTION_INVALID_MARSHALL_BUFFER constant.
    /// </summary>
    StatusTransactionInvalidMarshallBuffer = 0xC0190017,

    /// <summary>
    ///     The STATUS_CURRENT_TRANSACTION_NOT_VALID constant.
    /// </summary>
    StatusCurrentTransactionNotValid = 0xC0190018,

    /// <summary>
    ///     The STATUS_LOG_GROWTH_FAILED constant.
    /// </summary>
    StatusLogGrowthFailed = 0xC0190019,

    /// <summary>
    ///     The STATUS_OBJECT_NO_LONGER_EXISTS constant.
    /// </summary>
    StatusObjectNoLongerExists = 0xC0190021,

    /// <summary>
    ///     The STATUS_STREAM_MINIVERSION_NOT_FOUND constant.
    /// </summary>
    StatusStreamMiniVersionNotFound = 0xC0190022,

    /// <summary>
    ///     The STATUS_STREAM_MINIVERSION_NOT_VALID constant.
    /// </summary>
    StatusStreamMiniVersionNotValid = 0xC0190023,

    /// <summary>
    ///     The STATUS_MINIVERSION_INACCESSIBLE_FROM_SPECIFIED_TRANSACTION constant.
    /// </summary>
    StatusMiniVersionInaccessibleFromSpecifiedTransaction = 0xC0190024,

    /// <summary>
    ///     The STATUS_CANT_OPEN_MINIVERSION_WITH_MODIFY_INTENT constant.
    /// </summary>
    StatusCantOpenMiniVersionWithModifyIntent = 0xC0190025,

    /// <summary>
    ///     The STATUS_CANT_CREATE_MORE_STREAM_MINIVERSIONS constant.
    /// </summary>
    StatusCantCreateMoreStreamMiniVersions = 0xC0190026,

    /// <summary>
    ///     The STATUS_HANDLE_NO_LONGER_VALID constant.
    /// </summary>
    StatusHandleNoLongerValid = 0xC0190028,

    /// <summary>
    ///     The STATUS_LOG_CORRUPTION_DETECTED constant.
    /// </summary>
    StatusLogCorruptionDetected = 0xC0190030,

    /// <summary>
    ///     The STATUS_RM_DISCONNECTED constant.
    /// </summary>
    StatusRmDisconnected = 0xC0190032,

    /// <summary>
    ///     The STATUS_ENLISTMENT_NOT_SUPERIOR constant.
    /// </summary>
    StatusEnlistmentNotSuperior = 0xC0190033,

    /// <summary>
    ///     The STATUS_FILE_IDENTITY_NOT_PERSISTENT constant.
    /// </summary>
    StatusFileIdentityNotPersistent = 0xC0190036,

    /// <summary>
    ///     The STATUS_CANT_BREAK_TRANSACTIONAL_DEPENDENCY constant.
    /// </summary>
    StatusCantBreakTransactionalDependency = 0xC0190037,

    /// <summary>
    ///     The STATUS_CANT_CROSS_RM_BOUNDARY constant.
    /// </summary>
    StatusCantCrossRmBoundary = 0xC0190038,

    /// <summary>
    ///     The STATUS_TXF_DIR_NOT_EMPTY constant.
    /// </summary>
    StatusTxfDirNotEmpty = 0xC0190039,

    /// <summary>
    ///     The STATUS_INDOUBT_TRANSACTIONS_EXIST constant.
    /// </summary>
    StatusInDoubtTransactionsExist = 0xC019003A,

    /// <summary>
    ///     The STATUS_TM_VOLATILE constant.
    /// </summary>
    StatusTmVolatile = 0xC019003B,

    /// <summary>
    ///     The STATUS_ROLLBACK_TIMER_EXPIRED constant.
    /// </summary>
    StatusRollbackTimerExpired = 0xC019003C,

    /// <summary>
    ///     The STATUS_TXF_ATTRIBUTE_CORRUPT constant.
    /// </summary>
    StatusTxfAttributeCorrupt = 0xC019003D,

    /// <summary>
    ///     The STATUS_EFS_NOT_ALLOWED_IN_TRANSACTION constant.
    /// </summary>
    StatusEfsNotAllowedInTransaction = 0xC019003E,

    /// <summary>
    ///     The STATUS_TRANSACTIONAL_OPEN_NOT_ALLOWED constant.
    /// </summary>
    StatusTransactionalOpenNotAllowed = 0xC019003F,

    /// <summary>
    ///     The STATUS_TRANSACTED_MAPPING_UNSUPPORTED_REMOTE constant.
    /// </summary>
    StatusTransactedMappingUnsupportedRemote = 0xC0190040,

    /// <summary>
    ///     The STATUS_TRANSACTION_REQUIRED_PROMOTION constant.
    /// </summary>
    StatusTransactionRequiredPromotion = 0xC0190043,

    /// <summary>
    ///     The STATUS_CANNOT_EXECUTE_FILE_IN_TRANSACTION constant.
    /// </summary>
    StatusCannotExecuteFileInTransaction = 0xC0190044,

    /// <summary>
    ///     The STATUS_TRANSACTIONS_NOT_FROZEN constant.
    /// </summary>
    StatusTransactionsNotFrozen = 0xC0190045,

    /// <summary>
    ///     The STATUS_TRANSACTION_FREEZE_IN_PROGRESS constant.
    /// </summary>
    StatusTransactionFreezeInProgress = 0xC0190046,

    /// <summary>
    ///     The STATUS_NOT_SNAPSHOT_VOLUME constant.
    /// </summary>
    StatusNotSnapshotVolume = 0xC0190047,

    /// <summary>
    ///     The STATUS_NO_SAVEPOINT_WITH_OPEN_FILES constant.
    /// </summary>
    StatusNoSavePointWithOpenFiles = 0xC0190048,

    /// <summary>
    ///     The STATUS_SPARSE_NOT_ALLOWED_IN_TRANSACTION constant.
    /// </summary>
    StatusSparseNotAllowedInTransaction = 0xC0190049,

    /// <summary>
    ///     The STATUS_TM_IDENTITY_MISMATCH constant.
    /// </summary>
    StatusTmIdentityMismatch = 0xC019004A,

    /// <summary>
    ///     The STATUS_FLOATED_SECTION constant.
    /// </summary>
    StatusFloatedSection = 0xC019004B,

    /// <summary>
    ///     The STATUS_CANNOT_ACCEPT_TRANSACTED_WORK constant.
    /// </summary>
    StatusCannotAcceptTransactedWork = 0xC019004C,

    /// <summary>
    ///     The STATUS_CANNOT_ABORT_TRANSACTIONS constant.
    /// </summary>
    StatusCannotAbortTransactions = 0xC019004D,

    /// <summary>
    ///     The STATUS_TRANSACTION_NOT_FOUND constant.
    /// </summary>
    StatusTransactionNotFound = 0xC019004E,

    /// <summary>
    ///     The STATUS_RESOURCEMANAGER_NOT_FOUND constant.
    /// </summary>
    StatusResourceManagerNotFound = 0xC019004F,

    /// <summary>
    ///     The STATUS_ENLISTMENT_NOT_FOUND constant.
    /// </summary>
    StatusEnlistmentNotFound = 0xC0190050,

    /// <summary>
    ///     The STATUS_TRANSACTIONMANAGER_NOT_FOUND constant.
    /// </summary>
    StatusTransactionManagerNotFound = 0xC0190051,

    /// <summary>
    ///     The STATUS_TRANSACTIONMANAGER_NOT_ONLINE constant.
    /// </summary>
    StatusTransactionManagerNotOnline = 0xC0190052,

    /// <summary>
    ///     The STATUS_TRANSACTIONMANAGER_RECOVERY_NAME_COLLISION constant.
    /// </summary>
    StatusTransactionManagerRecoveryNameCollision = 0xC0190053,

    /// <summary>
    ///     The STATUS_TRANSACTION_NOT_ROOT constant.
    /// </summary>
    StatusTransactionNotRoot = 0xC0190054,

    /// <summary>
    ///     The STATUS_TRANSACTION_OBJECT_EXPIRED constant.
    /// </summary>
    StatusTransactionObjectExpired = 0xC0190055,

    /// <summary>
    ///     The STATUS_COMPRESSION_NOT_ALLOWED_IN_TRANSACTION constant.
    /// </summary>
    StatusCompressionNotAllowedInTransaction = 0xC0190056,

    /// <summary>
    ///     The STATUS_TRANSACTION_RESPONSE_NOT_ENLISTED constant.
    /// </summary>
    StatusTransactionResponseNotEnlisted = 0xC0190057,

    /// <summary>
    ///     The STATUS_TRANSACTION_RECORD_TOO_LONG constant.
    /// </summary>
    StatusTransactionRecordTooLong = 0xC0190058,

    /// <summary>
    ///     The STATUS_NO_LINK_TRACKING_IN_TRANSACTION constant.
    /// </summary>
    StatusNoLinkTrackingInTransaction = 0xC0190059,

    /// <summary>
    ///     The STATUS_OPERATION_NOT_SUPPORTED_IN_TRANSACTION constant.
    /// </summary>
    StatusOperationNotSupportedInTransaction = 0xC019005A,

    /// <summary>
    ///     The STATUS_TRANSACTION_INTEGRITY_VIOLATED constant.
    /// </summary>
    StatusTransactionIntegrityViolated = 0xC019005B,

    /// <summary>
    ///     The STATUS_EXPIRED_HANDLE constant.
    /// </summary>
    StatusExpiredHandle = 0xC0190060,

    /// <summary>
    ///     The STATUS_TRANSACTION_NOT_ENLISTED constant.
    /// </summary>
    StatusTransactionNotEnlisted = 0xC0190061,

    /// <summary>
    ///     The STATUS_LOG_SECTOR_INVALID constant.
    /// </summary>
    StatusLogSectorInvalid = 0xC01A0001,

    /// <summary>
    ///     The STATUS_LOG_SECTOR_PARITY_INVALID constant.
    /// </summary>
    StatusLogSectorParityInvalid = 0xC01A0002,

    /// <summary>
    ///     The STATUS_LOG_SECTOR_REMAPPED constant.
    /// </summary>
    StatusLogSectorRemapped = 0xC01A0003,

    /// <summary>
    ///     The STATUS_LOG_BLOCK_INCOMPLETE constant.
    /// </summary>
    StatusLogBlockIncomplete = 0xC01A0004,

    /// <summary>
    ///     The STATUS_LOG_INVALID_RANGE constant.
    /// </summary>
    StatusLogInvalidRange = 0xC01A0005,

    /// <summary>
    ///     The STATUS_LOG_BLOCKS_EXHAUSTED constant.
    /// </summary>
    StatusLogBlocksExhausted = 0xC01A0006,

    /// <summary>
    ///     The STATUS_LOG_READ_CONTEXT_INVALID constant.
    /// </summary>
    StatusLogReadContextInvalid = 0xC01A0007,

    /// <summary>
    ///     The STATUS_LOG_RESTART_INVALID constant.
    /// </summary>
    StatusLogRestartInvalid = 0xC01A0008,

    /// <summary>
    ///     The STATUS_LOG_BLOCK_VERSION constant.
    /// </summary>
    StatusLogBlockVersion = 0xC01A0009,

    /// <summary>
    ///     The STATUS_LOG_BLOCK_INVALID constant.
    /// </summary>
    StatusLogBlockInvalid = 0xC01A000A,

    /// <summary>
    ///     The STATUS_LOG_READ_MODE_INVALID constant.
    /// </summary>
    StatusLogReadModeInvalid = 0xC01A000B,

    /// <summary>
    ///     The STATUS_LOG_METADATA_CORRUPT constant.
    /// </summary>
    StatusLogMetadataCorrupt = 0xC01A000D,

    /// <summary>
    ///     The STATUS_LOG_METADATA_INVALID constant.
    /// </summary>
    StatusLogMetadataInvalid = 0xC01A000E,

    /// <summary>
    ///     The STATUS_LOG_METADATA_INCONSISTENT constant.
    /// </summary>
    StatusLogMetadataInconsistent = 0xC01A000F,

    /// <summary>
    ///     The STATUS_LOG_RESERVATION_INVALID constant.
    /// </summary>
    StatusLogReservationInvalid = 0xC01A0010,

    /// <summary>
    ///     The STATUS_LOG_CANT_DELETE constant.
    /// </summary>
    StatusLogCantDelete = 0xC01A0011,

    /// <summary>
    ///     The STATUS_LOG_CONTAINER_LIMIT_EXCEEDED constant.
    /// </summary>
    StatusLogContainerLimitExceeded = 0xC01A0012,

    /// <summary>
    ///     The STATUS_LOG_START_OF_LOG constant.
    /// </summary>
    StatusLogStartOfLog = 0xC01A0013,

    /// <summary>
    ///     The STATUS_LOG_POLICY_ALREADY_INSTALLED constant.
    /// </summary>
    StatusLogPolicyAlreadyInstalled = 0xC01A0014,

    /// <summary>
    ///     The STATUS_LOG_POLICY_NOT_INSTALLED constant.
    /// </summary>
    StatusLogPolicyNotInstalled = 0xC01A0015,

    /// <summary>
    ///     The STATUS_LOG_POLICY_INVALID constant.
    /// </summary>
    StatusLogPolicyInvalid = 0xC01A0016,

    /// <summary>
    ///     The STATUS_LOG_POLICY_CONFLICT constant.
    /// </summary>
    StatusLogPolicyConflict = 0xC01A0017,

    /// <summary>
    ///     The STATUS_LOG_PINNED_ARCHIVE_TAIL constant.
    /// </summary>
    StatusLogPinnedArchiveTail = 0xC01A0018,

    /// <summary>
    ///     The STATUS_LOG_RECORD_NONEXISTENT constant.
    /// </summary>
    StatusLogRecordNonexistent = 0xC01A0019,

    /// <summary>
    ///     The STATUS_LOG_RECORDS_RESERVED_INVALID constant.
    /// </summary>
    StatusLogRecordsReservedInvalid = 0xC01A001A,

    /// <summary>
    ///     The STATUS_LOG_SPACE_RESERVED_INVALID constant.
    /// </summary>
    StatusLogSpaceReservedInvalid = 0xC01A001B,

    /// <summary>
    ///     The STATUS_LOG_TAIL_INVALID constant.
    /// </summary>
    StatusLogTailInvalid = 0xC01A001C,

    /// <summary>
    ///     The STATUS_LOG_FULL constant.
    /// </summary>
    StatusLogFull = 0xC01A001D,

    /// <summary>
    ///     The STATUS_LOG_MULTIPLEXED constant.
    /// </summary>
    StatusLogMultiplexed = 0xC01A001E,

    /// <summary>
    ///     The STATUS_LOG_DEDICATED constant.
    /// </summary>
    StatusLogDedicated = 0xC01A001F,

    /// <summary>
    ///     The STATUS_LOG_ARCHIVE_NOT_IN_PROGRESS constant.
    /// </summary>
    StatusLogArchiveNotInProgress = 0xC01A0020,

    /// <summary>
    ///     The STATUS_LOG_ARCHIVE_IN_PROGRESS constant.
    /// </summary>
    StatusLogArchiveInProgress = 0xC01A0021,

    /// <summary>
    ///     The STATUS_LOG_EPHEMERAL constant.
    /// </summary>
    StatusLogEphemeral = 0xC01A0022,

    /// <summary>
    ///     The STATUS_LOG_NOT_ENOUGH_CONTAINERS constant.
    /// </summary>
    StatusLogNotEnoughContainers = 0xC01A0023,

    /// <summary>
    ///     The STATUS_LOG_CLIENT_ALREADY_REGISTERED constant.
    /// </summary>
    StatusLogClientAlreadyRegistered = 0xC01A0024,

    /// <summary>
    ///     The STATUS_LOG_CLIENT_NOT_REGISTERED constant.
    /// </summary>
    StatusLogClientNotRegistered = 0xC01A0025,

    /// <summary>
    ///     The STATUS_LOG_FULL_HANDLER_IN_PROGRESS constant.
    /// </summary>
    StatusLogFullHandlerInProgress = 0xC01A0026,

    /// <summary>
    ///     The STATUS_LOG_CONTAINER_READ_FAILED constant.
    /// </summary>
    StatusLogContainerReadFailed = 0xC01A0027,

    /// <summary>
    ///     The STATUS_LOG_CONTAINER_WRITE_FAILED constant.
    /// </summary>
    StatusLogContainerWriteFailed = 0xC01A0028,

    /// <summary>
    ///     The STATUS_LOG_CONTAINER_OPEN_FAILED constant.
    /// </summary>
    StatusLogContainerOpenFailed = 0xC01A0029,

    /// <summary>
    ///     The STATUS_LOG_CONTAINER_STATE_INVALID constant.
    /// </summary>
    StatusLogContainerStateInvalid = 0xC01A002A,

    /// <summary>
    ///     The STATUS_LOG_STATE_INVALID constant.
    /// </summary>
    StatusLogStateInvalid = 0xC01A002B,

    /// <summary>
    ///     The STATUS_LOG_PINNED constant.
    /// </summary>
    StatusLogPinned = 0xC01A002C,

    /// <summary>
    ///     The STATUS_LOG_METADATA_FLUSH_FAILED constant.
    /// </summary>
    StatusLogMetadataFlushFailed = 0xC01A002D,

    /// <summary>
    ///     The STATUS_LOG_INCONSISTENT_SECURITY constant.
    /// </summary>
    StatusLogInconsistentSecurity = 0xC01A002E,

    /// <summary>
    ///     The STATUS_LOG_APPENDED_FLUSH_FAILED constant.
    /// </summary>
    StatusLogAppendedFlushFailed = 0xC01A002F,

    /// <summary>
    ///     The STATUS_LOG_PINNED_RESERVATION constant.
    /// </summary>
    StatusLogPinnedReservation = 0xC01A0030,

    /// <summary>
    ///     The STATUS_VIDEO_HUNG_DISPLAY_DRIVER_THREAD constant.
    /// </summary>
    StatusVideoHungDisplayDriverThread = 0xC01B00EA,

    /// <summary>
    ///     The STATUS_FLT_NO_HANDLER_DEFINED constant.
    /// </summary>
    StatusFltNoHandlerDefined = 0xC01C0001,

    /// <summary>
    ///     The STATUS_FLT_CONTEXT_ALREADY_DEFINED constant.
    /// </summary>
    StatusFltContextAlreadyDefined = 0xC01C0002,

    /// <summary>
    ///     The STATUS_FLT_INVALID_ASYNCHRONOUS_REQUEST constant.
    /// </summary>
    StatusFltInvalidAsynchronousRequest = 0xC01C0003,

    /// <summary>
    ///     The STATUS_FLT_DISALLOW_FAST_IO constant.
    /// </summary>
    StatusFltDisallowFastIo = 0xC01C0004,

    /// <summary>
    ///     The STATUS_FLT_INVALID_NAME_REQUEST constant.
    /// </summary>
    StatusFltInvalidNameRequest = 0xC01C0005,

    /// <summary>
    ///     The STATUS_FLT_NOT_SAFE_TO_POST_OPERATION constant.
    /// </summary>
    StatusFltNotSafeToPostOperation = 0xC01C0006,

    /// <summary>
    ///     The STATUS_FLT_NOT_INITIALIZED constant.
    /// </summary>
    StatusFltNotInitialized = 0xC01C0007,

    /// <summary>
    ///     The STATUS_FLT_FILTER_NOT_READY constant.
    /// </summary>
    StatusFltFilterNotReady = 0xC01C0008,

    /// <summary>
    ///     The STATUS_FLT_POST_OPERATION_CLEANUP constant.
    /// </summary>
    StatusFltPostOperationCleanup = 0xC01C0009,

    /// <summary>
    ///     The STATUS_FLT_INTERNAL_ERROR constant.
    /// </summary>
    StatusFltInternalError = 0xC01C000A,

    /// <summary>
    ///     The STATUS_FLT_DELETING_OBJECT constant.
    /// </summary>
    StatusFltDeletingObject = 0xC01C000B,

    /// <summary>
    ///     The STATUS_FLT_MUST_BE_NONPAGED_POOL constant.
    /// </summary>
    StatusFltMustBeNonpagedPool = 0xC01C000C,

    /// <summary>
    ///     The STATUS_FLT_DUPLICATE_ENTRY constant.
    /// </summary>
    StatusFltDuplicateEntry = 0xC01C000D,

    /// <summary>
    ///     The STATUS_FLT_CBDQ_DISABLED constant.
    /// </summary>
    StatusFltCbdqDisabled = 0xC01C000E,

    /// <summary>
    ///     The STATUS_FLT_DO_NOT_ATTACH constant.
    /// </summary>
    StatusFltDoNotAttach = 0xC01C000F,

    /// <summary>
    ///     The STATUS_FLT_DO_NOT_DETACH constant.
    /// </summary>
    StatusFltDoNotDetach = 0xC01C0010,

    /// <summary>
    ///     The STATUS_FLT_INSTANCE_ALTITUDE_COLLISION constant.
    /// </summary>
    StatusFltInstanceAltitudeCollision = 0xC01C0011,

    /// <summary>
    ///     The STATUS_FLT_INSTANCE_NAME_COLLISION constant.
    /// </summary>
    StatusFltInstanceNameCollision = 0xC01C0012,

    /// <summary>
    ///     The STATUS_FLT_FILTER_NOT_FOUND constant.
    /// </summary>
    StatusFltFilterNotFound = 0xC01C0013,

    /// <summary>
    ///     The STATUS_FLT_VOLUME_NOT_FOUND constant.
    /// </summary>
    StatusFltVolumeNotFound = 0xC01C0014,

    /// <summary>
    ///     The STATUS_FLT_INSTANCE_NOT_FOUND constant.
    /// </summary>
    StatusFltInstanceNotFound = 0xC01C0015,

    /// <summary>
    ///     The STATUS_FLT_CONTEXT_ALLOCATION_NOT_FOUND constant.
    /// </summary>
    StatusFltContextAllocationNotFound = 0xC01C0016,

    /// <summary>
    ///     The STATUS_FLT_INVALID_CONTEXT_REGISTRATION constant.
    /// </summary>
    StatusFltInvalidContextRegistration = 0xC01C0017,

    /// <summary>
    ///     The STATUS_FLT_NAME_CACHE_MISS constant.
    /// </summary>
    StatusFltNameCacheMiss = 0xC01C0018,

    /// <summary>
    ///     The STATUS_FLT_NO_DEVICE_OBJECT constant.
    /// </summary>
    StatusFltNoDeviceObject = 0xC01C0019,

    /// <summary>
    ///     The STATUS_FLT_VOLUME_ALREADY_MOUNTED constant.
    /// </summary>
    StatusFltVolumeAlreadyMounted = 0xC01C001A,

    /// <summary>
    ///     The STATUS_FLT_ALREADY_ENLISTED constant.
    /// </summary>
    StatusFltAlreadyEnlisted = 0xC01C001B,

    /// <summary>
    ///     The STATUS_FLT_CONTEXT_ALREADY_LINKED constant.
    /// </summary>
    StatusFltContextAlreadyLinked = 0xC01C001C,

    /// <summary>
    ///     The STATUS_FLT_NO_WAITER_FOR_REPLY constant.
    /// </summary>
    StatusFltNoWaiterForReply = 0xC01C0020,

    /// <summary>
    ///     The STATUS_MONITOR_NO_DESCRIPTOR constant.
    /// </summary>
    StatusMonitorNoDescriptor = 0xC01D0001,

    /// <summary>
    ///     The STATUS_MONITOR_UNKNOWN_DESCRIPTOR_FORMAT constant.
    /// </summary>
    StatusMonitorUnknownDescriptorFormat = 0xC01D0002,

    /// <summary>
    ///     The STATUS_MONITOR_INVALID_DESCRIPTOR_CHECKSUM constant.
    /// </summary>
    StatusMonitorInvalidDescriptorChecksum = 0xC01D0003,

    /// <summary>
    ///     The STATUS_MONITOR_INVALID_STANDARD_TIMING_BLOCK constant.
    /// </summary>
    StatusMonitorInvalidStandardTimingBlock = 0xC01D0004,

    /// <summary>
    ///     The STATUS_MONITOR_WMI_DATABLOCK_REGISTRATION_FAILED constant.
    /// </summary>
    StatusMonitorWmiDataBlockRegistrationFailed = 0xC01D0005,

    /// <summary>
    ///     The STATUS_MONITOR_INVALID_SERIAL_NUMBER_MONDSC_BLOCK constant.
    /// </summary>
    StatusMonitorInvalidSerialNumberMondDscBlock = 0xC01D0006,

    /// <summary>
    ///     The STATUS_MONITOR_INVALID_USER_FRIENDLY_MONDSC_BLOCK constant.
    /// </summary>
    StatusMonitorInvalidUserFriendlyMonDscBlock = 0xC01D0007,

    /// <summary>
    ///     The STATUS_MONITOR_NO_MORE_DESCRIPTOR_DATA constant.
    /// </summary>
    StatusMonitorNoMoreDescriptorData = 0xC01D0008,

    /// <summary>
    ///     The STATUS_MONITOR_INVALID_DETAILED_TIMING_BLOCK constant.
    /// </summary>
    StatusMonitorInvalidDetailedTimingBlock = 0xC01D0009,

    /// <summary>
    ///     The STATUS_MONITOR_INVALID_MANUFACTURE_DATE constant.
    /// </summary>
    StatusMonitorInvalidManufactureDate = 0xC01D000A,

    /// <summary>
    ///     The STATUS_GRAPHICS_NOT_EXCLUSIVE_MODE_OWNER constant.
    /// </summary>
    StatusGraphicsNotExclusiveModeOwner = 0xC01E0000,

    /// <summary>
    ///     The STATUS_GRAPHICS_INSUFFICIENT_DMA_BUFFER constant.
    /// </summary>
    StatusGraphicsInsufficientDmaBuffer = 0xC01E0001,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_DISPLAY_ADAPTER constant.
    /// </summary>
    StatusGraphicsInvalidDisplayAdapter = 0xC01E0002,

    /// <summary>
    ///     The STATUS_GRAPHICS_ADAPTER_WAS_RESET constant.
    /// </summary>
    StatusGraphicsAdapterWasReset = 0xC01E0003,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_DRIVER_MODEL constant.
    /// </summary>
    StatusGraphicsInvalidDriverModel = 0xC01E0004,

    /// <summary>
    ///     The STATUS_GRAPHICS_PRESENT_MODE_CHANGED constant.
    /// </summary>
    StatusGraphicsPresentModeChanged = 0xC01E0005,

    /// <summary>
    ///     The STATUS_GRAPHICS_PRESENT_OCCLUDED constant.
    /// </summary>
    StatusGraphicsPresentOccluded = 0xC01E0006,

    /// <summary>
    ///     The STATUS_GRAPHICS_PRESENT_DENIED constant.
    /// </summary>
    StatusGraphicsPresentDenied = 0xC01E0007,

    /// <summary>
    ///     The STATUS_GRAPHICS_CANNOTCOLORCONVERT constant.
    /// </summary>
    StatusGraphicsCannotColorConvert = 0xC01E0008,

    /// <summary>
    ///     The STATUS_GRAPHICS_PRESENT_REDIRECTION_DISABLED constant.
    /// </summary>
    StatusGraphicsPresentRedirectionDisabled = 0xC01E000B,

    /// <summary>
    ///     The STATUS_GRAPHICS_PRESENT_UNOCCLUDED constant.
    /// </summary>
    StatusGraphicsPresentUnoccluded = 0xC01E000C,

    /// <summary>
    ///     The STATUS_GRAPHICS_NO_VIDEO_MEMORY constant.
    /// </summary>
    StatusGraphicsNoVideoMemory = 0xC01E0100,

    /// <summary>
    ///     The STATUS_GRAPHICS_CANT_LOCK_MEMORY constant.
    /// </summary>
    StatusGraphicsCantLockMemory = 0xC01E0101,

    /// <summary>
    ///     The STATUS_GRAPHICS_ALLOCATION_BUSY constant.
    /// </summary>
    StatusGraphicsAllocationBusy = 0xC01E0102,

    /// <summary>
    ///     The STATUS_GRAPHICS_TOO_MANY_REFERENCES constant.
    /// </summary>
    StatusGraphicsTooManyReferences = 0xC01E0103,

    /// <summary>
    ///     The STATUS_GRAPHICS_TRY_AGAIN_LATER constant.
    /// </summary>
    StatusGraphicsTryAgainLater = 0xC01E0104,

    /// <summary>
    ///     The STATUS_GRAPHICS_TRY_AGAIN_NOW constant.
    /// </summary>
    StatusGraphicsTryAgainNow = 0xC01E0105,

    /// <summary>
    ///     The STATUS_GRAPHICS_ALLOCATION_INVALID constant.
    /// </summary>
    StatusGraphicsAllocationInvalid = 0xC01E0106,

    /// <summary>
    ///     The STATUS_GRAPHICS_UNSWIZZLING_APERTURE_UNAVAILABLE constant.
    /// </summary>
    StatusGraphicsUnswizzlingApertureUnavailable = 0xC01E0107,

    /// <summary>
    ///     The STATUS_GRAPHICS_UNSWIZZLING_APERTURE_UNSUPPORTED constant.
    /// </summary>
    StatusGraphicsUnswizzlingApertureUnsupported = 0xC01E0108,

    /// <summary>
    ///     The STATUS_GRAPHICS_CANT_EVICT_PINNED_ALLOCATION constant.
    /// </summary>
    StatusGraphicsCantEvictPinnedAllocation = 0xC01E0109,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_ALLOCATION_USAGE constant.
    /// </summary>
    StatusGraphicsInvalidAllocationUsage = 0xC01E0110,

    /// <summary>
    ///     The STATUS_GRAPHICS_CANT_RENDER_LOCKED_ALLOCATION constant.
    /// </summary>
    StatusGraphicsCantRenderLockedAllocation = 0xC01E0111,

    /// <summary>
    ///     The STATUS_GRAPHICS_ALLOCATION_CLOSED constant.
    /// </summary>
    StatusGraphicsAllocationClosed = 0xC01E0112,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_ALLOCATION_INSTANCE constant.
    /// </summary>
    StatusGraphicsInvalidAllocationInstance = 0xC01E0113,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_ALLOCATION_HANDLE constant.
    /// </summary>
    StatusGraphicsInvalidAllocationHandle = 0xC01E0114,

    /// <summary>
    ///     The STATUS_GRAPHICS_WRONG_ALLOCATION_DEVICE constant.
    /// </summary>
    StatusGraphicsWrongAllocationDevice = 0xC01E0115,

    /// <summary>
    ///     The STATUS_GRAPHICS_ALLOCATION_CONTENT_LOST constant.
    /// </summary>
    StatusGraphicsAllocationContentLost = 0xC01E0116,

    /// <summary>
    ///     The STATUS_GRAPHICS_GPU_EXCEPTION_ON_DEVICE constant.
    /// </summary>
    StatusGraphicsGpuExceptionOnDevice = 0xC01E0200,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_VIDPN_TOPOLOGY constant.
    /// </summary>
    StatusGraphicsInvalidVidpnTopology = 0xC01E0300,

    /// <summary>
    ///     The STATUS_GRAPHICS_VIDPN_TOPOLOGY_NOT_SUPPORTED constant.
    /// </summary>
    StatusGraphicsVidpnTopologyNotSupported = 0xC01E0301,

    /// <summary>
    ///     The STATUS_GRAPHICS_VIDPN_TOPOLOGY_CURRENTLY_NOT_SUPPORTED constant.
    /// </summary>
    StatusGraphicsVidpnTopologyCurrentlyNotSupported = 0xC01E0302,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_VIDPN constant.
    /// </summary>
    StatusGraphicsInvalidVidpn = 0xC01E0303,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_VIDEO_PRESENT_SOURCE constant.
    /// </summary>
    StatusGraphicsInvalidVideoPresentSource = 0xC01E0304,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_VIDEO_PRESENT_TARGET constant.
    /// </summary>
    StatusGraphicsInvalidVideoPresentTarget = 0xC01E0305,

    /// <summary>
    ///     The STATUS_GRAPHICS_VIDPN_MODALITY_NOT_SUPPORTED constant.
    /// </summary>
    StatusGraphicsVidpnModalityNotSupported = 0xC01E0306,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_VIDPN_SOURCEMODESET constant.
    /// </summary>
    StatusGraphicsInvalidVidpnSourceModeSet = 0xC01E0308,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_VIDPN_TARGETMODESET constant.
    /// </summary>
    StatusGraphicsInvalidVidpnTargetModeSet = 0xC01E0309,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_FREQUENCY constant.
    /// </summary>
    StatusGraphicsInvalidFrequency = 0xC01E030A,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_ACTIVE_REGION constant.
    /// </summary>
    StatusGraphicsInvalidActiveRegion = 0xC01E030B,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_TOTAL_REGION constant.
    /// </summary>
    StatusGraphicsInvalidTotalRegion = 0xC01E030C,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_VIDEO_PRESENT_SOURCE_MODE constant.
    /// </summary>
    StatusGraphicsInvalidVideoPresentSourceMode = 0xC01E0310,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_VIDEO_PRESENT_TARGET_MODE constant.
    /// </summary>
    StatusGraphicsInvalidVideoPresentTargetMode = 0xC01E0311,

    /// <summary>
    ///     The STATUS_GRAPHICS_PINNED_MODE_MUST_REMAIN_IN_SET constant.
    /// </summary>
    StatusGraphicsPinnedModeMustRemainInSet = 0xC01E0312,

    /// <summary>
    ///     The STATUS_GRAPHICS_PATH_ALREADY_IN_TOPOLOGY constant.
    /// </summary>
    StatusGraphicsPathAlreadyInTopology = 0xC01E0313,

    /// <summary>
    ///     The STATUS_GRAPHICS_MODE_ALREADY_IN_MODESET constant.
    /// </summary>
    StatusGraphicsModeAlreadyInModeSet = 0xC01E0314,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_VIDEOPRESENTSOURCESET constant.
    /// </summary>
    StatusGraphicsInvalidVideoPresentSourceSet = 0xC01E0315,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_VIDEOPRESENTTARGETSET constant.
    /// </summary>
    StatusGraphicsInvalidVideoPresentTargetSet = 0xC01E0316,

    /// <summary>
    ///     The STATUS_GRAPHICS_SOURCE_ALREADY_IN_SET constant.
    /// </summary>
    StatusGraphicsSourceAlreadyInSet = 0xC01E0317,

    /// <summary>
    ///     The STATUS_GRAPHICS_TARGET_ALREADY_IN_SET constant.
    /// </summary>
    StatusGraphicsTargetAlreadyInSet = 0xC01E0318,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_VIDPN_PRESENT_PATH constant.
    /// </summary>
    StatusGraphicsInvalidVidpnPresentPath = 0xC01E0319,

    /// <summary>
    ///     The STATUS_GRAPHICS_NO_RECOMMENDED_VIDPN_TOPOLOGY constant.
    /// </summary>
    StatusGraphicsNoRecommendedVidpnTopology = 0xC01E031A,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_MONITOR_FREQUENCYRANGESET constant.
    /// </summary>
    StatusGraphicsInvalidMonitorFrequencyRangeSet = 0xC01E031B,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_MONITOR_FREQUENCYRANGE constant.
    /// </summary>
    StatusGraphicsInvalidMonitorFrequencyRange = 0xC01E031C,

    /// <summary>
    ///     The STATUS_GRAPHICS_FREQUENCYRANGE_NOT_IN_SET constant.
    /// </summary>
    StatusGraphicsFrequencyRangeNotInSet = 0xC01E031D,

    /// <summary>
    ///     The STATUS_GRAPHICS_FREQUENCYRANGE_ALREADY_IN_SET constant.
    /// </summary>
    StatusGraphicsFrequencyRangeAlreadyInSet = 0xC01E031F,

    /// <summary>
    ///     The STATUS_GRAPHICS_STALE_MODESET constant.
    /// </summary>
    StatusGraphicsStaleModeSet = 0xC01E0320,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_MONITOR_SOURCEMODESET constant.
    /// </summary>
    StatusGraphicsInvalidMonitorSourceModeSet = 0xC01E0321,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_MONITOR_SOURCE_MODE constant.
    /// </summary>
    StatusGraphicsInvalidMonitorSourceMode = 0xC01E0322,

    /// <summary>
    ///     The STATUS_GRAPHICS_NO_RECOMMENDED_FUNCTIONAL_VIDPN constant.
    /// </summary>
    StatusGraphicsNoRecommendedFunctionalVidpn = 0xC01E0323,

    /// <summary>
    ///     The STATUS_GRAPHICS_MODE_ID_MUST_BE_UNIQUE constant.
    /// </summary>
    StatusGraphicsModeIdMustBeUnique = 0xC01E0324,

    /// <summary>
    ///     The STATUS_GRAPHICS_EMPTY_ADAPTER_MONITOR_MODE_SUPPORT_INTERSECTION constant.
    /// </summary>
    StatusGraphicsEmptyAdapterMonitorModeSupportIntersection = 0xC01E0325,

    /// <summary>
    ///     The STATUS_GRAPHICS_VIDEO_PRESENT_TARGETS_LESS_THAN_SOURCES constant.
    /// </summary>
    StatusGraphicsVideoPresentTargetsLessThanSources = 0xC01E0326,

    /// <summary>
    ///     The STATUS_GRAPHICS_PATH_NOT_IN_TOPOLOGY constant.
    /// </summary>
    StatusGraphicsPathNotInTopology = 0xC01E0327,

    /// <summary>
    ///     The STATUS_GRAPHICS_ADAPTER_MUST_HAVE_AT_LEAST_ONE_SOURCE constant.
    /// </summary>
    StatusGraphicsAdapterMustHaveAtLeastOneSource = 0xC01E0328,

    /// <summary>
    ///     The STATUS_GRAPHICS_ADAPTER_MUST_HAVE_AT_LEAST_ONE_TARGET constant.
    /// </summary>
    StatusGraphicsAdapterMustHaveAtLeastOneTarget = 0xC01E0329,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_MONITORDESCRIPTORSET constant.
    /// </summary>
    StatusGraphicsInvalidMonitorDescriptorSet = 0xC01E032A,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_MONITORDESCRIPTOR constant.
    /// </summary>
    StatusGraphicsInvalidMonitorDescriptor = 0xC01E032B,

    /// <summary>
    ///     The STATUS_GRAPHICS_MONITORDESCRIPTOR_NOT_IN_SET constant.
    /// </summary>
    StatusGraphicsMonitorDescriptorNotInSet = 0xC01E032C,

    /// <summary>
    ///     The STATUS_GRAPHICS_MONITORDESCRIPTOR_ALREADY_IN_SET constant.
    /// </summary>
    StatusGraphicsMonitorDescriptorAlreadyInSet = 0xC01E032D,

    /// <summary>
    ///     The STATUS_GRAPHICS_MONITORDESCRIPTOR_ID_MUST_BE_UNIQUE constant.
    /// </summary>
    StatusGraphicsMonitorDescriptorIdMustBeUnique = 0xC01E032E,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_VIDPN_TARGET_SUBSET_TYPE constant.
    /// </summary>
    StatusGraphicsInvalidVidpnTargetSubsetType = 0xC01E032F,

    /// <summary>
    ///     The STATUS_GRAPHICS_RESOURCES_NOT_RELATED constant.
    /// </summary>
    StatusGraphicsResourcesNotRelated = 0xC01E0330,

    /// <summary>
    ///     The STATUS_GRAPHICS_SOURCE_ID_MUST_BE_UNIQUE constant.
    /// </summary>
    StatusGraphicsSourceIdMustBeUnique = 0xC01E0331,

    /// <summary>
    ///     The STATUS_GRAPHICS_TARGET_ID_MUST_BE_UNIQUE constant.
    /// </summary>
    StatusGraphicsTargetIdMustBeUnique = 0xC01E0332,

    /// <summary>
    ///     The STATUS_GRAPHICS_NO_AVAILABLE_VIDPN_TARGET constant.
    /// </summary>
    StatusGraphicsNoAvailableVidpnTarget = 0xC01E0333,

    /// <summary>
    ///     The STATUS_GRAPHICS_MONITOR_COULD_NOT_BE_ASSOCIATED_WITH_ADAPTER constant.
    /// </summary>
    StatusGraphicsMonitorCouldNotBeAssociatedWithAdapter = 0xC01E0334,

    /// <summary>
    ///     The STATUS_GRAPHICS_NO_VIDPNMGR constant.
    /// </summary>
    StatusGraphicsNoVidpnMgr = 0xC01E0335,

    /// <summary>
    ///     The STATUS_GRAPHICS_NO_ACTIVE_VIDPN constant.
    /// </summary>
    StatusGraphicsNoActiveVidpn = 0xC01E0336,

    /// <summary>
    ///     The STATUS_GRAPHICS_STALE_VIDPN_TOPOLOGY constant.
    /// </summary>
    StatusGraphicsStaleVidpnTopology = 0xC01E0337,

    /// <summary>
    ///     The STATUS_GRAPHICS_MONITOR_NOT_CONNECTED constant.
    /// </summary>
    StatusGraphicsMonitorNotConnected = 0xC01E0338,

    /// <summary>
    ///     The STATUS_GRAPHICS_SOURCE_NOT_IN_TOPOLOGY constant.
    /// </summary>
    StatusGraphicsSourceNotInTopology = 0xC01E0339,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_PRIMARYSURFACE_SIZE constant.
    /// </summary>
    StatusGraphicsInvalidPrimarySurfaceSize = 0xC01E033A,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_VISIBLEREGION_SIZE constant.
    /// </summary>
    StatusGraphicsInvalidVisibleRegionSize = 0xC01E033B,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_STRIDE constant.
    /// </summary>
    StatusGraphicsInvalidStride = 0xC01E033C,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_PIXELFORMAT constant.
    /// </summary>
    StatusGraphicsInvalidPixelFormat = 0xC01E033D,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_COLORBASIS constant.
    /// </summary>
    StatusGraphicsInvalidColorBasis = 0xC01E033E,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_PIXELVALUEACCESSMODE constant.
    /// </summary>
    StatusGraphicsInvalidPixelValueAccessMode = 0xC01E033F,

    /// <summary>
    ///     The STATUS_GRAPHICS_TARGET_NOT_IN_TOPOLOGY constant.
    /// </summary>
    StatusGraphicsTargetNotInTopology = 0xC01E0340,

    /// <summary>
    ///     The STATUS_GRAPHICS_NO_DISPLAY_MODE_MANAGEMENT_SUPPORT constant.
    /// </summary>
    StatusGraphicsNoDisplayModeManagementSupport = 0xC01E0341,

    /// <summary>
    ///     The STATUS_GRAPHICS_VIDPN_SOURCE_IN_USE constant.
    /// </summary>
    StatusGraphicsVidpnSourceInUse = 0xC01E0342,

    /// <summary>
    ///     The STATUS_GRAPHICS_CANT_ACCESS_ACTIVE_VIDPN constant.
    /// </summary>
    StatusGraphicsCantAccessActiveVidpn = 0xC01E0343,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_PATH_IMPORTANCE_ORDINAL constant.
    /// </summary>
    StatusGraphicsInvalidPathImportanceOrdinal = 0xC01E0344,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_PATH_CONTENT_GEOMETRY_TRANSFORMATION constant.
    /// </summary>
    StatusGraphicsInvalidPathContentGeometryTransformation = 0xC01E0345,

    /// <summary>
    ///     The STATUS_GRAPHICS_PATH_CONTENT_GEOMETRY_TRANSFORMATION_NOT_SUPPORTED constant.
    /// </summary>
    StatusGraphicsPathContentGeometryTransformationNotSupported = 0xC01E0346,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_GAMMA_RAMP constant.
    /// </summary>
    StatusGraphicsInvalidGammaRamp = 0xC01E0347,

    /// <summary>
    ///     The STATUS_GRAPHICS_GAMMA_RAMP_NOT_SUPPORTED constant.
    /// </summary>
    StatusGraphicsGammaRampNotSupported = 0xC01E0348,

    /// <summary>
    ///     The STATUS_GRAPHICS_MULTISAMPLING_NOT_SUPPORTED constant.
    /// </summary>
    StatusGraphicsMultisamplingNotSupported = 0xC01E0349,

    /// <summary>
    ///     The STATUS_GRAPHICS_MODE_NOT_IN_MODESET constant.
    /// </summary>
    StatusGraphicsModeNotInModeSet = 0xC01E034A,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_VIDPN_TOPOLOGY_RECOMMENDATION_REASON constant.
    /// </summary>
    StatusGraphicsInvalidVidpnTopologyRecommendationReason = 0xC01E034D,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_PATH_CONTENT_TYPE constant.
    /// </summary>
    StatusGraphicsInvalidPathContentType = 0xC01E034E,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_COPYPROTECTION_TYPE constant.
    /// </summary>
    StatusGraphicsInvalidCopyProtectionType = 0xC01E034F,

    /// <summary>
    ///     The STATUS_GRAPHICS_UNASSIGNED_MODESET_ALREADY_EXISTS constant.
    /// </summary>
    StatusGraphicsUnassignedModeSetAlreadyExists = 0xC01E0350,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_SCANLINE_ORDERING constant.
    /// </summary>
    StatusGraphicsInvalidScanlineOrdering = 0xC01E0352,

    /// <summary>
    ///     The STATUS_GRAPHICS_TOPOLOGY_CHANGES_NOT_ALLOWED constant.
    /// </summary>
    StatusGraphicsTopologyChangesNotAllowed = 0xC01E0353,

    /// <summary>
    ///     The STATUS_GRAPHICS_NO_AVAILABLE_IMPORTANCE_ORDINALS constant.
    /// </summary>
    StatusGraphicsNoAvailableImportanceOrdinals = 0xC01E0354,

    /// <summary>
    ///     The STATUS_GRAPHICS_INCOMPATIBLE_PRIVATE_FORMAT constant.
    /// </summary>
    StatusGraphicsIncompatiblePrivateFormat = 0xC01E0355,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_MODE_PRUNING_ALGORITHM constant.
    /// </summary>
    StatusGraphicsInvalidModePruningAlgorithm = 0xC01E0356,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_MONITOR_CAPABILITY_ORIGIN constant.
    /// </summary>
    StatusGraphicsInvalidMonitorCapabilityOrigin = 0xC01E0357,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_MONITOR_FREQUENCYRANGE_CONSTRAINT constant.
    /// </summary>
    StatusGraphicsInvalidMonitorFrequencyRangeConstraint = 0xC01E0358,

    /// <summary>
    ///     The STATUS_GRAPHICS_MAX_NUM_PATHS_REACHED constant.
    /// </summary>
    StatusGraphicsMaxNumPathsReached = 0xC01E0359,

    /// <summary>
    ///     The STATUS_GRAPHICS_CANCEL_VIDPN_TOPOLOGY_AUGMENTATION constant.
    /// </summary>
    StatusGraphicsCancelVidpnTopologyAugmentation = 0xC01E035A,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_CLIENT_TYPE constant.
    /// </summary>
    StatusGraphicsInvalidClientType = 0xC01E035B,

    /// <summary>
    ///     The STATUS_GRAPHICS_CLIENTVIDPN_NOT_SET constant.
    /// </summary>
    StatusGraphicsClientVidpnNotSet = 0xC01E035C,

    /// <summary>
    ///     The STATUS_GRAPHICS_SPECIFIED_CHILD_ALREADY_CONNECTED constant.
    /// </summary>
    StatusGraphicsSpecifiedChildAlreadyConnected = 0xC01E0400,

    /// <summary>
    ///     The STATUS_GRAPHICS_CHILD_DESCRIPTOR_NOT_SUPPORTED constant.
    /// </summary>
    StatusGraphicsChildDescriptorNotSupported = 0xC01E0401,

    /// <summary>
    ///     The STATUS_GRAPHICS_NOT_A_LINKED_ADAPTER constant.
    /// </summary>
    StatusGraphicsNotALinkedAdapter = 0xC01E0430,

    /// <summary>
    ///     The STATUS_GRAPHICS_LEADLINK_NOT_ENUMERATED constant.
    /// </summary>
    StatusGraphicsLeadLinkNotEnumerated = 0xC01E0431,

    /// <summary>
    ///     The STATUS_GRAPHICS_CHAINLINKS_NOT_ENUMERATED constant.
    /// </summary>
    StatusGraphicsChainLinksNotEnumerated = 0xC01E0432,

    /// <summary>
    ///     The STATUS_GRAPHICS_ADAPTER_CHAIN_NOT_READY constant.
    /// </summary>
    StatusGraphicsAdapterChainNotReady = 0xC01E0433,

    /// <summary>
    ///     The STATUS_GRAPHICS_CHAINLINKS_NOT_STARTED constant.
    /// </summary>
    StatusGraphicsChainLinksNotStarted = 0xC01E0434,

    /// <summary>
    ///     The STATUS_GRAPHICS_CHAINLINKS_NOT_POWERED_ON constant.
    /// </summary>
    StatusGraphicsChainLinksNotPoweredOn = 0xC01E0435,

    /// <summary>
    ///     The STATUS_GRAPHICS_INCONSISTENT_DEVICE_LINK_STATE constant.
    /// </summary>
    StatusGraphicsInconsistentDeviceLinkState = 0xC01E0436,

    /// <summary>
    ///     The STATUS_GRAPHICS_NOT_POST_DEVICE_DRIVER constant.
    /// </summary>
    StatusGraphicsNotPostDeviceDriver = 0xC01E0438,

    /// <summary>
    ///     The STATUS_GRAPHICS_ADAPTER_ACCESS_NOT_EXCLUDED constant.
    /// </summary>
    StatusGraphicsAdapterAccessNotExcluded = 0xC01E043B,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_NOT_SUPPORTED constant.
    /// </summary>
    StatusGraphicsOpmNotSupported = 0xC01E0500,

    /// <summary>
    ///     The STATUS_GRAPHICS_COPP_NOT_SUPPORTED constant.
    /// </summary>
    StatusGraphicsCoppNotSupported = 0xC01E0501,

    /// <summary>
    ///     The STATUS_GRAPHICS_UAB_NOT_SUPPORTED constant.
    /// </summary>
    StatusGraphicsUabNotSupported = 0xC01E0502,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_INVALID_ENCRYPTED_PARAMETERS constant.
    /// </summary>
    StatusGraphicsOpmInvalidEncryptedParameters = 0xC01E0503,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_PARAMETER_ARRAY_TOO_SMALL constant.
    /// </summary>
    StatusGraphicsOpmParameterArrayTooSmall = 0xC01E0504,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_NO_PROTECTED_OUTPUTS_EXIST constant.
    /// </summary>
    StatusGraphicsOpmNoProtectedOutputsExist = 0xC01E0505,

    /// <summary>
    ///     The STATUS_GRAPHICS_PVP_NO_DISPLAY_DEVICE_CORRESPONDS_TO_NAME constant.
    /// </summary>
    StatusGraphicsPvpNoDisplayDeviceCorrespondsToName = 0xC01E0506,

    /// <summary>
    ///     The STATUS_GRAPHICS_PVP_DISPLAY_DEVICE_NOT_ATTACHED_TO_DESKTOP constant.
    /// </summary>
    StatusGraphicsPvpDisplayDeviceNotAttachedToDesktop = 0xC01E0507,

    /// <summary>
    ///     The STATUS_GRAPHICS_PVP_MIRRORING_DEVICES_NOT_SUPPORTED constant.
    /// </summary>
    StatusGraphicsPvpMirroringDevicesNotSupported = 0xC01E0508,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_INVALID_POINTER constant.
    /// </summary>
    StatusGraphicsOpmInvalidPointer = 0xC01E050A,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_INTERNAL_ERROR constant.
    /// </summary>
    StatusGraphicsOpmInternalError = 0xC01E050B,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_INVALID_HANDLE constant.
    /// </summary>
    StatusGraphicsOpmInvalidHandle = 0xC01E050C,

    /// <summary>
    ///     The STATUS_GRAPHICS_PVP_NO_MONITORS_CORRESPOND_TO_DISPLAY_DEVICE constant.
    /// </summary>
    StatusGraphicsPvpNoMonitorsCorrespondToDisplayDevice = 0xC01E050D,

    /// <summary>
    ///     The STATUS_GRAPHICS_PVP_INVALID_CERTIFICATE_LENGTH constant.
    /// </summary>
    StatusGraphicsPvpInvalidCertificateLength = 0xC01E050E,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_SPANNING_MODE_ENABLED constant.
    /// </summary>
    StatusGraphicsOpmSpanningModeEnabled = 0xC01E050F,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_THEATER_MODE_ENABLED constant.
    /// </summary>
    StatusGraphicsOpmTheaterModeEnabled = 0xC01E0510,

    /// <summary>
    ///     The STATUS_GRAPHICS_PVP_HFS_FAILED constant.
    /// </summary>
    StatusGraphicsPvpHfsFailed = 0xC01E0511,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_INVALID_SRM constant.
    /// </summary>
    StatusGraphicsOpmInvalidSrm = 0xC01E0512,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_OUTPUT_DOES_NOT_SUPPORT_HDCP constant.
    /// </summary>
    StatusGraphicsOpmOutputDoesNotSupportHdcp = 0xC01E0513,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_OUTPUT_DOES_NOT_SUPPORT_ACP constant.
    /// </summary>
    StatusGraphicsOpmOutputDoesNotSupportAcp = 0xC01E0514,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_OUTPUT_DOES_NOT_SUPPORT_CGMSA constant.
    /// </summary>
    StatusGraphicsOpmOutputDoesNotSupportCgmsa = 0xC01E0515,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_HDCP_SRM_NEVER_SET constant.
    /// </summary>
    StatusGraphicsOpmHdcpSrmNeverSet = 0xC01E0516,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_RESOLUTION_TOO_HIGH constant.
    /// </summary>
    StatusGraphicsOpmResolutionTooHigh = 0xC01E0517,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_ALL_HDCP_HARDWARE_ALREADY_IN_USE constant.
    /// </summary>
    StatusGraphicsOpmAllHdcpHardwareAlreadyInUse = 0xC01E0518,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_PROTECTED_OUTPUT_NO_LONGER_EXISTS constant.
    /// </summary>
    StatusGraphicsOpmProtectedOutputNoLongerExists = 0xC01E051A,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_SESSION_TYPE_CHANGE_IN_PROGRESS constant.
    /// </summary>
    StatusGraphicsOpmSessionTypeChangeInProgress = 0xC01E051B,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_PROTECTED_OUTPUT_DOES_NOT_HAVE_COPP_SEMANTICS constant.
    /// </summary>
    StatusGraphicsOpmProtectedOutputDoesNotHaveCoppSemantics = 0xC01E051C,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_INVALID_INFORMATION_REQUEST constant.
    /// </summary>
    StatusGraphicsOpmInvalidInformationRequest = 0xC01E051D,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_DRIVER_INTERNAL_ERROR constant.
    /// </summary>
    StatusGraphicsOpmDriverInternalError = 0xC01E051E,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_PROTECTED_OUTPUT_DOES_NOT_HAVE_OPM_SEMANTICS constant.
    /// </summary>
    StatusGraphicsOpmProtectedOutputDoesNotHaveOpmSemantics = 0xC01E051F,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_SIGNALING_NOT_SUPPORTED constant.
    /// </summary>
    StatusGraphicsOpmSignalingNotSupported = 0xC01E0520,

    /// <summary>
    ///     The STATUS_GRAPHICS_OPM_INVALID_CONFIGURATION_REQUEST constant.
    /// </summary>
    StatusGraphicsOpmInvalidConfigurationRequest = 0xC01E0521,

    /// <summary>
    ///     The STATUS_GRAPHICS_I2C_NOT_SUPPORTED constant.
    /// </summary>
    StatusGraphicsI2cNotSupported = 0xC01E0580,

    /// <summary>
    ///     The STATUS_GRAPHICS_I2C_DEVICE_DOES_NOT_EXIST constant.
    /// </summary>
    StatusGraphicsI2cDeviceDoesNotExist = 0xC01E0581,

    /// <summary>
    ///     The STATUS_GRAPHICS_I2C_ERROR_TRANSMITTING_DATA constant.
    /// </summary>
    StatusGraphicsI2cErrorTransmittingData = 0xC01E0582,

    /// <summary>
    ///     The STATUS_GRAPHICS_I2C_ERROR_RECEIVING_DATA constant.
    /// </summary>
    StatusGraphicsI2cErrorReceivingData = 0xC01E0583,

    /// <summary>
    ///     The STATUS_GRAPHICS_DDCCI_VCP_NOT_SUPPORTED constant.
    /// </summary>
    StatusGraphicsDdcciVcpNotSupported = 0xC01E0584,

    /// <summary>
    ///     The STATUS_GRAPHICS_DDCCI_INVALID_DATA constant.
    /// </summary>
    StatusGraphicsDdcciInvalidData = 0xC01E0585,

    /// <summary>
    ///     The STATUS_GRAPHICS_DDCCI_MONITOR_RETURNED_INVALID_TIMING_STATUS_BYTE constant.
    /// </summary>
    StatusGraphicsDdcciMonitorReturnedInvalidTimingStatusByte = 0xC01E0586,

    /// <summary>
    ///     The STATUS_GRAPHICS_DDCCI_INVALID_CAPABILITIES_STRING constant.
    /// </summary>
    StatusGraphicsDdcciInvalidCapabilitiesString = 0xC01E0587,

    /// <summary>
    ///     The STATUS_GRAPHICS_MCA_INTERNAL_ERROR constant.
    /// </summary>
    StatusGraphicsMcaInternalError = 0xC01E0588,

    /// <summary>
    ///     The STATUS_GRAPHICS_DDCCI_INVALID_MESSAGE_COMMAND constant.
    /// </summary>
    StatusGraphicsDdcciInvalidMessageCommand = 0xC01E0589,

    /// <summary>
    ///     The STATUS_GRAPHICS_DDCCI_INVALID_MESSAGE_LENGTH constant.
    /// </summary>
    StatusGraphicsDdcciInvalidMessageLength = 0xC01E058A,

    /// <summary>
    ///     The STATUS_GRAPHICS_DDCCI_INVALID_MESSAGE_CHECKSUM constant.
    /// </summary>
    StatusGraphicsDdcciInvalidMessageChecksum = 0xC01E058B,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_PHYSICAL_MONITOR_HANDLE constant.
    /// </summary>
    StatusGraphicsInvalidPhysicalMonitorHandle = 0xC01E058C,

    /// <summary>
    ///     The STATUS_GRAPHICS_MONITOR_NO_LONGER_EXISTS constant.
    /// </summary>
    StatusGraphicsMonitorNoLongerExists = 0xC01E058D,

    /// <summary>
    ///     The STATUS_GRAPHICS_ONLY_CONSOLE_SESSION_SUPPORTED constant.
    /// </summary>
    StatusGraphicsOnlyConsoleSessionSupported = 0xC01E05E0,

    /// <summary>
    ///     The STATUS_GRAPHICS_NO_DISPLAY_DEVICE_CORRESPONDS_TO_NAME constant.
    /// </summary>
    StatusGraphicsNoDisplayDeviceCorrespondsToName = 0xC01E05E1,

    /// <summary>
    ///     The STATUS_GRAPHICS_DISPLAY_DEVICE_NOT_ATTACHED_TO_DESKTOP constant.
    /// </summary>
    StatusGraphicsDisplayDeviceNotAttachedToDesktop = 0xC01E05E2,

    /// <summary>
    ///     The STATUS_GRAPHICS_MIRRORING_DEVICES_NOT_SUPPORTED constant.
    /// </summary>
    StatusGraphicsMirroringDevicesNotSupported = 0xC01E05E3,

    /// <summary>
    ///     The STATUS_GRAPHICS_INVALID_POINTER constant.
    /// </summary>
    StatusGraphicsInvalidPointer = 0xC01E05E4,

    /// <summary>
    ///     The STATUS_GRAPHICS_NO_MONITORS_CORRESPOND_TO_DISPLAY_DEVICE constant.
    /// </summary>
    StatusGraphicsNoMonitorsCorrespondToDisplayDevice = 0xC01E05E5,

    /// <summary>
    ///     The STATUS_GRAPHICS_PARAMETER_ARRAY_TOO_SMALL constant.
    /// </summary>
    StatusGraphicsParameterArrayTooSmall = 0xC01E05E6,

    /// <summary>
    ///     The STATUS_GRAPHICS_INTERNAL_ERROR constant.
    /// </summary>
    StatusGraphicsInternalError = 0xC01E05E7,

    /// <summary>
    ///     The STATUS_GRAPHICS_SESSION_TYPE_CHANGE_IN_PROGRESS constant.
    /// </summary>
    StatusGraphicsSessionTypeChangeInProgress = 0xC01E05E8,

    /// <summary>
    ///     The STATUS_FVE_LOCKED_VOLUME constant.
    /// </summary>
    StatusFveLockedVolume = 0xC0210000,

    /// <summary>
    ///     The STATUS_FVE_NOT_ENCRYPTED constant.
    /// </summary>
    StatusFveNotEncrypted = 0xC0210001,

    /// <summary>
    ///     The STATUS_FVE_BAD_INFORMATION constant.
    /// </summary>
    StatusFveBadInformation = 0xC0210002,

    /// <summary>
    ///     The STATUS_FVE_TOO_SMALL constant.
    /// </summary>
    StatusFveTooSmall = 0xC0210003,

    /// <summary>
    ///     The STATUS_FVE_FAILED_WRONG_FS constant.
    /// </summary>
    StatusFveFailedWrongFs = 0xC0210004,

    /// <summary>
    ///     The STATUS_FVE_FAILED_BAD_FS constant.
    /// </summary>
    StatusFveFailedBadFs = 0xC0210005,

    /// <summary>
    ///     The STATUS_FVE_FS_NOT_EXTENDED constant.
    /// </summary>
    StatusFveFsNotExtended = 0xC0210006,

    /// <summary>
    ///     The STATUS_FVE_FS_MOUNTED constant.
    /// </summary>
    StatusFveFsMounted = 0xC0210007,

    /// <summary>
    ///     The STATUS_FVE_NO_LICENSE constant.
    /// </summary>
    StatusFveNoLicense = 0xC0210008,

    /// <summary>
    ///     The STATUS_FVE_ACTION_NOT_ALLOWED constant.
    /// </summary>
    StatusFveActionNotAllowed = 0xC0210009,

    /// <summary>
    ///     The STATUS_FVE_BAD_DATA constant.
    /// </summary>
    StatusFveBadData = 0xC021000A,

    /// <summary>
    ///     The STATUS_FVE_VOLUME_NOT_BOUND constant.
    /// </summary>
    StatusFveVolumeNotBound = 0xC021000B,

    /// <summary>
    ///     The STATUS_FVE_NOT_DATA_VOLUME constant.
    /// </summary>
    StatusFveNotDataVolume = 0xC021000C,

    /// <summary>
    ///     The STATUS_FVE_CONV_READ_ERROR constant.
    /// </summary>
    StatusFveConvReadError = 0xC021000D,

    /// <summary>
    ///     The STATUS_FVE_CONV_WRITE_ERROR constant.
    /// </summary>
    StatusFveConvWriteError = 0xC021000E,

    /// <summary>
    ///     The STATUS_FVE_OVERLAPPED_UPDATE constant.
    /// </summary>
    StatusFveOverlappedUpdate = 0xC021000F,

    /// <summary>
    ///     The STATUS_FVE_FAILED_SECTOR_SIZE constant.
    /// </summary>
    StatusFveFailedSectorSize = 0xC0210010,

    /// <summary>
    ///     The STATUS_FVE_FAILED_AUTHENTICATION constant.
    /// </summary>
    StatusFveFailedAuthentication = 0xC0210011,

    /// <summary>
    ///     The STATUS_FVE_NOT_OS_VOLUME constant.
    /// </summary>
    StatusFveNotOsVolume = 0xC0210012,

    /// <summary>
    ///     The STATUS_FVE_KEYFILE_NOT_FOUND constant.
    /// </summary>
    StatusFveKeyFileNotFound = 0xC0210013,

    /// <summary>
    ///     The STATUS_FVE_KEYFILE_INVALID constant.
    /// </summary>
    StatusFveKeyFileInvalid = 0xC0210014,

    /// <summary>
    ///     The STATUS_FVE_KEYFILE_NO_VMK constant.
    /// </summary>
    StatusFveKeyFileNoVmk = 0xC0210015,

    /// <summary>
    ///     The STATUS_FVE_TPM_DISABLED constant.
    /// </summary>
    StatusFveTpmDisabled = 0xC0210016,

    /// <summary>
    ///     The STATUS_FVE_TPM_SRK_AUTH_NOT_ZERO constant.
    /// </summary>
    StatusFveTpmSrkAuthNotZero = 0xC0210017,

    /// <summary>
    ///     The STATUS_FVE_TPM_INVALID_PCR constant.
    /// </summary>
    StatusFveTpmInvalidPcr = 0xC0210018,

    /// <summary>
    ///     The STATUS_FVE_TPM_NO_VMK constant.
    /// </summary>
    StatusFveTpmNoVmk = 0xC0210019,

    /// <summary>
    ///     The STATUS_FVE_PIN_INVALID constant.
    /// </summary>
    StatusFvePinInvalid = 0xC021001A,

    /// <summary>
    ///     The STATUS_FVE_AUTH_INVALID_APPLICATION constant.
    /// </summary>
    StatusFveAuthInvalidApplication = 0xC021001B,

    /// <summary>
    ///     The STATUS_FVE_AUTH_INVALID_CONFIG constant.
    /// </summary>
    StatusFveAuthInvalidConfig = 0xC021001C,

    /// <summary>
    ///     The STATUS_FVE_DEBUGGER_ENABLED constant.
    /// </summary>
    StatusFveDebuggerEnabled = 0xC021001D,

    /// <summary>
    ///     The STATUS_FVE_DRY_RUN_FAILED constant.
    /// </summary>
    StatusFveDryRunFailed = 0xC021001E,

    /// <summary>
    ///     The STATUS_FVE_BAD_METADATA_POINTER constant.
    /// </summary>
    StatusFveBadMetadataPointer = 0xC021001F,

    /// <summary>
    ///     The STATUS_FVE_OLD_METADATA_COPY constant.
    /// </summary>
    StatusFveOldMetadataCopy = 0xC0210020,

    /// <summary>
    ///     The STATUS_FVE_REBOOT_REQUIRED constant.
    /// </summary>
    StatusFveRebootRequired = 0xC0210021,

    /// <summary>
    ///     The STATUS_FVE_RAW_ACCESS constant.
    /// </summary>
    StatusFveRawAccess = 0xC0210022,

    /// <summary>
    ///     The STATUS_FVE_RAW_BLOCKED constant.
    /// </summary>
    StatusFveRawBlocked = 0xC0210023,

    /// <summary>
    ///     The STATUS_FVE_NO_FEATURE_LICENSE constant.
    /// </summary>
    StatusFveNoFeatureLicense = 0xC0210026,

    /// <summary>
    ///     The STATUS_FVE_POLICY_USER_DISABLE_RDV_NOT_ALLOWED constant.
    /// </summary>
    StatusFvePolicyUserDisableRdvNotAllowed = 0xC0210027,

    /// <summary>
    ///     The STATUS_FVE_CONV_RECOVERY_FAILED constant.
    /// </summary>
    StatusFveConvRecoveryFailed = 0xC0210028,

    /// <summary>
    ///     The STATUS_FVE_VIRTUALIZED_SPACE_TOO_BIG constant.
    /// </summary>
    StatusFveVirtualizedSpaceTooBig = 0xC0210029,

    /// <summary>
    ///     The STATUS_FVE_VOLUME_TOO_SMALL constant.
    /// </summary>
    StatusFveVolumeTooSmall = 0xC0210030,

    /// <summary>
    ///     The STATUS_FWP_CALLOUT_NOT_FOUND constant.
    /// </summary>
    StatusFwpCalloutNotFound = 0xC0220001,

    /// <summary>
    ///     The STATUS_FWP_CONDITION_NOT_FOUND constant.
    /// </summary>
    StatusFwpConditionNotFound = 0xC0220002,

    /// <summary>
    ///     The STATUS_FWP_FILTER_NOT_FOUND constant.
    /// </summary>
    StatusFwpFilterNotFound = 0xC0220003,

    /// <summary>
    ///     The STATUS_FWP_LAYER_NOT_FOUND constant.
    /// </summary>
    StatusFwpLayerNotFound = 0xC0220004,

    /// <summary>
    ///     The STATUS_FWP_PROVIDER_NOT_FOUND constant.
    /// </summary>
    StatusFwpProviderNotFound = 0xC0220005,

    /// <summary>
    ///     The STATUS_FWP_PROVIDER_CONTEXT_NOT_FOUND constant.
    /// </summary>
    StatusFwpProviderContextNotFound = 0xC0220006,

    /// <summary>
    ///     The STATUS_FWP_SUBLAYER_NOT_FOUND constant.
    /// </summary>
    StatusFwpSublayerNotFound = 0xC0220007,

    /// <summary>
    ///     The STATUS_FWP_NOT_FOUND constant.
    /// </summary>
    StatusFwpNotFound = 0xC0220008,

    /// <summary>
    ///     The STATUS_FWP_ALREADY_EXISTS constant.
    /// </summary>
    StatusFwpAlreadyExists = 0xC0220009,

    /// <summary>
    ///     The STATUS_FWP_IN_USE constant.
    /// </summary>
    StatusFwpInUse = 0xC022000A,

    /// <summary>
    ///     The STATUS_FWP_DYNAMIC_SESSION_IN_PROGRESS constant.
    /// </summary>
    StatusFwpDynamicSessionInProgress = 0xC022000B,

    /// <summary>
    ///     The STATUS_FWP_WRONG_SESSION constant.
    /// </summary>
    StatusFwpWrongSession = 0xC022000C,

    /// <summary>
    ///     The STATUS_FWP_NO_TXN_IN_PROGRESS constant.
    /// </summary>
    StatusFwpNoTxnInProgress = 0xC022000D,

    /// <summary>
    ///     The STATUS_FWP_TXN_IN_PROGRESS constant.
    /// </summary>
    StatusFwpTxnInProgress = 0xC022000E,

    /// <summary>
    ///     The STATUS_FWP_TXN_ABORTED constant.
    /// </summary>
    StatusFwpTxnAborted = 0xC022000F,

    /// <summary>
    ///     The STATUS_FWP_SESSION_ABORTED constant.
    /// </summary>
    StatusFwpSessionAborted = 0xC0220010,

    /// <summary>
    ///     The STATUS_FWP_INCOMPATIBLE_TXN constant.
    /// </summary>
    StatusFwpIncompatibleTxn = 0xC0220011,

    /// <summary>
    ///     The STATUS_FWP_TIMEOUT constant.
    /// </summary>
    StatusFwpTimeout = 0xC0220012,

    /// <summary>
    ///     The STATUS_FWP_NET_EVENTS_DISABLED constant.
    /// </summary>
    StatusFwpNetEventsDisabled = 0xC0220013,

    /// <summary>
    ///     The STATUS_FWP_INCOMPATIBLE_LAYER constant.
    /// </summary>
    StatusFwpIncompatibleLayer = 0xC0220014,

    /// <summary>
    ///     The STATUS_FWP_KM_CLIENTS_ONLY constant.
    /// </summary>
    StatusFwpKmClientsOnly = 0xC0220015,

    /// <summary>
    ///     The STATUS_FWP_LIFETIME_MISMATCH constant.
    /// </summary>
    StatusFwpLifetimeMismatch = 0xC0220016,

    /// <summary>
    ///     The STATUS_FWP_BUILTIN_OBJECT constant.
    /// </summary>
    StatusFwpBuiltInObject = 0xC0220017,

    /// <summary>
    ///     The STATUS_FWP_TOO_MANY_BOOTTIME_FILTERS constant.
    /// </summary>
    StatusFwpTooManyBootTimeFilters = 0xC0220018,

    /// <summary>
    ///     The STATUS_FWP_TOO_MANY_CALLOUTS constant.
    /// </summary>
    StatusFwpTooManyCallouts = 0xC0220018,

    /// <summary>
    ///     The STATUS_FWP_NOTIFICATION_DROPPED constant.
    /// </summary>
    StatusFwpNotificationDropped = 0xC0220019,

    /// <summary>
    ///     The STATUS_FWP_TRAFFIC_MISMATCH constant.
    /// </summary>
    StatusFwpTrafficMismatch = 0xC022001A,

    /// <summary>
    ///     The STATUS_FWP_INCOMPATIBLE_SA_STATE constant.
    /// </summary>
    StatusFwpIncompatibleSaState = 0xC022001B,

    /// <summary>
    ///     The STATUS_FWP_NULL_POINTER constant.
    /// </summary>
    StatusFwpNullPointer = 0xC022001C,

    /// <summary>
    ///     The STATUS_FWP_INVALID_ENUMERATOR constant.
    /// </summary>
    StatusFwpInvalidEnumerator = 0xC022001D,

    /// <summary>
    ///     The STATUS_FWP_INVALID_FLAGS constant.
    /// </summary>
    StatusFwpInvalidFlags = 0xC022001E,

    /// <summary>
    ///     The STATUS_FWP_INVALID_NET_MASK constant.
    /// </summary>
    StatusFwpInvalidNetMask = 0xC022001F,

    /// <summary>
    ///     The STATUS_FWP_INVALID_RANGE constant.
    /// </summary>
    StatusFwpInvalidRange = 0xC0220020,

    /// <summary>
    ///     The STATUS_FWP_INVALID_INTERVAL constant.
    /// </summary>
    StatusFwpInvalidInterval = 0xC0220021,

    /// <summary>
    ///     The STATUS_FWP_ZERO_LENGTH_ARRAY constant.
    /// </summary>
    StatusFwpZeroLengthArray = 0xC0220022,

    /// <summary>
    ///     The STATUS_FWP_NULL_DISPLAY_NAME constant.
    /// </summary>
    StatusFwpNullDisplayName = 0xC0220023,

    /// <summary>
    ///     The STATUS_FWP_INVALID_ACTION_TYPE constant.
    /// </summary>
    StatusFwpInvalidActionType = 0xC0220024,

    /// <summary>
    ///     The STATUS_FWP_INVALID_WEIGHT constant.
    /// </summary>
    StatusFwpInvalidWeight = 0xC0220025,

    /// <summary>
    ///     The STATUS_FWP_MATCH_TYPE_MISMATCH constant.
    /// </summary>
    StatusFwpMatchTypeMismatch = 0xC0220026,

    /// <summary>
    ///     The STATUS_FWP_TYPE_MISMATCH constant.
    /// </summary>
    StatusFwpTypeMismatch = 0xC0220027,

    /// <summary>
    ///     The STATUS_FWP_OUT_OF_BOUNDS constant.
    /// </summary>
    StatusFwpOutOfBounds = 0xC0220028,

    /// <summary>
    ///     The STATUS_FWP_RESERVED constant.
    /// </summary>
    StatusFwpReserved = 0xC0220029,

    /// <summary>
    ///     The STATUS_FWP_DUPLICATE_CONDITION constant.
    /// </summary>
    StatusFwpDuplicateCondition = 0xC022002A,

    /// <summary>
    ///     The STATUS_FWP_DUPLICATE_KEYMOD constant.
    /// </summary>
    StatusFwpDuplicateKeyMod = 0xC022002B,

    /// <summary>
    ///     The STATUS_FWP_ACTION_INCOMPATIBLE_WITH_LAYER constant.
    /// </summary>
    StatusFwpActionIncompatibleWithLayer = 0xC022002C,

    /// <summary>
    ///     The STATUS_FWP_ACTION_INCOMPATIBLE_WITH_SUBLAYER constant.
    /// </summary>
    StatusFwpActionIncompatibleWithSublayer = 0xC022002D,

    /// <summary>
    ///     The STATUS_FWP_CONTEXT_INCOMPATIBLE_WITH_LAYER constant.
    /// </summary>
    StatusFwpContextIncompatibleWithLayer = 0xC022002E,

    /// <summary>
    ///     The STATUS_FWP_CONTEXT_INCOMPATIBLE_WITH_CALLOUT constant.
    /// </summary>
    StatusFwpContextIncompatibleWithCallout = 0xC022002F,

    /// <summary>
    ///     The STATUS_FWP_INCOMPATIBLE_AUTH_METHOD constant.
    /// </summary>
    StatusFwpIncompatibleAuthMethod = 0xC0220030,

    /// <summary>
    ///     The STATUS_FWP_INCOMPATIBLE_DH_GROUP constant.
    /// </summary>
    StatusFwpIncompatibleDhGroup = 0xC0220031,

    /// <summary>
    ///     The STATUS_FWP_EM_NOT_SUPPORTED constant.
    /// </summary>
    StatusFwpEmNotSupported = 0xC0220032,

    /// <summary>
    ///     The STATUS_FWP_NEVER_MATCH constant.
    /// </summary>
    StatusFwpNeverMatch = 0xC0220033,

    /// <summary>
    ///     The STATUS_FWP_PROVIDER_CONTEXT_MISMATCH constant.
    /// </summary>
    StatusFwpProviderContextMismatch = 0xC0220034,

    /// <summary>
    ///     The STATUS_FWP_INVALID_PARAMETER constant.
    /// </summary>
    StatusFwpInvalidParameter = 0xC0220035,

    /// <summary>
    ///     The STATUS_FWP_TOO_MANY_SUBLAYERS constant.
    /// </summary>
    StatusFwpTooManySublayers = 0xC0220036,

    /// <summary>
    ///     The STATUS_FWP_CALLOUT_NOTIFICATION_FAILED constant.
    /// </summary>
    StatusFwpCalloutNotificationFailed = 0xC0220037,

    /// <summary>
    ///     The STATUS_FWP_INCOMPATIBLE_AUTH_CONFIG constant.
    /// </summary>
    StatusFwpIncompatibleAuthConfig = 0xC0220038,

    /// <summary>
    ///     The STATUS_FWP_INCOMPATIBLE_CIPHER_CONFIG constant.
    /// </summary>
    StatusFwpIncompatibleCipherConfig = 0xC0220039,

    /// <summary>
    ///     The STATUS_FWP_DUPLICATE_AUTH_METHOD constant.
    /// </summary>
    StatusFwpDuplicateAuthMethod = 0xC022003C,

    /// <summary>
    ///     The STATUS_FWP_TCPIP_NOT_READY constant.
    /// </summary>
    StatusFwpTcpIpNotReady = 0xC0220100,

    /// <summary>
    ///     The STATUS_FWP_INJECT_HANDLE_CLOSING constant.
    /// </summary>
    StatusFwpInjectHandleClosing = 0xC0220101,

    /// <summary>
    ///     The STATUS_FWP_INJECT_HANDLE_STALE constant.
    /// </summary>
    StatusFwpInjectHandleStale = 0xC0220102,

    /// <summary>
    ///     The STATUS_FWP_CANNOT_PEND constant.
    /// </summary>
    StatusFwpCannotPend = 0xC0220103,

    /// <summary>
    ///     The STATUS_NDIS_CLOSING constant.
    /// </summary>
    StatusNdisClosing = 0xC0230002,

    /// <summary>
    ///     The STATUS_NDIS_BAD_VERSION constant.
    /// </summary>
    StatusNdisBadVersion = 0xC0230004,

    /// <summary>
    ///     The STATUS_NDIS_BAD_CHARACTERISTICS constant.
    /// </summary>
    StatusNdisBadCharacteristics = 0xC0230005,

    /// <summary>
    ///     The STATUS_NDIS_ADAPTER_NOT_FOUND constant.
    /// </summary>
    StatusNdisAdapterNotFound = 0xC0230006,

    /// <summary>
    ///     The STATUS_NDIS_OPEN_FAILED constant.
    /// </summary>
    StatusNdisOpenFailed = 0xC0230007,

    /// <summary>
    ///     The STATUS_NDIS_DEVICE_FAILED constant.
    /// </summary>
    StatusNdisDeviceFailed = 0xC0230008,

    /// <summary>
    ///     The STATUS_NDIS_MULTICAST_FULL constant.
    /// </summary>
    StatusNdisMulticastFull = 0xC0230009,

    /// <summary>
    ///     The STATUS_NDIS_MULTICAST_EXISTS constant.
    /// </summary>
    StatusNdisMulticastExists = 0xC023000A,

    /// <summary>
    ///     The STATUS_NDIS_MULTICAST_NOT_FOUND constant.
    /// </summary>
    StatusNdisMulticastNotFound = 0xC023000B,

    /// <summary>
    ///     The STATUS_NDIS_REQUEST_ABORTED constant.
    /// </summary>
    StatusNdisRequestAborted = 0xC023000C,

    /// <summary>
    ///     The STATUS_NDIS_RESET_IN_PROGRESS constant.
    /// </summary>
    StatusNdisResetInProgress = 0xC023000D,

    /// <summary>
    ///     The STATUS_NDIS_INVALID_PACKET constant.
    /// </summary>
    StatusNdisInvalidPacket = 0xC023000F,

    /// <summary>
    ///     The STATUS_NDIS_INVALID_DEVICE_REQUEST constant.
    /// </summary>
    StatusNdisInvalidDeviceRequest = 0xC0230010,

    /// <summary>
    ///     The STATUS_NDIS_ADAPTER_NOT_READY constant.
    /// </summary>
    StatusNdisAdapterNotReady = 0xC0230011,

    /// <summary>
    ///     The STATUS_NDIS_INVALID_LENGTH constant.
    /// </summary>
    StatusNdisInvalidLength = 0xC0230014,

    /// <summary>
    ///     The STATUS_NDIS_INVALID_DATA constant.
    /// </summary>
    StatusNdisInvalidData = 0xC0230015,

    /// <summary>
    ///     The STATUS_NDIS_BUFFER_TOO_SHORT constant.
    /// </summary>
    StatusNdisBufferTooShort = 0xC0230016,

    /// <summary>
    ///     The STATUS_NDIS_INVALID_OID constant.
    /// </summary>
    StatusNdisInvalidOid = 0xC0230017,

    /// <summary>
    ///     The STATUS_NDIS_ADAPTER_REMOVED constant.
    /// </summary>
    StatusNdisAdapterRemoved = 0xC0230018,

    /// <summary>
    ///     The STATUS_NDIS_UNSUPPORTED_MEDIA constant.
    /// </summary>
    StatusNdisUnsupportedMedia = 0xC0230019,

    /// <summary>
    ///     The STATUS_NDIS_GROUP_ADDRESS_IN_USE constant.
    /// </summary>
    StatusNdisGroupAddressInUse = 0xC023001A,

    /// <summary>
    ///     The STATUS_NDIS_FILE_NOT_FOUND constant.
    /// </summary>
    StatusNdisFileNotFound = 0xC023001B,

    /// <summary>
    ///     The STATUS_NDIS_ERROR_READING_FILE constant.
    /// </summary>
    StatusNdisErrorReadingFile = 0xC023001C,

    /// <summary>
    ///     The STATUS_NDIS_ALREADY_MAPPED constant.
    /// </summary>
    StatusNdisAlreadyMapped = 0xC023001D,

    /// <summary>
    ///     The STATUS_NDIS_RESOURCE_CONFLICT constant.
    /// </summary>
    StatusNdisResourceConflict = 0xC023001E,

    /// <summary>
    ///     The STATUS_NDIS_MEDIA_DISCONNECTED constant.
    /// </summary>
    StatusNdisMediaDisconnected = 0xC023001F,

    /// <summary>
    ///     The STATUS_NDIS_INVALID_ADDRESS constant.
    /// </summary>
    StatusNdisInvalidAddress = 0xC0230022,

    /// <summary>
    ///     The STATUS_NDIS_PAUSED constant.
    /// </summary>
    StatusNdisPaused = 0xC023002A,

    /// <summary>
    ///     The STATUS_NDIS_INTERFACE_NOT_FOUND constant.
    /// </summary>
    StatusNdisInterfaceNotFound = 0xC023002B,

    /// <summary>
    ///     The STATUS_NDIS_UNSUPPORTED_REVISION constant.
    /// </summary>
    StatusNdisUnsupportedRevision = 0xC023002C,

    /// <summary>
    ///     The STATUS_NDIS_INVALID_PORT constant.
    /// </summary>
    StatusNdisInvalidPort = 0xC023002D,

    /// <summary>
    ///     The STATUS_NDIS_INVALID_PORT_STATE constant.
    /// </summary>
    StatusNdisInvalidPortState = 0xC023002E,

    /// <summary>
    ///     The STATUS_NDIS_LOW_POWER_STATE constant.
    /// </summary>
    StatusNdisLowPowerState = 0xC023002F,

    /// <summary>
    ///     The STATUS_NDIS_NOT_SUPPORTED constant.
    /// </summary>
    StatusNdisNotSupported = 0xC02300BB,

    /// <summary>
    ///     The STATUS_NDIS_OFFLOAD_POLICY constant.
    /// </summary>
    StatusNdisOffloadPolicy = 0xC023100F,

    /// <summary>
    ///     The STATUS_NDIS_OFFLOAD_CONNECTION_REJECTED constant.
    /// </summary>
    StatusNdisOffloadConnectionRejected = 0xC0231012,

    /// <summary>
    ///     The STATUS_NDIS_OFFLOAD_PATH_REJECTED constant.
    /// </summary>
    StatusNdisOffloadPathRejected = 0xC0231013,

    /// <summary>
    ///     The STATUS_NDIS_DOT11_AUTO_CONFIG_ENABLED constant.
    /// </summary>
    StatusNdisDot11AutoConfigEnabled = 0xC0232000,

    /// <summary>
    ///     The STATUS_NDIS_DOT11_MEDIA_IN_USE constant.
    /// </summary>
    StatusNdisDot11MediaInUse = 0xC0232001,

    /// <summary>
    ///     The STATUS_NDIS_DOT11_POWER_STATE_INVALID constant.
    /// </summary>
    StatusNdisDot11PowerStateInvalid = 0xC0232002,

    /// <summary>
    ///     The STATUS_NDIS_PM_WOL_PATTERN_LIST_FULL constant.
    /// </summary>
    StatusNdisPmWolPatternListFull = 0xC0232003,

    /// <summary>
    ///     The STATUS_NDIS_PM_PROTOCOL_OFFLOAD_LIST_FULL constant.
    /// </summary>
    StatusNdisPmProtocolOffloadListFull = 0xC0232004,

    /// <summary>
    ///     The STATUS_IPSEC_BAD_SPI constant.
    /// </summary>
    StatusIpSecBadSpi = 0xC0360001,

    /// <summary>
    ///     The STATUS_IPSEC_SA_LIFETIME_EXPIRED constant.
    /// </summary>
    StatusIpSecSaLifetimeExpired = 0xC0360002,

    /// <summary>
    ///     The STATUS_IPSEC_WRONG_SA constant.
    /// </summary>
    StatusIpSecWrongSa = 0xC0360003,

    /// <summary>
    ///     The STATUS_IPSEC_REPLAY_CHECK_FAILED constant.
    /// </summary>
    StatusIpSecReplayCheckFailed = 0xC0360004,

    /// <summary>
    ///     The STATUS_IPSEC_INVALID_PACKET constant.
    /// </summary>
    StatusIpSecInvalidPacket = 0xC0360005,

    /// <summary>
    ///     The STATUS_IPSEC_INTEGRITY_CHECK_FAILED constant.
    /// </summary>
    StatusIpSecIntegrityCheckFailed = 0xC0360006,

    /// <summary>
    ///     The STATUS_IPSEC_CLEAR_TEXT_DROP constant.
    /// </summary>
    StatusIpSecClearTextDrop = 0xC0360007,

    /// <summary>
    ///     The STATUS_IPSEC_AUTH_FIREWALL_DROP constant.
    /// </summary>
    StatusIpSecAuthFirewallDrop = 0xC0360008,

    /// <summary>
    ///     The STATUS_IPSEC_THROTTLE_DROP constant.
    /// </summary>
    StatusIpSecThrottleDrop = 0xC0360009,

    /// <summary>
    ///     The STATUS_IPSEC_DOSP_BLOCK constant.
    /// </summary>
    StatusIpSecDospBlock = 0xC0368000,

    /// <summary>
    ///     The STATUS_IPSEC_DOSP_RECEIVED_MULTICAST constant.
    /// </summary>
    StatusIpSecDospReceivedMulticast = 0xC0368001,

    /// <summary>
    ///     The STATUS_IPSEC_DOSP_INVALID_PACKET constant.
    /// </summary>
    StatusIpSecDospInvalidPacket = 0xC0368002,

    /// <summary>
    ///     The STATUS_IPSEC_DOSP_STATE_LOOKUP_FAILED constant.
    /// </summary>
    StatusIpSecDospStateLookupFailed = 0xC0368003,

    /// <summary>
    ///     The STATUS_IPSEC_DOSP_MAX_ENTRIES constant.
    /// </summary>
    StatusIpSecDospMaxEntries = 0xC0368004,

    /// <summary>
    ///     The STATUS_IPSEC_DOSP_KEYMOD_NOT_ALLOWED constant.
    /// </summary>
    StatusIpSecDospKeyModNotAllowed = 0xC0368005,

    /// <summary>
    ///     The STATUS_IPSEC_DOSP_MAX_PER_IP_RATELIMIT_QUEUES constant.
    /// </summary>
    StatusIpSecDospMaxPerIpRateLimitQueues = 0xC0368006,

    /// <summary>
    ///     The STATUS_VOLMGR_MIRROR_NOT_SUPPORTED constant.
    /// </summary>
    StatusVolMgrMirrorNotSupported = 0xC038005B,

    /// <summary>
    ///     The STATUS_VOLMGR_RAID5_NOT_SUPPORTED constant.
    /// </summary>
    StatusVolMgrRaid5NotSupported = 0xC038005C,

    /// <summary>
    ///     The STATUS_VIRTDISK_PROVIDER_NOT_FOUND constant.
    /// </summary>
    StatusVirtDiskProviderNotFound = 0xC03A0014,

    /// <summary>
    ///     The STATUS_VIRTDISK_NOT_VIRTUAL_DISK constant.
    /// </summary>
    StatusVirtDiskNotVirtualDisk = 0xC03A0015,

    /// <summary>
    ///     The STATUS_VHD_PARENT_VHD_ACCESS_DENIED constant.
    /// </summary>
    StatusVhdParentVhdAccessDenied = 0xC03A0016,

    /// <summary>
    ///     The STATUS_VHD_CHILD_PARENT_SIZE_MISMATCH constant.
    /// </summary>
    StatusVhdChildParentSizeMismatch = 0xC03A0017,

    /// <summary>
    ///     The STATUS_VHD_DIFFERENCING_CHAIN_CYCLE_DETECTED constant.
    /// </summary>
    StatusVhdDifferencingChainCycleDetected = 0xC03A0018,

    /// <summary>
    ///     The STATUS_VHD_DIFFERENCING_CHAIN_ERROR_IN_PARENT constant.
    /// </summary>
    StatusVhdDifferencingChainErrorInParent = 0xC03A0019,
}
