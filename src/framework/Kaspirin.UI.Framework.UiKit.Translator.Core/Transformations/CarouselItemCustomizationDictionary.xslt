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

	<xsl:template match="CarouselItems/CarouselItem">

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

					<!--BackgroundDisabled-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundDisabled</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundDisabled"/>
					</xsl:call-template>

					<!--BackgroundHover-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundHover</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundHover"/>
					</xsl:call-template>

					<!--BackgroundNormal-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundNormal</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundNormal"/>
					</xsl:call-template>

					<!--BackgroundSelected-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundSelected</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundSelected"/>
					</xsl:call-template>

					<!--BorderColorDisabled-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderColorDisabled</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorDisabled"/>
					</xsl:call-template>

					<!--BorderColorHover-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderColorHover</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorHover"/>
					</xsl:call-template>

					<!--BorderColorNormal-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderColorNormal</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorNormal"/>
					</xsl:call-template>

					<!--BorderColorSelected-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderColorSelected</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorSelected"/>
					</xsl:call-template>					
					
				</ResourceDictionary>

			</xsl:element>
		</xsl:if>

	</xsl:template>

</xsl:transform>