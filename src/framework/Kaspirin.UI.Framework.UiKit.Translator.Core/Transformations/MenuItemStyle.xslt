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

	<xsl:template match="MenuItems">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">MenuItems</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="MenuItem">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="generateStyle">
			<xsl:with-param name="targetType">visuals:MenuItemBase</xsl:with-param>
			<xsl:with-param name="basedOn">MenuItemUniversal</xsl:with-param>
			<xsl:with-param name="key" select="$id"/>
			<xsl:with-param name="setters">

				<!--Style.Resources-->
				<xsl:element name ="Style.Resources">

					<!--DescriptionStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@DescriptionStyleId"/>
						<xsl:with-param name="key">DescriptionStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">DescriptionForegroundNormal</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--DescriptionStyleDisabled-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@DescriptionStyleId"/>
						<xsl:with-param name="key">DescriptionStyleDisabled</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">DescriptionForegroundDisabled</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--DescriptionStyleHover-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@DescriptionStyleId"/>
						<xsl:with-param name="key">DescriptionStyleHover</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">DescriptionForegroundHover</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--DescriptionStylePressed-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@DescriptionStyleId"/>
						<xsl:with-param name="key">DescriptionStylePressed</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">DescriptionForegroundPressed</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--GroupHeaderStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@GroupHeaderStyleId"/>
						<xsl:with-param name="key">GroupHeaderStyle</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">GroupHeaderForeground</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--PopupDecoratorStyle-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">visuals:ContextMenuPopupDecorator</xsl:with-param>
						<xsl:with-param name="basedOn">PopupDecoratorUniversal</xsl:with-param>
						<xsl:with-param name="key">PopupDecoratorStyle</xsl:with-param>
						<xsl:with-param name="setters">

							<!--PopupDecorator_Background-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">PopupDecorator_Background</xsl:with-param>
								<xsl:with-param name="brushName">PopupDecoratorBackground</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>

							<!--PopupDecorator_BorderBrush-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyId">PopupDecorator_BorderBrush</xsl:with-param>
								<xsl:with-param name="brushName">PopupDecoratorBorderColor</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id" />
							</xsl:call-template>

							<!--PopupDecorator_BorderThickness-->
							<xsl:call-template name="generateThicknessSetter">
								<xsl:with-param name="propertyId">PopupDecorator_BorderThickness</xsl:with-param>
								<xsl:with-param name="thicknessNode" select="PopupDecoratorBorderThickness" />
							</xsl:call-template>

							<!--PopupDecorator_CornerRadius-->
							<xsl:call-template name="generateCornerRadiusSetter">
								<xsl:with-param name="propertyId">PopupDecorator_CornerRadius</xsl:with-param>
								<xsl:with-param name="cornerRadiusNode" select="PopupDecoratorCornerRadius" />
							</xsl:call-template>

							<!--PopupDecorator_Padding-->
							<xsl:call-template name="generatePaddingSetter">
								<xsl:with-param name="propertyId">PopupDecorator_Padding</xsl:with-param>
								<xsl:with-param name="paddingNode" select="PopupDecoratorPadding" />
							</xsl:call-template>

							<!--PopupDecorator_Shadow-->
							<xsl:call-template name="generateShadowSetter">
								<xsl:with-param name="propertyId">PopupDecorator_Shadow</xsl:with-param>
								<xsl:with-param name="shadowNode" select="PopupDecoratorShadow" />
								<xsl:with-param name="scope" select="$id"/>
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
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundNormal</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleDisabled-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleDisabled</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundDisabled</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStyleHover-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStyleHover</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundHover</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

					<!--TextStylePressed-->
					<xsl:call-template name="generateStyle">
						<xsl:with-param name="targetType">TextBlock</xsl:with-param>
						<xsl:with-param name="basedOn" select="@TextStyleId"/>
						<xsl:with-param name="key">TextStylePressed</xsl:with-param>
						<xsl:with-param name="setters">
							<!--TextElement.Foreground-->
							<xsl:call-template name="generateBrushSetter">
								<xsl:with-param name="propertyName">TextElement.Foreground</xsl:with-param>
								<xsl:with-param name="brushName">ForegroundPressed</xsl:with-param>
								<xsl:with-param name="scopeName" select="$id"/>
							</xsl:call-template>
						</xsl:with-param>
					</xsl:call-template>

				</xsl:element>

				<!--MenuItem_Background-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">MenuItem_Background</xsl:with-param>
					<xsl:with-param name="brushName">BackgroundNormal</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--MenuItem_Badge_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">MenuItem_Badge_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="BadgeMargin" />
				</xsl:call-template>

				<!--MenuItem_CheckBoxMarkIcon_Foreground-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">MenuItem_CheckBoxMarkIcon_Foreground</xsl:with-param>
					<xsl:with-param name="brushName">CheckBoxMarkIconForegroundNormal</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--MenuItem_CheckBoxMarkIcon_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">MenuItem_CheckBoxMarkIcon_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="CheckBoxMarkIconMargin" />
				</xsl:call-template>

				<!--MenuItem_CheckBoxMarkIcon_Name-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">MenuItem_CheckBoxMarkIcon_Name</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@CheckBoxMarkIconName" />
				</xsl:call-template>

				<!--MenuItem_Description_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">MenuItem_Description_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="DescriptionMargin" />
				</xsl:call-template>

				<!--MenuItem_DescriptionStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">MenuItem_DescriptionStyle</xsl:with-param>
					<xsl:with-param name="resourceName">DescriptionStyle</xsl:with-param>
				</xsl:call-template>

				<!--MenuItem_DescriptionStyleDisabled-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">MenuItem_DescriptionStyleDisabled</xsl:with-param>
					<xsl:with-param name="resourceName">DescriptionStyleDisabled</xsl:with-param>
				</xsl:call-template>

				<!--MenuItem_DescriptionStyleHover-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">MenuItem_DescriptionStyleHover</xsl:with-param>
					<xsl:with-param name="resourceName">DescriptionStyleHover</xsl:with-param>
				</xsl:call-template>

				<!--MenuItem_DescriptionStylePressed-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">MenuItem_DescriptionStylePressed</xsl:with-param>
					<xsl:with-param name="resourceName">DescriptionStylePressed</xsl:with-param>
				</xsl:call-template>

				<!--MenuItem_GroupHeader_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">MenuItem_GroupHeader_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="GroupHeaderMargin" />
				</xsl:call-template>

				<!--MenuItem_GroupHeaderStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">MenuItem_GroupHeaderStyle</xsl:with-param>
					<xsl:with-param name="resourceName">GroupHeaderStyle</xsl:with-param>
				</xsl:call-template>

				<!--MenuItem_Icon_Foreground-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">MenuItem_Icon_Foreground</xsl:with-param>
					<xsl:with-param name="brushName">IconForegroundNormal</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--MenuItem_Icon_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">MenuItem_Icon_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="IconMargin" />
				</xsl:call-template>

				<!--MenuItem_MinHeight-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">MenuItem_MinHeight</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@MinHeight" />
				</xsl:call-template>

				<!--MenuItem_Padding-->
				<xsl:call-template name="generatePaddingSetter">
					<xsl:with-param name="propertyId">MenuItem_Padding</xsl:with-param>
				</xsl:call-template>

				<!--MenuItem_PopupDecoratorStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">MenuItem_PopupDecoratorStyle</xsl:with-param>
					<xsl:with-param name="resourceName">PopupDecoratorStyle</xsl:with-param>
				</xsl:call-template>

				<!--MenuItem_SubmenuIcon_Name-->
				<xsl:call-template name="generateUiKitSetterViaAttribute">
					<xsl:with-param name="propertyId">MenuItem_SubmenuIcon_Name</xsl:with-param>
					<xsl:with-param name="propertyValue" select="@SubmenuIconName" />
				</xsl:call-template>

				<!--MenuItem_SubmenuIcon_Foreground-->
				<xsl:call-template name="generateBrushSetter">
					<xsl:with-param name="propertyId">MenuItem_SubmenuIcon_Foreground</xsl:with-param>
					<xsl:with-param name="brushName">SubmenuIconForegroundNormal</xsl:with-param>
					<xsl:with-param name="scopeName" select="$id" />
				</xsl:call-template>

				<!--MenuItem_SubmenuIcon_Margin-->
				<xsl:call-template name="generateMarginSetter">
					<xsl:with-param name="propertyId">MenuItem_SubmenuIcon_Margin</xsl:with-param>
					<xsl:with-param name="marginNode" select="SubmenuIconMargin" />
				</xsl:call-template>

				<!--MenuItem_TextStyle-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">MenuItem_TextStyle</xsl:with-param>
					<xsl:with-param name="resourceName">TextStyle</xsl:with-param>
				</xsl:call-template>

				<!--MenuItem_TextStyleDisabled-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">MenuItem_TextStyleDisabled</xsl:with-param>
					<xsl:with-param name="resourceName">TextStyleDisabled</xsl:with-param>
				</xsl:call-template>

				<!--MenuItem_TextStyleHover-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">MenuItem_TextStyleHover</xsl:with-param>
					<xsl:with-param name="resourceName">TextStyleHover</xsl:with-param>
				</xsl:call-template>

				<!--MenuItem_TextStylePressed-->
				<xsl:call-template name="generateStaticResourceSetter">
					<xsl:with-param name="propertyId">MenuItem_TextStylePressed</xsl:with-param>
					<xsl:with-param name="resourceName">TextStylePressed</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>

			<xsl:with-param name="triggers">

				<!--[Disabled trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
					<xsl:with-param name="setters">

						<!--MenuItem_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">MenuItem_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--MenuItem_CheckBoxMarkIcon_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">MenuItem_CheckBoxMarkIcon_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">CheckBoxMarkIconForegroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--MenuItem_Icon_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">MenuItem_Icon_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">IconForegroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--MenuItem_SubmenuIcon_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">MenuItem_SubmenuIcon_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">SubmenuIconForegroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Hover trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Hover</xsl:with-param>
					<xsl:with-param name="setters">

						<!--MenuItem_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">MenuItem_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--MenuItem_CheckBoxMarkIcon_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">MenuItem_CheckBoxMarkIcon_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">CheckBoxMarkIconForegroundHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--MenuItem_Icon_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">MenuItem_Icon_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">IconForegroundHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--MenuItem_SubmenuIcon_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">MenuItem_SubmenuIcon_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">SubmenuIconForegroundHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[Pressed trigger]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.State</xsl:with-param>
					<xsl:with-param name="propertyValue">Pressed</xsl:with-param>
					<xsl:with-param name="setters">

						<!--MenuItem_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">MenuItem_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--MenuItem_CheckBoxMarkIcon_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">MenuItem_CheckBoxMarkIcon_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">CheckBoxMarkIconForegroundPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--MenuItem_Icon_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">MenuItem_Icon_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">IconForegroundPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--MenuItem_SubmenuIcon_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">MenuItem_SubmenuIcon_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">SubmenuIconForegroundPressed</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>

</xsl:transform>