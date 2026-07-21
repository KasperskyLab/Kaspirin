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

	<xsl:template match="SplitButtons">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">SplitButtons</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="SplitButton">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:variable name="rightButtonStyleId">
			<xsl:call-template name="replace">
				<xsl:with-param name="value" select="@RightButtonStyleId"/>
				<xsl:with-param name="old">Button</xsl:with-param>
				<xsl:with-param name="new">ToggleButton</xsl:with-param>
			</xsl:call-template>
		</xsl:variable>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:SplitButton</xsl:with-param>
			<xsl:with-param name="basedOn">SplitButtonUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">

					<!--DividerStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">visuals:Divider</xsl:with-param>
						<xsl:with-param name="basedOn" select="@DividerStyleId"/>
						<xsl:with-param name="key">DividerStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--Divider_Background-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Divider_Background</xsl:with-param>
								<xsl:with-param name="brushName">DividerBackground</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
							<!--Divider_Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Divider_Foreground</xsl:with-param>
								<xsl:with-param name="brushName">DividerForeground</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--DividerDisabledAllStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">visuals:Divider</xsl:with-param>
						<xsl:with-param name="basedOn" select="@DividerStyleIdDisabledAll"/>
						<xsl:with-param name="key">DividerDisabledAllStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--Divider_Background-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Divider_Background</xsl:with-param>
								<xsl:with-param name="brushName">DividerBackgroundDisabledAll</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>								
							</xsl:call-template>
							<!--Divider_Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Divider_Foreground</xsl:with-param>
								<xsl:with-param name="brushName">DividerForegroundDisabledAll</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--DividerDisabledSomeStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">visuals:Divider</xsl:with-param>
						<xsl:with-param name="basedOn" select="@DividerStyleIdDisabledSome"/>
						<xsl:with-param name="key">DividerDisabledSomeStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--Divider_Background-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Divider_Background</xsl:with-param>
								<xsl:with-param name="brushName">DividerBackgroundDisabledSome</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
							<!--Divider_Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Divider_Foreground</xsl:with-param>
								<xsl:with-param name="brushName">DividerForegroundDisabledSome</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--DividerPressedStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">visuals:Divider</xsl:with-param>
						<xsl:with-param name="basedOn" select="@DividerStyleIdPressed"/>
						<xsl:with-param name="key">DividerPressedStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--Divider_Background-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Divider_Background</xsl:with-param>
								<xsl:with-param name="brushName">DividerBackgroundPressed</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
							<!--Divider_Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Divider_Foreground</xsl:with-param>
								<xsl:with-param name="brushName">DividerForegroundPressed</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--MainButtonStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">Button</xsl:with-param>
						<xsl:with-param name="basedOn" select="@LeftButtonStyleId"/>
						<xsl:with-param name="key">MainButtonStyle</xsl:with-param>
						<xsl:with-param name="setters">

							<!--Button_Container_BorderThickness-->
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">Button_Container_BorderThickness</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateThickness">
										<xsl:with-param name="node" select="LeftButtonBorderThicknessNormal"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>

							<!--Button_Container_CornerRadius-->
							<xsl:call-template name="generateCornerRadiusSetter">
								<xsl:with-param name="propertyId">Button_Container_CornerRadius</xsl:with-param>
								<xsl:with-param name="cornerRadiusNode" select="LeftButtonCornerRadius"/>
							</xsl:call-template>

							<!--Button_FocusVisualStyle-->
							<xsl:call-template name="generateFocusVisualStyleSetter">
								<xsl:with-param name="propertyId">Button_FocusVisualStyle</xsl:with-param>
								<xsl:with-param name="focusNode" select="LeftButtonFocus"/>
								<xsl:with-param name="focusVisualBrush">MainButtonFocusVisualBrush</xsl:with-param>
								<xsl:with-param name="scope" select="$id"/>
							</xsl:call-template>

						</xsl:with-param>

						<xsl:with-param name="triggers">

							<!--[State Hover trigger]-->
							<xsl:call-template name="generateTrigger">
								<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
								<xsl:with-param name="propertyValue">Hover</xsl:with-param>
								<xsl:with-param name="setters">

									<!--Button_Container_BorderThickness-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId">Button_Container_BorderThickness</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateThickness">
												<xsl:with-param name="node" select="LeftButtonBorderThicknessHover"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[State Pressed trigger]-->
							<xsl:call-template name="generateTrigger">
								<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
								<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
								<xsl:with-param name="setters">

									<!--Button_Container_BorderThickness-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId">Button_Container_BorderThickness</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateThickness">
												<xsl:with-param name="node" select="LeftButtonBorderThicknessPressed"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[State Disabled trigger]-->
							<xsl:call-template name="generateTrigger">
								<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
								<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
								<xsl:with-param name="setters">

									<!--Button_Container_BorderThickness-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId">Button_Container_BorderThickness</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateThickness">
												<xsl:with-param name="node" select="LeftButtonBorderThicknessDisabled"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[State Disabled data trigger]-->
							<xsl:call-template name="generateDataTrigger">

								<xsl:with-param name="binding">
									<xsl:call-template name="generateBinding">
										<xsl:with-param name="propertyName">State</xsl:with-param>
										<xsl:with-param name="relativeSource">
											<xsl:call-template name="generateRelativeSource">
												<xsl:with-param name="mode">FindAncestor</xsl:with-param>
												<xsl:with-param name="ancestorType">visuals:SplitButton</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>
								</xsl:with-param>
								
								<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
								
								<xsl:with-param name="setters">

									<!--Button_Container_BorderThickness-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId">Button_Container_BorderThickness</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateThickness">
												<xsl:with-param name="node" select="LeftButtonBorderThicknessDisabledBoth"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

						</xsl:with-param>

					</xsl:call-template>

					<!--ToggleButtonStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">ToggleButton</xsl:with-param>
						<xsl:with-param name="basedOn" select="$rightButtonStyleId"/>
						<xsl:with-param name="key">ToggleButtonStyle</xsl:with-param>
						<xsl:with-param name="setters">

							<!--Button_Container_BorderThickness-->
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">Button_Container_BorderThickness</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateThickness">
										<xsl:with-param name="node" select="RightButtonBorderThicknessNormal"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>

							<!--Button_Container_CornerRadius-->
							<xsl:call-template name="generateCornerRadiusSetter">
								<xsl:with-param name="propertyId">Button_Container_CornerRadius</xsl:with-param>
								<xsl:with-param name="cornerRadiusNode" select="RightButtonCornerRadius"/>
							</xsl:call-template>

							<!--Button_FocusVisualStyle-->
							<xsl:call-template name="generateFocusVisualStyleSetter">
								<xsl:with-param name="propertyId">Button_FocusVisualStyle</xsl:with-param>
								<xsl:with-param name="focusNode" select="RightButtonFocus"/>
								<xsl:with-param name="focusVisualBrush">ToggleButtonFocusVisualBrush</xsl:with-param>
								<xsl:with-param name="scope" select="$id"/>
							</xsl:call-template>

						</xsl:with-param>

						<xsl:with-param name="triggers">

							<!--[State Hover trigger]-->
							<xsl:call-template name="generateTrigger">
								<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
								<xsl:with-param name="propertyValue">Hover</xsl:with-param>
								<xsl:with-param name="setters">

									<!--Button_Container_BorderThickness-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId">Button_Container_BorderThickness</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateThickness">
												<xsl:with-param name="node" select="RightButtonBorderThicknessHover"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[State Pressed trigger]-->
							<xsl:call-template name="generateTrigger">
								<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
								<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
								<xsl:with-param name="setters">

									<!--Button_Container_BorderThickness-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId">Button_Container_BorderThickness</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateThickness">
												<xsl:with-param name="node" select="RightButtonBorderThicknessPressed"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[State Disabled trigger]-->
							<xsl:call-template name="generateTrigger">
								<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
								<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
								<xsl:with-param name="setters">

									<!--Button_Container_BorderThickness-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId">Button_Container_BorderThickness</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateThickness">
												<xsl:with-param name="node" select="RightButtonBorderThicknessDisabled"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[State Disabled data trigger]-->
							<xsl:call-template name="generateDataTrigger">

								<xsl:with-param name="binding">
									<xsl:call-template name="generateBinding">
										<xsl:with-param name="propertyName">State</xsl:with-param>
										<xsl:with-param name="relativeSource">
											<xsl:call-template name="generateRelativeSource">
												<xsl:with-param name="mode">FindAncestor</xsl:with-param>
												<xsl:with-param name="ancestorType">visuals:SplitButton</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>
								</xsl:with-param>

								<xsl:with-param name="propertyValue">Disabled</xsl:with-param>

								<xsl:with-param name="setters">

									<!--Button_Container_BorderThickness-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId">Button_Container_BorderThickness</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateThickness">
												<xsl:with-param name="node" select="RightButtonBorderThicknessDisabledBoth"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

						</xsl:with-param>

					</xsl:call-template>

				</xsl:element>

				<!--SplitButton_Divider_Style-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">SplitButton_Divider_Style</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">DividerStyle</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--SplitButton_MainButton_Style-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">SplitButton_MainButton_Style</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">MainButtonStyle</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--SplitButton_ToggleButton_MenuClosedIcon-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">SplitButton_ToggleButton_MenuClosedIcon</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@RightButtonMenuClosedIconName"/>
				</xsl:call-template>

				<!--SplitButton_ToggleButton_MenuOpenedIcon-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">SplitButton_ToggleButton_MenuOpenedIcon</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@RightButtonMenuOpenedIconName"/>
				</xsl:call-template>

				<!--SplitButton_ToggleButton_Style-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">SplitButton_ToggleButton_Style</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">ToggleButtonStyle</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[State Disabled trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">State</xsl:with-param>
					<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
					<xsl:with-param name="setters">

						<!--SplitButton_Divider_Style-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">SplitButton_Divider_Style</xsl:with-param>
							<xsl:with-param name="resourceName">DividerDisabledAllStyle</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>
				
				<!--[State DisabledMain trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">State</xsl:with-param>
					<xsl:with-param name="propertyValue">DisabledMain</xsl:with-param>
					<xsl:with-param name="setters">

						<!--SplitButton_Divider_Style-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">SplitButton_Divider_Style</xsl:with-param>
							<xsl:with-param name="resourceName">DividerDisabledSomeStyle</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State DisabledContextMenu trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">State</xsl:with-param>
					<xsl:with-param name="propertyValue">DisabledContextMenu</xsl:with-param>
					<xsl:with-param name="setters">

						<!--SplitButton_Divider_Style-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">SplitButton_Divider_Style</xsl:with-param>
							<xsl:with-param name="resourceName">DividerDisabledSomeStyle</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>
				
				<!--[State PressedMain trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">State</xsl:with-param>
					<xsl:with-param name="propertyValue">PressedMain</xsl:with-param>
					<xsl:with-param name="setters">

						<!--SplitButton_Divider_Style-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">SplitButton_Divider_Style</xsl:with-param>
							<xsl:with-param name="resourceName">DividerPressedStyle</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State PressedContextMenu trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">State</xsl:with-param>
					<xsl:with-param name="propertyValue">PressedContextMenu</xsl:with-param>
					<xsl:with-param name="setters">

						<!--SplitButton_Divider_Style-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">SplitButton_Divider_Style</xsl:with-param>
							<xsl:with-param name="resourceName">DividerPressedStyle</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>

		</xsl:call-template>
	</xsl:template>

</xsl:transform>