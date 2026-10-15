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

	<xsl:template match="DateTimeInputs">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">DateTimeInputs</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="DateTimeInput">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:DateTimeInput</xsl:with-param>
			<xsl:with-param name="basedOn">DateTimeInputUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">

					<!--PopupDecoratorStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">visuals:DateTimePopupDecorator</xsl:with-param>
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

				<!--DateTimeInput_ActionDate_IconName-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">DateTimeInput_ActionDate_IconName</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@ActionButtonDateIconName" />
				</xsl:call-template>

				<!--DateTimeInput_ActionTime_IconName-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">DateTimeInput_ActionTime_IconName</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@ActionButtonTimeIconName" />
				</xsl:call-template>

				<!--DateTimeInput_PopupDecoratorStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">DateTimeInput_PopupDecoratorStyle</xsl:with-param>
					<xsl:with-param name="resourceName">PopupDecoratorStyle</xsl:with-param>
				</xsl:call-template>

				<!--DateTimeInput_PopupPresenterStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">DateTimeInput_PopupPresenterStyle</xsl:with-param>
					<xsl:with-param name="resourceName" select="@DateTimePopupPresenterStyleId" />
				</xsl:call-template>

			</xsl:with-param>

		</xsl:call-template>

	</xsl:template>

</xsl:transform>
