Imports TestTDBAccess.TDBAccess
Imports TestTDBAccess.TdbTableProperties
Imports System.Text

Public Class frmTDBAccess
    Inherits System.Windows.Forms.Form
    'Some private variables
    Private OpenIndex As Integer = -1
    Private Modified As Boolean = False
    Public RecNo As Short = 0
    Public SelectedTable As String = ""
    
    'Fills lstTables with table names from the database
    Private Sub LoadTables()
        'Number of tables
        Dim TableListSize, i As Integer
        'Properties of the current table
        Dim TableProps As TdbTableProperties
        'Table name buffer
        Dim TableName As New StringBuilder("    ", 5)
        'Makes sense only when database is opened
        If OpenIndex <> -1 Then
            'Get array of tables
            TableListSize = TDBDatabaseGetTableCount(OpenIndex)
            'Clear tables list
            lstTables.BeginUpdate()
            lstTables.Items.Clear()
            'For each table
            For i = 0 To TableListSize - 1
                TableProps.Name = TableName.ToString
                If TDBTableGetProperties(OpenIndex, i, TableProps) Then
                    lstTables.Items.Add(TableProps.Name)
                End If
            Next
            lstTables.EndUpdate()
        End If
    End Sub

    'Fills lstFields with field names from the database
    Private Sub LoadFields()
        Dim i As Integer
        'Properties of the current table
        Dim TableProps As TdbTableProperties
        'Properties of the current field
        Dim FieldProps As TdbFieldProperties
        'Table name buffer
        Dim FieldName As New StringBuilder("    ", 5)
        'Makes sense only when database is opened
        If OpenIndex <> -1 Then
            'Get array of tables
            TableProps.Name = Nothing
            If TDBTableGetProperties(OpenIndex, lstTables.SelectedIndices(0), TableProps) Then
                lstFields.BeginUpdate()
                lstFields.Items.Clear()
                For i = 0 To TableProps.FieldCount - 1
                    FieldProps.Name = FieldName.ToString
                    If TDBFieldGetProperties(OpenIndex, SelectedTable, i, FieldProps) Then
                        lstFields.Items.Add(FieldProps.Name)
                        lstFields.Items(i).SubItems.Add(CStr(FieldProps.Size))
                    End If
                Next
                lstFields.EndUpdate()
            End If
        End If
    End Sub

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents MainMenu As System.Windows.Forms.MainMenu
    Friend WithEvents mnuFile As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileOpen As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileSave As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileSep2 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileExit As System.Windows.Forms.MenuItem
    Friend WithEvents dlgOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lstTables As System.Windows.Forms.ListView
    Friend WithEvents hcolTableName As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstFields As System.Windows.Forms.ListView
    Friend WithEvents hcolFieldname As System.Windows.Forms.ColumnHeader
    Friend WithEvents hcolFieldSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents mnuFileClose As System.Windows.Forms.MenuItem
    Friend WithEvents Timer As System.Windows.Forms.Timer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.MainMenu = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuFile = New System.Windows.Forms.MenuItem()
        Me.mnuFileOpen = New System.Windows.Forms.MenuItem()
        Me.mnuFileSave = New System.Windows.Forms.MenuItem()
        Me.mnuFileClose = New System.Windows.Forms.MenuItem()
        Me.mnuFileSep2 = New System.Windows.Forms.MenuItem()
        Me.mnuFileExit = New System.Windows.Forms.MenuItem()
        Me.dlgOpen = New System.Windows.Forms.OpenFileDialog()
        Me.lstTables = New System.Windows.Forms.ListView()
        Me.hcolTableName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lstFields = New System.Windows.Forms.ListView()
        Me.hcolFieldname = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.hcolFieldSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'MainMenu
        '
        Me.MainMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFile})
        '
        'mnuFile
        '
        Me.mnuFile.Index = 0
        Me.mnuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFileOpen, Me.mnuFileSave, Me.mnuFileClose, Me.mnuFileSep2, Me.mnuFileExit})
        Me.mnuFile.Text = "&File"
        '
        'mnuFileOpen
        '
        Me.mnuFileOpen.Index = 0
        Me.mnuFileOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO
        Me.mnuFileOpen.Text = "&Open..."
        '
        'mnuFileSave
        '
        Me.mnuFileSave.Index = 1
        Me.mnuFileSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS
        Me.mnuFileSave.Text = "&Save"
        '
        'mnuFileClose
        '
        Me.mnuFileClose.Index = 2
        Me.mnuFileClose.Text = "&Close"
        '
        'mnuFileSep2
        '
        Me.mnuFileSep2.Index = 3
        Me.mnuFileSep2.Text = "-"
        '
        'mnuFileExit
        '
        Me.mnuFileExit.Index = 4
        Me.mnuFileExit.Text = "&Exit"
        '
        'dlgOpen
        '
        Me.dlgOpen.Filter = "All Files (*.*)|*.*"
        '
        'lstTables
        '
        Me.lstTables.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstTables.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.hcolTableName})
        Me.lstTables.FullRowSelect = True
        Me.lstTables.HideSelection = False
        Me.lstTables.Location = New System.Drawing.Point(0, 0)
        Me.lstTables.MultiSelect = False
        Me.lstTables.Name = "lstTables"
        Me.lstTables.Size = New System.Drawing.Size(144, 448)
        Me.lstTables.TabIndex = 0
        Me.lstTables.UseCompatibleStateImageBehavior = False
        Me.lstTables.View = System.Windows.Forms.View.Details
        '
        'hcolTableName
        '
        Me.hcolTableName.Text = "Table Name"
        Me.hcolTableName.Width = 140
        '
        'lstFields
        '
        Me.lstFields.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstFields.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.hcolFieldname, Me.hcolFieldSize})
        Me.lstFields.FullRowSelect = True
        Me.lstFields.HideSelection = False
        Me.lstFields.Location = New System.Drawing.Point(144, 0)
        Me.lstFields.MultiSelect = False
        Me.lstFields.Name = "lstFields"
        Me.lstFields.Size = New System.Drawing.Size(368, 448)
        Me.lstFields.TabIndex = 1
        Me.lstFields.UseCompatibleStateImageBehavior = False
        Me.lstFields.View = System.Windows.Forms.View.Details
        '
        'hcolFieldname
        '
        Me.hcolFieldname.Text = "Field Name"
        Me.hcolFieldname.Width = 85
        '
        'hcolFieldSize
        '
        Me.hcolFieldSize.Text = "Field Size"
        Me.hcolFieldSize.Width = 100
        '
        'Timer
        '
        Me.Timer.Enabled = True
        '
        'frmTDBAccess
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(512, 450)
        Me.Controls.Add(Me.lstFields)
        Me.Controls.Add(Me.lstTables)
        Me.Menu = Me.MainMenu
        Me.Name = "frmTDBAccess"
        Me.Text = "Test TDB Access"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub mnuFileExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileExit.Click
        mnuFileClose.PerformClick()
        End
    End Sub
    Private Sub mnuFileOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileOpen.Click
        If dlgOpen.ShowDialog = DialogResult.OK Then
            OpenIndex = TDBOpen(dlgOpen.FileName)
            If OpenIndex = -1 Then
                MessageBox.Show("Unable to open the database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Else
                LoadTables()
            End If
        End If
    End Sub

    Private Sub lstTables_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstTables.SelectedIndexChanged
        If (lstTables.SelectedItems.Count > 0) And (OpenIndex <> -1) Then
            SelectedTable = lstTables.SelectedItems.Item(0).Text
            LoadFields()
        Else
            SelectedTable = ""
            lstFields.BeginUpdate()
            lstFields.Items.Clear()
            lstFields.EndUpdate()
        End If
    End Sub

    Private Sub mnuFileClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileClose.Click
        If OpenIndex <> -1 Then
            Dim Rep As DialogResult
            If Modified Then
                'Confirm changes, although the program currently never modifies anything
                Rep = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If Rep = DialogResult.Yes Then
                    mnuFileSave.PerformClick()
                ElseIf Rep = DialogResult.Cancel Then
                    Exit Sub
                End If
            End If
            'Close database and release memory
            TDBClose(OpenIndex)
            'Clear all lists
            lstFields.BeginUpdate()
            lstFields.Items.Clear()
            lstFields.EndUpdate()
            lstTables.BeginUpdate()
            lstTables.Items.Clear()
            lstTables.EndUpdate()
            'Clear flags
            OpenIndex = -1
            Modified = False
        End If
    End Sub

    Private Sub mnuFileSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFileSave.Click
        If OpenIndex <> -1 Then
            TDBSave(OpenIndex)
            Modified = False
        End If
    End Sub

    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        mnuFileSave.Enabled = Modified
        mnuFileClose.Enabled = OpenIndex <> -1
    End Sub

End Class
