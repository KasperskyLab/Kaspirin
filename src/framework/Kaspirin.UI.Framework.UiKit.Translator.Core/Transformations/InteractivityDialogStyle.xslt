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

	<xsl:template match="InteractivityDialogs">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">InteractivityDialogs</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="InteractivityDialog">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:InteractivityDialog</xsl:with-param>
			<xsl:with-param name="basedOn">InteractivityDialogUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">

					<!--ButtonStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">visuals:IconButton</xsl:with-param>
						<xsl:with-param name="basedOn" select="@ButtonStyleId"/>
						<xsl:with-param name="key">ButtonStyle</xsl:with-param>
						<xsl:with-param name="setters">

							<!--IconForeground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">IconForeground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">ButtonForeground</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>

						</xsl:with-param>

					</xsl:call-template>

					<!--FooterStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">visuals:InteractivityDialogFooter</xsl:with-param>
						<xsl:with-param name="basedOn">InteractivityDialogFooterUniversal</xsl:with-param>
						<xsl:with-param name="key">FooterStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--InteractivityDialogFooter_ButtonMargin-->
							<xsl:call-template name="generateMarginSetter">
								<xsl:with-param name="propertyId">InteractivityDialogFooter_ButtonMargin</xsl:with-param>
								<xsl:with-param name="marginNode" select="FooterButtonMargin" />
							</xsl:call-template>
							<!--InteractivityDialogFooter_PrimaryButtonStyle-->
							<xsl:call-template name="generateStaticResourceSetter">
								<xsl:with-param name="propertyId">InteractivityDialogFooter_PrimaryButtonStyle</xsl:with-param>
								<xsl:with-param name="resourceName" select="@FooterPrimaryButtonStyleId" />
							</xsl:call-template>
							<!--InteractivityDialogFooter_ReferenceButtonStyle-->
							<xsl:call-template name="generateStaticResourceSetter">
								<xsl:with-param name="propertyId">InteractivityDialogFooter_ReferenceButtonStyle</xsl:with-param>
								<xsl:with-param name="resourceName" select="@FooterReferenceButtonStyleId" />
							</xsl:call-template>
							<!--InteractivityDialogFooter_SecondaryButtonStyle-->
							<xsl:call-template name="generateStaticResourceSetter">
								<xsl:with-param name="propertyId">InteractivityDialogFooter_SecondaryButtonStyle</xsl:with-param>
								<xsl:with-param name="resourceName" select="@FooterSecondaryButtonStyleId" />
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--HeaderStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@HeaderStyleId"/>
						<xsl:with-param name="key">HeaderStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">HeaderForeground</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--SubHeaderStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@SubHeaderStyleId"/>
						<xsl:with-param name="key">SubHeaderStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">SubHeaderForeground</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateSetterViaAttribute">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="propertyValue">
									<xsl:call-template name="generateResExtension">
										<xsl:with-param name="key">TextForeground</xsl:with-param>
										<xsl:with-param name="scope" select="$id"/>
									</xsl:call-template>
								</xsl:with-param>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

				</xsl:element>

				<!--InteractivityDialog_Background-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_Background</xsl:with-param>
					<xsl:with-param name="brushName">Background</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--InteractivityDialog_BorderBrush-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_BorderBrush</xsl:with-param>
					<xsl:with-param name="brushName">BorderColor</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--InteractivityDialog_BorderThickness-->
				<xsl:call-template name="generateThicknessSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_BorderThickness</xsl:with-param>
				</xsl:call-template>

				<!--InteractivityDialog_Button_CloseIcon-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId" >InteractivityDialog_Button_CloseIcon</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@ButtonCloseIconName" />
				</xsl:call-template>

				<!--InteractivityDialog_Button_HelpIcon-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId" >InteractivityDialog_Button_HelpIcon</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@ButtonHelpIconName" />
				</xsl:call-template>

				<!--InteractivityDialog_Button_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_Button_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="ButtonMargin" />
				</xsl:call-template>

				<!--InteractivityDialog_Button_Style-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_Button_Style</xsl:with-param>
					<xsl:with-param name="resourceName">ButtonStyle</xsl:with-param>
				</xsl:call-template>

				<!--InteractivityDialog_ButtonsContainer_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_ButtonsContainer_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="ButtonContainerMargin" />
				</xsl:call-template>

				<!--InteractivityDialog_ContentContainer_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_ContentContainer_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="ContentContainerMargin" />
				</xsl:call-template>

				<!--InteractivityDialog_CornerRadius-->
				<xsl:call-template name="generateCornerRadiusSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_CornerRadius</xsl:with-param>
				</xsl:call-template>

				<!--InteractivityDialog_DescriptionStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_DescriptionStyle</xsl:with-param>
					<xsl:with-param name="resourceName">TextStyle</xsl:with-param>
				</xsl:call-template>

				<!--InteractivityDialog_Description_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_Description_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="TextMargin" />
				</xsl:call-template>

				<!--InteractivityDialog_FooterContainer_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_FooterContainer_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="FooterContainerMargin" />
				</xsl:call-template>

				<!--InteractivityDialog_FooterStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_FooterStyle</xsl:with-param>
					<xsl:with-param name="resourceName">FooterStyle</xsl:with-param>
				</xsl:call-template>

				<!--InteractivityDialog_HeaderContainer_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_HeaderContainer_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="HeaderContainerMargin" />
				</xsl:call-template>
				
				<!--InteractivityDialog_Header_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_Header_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="HeaderMargin" />
				</xsl:call-template>

				<!--InteractivityDialog_HeaderStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_HeaderStyle</xsl:with-param>
					<xsl:with-param name="resourceName">HeaderStyle</xsl:with-param>
				</xsl:call-template>

				<!--InteractivityDialog_Icon_Height-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId" >InteractivityDialog_Icon_Height</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@IconHeight" />
				</xsl:call-template>

				<!--InteractivityDialog_Icon_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_Icon_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="IconMargin" />
				</xsl:call-template>

				<!--InteractivityDialog_Icon_Width-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId" >InteractivityDialog_Icon_Width</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@IconWidth" />
				</xsl:call-template>

				<!--InteractivityDialog_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_Margin</xsl:with-param>
				</xsl:call-template>

				<!--InteractivityDialog_InteractivityOverlayStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_InteractivityOverlayStyle</xsl:with-param>
					<xsl:with-param name="resourceName" select="@OverlayStyleId" />
				</xsl:call-template>

				<!--InteractivityDialog_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_Padding</xsl:with-param>
				</xsl:call-template>

				<!--InteractivityDialog_Spinner_Style-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_Spinner_Style</xsl:with-param>
					<xsl:with-param name="resourceName" select="@SpinnerStyleId" />
				</xsl:call-template>

				<!--InteractivityDialog_Status_Background_Height-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId" >InteractivityDialog_Status_Background_Height</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@StatusBackgroundHeight" />
				</xsl:call-template>

				<!--InteractivityDialog_Status_Background_Width-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId" >InteractivityDialog_Status_Background_Width</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@StatusBackgroundWidth" />
				</xsl:call-template>

				<!--InteractivityDialog_SubHeader_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_SubHeader_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="SubHeaderMargin" />
				</xsl:call-template>

				<!--InteractivityDialog_SubHeaderStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">InteractivityDialog_SubHeaderStyle</xsl:with-param>
					<xsl:with-param name="resourceName">SubHeaderStyle</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Type = Danger trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Danger</xsl:with-param>
					<xsl:with-param name="setters">

						<!--InteractivityDialog_Status_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">InteractivityDialog_Status_Background</xsl:with-param>
							<xsl:with-param name="brushName">StatusBackgroundDanger</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--InteractivityDialog_Status_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">InteractivityDialog_Status_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">StatusForegroundDanger</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--InteractivityDialog_Status_IconName-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">InteractivityDialog_Status_IconName</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@StatusIconNameDanger" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Type = Info trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Info</xsl:with-param>
					<xsl:with-param name="setters">

						<!--InteractivityDialog_Status_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">InteractivityDialog_Status_Background</xsl:with-param>
							<xsl:with-param name="brushName">StatusBackgroundInfo</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--InteractivityDialog_Status_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">InteractivityDialog_Status_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">StatusForegroundInfo</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--InteractivityDialog_Status_IconName-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">InteractivityDialog_Status_IconName</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@StatusIconNameInfo" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Type = Loading trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Loading</xsl:with-param>
					<xsl:with-param name="setters">

						<!--InteractivityDialog_Status_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">InteractivityDialog_Status_Background</xsl:with-param>
							<xsl:with-param name="brushName">StatusBackgroundLoading</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Type = Positive trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Positive</xsl:with-param>
					<xsl:with-param name="setters">

						<!--InteractivityDialog_Status_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">InteractivityDialog_Status_Background</xsl:with-param>
							<xsl:with-param name="brushName">StatusBackgroundPositive</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--InteractivityDialog_Status_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">InteractivityDialog_Status_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">StatusForegroundPositive</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--InteractivityDialog_Status_IconName-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">InteractivityDialog_Status_IconName</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@StatusIconNamePositive" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Type = Warning trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">Type</xsl:with-param>
					<xsl:with-param name="propertyValue">Warning</xsl:with-param>
					<xsl:with-param name="setters">

						<!--InteractivityDialog_Status_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">InteractivityDialog_Status_Background</xsl:with-param>
							<xsl:with-param name="brushName">StatusBackgroundWarning</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--InteractivityDialog_Status_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">InteractivityDialog_Status_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">StatusForegroundWarning</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--InteractivityDialog_Status_IconName-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">InteractivityDialog_Status_IconName</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@StatusIconNameWarning" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[DialogSize = Standard trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">DialogSize</xsl:with-param>
					<xsl:with-param name="propertyValue">Standard</xsl:with-param>
					<xsl:with-param name="setters">

						<!--InteractivityDialog_Width-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" >InteractivityDialog_Width</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@DialogWidthStandard" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[DialogSize = Medium trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">DialogSize</xsl:with-param>
					<xsl:with-param name="propertyValue">Medium</xsl:with-param>
					<xsl:with-param name="setters">

						<!--InteractivityDialog_Width-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" >InteractivityDialog_Width</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@DialogWidthMedium" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[DialogSize = Wide trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">DialogSize</xsl:with-param>
					<xsl:with-param name="propertyValue">Wide</xsl:with-param>
					<xsl:with-param name="setters">

						<!--InteractivityDialog_Width-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId" >InteractivityDialog_Width</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@DialogWidthWide" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>
				
			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

</xsl:transform>