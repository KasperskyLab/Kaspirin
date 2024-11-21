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

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Kaspirin.UI.Framework.NativeMethods.Api.Ntdll.Enums
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows-hardware/drivers/ddi/wdm/ne-wdm-_file_information_class">Learn more</seealso>.
    /// </summary>
    public enum FileInformationType : uint
    {
        FileDirectoryInformation = 1,
        FileFullDirectoryInformation = 2,
        FileBothDirectoryInformation = 3,
        FileBasicInformation = 4,
        FileStandardInformation = 5,
        FileInternalInformation = 6,
        FileEaInformation = 7,
        FileAccessInformation = 8,
        FileNameInformation = 9,
        FileRenameInformation = 10,
        FileLinkInformation = 11,
        FileNamesInformation = 12,
        FileDispositionInformation = 13,
        FilePositionInformation = 14,
        FileFullEaInformation = 15,
        FileModeInformation = 16,
        FileAlignmentInformation = 17,
        FileAllInformation = 18,
        FileAllocationInformation = 19,
        FileEndOfFileInformation = 20,
        FileAlternateNameInformation = 21,
        FileStreamInformation = 22,
        FilePipeInformation = 23,
        FilePipeLocalInformation = 24,
        FilePipeRemoteInformation = 25,
        FileMailslotQueryInformation = 26,
        FileMailslotSetInformation = 27,
        FileCompressionInformation = 28,
        FileObjectIdInformation = 29,
        FileCompletionInformation = 30,
        FileMoveClusterInformation = 31,
        FileQuotaInformation = 32,
        FileReparsePointInformation = 33,
        FileNetworkOpenInformation = 34,
        FileAttributeTagInformation = 35,
        FileTrackingInformation = 36,
        FileIdBothDirectoryInformation = 37,
        FileIdFullDirectoryInformation = 38,
        FileValidDataLengthInformation = 39,
        FileShortNameInformation = 40,
        FileIoCompletionNotificationInformation = 41,
        FileIoStatusBlockRangeInformation = 42,
        FileIoPriorityHintInformation = 43,
        FileSfioReserveInformation = 44,
        FileSfioVolumeInformation = 45,
        FileHardLinkInformation = 46,
        FileProcessIdsUsingFileInformation = 47,
        FileNormalizedNameInformation = 48,
        FileNetworkPhysicalNameInformation = 49,
        FileIdGlobalTxDirectoryInformation = 50,
        FileIsRemoteDeviceInformation = 51,
        FileUnusedInformation = 52,
        FileNumaNodeInformation = 53,
        FileStandardLinkInformation = 54,
        FileRemoteProtocolInformation = 55,
        FileRenameInformationBypassAccessCheck = 56,
        FileLinkInformationBypassAccessCheck = 57,
        FileVolumeNameInformation = 58,
        FileIdInformation = 59,
        FileIdExtdDirectoryInformation = 60,
        FileReplaceCompletionInformation = 61,
        FileHardLinkFullIdInformation = 62,
        FileIdExtdBothDirectoryInformation = 63,
        FileDispositionInformationEx = 64,
        FileRenameInformationEx = 65,
        FileRenameInformationExBypassAccessCheck = 66,
        FileDesiredStorageClassInformation = 67,
        FileStatInformation = 68,
        FileMemoryPartitionInformation = 69,
        FileStatLxInformation = 70,
        FileCaseSensitiveInformation = 71,
        FileLinkInformationEx = 72,
        FileLinkInformationExBypassAccessCheck = 73,
        FileStorageReserveIdInformation = 74,
        FileCaseSensitiveInformationForceAccessCheck = 75,
        FileKnownFolderInformation = 76,
        FileStatBasicInformation = 77,
        FileId64ExtdDirectoryInformation = 78,
        FileId64ExtdBothDirectoryInformation = 79,
        FileIdAllExtdDirectoryInformation = 80,
        FileIdAllExtdBothDirectoryInformation = 81,
    }
}
