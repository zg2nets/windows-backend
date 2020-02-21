#ifndef DALI_TOOLKIT_TEXT_LABEL_DEVEL_H
#define DALI_TOOLKIT_TEXT_LABEL_DEVEL_H

/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

// INTERNAL INCLUDES
#include <dali-toolkit/public-api/controls/text-controls/text-label.h>

namespace Dali
{

namespace Toolkit
{

namespace DevelTextLabel
{

namespace Property
{
  enum Type
  {
    RENDERING_BACKEND = Dali::Toolkit::TextLabel::Property::RENDERING_BACKEND,
    TEXT = Dali::Toolkit::TextLabel::Property::TEXT,
    FONT_FAMILY = Dali::Toolkit::TextLabel::Property::FONT_FAMILY,
    FONT_STYLE = Dali::Toolkit::TextLabel::Property::FONT_STYLE,
    POINT_SIZE = Dali::Toolkit::TextLabel::Property::POINT_SIZE,
    MULTI_LINE = Dali::Toolkit::TextLabel::Property::MULTI_LINE,
    HORIZONTAL_ALIGNMENT = Dali::Toolkit::TextLabel::Property::HORIZONTAL_ALIGNMENT,
    VERTICAL_ALIGNMENT = Dali::Toolkit::TextLabel::Property::VERTICAL_ALIGNMENT,
    UNUSED_PROPERTY_TEXT_COLOR = Dali::Toolkit::TextLabel::Property::UNUSED_PROPERTY_TEXT_COLOR,
    RESERVED_PROPERTY_01 = Dali::Toolkit::TextLabel::Property::RESERVED_PROPERTY_01,
    RESERVED_PROPERTY_02 = Dali::Toolkit::TextLabel::Property::RESERVED_PROPERTY_02,
    RESERVED_PROPERTY_03 = Dali::Toolkit::TextLabel::Property::RESERVED_PROPERTY_03,
    RESERVED_PROPERTY_04 = Dali::Toolkit::TextLabel::Property::RESERVED_PROPERTY_04,
    RESERVED_PROPERTY_05 = Dali::Toolkit::TextLabel::Property::RESERVED_PROPERTY_05,
    ENABLE_MARKUP = Dali::Toolkit::TextLabel::Property::ENABLE_MARKUP,
    ENABLE_AUTO_SCROLL = Dali::Toolkit::TextLabel::Property::ENABLE_AUTO_SCROLL,
    AUTO_SCROLL_SPEED = Dali::Toolkit::TextLabel::Property::AUTO_SCROLL_SPEED,
    AUTO_SCROLL_LOOP_COUNT = Dali::Toolkit::TextLabel::Property::AUTO_SCROLL_LOOP_COUNT,
    AUTO_SCROLL_GAP = Dali::Toolkit::TextLabel::Property::AUTO_SCROLL_GAP,
    LINE_SPACING = Dali::Toolkit::TextLabel::Property::LINE_SPACING,
    UNDERLINE = Dali::Toolkit::TextLabel::Property::UNDERLINE,
    SHADOW = Dali::Toolkit::TextLabel::Property::SHADOW,
    EMBOSS = Dali::Toolkit::TextLabel::Property::EMBOSS,
    OUTLINE = Dali::Toolkit::TextLabel::Property::OUTLINE,
    PIXEL_SIZE = Dali::Toolkit::TextLabel::Property::PIXEL_SIZE,
    ELLIPSIS = Dali::Toolkit::TextLabel::Property::ELLIPSIS,
    AUTO_SCROLL_LOOP_DELAY = Dali::Toolkit::TextLabel::Property::AUTO_SCROLL_LOOP_DELAY,
    AUTO_SCROLL_STOP_MODE = Dali::Toolkit::TextLabel::Property::AUTO_SCROLL_STOP_MODE,
    LINE_COUNT = Dali::Toolkit::TextLabel::Property::LINE_COUNT,
    LINE_WRAP_MODE = Dali::Toolkit::TextLabel::Property::LINE_WRAP_MODE,

