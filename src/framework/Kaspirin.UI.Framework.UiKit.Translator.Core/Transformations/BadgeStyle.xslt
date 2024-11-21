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

	<xsl:template match="Badges">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">Badges</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="Badge">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>
		
		<xsl:variable name="prefix" select="concat(@Type, '_')" />

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType" select="concat('visuals:', @Type)" />
			<xsl:with-param name="basedOn" select="concat(@Type, 'Universal')" />
			<xsl:with-param name="key" select="$id" />
			<xsl:with-param name="setters">

				<!--[BadgePrefix]_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'CornerRadius')" />
				</xsl:call-template>

				<!--[BadgePrefix]_Foreground-->
				<xsl:if test="Foreground">
					<xsl:call-template name="generateUiKitSetterViaAttribute">
						<xsl:with-param name="propertyId" select="concat($prefix, 'Foreground')" />
						<xsl:with-param name="propertyValue">
							<xsl:call-template name="generateResExtension">
								<xsl:with-param name="key">Foreground</xsl:with-param>
								<xsl:with-param name="scope" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>
				</xsl:if>

				<!--[BadgePrefix]_Height-->
				<xsl:call-template name="generateHeightSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'Height')" />
				</xsl:call-template>

				<!--[BadgePrefix]_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId" select="concat($prefix, 'Padding')" />
				</xsl:call-template>

				<!--[BadgePrefix]_TextStyle-->
				<xsl:if test="@TextStyleId">
					<xsl:call-template name="generateUiKitSetterViaAttribute">
						<xsl:with-param name="propertyId" select="concat($prefix, 'TextStyle')" />
						<xsl:with-param name="propertyValue">
							<xsl:call-template name="generateStaticResource">
								<xsl:with-param name="key" select="@TextStyleId"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>
				</xsl:if>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Danger trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Danger</xsl:with-param>
					<xsl:with-param name="setters">

						<!--[BadgePrefix]_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Background')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundDanger</xsl:with-param>
									<xsl:with-param name="scope">
										<xsl:value-of select="@Id"/>
									</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Info trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Info</xsl:with-param>
					<xsl:with-param name="setters">

						<!--[BadgePrefix]_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Background')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundInfo</xsl:with-param>
									<xsl:with-param name="scope">
										<xsl:value-of select="@Id"/>
									</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Neutral trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Neutral</xsl:with-param>
					<xsl:with-param name="setters">

						<!--[BadgePrefix]_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Background')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundNeutral</xsl:with-param>
									<xsl:with-param name="scope">
										<xsl:value-of select="@Id"/>
									</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Positive trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Positive</xsl:with-param>
					<xsl:with-param name="setters">

						<!--[BadgePrefix]_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Background')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundPositive</xsl:with-param>
									<xsl:with-param name="scope">
										<xsl:value-of select="@Id"/>
									</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Warning trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Warning</xsl:with-param>
					<xsl:with-param name="setters">

						<!--[BadgePrefix]_Background-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" select="concat($prefix, 'Background')" />
							<xsl:with-param name="propertyValue">
								<xsl:call-template name="generateResExtension">
									<xsl:with-param name="key">BackgroundWarning</xsl:with-param>
									<xsl:with-param name="scope">
										<xsl:value-of select="@Id"/>
									</xsl:with-param>
								</xsl:call-template>
							</xsl:with-param>
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>
</xsl:transform>