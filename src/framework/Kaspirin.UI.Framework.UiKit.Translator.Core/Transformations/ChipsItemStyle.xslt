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

	<xsl:template match="ChipsItems">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">ChipsItems</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="ChipsItem">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:variable name="targetType">visuals:ChipsItem</xsl:variable>
		<xsl:variable name="basedOn">ChipsItemUniversal</xsl:variable>
		<xsl:variable name="prefix">ChipsItem_</xsl:variable>

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

							<!--[SelectableState Normal data trigger]-->
							<xsl:call-template name="generateDataTrigger">

								<xsl:with-param name="binding">
									<xsl:call-template name="generateBinding">
										<xsl:with-param name="propertyName">Path=(visuals:StateService.SelectableState)</xsl:with-param>
										<xsl:with-param name="relativeSource">
											<xsl:call-template name="generateRelativeSource">
												<xsl:with-param name="mode">FindAncestor</xsl:with-param>
												<xsl:with-param name="ancestorType">visuals:ChipsItem</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>
								</xsl:with-param>

								<xsl:with-param name="propertyValue">Normal</xsl:with-param>

								<xsl:with-param name="setters">

									<!--BadgeCounter_Background-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId" >BadgeCounter_Background</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateResExtension">
												<xsl:with-param name="key">BadgeCounterBackgroundNormal</xsl:with-param>
												<xsl:with-param name="scope" select="$id"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>

									<!--BadgeCounter_Foreground-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId" >BadgeCounter_Foreground</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateResExtension">
												<xsl:with-param name="key">BadgeCounterForegroundNormal</xsl:with-param>
												<xsl:with-param name="scope" select="$id"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[SelectableState Disabled data trigger]-->
							<xsl:call-template name="generateDataTrigger">

								<xsl:with-param name="binding">
									<xsl:call-template name="generateBinding">
										<xsl:with-param name="propertyName">Path=(visuals:StateService.SelectableState)</xsl:with-param>
										<xsl:with-param name="relativeSource">
											<xsl:call-template name="generateRelativeSource">
												<xsl:with-param name="mode">FindAncestor</xsl:with-param>
												<xsl:with-param name="ancestorType">visuals:ChipsItem</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>
								</xsl:with-param>

								<xsl:with-param name="propertyValue">Disabled</xsl:with-param>

								<xsl:with-param name="setters">

									<!--BadgeCounter_Background-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId" >BadgeCounter_Background</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateResExtension">
												<xsl:with-param name="key">BadgeCounterBackgroundDisabled</xsl:with-param>
												<xsl:with-param name="scope" select="$id"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>

									<!--BadgeCounter_Foreground-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId" >BadgeCounter_Foreground</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateResExtension">
												<xsl:with-param name="key">BadgeCounterForegroundDisabled</xsl:with-param>
												<xsl:with-param name="scope" select="$id"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[SelectableState Hover data trigger]-->
							<xsl:call-template name="generateDataTrigger">

								<xsl:with-param name="binding">
									<xsl:call-template name="generateBinding">
										<xsl:with-param name="propertyName">Path=(visuals:StateService.SelectableState)</xsl:with-param>
										<xsl:with-param name="relativeSource">
											<xsl:call-template name="generateRelativeSource">
												<xsl:with-param name="mode">FindAncestor</xsl:with-param>
												<xsl:with-param name="ancestorType">visuals:ChipsItem</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>
								</xsl:with-param>

								<xsl:with-param name="propertyValue">Hover</xsl:with-param>

								<xsl:with-param name="setters">

									<!--BadgeCounter_Background-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId" >BadgeCounter_Background</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateResExtension">
												<xsl:with-param name="key">BadgeCounterBackgroundHover</xsl:with-param>
												<xsl:with-param name="scope" select="$id"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>

									<!--BadgeCounter_Foreground-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId" >BadgeCounter_Foreground</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateResExtension">
												<xsl:with-param name="key">BadgeCounterForegroundHover</xsl:with-param>
												<xsl:with-param name="scope" select="$id"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[SelectableState SelectedNormal data trigger]-->
							<xsl:call-template name="generateDataTrigger">

								<xsl:with-param name="binding">
									<xsl:call-template name="generateBinding">
										<xsl:with-param name="propertyName">Path=(visuals:StateService.SelectableState)</xsl:with-param>
										<xsl:with-param name="relativeSource">
											<xsl:call-template name="generateRelativeSource">
												<xsl:with-param name="mode">FindAncestor</xsl:with-param>
												<xsl:with-param name="ancestorType">visuals:ChipsItem</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>
								</xsl:with-param>

								<xsl:with-param name="propertyValue">SelectedNormal</xsl:with-param>

								<xsl:with-param name="setters">

									<!--BadgeCounter_Background-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId" >BadgeCounter_Background</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateResExtension">
												<xsl:with-param name="key">BadgeCounterBackgroundSelectedNormal</xsl:with-param>
												<xsl:with-param name="scope" select="$id"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>

									<!--BadgeCounter_Foreground-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId" >BadgeCounter_Foreground</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateResExtension">
												<xsl:with-param name="key">BadgeCounterForegroundSelectedNormal</xsl:with-param>
												<xsl:with-param name="scope" select="$id"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[SelectableState SelectedHover data trigger]-->
							<xsl:call-template name="generateDataTrigger">

								<xsl:with-param name="binding">
									<xsl:call-template name="generateBinding">
										<xsl:with-param name="propertyName">Path=(visuals:StateService.SelectableState)</xsl:with-param>
										<xsl:with-param name="relativeSource">
											<xsl:call-template name="generateRelativeSource">
												<xsl:with-param name="mode">FindAncestor</xsl:with-param>
												<xsl:with-param name="ancestorType">visuals:ChipsItem</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>
								</xsl:with-param>

								<xsl:with-param name="propertyValue">SelectedHover</xsl:with-param>

								<xsl:with-param name="setters">

									<!--BadgeCounter_Background-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId" >BadgeCounter_Background</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateResExtension">
												<xsl:with-param name="key">BadgeCounterBackgroundSelectedHover</xsl:with-param>
												<xsl:with-param name="scope" select="$id"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>

									<!--BadgeCounter_Foreground-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId" >BadgeCounter_Foreground</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateResExtension">
												<xsl:with-param name="key">BadgeCounterForegroundSelectedHover</xsl:with-param>
												<xsl:with-param name="scope" select="$id"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[SelectableState SelectedDisabled data trigger]-->
							<xsl:call-template name="generateDataTrigger">

								<xsl:with-param name="binding">
									<xsl:call-template name="generateBinding">
										<xsl:with-param name="propertyName">Path=(visuals:StateService.SelectableState)</xsl:with-param>
										<xsl:with-param name="relativeSource">
											<xsl:call-template name="generateRelativeSource">
												<xsl:with-param name="mode">FindAncestor</xsl:with-param>
												<xsl:with-param name="ancestorType">visuals:ChipsItem</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>
								</xsl:with-param>

								<xsl:with-param name="propertyValue">SelectedDisabled</xsl:with-param>

								<xsl:with-param name="setters">

									<!--BadgeCounter_Background-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId" >BadgeCounter_Background</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateResExtension">
												<xsl:with-param name="key">BadgeCounterBackgroundSelectedDisabled</xsl:with-param>
												<xsl:with-param name="scope" select="$id"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>

									<!--BadgeCounter_Foreground-->
									<xsl:call-template name="generateUiKitSetterViaAttribute">
										<xsl:with-param name="propertyId" >BadgeCounter_Foreground</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateResExtension">
												<xsl:with-param name="key">BadgeCounterForegroundSelectedDisabled</xsl:with-param>
												<xsl:with-param name="scope" select="$id"/>
											</xsl:call-template>
										</xsl:with-param>
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

					<!--TextStyleSelectedHover-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleSelectedHover</xsl:with-param>

						<xsl:with-param name="setters">

							<!--Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundSelectedHover</xsl:with-param>
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

				<!--ChipsItem_BadgeCounter_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'BadgeCounter_Margin')" />
					<xsl:with-param name="marginNode" select="BadgeCounterMargin" />
				</xsl:call-template>

				<!--ChipsItem_BadgeCounter_Style-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId" select="concat($prefix, 'BadgeCounter_Style')" />
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">BadgeCounterStyle</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>
				
				<!--ChipsItem_CloseIcon_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'CloseIcon_Margin')" />
					<xsl:with-param name="marginNode" select="CloseIconMargin" />
				</xsl:call-template>

				<!--ChipsItem_Container_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'Container_CornerRadius')" />
				</xsl:call-template>

				<!--ChipsItem_Container_Height-->
				<xsl:call-template name="generateHeightSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'Container_Height')" />
				</xsl:call-template>

				<!--ChipsItem_Container_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'Container_Padding')" />
				</xsl:call-template>

				<!--ChipsItem_FocusVisualStyle-->
				<xsl:call-template name="generateFocusVisualStyleSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'FocusVisualStyle')" />
					<xsl:with-param name="scope" select="$id"/>
				</xsl:call-template>
				
				<!--ChipsItem_Icon_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'Icon_Margin')" />
					<xsl:with-param name="marginNode" select="IconMargin" />
				</xsl:call-template>

				<!--ChipsItem_TextStyle-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId" select="concat($prefix, 'TextStyle')" />
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">TextStyle</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--ChipsItem_TextStyleDisabled-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId" select="concat($prefix, 'TextStyleDisabled')" />
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">TextStyleDisabled</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--ChipsItem_TextStyleHover-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId" select="concat($prefix, 'TextStyleHover')" />
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">TextStyleHover</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--ChipsItem_TextStyleSelectedDisabled-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId" select="concat($prefix, 'TextStyleSelectedDisabled')" />
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">TextStyleSelectedDisabled</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--ChipsItem_TextStyleSelectedHover-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId" select="concat($prefix, 'TextStyleSelectedHover')" />
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">TextStyleSelectedHover</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--ChipsItem_TextStyleSelectedNormal-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId" select="concat($prefix, 'TextStyleSelectedNormal')" />
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">TextStyleSelectedNormal</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[State Normal trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
					<xsl:with-param name="propertyValue">Normal</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ChipsItem_Container_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_Background')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundNormal</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Container_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderBrush')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorNormal</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Container_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderThickness')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessNormal"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Icon_Fill-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Icon_Fill')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundNormal</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State Hover trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
					<xsl:with-param name="propertyValue">Hover</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ChipsItem_Container_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_Background')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundHover</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Container_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderBrush')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorHover</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Container_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderThickness')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessHover"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Icon_Fill-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Icon_Fill')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundHover</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State Disabled trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
					<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ChipsItem_Container_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_Background')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundDisabled</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Container_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderBrush')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorDisabled</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Container_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderThickness')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessDisabled"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Icon_Fill-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Icon_Fill')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundDisabled</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State SelectedNormal trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
					<xsl:with-param name="propertyValue">SelectedNormal</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ChipsItem_CloseIcon_Fill-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'CloseIcon_Fill')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">CloseIconForegroundSelectedNormal</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Container_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_Background')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundSelectedNormal</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Container_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderBrush')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorSelectedNormal</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Container_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderThickness')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessSelectedNormal"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Icon_Fill-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Icon_Fill')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundSelectedNormal</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State SelectedHover trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
					<xsl:with-param name="propertyValue">SelectedHover</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ChipsItem_CloseIcon_Fill-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'CloseIcon_Fill')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">CloseIconForegroundSelectedHover</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Container_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_Background')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundSelectedHover</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Container_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderBrush')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorSelectedHover</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Container_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderThickness')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessSelectedHover"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Icon_Fill-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Icon_Fill')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundSelectedHover</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State SelectedDisabled trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.SelectableState</xsl:with-param>
					<xsl:with-param name="propertyValue">SelectedDisabled</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ChipsItem_CloseIcon_Fill-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'CloseIcon_Fill')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">CloseIconForegroundSelectedDisabled</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Container_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_Background')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundSelectedDisabled</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Container_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderBrush')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorSelectedDisabled</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Container_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Container_BorderThickness')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessSelectedDisabled"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--ChipsItem_Icon_Fill-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Icon_Fill')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundSelectedDisabled</xsl:with-param>
									<xsl:with-param name="scope" select="$id"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>
				
	</xsl:template>
</xsl:transform>