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

	<xsl:template match="Tags">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">Tags</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="Tag">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:Tag</xsl:with-param>
			<xsl:with-param name="basedOn">TagUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">

					<!--TextStyleEmerald-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleEmerald</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundEmerald</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleGrass-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleGrass</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundGrass</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleMarina-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleMarina</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundMarina</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleMarengo-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleMarengo</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundMarengo</xsl:with-param>
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

					<!--TextStyleOrange-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleOrange</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundOrange</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStylePurple-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStylePurple</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundPurple</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleRed-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleRed</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundRed</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleViolet-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleViolet</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundViolet</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleYellow-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleYellow</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundYellow</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

				</xsl:element>

				<!--Tag_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId">Tag_CornerRadius</xsl:with-param>
				</xsl:call-template>

				<!--Tag_Height-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId" >Tag_Height</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@Height" />
				</xsl:call-template>

				<!--Tag_Icon_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">Tag_Icon_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="IconMargin" />
				</xsl:call-template>

				<!--Tag_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">Tag_Padding</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Emerald trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Emerald</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Tag_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundEmerald</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorEmerald</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessEmerald"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_Icon_Color-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Icon_Color</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundEmerald</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_TextStyle-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_TextStyle</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateStaticResource">
									<xsl:with-param name="key">TextStyleEmerald</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Grass trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Grass</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Tag_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundGrass</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorGrass</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessGrass"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_Icon_Color-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Icon_Color</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundGrass</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_TextStyle-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_TextStyle</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateStaticResource">
									<xsl:with-param name="key">TextStyleGrass</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Marina trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Marina</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Tag_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundMarina</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorMarina</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessMarina"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_Icon_Color-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Icon_Color</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundMarina</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_TextStyle-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_TextStyle</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateStaticResource">
									<xsl:with-param name="key">TextStyleMarina</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Marengo trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Marengo</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Tag_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundMarengo</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorMarengo</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessMarengo"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_Icon_Color-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Icon_Color</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundMarengo</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_TextStyle-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_TextStyle</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateStaticResource">
									<xsl:with-param name="key">TextStyleMarengo</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Neutral trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Neutral</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Tag_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundNeutral</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorNeutral</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessNeutral"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_Icon_Color-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Icon_Color</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundNeutral</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_TextStyle-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_TextStyle</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateStaticResource">
									<xsl:with-param name="key">TextStyleNeutral</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Orange trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Orange</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Tag_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundOrange</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorOrange</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessOrange"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_Icon_Color-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Icon_Color</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundOrange</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_TextStyle-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_TextStyle</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateStaticResource">
									<xsl:with-param name="key">TextStyleOrange</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Purple trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Purple</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Tag_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundPurple</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorPurple</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessPurple"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_Icon_Color-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Icon_Color</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundPurple</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_TextStyle-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_TextStyle</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateStaticResource">
									<xsl:with-param name="key">TextStylePurple</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Red trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Red</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Tag_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundRed</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorRed</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessRed"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_Icon_Color-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Icon_Color</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundRed</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_TextStyle-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_TextStyle</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateStaticResource">
									<xsl:with-param name="key">TextStyleRed</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Violet trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Violet</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Tag_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundViolet</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorViolet</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessViolet"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_Icon_Color-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Icon_Color</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundViolet</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_TextStyle-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_TextStyle</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateStaticResource">
									<xsl:with-param name="key">TextStyleViolet</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Yellow trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Color</xsl:with-param>
					<xsl:with-param name="propertyValue">Yellow</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Tag_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Background</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundYellow</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderBrush-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderBrush</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BorderColorYellow</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_BorderThickness-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_BorderThickness</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateThickness">
									<xsl:with-param name="node" select="BorderThicknessYellow"/>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_Icon_Color-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_Icon_Color</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">IconForegroundYellow</xsl:with-param>
									<xsl:with-param name="scope" select="$id" />
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

						<!--Tag_TextStyle-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">Tag_TextStyle</xsl:with-param>
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateStaticResource">
									<xsl:with-param name="key">TextStyleYellow</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

</xsl:transform>