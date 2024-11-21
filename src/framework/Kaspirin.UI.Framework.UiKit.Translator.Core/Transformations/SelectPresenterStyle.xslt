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

	<xsl:template match="SelectPresenters">
		<xsl:variable name="shouldProcess">
			<xsl:call-template name="shouldProcessControl">
				<xsl:with-param name="control" select="name()"/>
				<xsl:with-param name="excludedControls" select="$excludedControls"/>
				<xsl:with-param name="excludedControlsDelimiter" select="$excludedControlsDelimiter"/>
			</xsl:call-template>
		</xsl:variable>

		<xsl:if test="$shouldProcess = 'true'">
			<xsl:call-template name="getRegionComment">
				<xsl:with-param name="description">SelectPresenters</xsl:with-param>
			</xsl:call-template>

			<xsl:apply-templates/>

			<xsl:call-template name="getEndRegionComment"/>
		</xsl:if>
	</xsl:template>

	<xsl:template match="SelectPresenter">
		<xsl:variable name="id" select="@Id"/>
		<xsl:call-template name="assertId">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:call-template name="getIdComment">
			<xsl:with-param name="id" select="$id"/>
		</xsl:call-template>

		<xsl:element name="Style">
			<xsl:attribute name="TargetType">{x:Type visuals:SelectPresenter}</xsl:attribute>
			<xsl:attribute name="BasedOn">{StaticResource SelectPresenterUniversal}</xsl:attribute>
			<xsl:attribute name="x:Key">
				<xsl:value-of select="$id"/>
			</xsl:attribute>

			<!--Style.Resources-->
			<xsl:element name ="Style.Resources">

				<!--TextInputStyle-->
				<xsl:call-template name="generateStyle">
					<xsl:with-param name="targetType">visuals:TextInput</xsl:with-param>
					<xsl:with-param name="key">TextInputStyle</xsl:with-param>
					<xsl:with-param name="setters">

						<!--SelectPresenter_TextInput_CaretBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">SelectPresenter_TextInput_CaretBrush</xsl:with-param>
							<xsl:with-param name="brushName">CaretBrush</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--SelectPresenter_TextInput_Placeholder_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">SelectPresenter_TextInput_Placeholder_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">PlaceholderForeground</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--SelectPresenter_TextInput_Placeholder_TextStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">SelectPresenter_TextInput_Placeholder_TextStyle</xsl:with-param>
							<xsl:with-param name="resourceName" select="@TextStyleId" />
						</xsl:call-template>

						<!--SelectPresenter_TextInput_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">SelectPresenter_TextInput_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">TextForeground</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--SelectPresenter_TextInput_TextStyle-->
						<xsl:call-template name="generateStaticResourceSetter">
							<xsl:with-param name="propertyId">SelectPresenter_TextInput_TextStyle</xsl:with-param>
							<xsl:with-param name="resourceName" select="@TextStyleId" />
						</xsl:call-template>

					</xsl:with-param>

				</xsl:call-template>

			</xsl:element>

			<!--SelectPresenter_ArrowIcon_Foreground-->
			<xsl:call-template name="generateBrushSetter">
				<xsl:with-param name="propertyId">SelectPresenter_ArrowIcon_Foreground</xsl:with-param>
				<xsl:with-param name="brushName">ArrowIconForeground</xsl:with-param>
				<xsl:with-param name="scopeName" select="$id" />
			</xsl:call-template>

			<!--SelectPresenter_ArrowIcon_Margin-->
			<xsl:call-template name="generateMarginSetter">
				<xsl:with-param name="propertyId">SelectPresenter_ArrowIcon_Margin</xsl:with-param>
				<xsl:with-param name="marginNode" select="ArrowIconMargin" />
			</xsl:call-template>

			<!--SelectPresenter_Background-->
			<xsl:call-template name="generateBrushSetter">
				<xsl:with-param name="propertyId">SelectPresenter_Background</xsl:with-param>
				<xsl:with-param name="brushName">BackgroundNormal</xsl:with-param>
				<xsl:with-param name="scopeName" select="$id" />
			</xsl:call-template>

			<!--SelectPresenter_BorderBrush-->
			<xsl:call-template name="generateBrushSetter">
				<xsl:with-param name="propertyId">SelectPresenter_BorderBrush</xsl:with-param>
				<xsl:with-param name="brushName">BorderColorNormal</xsl:with-param>
				<xsl:with-param name="scopeName" select="$id" />
			</xsl:call-template>

			<!--SelectPresenter_BorderThickness-->
			<xsl:call-template name="generateThicknessSetter">
				<xsl:with-param name="propertyId">SelectPresenter_BorderThickness</xsl:with-param>
				<xsl:with-param name="thicknessNode" select="BorderThicknessNormal" />
			</xsl:call-template>

			<!--SelectPresenter_CornerRadius-->
			<xsl:call-template name="generateCornerRadiusSetter">
				<xsl:with-param name="propertyId">SelectPresenter_CornerRadius</xsl:with-param>
			</xsl:call-template>

			<!--SelectPresenter_Height-->
			<xsl:call-template name="generateHeightSetter">
				<xsl:with-param name="propertyId">SelectPresenter_Height</xsl:with-param>
			</xsl:call-template>

			<!--SelectPresenter_ItemHeader_Foreground-->
			<xsl:call-template name="generateBrushSetter">
				<xsl:with-param name="propertyId">SelectPresenter_ItemHeader_Foreground</xsl:with-param>
				<xsl:with-param name="brushName">TextForeground</xsl:with-param>
				<xsl:with-param name="scopeName" select="$id" />
			</xsl:call-template>

			<!--SelectPresenter_ItemHeader_TextStyle-->
			<xsl:call-template name="generateStaticResourceSetter">
				<xsl:with-param name="propertyId">SelectPresenter_ItemHeader_TextStyle</xsl:with-param>
				<xsl:with-param name="resourceName" select="@TextStyleId"/>
			</xsl:call-template>

			<!--SelectPresenter_ItemIcon_Foreground-->
			<xsl:call-template name="generateBrushSetter">
				<xsl:with-param name="propertyId">SelectPresenter_ItemIcon_Foreground</xsl:with-param>
				<xsl:with-param name="brushName">IconForeground</xsl:with-param>
				<xsl:with-param name="scopeName" select="$id" />
			</xsl:call-template>

			<!--SelectPresenter_ItemIcon_Margin-->
			<xsl:call-template name="generateMarginSetter">
				<xsl:with-param name="propertyId">SelectPresenter_ItemIcon_Margin</xsl:with-param>
				<xsl:with-param name="marginNode" select="IconMargin" />
			</xsl:call-template>

			<!--SelectPresenter_Padding-->
			<xsl:call-template name="generatePaddingSetter">
				<xsl:with-param name="propertyId">SelectPresenter_Padding</xsl:with-param>
			</xsl:call-template>

			<!--SelectPresenter_Placeholder_Foreground-->
			<xsl:call-template name="generateBrushSetter">
				<xsl:with-param name="propertyId">SelectPresenter_Placeholder_Foreground</xsl:with-param>
				<xsl:with-param name="brushName">PlaceholderForeground</xsl:with-param>
				<xsl:with-param name="scopeName" select="$id" />
			</xsl:call-template>

			<!--SelectPresenter_Placeholder_TextStyle-->
			<xsl:call-template name="generateStaticResourceSetter">
				<xsl:with-param name="propertyId">SelectPresenter_Placeholder_TextStyle</xsl:with-param>
				<xsl:with-param name="resourceName" select="@TextStyleId" />
			</xsl:call-template>

			<!--SelectPresenter_TextInputStyle-->
			<xsl:call-template name="generateStaticResourceSetter">
				<xsl:with-param name="propertyId">SelectPresenter_TextInputStyle</xsl:with-param>
				<xsl:with-param name="resourceName">TextInputStyle</xsl:with-param>
			</xsl:call-template>

			<xsl:element name="Style.Triggers">

				<!--[IsActive = True]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">IsActive</xsl:with-param>
					<xsl:with-param name="propertyValue">True</xsl:with-param>
					<xsl:with-param name="setters">

						<!--SelectPresenter_ArrowIcon_Name-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">SelectPresenter_ArrowIcon_Name</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@ArrowIconNameFocus" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[IsActive = False]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">IsActive</xsl:with-param>
					<xsl:with-param name="propertyValue">False</xsl:with-param>
					<xsl:with-param name="setters">

						<!--SelectPresenter_ArrowIcon_Name-->
						<xsl:call-template name="generateUiKitSetterViaAttribute">
							<xsl:with-param name="propertyId">SelectPresenter_ArrowIcon_Name</xsl:with-param>
							<xsl:with-param name="propertyValue" select="@ArrowIconName" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State = Disabled]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.TextInputState</xsl:with-param>
					<xsl:with-param name="propertyValue">Disabled</xsl:with-param>
					<xsl:with-param name="setters">

						<!--SelectPresenter_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">SelectPresenter_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--SelectPresenter_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">SelectPresenter_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--SelectPresenter_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">SelectPresenter_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessDisabled" />
						</xsl:call-template>

						<!--SelectPresenter_ArrowIcon_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">SelectPresenter_ArrowIcon_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">ArrowIconForegroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--SelectPresenter_ItemHeader_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">SelectPresenter_ItemHeader_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">TextForegroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--SelectPresenter_ItemIcon_Foreground-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">SelectPresenter_ItemIcon_Foreground</xsl:with-param>
							<xsl:with-param name="brushName">IconForegroundDisabled</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State = Hover]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.TextInputState</xsl:with-param>
					<xsl:with-param name="propertyValue">Hover</xsl:with-param>
					<xsl:with-param name="setters">

						<!--SelectPresenter_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">SelectPresenter_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--SelectPresenter_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">SelectPresenter_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--SelectPresenter_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">SelectPresenter_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessHover" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State = Focus]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.TextInputState</xsl:with-param>
					<xsl:with-param name="propertyValue">Focus</xsl:with-param>
					<xsl:with-param name="setters">

						<!--SelectPresenter_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">SelectPresenter_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundFocus</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--SelectPresenter_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">SelectPresenter_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorFocus</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--SelectPresenter_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">SelectPresenter_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessFocus" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State = InvalidNormal]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.TextInputState</xsl:with-param>
					<xsl:with-param name="propertyValue">InvalidNormal</xsl:with-param>
					<xsl:with-param name="setters">

						<!--SelectPresenter_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">SelectPresenter_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundInvalidNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--SelectPresenter_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">SelectPresenter_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorInvalidNormal</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--SelectPresenter_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">SelectPresenter_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessInvalidNormal" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State = InvalidHover]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.TextInputState</xsl:with-param>
					<xsl:with-param name="propertyValue">InvalidHover</xsl:with-param>
					<xsl:with-param name="setters">

						<!--SelectPresenter_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">SelectPresenter_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundInvalidHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--SelectPresenter_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">SelectPresenter_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorInvalidHover</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--SelectPresenter_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">SelectPresenter_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessInvalidHover" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

				<!--[State = InvalidFocus]-->
				<xsl:call-template name="generateTrigger">
					<xsl:with-param name="propertyName">visuals:StateService.TextInputState</xsl:with-param>
					<xsl:with-param name="propertyValue">InvalidFocus</xsl:with-param>
					<xsl:with-param name="setters">

						<!--SelectPresenter_Background-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">SelectPresenter_Background</xsl:with-param>
							<xsl:with-param name="brushName">BackgroundInvalidFocus</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--SelectPresenter_BorderBrush-->
						<xsl:call-template name="generateBrushSetter">
							<xsl:with-param name="propertyId">SelectPresenter_BorderBrush</xsl:with-param>
							<xsl:with-param name="brushName">BorderColorInvalidFocus</xsl:with-param>
							<xsl:with-param name="scopeName" select="$id" />
						</xsl:call-template>

						<!--SelectPresenter_BorderThickness-->
						<xsl:call-template name="generateThicknessSetter">
							<xsl:with-param name="propertyId">SelectPresenter_BorderThickness</xsl:with-param>
							<xsl:with-param name="thicknessNode" select="BorderThicknessInvalidFocus" />
						</xsl:call-template>

					</xsl:with-param>
				</xsl:call-template>

			</xsl:element>

		</xsl:element>
	</xsl:template>

</xsl:transform>