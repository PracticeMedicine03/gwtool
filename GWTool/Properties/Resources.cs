// Decompiled with JetBrains decompiler
// Type: GWTool.Properties.Resources
// Assembly: GWTool, Version=0.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9A611879-1CBE-467F-9A4F-F5F3330B5275
// Assembly location: D:\GWtool_File_made_by_ZombieSlayer103\GWtool File made by ZombieSlayer103\GWTool.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace GWTool.Properties
{
    [CompilerGenerated]
    [DebuggerNonUserCode]
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    internal class Resources
    {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal Resources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals((object)GWTool.Properties.Resources.resourceMan, (object)null))
                    GWTool.Properties.Resources.resourceMan = new ResourceManager("GWTool.Properties.Resources", typeof(GWTool.Properties.Resources).Assembly);
                return GWTool.Properties.Resources.resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get => GWTool.Properties.Resources.resourceCulture;
            set => GWTool.Properties.Resources.resourceCulture = value;
        }
    }
}
