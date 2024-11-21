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

	<xsl:template match="SelectableTexts">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">SelectableTexts</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="SelectableText">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:SelectableText</xsl:with-param>
			<xsl:with-param name="basedOn">SelectableTextUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">
					
					<!--HyperlinkStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">Hyperlink</xsl:with-param>
						<xsl:with-param name="basedOn">HyperlinkUniversal</xsl:with-param>
						<xsl:with-param name="key">HyperlinkStyle</xsl:with-param>
						<xsl:with-param name="setters">

							<!--Hyperlink_Background-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Hyperlink_Background</xsl:with-param>
								<xsl:with-param name="brushName">BackgroundNormal</xsl:with-param>
								<xsl:with-param name="scopeName" select = "$id"/>
							</xsl:call-template>

							<!--Hyperlink_Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">Hyperlink_Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundNormal</xsl:with-param>
								<xsl:with-param name="scopeName" select = "$id"/>
							</xsl:call-template>

						</xsl:with-param>

						<xsl:with-param name="triggers">

							<!--[Hover trigger]-->
							<xsl:call-template name="generateTrigger">
								<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
								<xsl:with-param name="propertyValue">Hover</xsl:with-param>
								<xsl:with-param name="setters">

									<!--Hyperlink_Background-->
									<xsl:call-template name="generateBrushSetter">
										<xsl:with-param name="propertyId">Hyperlink_Background</xsl:with-param>
										<xsl:with-param name="brushName">BackgroundHover</xsl:with-param>
										<xsl:with-param name="scopeName" select = "$id"/>
									</xsl:call-template>

									<!--Hyperlink_Foreground-->
									<xsl:call-template name="generateBrushSetter">
										<xsl:with-param name="propertyId">Hyperlink_Foreground</xsl:with-param>
										<xsl:with-param name="brushName">ForegroundHover</xsl:with-param>
										<xsl:with-param name="scopeName" select = "$id"/>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

							<!--[Pressed trigger]-->
							<xsl:call-template name="generateTrigger">
								<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
								<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
								<xsl:with-param name="setters">

									<!--Hyperlink_Background-->
									<xsl:call-template name="generateBrushSetter">
										<xsl:with-param name="propertyId">Hyperlink_Background</xsl:with-param>
										<xsl:with-param name="brushName">BackgroundPressed</xsl:with-param>
										<xsl:with-param name="scopeName" select = "$id"/>
									</xsl:call-template>

									<!--Hyperlink_Foreground-->
									<xsl:call-template name="generateBrushSetter">
										<xsl:with-param name="propertyId">Hyperlink_Foreground</xsl:with-param>
										<xsl:with-param name="brushName">ForegroundPressed</xsl:with-param>
										<xsl:with-param name="scopeName" select = "$id"/>
									</xsl:call-template>

								</xsl:with-param>
							</xsl:call-template>

						</xsl:with-param>
					</xsl:call-template>
				
				</xsl:element>

				<!--SelectableText_FontStyle-->
				<xsl:call-template name="generateFontStyleSetter">
					<xsl:with-param name="propertyId">SelectableText_FontStyle</xsl:with-param>
					<xsl:with-param name="resourceName" select="@TextStyleId" />
				</xsl:call-template>

				<!--SelectableText_HyperlinkStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">SelectableText_HyperlinkStyle</xsl:with-param>
					<xsl:with-param name="resourceName">HyperlinkStyle</xsl:with-param>
				</xsl:call-template>

				<!--SelectableText_HyperlinkBackgroundPressed-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">SelectableText_HyperlinkBackgroundPressed</xsl:with-param>
					<xsl:with-param name="brushName">BackgroundPressed</xsl:with-param>
					<xsl:with-param name="scopeName" select = "$id"/>
				</xsl:call-template>

				<!--SelectableText_PopupStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">SelectableText_PopupStyle</xsl:with-param>
					<xsl:with-param name="resourceName" select="@PopupStyleId" />
				</xsl:call-template>

			</xsl:with-param>

		</xsl:call-template>
	</xsl:template>

</xsl:transform>