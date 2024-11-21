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

	<xsl:template match="Searches">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">Searches</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="Search">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:element name="Style">
			<xsl:attribute name="TargetType">{x:Type visuals:Search}</xsl:attribute>
			<xsl:attribute name="BasedOn">{StaticResource SearchUniversal}</xsl:attribute>
			<xsl:attribute name="x:Key">
				<xsl:value-of select="$id"/>
			</xsl:attribute>

			<!--Style.Resources-->
			<xsl:element name ="Style.Resources">

				<!--Search_TextInputStyle-->
				<xsl:call-template name="generateStyle">
					<xsl:with-param name="targetType">visuals:TextInput</xsl:with-param>
					<xsl:with-param name="key">TextInputStyle</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Search_TextInput_CaretBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Search_TextInput_CaretBrush</xsl:with-param>
							<xsl:with-param name="brushName">CaretBrush</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Search_TextInput_Padding-->
						<xsl:call-template name="generatePaddingSetter">
							<xsl:with-param name="propertyId">Search_TextInput_Padding</xsl:with-param>
						</xsl:call-template>

						<!--Search_TextInput_PlaceholderForeground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Search_TextInput_PlaceholderForeground</xsl:with-param>
							<xsl:with-param name="brushName">PlaceholderForeground</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Search_TextInput_PlaceholderStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">Search_TextInput_PlaceholderStyle</xsl:with-param>
							<xsl:with-param name="resourceName" select="@PlaceholderStyleId" />
						</xsl:call-template>

						<!--Search_TextInput_TextForeground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Search_TextInput_TextForeground</xsl:with-param>
							<xsl:with-param name="brushName">TextForeground</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Search_TextInput_TextStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">Search_TextInput_TextStyle</xsl:with-param>
							<xsl:with-param name="resourceName" select="@TextStyleId" />
						</xsl:call-template>

						<xsl:element name="Style.Triggers">

							<!--[Disabled trigger]-->
							<xsl:call-template name="generateTrigger">
								<xsl:with-param name="propertyName">visuals:StateService.TextInputState</xsl:with-param>
								<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
								<xsl:with-param name="setters">

									<!--Search_TextInput_PlaceholderForeground-->
									<xsl:call-template name="generateBrushSetter">
										<xsl:with-param name="propertyId">Search_TextInput_PlaceholderForeground</xsl:with-param>
										<xsl:with-param name="brushName">PlaceholderForegroundDisabled</xsl:with-param>
										<xsl:with-param name="scopeName" select="$id" />
									</xsl:call-template>

									<!--Search_TextInput_TextForeground-->
									<xsl:call-template name="generateBrushSetter">
										<xsl:with-param name="propertyId">Search_TextInput_TextForeground</xsl:with-param>
										<xsl:with-param name="brushName">TextForegroundDisabled</xsl:with-param>
										<xsl:with-param name="scopeName" select="$id" />
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

						</xsl:element>

					</xsl:with-param>

				</xsl:call-template>

			</xsl:element>

			<!--Search_Background-->
			<xsl:call-template name="generateBrushSetter">
				<xsl:with-param name="propertyId">Search_Background</xsl:with-param>
				<xsl:with-param name="brushName">BackgroundNormal</xsl:with-param>
				<xsl:with-param name="scopeName" select="$id" />
			</xsl:call-template>

			<!--Search_BorderBrush-->
			<xsl:call-template name="generateBrushSetter">
				<xsl:with-param name="propertyId">Search_BorderBrush</xsl:with-param>
				<xsl:with-param name="brushName">BorderColorNormal</xsl:with-param>
				<xsl:with-param name="scopeName" select="$id" />
			</xsl:call-template>

			<!--Search_BorderThickness-->
			<xsl:call-template name="generateThicknessSetter">
				<xsl:with-param name="propertyId">Search_BorderThickness</xsl:with-param>
				<xsl:with-param name="thicknessNode" select="BorderThicknessNormal" />
			</xsl:call-template>

			<!--Search_ClearButtonIcon-->
			<xsl:call-template name="generateUiKitSetterViaAttribute">
				<xsl:with-param name="propertyId">Search_ClearButtonIcon</xsl:with-param>
				<xsl:with-param name="propertyValue" select="@ClearButtonIconName" />
			</xsl:call-template>

			<!--Search_ClearButtonMargin-->
			<xsl:call-template name="generateMarginSetter">
				<xsl:with-param name="propertyId">Search_ClearButtonMargin</xsl:with-param>
				<xsl:with-param name="marginNode" select="ClearButtonMargin" />
			</xsl:call-template>

			<!--Search_ClearButtonStyle-->
			<xsl:call-template name="generateStaticResourceSetter">
				<xsl:with-param name="propertyId">Search_ClearButtonStyle</xsl:with-param>
				<xsl:with-param name="resourceName" select="@ClearButtonStyleId" />
			</xsl:call-template>

			<!--Search_CornerRadius-->
			<xsl:call-template name="generateCornerRadiusSetter">
				<xsl:with-param name="propertyId">Search_CornerRadius</xsl:with-param>
			</xsl:call-template>

			<!--Search_Height-->
			<xsl:call-template name="generateHeightSetter">
				<xsl:with-param name="propertyId">Search_Height</xsl:with-param>
			</xsl:call-template>

			<!--Search_Icon-->
			<xsl:call-template name="generateUiKitSetterViaAttribute">
				<xsl:with-param name="propertyId">Search_Icon</xsl:with-param>
				<xsl:with-param name="propertyValue" select="@SearchIconName" />
			</xsl:call-template>

			<!--Search_IconForeground-->
			<xsl:call-template name="generateBrushSetter">
				<xsl:with-param name="propertyId">Search_IconForeground</xsl:with-param>
				<xsl:with-param name="brushName">SearchIconForeground</xsl:with-param>
				<xsl:with-param name="scopeName" select="$id" />
			</xsl:call-template>

			<!--Search_IconMargin-->
			<xsl:call-template name="generateMarginSetter">
				<xsl:with-param name="propertyId">Search_IconMargin</xsl:with-param>
				<xsl:with-param name="marginNode" select="SearchIconMargin" />
			</xsl:call-template>

			<!--Search_InputStyle-->
			<xsl:call-template name="generateStaticResourceSetter">
				<xsl:with-param name="propertyId">Search_TextInputStyle</xsl:with-param>
				<xsl:with-param name="resourceName">TextInputStyle</xsl:with-param>
			</xsl:call-template>

			<!--Search_SpinnerMargin-->
			<xsl:call-template name="generateMarginSetter">
				<xsl:with-param name="propertyId">Search_SpinnerMargin</xsl:with-param>
				<xsl:with-param name="marginNode" select="SpinnerMargin" />
			</xsl:call-template>

			<!--Search_SpinnerStyle-->
			<xsl:call-template name="generateStaticResourceSetter">
				<xsl:with-param name="propertyId">Search_SpinnerStyle</xsl:with-param>
				<xsl:with-param name="resourceName" select="@SpinnerStyleId" />
			</xsl:call-template>

			<xsl:element name="Style.Triggers">

				<!--[Disabled trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.TextInputState</xsl:with-param>
					<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Search_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Search_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Search_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Search_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Search_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Search_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessDisabled" />
						</xsl:call-template>

						<!--Search_IconForeground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Search_IconForeground</xsl:with-param>
							<xsl:with-param name="brushName">SearchIconForegroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Hover trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.TextInputState</xsl:with-param>
					<xsl:with-param name="propertyValue">Hover</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Search_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Search_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Search_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Search_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Search_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Search_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessHover" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Focus trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.TextInputState</xsl:with-param>
					<xsl:with-param name="propertyValue">Focus</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Search_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Search_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundFocus</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Search_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Search_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorFocus</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Search_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Search_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessFocus" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:element>

		</xsl:element>
	</xsl:template>

</xsl:transform>