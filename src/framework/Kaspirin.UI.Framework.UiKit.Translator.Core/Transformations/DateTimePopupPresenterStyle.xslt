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

<xsl:transform xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
               xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
               xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
               version="1.0">

	<xsl:import href="Common.xslt"/>

	<xsl:template match="DateTimePopupPresenters">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">DateTimePopupPresenters</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="DateTimePopupPresenter">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:DateTimePopupPresenter</xsl:with-param>
			<xsl:with-param name="basedOn">DateTimePopupPresenterUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">
					<!--ItemButtonTextStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@ItemButtonTextStyleId"/>
						<xsl:with-param name="key">ItemButtonTextStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ItemButtonTextForeground</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>
				</xsl:element>

				<!--DateTimePopupPresenter_FooterButton_Cancel_IconName-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">DateTimePopupPresenter_FooterButton_Cancel_IconName</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@FooterButtonCancelIconName" />
				</xsl:call-template>

				<!--DateTimePopupPresenter_FooterButton_Cancel_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">DateTimePopupPresenter_FooterButton_Cancel_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="FooterButtonCancelMargin" />
				</xsl:call-template>

				<!--DateTimePopupPresenter_FooterButton_Confirm_IconName-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">DateTimePopupPresenter_FooterButton_Confirm_IconName</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@FooterButtonConfirmIconName" />
				</xsl:call-template>

				<!--DateTimePopupPresenter_FooterButton_Confirm_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">DateTimePopupPresenter_FooterButton_Confirm_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="FooterButtonConfirmMargin" />
				</xsl:call-template>

				<!--DateTimePopupPresenter_FooterButtonStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">DateTimePopupPresenter_FooterButtonStyle</xsl:with-param>
					<xsl:with-param name="resourceName" select="@FooterButtonStyleId" />
				</xsl:call-template>

				<!--DateTimePopupPresenter_SelectionMarker_Background-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">DateTimePopupPresenter_SelectionMarker_Background</xsl:with-param>
					<xsl:with-param name="brushName">MarkerBackground</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--DateTimePopupPresenter_SelectionMarker_BorderBrush-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">DateTimePopupPresenter_SelectionMarker_BorderBrush</xsl:with-param>
					<xsl:with-param name="brushName">MarkerBorderColor</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--DateTimePopupPresenter_SelectionMarker_BorderThickness-->
				<xsl:call-template name="generateThicknessSetter">
					<xsl:with-param name="propertyId">DateTimePopupPresenter_SelectionMarker_BorderThickness</xsl:with-param>
					<xsl:with-param name="thicknessNode" select="MarkerBorderThickness" />
				</xsl:call-template>

				<!--DateTimePopupPresenter_SelectionMarker_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId">DateTimePopupPresenter_SelectionMarker_CornerRadius</xsl:with-param>
					<xsl:with-param name="cornerRadiusNode" select="MarkerCornerRadius" />
				</xsl:call-template>

				<!--DateTimePopupPresenter_SelectionMarker_Height-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">DateTimePopupPresenter_SelectionMarker_Height</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@MarkerHeight" />
				</xsl:call-template>

				<!--DateTimePopupPresenter_SelectionMarker_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">DateTimePopupPresenter_SelectionMarker_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="MarkerMargin" />
				</xsl:call-template>

				<!--DateTimePopupPresenter_SelectorItem_ButtonStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">DateTimePopupPresenter_SelectorItem_ButtonStyle</xsl:with-param>
					<xsl:with-param name="resourceName" select="@ItemButtonStyleId" />
				</xsl:call-template>

				<!--DateTimePopupPresenter_SelectorItem_Height-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">DateTimePopupPresenter_SelectorItem_Height</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@ItemButtonHeight" />
				</xsl:call-template>

				<!--DateTimePopupPresenter_SelectorItem_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">DateTimePopupPresenter_SelectorItem_Padding</xsl:with-param>
					<xsl:with-param name="paddingNode" select="ItemButtonPadding" />
				</xsl:call-template>

				<!--DateTimePopupPresenter_SelectorItem_TextStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">DateTimePopupPresenter_SelectorItem_TextStyle</xsl:with-param>
					<xsl:with-param name="resourceName">ItemButtonTextStyle</xsl:with-param>
				</xsl:call-template>

				<!--DateTimePopupPresenter_SelectorItem_Width-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">DateTimePopupPresenter_SelectorItem_Width</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@ItemButtonWidth" />
				</xsl:call-template>

			</xsl:with-param>

		</xsl:call-template>

	</xsl:template>

</xsl:transform>