    /**
     * @brief The direction of the layout.
     * @details Name "textDirection", type [Type](@ref Dali::Toolkit::DevelText::TextDirection::Type) (Property::INTEGER), Read/Write
     * @note The text direction can be changed only by replacing the text itself.
     * @see TextDirection::Type for supported values.
     */
    TEXT_DIRECTION,

    /**
     * @brief Alignment of text within area of single line
     * @details Name "verticalLineAlignment", type [Type](@ref Dali::Toolkit::DevelText::VerticalLineAlignment::Type) (Property::INTEGER), Read/Write
     * @note The default value is TOP
     * @see VerticalLineAlignment::Type for supported values
     */
    VERTICAL_LINE_ALIGNMENT,

    /**
     * @brief The default text background parameters.
     * @details Name "textBackground", type Property::MAP.
     * @note Use "textBackground" as property name to avoid conflict with Control's "background" property
     *
     * The background map contains the following keys:
     *
     * | %Property Name       | Type     | Required | Description                                                                                                        |
     * |----------------------|----------|----------|--------------------------------------------------------------------------------------------------------------------|
     * | enable               | BOOLEAN  | No       | True to enable the background or false to disable (the default value is false)                                     |
     * | color                | VECTOR4  | No       | The color of the background (the default value is Color::CYAN)                                                     |
     */
    BACKGROUND,

    /**
     * @brief Ignore spaces after text.
     * @details Name "ignoreSpacesAfterText", type (Property::BOLEAN), Read/Write
     * @note The default value is true
     */
    IGNORE_SPACES_AFTER_TEXT,

    /**
     * @brief Modifies the default text alignment to match the direction of the system language.
     * @details Name "matchSystemLanguageDirection", type (Property::BOLEAN), Read/Write
     * @note The default value is false
     *
     * If MATCH_SYSTEM_LANGUAGE_DIRECTION property set true, the default text alignment to match the direction of the system language.
     *
     * ex) Current system language direction LTR.
     *     TextLabel::New("Hello world \n  ﻡﺮﺤﺑﺍ. ");
     *     TextLabel::Property::HORIZONTAL_ALIGNMENT, "END"
     *
     * | TextLabel::Property::MATCH_SYSTEM_LANGUAGE_DIRECTION                 |
     * |-----------------------------------------------------------------------
     * |        false (default)            |                true              |
     * |-----------------------------------|----------------------------------|
     * |                     Hello world   |                  Hello world     |
     * |   ﻡﺮﺤﺑﺍ.                          |                      ﻡﺮﺤﺑﺍ.      |
     *
     */
    MATCH_SYSTEM_LANGUAGE_DIRECTION,

    /**
     * @brief The text fit parameters.
     * @details Name "textFit", type Property::MAP.
     * @note The default value is false
     *
     * The textFit map contains the following keys:
     *
     * | %Property Name       | Type     | Required | Description                                                                                                        |
     * |----------------------|----------|----------|--------------------------------------------------------------------------------------------------------------------|
     * | enable               | BOOLEAN  | No       | True to enable the text fit or false to disable (the default value is false)                                     |
     * | minSize              | FLOAT    | No       | Minimum Size for text fit (the default value is 10.f)                                                     |
     * | maxSize              | FLOAT    | No       | Maximum Size for text fit (the default value is 100.f)                                                     |
     * | stepSize             | FLOAT    | No       | Step Size for font increase (the default value is 1.f)                                                     |
     * | fontSizeType         | STRING   | No       | The size type of font, You can choose between "pointSize" or "pixelSize". (the default value is "pointSize")                                                     |
     */
    TEXT_FIT,

  };

} // namespace Property

} // namespace DevelTextLabel

} // namespace Toolkit

} // namespace Dali

#endif // DALI_TOOLKIT_TEXT_LABEL_DEVEL_H
