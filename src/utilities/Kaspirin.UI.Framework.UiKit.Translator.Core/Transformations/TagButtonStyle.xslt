<!--
    Copyright © 2026 AO Kaspersky Lab.

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

	<xsl:template match="TagButtons">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">TagButtons</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="TagButton">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:TagButton</xsl:with-param>
			<xsl:with-param name="basedOn">TagButtonUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">

					<!--TextStyleEmerald-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleEmerald</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundEmerald</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleEmeraldDisabled-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleEmeraldDisabled</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundEmeraldDisabled</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleGrass-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleGrass</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundGrass</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleGrassDisabled-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleGrassDisabled</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundGrassDisabled</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleMarina-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleMarina</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundMarina</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleMarinaDisabled-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleMarinaDisabled</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundMarinaDisabled</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleMarengo-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleMarengo</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundMarengo</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleMarengoDisabled-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleMarengoDisabled</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundMarengoDisabled</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleNeutral-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleNeutral</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundNeutral</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleNeutralDisabled-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleNeutralDisabled</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundNeutralDisabled</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleOrange-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleOrange</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundOrange</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleOrangeDisabled-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleOrangeDisabled</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundOrangeDisabled</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStylePurple-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStylePurple</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundPurple</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStylePurpleDisabled-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStylePurpleDisabled</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundPurpleDisabled</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleRed-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleRed</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundRed</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleRedDisabled-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleRedDisabled</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundRedDisabled</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleViolet-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleViolet</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundViolet</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleVioletDisabled-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleVioletDisabled</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundVioletDisabled</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleYellow-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleYellow</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundYellow</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleYellowDisabled-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleYellowDisabled</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundYellowDisabled</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

				</xsl:element>

				<!--TagButton_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId">TagButton_CornerRadius</xsl:with-param>
				</xsl:call-template>

				<!--TagButton_FocusVisualStyle-->
				<xsl:call-template name="generateFocusVisualStyleSetter">
					<xsl:with-param name="propertyId">TagButton_FocusVisualStyle</xsl:with-param>
					<xsl:with-param name="scope" select="$id"/>
				</xsl:call-template>

				<!--TagButton_Height-->
				<xsl:call-template name="generateHeightSetter">
					<xsl:with-param name="propertyId">TagButton_Height</xsl:with-param>
				</xsl:call-template>

				<!--TagButton_Icon_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">TagButton_Icon_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="IconMargin" />
				</xsl:call-template>

				<!--TagButton_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">TagButton_Padding</xsl:with-param>
				</xsl:call-template>

				<!--TagButton_TopLayer_Background-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">TagButton_TopLayer_Background</xsl:with-param>
					<xsl:with-param name="propertyValue">Transparent</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Emerald trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Emerald</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundEmerald</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorEmerald</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessEmerald" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushEmerald</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_TextStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyle</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleEmerald</xsl:with-param>
						</xsl:call-template>

						<!--TagButton_TextStyleDisabled-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyleDisabled</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleEmeraldDisabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Grass trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Grass</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundGrass</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorGrass</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessGrass" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushGrass</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_TextStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyle</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleGrass</xsl:with-param>
						</xsl:call-template>

						<!--TagButton_TextStyleDisabled-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyleDisabled</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleGrassDisabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Marina trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Marina</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundMarina</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorMarina</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessMarina" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushMarina</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_TextStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyle</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleMarina</xsl:with-param>
						</xsl:call-template>

						<!--TagButton_TextStyleDisabled-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyleDisabled</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleMarinaDisabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Marengo trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Marengo</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundMarengo</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorMarengo</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessMarengo" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushMarengo</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_TextStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyle</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleMarengo</xsl:with-param>
						</xsl:call-template>

						<!--TagButton_TextStyleDisabled-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyleDisabled</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleMarengoDisabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Neutral trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Neutral</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundNeutral</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorNeutral</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessNeutral" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushNeutral</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_TextStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyle</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleNeutral</xsl:with-param>
						</xsl:call-template>

						<!--TagButton_TextStyleDisabled-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyleDisabled</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleNeutralDisabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Orange trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Orange</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundOrange</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorOrange</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessOrange" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushOrange</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_TextStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyle</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleOrange</xsl:with-param>
						</xsl:call-template>

						<!--TagButton_TextStyleDisabled-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyleDisabled</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleOrangeDisabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Purple trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Purple</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPurple</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorPurple</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPurple" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushPurple</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_TextStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyle</xsl:with-param>
							<xsl:with-param name="resourceName">TextStylePurple</xsl:with-param>
						</xsl:call-template>

						<!--TagButton_TextStyleDisabled-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyleDisabled</xsl:with-param>
							<xsl:with-param name="resourceName">TextStylePurpleDisabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Red trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Red</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundRed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorRed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessRed" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushRed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_TextStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyle</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleRed</xsl:with-param>
						</xsl:call-template>

						<!--TagButton_TextStyleDisabled-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyleDisabled</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleRedDisabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Violet trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Violet</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundViolet</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorViolet</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessViolet" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushViolet</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_TextStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyle</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleViolet</xsl:with-param>
						</xsl:call-template>

						<!--TagButton_TextStyleDisabled-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyleDisabled</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleVioletDisabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Yellow trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Yellow</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundYellow</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorYellow</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessYellow" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushYellow</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_TextStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyle</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleYellow</xsl:with-param>
						</xsl:call-template>

						<!--TagButton_TextStyleDisabled-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">TagButton_TextStyleDisabled</xsl:with-param>
							<xsl:with-param name="resourceName">TextStyleYellowDisabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>


				<!--[Emerald Disabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--Color == Emerald-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Color</xsl:with-param>
							<xsl:with-param name="propertyValue">Emerald</xsl:with-param>
						</xsl:call-template>

						<!--State == Disabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundEmeraldDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorEmeraldDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessEmeraldDisabled" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushEmeraldDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Grass Disabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--Color == Grass-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Color</xsl:with-param>
							<xsl:with-param name="propertyValue">Grass</xsl:with-param>
						</xsl:call-template>

						<!--State == Disabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundGrassDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorGrassDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessGrassDisabled" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushGrassDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Marina Disabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--Color == Marina-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Color</xsl:with-param>
							<xsl:with-param name="propertyValue">Marina</xsl:with-param>
						</xsl:call-template>

						<!--State == Disabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundMarinaDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorMarinaDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessMarinaDisabled" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushMarinaDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Marengo Disabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--Color == Marengo-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Color</xsl:with-param>
							<xsl:with-param name="propertyValue">Marengo</xsl:with-param>
						</xsl:call-template>

						<!--State == Disabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundMarengoDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorMarengoDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessMarengoDisabled" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushMarengoDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Neutral Disabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--Color == Neutral-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Color</xsl:with-param>
							<xsl:with-param name="propertyValue">Neutral</xsl:with-param>
						</xsl:call-template>

						<!--State == Disabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundNeutralDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorNeutralDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessNeutralDisabled" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushNeutralDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Orange Disabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--Color == Orange-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Color</xsl:with-param>
							<xsl:with-param name="propertyValue">Orange</xsl:with-param>
						</xsl:call-template>

						<!--State == Disabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundOrangeDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorOrangeDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessOrangeDisabled" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushOrangeDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Purple Disabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--Color == Purple-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Color</xsl:with-param>
							<xsl:with-param name="propertyValue">Purple</xsl:with-param>
						</xsl:call-template>

						<!--State == Disabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPurpleDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorPurpleDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPurpleDisabled" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushPurpleDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Red Disabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--Color == Red-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Color</xsl:with-param>
							<xsl:with-param name="propertyValue">Red</xsl:with-param>
						</xsl:call-template>

						<!--State == Disabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundRedDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorRedDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessRedDisabled" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushRedDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Violet Disabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--Color == Violet-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Color</xsl:with-param>
							<xsl:with-param name="propertyValue">Violet</xsl:with-param>
						</xsl:call-template>

						<!--State == Disabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundVioletDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorVioletDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessVioletDisabled" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushVioletDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Yellow Disabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--Color == Yellow-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Color</xsl:with-param>
							<xsl:with-param name="propertyValue">Yellow</xsl:with-param>
						</xsl:call-template>

						<!--State == Disabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--TagButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundYellowDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorYellowDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--TagButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">TagButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessYellowDisabled" />
						</xsl:call-template>

						<!--TagButton_Icon_Color-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_Icon_Color</xsl:with-param>
							<xsl:with-param name="brushName">IconBrushYellowDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>


				<!--[Hover trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Hover</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TagButton_TopLayer_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_TopLayer_Background</xsl:with-param>
							<xsl:with-param name="brushName">TopLayerHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Pressed trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TagButton_TopLayer_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">TagButton_TopLayer_Background</xsl:with-param>
							<xsl:with-param name="brushName">TopLayerPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

</xsl:transform>