﻿#pragma checksum "..\..\..\ProtectionModeWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "33038BA82D1511ED986B641BCAE0E5D6F04DCE37"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Prac_01;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Prac_01 {
    
    
    /// <summary>
    /// ProtectionModeWindow
    /// </summary>
    public partial class ProtectionModeWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\ProtectionModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock VerifField;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\ProtectionModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox InputField;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\ProtectionModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseStudyMode;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\ProtectionModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label SymbolCount;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\ProtectionModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CountProtection;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\ProtectionModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label StatisticsBlock;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\ProtectionModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox AlphaSelector;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\ProtectionModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label P1Field;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\ProtectionModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label P2Field;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\ProtectionModeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label DispField;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.14.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Prac_01;component/protectionmodewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ProtectionModeWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.14.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.VerifField = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.InputField = ((System.Windows.Controls.TextBox)(target));
            
            #line 27 "..\..\..\ProtectionModeWindow.xaml"
            this.InputField.PreviewKeyUp += new System.Windows.Input.KeyEventHandler(this.InputField_PreviewKeyUp);
            
            #line default
            #line hidden
            
            #line 27 "..\..\..\ProtectionModeWindow.xaml"
            this.InputField.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.InputField_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CloseStudyMode = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\ProtectionModeWindow.xaml"
            this.CloseStudyMode.Click += new System.Windows.RoutedEventHandler(this.CloseStudyMode_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SymbolCount = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.CountProtection = ((System.Windows.Controls.ComboBox)(target));
            
            #line 42 "..\..\..\ProtectionModeWindow.xaml"
            this.CountProtection.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CountProtection_SelectionChanged_1);
            
            #line default
            #line hidden
            return;
            case 6:
            this.StatisticsBlock = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.AlphaSelector = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.P1Field = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.P2Field = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.DispField = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

