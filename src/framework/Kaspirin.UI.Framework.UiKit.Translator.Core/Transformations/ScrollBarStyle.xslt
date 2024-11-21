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

	<xsl:template match="ScrollBars">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">ScrollBars</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="ScrollBar">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">ScrollBar</xsl:with-param>
			<xsl:with-param name="basedOn">ScrollBarUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">

					<!--ThumbStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">Thumb</xsl:with-param>
						<xsl:with-param name="basedOn">ScrollBarThumbUniversal</xsl:with-param>
						<xsl:with-param name="key">ThumbStyle</xsl:with-param>
						<xsl:with-param name="setters">

							<!--ScrollBarThumb_Background-->
							<xsl:if test="ThumbColorNormal">
								<xsl:call-template name="generateUiKitSetterViaAttribute">
									<xsl:with-param name="propertyId">ScrollBarThumb_Background</xsl:with-param>
									<xsl:with-param name="propertyValue">
										<xsl:call-template name="generateResExtension">
											<xsl:with-param name="key">ThumbColorNormal</xsl:with-param>
											<xsl:with-param name="scope" select="$id" />
										</xsl:call-template>
									</xsl:with-param>
								</xsl:call-template>
							</xsl:if>

							<!--ScrollBarThumb_CornerRadius-->
							<xsl:call-template name="generateCornerRadiusSetter">
								<xsl:with-param name="propertyId">ScrollBarThumb_CornerRadius</xsl:with-param>
								<xsl:with-param name="cornerRadiusNode" select="ThumbCornerRadiusNormal"/>
							</xsl:call-template>

						</xsl:with-param>

						<xsl:with-param name="triggers">

							<!--[Disabled trigger]-->
							<xsl:call-template name="generateTrigger">
								<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
								<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
								<xsl:with-param name="setters">

									<!--ScrollBarThumb_Background-->
									<xsl:if test="ThumbColorDisabled">
										<xsl:call-template name="generateUiKitSetterViaAttribute">
											<xsl:with-param name="propertyId">ScrollBarThumb_Background</xsl:with-param>
											<xsl:with-param name="propertyValue">
												<xsl:call-template name="generateResExtension">
													<xsl:with-param name="key">ThumbColorDisabled</xsl:with-param>
													<xsl:with-param name="scope" select="$id" />
												</xsl:call-template>
											</xsl:with-param>
										</xsl:call-template>
									</xsl:if>

									<!--ScrollBarThumb_CornerRadius-->
									<xsl:call-template name="generateCornerRadiusSetter">
										<xsl:with-param name="propertyId">ScrollBarThumb_CornerRadius</xsl:with-param>
										<xsl:with-param name="cornerRadiusNode" select="ThumbCornerRadiusDisabled"/>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[Hover trigger]-->
							<xsl:call-template name="generateTrigger">
								<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
								<xsl:with-param name="propertyValue">Hover</xsl:with-param>
								<xsl:with-param name="setters">

									<!--ScrollBarThumb_Background-->
									<xsl:if test="ThumbColorHover">
										<xsl:call-template name="generateUiKitSetterViaAttribute">
											<xsl:with-param name="propertyId">ScrollBarThumb_Background</xsl:with-param>
											<xsl:with-param name="propertyValue">
												<xsl:call-template name="generateResExtension">
													<xsl:with-param name="key">ThumbColorHover</xsl:with-param>
													<xsl:with-param name="scope" select="$id" />
												</xsl:call-template>
											</xsl:with-param>
										</xsl:call-template>
									</xsl:if>

									<!--ScrollBarThumb_CornerRadius-->
									<xsl:call-template name="generateCornerRadiusSetter">
										<xsl:with-param name="propertyId">ScrollBarThumb_CornerRadius</xsl:with-param>
										<xsl:with-param name="cornerRadiusNode" select="ThumbCornerRadiusHover"/>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[Pressed trigger]-->
							<xsl:call-template name="generateTrigger">
								<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
								<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
								<xsl:with-param name="setters">

									<!--ScrollBarThumb_Background-->
									<xsl:if test="ThumbColorPressed">
										<xsl:call-template name="generateUiKitSetterViaAttribute">
											<xsl:with-param name="propertyId">ScrollBarThumb_Background</xsl:with-param>
											<xsl:with-param name="propertyValue">
												<xsl:call-template name="generateResExtension">
													<xsl:with-param name="key">ThumbColorPressed</xsl:with-param>
													<xsl:with-param name="scope" select="$id" />
												</xsl:call-template>
											</xsl:with-param>
										</xsl:call-template>
									</xsl:if>

									<!--ScrollBarThumb_CornerRadius-->
									<xsl:call-template name="generateCornerRadiusSetter">
										<xsl:with-param name="propertyId">ScrollBarThumb_CornerRadius</xsl:with-param>
										<xsl:with-param name="cornerRadiusNode" select="ThumbCornerRadiusPressed"/>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[Horizontal Disabled multi trigger]-->
							<xsl:call-template name="generateMultiDataTrigger">
								<xsl:with-param name="conditions">

									<!--Orientation == Horizontal-->
									<xsl:call-template name="generateCondition">
										<xsl:with-param name="binding">
											<xsl:call-template name="generateBinding">
												<xsl:with-param name="propertyName">Orientation</xsl:with-param>
												<xsl:with-param name="relativeSource">
													<xsl:call-template name="generateRelativeSource">
														<xsl:with-param name="mode">FindAncestor</xsl:with-param>
														<xsl:with-param name="ancestorType">ScrollBar</xsl:with-param>
													</xsl:call-template>
												</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
										<xsl:with-param name="propertyValue">Horizontal</xsl:with-param>
									</xsl:call-template>

									<!--StateService.State == Disabled-->
									<xsl:call-template name="generateCondition">
										<xsl:with-param name="binding">
											<xsl:call-template name="generateBinding">
												<xsl:with-param name="path">visuals:StateService.State</xsl:with-param>
												<xsl:with-param name="relativeSource">
													<xsl:call-template name="generateRelativeSource"/>
												</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
										<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>

								<xsl:with-param name="setters">

									<!--ScrollBarThumb_Margin-->
									<xsl:call-template name="generateMarginSetter">
										<xsl:with-param name="propertyId">ScrollBarThumb_Margin</xsl:with-param>
										<xsl:with-param name="marginNode" select="ThumbHorizontalMarginDisabled" />
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[Horizontal Hover multi trigger]-->
							<xsl:call-template name="generateMultiDataTrigger">
								<xsl:with-param name="conditions">

									<!--Orientation == Horizontal-->
									<xsl:call-template name="generateCondition">
										<xsl:with-param name="binding">
											<xsl:call-template name="generateBinding">
												<xsl:with-param name="propertyName">Orientation</xsl:with-param>
												<xsl:with-param name="relativeSource">
													<xsl:call-template name="generateRelativeSource">
														<xsl:with-param name="mode">FindAncestor</xsl:with-param>
														<xsl:with-param name="ancestorType">ScrollBar</xsl:with-param>
													</xsl:call-template>
												</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
										<xsl:with-param name="propertyValue">Horizontal</xsl:with-param>
									</xsl:call-template>

									<!--StateService.State == Hover-->
									<xsl:call-template name="generateCondition">
										<xsl:with-param name="binding">
											<xsl:call-template name="generateBinding">
												<xsl:with-param name="path">visuals:StateService.State</xsl:with-param>
												<xsl:with-param name="relativeSource">
													<xsl:call-template name="generateRelativeSource"/>
												</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
										<xsl:with-param name="propertyValue">Hover</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>

								<xsl:with-param name="setters">

									<!--ScrollBarThumb_Margin-->
									<xsl:call-template name="generateMarginSetter">
										<xsl:with-param name="propertyId">ScrollBarThumb_Margin</xsl:with-param>
										<xsl:with-param name="marginNode" select="ThumbHorizontalMarginHover" />
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[Horizontal Normal multi trigger]-->
							<xsl:call-template name="generateMultiDataTrigger">
								<xsl:with-param name="conditions">

									<!--Orientation == Horizontal-->
									<xsl:call-template name="generateCondition">
										<xsl:with-param name="binding">
											<xsl:call-template name="generateBinding">
												<xsl:with-param name="propertyName">Orientation</xsl:with-param>
												<xsl:with-param name="relativeSource">
													<xsl:call-template name="generateRelativeSource">
														<xsl:with-param name="mode">FindAncestor</xsl:with-param>
														<xsl:with-param name="ancestorType">ScrollBar</xsl:with-param>
													</xsl:call-template>
												</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
										<xsl:with-param name="propertyValue">Horizontal</xsl:with-param>
									</xsl:call-template>

									<!--StateService.State == Normal-->
									<xsl:call-template name="generateCondition">
										<xsl:with-param name="binding">
											<xsl:call-template name="generateBinding">
												<xsl:with-param name="path">visuals:StateService.State</xsl:with-param>
												<xsl:with-param name="relativeSource">
													<xsl:call-template name="generateRelativeSource"/>
												</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
										<xsl:with-param name="propertyValue">Normal</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>

								<xsl:with-param name="setters">

									<!--ScrollBarThumb_Margin-->
									<xsl:call-template name="generateMarginSetter">
										<xsl:with-param name="propertyId">ScrollBarThumb_Margin</xsl:with-param>
										<xsl:with-param name="marginNode" select="ThumbHorizontalMarginNormal" />
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[Horizontal Pressed multi trigger]-->
							<xsl:call-template name="generateMultiDataTrigger">
								<xsl:with-param name="conditions">

									<!--Orientation == Horizontal-->
									<xsl:call-template name="generateCondition">
										<xsl:with-param name="binding">
											<xsl:call-template name="generateBinding">
												<xsl:with-param name="propertyName">Orientation</xsl:with-param>
												<xsl:with-param name="relativeSource">
													<xsl:call-template name="generateRelativeSource">
														<xsl:with-param name="mode">FindAncestor</xsl:with-param>
														<xsl:with-param name="ancestorType">ScrollBar</xsl:with-param>
													</xsl:call-template>
												</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
										<xsl:with-param name="propertyValue">Horizontal</xsl:with-param>
									</xsl:call-template>

									<!--StateService.State == Pressed-->
									<xsl:call-template name="generateCondition">
										<xsl:with-param name="binding">
											<xsl:call-template name="generateBinding">
												<xsl:with-param name="path">visuals:StateService.State</xsl:with-param>
												<xsl:with-param name="relativeSource">
													<xsl:call-template name="generateRelativeSource"/>
												</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
										<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>

								<xsl:with-param name="setters">

									<!--ScrollBarThumb_Margin-->
									<xsl:call-template name="generateMarginSetter">
										<xsl:with-param name="propertyId">ScrollBarThumb_Margin</xsl:with-param>
										<xsl:with-param name="marginNode" select="ThumbHorizontalMarginPressed" />
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[Vertical Disabled multi trigger]-->
							<xsl:call-template name="generateMultiDataTrigger">
								<xsl:with-param name="conditions">

									<!--Orientation == Vertical-->
									<xsl:call-template name="generateCondition">
										<xsl:with-param name="binding">
											<xsl:call-template name="generateBinding">
												<xsl:with-param name="propertyName">Orientation</xsl:with-param>
												<xsl:with-param name="relativeSource">
													<xsl:call-template name="generateRelativeSource">
														<xsl:with-param name="mode">FindAncestor</xsl:with-param>
														<xsl:with-param name="ancestorType">ScrollBar</xsl:with-param>
													</xsl:call-template>
												</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
										<xsl:with-param name="propertyValue">Vertical</xsl:with-param>
									</xsl:call-template>

									<!--StateService.State == Disabled-->
									<xsl:call-template name="generateCondition">
										<xsl:with-param name="binding">
											<xsl:call-template name="generateBinding">
												<xsl:with-param name="path">visuals:StateService.State</xsl:with-param>
												<xsl:with-param name="relativeSource">
													<xsl:call-template name="generateRelativeSource"/>
												</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
										<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>

								<xsl:with-param name="setters">

									<!--ScrollBarThumb_Margin-->
									<xsl:call-template name="generateMarginSetter">
										<xsl:with-param name="propertyId">ScrollBarThumb_Margin</xsl:with-param>
										<xsl:with-param name="marginNode" select="ThumbVerticalMarginDisabled" />
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[Vertical Hover multi trigger]-->
							<xsl:call-template name="generateMultiDataTrigger">
								<xsl:with-param name="conditions">

									<!--Orientation == Vertical-->
									<xsl:call-template name="generateCondition">
										<xsl:with-param name="binding">
											<xsl:call-template name="generateBinding">
												<xsl:with-param name="propertyName">Orientation</xsl:with-param>
												<xsl:with-param name="relativeSource">
													<xsl:call-template name="generateRelativeSource">
														<xsl:with-param name="mode">FindAncestor</xsl:with-param>
														<xsl:with-param name="ancestorType">ScrollBar</xsl:with-param>
													</xsl:call-template>
												</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
										<xsl:with-param name="propertyValue">Vertical</xsl:with-param>
									</xsl:call-template>

									<!--StateService.State == Hover-->
									<xsl:call-template name="generateCondition">
										<xsl:with-param name="binding">
											<xsl:call-template name="generateBinding">
												<xsl:with-param name="path">visuals:StateService.State</xsl:with-param>
												<xsl:with-param name="relativeSource">
													<xsl:call-template name="generateRelativeSource"/>
												</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
										<xsl:with-param name="propertyValue">Hover</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>

								<xsl:with-param name="setters">

									<!--ScrollBarThumb_Margin-->
									<xsl:call-template name="generateMarginSetter">
										<xsl:with-param name="propertyId">ScrollBarThumb_Margin</xsl:with-param>
										<xsl:with-param name="marginNode" select="ThumbVerticalMarginHover" />
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[Vertical Normal multi trigger]-->
							<xsl:call-template name="generateMultiDataTrigger">
								<xsl:with-param name="conditions">

									<!--Orientation == Vertical-->
									<xsl:call-template name="generateCondition">
										<xsl:with-param name="binding">
											<xsl:call-template name="generateBinding">
												<xsl:with-param name="propertyName">Orientation</xsl:with-param>
												<xsl:with-param name="relativeSource">
													<xsl:call-template name="generateRelativeSource">
														<xsl:with-param name="mode">FindAncestor</xsl:with-param>
														<xsl:with-param name="ancestorType">ScrollBar</xsl:with-param>
													</xsl:call-template>
												</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
										<xsl:with-param name="propertyValue">Vertical</xsl:with-param>
									</xsl:call-template>

									<!--StateService.State == Normal-->
									<xsl:call-template name="generateCondition">
										<xsl:with-param name="binding">
											<xsl:call-template name="generateBinding">
												<xsl:with-param name="path">visuals:StateService.State</xsl:with-param>
												<xsl:with-param name="relativeSource">
													<xsl:call-template name="generateRelativeSource"/>
												</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
										<xsl:with-param name="propertyValue">Normal</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>

								<xsl:with-param name="setters">

									<!--ScrollBarThumb_Margin-->
									<xsl:call-template name="generateMarginSetter">
										<xsl:with-param name="propertyId">ScrollBarThumb_Margin</xsl:with-param>
										<xsl:with-param name="marginNode" select="ThumbVerticalMarginNormal" />
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[Vertical Pressed multi trigger]-->
							<xsl:call-template name="generateMultiDataTrigger">
								<xsl:with-param name="conditions">

									<!--Orientation == Vertical-->
									<xsl:call-template name="generateCondition">
										<xsl:with-param name="binding">
											<xsl:call-template name="generateBinding">
												<xsl:with-param name="propertyName">Orientation</xsl:with-param>
												<xsl:with-param name="relativeSource">
													<xsl:call-template name="generateRelativeSource">
														<xsl:with-param name="mode">FindAncestor</xsl:with-param>
														<xsl:with-param name="ancestorType">ScrollBar</xsl:with-param>
													</xsl:call-template>
												</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
										<xsl:with-param name="propertyValue">Vertical</xsl:with-param>
									</xsl:call-template>

									<!--StateService.State == Pressed-->
									<xsl:call-template name="generateCondition">
										<xsl:with-param name="binding">
											<xsl:call-template name="generateBinding">
												<xsl:with-param name="path">visuals:StateService.State</xsl:with-param>
												<xsl:with-param name="relativeSource">
													<xsl:call-template name="generateRelativeSource"/>
												</xsl:with-param>
											</xsl:call-template>
										</xsl:with-param>
										<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
									</xsl:call-template>

								</xsl:with-param>

								<xsl:with-param name="setters">

									<!--ScrollBarThumb_Margin-->
									<xsl:call-template name="generateMarginSetter">
										<xsl:with-param name="propertyId">ScrollBarThumb_Margin</xsl:with-param>
										<xsl:with-param name="marginNode" select="ThumbVerticalMarginPressed" />
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

						</xsl:with-param>
					</xsl:call-template>

				</xsl:element>

				<!--ScrollBar_Background-->
				<xsl:if test="BackgroundNormal">
					<xsl:call-template name="generateUiKitSetterViaAttribute">
						<xsl:with-param name="propertyId">ScrollBar_Background</xsl:with-param>
						<xsl:with-param name="propertyValue">
							<xsl:call-template name="generateResExtension">
								<xsl:with-param name="key">BackgroundNormal</xsl:with-param>
								<xsl:with-param name="scope" select="$id" />
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>
				</xsl:if>

				<!--ScrollBar_Size-->
				<xsl:if test="@ThicknessNormal">
					<xsl:call-template name="generateUiKitSetterViaAttribute">
						<xsl:with-param name="propertyId">ScrollBar_Size</xsl:with-param>
						<xsl:with-param name="propertyValue" select = "@ThicknessNormal" />
					</xsl:call-template>
				</xsl:if>

				<!--ScrollBar_ThumbStyle-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">ScrollBar_ThumbStyle</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">ThumbStyle</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Disabled trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ScrollBar_Size-->
						<xsl:if test="@ThicknessDisabled">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">ScrollBar_Size</xsl:with-param>
								<xsl:with-param name="propertyValue" select = "@ThicknessDisabled" />
							</xsl:call-template>
						</xsl:if>

						<!--ScrollBar_Background-->
						<xsl:if test="BackgroundDisabled">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">ScrollBar_Background</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">BackgroundDisabled</xsl:with-param>
										<xsl:with-param name="scope" select="$id" />
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:if>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Hover trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Hover</xsl:with-param>
					<xsl:with-param name="setters">

						<!--ScrollBar_Size-->
						<xsl:if test="@ThicknessHover">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">ScrollBar_Size</xsl:with-param>
								<xsl:with-param name="propertyValue" select = "@ThicknessHover" />
							</xsl:call-template>
						</xsl:if>

						<!--ScrollBar_Background-->
						<xsl:if test="BackgroundHover">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">ScrollBar_Background</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">BackgroundHover</xsl:with-param>
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

						<!--ScrollBar_Size-->
						<xsl:if test="@ThicknessPressed">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">ScrollBar_Size</xsl:with-param>
								<xsl:with-param name="propertyValue" select = "@ThicknessPressed" />
							</xsl:call-template>
						</xsl:if>

						<!--ScrollBar_Background-->
						<xsl:if test="BackgroundPressed">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">ScrollBar_Background</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">BackgroundPressed</xsl:with-param>
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