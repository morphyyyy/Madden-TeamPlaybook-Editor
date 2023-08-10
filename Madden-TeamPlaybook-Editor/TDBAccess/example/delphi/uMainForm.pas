unit uMainForm;

interface

uses
  Winapi.Windows, Winapi.Messages, System.SysUtils, System.Variants, System.Classes, Vcl.Graphics,
  Vcl.Controls, Vcl.Forms, Vcl.Dialogs, Vcl.Menus, Vcl.StdActns, System.Actions,
  Vcl.ActnList, Vcl.ImgList, Vcl.ComCtrls, Vcl.Grids, Vcl.ExtCtrls, Vcl.ToolWin;

type
  // Custom draw grid component allows to make changes to the way certain cells are rendered.
  // In this example, DrawCell is overridden to draw fixed cells in bold
  // and TDB deleted rows in gray
  TCustomDrawStringGrid = class(TStringGrid)
  private
    procedure WMCommand(var Message: TWMCommand); message WM_COMMAND;
  protected
    procedure DrawCell(ACol, ARow: Longint; ARect: TRect;
      AState: TGridDrawState); override;
  public
    constructor Create(AOwner: TComponent); override;
  end;

  // Main form class
  TfrmMain = class(TForm)
    MainMenu: TMainMenu;
    ActionList: TActionList;
    ImageList: TImageList;
    actSave: TAction;
    actFileOpen: TFileOpen;
    actFileExit: TFileExit;
    actOpenPS3: TBrowseForFolder;
    itmFile: TMenuItem;
    itmFileOpen: TMenuItem;
    itmOpenPS3: TMenuItem;
    itmSave: TMenuItem;
    itmExit: TMenuItem;
    itmFileSep1: TMenuItem;
    actClose: TAction;
    itmClose: TMenuItem;
    lstTables: TListView;
    pnlClient: TPanel;
    Splitter: TSplitter;
    PopupMenu: TPopupMenu;
    itmGridRowDeleteRestore: TMenuItem;
    CoolBar: TCoolBar;
    ToolBar: TToolBar;
    btnOpen: TToolButton;
    btnSave: TToolButton;
    btnSep1: TToolButton;
    btnExit: TToolButton;
    procedure FormCreate(Sender: TObject);
    procedure FormCloseQuery(Sender: TObject; var CanClose: Boolean);
    procedure actCloseExecute(Sender: TObject);
    procedure actSaveExecute(Sender: TObject);
    procedure actFileOpenAccept(Sender: TObject);
    procedure actOpenPS3Accept(Sender: TObject);
    procedure lstTablesSelectItem(Sender: TObject; Item: TListItem;
      Selected: Boolean);
    procedure itmGridRowDeleteRestoreClick(Sender: TObject);
    procedure PopupMenuPopup(Sender: TObject);
    procedure gridTableMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure gridTableSetEditText(Sender: TObject; ACol, ARow: Integer;
      const Value: string);
    procedure gridTableSelectCell(Sender: TObject; ACol, ARow: Integer;
      var CanSelect: Boolean);
  private
    { Private declarations }
    gridTable: TCustomDrawStringGrid;
    FModified: Boolean;
    FIndex: Integer;
    function CheckClose: Boolean;
    procedure ReadTables;
    procedure CheckEnabled;
    procedure UpdateTableListItem(const LI: TListItem);
  public
    { Public declarations }
  end;

var
  frmMain: TfrmMain;

implementation

uses
  uTDBAccess, IOUtils, UITypes, Math, Themes;

{$R *.dfm}

procedure TfrmMain.actCloseExecute(Sender: TObject);
begin
  CheckClose;
end;

procedure TfrmMain.actFileOpenAccept(Sender: TObject);
begin
  // Open the file specified in the dialog
  FIndex := TDBOpen(PChar(actFileOpen.Dialog.FileName));
  if FIndex = -1 then
    MessageDlg('Open failed', mtError, [mbOK], -1)
  else
  begin
    // Show tables in the list
    ReadTables;
  end;

  // Update enabled actions
  CheckEnabled;
