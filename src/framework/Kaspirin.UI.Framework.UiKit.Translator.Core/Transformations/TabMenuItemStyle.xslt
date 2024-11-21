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

	<xsl:template match="TabMenuItems">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">TabMenuItems</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="TabMenuItem">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:variable name="targetType">visuals:TabMenuItem</xsl:variable>
		<xsl:variable name="basedOn">TabMenuItemUniversal</xsl:variable>
		<xsl:variable name="prefix">TabMenuItem_</xsl:variable>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType" select="$targetType" />
			<xsl:with-param name="basedOn" select="$basedOn" />
			<xsl:with-param name="key" select="$id" />
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">

					<!--BadgeCounterStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">visuals:BadgeCounter</xsl:with-param>
						<xsl:with-param name="basedOn" select="@BadgeCounterStyleId"/>
						<xsl:with-param name="key">BadgeCounterStyle</xsl:with-param>

						<xsl:with-param name="triggers">

							<!--[SelectableState = Normal]-->
							<xsl:call-template name="generateDataTrigger">

								<xsl:with-param name="binding">
									<xsl:call-template name="generateBinding">
										<xsl:with-param name="propertyName">Path=(visuals:StateService.SelectableState)</xsl:with-param>
										<xsl:with-param name="relativeSource">
											<xsl:call-template name="generateRelativeSource">
												<xsl:with-param name="mode">FindAncestor</xsl:with-param>
												<xsl:with-param name="ancestorType">visuals:TabMenuItem</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>
								</xsl:with-param>

								<xsl:with-param name="propertyValue">Normal</xsl:with-param>

								<xsl:with-param name="setters">

									<!--BadgeCounter_Background-->
									<xsl:call-template name="generateBrushSetter">
										<xsl:with-param name="propertyId" >BadgeCounter_Background</xsl:with-param>
										<xsl:with-param name="brushName">BadgeCounterBackgroundNormal</xsl:with-param>
										<xsl:with-param name="scopeName" select="$id"/>
									</xsl:call-template>

									<!--BadgeCounter_Foreground-->
									<xsl:call-template name="generateBrushSetter">
										<xsl:with-param name="propertyId" >BadgeCounter_Foreground</xsl:with-param>
										<xsl:with-param name="brushName">BadgeCounterForegroundNormal</xsl:with-param>
										<xsl:with-param name="scopeName" select="$id"/>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[SelectableState = Disabled]-->
							<xsl:call-template name="generateDataTrigger">

								<xsl:with-param name="binding">
									<xsl:call-template name="generateBinding">
										<xsl:with-param name="propertyName">Path=(visuals:StateService.SelectableState)</xsl:with-param>
										<xsl:with-param name="relativeSource">
											<xsl:call-template name="generateRelativeSource">
												<xsl:with-param name="mode">FindAncestor</xsl:with-param>
												<xsl:with-param name="ancestorType">visuals:TabMenuItem</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>
								</xsl:with-param>

								<xsl:with-param name="propertyValue">Disabled</xsl:with-param>

								<xsl:with-param name="setters">

									<!--BadgeCounter_Background-->
									<xsl:call-template name="generateBrushSetter">
										<xsl:with-param name="propertyId" >BadgeCounter_Background</xsl:with-param>
										<xsl:with-param name="brushName">BadgeCounterBackgroundDisabled</xsl:with-param>
										<xsl:with-param name="scopeName" select="$id"/>
									</xsl:call-template>

									<!--BadgeCounter_Foreground-->
									<xsl:call-template name="generateBrushSetter">
										<xsl:with-param name="propertyId" >BadgeCounter_Foreground</xsl:with-param>
										<xsl:with-param name="brushName">BadgeCounterForegroundDisabled</xsl:with-param>
										<xsl:with-param name="scopeName" select="$id"/>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[SelectableState = Hover]-->
							<xsl:call-template name="generateDataTrigger">

								<xsl:with-param name="binding">
									<xsl:call-template name="generateBinding">
										<xsl:with-param name="propertyName">Path=(visuals:StateService.SelectableState)</xsl:with-param>
										<xsl:with-param name="relativeSource">
											<xsl:call-template name="generateRelativeSource">
												<xsl:with-param name="mode">FindAncestor</xsl:with-param>
												<xsl:with-param name="ancestorType">visuals:TabMenuItem</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>
								</xsl:with-param>

								<xsl:with-param name="propertyValue">Hover</xsl:with-param>

								<xsl:with-param name="setters">

									<!--BadgeCounter_Background-->
									<xsl:call-template name="generateBrushSetter">
										<xsl:with-param name="propertyId" >BadgeCounter_Background</xsl:with-param>
										<xsl:with-param name="brushName">BadgeCounterBackgroundHover</xsl:with-param>
										<xsl:with-param name="scopeName" select="$id"/>
									</xsl:call-template>

									<!--BadgeCounter_Foreground-->
									<xsl:call-template name="generateBrushSetter">
										<xsl:with-param name="propertyId" >BadgeCounter_Foreground</xsl:with-param>
										<xsl:with-param name="brushName">BadgeCounterForegroundHover</xsl:with-param>
										<xsl:with-param name="scopeName" select="$id"/>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[SelectableState = SelectedNormal]-->
							<xsl:call-template name="generateDataTrigger">

								<xsl:with-param name="binding">
									<xsl:call-template name="generateBinding">
										<xsl:with-param name="propertyName">Path=(visuals:StateService.SelectableState)</xsl:with-param>
										<xsl:with-param name="relativeSource">
											<xsl:call-template name="generateRelativeSource">
												<xsl:with-param name="mode">FindAncestor</xsl:with-param>
												<xsl:with-param name="ancestorType">visuals:TabMenuItem</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>
								</xsl:with-param>

								<xsl:with-param name="propertyValue">SelectedNormal</xsl:with-param>

								<xsl:with-param name="setters">

									<!--BadgeCounter_Background-->
									<xsl:call-template name="generateBrushSetter">
										<xsl:with-param name="propertyId" >BadgeCounter_Background</xsl:with-param>
										<xsl:with-param name="brushName">BadgeCounterBackgroundSelectedNormal</xsl:with-param>
										<xsl:with-param name="scopeName" select="$id"/>
									</xsl:call-template>

									<!--BadgeCounter_Foreground-->
									<xsl:call-template name="generateBrushSetter">
										<xsl:with-param name="propertyId" >BadgeCounter_Foreground</xsl:with-param>
										<xsl:with-param name="brushName">BadgeCounterForegroundSelectedNormal</xsl:with-param>
										<xsl:with-param name="scopeName" select="$id"/>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[SelectableState = SelectedDisabled]-->
							<xsl:call-template name="generateDataTrigger">

								<xsl:with-param name="binding">
									<xsl:call-template name="generateBinding">
										<xsl:with-param name="propertyName">Path=(visuals:StateService.SelectableState)</xsl:with-param>
										<xsl:with-param name="relativeSource">
											<xsl:call-template name="generateRelativeSource">
												<xsl:with-param name="mode">FindAncestor</xsl:with-param>
												<xsl:with-param name="ancestorType">visuals:TabMenuItem</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>
								</xsl:with-param>

								<xsl:with-param name="propertyValue">SelectedDisabled</xsl:with-param>

								<xsl:with-param name="setters">

									<!--BadgeCounter_Background-->
									<xsl:call-template name="generateBrushSetter">
										<xsl:with-param name="propertyId" >BadgeCounter_Background</xsl:with-param>
										<xsl:with-param name="brushName">BadgeCounterBackgroundSelectedDisabled</xsl:with-param>
										<xsl:with-param name="scopeName" select="$id"/>
									</xsl:call-template>

									<!--BadgeCounter_Foreground-->
									<xsl:call-template name="generateBrushSetter">
										<xsl:with-param name="propertyId" >BadgeCounter_Foreground</xsl:with-param>
										<xsl:with-param name="brushName">BadgeCounterForegroundSelectedDisabled</xsl:with-param>
										<xsl:with-param name="scopeName" select="$id"/>
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

							<!--Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundNormal</xsl:with-param>
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

							<!--Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundDisabled</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleHover-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleHover</xsl:with-param>

						<xsl:with-param name="setters">

							<!--Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundHover</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleSelectedDisabled-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleSelectedDisabled</xsl:with-param>

						<xsl:with-param name="setters">

							<!--Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundSelectedDisabled</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleSelectedNormal-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleSelectedNormal</xsl:with-param>

						<xsl:with-param name="setters">

							<!--Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundSelectedNormal</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

				</xsl:element>

				<!--TabMenuItem_BadgeCounter_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'BadgeCounter_Margin')" />
					<xsl:with-param name="marginNode" select="BadgeCounterMargin" />
				</xsl:call-template>

				<!--TabMenuItem_BadgeCounter_Style-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'BadgeCounter_Style')" />
					<xsl:with-param name="resourceName">BadgeCounterStyle</xsl:with-param>
				</xsl:call-template>

				<!--TabMenuItem_Container_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'Container_CornerRadius')" />
				</xsl:call-template>

				<!--TabMenuItem_Container_Height-->
				<xsl:call-template name="generateHeightSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'Container_Height')" />
				</xsl:call-template>

				<!--TabMenuItem_Container_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'Container_Padding')" />
				</xsl:call-template>

				<!--TabMenuItem_FocusVisualStyle-->
				<xsl:call-template name="generateFocusVisualStyleSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'FocusVisualStyle')" />
					<xsl:with-param name="scope" select="$id"/>
				</xsl:call-template>

				<!--TabMenuItem_Icon_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'Icon_Margin')" />
					<xsl:with-param name="marginNode" select="IconMargin" />
				</xsl:call-template>

				<!--TabMenuItem_TextStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'TextStyle')" />
					<xsl:with-param name="resourceName">TextStyle</xsl:with-param>
				</xsl:call-template>

				<!--TabMenuItem_TextStyleDisabled-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'TextStyleDisabled')" />
					<xsl:with-param name="resourceName">TextStyleDisabled</xsl:with-param>
				</xsl:call-template>

				<!--TabMenuItem_TextStyleHover-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'TextStyleHover')" />
					<xsl:with-param name="resourceName">TextStyleHover</xsl:with-param>
				</xsl:call-template>

				<!--TabMenuItem_TextStyleSelectedDisabled-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'TextStyleSelectedDisabled')" />
					<xsl:with-param name="resourceName">TextStyleSelectedDisabled</xsl:with-param>
				</xsl:call-template>

				<!--TabMenuItem_TextStyleSelectedNormal-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'TextStyleSelectedNormal')" />
					<xsl:with-param name="resourceName">TextStyleSelectedNormal</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Content = Null]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Content</xsl:with-param>
					<xsl:with-param name="propertyValue">{x:Null}</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TabMenuItem_Container_Padding-->
						<xsl:call-template name="generatePaddingSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_Padding')" />
							<xsl:with-param name="paddingNode" select="PaddingIconOnly" />
						</xsl:call-template>

						<!--TabMenuItem_Icon_Margin-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Icon_Margin')" />
							<xsl:with-param name="propertyValue">0</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>


				<!--[State = Normal]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
					<xsl:with-param name="propertyValue">Normal</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TabMenuItem_Container_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_Background')" />
							<xsl:with-param name="brushName">BackgroundNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--TabMenuItem_Container_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderBrush')" />
							<xsl:with-param name="brushName">BorderColorNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--TabMenuItem_Container_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderThickness')" />
							<xsl:with-param name="thicknessNode" select="BorderThicknessNormal"/>
						</xsl:call-template>

						<!--TabMenuItem_Icon_Fill-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Icon_Fill')" />
							<xsl:with-param name="brushName">IconForegroundNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State = Hover]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
					<xsl:with-param name="propertyValue">Hover</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TabMenuItem_Container_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_Background')" />
							<xsl:with-param name="brushName">BackgroundHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--TabMenuItem_Container_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderBrush')" />
							<xsl:with-param name="brushName">BorderColorHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--TabMenuItem_Container_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderThickness')" />
							<xsl:with-param name="thicknessNode" select="BorderThicknessHover"/>
						</xsl:call-template>

						<!--TabMenuItem_Icon_Fill-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Icon_Fill')" />
							<xsl:with-param name="brushName">IconForegroundHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State = Disabled]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
					<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TabMenuItem_Container_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_Background')" />
							<xsl:with-param name="brushName">BackgroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--TabMenuItem_Container_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderBrush')" />
							<xsl:with-param name="brushName">BorderColorDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--TabMenuItem_Container_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderThickness')" />
							<xsl:with-param name="thicknessNode" select="BorderThicknessDisabled"/>
						</xsl:call-template>

						<!--TabMenuItem_Icon_Fill-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Icon_Fill')" />
							<xsl:with-param name="brushName">IconForegroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State = SelectedNormal]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
					<xsl:with-param name="propertyValue">SelectedNormal</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TabMenuItem_Container_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_Background')" />
							<xsl:with-param name="brushName">BackgroundSelectedNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--TabMenuItem_Container_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderBrush')" />
							<xsl:with-param name="brushName">BorderColorSelectedNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--TabMenuItem_Container_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderThickness')" />
							<xsl:with-param name="thicknessNode" select="BorderThicknessSelectedNormal"/>
						</xsl:call-template>

						<!--TabMenuItem_Icon_Fill-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Icon_Fill')" />
							<xsl:with-param name="brushName">IconForegroundSelectedNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State = SelectedDisabled]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
					<xsl:with-param name="propertyValue">SelectedDisabled</xsl:with-param>
					<xsl:with-param name="setters">

						<!--TabMenuItem_Container_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_Background')" />
							<xsl:with-param name="brushName">BackgroundSelectedDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--TabMenuItem_Container_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderBrush')" />
							<xsl:with-param name="brushName">BorderColorSelectedDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

						<!--TabMenuItem_Container_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderThickness')" />
							<xsl:with-param name="thicknessNode" select="BorderThicknessSelectedDisabled"/>
						</xsl:call-template>

						<!--TabMenuItem_Icon_Fill-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Icon_Fill')" />
							<xsl:with-param name="brushName">IconForegroundSelectedDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id"/>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>

	</xsl:template>
</xsl:transform>