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

	<xsl:template match="HyperLinks">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">Hyperlinks</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="HyperLink">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">Hyperlink</xsl:with-param>
			<xsl:with-param name="basedOn">HyperlinkUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Hyperlink_FocusVisualStyle-->
				<xsl:call-template name="generateFocusVisualStyleSetter">
					<xsl:with-param name="propertyId">Hyperlink_FocusVisualStyle</xsl:with-param>
					<xsl:with-param name="scope" select="$id"/>
				</xsl:call-template>

				<!--Hyperlink_Foreground-->
				<xsl:if test="ForegroundNormal">
					<xsl:call-template name="generateUiKitSetterViaAttribute">
						<xsl:with-param name="propertyId">Hyperlink_Foreground</xsl:with-param>
						<xsl:with-param name="propertyValue">
							<xsl:call-template name="generateResExtension">
								<xsl:with-param name="key">ForegroundNormal</xsl:with-param>
								<xsl:with-param name="scope" select="$id" />
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>
				</xsl:if>

				<!--Hyperlink_TextDecoration-->
				<xsl:if test="@TextDecorationNormal">
					<xsl:call-template name="generateTextDecorationsSetter">
						<xsl:with-param name="value" select = "@TextDecorationNormal"/>
						<xsl:with-param name="propertyId">Hyperlink_TextDecoration</xsl:with-param>
						<xsl:with-param name="key">TextDecorationsNormal</xsl:with-param>
						<xsl:with-param name="scope" select = "$id"/>
						<xsl:with-param name="ignoreNoneValue" select="false"/>
					</xsl:call-template>
				</xsl:if>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Disabled trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Hyperlink_Foreground-->
						<xsl:if test="ForegroundDisabled">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">Hyperlink_Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundDisabled</xsl:with-param>
										<xsl:with-param name="scope" select="$id" />
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:if>

						<!--Hyperlink_TextDecoration-->
						<xsl:if test="@TextDecorationDisabled">
							<xsl:call-template name="generateTextDecorationsSetter">
								<xsl:with-param name="value" select = "@TextDecorationDisabled"/>
								<xsl:with-param name="propertyId">Hyperlink_TextDecoration</xsl:with-param>
								<xsl:with-param name="key">TextDecorationsDisabled</xsl:with-param>
								<xsl:with-param name="scope" select = "$id"/>
								<xsl:with-param name="ignoreNoneValue" select="false"/>
							</xsl:call-template>
						</xsl:if>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Hover trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Hover</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Hyperlink_Foreground-->
						<xsl:if test="ForegroundHover">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">Hyperlink_Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundHover</xsl:with-param>
										<xsl:with-param name="scope" select="$id" />
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:if>

						<!--Hyperlink_TextDecoration-->
						<xsl:if test="@TextDecorationHover">
							<xsl:call-template name="generateTextDecorationsSetter">
								<xsl:with-param name="value" select = "@TextDecorationHover"/>
								<xsl:with-param name="propertyId">Hyperlink_TextDecoration</xsl:with-param>
								<xsl:with-param name="key">TextDecorationsHover</xsl:with-param>
								<xsl:with-param name="scope" select = "$id"/>
								<xsl:with-param name="ignoreNoneValue" select="false"/>
							</xsl:call-template>
						</xsl:if>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Pressed trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
					<xsl:with-param name="setters">

						<!--Hyperlink_Foreground-->
						<xsl:if test="ForegroundPressed">
							<xsl:call-template name="generateUiKitSetterViaAttribute">
								<xsl:with-param name="propertyId">Hyperlink_Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ForegroundPressed</xsl:with-param>
										<xsl:with-param name="scope" select="$id" />
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:if>

						<!--Hyperlink_TextDecoration-->
						<xsl:if test="@TextDecorationPressed">
							<xsl:call-template name="generateTextDecorationsSetter">
								<xsl:with-param name="value" select = "@TextDecorationPressed"/>
								<xsl:with-param name="propertyId">Hyperlink_TextDecoration</xsl:with-param>
								<xsl:with-param name="key">TextDecorationsPressed</xsl:with-param>
								<xsl:with-param name="scope" select = "$id"/>
								<xsl:with-param name="ignoreNoneValue" select="false"/>
							</xsl:call-template>
						</xsl:if>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

</xsl:transform>