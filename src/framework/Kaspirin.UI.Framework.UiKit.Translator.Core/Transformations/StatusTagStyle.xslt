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

	<xsl:template match="StatusTags">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">StatusTags</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="StatusTag">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:StatusTag</xsl:with-param>
			<xsl:with-param name="basedOn">StatusTagUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">

					<!--TextStyleDanger-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleDanger</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundDanger</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleInfo-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleInfo</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundInfo</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleNeutral-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleNeutral</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundNeutral</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStylePositive-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStylePositive</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundPositive</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleWarning-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleWarning</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundWarning</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

				</xsl:element>

				<!--StatusTag_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId">StatusTag_CornerRadius</xsl:with-param>
				</xsl:call-template>

				<!--StatusTag_Height-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId" >StatusTag_Height</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@Height" />
				</xsl:call-template>

				<!--StatusTag_Icon_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">StatusTag_Icon_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="IconMargin" />
				</xsl:call-template>

				<!--StatusTag_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">StatusTag_Padding</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Danger trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Danger</xsl:with-param>
					<xsl:with-param name="setters">

						<!--StatusTag_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundDanger</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorDanger</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessDanger"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_Icon_Color-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_Icon_Color</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundDanger</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_Icon_Name-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_Icon_Name</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@IconNameDanger" />
						</xsl:call-template>

						<!--StatusTag_TextStyle-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_TextStyle</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateStaticResource">
									<xsl:with-param name="key">TextStyleDanger</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Info trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Info</xsl:with-param>
					<xsl:with-param name="setters">

						<!--StatusTag_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundInfo</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorInfo</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessInfo"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_Icon_Color-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_Icon_Color</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundInfo</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_Icon_Name-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_Icon_Name</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@IconNameInfo" />
						</xsl:call-template>

						<!--StatusTag_TextStyle-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_TextStyle</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateStaticResource">
									<xsl:with-param name="key">TextStyleInfo</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Neutral trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Neutral</xsl:with-param>
					<xsl:with-param name="setters">

						<!--StatusTag_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundNeutral</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorNeutral</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessNeutral"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_Icon_Color-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_Icon_Color</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundNeutral</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_Icon_Name-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_Icon_Name</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@IconNameNeutral" />
						</xsl:call-template>

						<!--StatusTag_TextStyle-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_TextStyle</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateStaticResource">
									<xsl:with-param name="key">TextStyleNeutral</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Positive trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Positive</xsl:with-param>
					<xsl:with-param name="setters">

						<!--StatusTag_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundPositive</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorPositive</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessPositive"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_Icon_Color-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_Icon_Color</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundPositive</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_Icon_Name-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_Icon_Name</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@IconNamePositive" />
						</xsl:call-template>

						<!--StatusTag_TextStyle-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_TextStyle</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateStaticResource">
									<xsl:with-param name="key">TextStylePositive</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Warning trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Warning</xsl:with-param>
					<xsl:with-param name="setters">

						<!--StatusTag_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundWarning</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorWarning</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessWarning"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_Icon_Color-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_Icon_Color</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundWarning</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--StatusTag_Icon_Name-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_Icon_Name</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@IconNameWarning" />
						</xsl:call-template>

						<!--StatusTag_TextStyle-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">StatusTag_TextStyle</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateStaticResource">
									<xsl:with-param name="key">TextStyleWarning</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

</xsl:transform>