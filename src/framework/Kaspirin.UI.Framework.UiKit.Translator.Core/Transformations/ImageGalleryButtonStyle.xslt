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

	<xsl:template match="ImageGalleryButtons">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">ImageGalleryButtons</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="ImageGalleryButton">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:ImageGalleryButton</xsl:with-param>
			<xsl:with-param name="basedOn">ImageGalleryButtonUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--ImageGalleryButton_Background-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">ImageGalleryButton_Background</xsl:with-param>
					<xsl:with-param name="brushName">BackgroundNormal</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--ImageGalleryButton_BorderBrush-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">ImageGalleryButton_BorderBrush</xsl:with-param>
					<xsl:with-param name="brushName">BorderNormal</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--ImageGalleryButton_BorderThickness-->
				<xsl:call-template name="generateThicknessSetter">
					<xsl:with-param name="propertyId">ImageGalleryButton_BorderThickness</xsl:with-param>
					<xsl:with-param name="brushName">BorderThicknessNormal</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--ImageGalleryButton_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId">ImageGalleryButton_CornerRadius</xsl:with-param>
				</xsl:call-template>

				<!--ImageGalleryButton_FocusVisualStyle-->
				<xsl:call-template name="generateFocusVisualStyleSetter">
					<xsl:with-param name="propertyId">ImageGalleryButton_FocusVisualStyle</xsl:with-param>
					<xsl:with-param name="scope" select="$id"/>
				</xsl:call-template>

				<!--ImageGalleryButton_Height-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">ImageGalleryButton_Height</xsl:with-param>
					<xsl:with-param name="propertyValue" select = "@Height" />
				</xsl:call-template>

				<!--ImageGalleryButton_Icon_Foreground-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">ImageGalleryButton_Icon_Foreground</xsl:with-param>
					<xsl:with-param name="brushName">IconForegroundNormal</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--ImageGalleryButton_Width-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">ImageGalleryButton_Width</xsl:with-param>
					<xsl:with-param name="propertyValue" select = "@Width" />
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Disabled trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ImageGalleryButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ImageGalleryButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--ImageGalleryButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ImageGalleryButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--ImageGalleryButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">ImageGalleryButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="brushName">BorderThicknessDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--ImageGalleryButton_Icon_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ImageGalleryButton_Icon_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">IconForegroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Hover trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Hover</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ImageGalleryButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ImageGalleryButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--ImageGalleryButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ImageGalleryButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--ImageGalleryButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">ImageGalleryButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="brushName">BorderThicknessHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--ImageGalleryButton_Icon_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ImageGalleryButton_Icon_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">IconForegroundHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Pressed trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ImageGalleryButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ImageGalleryButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--ImageGalleryButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ImageGalleryButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--ImageGalleryButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">ImageGalleryButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="brushName">BorderThicknessPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--ImageGalleryButton_Icon_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ImageGalleryButton_Icon_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">IconForegroundPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>

	</xsl:template>

</xsl:transform>
