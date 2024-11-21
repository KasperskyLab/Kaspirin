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

	<xsl:template match="ChipsControls">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">ChipsControls</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="ChipsControl">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:ChipsControl</xsl:with-param>
			<xsl:with-param name="basedOn">ChipsControlUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">

					<!--IconButtonStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">visuals:IconButtonBase</xsl:with-param>
						<xsl:with-param name="basedOn" select="@ButtonStyleId"/>
						<xsl:with-param name="key">IconButtonStyle</xsl:with-param>

						<xsl:with-param name="setters">

							<!--IconButton_Icon_Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">IconButton_Icon_Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ButtonForeground</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>

							<!--IconButton_Padding-->
							<xsl:call-template name="generatePaddingSetter">
								<xsl:with-param name="propertyId">IconButton_Padding</xsl:with-param>
								<xsl:with-param name="paddingNode" select="ButtonPadding" />
							</xsl:call-template>

						</xsl:with-param>

					</xsl:call-template>

					<!--FadeLineStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">visuals:FadeLine</xsl:with-param>
						<xsl:with-param name="basedOn" >FadeLineUniversal</xsl:with-param>
						<xsl:with-param name="key">FadeLineStyle</xsl:with-param>

						<xsl:with-param name="setters">

							<!--FadeLine_IconButton_LeftIcon-->
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">FadeLine_IconButton_LeftIcon</xsl:with-param>
								<xsl:with-param name="propertyValue" select="@ButtonLeftIconName"/>
							</xsl:call-template>

							<!--FadeLine_IconButton_RightIcon-->
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">FadeLine_IconButton_RightIcon</xsl:with-param>
								<xsl:with-param name="propertyValue" select="@ButtonRightIconName"/>
							</xsl:call-template>

							<!--FadeLine_IconButtonStyle-->
							<xsl:call-template name="generateStaticResourceSetter">
								<xsl:with-param name="propertyId">FadeLine_IconButtonStyle</xsl:with-param>
								<xsl:with-param name="resourceName">IconButtonStyle</xsl:with-param>
							</xsl:call-template>

						</xsl:with-param>

					</xsl:call-template>

				</xsl:element>

				<!--ChipsControl_FadeLineStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">ChipsControl_FadeLineStyle</xsl:with-param>
					<xsl:with-param name="resourceName">FadeLineStyle</xsl:with-param>
				</xsl:call-template>

				<!--ChipsControl_ItemContainerStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">ChipsControl_ItemContainerStyle</xsl:with-param>
					<xsl:with-param name="resourceName" select="@ChipsItemStyleId" />
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

</xsl:transform>