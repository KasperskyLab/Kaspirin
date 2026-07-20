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

	<xsl:template match="NotificationSpinners">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">NotificationSpinners</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="NotificationSpinner">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:NotificationSpinner</xsl:with-param>
			<xsl:with-param name="basedOn">NotificationSpinnerUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">
				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">

					<!--HeaderStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@HeaderStyleId"/>
						<xsl:with-param name="key">HeaderStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextAlignment-->
							<xsl:if test="@Orientation = 'VERTICAL'">
								<xsl:call-template name="generateSetterViaAttribute">
									<xsl:with-param name="propertyName">TextBlock.TextAlignment</xsl:with-param>
									<xsl:with-param name="propertyValue" select="'CENTER'"/>
								</xsl:call-template>
							</xsl:if>
						</xsl:with-param>
					</xsl:call-template>

					<!--SpinnerStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">visuals:Spinner</xsl:with-param>
						<xsl:with-param name="basedOn" select="@SpinnerStyleId"/>
						<xsl:with-param name="key">SpinnerStyle</xsl:with-param>
					</xsl:call-template>
					
					<!--TextStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextAlignment-->
							<xsl:if test="@Orientation = 'VERTICAL'">
								<xsl:call-template name="generateSetterViaAttribute">
									<xsl:with-param name="propertyName">TextBlock.TextAlignment</xsl:with-param>
									<xsl:with-param name="propertyValue" select="'CENTER'"/>
								</xsl:call-template>
							</xsl:if>
						</xsl:with-param>
					</xsl:call-template>

				</xsl:element>

				<!--NotificationSpinner_Header_Style-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">NotificationSpinner_Header_Style</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">HeaderStyle</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>
				
				<!--NotificationSpinner_Orientation-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">NotificationSpinner_Orientation</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@Orientation"/>
				</xsl:call-template>
				
				<!--NotificationSpinner_Spinner_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">NotificationSpinner_Spinner_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="SpinnerMargin" />
				</xsl:call-template>

				<!--NotificationSpinner_Spinner_Style-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">NotificationSpinner_Spinner_Style</xsl:with-param>
					<xsl:with-param name="resourceName">SpinnerStyle</xsl:with-param>
				</xsl:call-template>

				<!--NotificationSpinner_Text_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">NotificationSpinner_Text_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="TextMargin" />
				</xsl:call-template>

				<!--NotificationSpinner_Text_Style-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">NotificationSpinner_Text_Style</xsl:with-param>
					<xsl:with-param name="propertyValue">
						<xsl:call-template name="generateStaticResource">
							<xsl:with-param name="key">TextStyle</xsl:with-param>
						</xsl:call-template>
					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

</xsl:transform>