end;

procedure TfrmMain.actOpenPS3Accept(Sender: TObject);
begin
  FIndex := TDBOpenPS3Save(PChar(actOpenPS3.Folder));
  if FIndex = -1 then
    MessageDlg('Open failed', mtError, [mbOK], -1)
  else
  begin
    // Show tables in the list
    ReadTables;
  end;

  // Update enabled actions
  CheckEnabled;
end;

procedure TfrmMain.actSaveExecute(Sender: TObject);
begin
  // Save the currently opened db
  if TDBSave(FIndex) then
    FModified := False
  else
    MessageDlg('Save failed', mtError, [mbOK], -1);

  // Update enabled actions
  CheckEnabled;
end;

function TfrmMain.CheckClose: Boolean;
var
  Resp: Integer;
  i, j: Integer;
begin
  Result := True;

  if FIndex <> - 1 then
  begin
    // If modified, prompt to save changes
    if FModified then
    begin
      Resp := MessageDlg('Do you want to save your changes?', mtConfirmation, [mbYes, mbNo, mbCancel], -1);
      Result := Resp <> mrCancel;
      if Resp = mrYes then
      begin
        actSave.Execute;
      end;
    end;

    // If close not cancelled, do close
    if Result then
    begin
      if TDBClose(FIndex) then
      begin
        FIndex := -1;
        FModified := False;

        // Clear the grid
        gridTable.RowCount := 2;
        gridTable.ColCount := 2;
        for i := 0 to gridTable.RowCount - 1 do
          for j := 0 to gridTable.ColCount - 1 do
            gridTable.Cells[j, i] := '';

        // Clear tables list
        lstTables.Items.BeginUpdate;
        try
          lstTables.Items.Clear;
        finally
          lstTables.Items.EndUpdate;
        end;
      end;
    end;
  end;

  // Update enabled actions
  CheckEnabled;
end;

procedure TfrmMain.CheckEnabled;
begin
  if FIndex = -1 then
  begin
    // No db opened
    actFileOpen.Enabled := True;
    actOpenPS3.Enabled := True;
    actClose.Enabled := False;
    actSave.Enabled := False;
  end
  else
  begin
    // DB opened
    actFileOpen.Enabled := False;
    actOpenPS3.Enabled := False;
    actClose.Enabled := True;
    actSave.Enabled := FModified;
  end;
end;

procedure TfrmMain.FormCloseQuery(Sender: TObject; var CanClose: Boolean);
begin
  // Before closing the app, close the database
  CanClose := CheckClose;
end;

procedure TfrmMain.FormCreate(Sender: TObject);
begin
  FIndex := -1;

  // Set the path to pfdtools.exe same as application executable
  TDBSetPFDToolPath(PChar(TPath.GetDirectoryName(Application.ExeName)));

  // Enable/disable actions
  CheckEnabled;

  // Grid is created at run-time not design-time
  gridTable := TCustomDrawStringGrid.Create(Self);
  gridTable.Parent := pnlClient;
  gridTable.Align := alClient;
  gridTable.RowCount := 2;
  gridTable.ColCount := 2;
  gridTable.FixedRows := 1;
  gridTable.FixedCols := 1;
  gridTable.PopupMenu := PopupMenu;
  gridTable.OnMouseDown := gridTableMouseDown;
  gridTable.Options := gridTable.Options + [goColSizing, goRowSizing, goEditing, goDrawFocusSelected, goFixedColClick];
  gridTable.OnSetEditText := gridTableSetEditText;
  gridTable.OnSelectCell := gridTableSelectCell;
end;

procedure TfrmMain.itmGridRowDeleteRestoreClick(Sender: TObject);
var
  RecNo: Integer;
  Code: Integer;
  NewDeleted: Boolean;
