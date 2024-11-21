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

	<xsl:template match="ListMenuItems">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">ListMenuItems</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="ListMenuItem">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:ListMenuItem</xsl:with-param>
			<xsl:with-param name="basedOn">ListMenuItemUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">

					<!--TextStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:if test="ForegroundNormal">
								<xsl:call-template name="generateSetterViaAttribute">
									<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
									<xsl:with-param name="propertyValue">
										<xsl:call-template name="generateResExtension">
											<xsl:with-param name="key">ForegroundNormal</xsl:with-param>
											<xsl:with-param name="scope" select="$id"/>
										</xsl:call-template>
									</xsl:with-param>
								</xsl:call-template>
							</xsl:if>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleDisabled-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleDisabled</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:if test="ForegroundDisabled">
								<xsl:call-template name="generateSetterViaAttribute">
									<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
									<xsl:with-param name="propertyValue">
										<xsl:call-template name="generateResExtension">
											<xsl:with-param name="key">ForegroundDisabled</xsl:with-param>
											<xsl:with-param name="scope" select="$id"/>
										</xsl:call-template>
									</xsl:with-param>
								</xsl:call-template>
							</xsl:if>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleHover-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleHover</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:if test="ForegroundHover">
								<xsl:call-template name="generateSetterViaAttribute">
									<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
									<xsl:with-param name="propertyValue">
										<xsl:call-template name="generateResExtension">
											<xsl:with-param name="key">ForegroundHover</xsl:with-param>
											<xsl:with-param name="scope" select="$id"/>
										</xsl:call-template>
									</xsl:with-param>
								</xsl:call-template>
							</xsl:if>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleSelectedNormal-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleSelectedNormal</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:if test="ForegroundPressed">
								<xsl:call-template name="generateSetterViaAttribute">
									<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
									<xsl:with-param name="propertyValue">
										<xsl:call-template name="generateResExtension">
											<xsl:with-param name="key">ForegroundPressed</xsl:with-param>
											<xsl:with-param name="scope" select="$id"/>
										</xsl:call-template>
									</xsl:with-param>
								</xsl:call-template>
							</xsl:if>
						</xsl:with-param>
					</xsl:call-template>

				</xsl:element>

				<!--ListMenuItem_Background-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">ListMenuItem_Background</xsl:with-param>
					<xsl:with-param name="brushName">BackgroundNormal</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id"/>
				</xsl:call-template>

				<!--ListMenuItem_BorderBrush-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">ListMenuItem_BorderBrush</xsl:with-param>
					<xsl:with-param name="brushName">BorderNormal</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id"/>
				</xsl:call-template>

				<!--ListMenuItem_BorderThickness-->
				<xsl:call-template name="generateThicknessSetter">
					<xsl:with-param name="propertyId">ListMenuItem_BorderThickness</xsl:with-param>
					<xsl:with-param name="thicknessNode" select="BorderThicknessNormal"/>
				</xsl:call-template>

				<!--ListMenuItem_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId">ListMenuItem_CornerRadius</xsl:with-param>
				</xsl:call-template>

				<!--ListMenuItem_FocusVisualStyle-->
				<xsl:call-template name="generateFocusVisualStyleSetter">
					<xsl:with-param name="propertyId">ListMenuItem_FocusVisualStyle</xsl:with-param>
					<xsl:with-param name="scope" select="$id"/>
				</xsl:call-template>

				<!--ListMenuItem_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">ListMenuItem_Padding</xsl:with-param>
				</xsl:call-template>

				<!--ListMenuItem_Icon_Foreground-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">ListMenuItem_Icon_Foreground</xsl:with-param>
					<xsl:with-param name="brushName">IconForegroundNormal</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id"/>
				</xsl:call-template>

				<!--ListMenuItem_Icon_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">ListMenuItem_Icon_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="IconMargin" />
				</xsl:call-template>

				<!--ListMenuItem_TextStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">ListMenuItem_TextStyle</xsl:with-param>
					<xsl:with-param name="resourceName">TextStyle</xsl:with-param>
				</xsl:call-template>

				<!--ListMenuItem_TextStyleDisabled-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">ListMenuItem_TextStyleDisabled</xsl:with-param>
					<xsl:with-param name="resourceName">TextStyleDisabled</xsl:with-param>
				</xsl:call-template>

				<!--ListMenuItem_TextStyleHover-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">ListMenuItem_TextStyleHover</xsl:with-param>
					<xsl:with-param name="resourceName">TextStyleHover</xsl:with-param>
				</xsl:call-template>

				<!--ListMenuItem_TextStyleSelectedNormal-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">ListMenuItem_TextStyleSelectedNormal</xsl:with-param>
					<xsl:with-param name="resourceName">TextStyleSelectedNormal</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Disabled trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
					<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ListMenuItem_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ListMenuItem_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--ListMenuItem_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ListMenuItem_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--ListMenuItem_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">ListMenuItem_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessDisabled"/>
						</xsl:call-template>

						<!--ListMenuItem_Icon_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ListMenuItem_Icon_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">IconForegroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Hover trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
					<xsl:with-param name="propertyValue">Hover</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ListMenuItem_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ListMenuItem_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--ListMenuItem_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ListMenuItem_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--ListMenuItem_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">ListMenuItem_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessHover"/>
						</xsl:call-template>

						<!--ListMenuItem_Icon_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ListMenuItem_Icon_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">IconForegroundHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[SelectedNormal trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
					<xsl:with-param name="propertyValue">SelectedNormal</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ListMenuItem_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ListMenuItem_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--ListMenuItem_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ListMenuItem_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--ListMenuItem_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">ListMenuItem_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPressed"/>
						</xsl:call-template>

						<!--ListMenuItem_Icon_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ListMenuItem_Icon_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">IconForegroundPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>

		<!--Also generate ListMenu style for every ListMenuItem style.-->

		<xsl:variable name="listMenuId">
			<xsl:call-template name="replace">
				<xsl:with-param name="value" select="$id"/>
				<xsl:with-param name="old">Item</xsl:with-param>
				<xsl:with-param name="new"></xsl:with-param>
			</xsl:call-template>
		</xsl:variable>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$listMenuId"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:ListMenu</xsl:with-param>
			<xsl:with-param name="basedOn">ListMenuUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$listMenuId"/>

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