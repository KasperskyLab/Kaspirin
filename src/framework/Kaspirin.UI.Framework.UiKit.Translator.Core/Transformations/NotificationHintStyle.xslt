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

	<xsl:template match="NotificationHints">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">NotificationHints</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="NotificationHint">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:NotificationHint</xsl:with-param>
			<xsl:with-param name="basedOn">NotificationHintUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">

					<!--TextStyleDanger-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleDanger</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundDanger</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleInfo-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleInfo</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundInfo</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStylePositive-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStylePositive</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundPositive</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleWarning-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleWarning</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundWarning</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

				</xsl:element>

				<!--NotificationHint_Icon_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">NotificationHint_Icon_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="IconMargin" />
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Danger trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Danger</xsl:with-param>
					<xsl:with-param name="setters">

						<!--NotificationHint_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">NotificationHint_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconForegroundDanger</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--NotificationHint_Icon_Name-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">NotificationHint_Icon_Name</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@IconNameDanger" />
						</xsl:call-template>

						<!--NotificationHint_TextStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">NotificationHint_TextStyle</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleDanger</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Info trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Info</xsl:with-param>
					<xsl:with-param name="setters">

						<!--NotificationHint_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">NotificationHint_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconForegroundInfo</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--NotificationHint_Icon_Name-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">NotificationHint_Icon_Name</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@IconNameInfo" />
						</xsl:call-template>

						<!--NotificationHint_TextStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">NotificationHint_TextStyle</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleInfo</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Positive trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Positive</xsl:with-param>
					<xsl:with-param name="setters">

						<!--NotificationHint_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">NotificationHint_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconForegroundPositive</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--NotificationHint_Icon_Name-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">NotificationHint_Icon_Name</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@IconNamePositive" />
						</xsl:call-template>

						<!--NotificationHint_TextStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">NotificationHint_TextStyle</xsl:with-param>
							<xsl:with-param name="resourceName">TextStylePositive</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Warning trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Warning</xsl:with-param>
					<xsl:with-param name="setters">

						<!--NotificationHint_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">NotificationHint_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconForegroundWarning</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--NotificationHint_Icon_Name-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">NotificationHint_Icon_Name</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@IconNameWarning" />
						</xsl:call-template>

						<!--NotificationHint_TextStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">NotificationHint_TextStyle</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleWarning</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

</xsl:transform>