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

	<xsl:template match="ImageGalleryLists">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">ImageGalleryLists</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="ImageGalleryList">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>
		
		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:ImageGalleryList</xsl:with-param>
			<xsl:with-param name="basedOn">ImageGalleryListUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">
					<!--ImageButtonStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">visuals:ImageGalleryListButton</xsl:with-param>
						<xsl:with-param name="basedOn" select="@ImageButtonStyleId"/>
						<xsl:with-param name="key">ImageButtonStyle</xsl:with-param>
					</xsl:call-template>
					<!--LeftButtonStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">visuals:ImageGalleryButton</xsl:with-param>
						<xsl:with-param name="basedOn" select="@LeftButtonStyleId"/>
						<xsl:with-param name="key">LeftButtonStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--Icon-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">Icon</xsl:with-param>
								<xsl:with-param name="propertyValue" select="@LeftButtonIconName"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>
					<!--RightButtonStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">visuals:ImageGalleryButton</xsl:with-param>
						<xsl:with-param name="basedOn" select="@RightButtonStyleId"/>
						<xsl:with-param name="key">RightButtonStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--Icon-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">Icon</xsl:with-param>
								<xsl:with-param name="propertyValue" select="@RightButtonIconName"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>
				</xsl:element>

				<!--ImageGalleryList_ImageButtonStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">ImageGalleryList_ImageButtonStyle</xsl:with-param>
					<xsl:with-param name="resourceName">ImageButtonStyle</xsl:with-param>
				</xsl:call-template>

				<!--ImageGalleryList_ImageGap-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">ImageGalleryList_ImageGap</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@ImageGap" />
				</xsl:call-template>

				<!--ImageGalleryList_LeftButton_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">ImageGalleryList_LeftButton_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="LeftButtonMargin" />
				</xsl:call-template>

				<!--ImageGalleryList_LeftButtonStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">ImageGalleryList_LeftButtonStyle</xsl:with-param>
					<xsl:with-param name="resourceName">LeftButtonStyle</xsl:with-param>
				</xsl:call-template>

				<!--ImageGalleryList_RightButton_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">ImageGalleryList_RightButton_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="RightButtonMargin" />
				</xsl:call-template>

				<!--ImageGalleryList_RightButtonStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">ImageGalleryList_RightButtonStyle</xsl:with-param>
					<xsl:with-param name="resourceName">RightButtonStyle</xsl:with-param>
				</xsl:call-template>
				
			</xsl:with-param>

		</xsl:call-template>

	</xsl:template>

</xsl:transform>
