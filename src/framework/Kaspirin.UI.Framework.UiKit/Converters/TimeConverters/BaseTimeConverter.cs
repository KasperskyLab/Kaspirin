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

using Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Options;
using Kaspirin.UI.Framework.UiKit.Converters.TimeConverters.Validation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Converters.TimeConverters
{
    public abstract class BaseTimeConverter<TDateType, TConvertionType>
        : ValueConverterMarkupExtension<BaseTimeConverter<TDateType, TConvertionType>>, IResourceProvider
        where TDateType : notnull, IEquatable<TDateType>
        where TConvertionType : struct, Enum
    {
        public BaseTimeConverter()
        {
            InvalidValueString = GetLocForResource("TimeConverter_InvalidValue");
        }

        #region IResourceProvider

        object? IResourceProvider.GetResource(object? value)
        {
            return ConvertValue(value);
        }

        #endregion

        public TConvertionType Type { get; set; }

        public bool LowercaseFirstLetter { get; set; }

        public BaseTimeValidator<TDateType>? Validator { get; set; }

        public LocExtension? InvalidValueString { get; set; }

        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
        {
            return ConvertValue(value).ProvideConstantStringValue();
        }

        public abstract LocExtension Convert(TDateType date);

        protected LocExtension GetLocForConstantString(string constantString)
        {
            return new LocExtension("TimeConverter_Value", UIKitConstants.LocalizationScope)
            {
                Param = new LocParameter("Value") { ParamSource = new Binding { Source = constantString } },
                Option = LowercaseFirstLetter ? _lowercaseFirstLetterLocOption : null
            };
        }

        protected LocExtension GetLocForResource(string resourceKey)
        {
            return new LocExtension(resourceKey, UIKitConstants.LocalizationScope)
            {
                Option = LowercaseFirstLetter ? _lowercaseFirstLetterLocOption : null
            };
        }

        protected LocExtension GetLocForResourceWithParam(string resourceKey, string paramName, string value)
        {
            return new LocExtension(resourceKey, UIKitConstants.LocalizationScope)
            {
                Param = new LocParameter(paramName) { ParamSource = new Binding { Source = value } },
                Option = LowercaseFirstLetter ? _lowercaseFirstLetterLocOption : null
            };
        }

        protected LocExtension GetLocForResourceWithParams(string resourceKey, Dictionary<string, string> paramValues)
        {
            return new LocExtension(resourceKey, UIKitConstants.LocalizationScope)
            {
                Params = new LocParameterCollection(paramValues),
                Option = LowercaseFirstLetter ? _lowercaseFirstLetterLocOption : null
            };
        }

        private LocExtension? GetInvalidValueString()
        {
            return InvalidValueString switch
            {
                null => null,
                _ => new LocExtension(InvalidValueString.Key, InvalidValueString.Scope)
                {
                    Option = LowercaseFirstLetter ? _lowercaseFirstLetterLocOption : null
                }
            };
        }

        private LocExtension ConvertValue(object? value)
        {
            if (value is TDateType dateTypeValue)
            {
                if (Validator?.ValueIsValid(dateTypeValue) != false)
                {
                    return Convert(dateTypeValue);
                }
            }

            var invalidValue = GetInvalidValueString();
            if (invalidValue != null)
            {
                return invalidValue;
            }

            return GetLocForConstantString(string.Empty);
        }

        private static readonly LocOption _lowercaseFirstLetterLocOption = LocOptionFactory.CreateChangeCaseOption(ChangeCaseOptionMode.LowercaseFirstLetterOnly);
    }
}
