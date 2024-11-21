// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 1/22/2024.
// Scope of modification:
//   - Code adaptation to project requirements.

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#nullable disable

namespace Kaspirin.UI.Framework.UiKit.ThirdParty.HtmlUtils
{
    /// <summary>
    ///     types of lexical tokens for html-to-xaml converter
    /// </summary>
    internal enum HtmlTokenType
    {
        OpeningTagStart,
        ClosingTagStart,
        TagEnd,
        EmptyTagEnd,
        EqualSign,
        Name,
        Atom, // any attribute value not in quotes
        Text, //text content when accepting text
        Comment,
        Eof
    }
}