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

	<xsl:template match="InteractivityNotifications">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">InteractivityNotifications</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="InteractivityNotification">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:InteractivityNotification</xsl:with-param>
			<xsl:with-param name="basedOn">InteractivityNotificationUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">
					<!--TextStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyle</xsl:with-param>
						<xsl:with-param name="setters">

							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">TextForeground</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>

						</xsl:with-param>
					</xsl:call-template>

					<!--CloseButtonStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">visuals:IconButton</xsl:with-param>
						<xsl:with-param name="basedOn" select="@CloseButtonStyleId"/>
						<xsl:with-param name="key">CloseButtonStyle</xsl:with-param>
						<xsl:with-param name="setters">

							<!--Icon-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">Icon</xsl:with-param>
								<xsl:with-param name="propertyValue" select="@CloseButtonIconName"/>
							</xsl:call-template>

							<!--IconForeground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">IconForeground</xsl:with-param>
								<xsl:with-param name="brushName">CloseButtonForeground</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>

						</xsl:with-param>
					</xsl:call-template>
				</xsl:element>

				<!--InteractivityNotification_ActionButton_ButtonStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">InteractivityNotification_ActionButton_ButtonStyle</xsl:with-param>
					<xsl:with-param name="resourceName" select="@ActionButtonStyleId"/>
				</xsl:call-template>

				<!--InteractivityNotification_ActionButton_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">InteractivityNotification_ActionButton_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="ActionButtonMargin" />
				</xsl:call-template>

				<!--InteractivityNotification_ActionButton_MaxWidth-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">InteractivityNotification_ActionButton_MaxWidth</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@ActionButtonMaxWidth"/>
				</xsl:call-template>

				<!--InteractivityNotification_Background-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">InteractivityNotification_Background</xsl:with-param>
					<xsl:with-param name="brushName">Background</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--InteractivityNotification_BorderBrush-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">InteractivityNotification_BorderBrush</xsl:with-param>
					<xsl:with-param name="brushName">BorderColor</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--InteractivityNotification_BorderThickness-->
				<xsl:call-template name="generateThicknessSetter">
					<xsl:with-param name="propertyId">InteractivityNotification_BorderThickness</xsl:with-param>
					<xsl:with-param name="thicknessNode" select="BorderThickness" />
				</xsl:call-template>

				<!--InteractivityNotification_CloseButtton_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">InteractivityNotification_CloseButtton_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="CloseButtonMargin" />
				</xsl:call-template>

				<!--InteractivityNotification_CloseButttonStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">InteractivityNotification_CloseButttonStyle</xsl:with-param>
					<xsl:with-param name="resourceName">CloseButtonStyle</xsl:with-param>
				</xsl:call-template>

				<!--InteractivityNotification_Content_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">InteractivityNotification_Content_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="ContentMargin" />
				</xsl:call-template>

				<!--InteractivityNotification_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId">InteractivityNotification_CornerRadius</xsl:with-param>
				</xsl:call-template>

				<!--InteractivityNotification_Icon_Foreground-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">InteractivityNotification_Icon_Foreground</xsl:with-param>
					<xsl:with-param name="brushName">IconForeground</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--InteractivityNotification_Icon_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">InteractivityNotification_Icon_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="IconMargin" />
				</xsl:call-template>

				<!--InteractivityNotification_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">InteractivityNotification_Margin</xsl:with-param>
				</xsl:call-template>

				<!--InteractivityNotification_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">InteractivityNotification_Padding</xsl:with-param>
				</xsl:call-template>

				<!--InteractivityNotification_Shadow-->
				<xsl:call-template name="generateShadowSetter">
					<xsl:with-param name="propertyId">InteractivityNotification_Shadow</xsl:with-param>
					<xsl:with-param name="scope" select="$id"/>
				</xsl:call-template>

				<!--InteractivityNotification_Text_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">InteractivityNotification_Text_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="TextMargin" />
				</xsl:call-template>

				<!--InteractivityNotification_Text_MaxWidth-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">InteractivityNotification_Text_MaxWidth</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@TextMaxWidth"/>
				</xsl:call-template>

				<!--InteractivityNotification_Text_MaxHeight-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">InteractivityNotification_Text_MaxHeight</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@TextMaxHeight"/>
				</xsl:call-template>

				<!--InteractivityNotification_TextStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">InteractivityNotification_TextStyle</xsl:with-param>
					<xsl:with-param name="resourceName">TextStyle</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Type = Danger trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Danger</xsl:with-param>
					<xsl:with-param name="setters">

						<!--InteractivityNotification_Status_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">InteractivityNotification_Status_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">StatusForegroundDanger</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--InteractivityNotification_Status_IconName-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">InteractivityNotification_Status_IconName</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@StatusIconNameDanger" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Type = Info trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Info</xsl:with-param>
					<xsl:with-param name="setters">

						<!--InteractivityNotification_Status_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">InteractivityNotification_Status_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">StatusForegroundInfo</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--InteractivityNotification_Status_IconName-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">InteractivityNotification_Status_IconName</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@StatusIconNameInfo" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Type = Positive trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Positive</xsl:with-param>
					<xsl:with-param name="setters">

						<!--InteractivityNotification_Status_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">InteractivityNotification_Status_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">StatusForegroundPositive</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--InteractivityNotification_Status_IconName-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">InteractivityNotification_Status_IconName</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@StatusIconNamePositive" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Type = Warning trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Warning</xsl:with-param>
					<xsl:with-param name="setters">

						<!--InteractivityNotification_Status_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">InteractivityNotification_Status_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">StatusForegroundWarning</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--InteractivityNotification_Status_IconName-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">InteractivityNotification_Status_IconName</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@StatusIconNameWarning" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

</xsl:transform>