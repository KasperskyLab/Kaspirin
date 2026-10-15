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

	<xsl:template match="IslandButtons">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">IslandButtons</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="IslandButton">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:IslandButton</xsl:with-param>
			<xsl:with-param name="basedOn">IslandButtonUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>

			<xsl:with-param name="setters">

				<!--IslandButton_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">IslandButton_Padding</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Level = First trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Level</xsl:with-param>
					<xsl:with-param name="propertyValue">First</xsl:with-param>
					<xsl:with-param name="setters">

						<!--IslandButton_FocusVisualStyle-->
						<xsl:call-template name="generateFocusVisualStyleSetter">
							<xsl:with-param name="propertyId">IslandButton_FocusVisualStyle</xsl:with-param>
							<xsl:with-param name="focusNode" select="FocusLevel1"/>
							<xsl:with-param name="scope" select="$id"/>
						</xsl:call-template>

						<!--IslandButton_CornerRadius-->
						<xsl:call-template name="generateCornerRadiusSetter">
							<xsl:with-param name="propertyId">IslandButton_CornerRadius</xsl:with-param>
							<xsl:with-param name="cornerRadiusNode" select="CornerRadiusLevel1" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Level = Second trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Level</xsl:with-param>
					<xsl:with-param name="propertyValue">Second</xsl:with-param>
					<xsl:with-param name="setters">

						<!--IslandButton_FocusVisualStyle-->
						<xsl:call-template name="generateFocusVisualStyleSetter">
							<xsl:with-param name="propertyId">IslandButton_FocusVisualStyle</xsl:with-param>
							<xsl:with-param name="focusNode" select="FocusLevel2"/>
							<xsl:with-param name="scope" select="$id"/>
						</xsl:call-template>

						<!--IslandButton_CornerRadius-->
						<xsl:call-template name="generateCornerRadiusSetter">
							<xsl:with-param name="propertyId">IslandButton_CornerRadius</xsl:with-param>
							<xsl:with-param name="cornerRadiusNode" select="CornerRadiusLevel2" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>


				<!--[Elevation First Normal multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Normal</xsl:with-param>
						</xsl:call-template>

						<!--Level == First-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">First</xsl:with-param>
						</xsl:call-template>

						<!--Type == Elevation-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Elevation</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundElevationNormalLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorElevationNormalLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessElevationNormalLevel1" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationNormalLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Elevation First Hover multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Hover-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Hover</xsl:with-param>
						</xsl:call-template>

						<!--Level == First-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">First</xsl:with-param>
						</xsl:call-template>

						<!--Type == Elevation-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Elevation</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundElevationHoverLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorElevationHoverLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessElevationHoverLevel1" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationHoverLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Elevation First Pressed multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Pressed-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
						</xsl:call-template>

						<!--Level == First-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">First</xsl:with-param>
						</xsl:call-template>

						<!--Type == Elevation-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Elevation</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundElevationPressedLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorElevationPressedLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessElevationPressedLevel1" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationPressedLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Elevation First Disabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Disabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

						<!--Level == First-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">First</xsl:with-param>
						</xsl:call-template>

						<!--Type == Elevation-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Elevation</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundElevationDisabledLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorElevationDisabledLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessElevationDisabledLevel1" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationDisabledLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>


				<!--[Elevation Second Normal multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Normal</xsl:with-param>
						</xsl:call-template>

						<!--Level == Second-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">Second</xsl:with-param>
						</xsl:call-template>

						<!--Type == Elevation-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Elevation</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundElevationNormalLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorElevationNormalLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessElevationNormalLevel2" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationNormalLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Elevation Second Hover multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Hover-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Hover</xsl:with-param>
						</xsl:call-template>

						<!--Level == Second-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">Second</xsl:with-param>
						</xsl:call-template>

						<!--Type == Elevation-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Elevation</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundElevationHoverLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorElevationHoverLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessElevationHoverLevel2" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationHoverLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Elevation Second Pressed multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Pressed-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
						</xsl:call-template>

						<!--Level == Second-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">Second</xsl:with-param>
						</xsl:call-template>

						<!--Type == Elevation-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Elevation</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundElevationPressedLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorElevationPressedLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessElevationPressedLevel2" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationPressedLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Elevation Second Disabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Disabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

						<!--Level == Second-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">Second</xsl:with-param>
						</xsl:call-template>

						<!--Type == Elevation-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Elevation</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundElevationDisabledLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorElevationDisabledLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessElevationDisabledLevel2" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationDisabledLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>


				<!--[Primary First Normal multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Normal</xsl:with-param>
						</xsl:call-template>

						<!--Level == First-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">First</xsl:with-param>
						</xsl:call-template>

						<!--Type == Primary-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Primary</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundPrimaryNormalLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorPrimaryNormalLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessPrimaryNormalLevel1" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryNormalLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Primary First Hover multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Hover-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Hover</xsl:with-param>
						</xsl:call-template>

						<!--Level == First-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">First</xsl:with-param>
						</xsl:call-template>

						<!--Type == Primary-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Primary</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundPrimaryHoverLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorPrimaryHoverLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessPrimaryHoverLevel1" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryHoverLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Primary First Pressed multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Pressed-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
						</xsl:call-template>

						<!--Level == First-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">First</xsl:with-param>
						</xsl:call-template>

						<!--Type == Primary-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Primary</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundPrimaryPressedLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorPrimaryPressedLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessPrimaryPressedLevel1" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryPressedLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Primary First Disabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Disabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

						<!--Level == First-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">First</xsl:with-param>
						</xsl:call-template>

						<!--Type == Primary-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Primary</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundPrimaryDisabledLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorPrimaryDisabledLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessPrimaryDisabledLevel1" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryDisabledLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>


				<!--[Primary Second Normal multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Normal</xsl:with-param>
						</xsl:call-template>

						<!--Level == Second-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">Second</xsl:with-param>
						</xsl:call-template>

						<!--Type == Primary-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Primary</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundPrimaryNormalLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorPrimaryNormalLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessPrimaryNormalLevel2" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryNormalLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Primary Second Hover multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Hover-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Hover</xsl:with-param>
						</xsl:call-template>

						<!--Level == Second-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">Second</xsl:with-param>
						</xsl:call-template>

						<!--Type == Primary-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Primary</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundPrimaryHoverLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorPrimaryHoverLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessPrimaryHoverLevel2" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryHoverLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Primary Second Pressed multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Pressed-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
						</xsl:call-template>

						<!--Level == Second-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">Second</xsl:with-param>
						</xsl:call-template>

						<!--Type == Primary-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Primary</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundPrimaryPressedLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorPrimaryPressedLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessPrimaryPressedLevel2" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryPressedLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Primary Second Disabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Disabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

						<!--Level == Second-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">Second</xsl:with-param>
						</xsl:call-template>

						<!--Type == Primary-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Primary</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundPrimaryDisabledLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorPrimaryDisabledLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessPrimaryDisabledLevel2" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryDisabledLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>


				<!--[Secondary First Normal multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Normal</xsl:with-param>
						</xsl:call-template>

						<!--Level == First-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">First</xsl:with-param>
						</xsl:call-template>

						<!--Type == Secondary-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Secondary</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundSecondaryNormalLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorSecondaryNormalLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessSecondaryNormalLevel1" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryNormalLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Secondary First Hover multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Hover-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Hover</xsl:with-param>
						</xsl:call-template>

						<!--Level == First-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">First</xsl:with-param>
						</xsl:call-template>

						<!--Type == Secondary-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Secondary</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundSecondaryHoverLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorSecondaryHoverLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessSecondaryHoverLevel1" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryHoverLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Secondary First Pressed multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Pressed-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
						</xsl:call-template>

						<!--Level == First-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">First</xsl:with-param>
						</xsl:call-template>

						<!--Type == Secondary-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Secondary</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundSecondaryPressedLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorSecondaryPressedLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessSecondaryPressedLevel1" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryPressedLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Secondary First Disabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Disabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

						<!--Level == First-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">First</xsl:with-param>
						</xsl:call-template>

						<!--Type == Secondary-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Secondary</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundSecondaryDisabledLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorSecondaryDisabledLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessSecondaryDisabledLevel1" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryDisabledLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>


				<!--[Secondary Second Normal multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Normal</xsl:with-param>
						</xsl:call-template>

						<!--Level == Second-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">Second</xsl:with-param>
						</xsl:call-template>

						<!--Type == Secondary-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Secondary</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundSecondaryNormalLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorSecondaryNormalLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessSecondaryNormalLevel2" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryNormalLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Secondary Second Hover multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Hover-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Hover</xsl:with-param>
						</xsl:call-template>

						<!--Level == Second-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">Second</xsl:with-param>
						</xsl:call-template>

						<!--Type == Secondary-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Secondary</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundSecondaryHoverLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorSecondaryHoverLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessSecondaryHoverLevel2" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryHoverLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Secondary Second Pressed multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Pressed-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
						</xsl:call-template>

						<!--Level == Second-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">Second</xsl:with-param>
						</xsl:call-template>

						<!--Type == Secondary-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Secondary</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundSecondaryPressedLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorSecondaryPressedLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessSecondaryPressedLevel2" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryPressedLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Secondary Second Disabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Disabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

						<!--Level == Second-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Level</xsl:with-param>
							<xsl:with-param name="propertyValue">Second</xsl:with-param>
						</xsl:call-template>

						<!--Type == Secondary-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">Type</xsl:with-param>
							<xsl:with-param name="propertyValue">Secondary</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--IslandButton_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundSecondaryDisabledLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorSecondaryDisabledLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">IslandButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessSecondaryDisabledLevel2" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--IslandButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryDisabledLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>


			</xsl:with-param>

		</xsl:call-template>

	</xsl:template>

</xsl:transform>