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

<xsl:transform xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
               xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
               version="1.0">

	<xsl:output method="xml" indent="yes" encoding="UTF-8" omit-xml-declaration="yes"/>

	<xsl:param name="excludedControlsFilter"/>
	<xsl:param name="excludedControlsFilterDelimiter"/>

	<xsl:include href="Common.xslt"/>

	<xsl:template match="UiKit">
		<xsl:element name="SvgFiles">
			<xsl:apply-templates select="Controls" />
		</xsl:element>
	</xsl:template>

	<xsl:template match="Svg">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name(../../..)"/>
				<xsl:with-param name="excludedControls" select="$excludedControlsFilter"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsFilterDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">

			<xsl:variable name="id" select="../../@Id"/>
			<xsl:call-template name="assertId">
				<xsl:with-param name="id" select="$id"/>
			</xsl:call-template>

			<xsl:element name="SvgFile">
				<xsl:attribute name="Filename">
					<xsl:call-template name="getSvgFilename">
						<xsl:with-param name="id" select="$id"/>
						<xsl:with-param name="member" select="name(../.)"/>
					</xsl:call-template>
				</xsl:attribute>

				<xsl:value-of select="." disable-output-escaping="yes"/>
			</xsl:element>

		</xsl:if>
	</xsl:template>

	<!--Template for removing redundant line breaks-->
	<xsl:template match="*/text()[not(normalize-space())]"/>

</xsl:transform>