// Decompiled with JetBrains decompiler
// Type: GWTool.Properties.Settings
// Assembly: GWTool, Version=0.3.0.0, Culture=neutral, PublicKeyToken=null

using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace GWTool.Properties
{
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    [CompilerGenerated]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static Settings defaultInstance = (Settings)SettingsBase.Synchronized((SettingsBase)new Settings());

        public static Settings Default
        {
            get
            {
                Settings defaultInstance = Settings.defaultInstance;
                return defaultInstance;
            }
        }
    }
}