begin
  if gridTable.Row > 0 then
  begin
    Val(gridTable.Cells[0, gridTable.Row], RecNo, Code);
    if Code <> 1 then
    begin
      if (Sender as TMenuItem).Tag = 2 then
        NewDeleted := False
      else
        NewDeleted := True;

      // Change the status of deleted for the selected row based on the tag of the menu item
      if TDBTableRecordChangeDeleted(FIndex, PChar(lstTables.Selected.Caption), RecNo, NewDeleted) then
      begin
        FModified := True;
        if NewDeleted then
          gridTable.Cells[0, gridTable.Row] := gridTable.Cells[0, gridTable.Row] + '*'
        else
          gridTable.Cells[0, gridTable.Row] := StringReplace(gridTable.Cells[0, gridTable.Row], '*', '', [rfReplaceAll]);
        UpdateTableListItem(lstTables.Selected);
        gridTable.Invalidate;
        CheckEnabled;
      end;
    end;
  end;
end;

procedure TfrmMain.lstTablesSelectItem(Sender: TObject; Item: TListItem;
  Selected: Boolean);
var
  i, j: Integer;
  TableIdx: Integer;
  TableName: string;
  TableProps: TtdbTableProperties;
  FieldProps: TtdbFieldProperties;
  FieldName: array[0..4] of Char;
  FieldStringValue: array of Char;
  FieldStringValuePtr: PChar;
begin
  if Selected then
  begin
    // Retrieve the table index, stored in Data property of the item
    TableIdx := Integer(Item.Data);
    TableName := Item.Caption;
    // Get table properties; Name is not allocated, so it won't be filled
    TableProps.Name := nil;
    if TDBTableGetProperties(FIndex, TableIdx, TableProps) then
    begin
      // Fill the table
      gridTable.RowCount := gridTable.FixedRows + Max(TableProps.RecordCount, 1);
      gridTable.ColCount := gridTable.FixedCols + TableProps.FieldCount;
      for i := 0 to gridTable.RowCount - 1 do
      begin
        // First col contains row index
        if i <> 0 then
          gridTable.Cells[0, i] := IntToStr(i - 1)
        else
          gridTable.Cells[0, i] := 'Rec No.';

        for j := 1 to gridTable.ColCount - 1 do
        begin
          // Allocate Name for the field by pointing it to the local var.
          // Field names require 4 chars + null terminator
          FieldProps.Name := FieldName;
          if TDBFieldGetProperties(FIndex, PChar(TableName), j - 1, FieldProps) then
          begin
            if i = 0 then
            begin
              // First row contains field names
              gridTable.Cells[j, i] := string(FieldProps.Name)
            end
            else if i <= TableProps.RecordCount then
            begin
              // Check to see if this record has deleted bit set;
              // if so indicate it in the index column.
              // Normally, you would ignore such a record entirely in an application
              if TDBTableRecordDeleted(FIndex, PChar(TableName), i - 1) then
              begin
                gridTable.Cells[0, i] := IntToStr(i - 1) + '*';
              end;

              // Otherwise, display the value based on field type
              case FieldProps.FieldType of
                tdbString, tdbVarchar, tdbLongVarchar:
                begin
                  // Allocate memory for a string value equal to the size
                  // of the field + 1 for a null terminator. Size is in bits
                  // so divide by 8
                  SetLength(FieldStringValue, (FieldProps.Size div 8) + 1);
                  try
                    // Point the buffer to the first element of the dynamic array.
                    // Alternatively, one could simply use StrNew/StrDispose
                    // directly on FieldStringValuePtr
                    FieldStringValuePtr := @FieldStringValue[0];

                    if TDBFieldGetValueAsString(FIndex, PChar(TableName), FieldProps.Name, i - 1, FieldStringValuePtr) then
                      gridTable.Cells[j, i] := string(FieldStringValuePtr);
                  finally
                    Finalize(FieldStringValue);
                  end;
                end;
                tdbBinary:
                begin
                  // Skip binary values, they do not work well in grids
                end;
                tdbSInt,
                tdbUInt,
                tdbInt:
                begin
                  gridTable.Cells[j, i] := IntToStr(TDBFieldGetValueAsInteger(FIndex, PChar(TableName), FieldProps.Name, i - 1));
                end;
                tdbFloat:
                begin
                  gridTable.Cells[j, i] := FloatToStr(TDBFieldGetValueAsFloat(FIndex, PChar(TableName), FieldProps.Name, i - 1));
                end;
              end;
            end
            else
            begin
              // Clear cell value when there are no records in a table
              gridTable.Cells[j, i] := '';
            end;
          end;
        end;
      end;
    end;
  end;
