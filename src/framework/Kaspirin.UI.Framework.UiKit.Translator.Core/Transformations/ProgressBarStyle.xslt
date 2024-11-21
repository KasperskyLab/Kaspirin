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

	<xsl:template match="ProgressBars">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">ProgressBars</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="ProgressBar">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:ProgressBar</xsl:with-param>
			<xsl:with-param name="basedOn">ProgressBarUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">

					<!--EstimationTextStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@EstimationTextStyleId"/>
						<xsl:with-param name="key">EstimationTextStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">EstimationForeground</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--EstimationTextDisabledStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@EstimationTextStyleId"/>
						<xsl:with-param name="key">EstimationTextDisabledStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">EstimationForegroundDisabled</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--StateTextStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@StateTextStyleId"/>
						<xsl:with-param name="key">StateTextStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">StateForeground</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--StateTextDisabledStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@StateTextStyleId"/>
						<xsl:with-param name="key">StateTextDisabledStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">StateForegroundDisabled</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--ValueTextStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@ValueTextStyleId"/>
						<xsl:with-param name="key">ValueTextStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ValueForeground</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--ValueTextDisabledStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@ValueTextStyleId"/>
						<xsl:with-param name="key">ValueTextDisabledStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ValueForegroundDisabled</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>
				</xsl:element>



				<!--ProgressBar_EstimationTextStyle-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">ProgressBar_EstimationTextStyle</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">EstimationTextStyle</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--ProgressBar_Glow_Brush-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">ProgressBar_Glow_Brush</xsl:with-param>
					<xsl:with-param name="brushName">GlowColor</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--ProgressBar_Glow_Width-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">ProgressBar_Glow_Width</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@GlowWidth" />
				</xsl:call-template>

				<!--ProgressBar_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">ProgressBar_Padding</xsl:with-param>
				</xsl:call-template>

				<!--ProgressBar_Track_Background-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">ProgressBar_Track_Background</xsl:with-param>
					<xsl:with-param name="brushName">Background</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!-- ProgressBar_Track_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId">ProgressBar_Track_CornerRadius</xsl:with-param>
				</xsl:call-template>

				<!--ProgressBar_Track_Height-->
				<xsl:call-template name="generateHeightSetter">
					<xsl:with-param name="propertyId">ProgressBar_Track_Height</xsl:with-param>
				</xsl:call-template>

				<!--ProgressBar_State_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">ProgressBar_State_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="StateMargin" />
				</xsl:call-template>

				<!--ProgressBar_StateTextStyle-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">ProgressBar_StateTextStyle</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">StateTextStyle</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--ProgressBar_ValueTextStyle-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">ProgressBar_ValueTextStyle</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">ValueTextStyle</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Type = Danger trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Danger</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ProgressBar_Indicator_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ProgressBar_Indicator_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">IndicatorColorDanger</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Type = Info trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Info</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ProgressBar_Indicator_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ProgressBar_Indicator_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">IndicatorColorInfo</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Type = Neutral trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Neutral</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ProgressBar_Indicator_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ProgressBar_Indicator_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">IndicatorColorNeutral</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Type = Positive trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Positive</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ProgressBar_Indicator_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ProgressBar_Indicator_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">IndicatorColorPositive</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Type = Warning trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Warning</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ProgressBar_Indicator_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ProgressBar_Indicator_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">IndicatorColorWarning</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State = Disabled trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ProgressBar_EstimationTextStyle-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">ProgressBar_EstimationTextStyle</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateStaticResource">
									<xsl:with-param name="key">EstimationTextDisabledStyle</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ProgressBar_Indicator_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">ProgressBar_Indicator_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">IndicatorColorDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--ProgressBar_StateTextStyle-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">ProgressBar_StateTextStyle</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateStaticResource">
									<xsl:with-param name="key">StateTextDisabledStyle</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ProgressBar_ValueTextStyle-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">ProgressBar_ValueTextStyle</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateStaticResource">
									<xsl:with-param name="key">ValueTextDisabledStyle</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

</xsl:transform>