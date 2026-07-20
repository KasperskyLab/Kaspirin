<!--
    Copyright © 2024 AO Kaspersky Lab.

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

        http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.
-->

<xsl:transform xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
               xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
               version="1.0">

	<xsl:output method="xml" indent="yes" encoding="UTF-8" omit-xml-declaration="yes"/>

	<xsl:param name="excludedControlsFilter"/>
	<xsl:param name="excludedControlsFilterDelimiter"/>
	<xsl:param name="commentText"/>

	<xsl:variable name="excludedControls" select="$excludedControlsFilter"/>
	<xsl:variable name="excludedControlsDelimiter" select="$excludedControlsFilterDelimiter"/>

	<xsl:variable name="source" select="UiKit/@Source"/>
	<xsl:variable name="comment" select="$commentText"/>

	<xsl:include href="BadgeCustomizationDictionary.xslt"/>
	<xsl:include href="BulletCustomizationDictionary.xslt"/>
	<xsl:include href="ButtonCustomizationDictionary.xslt"/>
	<xsl:include href="CarouselItemCustomizationDictionary.xslt"/>
	<xsl:include href="CheckBoxRadioButtonCustomizationDictionary.xslt"/>
	<xsl:include href="ChipsControlCustomizationDictionary.xslt"/>
	<xsl:include href="ChipsItemCustomizationDictionary.xslt"/>
	<xsl:include href="CodeInputCustomizationDictionary.xslt"/>
	<xsl:include href="ContentButtonCustomizationDictionary.xslt"/>
	<xsl:include href="ContextMenuCustomizationDictionary.xslt"/>
	<xsl:include href="DateTimeInputCustomizationDictionary.xslt"/>
	<xsl:include href="DateTimePopupFooterButtonCustomizationDictionary.xslt"/>
	<xsl:include href="DateTimePopupItemButtonCustomizationDictionary.xslt"/>
	<xsl:include href="DateTimePopupPresenterCustomizationDictionary.xslt"/>
	<xsl:include href="DividerCustomizationDictionary.xslt"/>
	<xsl:include href="HyperlinkCustomizationDictionary.xslt"/>
	<xsl:include href="IconButtonCustomizationDictionary.xslt"/>
	<xsl:include href="IconCustomizationDictionary.xslt"/>
	<xsl:include href="ImageGalleryButtonCustomizationDictionary.xslt"/>
	<xsl:include href="ImageGalleryCustomizationDictionary.xslt"/>
	<xsl:include href="ImageGalleryListButtonCustomizationDictionary.xslt"/>
	<xsl:include href="InteractivityDialogCustomizationDictionary.xslt"/>
	<xsl:include href="InteractivityNotificationCustomizationDictionary.xslt"/>
	<xsl:include href="InteractivityOverlayCustomizationDictionary.xslt"/>
	<xsl:include href="IslandButtonCustomizationDictionary.xslt"/>
	<xsl:include href="IslandCustomizationDictionary.xslt"/>
	<xsl:include href="IslandToggleButtonCustomizationDictionary.xslt"/>
	<xsl:include href="ListMenuItemCustomizationDictionary.xslt"/>
	<xsl:include href="MenuItemCustomizationDictionary.xslt"/>
	<xsl:include href="NavigationMenuButtonCustomizationDictionary.xslt"/>
	<xsl:include href="NavigationMenuFooterButtonCustomizationDictionary.xslt"/>
	<xsl:include href="NotificationHintCustomizationDictionary.xslt"/>
	<xsl:include href="NotificationPanelCustomizationDictionary.xslt"/>
	<xsl:include href="PasswordInputCustomizationDictionary.xslt"/>
	<xsl:include href="PopupCustomizationDictionary.xslt"/>
	<xsl:include href="ProgressBarCustomizationDictionary.xslt"/>
	<xsl:include href="RoundProgressCustomizationDictionary.xslt"/>
	<xsl:include href="RoundTimerCustomizationDictionary.xslt"/>
	<xsl:include href="ScrollBarCustomizationDictionary.xslt"/>
	<xsl:include href="SearchCustomizationDictionary.xslt"/>
	<xsl:include href="SelectableTextCustomizationDictionary.xslt"/>
	<xsl:include href="SelectCustomizationDictionary.xslt"/>
	<xsl:include href="SelectItemCustomizationDictionary.xslt"/>
	<xsl:include href="SelectPresenterCustomizationDictionary.xslt"/>
	<xsl:include href="SpinnerCustomizationDictionary.xslt"/>
	<xsl:include href="SplitButtonCustomizationDictionary.xslt"/>
	<xsl:include href="SsoButtonCustomizationDictionary.xslt"/>
	<xsl:include href="StatusBulletCustomizationDictionary.xslt"/>
	<xsl:include href="StatusTagCustomizationDictionary.xslt"/>
	<xsl:include href="SwitchCustomizationDictionary.xslt"/>
	<xsl:include href="TabMenuCustomizationDictionary.xslt"/>
	<xsl:include href="TabMenuItemCustomizationDictionary.xslt"/>
	<xsl:include href="TagCustomizationDictionary.xslt"/>
	<xsl:include href="TextAreaCustomizationDictionary.xslt"/>
	<xsl:include href="TextInputCustomizationDictionary.xslt"/>
	<xsl:include href="TextStyleCustomizationDictionary.xslt"/>
	<xsl:include href="TextViewerCustomizationDictionary.xslt"/>
	<xsl:include href="ToolTipCustomizationDictionary.xslt"/>

	<xsl:template match="UiKit">
		<xsl:element name="CustomizationDictionaries">
			<xsl:apply-templates select="Controls" />
		</xsl:element>
	</xsl:template>

</xsl:transform>