end;

procedure TfrmMain.gridTableMouseDown(Sender: TObject; Button: TMouseButton;
  Shift: TShiftState; X, Y: Integer);
var
  ARow, ACol: Integer;
begin
  if Button = mbRight then
  begin
    // Select the cell at clicked coordinates when right-clicking
    // (default behaviour does nothing)
    (Sender as TStringGrid).MouseToCell(X, Y, ACol, ARow);
    if ARow > 0 then
      TStringGrid(Sender).Row := ARow;
    if ACol > 0 then
      TStringGrid(Sender).Col := ACol;
  end;
end;

procedure TfrmMain.PopupMenuPopup(Sender: TObject);
var
  RecNo: Integer;
  Code: Integer;
begin
  if gridTable.Row > 0 then
  begin
    Val(gridTable.Cells[0, gridTable.Row], RecNo, Code);
    if Code <> 1 then
    begin
      // Change the caption of the popup menu item based on the current field status
      // Set the tag to know which action to execute on click
      if TDBTableRecordDeleted(FIndex, PChar(lstTables.Selected.Caption), RecNo) then
      begin
        itmGridRowDeleteRestore.Caption := 'Restore';
        itmGridRowDeleteRestore.Tag := 2;
      end
      else
      begin
        itmGridRowDeleteRestore.Caption := 'Delete';
        itmGridRowDeleteRestore.Tag := 1;
      end;
    end;
  end;
end;

procedure TfrmMain.ReadTables;
var
  i: Integer;
  TableCount: Integer;
  LI: TListItem;
begin
  // Get total number of tables
  TableCount := TDBDatabaseGetTableCount(FIndex);

  lstTables.Items.BeginUpdate;
  try
    for i := 0 to TableCount - 1 do
    begin
      // Add table to the list
      LI := lstTables.Items.Add;
      LI.Data := Pointer(i);
      UpdateTableListItem(LI);
    end;
  finally
    lstTables.Items.EndUpdate;
  end;
end;

procedure TfrmMain.gridTableSelectCell(Sender: TObject; ACol, ARow: Integer;
  var CanSelect: Boolean);
begin
  CanSelect := True;
end;

procedure TfrmMain.gridTableSetEditText(Sender: TObject; ACol, ARow: Integer;
  const Value: string);
var
  RecNo: Integer;
  Code: Integer;
  FieldProps: TtdbFieldProperties;
  FieldName: array[0..4] of Char;
  IntValue: Integer;
  StrValue: string;
  FloatValue: Single;
