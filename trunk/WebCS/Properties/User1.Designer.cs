﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3603
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebCS.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class User : global::System.Configuration.ApplicationSettingsBase {
        
        private static User defaultInstance = ((User)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new User())));
        
        public static User Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Select Webcam")]
        public string loadWebcamName {
            get {
                return ((string)(this["loadWebcamName"]));
            }
            set {
                this["loadWebcamName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Black")]
        public global::System.Drawing.Color firstMarkerColorUser {
            get {
                return ((global::System.Drawing.Color)(this["firstMarkerColorUser"]));
            }
            set {
                this["firstMarkerColorUser"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Black")]
        public global::System.Drawing.Color secondMarkerColorUser {
            get {
                return ((global::System.Drawing.Color)(this["secondMarkerColorUser"]));
            }
            set {
                this["secondMarkerColorUser"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("20")]
        public int firstMarkerRangeUser {
            get {
                return ((int)(this["firstMarkerRangeUser"]));
            }
            set {
                this["firstMarkerRangeUser"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("20")]
        public int secondMarkerRangeUser {
            get {
                return ((int)(this["secondMarkerRangeUser"]));
            }
            set {
                this["secondMarkerRangeUser"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool applyMedianFilter {
            get {
                return ((bool)(this["applyMedianFilter"]));
            }
            set {
                this["applyMedianFilter"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("20, 20, 312, 248")]
        public global::System.Drawing.Rectangle desktopAreaBoundriesRectangle {
            get {
                return ((global::System.Drawing.Rectangle)(this["desktopAreaBoundriesRectangle"]));
            }
            set {
                this["desktopAreaBoundriesRectangle"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool areDesktopAreaBoundriesVisible {
            get {
                return ((bool)(this["areDesktopAreaBoundriesVisible"]));
            }
            set {
                this["areDesktopAreaBoundriesVisible"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool isClickingEnabled {
            get {
                return ((bool)(this["isClickingEnabled"]));
            }
            set {
                this["isClickingEnabled"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool showCenterLine {
            get {
                return ((bool)(this["showCenterLine"]));
            }
            set {
                this["showCenterLine"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("50")]
        public int proximityClick {
            get {
                return ((int)(this["proximityClick"]));
            }
            set {
                this["proximityClick"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool applyMeanFilter {
            get {
                return ((bool)(this["applyMeanFilter"]));
            }
            set {
                this["applyMeanFilter"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool isMovingEnabled {
            get {
                return ((bool)(this["isMovingEnabled"]));
            }
            set {
                this["isMovingEnabled"] = value;
            }
        }
    }
}
