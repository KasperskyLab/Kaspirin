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

	<xsl:template match="IslandToggleButtons">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">IslandToggleButtons</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="IslandToggleButton">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:IslandToggleButton</xsl:with-param>
			<xsl:with-param name="basedOn">IslandToggleButtonUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>

			<xsl:with-param name="setters">

				<!--IslandToggleButton_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">IslandToggleButton_Padding</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Level = First trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Level</xsl:with-param>
					<xsl:with-param name="propertyValue">First</xsl:with-param>
					<xsl:with-param name="setters">

						<!--IslandToggleButton_FocusVisualStyle-->
						<xsl:call-template name="generateFocusVisualStyleSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_FocusVisualStyle</xsl:with-param>
							<xsl:with-param name="focusNode" select="FocusLevel1"/>
							<xsl:with-param name="scope" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_CornerRadius-->
						<xsl:call-template name="generateCornerRadiusSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_CornerRadius</xsl:with-param>
							<xsl:with-param name="cornerRadiusNode" select="CornerRadiusLevel1" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Level = Second trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Level</xsl:with-param>
					<xsl:with-param name="propertyValue">Second</xsl:with-param>
					<xsl:with-param name="setters">

						<!--IslandToggleButton_FocusVisualStyle-->
						<xsl:call-template name="generateFocusVisualStyleSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_FocusVisualStyle</xsl:with-param>
							<xsl:with-param name="focusNode" select="FocusLevel2"/>
							<xsl:with-param name="scope" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_CornerRadius-->
						<xsl:call-template name="generateCornerRadiusSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_CornerRadius</xsl:with-param>
							<xsl:with-param name="cornerRadiusNode" select="CornerRadiusLevel2" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>


				<!--[Elevation First Normal multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundElevationNormalLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorElevationNormalLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessElevationNormalLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
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
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundElevationHoverLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorElevationHoverLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessElevationHoverLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
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
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundElevationPressedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorElevationPressedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessElevationPressedLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
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
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundElevationDisabledLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorElevationDisabledLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessElevationDisabledLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationDisabledLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Elevation First SelectedNormal multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedNormal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedNormal</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundElevationNormalCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorElevationNormalCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessElevationNormalCheckedLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationNormalCheckedLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Elevation First SelectedHover multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedHover-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedHover</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundElevationHoverCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorElevationHoverCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessElevationHoverCheckedLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationHoverCheckedLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Elevation First SelectedPressed multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedPressed-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedPressed</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundElevationPressedCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorElevationPressedCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessElevationPressedCheckedLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationPressedCheckedLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Elevation First SelectedDisabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedDisabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedDisabled</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundElevationDisabledCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorElevationDisabledCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessElevationDisabledCheckedLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationDisabledCheckedLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>


				<!--[Elevation Second Normal multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundElevationNormalLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorElevationNormalLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessElevationNormalLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
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
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundElevationHoverLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorElevationHoverLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessElevationHoverLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
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
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundElevationPressedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorElevationPressedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessElevationPressedLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
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
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundElevationDisabledLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorElevationDisabledLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessElevationDisabledLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationDisabledLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Elevation Second SelectedNormal multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedNormal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedNormal</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundElevationNormalCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorElevationNormalCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessElevationNormalCheckedLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationNormalCheckedLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Elevation Second SelectedHover multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedHover-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedHover</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundElevationHoverCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorElevationHoverCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessElevationHoverCheckedLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationHoverCheckedLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Elevation Second SelectedPressed multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedPressed-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedPressed</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundElevationPressedCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorElevationPressedCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessElevationPressedCheckedLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationPressedCheckedLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Elevation Second SelectedDisabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedDisabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedDisabled</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundElevationDisabledCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorElevationDisabledCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessElevationDisabledCheckedLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationDisabledCheckedLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>


				<!--[Primary First Normal multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPrimaryNormalLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorPrimaryNormalLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPrimaryNormalLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
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
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPrimaryHoverLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorPrimaryHoverLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPrimaryHoverLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
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
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPrimaryPressedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorPrimaryPressedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPrimaryPressedLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
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
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPrimaryDisabledLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorPrimaryDisabledLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPrimaryDisabledLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryDisabledLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Primary First SelectedNormal multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedNormal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedNormal</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPrimaryNormalCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorPrimaryNormalCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPrimaryNormalCheckedLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryNormalCheckedLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Primary First SelectedHover multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedHover-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedHover</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPrimaryHoverCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorPrimaryHoverCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPrimaryHoverCheckedLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryHoverCheckedLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Primary First SelectedPressed multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedPressed-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedPressed</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPrimaryPressedCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorPrimaryPressedCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPrimaryPressedCheckedLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryPressedCheckedLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Primary First SelectedDisabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedDisabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedDisabled</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPrimaryDisabledCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorPrimaryDisabledCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPrimaryDisabledCheckedLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryDisabledCheckedLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>


				<!--[Primary Second Normal multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPrimaryNormalLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorPrimaryNormalLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPrimaryNormalLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
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
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPrimaryHoverLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorPrimaryHoverLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPrimaryHoverLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
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
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPrimaryPressedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorPrimaryPressedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPrimaryPressedLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
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
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPrimaryDisabledLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorPrimaryDisabledLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPrimaryDisabledLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryDisabledLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Primary Second SelectedNormal multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedNormal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedNormal</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPrimaryNormalCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorPrimaryNormalCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPrimaryNormalCheckedLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryNormalCheckedLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Primary Second SelectedHover multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedHover-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedHover</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPrimaryHoverCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorPrimaryHoverCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPrimaryHoverCheckedLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryHoverCheckedLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Primary Second SelectedPressed multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedPressed-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedPressed</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPrimaryPressedCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorPrimaryPressedCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPrimaryPressedCheckedLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryPressedCheckedLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Primary Second SelectedDisabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedDisabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedDisabled</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPrimaryDisabledCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorPrimaryDisabledCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPrimaryDisabledCheckedLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryDisabledCheckedLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>


				<!--[Secondary First Normal multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundSecondaryNormalLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorSecondaryNormalLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessSecondaryNormalLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
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
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundSecondaryHoverLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorSecondaryHoverLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessSecondaryHoverLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
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
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundSecondaryPressedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorSecondaryPressedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessSecondaryPressedLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
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
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundSecondaryDisabledLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorSecondaryDisabledLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessSecondaryDisabledLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryDisabledLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Secondary First SelectedNormal multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedNormal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedNormal</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundSecondaryNormalCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorSecondaryNormalCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessSecondaryNormalCheckedLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryNormalCheckedLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Secondary First SelectedHover multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedHover-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedHover</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundSecondaryHoverCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorSecondaryHoverCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessSecondaryHoverCheckedLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryHoverCheckedLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Secondary First SelectedPressed multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedPressed-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedPressed</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundSecondaryPressedCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorSecondaryPressedCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessSecondaryPressedCheckedLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryPressedCheckedLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Secondary First SelectedDisabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedDisabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedDisabled</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundSecondaryDisabledCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorSecondaryDisabledCheckedLevel1</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessSecondaryDisabledCheckedLevel1" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryDisabledCheckedLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>


				<!--[Secondary Second Normal multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundSecondaryNormalLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorSecondaryNormalLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessSecondaryNormalLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
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
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundSecondaryHoverLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorSecondaryHoverLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessSecondaryHoverLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
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
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundSecondaryPressedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorSecondaryPressedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessSecondaryPressedLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
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
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundSecondaryDisabledLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorSecondaryDisabledLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessSecondaryDisabledLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryDisabledLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Secondary Second SelectedNormal multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedNormal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedNormal</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundSecondaryNormalCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorSecondaryNormalCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessSecondaryNormalCheckedLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryNormalCheckedLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Secondary Second SelectedHover multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedHover-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedHover</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundSecondaryHoverCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorSecondaryHoverCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessSecondaryHoverCheckedLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryHoverCheckedLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Secondary Second SelectedPressed multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedPressed-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedPressed</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundSecondaryPressedCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorSecondaryPressedCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessSecondaryPressedCheckedLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryPressedCheckedLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Secondary Second SelectedDisabled multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--State == SelectedDisabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
							<xsl:with-param name="propertyValue">SelectedDisabled</xsl:with-param>
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

						<!--IslandToggleButton_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundSecondaryDisabledCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorSecondaryDisabledCheckedLevel2</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--IslandToggleButton_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessSecondaryDisabledCheckedLevel2" />
						</xsl:call-template>

						<!--IslandToggleButton_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">IslandToggleButton_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryDisabledCheckedLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

			</xsl:with-param>

		</xsl:call-template>

	</xsl:template>

</xsl:transform>