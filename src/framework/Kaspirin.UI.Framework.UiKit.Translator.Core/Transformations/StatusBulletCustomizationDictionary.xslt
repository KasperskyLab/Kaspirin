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

	<xsl:template match="StatusBullets/StatusBullet">

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

					<!--BulletColorDanger-->
					<xsl:if test="BulletColorDanger">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">BulletColorDanger</xsl:with-param>
							<xsl:with-param name="brushNode" select="BulletColorDanger"/>
						</xsl:call-template>
					</xsl:if>

					<!--BulletColorInfo-->
					<xsl:if test="BulletColorInfo">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">BulletColorInfo</xsl:with-param>
							<xsl:with-param name="brushNode" select="BulletColorInfo"/>
						</xsl:call-template>
					</xsl:if>

					<!--BulletColorNeutral-->
					<xsl:if test="BulletColorNeutral">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">BulletColorNeutral</xsl:with-param>
							<xsl:with-param name="brushNode" select="BulletColorNeutral"/>
						</xsl:call-template>
					</xsl:if>

					<!--BulletColorPositive-->
					<xsl:if test="BulletColorPositive">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">BulletColorPositive</xsl:with-param>
							<xsl:with-param name="brushNode" select="BulletColorPositive"/>
						</xsl:call-template>
					</xsl:if>

					<!--BulletColorWarning-->
					<xsl:if test="BulletColorWarning">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">BulletColorWarning</xsl:with-param>
							<xsl:with-param name="brushNode" select="BulletColorWarning"/>
						</xsl:call-template>
					</xsl:if>

					<!--Foreground-->
					<xsl:if test="Foreground">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">Foreground</xsl:with-param>
							<xsl:with-param name="brushNode" select="Foreground"/>
						</xsl:call-template>
					</xsl:if>

				</ResourceDictionary>

			</xsl:element>
		</xsl:if>

	</xsl:template>

</xsl:transform>