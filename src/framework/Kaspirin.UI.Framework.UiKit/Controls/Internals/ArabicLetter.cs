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

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
    internal sealed class ArabicLetter
    {
        public ArabicLetter(char commonUnicode, char isolated, char final, char medial, char initial)
        {
            CommonUnicode = commonUnicode;
            IsolatedForm = isolated;
            FinalForm = final;
            _initialForm = initial;
            _medialForm = medial;
        }

        public ArabicLetter(char commonUnicode, char isolated, char final)
        {
            CommonUnicode = commonUnicode;
            IsolatedForm = isolated;
            FinalForm = final;
        }

        public char CommonUnicode { get; }
        public char IsolatedForm { get; }
        public char FinalForm { get; }
        public char InitialForm => HasInitialForm ? _initialForm : IsolatedForm;
        public char MedialForm => HasMedialForm ? _medialForm : FinalForm;
        public bool HasInitialForm => _initialForm != 0;
        public bool HasMedialForm => _medialForm != 0;

        public const char Alif = (char)0x0627;
        public const char AlifMaddah = (char)0x0622;
        public const char AlifHamzaA = (char)0x0623;
        public const char AlifHamzaB = (char)0x0625;

        public const char Lam = (char)0x0644;

        public const char LamWithAlif = (char)0xFEFB;
        public const char LamWithAlifMaddah = (char)0xFEF5;
        public const char LamWithAlifHamzaA = (char)0xFEF7;
        public const char LamWithAlifHamzaB = (char)0xFEF9;

        public static ArabicPresentationForms PresentationForms { get; } = new()
        {
            new ArabicLetter(commonUnicode: AlifMaddah,   isolated: (char)0xFE81, final: (char)0xFE82),
            new ArabicLetter(commonUnicode: AlifHamzaA,   isolated: (char)0xFE83, final: (char)0xFE84),
            new ArabicLetter(commonUnicode: (char)0x0624, isolated: (char)0xFE85, final: (char)0xFE86),
            new ArabicLetter(commonUnicode: AlifHamzaB,   isolated: (char)0xFE87, final: (char)0xFE88),
            new ArabicLetter(commonUnicode: (char)0x0626, isolated: (char)0xFE89, final: (char)0xFE8A, medial: (char)0xFE8C, initial: (char)0xFE8B),
            new ArabicLetter(commonUnicode: Alif,         isolated: (char)0xFE8D, final: (char)0xFE8E),
            new ArabicLetter(commonUnicode: (char)0x0628, isolated: (char)0xFE8F, final: (char)0xFE90, medial: (char)0xFE92, initial: (char)0xFE91),
            new ArabicLetter(commonUnicode: (char)0x0629, isolated: (char)0xFE93, final: (char)0xFE94),
            new ArabicLetter(commonUnicode: (char)0x062A, isolated: (char)0xFE95, final: (char)0xFE96, medial: (char)0xFE98, initial: (char)0xFE97),
            new ArabicLetter(commonUnicode: (char)0x062B, isolated: (char)0xFE99, final: (char)0xFE9A, medial: (char)0xFE9C, initial: (char)0xFE9B),
            new ArabicLetter(commonUnicode: (char)0x062C, isolated: (char)0xFE9D, final: (char)0xFE9E, medial: (char)0xFEA0, initial: (char)0xFE9F),
            new ArabicLetter(commonUnicode: (char)0x062D, isolated: (char)0xFEA1, final: (char)0xFEA2, medial: (char)0xFEA4, initial: (char)0xFEA3),
            new ArabicLetter(commonUnicode: (char)0x062E, isolated: (char)0xFEA5, final: (char)0xFEA6, medial: (char)0xFEA8, initial: (char)0xFEA7),
            new ArabicLetter(commonUnicode: (char)0x062F, isolated: (char)0xFEA9, final: (char)0xFEAA),
            new ArabicLetter(commonUnicode: (char)0x0630, isolated: (char)0xFEAB, final: (char)0xFEAC),
            new ArabicLetter(commonUnicode: (char)0x0631, isolated: (char)0xFEAD, final: (char)0xFEAE),
            new ArabicLetter(commonUnicode: (char)0x0632, isolated: (char)0xFEAF, final: (char)0xFEB0),
            new ArabicLetter(commonUnicode: (char)0x0633, isolated: (char)0xFEB1, final: (char)0xFEB2, medial: (char)0xFEB4, initial: (char)0xFEB3),
            new ArabicLetter(commonUnicode: (char)0x0634, isolated: (char)0xFEB5, final: (char)0xFEB6, medial: (char)0xFEB8, initial: (char)0xFEB7),
            new ArabicLetter(commonUnicode: (char)0x0635, isolated: (char)0xFEB9, final: (char)0xFEBA, medial: (char)0xFEBC, initial: (char)0xFEBB),
            new ArabicLetter(commonUnicode: (char)0x0636, isolated: (char)0xFEBD, final: (char)0xFEBE, medial: (char)0xFEC0, initial: (char)0xFEBF),
            new ArabicLetter(commonUnicode: (char)0x0637, isolated: (char)0xFEC1, final: (char)0xFEC2, medial: (char)0xFEC4, initial: (char)0xFEC3),
            new ArabicLetter(commonUnicode: (char)0x0638, isolated: (char)0xFEC5, final: (char)0xFEC6, medial: (char)0xFEC8, initial: (char)0xFEC7),
            new ArabicLetter(commonUnicode: (char)0x0639, isolated: (char)0xFEC9, final: (char)0xFECA, medial: (char)0xFECC, initial: (char)0xFECB),
            new ArabicLetter(commonUnicode: (char)0x063A, isolated: (char)0xFECD, final: (char)0xFECE, medial: (char)0xFED0, initial: (char)0xFECF),
            new ArabicLetter(commonUnicode: (char)0x0641, isolated: (char)0xFED1, final: (char)0xFED2, medial: (char)0xFED4, initial: (char)0xFED3),
            new ArabicLetter(commonUnicode: (char)0x0642, isolated: (char)0xFED5, final: (char)0xFED6, medial: (char)0xFED8, initial: (char)0xFED7),
            new ArabicLetter(commonUnicode: (char)0x0643, isolated: (char)0xFED9, final: (char)0xFEDA, medial: (char)0xFEDC, initial: (char)0xFEDB),
            new ArabicLetter(commonUnicode: Lam,          isolated: (char)0xFEDD, final: (char)0xFEDE, medial: (char)0xFEE0, initial: (char)0xFEDF),
            new ArabicLetter(commonUnicode: (char)0x0645, isolated: (char)0xFEE1, final: (char)0xFEE2, medial: (char)0xFEE4, initial: (char)0xFEE3),
            new ArabicLetter(commonUnicode: (char)0x0646, isolated: (char)0xFEE5, final: (char)0xFEE6, medial: (char)0xFEE8, initial: (char)0xFEE7),
            new ArabicLetter(commonUnicode: (char)0x0647, isolated: (char)0xFEE9, final: (char)0xFEEA, medial: (char)0xFEEC, initial: (char)0xFEEB),
            new ArabicLetter(commonUnicode: (char)0x0648, isolated: (char)0xFEED, final: (char)0xFEEE),
            new ArabicLetter(commonUnicode: (char)0x0649, isolated: (char)0xFEEF, final: (char)0xFEF0),
            new ArabicLetter(commonUnicode: (char)0x064A, isolated: (char)0xFEF1, final: (char)0xFEF2, medial: (char)0xFEF4, initial: (char)0xFEF3),
            new ArabicLetter(commonUnicode: LamWithAlif,  isolated: (char)0xFEFB, final: (char)0xFEFC),
            new ArabicLetter(commonUnicode: LamWithAlifMaddah,  isolated: (char)0xFEF5, final: (char)0xFEF6),
            new ArabicLetter(commonUnicode: LamWithAlifHamzaA,  isolated: (char)0xFEF7, final: (char)0xFEF8),
            new ArabicLetter(commonUnicode: LamWithAlifHamzaB,  isolated: (char)0xFEF9, final: (char)0xFEFA),
        };

        public static bool IsAlifChar(char c)
        {
            return _alifChars.Contains(c);
        }

        public sealed class ArabicPresentationForms : KeyedCollection<char, ArabicLetter>
        {
            protected override char GetKeyForItem(ArabicLetter item)
            {
                return item.CommonUnicode;
            }
        }

        private readonly char _initialForm;
        private readonly char _medialForm;

        private static readonly IList<char> _alifChars = new[] { Alif, AlifMaddah, AlifHamzaA, AlifHamzaB };
    }
}
