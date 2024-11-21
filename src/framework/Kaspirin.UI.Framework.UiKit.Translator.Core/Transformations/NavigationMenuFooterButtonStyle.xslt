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

	<xsl:template match="NavigationMenuFooterButtons">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">NavigationMenuFooterButtons</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="NavigationMenuFooterButton">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:NavigationMenuFooterButton</xsl:with-param>
			<xsl:with-param name="basedOn">NavigationMenuFooterButtonUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--NavigationMenuFooterButton_Badge_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">NavigationMenuFooterButton_Badge_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="BadgeMargin" />
				</xsl:call-template>

				<!--NavigationMenuFooterButton_Badge_Style-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">NavigationMenuFooterButton_Badge_Style</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key" select="@BadgeStyleId" />
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--NavigationMenuFooterButton_Container_Background-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">NavigationMenuFooterButton_Container_BackgroundBrush</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateResExtension">
							<xsl:with-param name="key">BackgroundNormal</xsl:with-param>
							<xsl:with-param name="scope" select="$id" />
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--NavigationMenuFooterButton_Container_BorderBrush-->
				<xsl:if test="BorderColorNormal">
					<xsl:call-template name="generateUiKitSetterViaAttribute">
						<xsl:with-param name="propertyId">NavigationMenuFooterButton_Container_BorderBrush</xsl:with-param>
						<xsl:with-param name="propertyValue">
							<xsl:call-template name="generateResExtension">
								<xsl:with-param name="key">BorderNormal</xsl:with-param>
								<xsl:with-param name="scope" select="$id" />
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>
				</xsl:if>

				<!--NavigationMenuFooterButton_Container_BorderThickness-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">NavigationMenuFooterButton_Container_BorderThickness</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateThickness">
							<xsl:with-param name="node" select="BorderThicknessNormal" />
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--NavigationMenuFooterButton_Container_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId">NavigationMenuFooterButton_Container_CornerRadius</xsl:with-param>
				</xsl:call-template>

				<!--NavigationMenuFooterButton_FocusVisualStyle-->
				<xsl:call-template name="generateFocusVisualStyleSetter">
					<xsl:with-param name="propertyId">NavigationMenuFooterButton_FocusVisualStyle</xsl:with-param>
					<xsl:with-param name="scope" select="$id"/>
				</xsl:call-template>

				<!--NavigationMenuFooterButton_Height-->
				<xsl:call-template name="generateHeightSetter">
					<xsl:with-param name="propertyId">NavigationMenuFooterButton_Height</xsl:with-param>
				</xsl:call-template>

				<!--NavigationMenuFooterButton_Icon_Foreground-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">NavigationMenuFooterButton_Icon_Brush</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateResExtension">
							<xsl:with-param name="key">IconForegroundNormal</xsl:with-param>
							<xsl:with-param name="scope" select="$id" />
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--NavigationMenuFooterButton_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">NavigationMenuFooterButton_Padding</xsl:with-param>
				</xsl:call-template>

				<!--NavigationMenuFooterButton_Width-->
				<xsl:call-template name="generateWidthSetter">
					<xsl:with-param name="propertyId">NavigationMenuFooterButton_Width</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Hover trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Hover</xsl:with-param>
					<xsl:with-param name="setters">

						<!--NavigationMenuFooterButton_Container_Background-->
						<xsl:if test="BackgroundHover">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">NavigationMenuFooterButton_Container_BackgroundBrush</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">BackgroundHover</xsl:with-param>
										<xsl:with-param name="scope" select="$id" />
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:if>

						<!--NavigationMenuFooterButton_Container_BorderBrush-->
						<xsl:if test="BorderColorHover">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">NavigationMenuFooterButton_Container_BorderBrush</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">BorderHover</xsl:with-param>
										<xsl:with-param name="scope" select="$id" />
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:if>

						<!--NavigationMenuFooterButton_Container_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">NavigationMenuFooterButton_Container_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessHover" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--NavigationMenuFooterButton_Icon_Foreground-->
						<xsl:if test="IconForegroundHover">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">NavigationMenuFooterButton_Icon_Brush</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">IconForegroundHover</xsl:with-param>
										<xsl:with-param name="scope" select="$id" />
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:if>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Pressed trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
					<xsl:with-param name="setters">

						<!--NavigationMenuFooterButton_Container_Background-->
						<xsl:if test="BackgroundPressed">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">NavigationMenuFooterButton_Container_BackgroundBrush</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">BackgroundPressed</xsl:with-param>
										<xsl:with-param name="scope" select="$id" />
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:if>

						<!--NavigationMenuFooterButton_Container_BorderBrush-->
						<xsl:if test="BorderColorPressed">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">NavigationMenuFooterButton_Container_BorderBrush</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">BorderPressed</xsl:with-param>
										<xsl:with-param name="scope" select="$id" />
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:if>

						<!--NavigationMenuFooterButton_Container_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">NavigationMenuFooterButton_Container_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessPressed" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--NavigationMenuFooterButton_Icon_Foreground-->
						<xsl:if test="IconForegroundPressed">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">NavigationMenuFooterButton_Icon_Brush</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">IconForegroundPressed</xsl:with-param>
										<xsl:with-param name="scope" select="$id" />
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:if>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

</xsl:transform>
