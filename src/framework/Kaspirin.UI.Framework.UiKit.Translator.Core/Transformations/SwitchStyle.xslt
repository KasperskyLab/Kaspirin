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

	<xsl:template match="Switches">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">Switches</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="Switch">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:Switch</xsl:with-param>
			<xsl:with-param name="basedOn">SwitchUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Switch_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId">Switch_CornerRadius</xsl:with-param>
				</xsl:call-template>

				<!--Switch_FocusVisualStyle-->
				<xsl:call-template name="generateFocusVisualStyleSetter">
					<xsl:with-param name="propertyId">Switch_FocusVisualStyle</xsl:with-param>
					<xsl:with-param name="scope" select="$id"/>
				</xsl:call-template>

				<!--Switch_Height-->
				<xsl:call-template name="generateHeightSetter">
					<xsl:with-param name="propertyId">Switch_Height</xsl:with-param>
				</xsl:call-template>

				<!--Switch_Opacity-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">Switch_Opacity</xsl:with-param>
					<xsl:with-param name="propertyValue" select = "@Opacity" />
				</xsl:call-template>
				
				<!--Switch_Thumb_Height-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">Switch_Thumb_Height</xsl:with-param>
					<xsl:with-param name="propertyValue" select = "@ThumbHeight" />
				</xsl:call-template>

				<!--Switch_Thumb_Margin-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">Switch_Thumb_Margin</xsl:with-param>
				</xsl:call-template>
				
				<!--Switch_Thumb_Shadow-->
				<xsl:call-template name="generateShadowSetter">
					<xsl:with-param name="propertyId">Switch_Thumb_Shadow</xsl:with-param>
					<xsl:with-param name="shadowNode" select="ThumbShadowNormal"/>
					<xsl:with-param name="scope" select="$id"/>
				</xsl:call-template>

				<!--Switch_Thumb_Width-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">Switch_Thumb_Width</xsl:with-param>
					<xsl:with-param name="propertyValue" select = "@ThumbWidth" />
				</xsl:call-template>
				
				<!--Switch_Width-->
				<xsl:call-template name="generateWidthSetter">
					<xsl:with-param name="propertyId">Switch_Width</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[IsChecked = False]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
					<xsl:with-param name="propertyValue">False</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Switch_Thumb_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">Switch_Thumb_Shadow</xsl:with-param>
							<xsl:with-param name="shadowNode" select="ThumbShadowOff"/>
							<xsl:with-param name="scope" select="$id"/>
						</xsl:call-template>

						<!--Switch_Thumb_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Switch_Thumb_Background</xsl:with-param>
							<xsl:with-param name="brushName">ThumbColorOff</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = True]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
					<xsl:with-param name="propertyValue">True</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Switch_Thumb_Shadow-->
						<xsl:call-template name="generateShadowSetter">
							<xsl:with-param name="propertyId">Switch_Thumb_Shadow</xsl:with-param>
							<xsl:with-param name="shadowNode" select="ThumbShadowOn"/>
							<xsl:with-param name="scope" select="$id"/>
						</xsl:call-template>

						<!--Switch_Thumb_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Switch_Thumb_Background</xsl:with-param>
							<xsl:with-param name="brushName">ThumbColorOn</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State = Disabled]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Switch_Opacity-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Switch_Opacity</xsl:with-param>
							<xsl:with-param name="propertyValue" select = "@OpacityDisabled" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked == False, State = Disabled]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == False-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">False</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Disabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Switch_Track_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Switch_Track_Background</xsl:with-param>
							<xsl:with-param name="brushName">TrackColorOffDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked == False, State = Normal]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == False-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">False</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Normal</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Switch_Track_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Switch_Track_Background</xsl:with-param>
							<xsl:with-param name="brushName">TrackColorOffNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked == False, State = Hover]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == False-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">False</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Hover-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Hover</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Switch_Track_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Switch_Track_Background</xsl:with-param>
							<xsl:with-param name="brushName">TrackColorOffHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked == False, State = Pressed]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == False-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">False</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Pressed-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Switch_Track_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Switch_Track_Background</xsl:with-param>
							<xsl:with-param name="brushName">TrackColorOffPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked == True, State = Disabled]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == True-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">True</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Disabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Switch_Track_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Switch_Track_Background</xsl:with-param>
							<xsl:with-param name="brushName">TrackColorOnDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked == True, State = Normal]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == True-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">True</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Normal</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Switch_Track_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Switch_Track_Background</xsl:with-param>
							<xsl:with-param name="brushName">TrackColorOnNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked == True, State = Hover]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == True-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">True</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Hover-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Hover</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Switch_Track_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Switch_Track_Background</xsl:with-param>
							<xsl:with-param name="brushName">TrackColorOnHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked == True, State = Pressed]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == True-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">True</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Pressed-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Switch_Track_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Switch_Track_Background</xsl:with-param>
							<xsl:with-param name="brushName">TrackColorOnPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

</xsl:transform>