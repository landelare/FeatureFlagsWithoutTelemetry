﻿// Copyright (c) Paul Harrington.  All Rights Reserved.  Licensed under the MIT License.  See LICENSE in the project root for license information.

using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FeatureFlags
{
    [Guid("421B127D-D5A0-42EC-8534-692DABF30A26")]
    internal class FeatureFlagsOptionPage : DialogPage
    {
        private FeatureFlagsUserControl _page;

        private FeatureFlagsUserControl Page => _page ?? (_page = CreatePage());

        private FeatureFlagsUserControl CreatePage()
        {
            var featureFlagsDataModel = new FeatureFlagsDataModel(GetSettingsManagerService());
            return new FeatureFlagsUserControl(featureFlagsDataModel);
        }

        protected override IWin32Window Window => Page;

#pragma warning disable VSTHRD010 // Invoke single-threaded types on Main thread
        private IVsSettingsManager GetSettingsManagerService() => (IVsSettingsManager)GetService(typeof(SVsSettingsManager));
#pragma warning restore VSTHRD010 // Invoke single-threaded types on Main thread

        public override void SaveSettingsToStorage() => Page.WriteChanges();

        public override void LoadSettingsFromStorage() => Page.Initialize();
    }
}
