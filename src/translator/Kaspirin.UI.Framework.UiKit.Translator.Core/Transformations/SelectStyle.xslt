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

	<xsl:template match="Selects">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">Selects</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="Select">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:Select</xsl:with-param>
			<xsl:with-param name="basedOn">SelectUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">

					<!--CaptionStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@CaptionStyleId"/>
						<xsl:with-param name="key">CaptionStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">CaptionForeground</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--LabelStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@LabelStyleId"/>
						<xsl:with-param name="key">LabelStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">LabelForeground</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--PopupDecoratorStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">visuals:SelectPopupDecorator</xsl:with-param>
						<xsl:with-param name="basedOn">PopupDecoratorUniversal</xsl:with-param>
						<xsl:with-param name="key">PopupDecoratorStyle</xsl:with-param>
						<xsl:with-param name="setters">

							<!--PopupDecorator_Background-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">PopupDecorator_Background</xsl:with-param>
								<xsl:with-param name="brushName">PopupDecoratorBackground</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>

							<!--PopupDecorator_BorderBrush-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">PopupDecorator_BorderBrush</xsl:with-param>
								<xsl:with-param name="brushName">PopupDecoratorBorderColor</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>

							<!--PopupDecorator_BorderThickness-->
							<xsl:call-template name="generateThicknessSetter">
								<xsl:with-param name="propertyId">PopupDecorator_BorderThickness</xsl:with-param>
								<xsl:with-param name="thicknessNode" select="PopupDecoratorBorderThickness" />
							</xsl:call-template>

							<!--PopupDecorator_CornerRadius-->
							<xsl:call-template name="generateCornerRadiusSetter">
								<xsl:with-param name="propertyId">PopupDecorator_CornerRadius</xsl:with-param>
								<xsl:with-param name="cornerRadiusNode" select="PopupDecoratorCornerRadius" />
							</xsl:call-template>

							<!--PopupDecorator_Padding-->
							<xsl:call-template name="generatePaddingSetter">
								<xsl:with-param name="propertyId">PopupDecorator_Padding</xsl:with-param>
								<xsl:with-param name="paddingNode" select="PopupDecoratorPadding" />
							</xsl:call-template>

							<!--PopupDecorator_Shadow-->
							<xsl:call-template name="generateShadowSetter">
								<xsl:with-param name="propertyId">PopupDecorator_Shadow</xsl:with-param>
								<xsl:with-param name="shadowNode" select="PopupDecoratorShadow" />
								<xsl:with-param name="scope" select="$id"/>
							</xsl:call-template>

						</xsl:with-param>
					</xsl:call-template>

				</xsl:element>

				<!--Select_Caption_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">Select_Caption_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="CaptionMargin" />
				</xsl:call-template>

				<!--Select_CaptionStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">Select_CaptionStyle</xsl:with-param>
					<xsl:with-param name="resourceName">CaptionStyle</xsl:with-param>
				</xsl:call-template>

				<!--Select_ItemContainerStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">Select_ItemContainerStyle</xsl:with-param>
					<xsl:with-param name="resourceName" select="@SelectItemStyleId" />
				</xsl:call-template>

				<!--Select_Label_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">Select_Label_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="LabelMargin" />
				</xsl:call-template>

				<!--Select_LabelStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">Select_LabelStyle</xsl:with-param>
					<xsl:with-param name="resourceName">LabelStyle</xsl:with-param>
				</xsl:call-template>

				<!--Select_SelectPopupDecoratorStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">Select_PopupDecoratorStyle</xsl:with-param>
					<xsl:with-param name="resourceName">PopupDecoratorStyle</xsl:with-param>
				</xsl:call-template>

				<!--Select_SelectPresenterStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">Select_SelectPresenterStyle</xsl:with-param>
					<xsl:with-param name="resourceName" select="@SelectPresenterStyleId" />
				</xsl:call-template>

				<!--Select_ValidationPopupStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">Select_ValidationPopupStyle</xsl:with-param>
					<xsl:with-param name="resourceName" select="@PopupStyleId" />
				</xsl:call-template>

			</xsl:with-param>

		</xsl:call-template>

	</xsl:template>

</xsl:transform>
