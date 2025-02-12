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

	<xsl:import href="Common.xslt"/>

	<xsl:template match="RoundProgresses/RoundProgress">

		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name(..)"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:element name="CustomizationDictionary">

				<xsl:variable name="id" select="@Id"/>
				<xsl:call-template name="assertId">
					<xsl:with-param name="id" select="$id"/>
				</xsl:call-template>

				<!--Id (used as filename when dictionaries will be splitted into separate files)-->
				<xsl:attribute name="Id">
					<xsl:value-of select="$id"/>
				</xsl:attribute>

				<xsl:call-template name="getAutogeneratedComment">
					<xsl:with-param name="source" select="$source"/>
					<xsl:with-param name="comment" select="$comment"/>
				</xsl:call-template>

				<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
									xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

					<!--Indicator-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">Indicator</xsl:with-param>
						<xsl:with-param name="brushNode" select="Indicator"/>
					</xsl:call-template>

					<!--IndicatorDisabled-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">IndicatorDisabled</xsl:with-param>
						<xsl:with-param name="brushNode" select="IndicatorDisabled"/>
					</xsl:call-template>

					<!--Track-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">Track</xsl:with-param>
						<xsl:with-param name="brushNode" select="Track"/>
					</xsl:call-template>

					<!--TrackDisabled-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">TrackDisabled</xsl:with-param>
						<xsl:with-param name="brushNode" select="TrackDisabled"/>
					</xsl:call-template>

					<!--ValueForeground-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">ValueForeground</xsl:with-param>
						<xsl:with-param name="brushNode" select="ValueForeground"/>
					</xsl:call-template>

					<!--ValueForegroundDisabled-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">ValueForegroundDisabled</xsl:with-param>
						<xsl:with-param name="brushNode" select="ValueForegroundDisabled"/>
					</xsl:call-template>

				</ResourceDictionary>

			</xsl:element>
		</xsl:if>

	</xsl:template>

</xsl:transform>