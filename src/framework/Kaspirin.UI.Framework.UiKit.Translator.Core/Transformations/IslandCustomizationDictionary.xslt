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

	<xsl:template match="Islands/Island">

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

					<!--BackgroundElevationLevel1-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundElevationLevel1</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundElevationLevel1"/>
					</xsl:call-template>

					<!--BackgroundElevationLevel2-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundElevationLevel2</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundElevationLevel2"/>
					</xsl:call-template>

					<!--BackgroundPrimaryLevel1-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundPrimaryLevel1</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundPrimaryLevel1"/>
					</xsl:call-template>

					<!--BackgroundPrimaryLevel2-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundPrimaryLevel2</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundPrimaryLevel2"/>
					</xsl:call-template>

					<!--BackgroundSecondaryLevel1-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundSecondaryLevel1</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundSecondaryLevel1"/>
					</xsl:call-template>

					<!--BackgroundSecondaryLevel2-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundSecondaryLevel2</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundSecondaryLevel2"/>
					</xsl:call-template>

					<!--BorderColorElevationLevel1-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderColorElevationLevel1</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorElevationLevel1"/>
					</xsl:call-template>

					<!--BorderColorElevationLevel2-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderColorElevationLevel2</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorElevationLevel2"/>
					</xsl:call-template>

					<!--BorderColorPrimaryLevel1-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderColorPrimaryLevel1</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorPrimaryLevel1"/>
					</xsl:call-template>

					<!--BorderColorPrimaryLevel2-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderColorPrimaryLevel2</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorPrimaryLevel2"/>
					</xsl:call-template>

					<!--BorderColorSecondaryLevel1-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderColorSecondaryLevel1</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorSecondaryLevel1"/>
					</xsl:call-template>

					<!--BorderColorSecondaryLevel2-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderColorSecondaryLevel2</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorSecondaryLevel2"/>
					</xsl:call-template>

					<!--ShadowElevationLevel1-->
					<xsl:if test="ShadowElevationLevel1">
						<xsl:call-template name="generateShadowEffectFromShadowElement">
							<xsl:with-param name="key">ShadowElevationLevel1</xsl:with-param>
							<xsl:with-param name="shadowNode" select="ShadowElevationLevel1"/>
						</xsl:call-template>
					</xsl:if>

					<!--ShadowElevationLevel2-->
					<xsl:if test="ShadowElevationLevel2">
						<xsl:call-template name="generateShadowEffectFromShadowElement">
							<xsl:with-param name="key">ShadowElevationLevel2</xsl:with-param>
							<xsl:with-param name="shadowNode" select="ShadowElevationLevel2"/>
						</xsl:call-template>
					</xsl:if>

					<!--ShadowPrimaryLevel1-->
					<xsl:if test="ShadowPrimaryLevel1">
						<xsl:call-template name="generateShadowEffectFromShadowElement">
							<xsl:with-param name="key">ShadowPrimaryLevel1</xsl:with-param>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryLevel1"/>
						</xsl:call-template>
					</xsl:if>

					<!--ShadowPrimaryLevel2-->
					<xsl:if test="ShadowPrimaryLevel2">
						<xsl:call-template name="generateShadowEffectFromShadowElement">
							<xsl:with-param name="key">ShadowPrimaryLevel2</xsl:with-param>
							<xsl:with-param name="shadowNode" select="ShadowPrimaryLevel2"/>
						</xsl:call-template>
					</xsl:if>

					<!--ShadowSecondaryLevel1-->
					<xsl:if test="ShadowSecondaryLevel1">
						<xsl:call-template name="generateShadowEffectFromShadowElement">
							<xsl:with-param name="key">ShadowSecondaryLevel1</xsl:with-param>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryLevel1"/>
						</xsl:call-template>
					</xsl:if>

					<!--ShadowSecondaryLevel2-->
					<xsl:if test="ShadowSecondaryLevel2">
						<xsl:call-template name="generateShadowEffectFromShadowElement">
							<xsl:with-param name="key">ShadowSecondaryLevel2</xsl:with-param>
							<xsl:with-param name="shadowNode" select="ShadowSecondaryLevel2"/>
						</xsl:call-template>
					</xsl:if>

				</ResourceDictionary>

			</xsl:element>
		</xsl:if>

	</xsl:template>

</xsl:transform>