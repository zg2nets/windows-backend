/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Runtime.InteropServices;

namespace Tizen.FH.NUI.Accessibility
{
    internal static partial class Interop
    {
        internal static partial class Accessibility
        {
            internal static int MIN_SPEECH_SPEED = 0;
            internal static int MAX_SPEECH_SPEED = 14;
            internal static int MIN_FONT_SIZE = 0;
            internal static int MAX_FONT_SIZE = 6;
            internal static int MIN_ACCESSIBLE_SCREEN_LEVEL = 0;
            internal static int MAX_ACCESSIBLE_SCREEN_LEVEL = 10;

            internal enum accessibility_error_e
            {
                ACCESSIBILITY_ERROR_NONE = 0, // = TIZEN_ERROR_NONE, /**< Successful */
                ACCESSIBILITY_ERROR_INVALID_PARAMETER = -22, // = TIZEN_ERROR_INVALID_PARAMETER, /**< Invalid parameter */
                ACCESSIBILITY_ERROR_OUT_OF_MEMORY = -12, // = TIZEN_ERROR_OUT_OF_MEMORY, /**< Out of memory */
                ACCESSIBILITY_ERROR_IO_ERROR = -5, // =  TIZEN_ERROR_IO_ERROR, /**< Internal I/O error */
                ACCESSIBILITY_ERROR_PERMISSION_DENIED = -13, // =  TIZEN_ERROR_PERMISSION_DENIED, /**< Permission denied */
                ACCESSIBILITY_ERROR_NOT_SUPPORTED = -1073741824 + 2, // =  TIZEN_ERROR_NOT_SUPPORTED, /**< Not supported @if MOBILE (Since 2.3.1) @endif */
                ACCESSIBILITY_ERROR_CALL_UNSUPPORTED_API, // = TIZEN_ERROR_NOT_SUPPORTED, /**< Not supported @if MOBILE (Since 2.3.1) @endif */
            }

            internal enum accessibility_key_e
            {
                ACCESSIBILITY_KEY_SCREEN_READER, /**< (bool) Indicates whether the screen-reader is enabled. */
                ACCESSIBILITY_KEY_SPEECH_SPEED, /**< (int) Indicates the screen-reader's speech speed. (0 to 14)*/
                ACCESSIBILITY_KEY_FONT_SIZE, /**< (int) Indicates the font size. (0 to 6)*/
                ACCESSIBILITY_KEY_GRAYSCALE, /**< (bool) Indicates whether the display is grayscale color. */
                ACCESSIBILITY_KEY_NEGATIVE, /**< (bool) Indicates whether the display is negative color. */
                ACCESSIBILITY_KEY_ACCESSIBLE_SCREEN, /**< (bool) Indicates the accessible screen is enabled.*/
                ACCESSIBILITY_KEY_ACCESSIBLE_SCREEN_LEVEL, /**< (Int) Indicates the accessible screen level. (0 to 10)*/
                ACCESSIBILITY_KEY_MAX = 0xfe,
            }

            internal delegate void accessibility_changed_cb(accessibility_key_e key, IntPtr user_data);

            internal delegate bool accessibility_iter_cb(int index, IntPtr value, IntPtr cb_data);

            [DllImport("capi-accessibility.so", EntryPoint = "accessibility_set_value_int")]
            internal static extern int accessibility_set_value_int(accessibility_key_e key, int value);

            [DllImport("capi-accessibility.so", EntryPoint = "accessibility_get_value_int")]
            internal static extern int accessibility_get_value_int(accessibility_key_e key, out int value);

            [DllImport("capi-accessibility.so", EntryPoint = "accessibility_set_value_bool")]
            internal static extern int accessibility_set_value_bool(accessibility_key_e key, bool value);

            [DllImport("capi-accessibility.so", EntryPoint = "accessibility_get_value_bool")]
            internal static extern int accessibility_get_value_bool(accessibility_key_e key, out bool value);

            [DllImport("capi-accessibility.so", EntryPoint = "accessibility_set_value_string")]
            internal static extern int accessibility_set_value_string(accessibility_key_e key, string value);

            [DllImport("capi-accessibility.so", EntryPoint = "accessibility_get_value_string")]
            internal static extern int accessibility_get_value_string(accessibility_key_e key, string[] value);

            [DllImport("capi-accessibility.so", EntryPoint = "accessibility_set_changed_cb")]
            internal static extern int accessibility_set_changed_cb(accessibility_key_e key, accessibility_changed_cb callback, IntPtr user_data);

            [DllImport("capi-accessibility.so", EntryPoint = "accessibility_unset_changed_cb")]
            internal static extern int accessibility_unset_changed_cb(accessibility_key_e key);

            [DllImport("capi-accessibility.so", EntryPoint = "accessibility_foreach_value_string")]
            internal static extern int accessibility_foreach_value_string(accessibility_key_e key, accessibility_iter_cb callback, IntPtr user_data);

            [DllImport("capi-accessibility.so", EntryPoint = "accessibility_add_value_string")]
            internal static extern int accessibility_add_value_string(accessibility_key_e key, string value);

            [DllImport("capi-accessibility.so", EntryPoint = "accessibility_delete_value_string")]
            internal static extern int accessibility_delete_value_string(accessibility_key_e key, string value);

            [DllImport("capi-accessibility.so", EntryPoint = "accessibility_add_changed_cb")]
            internal static extern int accessibility_add_changed_cb(accessibility_key_e key, accessibility_changed_cb callback, IntPtr user_data);

            [DllImport("capi-accessibility.so", EntryPoint = "accessibility_remove_changed_cb")]
            internal static extern int accessibility_remove_changed_cb(accessibility_key_e key, accessibility_changed_cb callback);
        }
    }
}