﻿/*
 * Copyright(c) 2019 Samsung Electronics Co., Ltd.
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
using System.ComponentModel;

namespace Tizen.NUI.CommonUI
{
    /// <summary>
    /// LoadingAttributes is a class which saves Loading's ux data.
    /// </summary>
    /// <since_tizen> 6 </since_tizen>
    /// This will be public opened in tizen_6 after ACR done. Before ACR, need to be hidden as inhouse API.
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class LoadingAttributes : ViewAttributes
    {
        /// <summary>
        /// Creates a new instance of a LoadingAttributes.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_6 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public LoadingAttributes() : base() { }

        /// <summary>
        /// Creates a new instance of a LoadingAttributes with attributes.
        /// </summary>
        /// <param name="attributes">Create LoadingAttributes by attributes customized by user.</param>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_6 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public LoadingAttributes(LoadingAttributes attributes) : base(attributes)
        {
            if(attributes == null)
            {
                return;
            }

            if (attributes.FPS != null)
            {
                FPS = attributes.FPS.Clone() as IntSelector;
            }
            if (attributes.LoadingSize!= null)
            {
                LoadingSize = attributes.LoadingSize;
            }
            if (attributes.ImageArray != null)
            {
                ImageArray = attributes.ImageArray;
            }
        }

        /// <summary>
        /// Loading Image resource URL array.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_6 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string[] ImageArray
        {
            get;
            set;
        }

        /// <summary>
        /// Get/Set Loading Image size.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_6 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Size2D LoadingSize
        {
            get;
            set;
        }

        /// <summary>
        /// Get/Set Loading frame per second.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_6 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public IntSelector FPS
        {
            get;
            set;
        }

        /// <summary>
        /// Attributes's clone function.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_6 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Attributes Clone()
        {
            return new LoadingAttributes(this);
        }
    }
}
