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

	<xsl:template match="TextViewers">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">TextViewers</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="TextViewer">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:TextViewer</xsl:with-param>
			<xsl:with-param name="basedOn">TextViewerUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">

					<!--TextStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBox</xsl:with-param>
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

				</xsl:element>

				<!--TextViewer_IslandStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">TextViewer_IslandStyle</xsl:with-param>
					<xsl:with-param name="resourceName" select="@IslandStyleId"></xsl:with-param>
				</xsl:call-template>

				<!--TextViewer_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">TextViewer_Padding</xsl:with-param>
				</xsl:call-template>

				<!--TextViewer_TextStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">TextViewer_TextStyle</xsl:with-param>
					<xsl:with-param name="resourceName">TextStyle</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

</xsl:transform>