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

	<xsl:template match="CodeInputs">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">CodeInputs</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="CodeInput">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:element name="Style">
			<xsl:attribute name="TargetType">{x:Type visuals:CodeInput}</xsl:attribute>
			<xsl:attribute name="BasedOn">{StaticResource TextInputUniversal}</xsl:attribute>
			<xsl:attribute name="x:Key">
				<xsl:value-of select="$id"/>
			</xsl:attribute>

			<!--Style.Resources-->
			<xsl:element name ="Style.Resources">

				<!--CaptionStyle-->
				<xsl:call-template name="generateStyle">
					<xsl:with-param name="targetType">TextBlock</xsl:with-param>
					<xsl:with-param name="basedOn" select="@CaptionStyleId"/>
					<xsl:with-param name="key">CaptionStyle</xsl:with-param>
					<xsl:with-param name="setters">
						<!--TextElement.Foreground-->
						<xsl:call-template name="generateSetterViaAttribute">
							<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">CaptionForeground</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
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
						<xsl:call-template name="generateSetterViaAttribute">
							<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">LabelForeground</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

			</xsl:element>

			<!--TextInput_Background-->
			<xsl:call-template name="generateBrushSetter">
				<xsl:with-param name="propertyId">TextInput_Background</xsl:with-param>
				<xsl:with-param name="brushName">BackgroundNormal</xsl:with-param>
				<xsl:with-param name="scopeName" select="$id" />
			</xsl:call-template>

			<!--TextInput_BorderBrush-->
			<xsl:call-template name="generateBrushSetter">
				<xsl:with-param name="propertyId">TextInput_BorderBrush</xsl:with-param>
				<xsl:with-param name="brushName">BorderColorNormal</xsl:with-param>
				<xsl:with-param name="scopeName" select="$id" />
			</xsl:call-template>

			<!--TextInput_BorderThickness-->
			<xsl:call-template name="generateThicknessSetter">
				<xsl:with-param name="propertyId">TextInput_BorderThickness</xsl:with-param>
				<xsl:with-param name="thicknessNode" select="BorderThicknessNormal" />
			</xsl:call-template>

			<!--TextInput_CaretBrush-->
			<xsl:call-template name="generateBrushSetter">
				<xsl:with-param name="propertyId">TextInput_CaretBrush</xsl:with-param>
				<xsl:with-param name="brushName">CaretBrush</xsl:with-param>
				<xsl:with-param name="scopeName" select="$id" />
			</xsl:call-template>

			<!--TextInput_CornerRadius-->
			<xsl:call-template name="generateCornerRadiusSetter">
				<xsl:with-param name="propertyId">TextInput_CornerRadius</xsl:with-param>
			</xsl:call-template>

			<!--TextInput_Height-->
			<xsl:call-template name="generateHeightSetter">
				<xsl:with-param name="propertyId">TextInput_Height</xsl:with-param>
			</xsl:call-template>

			<!--TextInput_Padding-->
			<xsl:call-template name="generatePaddingSetter">
				<xsl:with-param name="propertyId">TextInput_Padding</xsl:with-param>
			</xsl:call-template>

			<!--TextInput_Placeholder_Foreground-->
			<xsl:call-template name="generateBrushSetter">
				<xsl:with-param name="propertyId">TextInput_Placeholder_Foreground</xsl:with-param>
				<xsl:with-param name="brushName">PlaceholderForeground</xsl:with-param>
				<xsl:with-param name="scopeName" select="$id" />
			</xsl:call-template>

			<!--TextInput_PlaceholderStyle-->
			<xsl:call-template name="generateStaticResourceSetter">
				<xsl:with-param name="propertyId">TextInput_PlaceholderStyle</xsl:with-param>
				<xsl:with-param name="resourceName" select="@TextRegularStyleId" />
			</xsl:call-template>

			<!--TextInput_CaptionStyle-->
			<xsl:call-template name="generateStaticResourceSetter">
				<xsl:with-param name="propertyId">TextInput_CaptionStyle</xsl:with-param>
				<xsl:with-param name="resourceName">CaptionStyle</xsl:with-param>
			</xsl:call-template>

			<!--TextInput_Caption_Margin-->
			<xsl:call-template name="generateMarginSetter">
				<xsl:with-param name="propertyId">TextInput_Caption_Margin</xsl:with-param>
				<xsl:with-param name="marginNode" select="CaptionMargin" />
			</xsl:call-template>

			<!--TextInput_LabelStyle-->
			<xsl:call-template name="generateStaticResourceSetter">
				<xsl:with-param name="propertyId">TextInput_LabelStyle</xsl:with-param>
				<xsl:with-param name="resourceName">LabelStyle</xsl:with-param>
			</xsl:call-template>

			<!--TextInput_Label_Margin-->
			<xsl:call-template name="generateMarginSetter">
				<xsl:with-param name="propertyId">TextInput_Label_Margin</xsl:with-param>
				<xsl:with-param name="marginNode" select="LabelMargin" />
			</xsl:call-template>

			<!--TextInput_TextForeground-->
			<xsl:call-template name="generateBrushSetter">
				<xsl:with-param name="propertyId">TextInput_TextForeground</xsl:with-param>
				<xsl:with-param name="brushName">TextForeground</xsl:with-param>
				<xsl:with-param name="scopeName" select="$id" />
			</xsl:call-template>

			<!--TextInput_ValidationPopupStyle-->
			<xsl:call-template name="generateStaticResourceSetter">
				<xsl:with-param name="propertyId">TextInput_ValidationPopupStyle</xsl:with-param>
				<xsl:with-param name="resourceName" select="@PopupStyleId"/>
			</xsl:call-template>

			<xsl:element name="Style.Triggers">
				
				<!--[FontMode = Regular]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">FontMode</xsl:with-param>
					<xsl:with-param name="propertyValue">Regular</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TextInput_TextStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TextInput_TextStyle</xsl:with-param>
							<xsl:with-param name="resourceName" select="@TextRegularStyleId" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[FontMode = Monospace]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">FontMode</xsl:with-param>
					<xsl:with-param name="propertyValue">Monospace</xsl:with-param>
					<xsl:with-param name="setters">
						
						<!--TextInput_TextStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TextInput_TextStyle</xsl:with-param>
							<xsl:with-param name="resourceName" select="@TextMonospaceStyleId" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Disabled trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.TextInputState</xsl:with-param>
					<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TextInput_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TextInput_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TextInput_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TextInput_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TextInput_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TextInput_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessDisabled" />
						</xsl:call-template>

						<!--TextInput_TextForeground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TextInput_TextForeground</xsl:with-param>
							<xsl:with-param name="brushName">TextForegroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Hover trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.TextInputState</xsl:with-param>
					<xsl:with-param name="propertyValue">Hover</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TextInput_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TextInput_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TextInput_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TextInput_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TextInput_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TextInput_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessHover" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Focus trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.TextInputState</xsl:with-param>
					<xsl:with-param name="propertyValue">Focus</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TextInput_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TextInput_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundFocus</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TextInput_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TextInput_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorFocus</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TextInput_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TextInput_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessFocus" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[InvalidNormal trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.TextInputState</xsl:with-param>
					<xsl:with-param name="propertyValue">InvalidNormal</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TextInput_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TextInput_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundInvalidNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TextInput_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TextInput_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorInvalidNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TextInput_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TextInput_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessInvalidNormal" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[InvalidHover trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.TextInputState</xsl:with-param>
					<xsl:with-param name="propertyValue">InvalidHover</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TextInput_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TextInput_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundInvalidHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TextInput_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TextInput_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorInvalidHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TextInput_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TextInput_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessInvalidHover" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[InvalidFocus trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.TextInputState</xsl:with-param>
					<xsl:with-param name="propertyValue">InvalidFocus</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TextInput_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TextInput_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundInvalidFocus</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TextInput_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TextInput_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorInvalidFocus</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TextInput_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TextInput_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessInvalidFocus" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[ReadOnly trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.TextInputState</xsl:with-param>
					<xsl:with-param name="propertyValue">ReadOnly</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TextInput_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TextInput_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundReadOnly</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TextInput_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TextInput_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorReadOnly</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TextInput_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TextInput_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessReadOnly" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:element>

		</xsl:element>
	</xsl:template>

</xsl:transform>