/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd.
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

using System;
using Tizen.NUI;

namespace Tizen.FH.NUI.Accessibility
{
    internal class AccessibilityController
    {
        private static AccessibilityController instance = null;
        private float default_font_size_ratio = 1.0f;

        internal static AccessibilityController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccessibilityController();
                }
                return instance;
            }
        }

        internal enum TTSRate
        {
            TTS_RATE_0 = 0,
            TTS_RATE_1,
            TTS_RATE_2,
            TTS_RATE_3,
            TTS_RATE_4,
            TTS_RATE_END,
        }

        internal enum FontSize
        {
            FONT_SIZE_0 = 0,
            FONT_SIZE_1,
            FONT_SIZE_2,
            FONT_SIZE_3,
            FONT_SIZE_4,
            FONT_SIZE_5,
            FONT_SIZE_6,
            FONT_SIZE_END,
        }

        private AccessibilityController()
        {
            default_font_size_ratio = accessibility_default_font_size_ratio_get();
        }

        internal TTSRate accessibility_tts_rate_get()
        {
            return 0; // for testing

            //int speed = 0;
            //int result = Interop.Accessibility.accessibility_get_value_int(Interop.Accessibility.accessibility_key_e.ACCESSIBILITY_KEY_SPEECH_SPEED, out speed);

            //Tizen.Log.Info("accessibility", "speed: [" + speed + "]");

            //if ((Interop.Accessibility.accessibility_error_e)result != Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_NONE)
            //{
            //    printError((Interop.Accessibility.accessibility_error_e)result);
            //}

            //if (speed <= 2)
            //{
            //    return TTSRate.TTS_RATE_0;
            //}
            //else if (speed <= 5)
            //{
            //    return TTSRate.TTS_RATE_1;
            //}
            //else if (speed <= 8)
            //{
            //    return TTSRate.TTS_RATE_2;
            //}
            //else if (speed <= 11)
            //{
            //    return TTSRate.TTS_RATE_3;
            //}

            //return TTSRate.TTS_RATE_4;
        }

        internal int accessibility_tts_rate_set(TTSRate rate)
        {
            return 0; // for testing


            //int speed = 8;

            //switch (rate)
            //{
            //    case TTSRate.TTS_RATE_0:
            //        speed = 2;
            //        break;
            //    case TTSRate.TTS_RATE_1:
            //        speed = 5;
            //        break;
            //    case TTSRate.TTS_RATE_2:
            //        speed = 8;
            //        break;
            //    case TTSRate.TTS_RATE_3:
            //        speed = 11;
            //        break;
            //    case TTSRate.TTS_RATE_4:
            //        speed = 14;
            //        break;
            //    default:
            //        break;
            //}

            //int result = Interop.Accessibility.accessibility_set_value_int(Interop.Accessibility.accessibility_key_e.ACCESSIBILITY_KEY_SPEECH_SPEED, speed);
            //Tizen.Log.Info("accessibility", "speed: [" + speed + "]");

            //if ((Interop.Accessibility.accessibility_error_e)result != Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_NONE)
            //{
            //    printError((Interop.Accessibility.accessibility_error_e)result);
            //}

            //return speed;
        }

        internal bool accessibility_screen_reader_state_get()
        {
            return false; // for testing

            //bool screen_reader = false;
            //int result = Interop.Accessibility.accessibility_get_value_bool(Interop.Accessibility.accessibility_key_e.ACCESSIBILITY_KEY_SCREEN_READER, out screen_reader);

            //string bv = screen_reader ? "true" : "false";
            //Tizen.Log.Info("accessibility", "screen_reader: [" + bv + "]");

            //if ((Interop.Accessibility.accessibility_error_e)result != Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_NONE)
            //{
            //    printError((Interop.Accessibility.accessibility_error_e)result);
            //}

            //return screen_reader;
        }

        internal void accessibility_screen_reader_state_set(bool state)
        {
            return ; // for testing

            //string bv = state ? "true" : "false";
            //Tizen.Log.Info("accessibility", "screen_reader: [" + bv + "]");

            //int result = Interop.Accessibility.accessibility_set_value_bool(Interop.Accessibility.accessibility_key_e.ACCESSIBILITY_KEY_SCREEN_READER, state);

            //if ((Interop.Accessibility.accessibility_error_e)result != Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_NONE)
            //{
            //    printError((Interop.Accessibility.accessibility_error_e)result);
            //}
        }

        internal string accessibility_convert_font_size_to_text(FontSize fs)
        {
            switch (fs)
            {
                case AccessibilityController.FontSize.FONT_SIZE_0:
                    return "Extra small";
                case AccessibilityController.FontSize.FONT_SIZE_1:
                    return "Small";
                case AccessibilityController.FontSize.FONT_SIZE_2:
                    return "Medium";
                case AccessibilityController.FontSize.FONT_SIZE_3:
                    return "Large";
                case AccessibilityController.FontSize.FONT_SIZE_4:
                    return "Extra large";
                case AccessibilityController.FontSize.FONT_SIZE_5:
                    return "Huge";
                case AccessibilityController.FontSize.FONT_SIZE_6:
                    return "Extra huge";
                default:
                    break;
            }
            return "";
        }

        internal float accessibility_default_font_size_ratio_get()
        {
            FontSize default_size = accessibility_font_size_get();

            if (default_size < FontSize.FONT_SIZE_0 || default_size > FontSize.FONT_SIZE_6)
            {
                Tizen.Log.Fatal("accessibility", "default font size is not correct!");
                return 1.0f;
            }

            float font_size = 0.0f;
            switch (default_size)
            {
                case FontSize.FONT_SIZE_0:
                    font_size = 0.70f;
                    break;
                case FontSize.FONT_SIZE_1:
                    font_size = 0.85f;
                    break;
                case FontSize.FONT_SIZE_2:
                    font_size = 1.0f;
                    break;
                case FontSize.FONT_SIZE_3:
                    font_size = 1.1f;
                    break;
                case FontSize.FONT_SIZE_4:
                    font_size = 1.25f;
                    break;
                case FontSize.FONT_SIZE_5:
                    font_size = 1.35f;
                    break;
                case FontSize.FONT_SIZE_6:
                    font_size = 1.50f;
                    break;
                default:
                    break;
            }

            return font_size;
        }

        internal FontSize accessibility_font_size_get()
        {
            return 0; 

            //int font_size = -1;
            //int result = Interop.Accessibility.accessibility_get_value_int(Interop.Accessibility.accessibility_key_e.ACCESSIBILITY_KEY_FONT_SIZE, out font_size);

            //Tizen.Log.Info("accessibility", "font_size: [" + font_size + "]");

            //if ((Interop.Accessibility.accessibility_error_e)result != Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_NONE)
            //{
            //    printError((Interop.Accessibility.accessibility_error_e)result);
            //}

            //return (FontSize)font_size;
        }

        internal void accessibility_font_size_set(FontSize size)
        {
            return;

            //Tizen.Log.Info("accessibility", "size: [" + size + "]");
            //int result = Interop.Accessibility.accessibility_set_value_int(Interop.Accessibility.accessibility_key_e.ACCESSIBILITY_KEY_FONT_SIZE, (int)size);

            //if ((Interop.Accessibility.accessibility_error_e)result != Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_NONE)
            //{
            //    printError((Interop.Accessibility.accessibility_error_e)result);
            //}
        }

        internal bool accessibility_grayscale_state_get()
        {
            return false; // for testing

            //bool gray_scale = false;
            //int result = Interop.Accessibility.accessibility_get_value_bool(Interop.Accessibility.accessibility_key_e.ACCESSIBILITY_KEY_GRAYSCALE, out gray_scale);

            //if ((Interop.Accessibility.accessibility_error_e)result != Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_NONE)
            //{
            //    printError((Interop.Accessibility.accessibility_error_e)result);
            //}

            //int gci = gray_scale ? 1 : 0;
            //result = Interop.Display.device_display_set_color_grayscale(gci);

            //string bs = gray_scale ? "true" : "false";
            //Tizen.Log.Info("accessibility", "gray_scale: [" + bs + "]");

            //if ((Interop.Accessibility.accessibility_error_e)result != Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_NONE)
            //{
            //    printError((Interop.Accessibility.accessibility_error_e)result);
            //}

            //return gray_scale;
        }

        internal void accessibility_grayscale_state_set(bool state)
        {
            return ; // for testing

            //string bs = state ? "true" : "false";
            //Tizen.Log.Info("accessibility", "gray_scale: [" + bs + "]");

            //int result = Interop.Accessibility.accessibility_set_value_bool(Interop.Accessibility.accessibility_key_e.ACCESSIBILITY_KEY_GRAYSCALE, state);

            //if ((Interop.Accessibility.accessibility_error_e)result != Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_NONE)
            //{
            //    printError((Interop.Accessibility.accessibility_error_e)result);
            //}

            //int iv = state ? 1 : 0;
            //result = Interop.Display.device_display_set_color_grayscale(iv);

            //if ((Interop.Accessibility.accessibility_error_e)result != Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_NONE)
            //{
            //    printError((Interop.Accessibility.accessibility_error_e)result);
            //}
        }


        internal bool accessibility_negative_color_state_get()
        {
            return false; // for testing

            //bool negative_color = false;
            //int result = Interop.Accessibility.accessibility_get_value_bool(Interop.Accessibility.accessibility_key_e.ACCESSIBILITY_KEY_NEGATIVE, out negative_color);

            //if ((Interop.Accessibility.accessibility_error_e)result != Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_NONE)
            //{
            //    printError((Interop.Accessibility.accessibility_error_e)result);
            //}

            //int nci = negative_color ? 1 : 0;
            //result = Interop.Display.device_display_set_color_negative(nci);

            //string bv = negative_color ? "true" : "false";
            //Tizen.Log.Info("accessibility", "negative_color: [" + bv + "]");

            //if ((Interop.Accessibility.accessibility_error_e)result != Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_NONE)
            //{
            //    printError((Interop.Accessibility.accessibility_error_e)result);
            //}

            //return negative_color;
        }

        internal void accessibility_negative_color_state_set(bool state)
        {
            return ; // for testing

            //string bv = state ? "true" : "false";
            //Tizen.Log.Info("accessibility", "negative_color: [" + bv + "]");
            //int result = Interop.Accessibility.accessibility_set_value_bool(Interop.Accessibility.accessibility_key_e.ACCESSIBILITY_KEY_NEGATIVE, state);

            //if ((Interop.Accessibility.accessibility_error_e)result != Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_NONE)
            //{
            //    printError((Interop.Accessibility.accessibility_error_e)result);
            //}

            //int iv = state ? 1 : 0;
            //result = Interop.Display.device_display_set_color_negative(iv);

            //if ((Interop.Accessibility.accessibility_error_e)result != Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_NONE)
            //{
            //    printError((Interop.Accessibility.accessibility_error_e)result);
            //}
        }

        internal void accessibility_accessible_screen_set(bool state)
        {
            return ; // for testing

            //string bv = state ? "true" : "false";
            //Tizen.Log.Info("accessibility", "accessible_screen: [" + bv + "]");

            //int result = Interop.Accessibility.accessibility_set_value_bool(Interop.Accessibility.accessibility_key_e.ACCESSIBILITY_KEY_ACCESSIBLE_SCREEN, state);

            //if ((Interop.Accessibility.accessibility_error_e)result != Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_NONE)
            //{
            //    printError((Interop.Accessibility.accessibility_error_e)result);
            //}
        }

        internal bool accessibility_accessible_screen_get()
        {
            return false; // for testing

            //bool accessible_screen = false;
            //int result = Interop.Accessibility.accessibility_get_value_bool(Interop.Accessibility.accessibility_key_e.ACCESSIBILITY_KEY_ACCESSIBLE_SCREEN, out accessible_screen);

            //string bv = accessible_screen ? "true" : "false";
            //Tizen.Log.Info("accessibility", "accessible_screen: [" + bv + "]" );

            //if ((Interop.Accessibility.accessibility_error_e)result != Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_NONE)
            //{
            //    printError((Interop.Accessibility.accessibility_error_e)result);
            //}

            //return accessible_screen;
        }

        internal void accessibility_accessible_screen_percentage_set(int percentage)
        {
            return ; // for testing


            //Tizen.Log.Info("accessibility", "percentage: [" + percentage + "]");
            //int result = Interop.Accessibility.accessibility_set_value_int(Interop.Accessibility.accessibility_key_e.ACCESSIBILITY_KEY_ACCESSIBLE_SCREEN_LEVEL, percentage / 10);

            //if ((Interop.Accessibility.accessibility_error_e)result != Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_NONE)
            //{
            //    printError((Interop.Accessibility.accessibility_error_e)result);
            //}
        }

        internal int accessibility_accessible_screen_percentage_get()
        {
            return 0; // for testing

            //int percentage = 0;
            //int result = Interop.Accessibility.accessibility_get_value_int(Interop.Accessibility.accessibility_key_e.ACCESSIBILITY_KEY_ACCESSIBLE_SCREEN_LEVEL, out percentage);

            //percentage *= 10;
            //Tizen.Log.Info("accessibility", "percentage: [" + percentage + "]");

            //if ((Interop.Accessibility.accessibility_error_e)result != Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_NONE)
            //{
            //    printError((Interop.Accessibility.accessibility_error_e)result);
            //}

            //return percentage;
        }

        internal bool accessibility_side_key_state_get()
        {
            return false; // for testing

            //bool side_key_state = true;

            //string bv = side_key_state ? "true" : "false";
            //Tizen.Log.Info("accessibility", "side_key_state: [" + bv + "]");

            //return side_key_state;
        }

        internal void accessibility_side_key_state_set(bool state)
        {
            return ; // for testing

            //string bv = state ? "true" : "false";
            //Tizen.Log.Info("accessibility", "side_key_state: [" + bv + "]");
        }

        //private void printError(Interop.Accessibility.accessibility_error_e e)
        //{
        //    switch(e)
        //    {
        //        case Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_INVALID_PARAMETER:
        //            Tizen.Log.Fatal("accessibility", "parameter is invalid!");
        //            break;
        //        case Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_OUT_OF_MEMORY:
        //            Tizen.Log.Fatal("accessibility", "out of memory!");
        //            break;
        //        case Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_IO_ERROR:
        //            Tizen.Log.Fatal("accessibility", "Internal I/O error!");
        //            break;
        //        case Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_PERMISSION_DENIED:
        //            Tizen.Log.Fatal("accessibility", "permission denied!");
        //            break;
        //        case Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_NOT_SUPPORTED:
        //            Tizen.Log.Fatal("accessibility", "not supported!");
        //            break;
        //        case Interop.Accessibility.accessibility_error_e.ACCESSIBILITY_ERROR_CALL_UNSUPPORTED_API:
        //            Tizen.Log.Fatal("accessibility", "not supported!");
        //            break;
        //        default:
        //            break;
        //    }
        //}

    }
}
