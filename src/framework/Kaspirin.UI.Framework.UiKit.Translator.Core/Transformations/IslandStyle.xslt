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

	<xsl:template match="Islands">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">Islands</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="Island">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:Island</xsl:with-param>
			<xsl:with-param name="basedOn">IslandUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>

			<xsl:with-param name="setters">

				<!--Island_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">Island_Padding</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Elevation First multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

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

						<!--Island_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Island_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundElevationLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Island_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Island_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorElevationLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Island_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Island_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessElevationLevel1" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Island_CornerRadius-->
						<xsl:call-template name="generateCornerRadiusSetter">
							<xsl:with-param name="propertyId">Island_CornerRadius</xsl:with-param>
							<xsl:with-param name="cornerRadiusNode" select="CornerRadiusElevationLevel1" />
						</xsl:call-template>

						<!--Island_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">Island_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Elevation Second multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

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

						<!--Island_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Island_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundElevationLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Island_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Island_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorElevationLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Island_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Island_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessElevationLevel2" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Island_CornerRadius-->
						<xsl:call-template name="generateCornerRadiusSetter">
							<xsl:with-param name="propertyId">Island_CornerRadius</xsl:with-param>
							<xsl:with-param name="cornerRadiusNode" select="CornerRadiusElevationLevel2" />
						</xsl:call-template>

						<!--Island_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">Island_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowElevationLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>
				
				<!--[Primary First multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

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

						<!--Island_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Island_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundPrimaryLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Island_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Island_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorPrimaryLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Island_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Island_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessPrimaryLevel1" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Island_CornerRadius-->
						<xsl:call-template name="generateCornerRadiusSetter">
							<xsl:with-param name="propertyId">Island_CornerRadius</xsl:with-param>
							<xsl:with-param name="cornerRadiusNode" select="CornerRadiusPrimaryLevel1" />
						</xsl:call-template>

						<!--Island_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">Island_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryLevel1"/>
						</xsl:call-template>

					</xsl:with-param>
					
				</xsl:call-template>

				<!--[Primary Second multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

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

						<!--Island_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Island_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundPrimaryLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Island_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Island_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorPrimaryLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Island_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Island_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessPrimaryLevel2" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Island_CornerRadius-->
						<xsl:call-template name="generateCornerRadiusSetter">
							<xsl:with-param name="propertyId">Island_CornerRadius</xsl:with-param>
							<xsl:with-param name="cornerRadiusNode" select="CornerRadiusPrimaryLevel2" />
						</xsl:call-template>

						<!--Island_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">Island_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Secondary First multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

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

						<!--Island_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Island_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundSecondaryLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Island_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Island_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorSecondaryLevel1</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Island_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Island_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessSecondaryLevel1" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Island_CornerRadius-->
						<xsl:call-template name="generateCornerRadiusSetter">
							<xsl:with-param name="propertyId">Island_CornerRadius</xsl:with-param>
							<xsl:with-param name="cornerRadiusNode" select="CornerRadiusSecondaryLevel1" />
						</xsl:call-template>

						<!--Island_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">Island_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryLevel1"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

				<!--[Secondary Second multi trigger]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

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

						<!--Island_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Island_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundSecondaryLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Island_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Island_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorSecondaryLevel2</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Island_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Island_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessSecondaryLevel2" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Island_CornerRadius-->
						<xsl:call-template name="generateCornerRadiusSetter">
							<xsl:with-param name="propertyId">Island_CornerRadius</xsl:with-param>
							<xsl:with-param name="cornerRadiusNode" select="CornerRadiusSecondaryLevel2" />
						</xsl:call-template>

						<!--Island_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">Island_Shadow</xsl:with-param>
							<xsl:with-param name="scope" select="$id"/>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryLevel2"/>
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

			</xsl:with-param>

		</xsl:call-template>
		
	</xsl:template>

</xsl:transform>