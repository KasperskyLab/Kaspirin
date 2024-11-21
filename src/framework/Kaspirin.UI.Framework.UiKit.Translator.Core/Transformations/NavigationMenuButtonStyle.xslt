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

	<xsl:template match="NavigationMenuButtons">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">NavigationMenuButtons</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="NavigationMenuButton">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:NavigationMenuButton</xsl:with-param>
			<xsl:with-param name="basedOn">NavigationMenuButtonUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">
					<!--CaptionTextStyle-->
					<xsl:if test="@TextStyleIdNormal or ForegroundNormal">
						<xsl:call-template name="generateStyle">
							<xsl:with-param name="targetType">TextBlock</xsl:with-param>
							<xsl:with-param name="basedOn" select="@TextStyleIdNormal"/>
							<xsl:with-param name="key">CaptionTextStyle</xsl:with-param>
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
					</xsl:if>

					<!--CaptionTextStyleHover-->
					<xsl:if test="@TextStyleIdHover or ForegroundHover">
						<xsl:call-template name="generateStyle">
							<xsl:with-param name="targetType">TextBlock</xsl:with-param>
							<xsl:with-param name="basedOn" select="@TextStyleIdHover"/>
							<xsl:with-param name="key">CaptionTextStyleHover</xsl:with-param>
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
					</xsl:if>

					<!--CaptionTextStylePressed-->
					<xsl:if test="@TextStyleIdPressed or ForegroundPressed">
						<xsl:call-template name="generateStyle">
							<xsl:with-param name="targetType">TextBlock</xsl:with-param>
							<xsl:with-param name="basedOn" select="@TextStyleIdPressed"/>
							<xsl:with-param name="key">CaptionTextStylePressed</xsl:with-param>
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
					</xsl:if>

					<!--DescriptionTextStyle-->
					<xsl:if test="@TextStyleIdSecondLineNormal or ForegroundSecondLineNormal">
						<xsl:call-template name="generateStyle">
							<xsl:with-param name="targetType">TextBlock</xsl:with-param>
							<xsl:with-param name="basedOn" select="@TextStyleIdSecondLineNormal"/>
							<xsl:with-param name="key">DescriptionTextStyle</xsl:with-param>
							<xsl:with-param name="setters">
								<!--TextElement.Foreground-->
								<xsl:if test="ForegroundSecondLineNormal">
									<xsl:call-template name="generateSetterViaAttribute">
										<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateResExtension">
												<xsl:with-param name="key">ForegroundSecondLineNormal</xsl:with-param>
												<xsl:with-param name="scope" select="$id"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>
								</xsl:if>
							</xsl:with-param>
						</xsl:call-template>
					</xsl:if>

					<!--DescriptionTextStyleHover-->
					<xsl:if test="@TextStyleIdSecondLineHover or ForegroundSecondLineHover">
						<xsl:call-template name="generateStyle">
							<xsl:with-param name="targetType">TextBlock</xsl:with-param>
							<xsl:with-param name="basedOn" select="@TextStyleIdSecondLineHover"/>
							<xsl:with-param name="key">DescriptionTextStyleHover</xsl:with-param>
							<xsl:with-param name="setters">
								<!--TextElement.Foreground-->
							<xsl:if test="ForegroundSecondLineHover">
									<xsl:call-template name="generateSetterViaAttribute">
										<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateResExtension">
												<xsl:with-param name="key">ForegroundSecondLineHover</xsl:with-param>
												<xsl:with-param name="scope" select="$id"/>
												</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>
								</xsl:if>
							</xsl:with-param>
						</xsl:call-template>
					</xsl:if>

					<!--DescriptionTextStylePressed-->
					<xsl:if test="@TextStyleIdSecondLinePressed or ForegroundSecondLinePressed">
						<xsl:call-template name="generateStyle">
							<xsl:with-param name="targetType">TextBlock</xsl:with-param>
							<xsl:with-param name="basedOn" select="@TextStyleIdSecondLinePressed"/>
							<xsl:with-param name="key">DescriptionTextStylePressed</xsl:with-param>
							<xsl:with-param name="setters">
								<!--TextElement.Foreground-->
								<xsl:if test="ForegroundSecondLinePressed">
									<xsl:call-template name="generateSetterViaAttribute">
										<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateResExtension">
												<xsl:with-param name="key">ForegroundSecondLinePressed</xsl:with-param>
												<xsl:with-param name="scope" select="$id"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>
								</xsl:if>
							</xsl:with-param>
						</xsl:call-template>
					</xsl:if>
				</xsl:element>

				<!--NavigationMenuButton_Background-->
				<xsl:if test="Background">
					<xsl:call-template name="generateUiKitSetterViaAttribute">
						<xsl:with-param name="propertyId">NavigationMenuButton_Background</xsl:with-param>
						<xsl:with-param name="propertyValue">
							<xsl:call-template name="generateResExtension">
								<xsl:with-param name="key">Background</xsl:with-param>
								<xsl:with-param name="scope" select="$id" />
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>
				</xsl:if>

				<!--NavigationMenuButton_Badge_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">NavigationMenuButton_Badge_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="BadgeMargin" />
				</xsl:call-template>

				<!--NavigationMenuButton_Badge_Style-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">NavigationMenuButton_Badge_Style</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key" select="@BadgeStyleId" />
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--NavigationMenuButton_CaptionTextStyle-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">NavigationMenuButton_CaptionTextStyle</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">CaptionTextStyle</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--NavigationMenuButton_CaptionTextStyleHover-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">NavigationMenuButton_CaptionTextStyleHover</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">CaptionTextStyleHover</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--NavigationMenuButton_CaptionTextStylePressed-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">NavigationMenuButton_CaptionTextStylePressed</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">CaptionTextStylePressed</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--NavigationMenuButton_Container_Background-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">NavigationMenuButton_Container_Background</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateResExtension">
							<xsl:with-param name="key">ContainerBackgroundNormal</xsl:with-param>
							<xsl:with-param name="scope" select="$id" />
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--NavigationMenuButton_Container_BorderBrush-->
				<xsl:if test="BorderColorNormal">
					<xsl:call-template name="generateUiKitSetterViaAttribute">
						<xsl:with-param name="propertyId">NavigationMenuButton_Container_BorderBrush</xsl:with-param>
						<xsl:with-param name="propertyValue">
							<xsl:call-template name="generateResExtension">
								<xsl:with-param name="key">BorderNormal</xsl:with-param>
								<xsl:with-param name="scope" select="$id" />
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>
				</xsl:if>

				<!--NavigationMenuButton_Container_BorderThickness-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">NavigationMenuButton_Container_BorderThickness</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateThickness">
							<xsl:with-param name="node" select="BorderThicknessNormal" />
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--NavigationMenuButton_Container_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId">NavigationMenuButton_Container_CornerRadius</xsl:with-param>
				</xsl:call-template>

				<!--NavigationMenuButton_Container_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">NavigationMenuButton_Container_Padding</xsl:with-param>
					<xsl:with-param name="paddingNode" select="ContainerPadding"/>
				</xsl:call-template>

				<!--NavigationMenuButton_DescriptionTextStyle-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">NavigationMenuButton_DescriptionTextStyle</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">DescriptionTextStyle</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--NavigationMenuButton_DescriptionTextStyleHover-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">NavigationMenuButton_DescriptionTextStyleHover</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">DescriptionTextStyleHover</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--NavigationMenuButton_DescriptionTextStylePressed-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">NavigationMenuButton_DescriptionTextStylePressed</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">DescriptionTextStylePressed</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--NavigationMenuButton_FocusVisualStyle-->
				<xsl:call-template name="generateFocusVisualStyleSetter">
					<xsl:with-param name="propertyId">NavigationMenuButton_FocusVisualStyle</xsl:with-param>
					<xsl:with-param name="scope" select="$id"/>
				</xsl:call-template>

				<!--NavigationMenuButton_Icon_Foreground-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">NavigationMenuButton_Icon_Foreground</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateResExtension">
							<xsl:with-param name="key">IconForegroundNormal</xsl:with-param>
							<xsl:with-param name="scope" select="$id" />
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--NavigationMenuButton_Icon_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">NavigationMenuButton_Icon_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="IconMargin" />
				</xsl:call-template>

				<!--NavigationMenuButton_Height-->
				<xsl:call-template name="generateHeightSetter">
					<xsl:with-param name="propertyId">NavigationMenuButton_Height</xsl:with-param>
				</xsl:call-template>

				<!--NavigationMenuButton_Container_MarginLevel1-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">NavigationMenuButton_Container_MarginLevel1</xsl:with-param>
					<xsl:with-param name="marginNode" select="ContainerMarginLevel1"/>
				</xsl:call-template>

				<!--NavigationMenuButton_Container_MarginLevel2-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">NavigationMenuButton_Container_MarginLevel2</xsl:with-param>
					<xsl:with-param name="marginNode" select="ContainerMarginLevel2"/>
				</xsl:call-template>
				
			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Hover trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Hover</xsl:with-param>
					<xsl:with-param name="setters">

						<!--NavigationMenuButton_Container_Background-->
						<xsl:if test="ContainerBackgroundHover">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">NavigationMenuButton_Container_Background</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ContainerBackgroundHover</xsl:with-param>
										<xsl:with-param name="scope" select="$id" />
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:if>

						<!--NavigationMenuButton_Container_BorderBrush-->
						<xsl:if test="BorderColorHover">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">NavigationMenuButton_Container_BorderBrush</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">BorderHover</xsl:with-param>
										<xsl:with-param name="scope" select="$id" />
									</xsl:call-template>
								</xsl:with-param>
						</xsl:call-template>
						</xsl:if>

						<!--NavigationMenuButton_Container_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">NavigationMenuButton_Container_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessHover" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--NavigationMenuButton_Icon_Foreground-->
						<xsl:if test="IconForegroundHover">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">NavigationMenuButton_Icon_Foreground</xsl:with-param>
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

						<!--NavigationMenuButton_Container_Background-->
						<xsl:if test="ContainerBackgroundPressed">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">NavigationMenuButton_Container_Background</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ContainerBackgroundPressed</xsl:with-param>
										<xsl:with-param name="scope" select="$id" />
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:if>

						<!--NavigationMenuButton_Container_BorderBrush-->
						<xsl:if test="BorderColorPressed">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">NavigationMenuButton_Container_BorderBrush</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">BorderPressed</xsl:with-param>
										<xsl:with-param name="scope" select="$id" />
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:if>

						<!--NavigationMenuButton_Container_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">NavigationMenuButton_Container_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessPressed" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--NavigationMenuButton_Icon_Foreground-->
						<xsl:if test="IconForegroundPressed">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">NavigationMenuButton_Icon_Foreground</xsl:with-param>
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
