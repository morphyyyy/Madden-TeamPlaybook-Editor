﻿#pragma checksum "..\..\..\UserControls\Play_Modal.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5F03D0785038560FA4364070A4D8EA87AEFF764175D9D41B188C98FEBFC93FAF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaddenTeamPlaybookEditor.Classes;
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
        
        
        #line 7 "..\..\..\UserControls\Play_Modal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaddenTeamPlaybookEditor.User_Controls.PlayModal uclPlayModal;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\UserControls\Play_Modal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas cvsField;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\UserControls\Play_Modal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border bdrField;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\UserControls\Play_Modal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas cvsPlayart;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\UserControls\Play_Modal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl iclPSALs;
        
        #line default
        #line hidden
        
        
        #line 192 "..\..\..\UserControls\Play_Modal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl iclIcons;
        
        #line default
        #line hidden
        
        
        #line 276 "..\..\..\UserControls\Play_Modal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel ReferenceInfo;
        
        #line default
        #line hidden
        
        
        #line 284 "..\..\..\UserControls\Play_Modal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl tabPlayControls;
        
        #line default
        #line hidden
        
        
        #line 320 "..\..\..\UserControls\Play_Modal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl tabPlay;
        
        #line default
        #line hidden
        
        
        #line 517 "..\..\..\UserControls\Play_Modal.xaml"
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
            
            #line 75 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.savePlayart);
            
            #line default
            #line hidden
            return;
            case 3:
            this.cvsField = ((System.Windows.Controls.Canvas)(target));
            return;
            case 4:
            this.bdrField = ((System.Windows.Controls.Border)(target));
            
            #line 88 "..\..\..\UserControls\Play_Modal.xaml"
            this.bdrField.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.bdrField_MouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.cvsPlayart = ((System.Windows.Controls.Canvas)(target));
            return;
            case 6:
            this.iclPSALs = ((System.Windows.Controls.ItemsControl)(target));
            
            #line 119 "..\..\..\UserControls\Play_Modal.xaml"
            this.iclPSALs.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.iclPSALs_MouseDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.iclIcons = ((System.Windows.Controls.ItemsControl)(target));
            
            #line 197 "..\..\..\UserControls\Play_Modal.xaml"
            this.iclIcons.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.iclIcons_MouseDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ReferenceInfo = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 9:
            this.tabPlayControls = ((System.Windows.Controls.TabControl)(target));
            return;
            case 10:
            this.tabPlay = ((System.Windows.Controls.TabControl)(target));
            return;
            case 11:
            
            #line 413 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.plylChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 415 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.plylChanged);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 417 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.plylChanged);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 419 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.plylChanged);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 421 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.plylChanged);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 423 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.plylChanged);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 425 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.plylChanged);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 427 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.plylChanged);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 429 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.plylChanged);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 431 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.plylChanged);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 433 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.plylChanged);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 435 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.plylChanged);
            
            #line default
            #line hidden
            return;
            case 23:
            this.tabPlayer = ((System.Windows.Controls.TabControl)(target));
            return;
            case 24:
            
            #line 565 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 25:
            
            #line 567 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 26:
            
            #line 569 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 27:
            
            #line 571 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 28:
            
            #line 573 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 29:
            
            #line 575 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 30:
            
            #line 601 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 31:
            
            #line 603 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 32:
            
            #line 605 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 33:
            
            #line 607 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 34:
            
            #line 609 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 35:
            
            #line 611 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 36:
            
            #line 613 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 37:
            
            #line 615 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 38:
            
            #line 617 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 39:
            
            #line 619 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 40:
            
            #line 621 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 41:
            
            #line 623 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 42:
            
            #line 625 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 43:
            
            #line 627 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 44:
            
            #line 652 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 45:
            
            #line 654 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 46:
            
            #line 656 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 47:
            
            #line 658 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 48:
            
            #line 660 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 49:
            
            #line 662 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 50:
            
            #line 664 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 51:
            
            #line 666 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 52:
            
            #line 668 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 53:
            
            #line 670 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 54:
            
            #line 672 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 55:
            
            #line 674 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 56:
            
            #line 676 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 57:
            
            #line 701 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 58:
            
            #line 703 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 59:
            
            #line 705 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 60:
            
            #line 707 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 61:
            
            #line 709 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 62:
            
            #line 711 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 63:
            
            #line 713 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 64:
            
            #line 715 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 65:
            
            #line 717 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 66:
            
            #line 719 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 67:
            
            #line 721 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 68:
            
            #line 723 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 69:
            
            #line 725 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtPlayerUpdated);
            
            #line default
            #line hidden
            return;
            case 70:
            
            #line 734 "..\..\..\UserControls\Play_Modal.xaml"
            ((System.Windows.Controls.DataGrid)(target)).CurrentCellChanged += new System.EventHandler<System.EventArgs>(this.dgdPSALupdated);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

