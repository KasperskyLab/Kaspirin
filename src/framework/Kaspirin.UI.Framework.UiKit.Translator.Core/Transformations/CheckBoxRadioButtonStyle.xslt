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

	<xsl:template match="CheckBoxes">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">CheckBoxes</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="RadioButtons">
		<xsl:call-template name="getRegionComment">
			<xsl:with-param name="description">RadioButtons</xsl:with-param>
		</xsl:call-template>

		<xsl:apply-templates/>

		<xsl:call-template name="getEndRegionComment"/>
	</xsl:template>

	<xsl:template match="CheckBox|RadioButton">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">ToggleButton</xsl:with-param>
			<xsl:with-param name="basedOn">CheckableUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">

					<!--DescriptionStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@DescriptionStyleId"/>
						<xsl:with-param name="key">DescriptionStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">DescriptionForeground</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--DescriptionStyleDisabled-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@DescriptionStyleId"/>
						<xsl:with-param name="key">DescriptionStyleDisabled</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">DescriptionForegroundDisabled</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">TextForeground</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleDisabled-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleDisabled</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">TextForegroundDisabled</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

				</xsl:element>

				<!--Checkable_DescriptionStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">Checkable_DescriptionStyle</xsl:with-param>
					<xsl:with-param name="resourceName">DescriptionStyle</xsl:with-param>
				</xsl:call-template>

				<!--Checkable_DescriptionStyleDisabled-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">Checkable_DescriptionStyleDisabled</xsl:with-param>
					<xsl:with-param name="resourceName">DescriptionStyleDisabled</xsl:with-param>
				</xsl:call-template>

				<!--Checkable_Description_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">Checkable_Description_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="DescriptionMargin"/>
				</xsl:call-template>

				<!--Checkable_Mark_Source-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">Checkable_Mark_Source</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateImgExtension">
							<xsl:with-param name="key">
								<xsl:call-template name="getSvgFilename">
									<xsl:with-param name="id" select="$id" />
									<xsl:with-param name="member" select="name(Icon)" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--Checkable_MarkContainer_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId">Checkable_MarkContainer_CornerRadius</xsl:with-param>
					<xsl:with-param name="isSingleValueExpected" select="true()"/>
				</xsl:call-template>

				<!--Checkable_MarkContainer_Height-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">Checkable_MarkContainer_Height</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@MarkContainerHeight" />
				</xsl:call-template>

				<!--Checkable_MarkContainer_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">Checkable_MarkContainer_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="MarkContainerMargin"/>
				</xsl:call-template>

				<!--Checkable_MarkContainer_Width-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">Checkable_MarkContainer_Width</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@MarkContainerWidth" />
				</xsl:call-template>

				<!--Checkable_FocusVisualStyle-->
				<xsl:call-template name="generateFocusVisualStyleSetter">
					<xsl:with-param name="propertyId">Checkable_FocusVisualStyle</xsl:with-param>
					<xsl:with-param name="scope" select="$id"/>
				</xsl:call-template>

				<!--Checkable_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">Checkable_Padding</xsl:with-param>
				</xsl:call-template>

				<!--Checkable_TextStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">Checkable_TextStyle</xsl:with-param>
					<xsl:with-param name="resourceName">TextStyle</xsl:with-param>
				</xsl:call-template>

				<!--Checkable_TextStyleDisabled-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">Checkable_TextStyleDisabled</xsl:with-param>
					<xsl:with-param name="resourceName">TextStyleDisabled</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[IsChecked = Null]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
					<xsl:with-param name="propertyValue">{x:Null}</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Checkable_Mark_Source-->
						<xsl:if test="IconPartial">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">Checkable_Mark_Source</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateImgExtension">
										<xsl:with-param name="key">
											<xsl:call-template name="getSvgFilename">
												<xsl:with-param name="id" select="$id" />
												<xsl:with-param name="member" select="name(IconPartial)" />
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:if>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = False and State = Disabled]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == {x:Null}-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">False</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Checkable_MarkContainer_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessDisabled" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = False and State = Normal]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == {x:Null}-->
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

						<!--Checkable_MarkContainer_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessNormal" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = False and State = Hover]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == {x:Null}-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">False</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Pressed-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Hover</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Checkable_MarkContainer_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessHover" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = False and State = Pressed]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == {x:Null}-->
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

						<!--Checkable_MarkContainer_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPressed" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = False and State = Normal and IsInvalidState = True]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == {x:Null}-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">False</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Normal</xsl:with-param>
						</xsl:call-template>

						<!--IsInvalidState == True-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:CheckableInternals.IsInvalidState</xsl:with-param>
							<xsl:with-param name="propertyValue">True</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Checkable_MarkContainer_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundInvalidNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderInvalidNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessInvalidNormal" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = False and State = Hover and IsInvalidState = True]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == {x:Null}-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">False</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Pressed-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Hover</xsl:with-param>
						</xsl:call-template>

						<!--IsInvalidState == True-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:CheckableInternals.IsInvalidState</xsl:with-param>
							<xsl:with-param name="propertyValue">True</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Checkable_MarkContainer_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundInvalidHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderInvalidHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessInvalidHover" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = False and State = Pressed and IsInvalidState = True]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == {x:Null}-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">False</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Pressed-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
						</xsl:call-template>

						<!--IsInvalidState == True-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:CheckableInternals.IsInvalidState</xsl:with-param>
							<xsl:with-param name="propertyValue">True</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Checkable_MarkContainer_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundInvalidPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderInvalidPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessInvalidPressed" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = Null and State = Disabled]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == {x:Null}-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">{x:Null}</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Disabled-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Checkable_Mark_Brush-->
						<xsl:if test="MarkColorPartialDisabled">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_Mark_Brush</xsl:with-param>
								<xsl:with-param name="brushName">MarkColorPartialDisabled</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_Background-->
						<xsl:if test="BackgroundPartialDisabled">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
								<xsl:with-param name="brushName">BackgroundPartialDisabled</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:if test="BorderColorPartialDisabled">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
								<xsl:with-param name="brushName">BorderPartialDisabled</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_BorderThickness -->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPartialDisabled" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = Null and State = Hover]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == {x:Null}-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">{x:Null}</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Hover-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Hover</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Checkable_Mark_Brush-->
						<xsl:if test="MarkColorPartialHover">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_Mark_Brush</xsl:with-param>
								<xsl:with-param name="brushName">MarkColorPartialHover</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_Background-->
						<xsl:if test="BackgroundPartialHover">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
								<xsl:with-param name="brushName">BackgroundPartialHover</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:if test="BorderColorPartialHover">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
								<xsl:with-param name="brushName">BorderPartialHover</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_BorderThickness -->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPartialHover" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = Null and State = Normal]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == {x:Null}-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">{x:Null}</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Normal</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Checkable_Mark_Brush-->
						<xsl:if test="MarkColorPartialNormal">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_Mark_Brush</xsl:with-param>
								<xsl:with-param name="brushName">MarkColorPartialNormal</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_Background-->
						<xsl:if test="BackgroundPartialNormal">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
								<xsl:with-param name="brushName">BackgroundPartialNormal</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:if test="BorderColorPartialNormal">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
								<xsl:with-param name="brushName">BorderPartialNormal</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_BorderThickness -->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPartialNormal" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = Null and State = Pressed]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == {x:Null}-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">{x:Null}</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Checkable_Mark_Brush-->
						<xsl:if test="MarkColorPartialPressed">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_Mark_Brush</xsl:with-param>
								<xsl:with-param name="brushName">MarkColorPartialPressed</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_Background-->
						<xsl:if test="BackgroundPartialPressed">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
								<xsl:with-param name="brushName">BackgroundPartialPressed</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:if test="BorderColorPartialPressed">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
								<xsl:with-param name="brushName">BorderPartialPressed</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_BorderThickness -->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="node" select="BorderThicknessPartialPressed" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = Null and State = Hover and IsInvalidState = True]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == {x:Null}-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">{x:Null}</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Hover-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Hover</xsl:with-param>
						</xsl:call-template>

						<!--IsInvalidState == True-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:CheckableInternals.IsInvalidState</xsl:with-param>
							<xsl:with-param name="propertyValue">True</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Checkable_Mark_Brush-->
						<xsl:if test="MarkColorPartialHover">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_Mark_Brush</xsl:with-param>
								<xsl:with-param name="brushName">MarkColorPartialInvalidHover</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_Background-->
						<xsl:if test="BackgroundPartialHover">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
								<xsl:with-param name="brushName">BackgroundPartialInvalidHover</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:if test="BorderColorPartialHover">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
								<xsl:with-param name="brushName">BorderPartialInvalidHover</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_BorderThickness -->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPartialInvalidHover" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = Null and State = Normal and IsInvalidState = True]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == {x:Null}-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">{x:Null}</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Normal</xsl:with-param>
						</xsl:call-template>

						<!--IsInvalidState == True-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:CheckableInternals.IsInvalidState</xsl:with-param>
							<xsl:with-param name="propertyValue">True</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Checkable_Mark_Brush-->
						<xsl:if test="MarkColorPartialNormal">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_Mark_Brush</xsl:with-param>
								<xsl:with-param name="brushName">MarkColorPartialInvalidNormal</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_Background-->
						<xsl:if test="BackgroundPartialNormal">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
								<xsl:with-param name="brushName">BackgroundPartialInvalidNormal</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:if test="BorderColorPartialNormal">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
								<xsl:with-param name="brushName">BorderPartialInvalidNormal</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_BorderThickness -->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessPartialInvalidNormal" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = Null and State = Pressed and IsInvalidState = True]-->
				<xsl:call-template name="generateMultiTrigger">
					<xsl:with-param name="conditions">

						<!--IsChecked == {x:Null}-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">IsChecked</xsl:with-param>
							<xsl:with-param name="propertyValue">{x:Null}</xsl:with-param>
						</xsl:call-template>

						<!--StateService.State == Normal-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
							<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
						</xsl:call-template>

						<!--IsInvalidState == True-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:CheckableInternals.IsInvalidState</xsl:with-param>
							<xsl:with-param name="propertyValue">True</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Checkable_Mark_Brush-->
						<xsl:if test="MarkColorPartialPressed">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_Mark_Brush</xsl:with-param>
								<xsl:with-param name="brushName">MarkColorPartialInvalidPressed</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_Background-->
						<xsl:if test="BackgroundPartialPressed">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
								<xsl:with-param name="brushName">BackgroundPartialInvalidPressed</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:if test="BorderColorPartialPressed">
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
								<xsl:with-param name="brushName">BorderPartialInvalidPressed</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>
						</xsl:if>

						<!--Checkable_MarkContainer_BorderThickness -->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="node" select="BorderThicknessPartialInvalidPressed" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>
				
				<!--[IsChecked = True and State = Disabled]-->
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

						<!--Checkable_Mark_Brush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_Mark_Brush</xsl:with-param>
							<xsl:with-param name="brushName">MarkColorCheckedDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundCheckedDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderCheckedDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderThickness -->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessCheckedDisabled" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = True and State = Hover]-->
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

						<!--Checkable_Mark_Brush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_Mark_Brush</xsl:with-param>
							<xsl:with-param name="brushName">MarkColorCheckedHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundCheckedHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderCheckedHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderThickness -->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessCheckedHover" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = True and State = Normal]-->
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

						<!--Checkable_Mark_Brush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_Mark_Brush</xsl:with-param>
							<xsl:with-param name="brushName">MarkColorCheckedNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundCheckedNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderCheckedNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderThickness -->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessCheckedNormal" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = True and State = Pressed]-->
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

						<!--Checkable_Mark_Brush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_Mark_Brush</xsl:with-param>
							<xsl:with-param name="brushName">MarkColorCheckedPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundCheckedPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderCheckedPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderThickness -->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessCheckedPressed" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = True and State = Hover and IsInvalidState = True]-->
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

						<!--IsInvalidState == True-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:CheckableInternals.IsInvalidState</xsl:with-param>
							<xsl:with-param name="propertyValue">True</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Checkable_Mark_Brush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_Mark_Brush</xsl:with-param>
							<xsl:with-param name="brushName">MarkColorCheckedInvalidHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundCheckedInvalidHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderCheckedInvalidHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderThickness -->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessCheckedInvalidHover" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = True and State = Normal and IsInvalidState = True]-->
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

						<!--IsInvalidState == True-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:CheckableInternals.IsInvalidState</xsl:with-param>
							<xsl:with-param name="propertyValue">True</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Checkable_Mark_Brush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_Mark_Brush</xsl:with-param>
							<xsl:with-param name="brushName">MarkColorCheckedInvalidNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundCheckedInvalidNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderCheckedInvalidNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderThickness -->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessCheckedInvalidNormal" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsChecked = True and State = Pressed and IsInvalidState = True]-->
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

						<!--IsInvalidState == True-->
						<xsl:call-template name="generateCondition">
							<xsl:with-param name="propertyName">visuals:CheckableInternals.IsInvalidState</xsl:with-param>
							<xsl:with-param name="propertyValue">True</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>

					<xsl:with-param name="setters">

						<!--Checkable_Mark_Brush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_Mark_Brush</xsl:with-param>
							<xsl:with-param name="brushName">MarkColorCheckedInvalidPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundCheckedInvalidPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderCheckedInvalidPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--Checkable_MarkContainer_BorderThickness -->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">Checkable_MarkContainer_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessCheckedInvalidPressed" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

</xsl:transform>