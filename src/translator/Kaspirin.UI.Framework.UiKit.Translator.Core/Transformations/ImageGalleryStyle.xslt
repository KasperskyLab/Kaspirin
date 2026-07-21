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

	<xsl:template match="ImageGalleries">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">ImageGalleries</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="ImageGallery">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		
		
		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:ImageGallery</xsl:with-param>
			<xsl:with-param name="basedOn">ImageGalleryUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">
					<xsl:variable name="carouselId">
						<xsl:call-template name="replace">
							<xsl:with-param name="value" select="@CarouselStyleId"/>
							<xsl:with-param name="old">Item</xsl:with-param>
							<xsl:with-param name="new"></xsl:with-param>
						</xsl:call-template>
					</xsl:variable>
					
					<!--CarouselStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">visuals:Carousel</xsl:with-param>
						<xsl:with-param name="basedOn" select="$carouselId"/>
						<xsl:with-param name="key">CarouselStyle</xsl:with-param>
					</xsl:call-template>
					
					<!--CounterTextStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@CounterTextStyleId"/>
						<xsl:with-param name="key">CounterTextStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">CounterTextForeground</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--CloseButtonStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">visuals:ImageGalleryButton</xsl:with-param>
						<xsl:with-param name="basedOn" select="@CloseButtonStyleId"/>
						<xsl:with-param name="key">CloseButtonStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--Icon-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">Icon</xsl:with-param>
								<xsl:with-param name="propertyValue" select="@CloseButtonIconName"/>
							</xsl:call-template>
						</xsl:with-param>
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
				
				<!--ImageGallery_CarouselContainer_Background-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">ImageGallery_CarouselContainer_Background</xsl:with-param>
					<xsl:with-param name="brushName">CarouselContainerBackground</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--ImageGallery_CarouselContainer_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId">ImageGallery_CarouselContainer_CornerRadius</xsl:with-param>
					<xsl:with-param name="cornerRadiusNode" select="CarouselContainerCornerRadius" />
				</xsl:call-template>

				<!--ImageGallery_CarouselContainer_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">ImageGallery_CarouselContainer_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="CarouselContainerMargin" />
				</xsl:call-template>

				<!--ImageGallery_CarouselContainer_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">ImageGallery_CarouselContainer_Padding</xsl:with-param>
					<xsl:with-param name="paddingNode" select="CarouselContainerPadding" />
				</xsl:call-template>

				<!--ImageGallery_CarouselStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">ImageGallery_CarouselStyle</xsl:with-param>
					<xsl:with-param name="resourceName">CarouselStyle</xsl:with-param>
				</xsl:call-template>

				<!--ImageGallery_CloseButton_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">ImageGallery_CloseButton_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="CloseButtonMargin" />
				</xsl:call-template>

				<!--ImageGallery_CloseButtonStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">ImageGallery_CloseButtonStyle</xsl:with-param>
					<xsl:with-param name="resourceName">CloseButtonStyle</xsl:with-param>
				</xsl:call-template>

				<!--ImageGallery_CounterContainer_Background-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">ImageGallery_CounterContainer_Background</xsl:with-param>
					<xsl:with-param name="brushName">CounterContainerBackground</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--ImageGallery_CounterContainer_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId">ImageGallery_CounterContainer_CornerRadius</xsl:with-param>
					<xsl:with-param name="cornerRadiusNode" select="CounterContainerCornerRadius" />
				</xsl:call-template>

				<!--ImageGallery_CounterContainer_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">ImageGallery_CounterContainer_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="CounterContainerMargin" />
				</xsl:call-template>

				<!--ImageGallery_CounterContainer_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">ImageGallery_CounterContainer_Padding</xsl:with-param>
					<xsl:with-param name="paddingNode" select="CounterContainerPadding" />
				</xsl:call-template>

				<!--ImageGallery_CounterTextStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">ImageGallery_CounterTextStyle</xsl:with-param>
					<xsl:with-param name="resourceName">CounterTextStyle</xsl:with-param>
				</xsl:call-template>

				<!--ImageGallery_LeftButton_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">ImageGallery_LeftButton_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="LeftButtonMargin" />
				</xsl:call-template>

				<!--ImageGallery_LeftButtonStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">ImageGallery_LeftButtonStyle</xsl:with-param>
					<xsl:with-param name="resourceName">LeftButtonStyle</xsl:with-param>
				</xsl:call-template>

				<!--ImageGallery_RightButton_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">ImageGallery_RightButton_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="RightButtonMargin" />
				</xsl:call-template>

				<!--ImageGallery_RightButtonStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">ImageGallery_RightButtonStyle</xsl:with-param>
					<xsl:with-param name="resourceName">RightButtonStyle</xsl:with-param>
				</xsl:call-template>
				
			</xsl:with-param>

		</xsl:call-template>

	</xsl:template>

</xsl:transform>
