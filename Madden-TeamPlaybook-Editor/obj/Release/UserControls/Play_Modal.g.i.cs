﻿#pragma checksum "..\..\..\UserControls\Play_Modal.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "EE87268962896C6C7EA62A4E2E61232B4FA2C936A3B7A71DA3F03A6F003C0D2D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaddenTeamPlaybookEditor.User_Controls;
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
    /// PlayModal
    /// </summary>
    public partial class PlayModal : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\UserControls\Play_Modal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaddenTeamPlaybookEditor.User_Controls.PlayModal uclPlayModal;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\UserControls\Play_Modal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas cvsField;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\UserControls\Play_Modal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border bdrField;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\UserControls\Play_Modal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl iclPSALs;
        
        #line default
        #line hidden
        
        
        #line 180 "..\..\..\UserControls\Play_Modal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl iclIcons;
        
        #line default
        #line hidden
        
        
        #line 263 "..\..\..\UserControls\Play_Modal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel ReferenceInfo;
        
        #line default
        #line hidden
        
        
        #line 271 "..\..\..\UserControls\Play_Modal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl tabPlayControls;
        
        #line default
        #line hidden
        
        
        #line 307 "..\..\..\UserControls\Play_Modal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl tabPlay;
        
        #line default
        #line hidden
        
        
        #line 473 "..\..\..\UserControls\Play_Modal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvwSituations;
        
        #line default
        #line hidden
        
        
        #line 521 "..\..\..\UserControls\Play_Modal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl tabPlayer;
        
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
            System.Uri resourceLocater = new System.Uri("/Madden-Playbook-Editor;component/usercontrols/play_modal.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\Play_Modal.xaml"
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
            this.uclPlayModal = ((MaddenTeamPlaybookEditor.User_Controls.PlayModal)(target));
            return;
            case 2:
            this.cvsField = ((System.Windows.Controls.Canvas)(target));
            return;
            case 3:
            this.bdrField = ((System.Windows.Controls.Border)(target));
            
            #line 77 "..\..\..\UserControls\Play_Modal.xaml"
            this.bdrField.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.bdrField_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.iclPSALs = ((System.Windows.Controls.ItemsControl)(target));
            
            #line 107 "..\..\..\UserControls\Play_Modal.xaml"
            this.iclPSALs.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.iclPSALs_MouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.iclIcons = ((System.Windows.Controls.ItemsControl)(target));
            
            #line 185 "..\..\..\UserControls\Play_Modal.xaml"
            this.iclIcons.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.iclIcons_MouseDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ReferenceInfo = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 7:
            this.tabPlayControls = ((System.Windows.Controls.TabControl)(target));
            return;
            case 8:
            this.tabPlay = ((System.Windows.Controls.TabControl)(target));
            return;
            case 9:
            this.lvwSituations = ((System.Windows.Controls.ListView)(target));
            return;
            case 10:
            this.tabPlayer = ((System.Windows.Controls.TabControl)(target));
            return;
            case 11:
            
            #line 738 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.DataGrid)(target)).CurrentCellChanged += new System.EventHandler<System.EventArgs>(this.dgdPSALupdated);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

