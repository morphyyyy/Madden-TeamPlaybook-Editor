﻿#pragma checksum "..\..\..\UserControls\Situation.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D784C5B101979B2A8C4FDA793B94B8EA45901B05864E02F98CAB19FB4A8F037A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaddenTeamPlaybookEditor.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace MaddenTeamPlaybookEditor.User_Controls {
    
    
    /// <summary>
    /// Situation
    /// </summary>
    public partial class Situation : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\UserControls\Situation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaddenTeamPlaybookEditor.User_Controls.Situation uclSituation;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\UserControls\Situation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar pbrSituation;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\UserControls\Situation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbkPlayName;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\UserControls\Situation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbkName;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Madden-Playbook-Editor;component/usercontrols/situation.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\Situation.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.uclSituation = ((MaddenTeamPlaybookEditor.User_Controls.Situation)(target));
            return;
            case 2:
            
            #line 56 "..\..\..\UserControls\Situation.xaml"
            ((System.Windows.Controls.Canvas)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.pbrSituation_MouseDown);
            
            #line default
            #line hidden
            
            #line 57 "..\..\..\UserControls\Situation.xaml"
            ((System.Windows.Controls.Canvas)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.pbrSituation_MouseMove);
            
            #line default
            #line hidden
            
            #line 58 "..\..\..\UserControls\Situation.xaml"
            ((System.Windows.Controls.Canvas)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.pbrSituation_MouseUp);
            
            #line default
            #line hidden
            
            #line 59 "..\..\..\UserControls\Situation.xaml"
            ((System.Windows.Controls.Canvas)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.pbrSituation_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 3:
            this.pbrSituation = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 4:
            this.tbkPlayName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.tbkName = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

