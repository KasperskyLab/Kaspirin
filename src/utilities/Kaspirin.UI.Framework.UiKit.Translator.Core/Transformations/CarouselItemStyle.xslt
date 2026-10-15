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

	<xsl:template match="CarouselItems">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">CarouselItems</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="CarouselItem">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:variable name="targetType">visuals:CarouselItem</xsl:variable>
		<xsl:variable name="basedOn">CarouselItemUniversal</xsl:variable>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType" select="$targetType" />
			<xsl:with-param name="basedOn" select="$basedOn" />
			<xsl:with-param name="key" select="$id" />
			<xsl:with-param name="setters">

				<!--CarouselItem_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId">CarouselItem_CornerRadius</xsl:with-param>
				</xsl:call-template>

				<!--CarouselItem_Height-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">CarouselItem_Height</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@Height" />
				</xsl:call-template>

				<!--CarouselItem_Width-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">CarouselItem_Width</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@Width" />
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[State Normal trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
					<xsl:with-param name="propertyValue">Normal</xsl:with-param>
					<xsl:with-param name="setters">

						<!--CarouselItem_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">CarouselItem_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--CarouselItem_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">CarouselItem_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--CarouselItem_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">CarouselItem_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessNormal" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State Hover trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
					<xsl:with-param name="propertyValue">Hover</xsl:with-param>
					<xsl:with-param name="setters">

						<!--CarouselItem_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">CarouselItem_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--CarouselItem_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">CarouselItem_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--CarouselItem_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">CarouselItem_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessHover" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State Disabled trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
					<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
					<xsl:with-param name="setters">

						<!--CarouselItem_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">CarouselItem_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--CarouselItem_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">CarouselItem_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--CarouselItem_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">CarouselItem_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessDisabled" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State SelectedNormal trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
					<xsl:with-param name="propertyValue">SelectedNormal</xsl:with-param>
					<xsl:with-param name="setters">

						<!--CarouselItem_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">CarouselItem_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundSelected</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--CarouselItem_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">CarouselItem_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorSelected</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--CarouselItem_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">CarouselItem_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessSelected" />
						</xsl:call-template>

						<!--CarouselItem_Height-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">CarouselItem_Height</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@HeightSelected" />
						</xsl:call-template>

						<!--CarouselItem_Width-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">CarouselItem_Width</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@WidthSelected" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State SelectedDisabled trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
					<xsl:with-param name="propertyValue">SelectedDisabled</xsl:with-param>
					<xsl:with-param name="setters">

						<!--CarouselItem_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">CarouselItem_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--CarouselItem_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">CarouselItem_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--CarouselItem_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">CarouselItem_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessDisabled" />
						</xsl:call-template>

						<!--CarouselItem_Height-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">CarouselItem_Height</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@HeightSelected" />
						</xsl:call-template>

						<!--CarouselItem_Width-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">CarouselItem_Width</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@WidthSelected" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>

		<!--Also generate Carousel style for every CarouselItem style.-->

		<xsl:variable name="carouselId">
			<xsl:call-template name="replace">
				<xsl:with-param name="value" select="$id"/>
				<xsl:with-param name="old">Item</xsl:with-param>
				<xsl:with-param name="new"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$carouselId"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:Carousel</xsl:with-param>
			<xsl:with-param name="basedOn">CarouselUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$carouselId"/>

			<xsl:with-param name="setters">

				<!--ItemContainerStyle-->
				<xsl:call-template name="generateSetterViaAttribute">
					<xsl:with-param name="propertyName">ItemContainerStyle</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key" select="$id"/>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>

	</xsl:template>
</xsl:transform>