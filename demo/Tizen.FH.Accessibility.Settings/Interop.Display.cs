﻿/*
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

using System.Runtime.InteropServices;

namespace Tizen.FH.NUI.Accessibility
{
    internal static partial class Interop
    {
        internal static partial class Display
        {
            [DllImport("capi-system-device.so.0", EntryPoint = "device_display_set_color_grayscale")]
            internal static extern int device_display_set_color_grayscale(int value);

            [DllImport("capi-system-device.so.0", EntryPoint = "device_display_set_color_negative")]
            internal static extern int device_display_set_color_negative(int value);
        }
    }
}
