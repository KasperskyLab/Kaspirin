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

	<xsl:template match="SsoButtons">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">SsoButtons</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="SsoButton">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:SsoButton</xsl:with-param>
			<xsl:with-param name="basedOn">SsoButtonUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">

					<!--TextStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleIdNormal"/>
						<xsl:with-param name="key">TextStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundNormal</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleDisabled-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleIdDisabled"/>
						<xsl:with-param name="key">TextStyleDisabled</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundDisabled</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleHover-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleIdHover"/>
						<xsl:with-param name="key">TextStyleHover</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundHover</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStylePressed-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleIdPressed"/>
						<xsl:with-param name="key">TextStylePressed</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundPressed</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

				</xsl:element>

				<!--Button_Container_Background-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">Button_Container_Background</xsl:with-param>
					<xsl:with-param name="brushName">BackgroundNormal</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--Button_Container_BorderBrush-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">Button_Container_BorderBrush</xsl:with-param>
					<xsl:with-param name="brushName">BorderNormal</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--Button_Container_BorderThickness-->
				<xsl:call-template name="generateThicknessSetter">
					<xsl:with-param name="propertyId">Button_Container_BorderThickness</xsl:with-param>
					<xsl:with-param name="thicknessNode" select="BorderThicknessNormal" />
				</xsl:call-template>

				<!--Button_Container_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId">Button_Container_CornerRadius</xsl:with-param>
				</xsl:call-template>

				<!--Button_Container_Shadow-->
				<xsl:call-template name="generateShadowSetter">
					<xsl:with-param name="propertyId">Button_Container_Shadow</xsl:with-param>
					<xsl:with-param name="shadowNode" select="ShadowNormal"/>
					<xsl:with-param name="scopeName" select="$id"/>
				</xsl:call-template>

				<!--Button_Icon_Foreground-->
				<xsl:if test="IconForegroundNormal">
					<xsl:call-template name="generateBrushSetter">
						<xsl:with-param name="propertyId">Button_Icon_Foreground</xsl:with-param>
						<xsl:with-param name="brushName">IconForegroundNormal</xsl:with-param>
						<xsl:with-param name="scopeName" select="$id" />
					</xsl:call-template>
				</xsl:if>

				<!--Button_Icon_Opacity-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">Button_Icon_Opacity</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@IconOpacityNormal" />
				</xsl:call-template>

				<!--Button_Icon_MarginLeft-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">Button_Icon_MarginLeft</xsl:with-param>
					<xsl:with-param name="marginNode" select="IconMargin" />
				</xsl:call-template>

				<!--Button_FocusVisualStyle-->
				<xsl:call-template name="generateFocusVisualStyleSetter">
					<xsl:with-param name="propertyId">Button_FocusVisualStyle</xsl:with-param>
					<xsl:with-param name="scope" select="$id"/>
				</xsl:call-template>

				<!--Button_Height-->
				<xsl:call-template name="generateHeightSetter">
					<xsl:with-param name="propertyId">Button_Height</xsl:with-param>
				</xsl:call-template>

				<!--Button_Opacity-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">Button_Opacity</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@OpacityNormal" />
				</xsl:call-template>

				<!--Button_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">Button_Padding</xsl:with-param>
				</xsl:call-template>

				<!--Button_TextStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">Button_TextStyle</xsl:with-param>
					<xsl:with-param name="resourceName">TextStyle</xsl:with-param>
				</xsl:call-template>

				<!--Button_TextStyleDisabled-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">Button_TextStyleDisabled</xsl:with-param>
					<xsl:with-param name="resourceName">TextStyleDisabled</xsl:with-param>
				</xsl:call-template>

				<!--Button_TextStyleHover-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">Button_TextStyleHover</xsl:with-param>
					<xsl:with-param name="resourceName">TextStyleHover</xsl:with-param>
				</xsl:call-template>

				<!--Button_TextStylePressed-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">Button_TextStylePressed</xsl:with-param>
					<xsl:with-param name="resourceName">TextStylePressed</xsl:with-param>
				</xsl:call-template>

				<!--SsoButton_Icon_Name-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">SsoButton_Icon_Name</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@IconName" />
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Disabled trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Button_Container_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Button_Container_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Button_Container_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Button_Container_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Button_Container_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Button_Container_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessDisabled" />
						</xsl:call-template>

						<!--Button_Container_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">Button_Container_Shadow</xsl:with-param>
							<xsl:with-param name="shadowNode" select="ShadowDisabled"/>
							<xsl:with-param name="scope" select="$id"/>
						</xsl:call-template>

						<!--Button_Icon_Foreground-->
						<xsl:if test="IconForegroundDisabled">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Button_Icon_Foreground</xsl:with-param>
								<xsl:with-param name="brushName">IconForegroundDisabled</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Button_Icon_Opacity-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Button_Icon_Opacity</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@IconOpacityDisabled" />
						</xsl:call-template>

						<!--Button_Opacity-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Button_Opacity</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@OpacityDisabled" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Hover trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Hover</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Button_Container_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Button_Container_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Button_Container_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Button_Container_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Button_Container_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Button_Container_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessHover" />
						</xsl:call-template>

						<!--Button_Container_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">Button_Container_Shadow</xsl:with-param>
							<xsl:with-param name="shadowNode" select="ShadowHover"/>
							<xsl:with-param name="scope" select="$id"/>
						</xsl:call-template>

						<!--Button_Icon_Foreground-->
						<xsl:if test="IconForegroundHover">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Button_Icon_Foreground</xsl:with-param>
								<xsl:with-param name="brushName">IconForegroundHover</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Button_Icon_Opacity-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Button_Icon_Opacity</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@IconOpacityHover" />
						</xsl:call-template>

						<!--Button_Opacity-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Button_Opacity</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@OpacityHover" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Pressed trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Button_Container_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Button_Container_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Button_Container_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Button_Container_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Button_Container_BorderThickness -->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Button_Container_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPressed" />
						</xsl:call-template>

						<!--Button_Container_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">Button_Container_Shadow</xsl:with-param>
							<xsl:with-param name="shadowNode" select="ShadowPressed"/>
							<xsl:with-param name="scope" select="$id"/>
						</xsl:call-template>

						<!--Button_Icon_Foreground-->
						<xsl:if test="IconForegroundPressed">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Button_Icon_Foreground</xsl:with-param>
								<xsl:with-param name="brushName">IconForegroundPressed</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Button_Icon_Opacity-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Button_Icon_Opacity</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@IconOpacityPressed" />
						</xsl:call-template>

						<!--Button_Opacity-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Button_Opacity</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@OpacityPressed" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Empty Content trigger]-->
				<xsl:if test="IconMarginCenterLocation">
					<xsl:call-template name="generateTrigger">
						<xsl:with-param name="propertyName">Content</xsl:with-param>
						<xsl:with-param name="propertyValue">{x:Null}</xsl:with-param>
						<xsl:with-param name="setters">

							<!--Button_Padding-->
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">Button_Padding</xsl:with-param>
								<xsl:with-param name="propertyValue">0</xsl:with-param>
							</xsl:call-template>

							<!--Button_Icon_MarginLeft-->
							<xsl:call-template name="generateMarginSetter">
								<xsl:with-param name="propertyId">Button_Icon_MarginLeft</xsl:with-param>
								<xsl:with-param name="marginNode" select="IconMarginCenterLocation" />
							</xsl:call-template>

							<!--Button_Icon_MarginRight-->
							<xsl:call-template name="generateMarginSetter">
								<xsl:with-param name="propertyId">Button_Icon_MarginRight</xsl:with-param>
								<xsl:with-param name="marginNode" select="IconMarginCenterLocation" />
							</xsl:call-template>

						</xsl:with-param>
					</xsl:call-template>
				</xsl:if>

			</xsl:with-param>
		</xsl:call-template>

	</xsl:template>

</xsl:transform>
