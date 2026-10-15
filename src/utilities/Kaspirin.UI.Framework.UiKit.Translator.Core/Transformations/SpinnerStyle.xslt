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

	<xsl:template match="Spinners">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">Spinners</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="Spinner">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:element name="Style">
			<xsl:attribute name="TargetType">{x:Type visuals:Spinner}</xsl:attribute>
			<xsl:attribute name="BasedOn">{StaticResource SpinnerUniversal}</xsl:attribute>
			<xsl:attribute name="x:Key">
				<xsl:value-of select="$id"/>
			</xsl:attribute>

			<!--Spinner_Foreground-->
			<xsl:if test="Foreground">
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">Spinner_Foreground</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateResExtension">
							<xsl:with-param name="key">Foreground</xsl:with-param>
							<xsl:with-param name="scope" select="$id" />
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>
			</xsl:if>

			<!--Spinner_Height-->
			<xsl:call-template name="generateHeightSetter">
				<xsl:with-param name="propertyId">Spinner_Height</xsl:with-param>
			</xsl:call-template>
			
			<!--Spinner_Image-->
			<xsl:call-template name="generateUiKitSetterViaAttribute">
				<xsl:with-param name="propertyId">Spinner_Image</xsl:with-param>
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

			<!--Spinner_Width-->
			<xsl:call-template name="generateWidthSetter">
				<xsl:with-param name="propertyId">Spinner_Width</xsl:with-param>
			</xsl:call-template>

		</xsl:element>
	</xsl:template>

</xsl:transform>