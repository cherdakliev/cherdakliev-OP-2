#pragma checksum "..\..\..\DataStu.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "71BE3E0BA0B8265F4B6360C1BB4F517D983CD255"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Lab_01_work;
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


namespace Lab_01_work {
    
    
    /// <summary>
    /// Window1
    /// </summary>
    public partial class Window1 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\DataStu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NumberStu;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\DataStu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LstName;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\DataStu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FstName;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\DataStu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MdlName;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\DataStu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox GroupStu;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\DataStu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SendBut;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.13.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Lab_01 work;V1.0.0.0;component/datastu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\DataStu.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.13.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.NumberStu = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.LstName = ((System.Windows.Controls.TextBox)(target));
            
            #line 21 "..\..\..\DataStu.xaml"
            this.LstName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.NumberStu_Copy_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.FstName = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\..\DataStu.xaml"
            this.FstName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.NumberStu_Copy_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.MdlName = ((System.Windows.Controls.TextBox)(target));
            
            #line 23 "..\..\..\DataStu.xaml"
            this.MdlName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.NumberStu_Copy_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.GroupStu = ((System.Windows.Controls.TextBox)(target));
            
            #line 24 "..\..\..\DataStu.xaml"
            this.GroupStu.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.NumberStu_Copy_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SendBut = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\DataStu.xaml"
            this.SendBut.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

