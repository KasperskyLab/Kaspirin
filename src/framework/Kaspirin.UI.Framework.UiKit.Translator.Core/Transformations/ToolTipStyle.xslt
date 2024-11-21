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

	<xsl:template match="ToolTips">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">ToolTips</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="ToolTip">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">ToolTip</xsl:with-param>
			<xsl:with-param name="basedOn">ToolTipUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">
					<!--TextStyle-->
					<xsl:if test="@TextStyleId or Foreground">
						<xsl:call-template name="generateStyle">
							<xsl:with-param name="targetType">TextBlock</xsl:with-param>
							<xsl:with-param name="basedOn" select="@TextStyleId"/>
							<xsl:with-param name="key">TextStyle</xsl:with-param>
							<xsl:with-param name="setters">
								<!--TextElement.Foreground-->
								<xsl:if test="Foreground">
									<xsl:call-template name="generateSetterViaAttribute">
										<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
										<xsl:with-param name="propertyValue">
											<xsl:call-template name="generateResExtension">
												<xsl:with-param name="key">Foreground</xsl:with-param>
												<xsl:with-param name="scope" select="$id"/>
											</xsl:call-template>
										</xsl:with-param>
									</xsl:call-template>
								</xsl:if>
							</xsl:with-param>
						</xsl:call-template>
					</xsl:if>
				</xsl:element>

				<!--ToolTip_Background-->
				<xsl:if test="Background">
					<xsl:call-template name="generateUiKitSetterViaAttribute">
						<xsl:with-param name="propertyId">ToolTip_Background</xsl:with-param>
						<xsl:with-param name="propertyValue">
							<xsl:call-template name="generateResExtension">
								<xsl:with-param name="key">Background</xsl:with-param>
								<xsl:with-param name="scope" select="$id" />
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>
				</xsl:if>

				<!--ToolTip_BorderBrush-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">ToolTip_BorderBrush</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateResExtension">
							<xsl:with-param name="key">BorderColor</xsl:with-param>
							<xsl:with-param name="scope" select="$id" />
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--ToolTip_BorderThickness-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">ToolTip_BorderThickness</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateThickness">
							<xsl:with-param name="node" select="BorderThickness" />
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

				<!--ToolTip_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId">ToolTip_CornerRadius</xsl:with-param>
				</xsl:call-template>

				<!--ToolTip_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">ToolTip_Padding</xsl:with-param>
				</xsl:call-template>

				<!--ToolTip_Shadow-->
				<xsl:call-template name="generateShadowSetter">
					<xsl:with-param name="propertyId">ToolTip_Shadow</xsl:with-param>
					<xsl:with-param name="scope" select="$id"/>
				</xsl:call-template>

				<!--ToolTip_TextStyle-->
				<xsl:if test="@TextStyleId or Foreground">
					<xsl:call-template name="generateUiKitSetterViaAttribute">
						<xsl:with-param name="propertyId">ToolTip_TextStyle</xsl:with-param>
						<xsl:with-param name="propertyValue">
							<xsl:call-template name="generateStaticResource">
								<xsl:with-param name="key">TextStyle</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>
				</xsl:if>

			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

</xsl:transform>