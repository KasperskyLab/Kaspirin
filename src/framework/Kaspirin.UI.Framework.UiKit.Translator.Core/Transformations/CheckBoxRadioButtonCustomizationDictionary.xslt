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

	<xsl:template match="CheckBoxes/CheckBox|RadioButtons/RadioButton">

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

					<!--BackgroundCheckedDisabled-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundCheckedDisabled</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundCheckedDisabled"/>
					</xsl:call-template>

					<!--BackgroundCheckedHover-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundCheckedHover</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundCheckedHover"/>
					</xsl:call-template>

					<!--BackgroundCheckedInvalidHover-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundCheckedInvalidHover</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundCheckedInvalidHover"/>
					</xsl:call-template>

					<!--BackgroundCheckedInvalidNormal-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundCheckedInvalidNormal</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundCheckedInvalidNormal"/>
					</xsl:call-template>

					<!--BackgroundCheckedInvalidPressed-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundCheckedInvalidPressed</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundCheckedInvalidPressed"/>
					</xsl:call-template>

					<!--BackgroundCheckedNormal-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundCheckedNormal</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundCheckedNormal"/>
					</xsl:call-template>

					<!--BackgroundCheckedPressed-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundCheckedPressed</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundCheckedPressed"/>
					</xsl:call-template>

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

					<!--BackgroundInvalidHover-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundInvalidHover</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundInvalidHover"/>
					</xsl:call-template>

					<!--BackgroundInvalidNormal-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundInvalidNormal</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundInvalidNormal"/>
					</xsl:call-template>

					<!--BackgroundInvalidPressed-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundInvalidPressed</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundInvalidPressed"/>
					</xsl:call-template>

					<!--BackgroundNormal-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundNormal</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundNormal"/>
					</xsl:call-template>

					<!--BackgroundPartialDisabled-->
					<xsl:if test="BackgroundPartialDisabled">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">BackgroundPartialDisabled</xsl:with-param>
							<xsl:with-param name="brushNode" select="BackgroundPartialDisabled"/>
						</xsl:call-template>
					</xsl:if>

					<!--BackgroundPartialHover-->
					<xsl:if test="BackgroundPartialHover">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">BackgroundPartialHover</xsl:with-param>
							<xsl:with-param name="brushNode" select="BackgroundPartialHover"/>
						</xsl:call-template>
					</xsl:if>

					<!--BackgroundPartialInvalidHover-->
					<xsl:if test="BackgroundPartialInvalidHover">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">BackgroundPartialInvalidHover</xsl:with-param>
							<xsl:with-param name="brushNode" select="BackgroundPartialInvalidHover"/>
						</xsl:call-template>
					</xsl:if>

					<!--BackgroundPartialInvalidNormal-->
					<xsl:if test="BackgroundPartialInvalidNormal">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">BackgroundPartialInvalidNormal</xsl:with-param>
							<xsl:with-param name="brushNode" select="BackgroundPartialInvalidNormal"/>
						</xsl:call-template>
					</xsl:if>

					<!--BackgroundPartialInvalidPressed-->
					<xsl:if test="BackgroundPartialInvalidPressed">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">BackgroundPartialInvalidPressed</xsl:with-param>
							<xsl:with-param name="brushNode" select="BackgroundPartialInvalidPressed"/>
						</xsl:call-template>
					</xsl:if>

					<!--BackgroundPartialNormal-->
					<xsl:if test="BackgroundPartialNormal">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">BackgroundPartialNormal</xsl:with-param>
							<xsl:with-param name="brushNode" select="BackgroundPartialNormal"/>
						</xsl:call-template>
					</xsl:if>

					<!--BackgroundPartialPressed-->
					<xsl:if test="BackgroundPartialPressed">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">BackgroundPartialPressed</xsl:with-param>
							<xsl:with-param name="brushNode" select="BackgroundPartialPressed"/>
						</xsl:call-template>
					</xsl:if>

					<!--BackgroundPressed-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BackgroundPressed</xsl:with-param>
						<xsl:with-param name="brushNode" select="BackgroundPressed"/>
					</xsl:call-template>

					<!--BorderCheckedDisabled-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderCheckedDisabled</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorCheckedDisabled"/>
					</xsl:call-template>

					<!--BorderCheckedHover-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderCheckedHover</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorCheckedHover"/>
					</xsl:call-template>

					<!--BorderCheckedInvalidHover-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderCheckedInvalidHover</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorCheckedInvalidHover"/>
					</xsl:call-template>

					<!--BorderCheckedInvalidNormal-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderCheckedInvalidNormal</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorCheckedInvalidNormal"/>
					</xsl:call-template>

					<!--BorderCheckedInvalidPressed-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderCheckedInvalidPressed</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorCheckedInvalidPressed"/>
					</xsl:call-template>

					<!--BorderCheckedNormal-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderCheckedNormal</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorCheckedNormal"/>
					</xsl:call-template>

					<!--BorderCheckedPressed-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderCheckedPressed</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorCheckedPressed"/>
					</xsl:call-template>

					<!--BorderDisabled-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderDisabled</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorDisabled"/>
					</xsl:call-template>

					<!--BorderHover-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderHover</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorHover"/>
					</xsl:call-template>

					<!--BorderInvalidHover-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderInvalidHover</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorInvalidHover"/>
					</xsl:call-template>

					<!--BorderInvalidNormal-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderInvalidNormal</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorInvalidNormal"/>
					</xsl:call-template>

					<!--BorderInvalidPressed-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderInvalidPressed</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorInvalidPressed"/>
					</xsl:call-template>

					<!--BorderNormal-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderNormal</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorNormal"/>
					</xsl:call-template>

					<!--BorderPartialDisabled-->
					<xsl:if test="BorderColorPartialDisabled">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">BorderPartialDisabled</xsl:with-param>
							<xsl:with-param name="brushNode" select="BorderColorPartialDisabled"/>
						</xsl:call-template>
					</xsl:if>

					<!--BorderPartialHover-->
					<xsl:if test="BorderColorPartialHover">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">BorderPartialHover</xsl:with-param>
							<xsl:with-param name="brushNode" select="BorderColorPartialHover"/>
						</xsl:call-template>
					</xsl:if>

					<!--BorderPartialInvalidHover-->
					<xsl:if test="BorderColorPartialInvalidHover">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">BorderPartialInvalidHover</xsl:with-param>
							<xsl:with-param name="brushNode" select="BorderColorPartialInvalidHover"/>
						</xsl:call-template>
					</xsl:if>

					<!--BorderPartialInvalidNormal-->
					<xsl:if test="BorderColorPartialInvalidNormal">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">BorderPartialInvalidNormal</xsl:with-param>
							<xsl:with-param name="brushNode" select="BorderColorPartialInvalidNormal"/>
						</xsl:call-template>
					</xsl:if>

					<!--BorderPartialInvalidPressed-->
					<xsl:if test="BorderColorPartialInvalidPressed">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">BorderPartialInvalidPressed</xsl:with-param>
							<xsl:with-param name="brushNode" select="BorderColorPartialInvalidPressed"/>
						</xsl:call-template>
					</xsl:if>

					<!--BorderPartialNormal-->
					<xsl:if test="BorderColorPartialNormal">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">BorderPartialNormal</xsl:with-param>
							<xsl:with-param name="brushNode" select="BorderColorPartialNormal"/>
						</xsl:call-template>
					</xsl:if>

					<!--BorderPartialPressed-->
					<xsl:if test="BorderColorPartialPressed">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">BorderPartialPressed</xsl:with-param>
							<xsl:with-param name="brushNode" select="BorderColorPartialPressed"/>
						</xsl:call-template>
					</xsl:if>

					<!--BorderPressed-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">BorderPressed</xsl:with-param>
						<xsl:with-param name="brushNode" select="BorderColorPressed"/>
					</xsl:call-template>

					<!--DescriptionForeground-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">DescriptionForeground</xsl:with-param>
						<xsl:with-param name="brushNode" select="DescriptionForeground"/>
					</xsl:call-template>

					<!--DescriptionForegroundDisabled-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">DescriptionForegroundDisabled</xsl:with-param>
						<xsl:with-param name="brushNode" select="DescriptionForegroundDisabled"/>
					</xsl:call-template>

					<!--FocusVisualBrush-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">FocusVisualBrush</xsl:with-param>
						<xsl:with-param name="brushNode" select="Focus/Color"/>
					</xsl:call-template>

					<!--MarkColorCheckedDisabled-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">MarkColorCheckedDisabled</xsl:with-param>
						<xsl:with-param name="brushNode" select="MarkColorCheckedDisabled"/>
					</xsl:call-template>

					<!--MarkColorCheckedHover-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">MarkColorCheckedHover</xsl:with-param>
						<xsl:with-param name="brushNode" select="MarkColorCheckedHover"/>
					</xsl:call-template>

					<!--MarkColorCheckedInvalidHover-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">MarkColorCheckedInvalidHover</xsl:with-param>
						<xsl:with-param name="brushNode" select="MarkColorCheckedInvalidHover"/>
					</xsl:call-template>

					<!--MarkColorCheckedInvalidNormal-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">MarkColorCheckedInvalidNormal</xsl:with-param>
						<xsl:with-param name="brushNode" select="MarkColorCheckedInvalidNormal"/>
					</xsl:call-template>

					<!--MarkColorCheckedInvalidPressed-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">MarkColorCheckedInvalidPressed</xsl:with-param>
						<xsl:with-param name="brushNode" select="MarkColorCheckedInvalidPressed"/>
					</xsl:call-template>

					<!--MarkColorCheckedNormal-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">MarkColorCheckedNormal</xsl:with-param>
						<xsl:with-param name="brushNode" select="MarkColorCheckedNormal"/>
					</xsl:call-template>

					<!--MarkColorCheckedPressed-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">MarkColorCheckedPressed</xsl:with-param>
						<xsl:with-param name="brushNode" select="MarkColorCheckedPressed"/>
					</xsl:call-template>

					<!--MarkColorPartialDisabled-->
					<xsl:if test="MarkColorPartialDisabled">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">MarkColorPartialDisabled</xsl:with-param>
							<xsl:with-param name="brushNode" select="MarkColorPartialDisabled"/>
						</xsl:call-template>
					</xsl:if>

					<!--MarkColorPartialHover-->
					<xsl:if test="MarkColorPartialHover">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">MarkColorPartialHover</xsl:with-param>
							<xsl:with-param name="brushNode" select="MarkColorPartialHover"/>
						</xsl:call-template>
					</xsl:if>

					<!--MarkColorPartialInvalidHover-->
					<xsl:if test="MarkColorPartialInvalidHover">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">MarkColorPartialInvalidHover</xsl:with-param>
							<xsl:with-param name="brushNode" select="MarkColorPartialInvalidHover"/>
						</xsl:call-template>
					</xsl:if>

					<!--MarkColorPartialInvalidNormal-->
					<xsl:if test="MarkColorPartialInvalidNormal">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">MarkColorPartialInvalidNormal</xsl:with-param>
							<xsl:with-param name="brushNode" select="MarkColorPartialInvalidNormal"/>
						</xsl:call-template>
					</xsl:if>

					<!--MarkColorPartialInvalidPressed-->
					<xsl:if test="MarkColorPartialInvalidPressed">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">MarkColorPartialInvalidPressed</xsl:with-param>
							<xsl:with-param name="brushNode" select="MarkColorPartialInvalidPressed"/>
						</xsl:call-template>
					</xsl:if>

					<!--MarkColorPartialNormal-->
					<xsl:if test="MarkColorPartialNormal">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">MarkColorPartialNormal</xsl:with-param>
							<xsl:with-param name="brushNode" select="MarkColorPartialNormal"/>
						</xsl:call-template>
					</xsl:if>

					<!--MarkColorPartialPressed-->
					<xsl:if test="MarkColorPartialPressed">
						<xsl:call-template name="generateBrush">
							<xsl:with-param name="key">MarkColorPartialPressed</xsl:with-param>
							<xsl:with-param name="brushNode" select="MarkColorPartialPressed"/>
						</xsl:call-template>
					</xsl:if>

					<!--TextForeground-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">TextForeground</xsl:with-param>
						<xsl:with-param name="brushNode" select="TextForeground"/>
					</xsl:call-template>

					<!--TextForegroundDisabled-->
					<xsl:call-template name="generateBrush">
						<xsl:with-param name="key">TextForegroundDisabled</xsl:with-param>
						<xsl:with-param name="brushNode" select="TextForegroundDisabled"/>
					</xsl:call-template>

				</ResourceDictionary>

			</xsl:element>
		</xsl:if>

	</xsl:template>

</xsl:transform>