begin
  // Get the current RecNo
  Val(gridTable.Cells[0, ARow], RecNo, Code);
  if (Code <> 1) then
  begin
    FieldProps.Name := FieldName;
    if TDBFieldGetProperties(FIndex, PChar(lstTables.Selected.Caption), ACol - 1, FieldProps) then
    begin
      case FieldProps.FieldType of
        tdbString, tdbVarchar, tdbLongVarchar:
        begin
          // Truncate the written value up to the maximum field size
          StrValue := Copy(Value, 1, FieldProps.Size div 8);
          if TDBFieldSetValueAsString(FIndex, PChar(lstTables.Selected.Caption), FieldProps.Name, RecNo, PChar(StrValue)) then
          begin
            FModified := True;
            CheckEnabled;
          end;

          // Update the grid with a possibly modified value
          gridTable.Cells[ACol, ARow] := StrValue;
        end;
        tdbBinary:
        begin
          // Skip binary values
        end;
        tdbSInt,
        tdbUInt,
        tdbInt:
        begin
          try
            // We "and" the input value to a maximum allowed number of bits in the field
            // Rest of the bits are discarded
            IntValue := StrToInt(Value) and ((1 shl FieldProps.Size) - 1);

            if TDBFieldSetValueAsInteger(FIndex, PChar(lstTables.Selected.Caption), FieldProps.Name, RecNo, IntValue) then
            begin
              FModified := True;
              CheckEnabled;
            end;

            // Update the grid with a possibly modified value
            gridTable.Cells[ACol, ARow] := IntToStr(IntValue);
          except
            on E: EConvertError do
            begin
              // On conversion error, restore the current value
              gridTable.Cells[ACol, ARow] := IntToStr(TDBFieldGetValueAsInteger(FIndex, PChar(lstTables.Selected.Caption), FieldProps.Name, RecNo));
              raise;
            end;
          end;
        end;
        tdbFloat:
        begin
          try
            if TDBFieldSetValueAsFloat(FIndex, PChar(lstTables.Selected.Caption), FieldProps.Name, RecNo, StrToFloat(Value)) then
            begin
              FModified := True;
              CheckEnabled;
            end;

            // Update the grid with a possibly modified value
            FloatValue := TDBFieldGetValueAsFloat(FIndex, PChar(lstTables.Selected.Caption), FieldProps.Name, RecNo);
            gridTable.Cells[ACol, ARow] := FloatToStr(FloatValue);
          except
            on E: EConvertError do
            begin
              // On conversion error, restore the current value
              gridTable.Cells[ACol, ARow] := FloatToStr(TDBFieldGetValueAsFloat(FIndex, PChar(lstTables.Selected.Caption), FieldProps.Name, RecNo));
              raise;
            end;
          end;
        end;
      end;
    end;
  end;
end;

procedure TfrmMain.UpdateTableListItem(const LI: TListItem);
var
  TableProps: TtdbTableProperties;
  TableNameBuffer: array[0..4] of Char;
  TableIdx: Integer;
  StatusText: string;
begin
  // Set the address of the name buffer into TableProps; otherwise Name is
  // not allocated and won't be filled. All table names are 4 characters
  // long + null terminator
  TableProps.Name := TableNameBuffer;

  TableIdx := Integer(LI.Data);
  if TDBTableGetProperties(FIndex, TableIdx, TableProps) then
  begin
    LI.Caption := string(TableProps.Name);
    StatusText := Format('%d / %d records (%d deleted)', [TableProps.RecordCount, TableProps.Capacity, TableProps.DeletedCount]);
    if LI.SubItems.Count = 0 then
      LI.SubItems.Add(StatusText)
    else
      LI.SubItems[0] := StatusText;
  end
  else
    MessageDlg(Format('Unable to read the properties of table %d', [TableIdx]), mtError, [mbOK], -1);
end;

{ TCustomDrawStringGrid }

constructor TCustomDrawStringGrid.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
end;

procedure TCustomDrawStringGrid.DrawCell(ACol, ARow: Integer; ARect: TRect;
  AState: TGridDrawState);
begin
  // Draw deleted cells in gray
  if Pos('*', Cells[0, ARow]) <> 0 then
    Canvas.Font.Color := clGrayText
  else
    Canvas.Font.Color := clWindowText;

  // Draw fixed cells in bold
  if (ACol = 0) or (ARow = 0) then
    Canvas.Font.Style := Canvas.Font.Style + [fsBold]
  else
    Canvas.Font.Style := Canvas.Font.Style - [fsBold];

  inherited DrawCell(ACol, ARow, ARect, AState);
end;

procedure TCustomDrawStringGrid.WMCommand(var Message: TWMCommand);
begin
  // Default implementation of the grid handles EN_CHANGE command which causes
  // SetEditText to be called while value is not final. Our custom grid
  // overrides the default handler in order to do nothing with EN_CHANGE message
  // This way, SetEditText is called only when user is completely done editing
  // cell value
end;

end.
