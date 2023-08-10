unit uTDBAccess;

interface

{ Types required to call TDBAccess functions }

type
  TtdbFieldType = (tdbString = 0, tdbBinary = 1, tdbSInt = 2, tdbUInt = 3,
    tdbFloat = 4, tdbVarchar = $D, tdbLongVarchar = $E, tdbInt = $2CE);

  TtdbTableProperties = packed record
    Name: PWideChar;
    FieldCount: Integer;
    Capacity: Integer;
    RecordCount: Integer;
    DeletedCount: Integer;
    NextDeletedRecord: Integer;
    Flag0: Boolean;
    Flag1: Boolean;
    Flag2: Boolean;
    Flag3: Boolean;
    NonAllocated: Boolean;
    HasVarchar : Boolean;
    HasCompressedVarchar: Boolean;
  end;
  TtdbFieldProperties = packed record
    Name: PWideChar;
    Size: Integer;
    FieldType: TtdbFieldType;
  end;

{ TDB Access functions }

function TDBOpen(const FileName: PWideChar): Integer; stdcall; external 'tdbaccess.dll';
function TDBClose(const DBIndex: Integer): Boolean; stdcall; external 'tdbaccess.dll';
function TDBSave(const DBIndex: Integer): Boolean; stdcall; external 'tdbaccess.dll';
function TDBDatabaseCompact(const DBIndex: Integer): Boolean; stdcall; external 'tdbaccess.dll';
function TDBDatabaseGetTableCount(const DBIndex: Integer): Integer; stdcall; external 'tdbaccess.dll';
function TDBTableGetProperties(const DBIndex, TableIndex: Integer;
  var TableProperties: TtdbTableProperties): Boolean; stdcall; external 'tdbaccess.dll';
function TDBTableRecordAdd(const DBIndex: Integer; const TableName: PWideChar;
  const AllowExpand: Boolean): Integer; stdcall; external 'tdbaccess.dll';
function TDBTableRecordChangeDeleted(const DBIndex: Integer; const TableName: PWideChar;
  const RecNo: Integer; const Deleted: Boolean): Boolean; stdcall; external 'tdbaccess.dll';
function TDBTableRecordDeleted(const DBIndex: Integer; const TableName: PWideChar;
  const RecNo: Integer): Boolean; stdcall; external 'tdbaccess.dll';
function TDBTableRecordRemove(const DBIndex: Integer; const TableName: PWideChar;
  const RecNo: Integer): Boolean; stdcall; external 'tdbaccess.dll';
function TDBQueryFindUnsignedInt(const DBIndex: Integer; const TableName, FieldName: PWideChar;
  const Value: Integer): Integer; stdcall; external 'tdbaccess.dll';
function TDBQueryGetResult(const Index: Integer): Integer; stdcall; external 'tdbaccess.dll';
function TDBQueryGetResultSize: Integer; stdcall; external 'tdbaccess.dll';
function TDBFieldGetProperties(const DBIndex: Integer; const TableName: PWideChar;
  const FieldIndex: Integer; var FieldProperties: TtdbFieldProperties): Boolean;
  stdcall; external 'tdbaccess.dll';
function TDBFieldGetValueAsBinary(const DBIndex: Integer; const TableName, FieldName: PWideChar;
  const RecNo: Integer; var OutBuffer: PWideChar): Boolean; stdcall; external 'tdbaccess.dll';
function TDBFieldGetValueAsFloat(const DBIndex: Integer; const TableName,
  FieldName: PWideChar; const RecNo: Integer): Single; stdcall; external 'tdbaccess.dll';
function TDBFieldGetValueAsInteger(const DBIndex: Integer; const TableName,
  FieldName: PWideChar; const RecNo: Integer): Integer; stdcall; external 'tdbaccess.dll';
function TDBFieldGetValueAsString(const DBIndex: Integer; const TableName, FieldName: PWideChar;
  const RecNo: Integer; var OutBuffer: PWideChar): Boolean; stdcall; external 'tdbaccess.dll';
function TDBFieldSetValueAsFloat(const DBIndex: Integer; const TableName,
  FieldName: PWideChar; const RecNo: Integer; const NewValue: Single): Boolean; stdcall; external 'tdbaccess.dll';
function TDBFieldSetValueAsInteger(const DBIndex: Integer; const TableName,
  FieldName: PWideChar; const RecNo: Integer; const NewValue: Integer): Boolean; stdcall; external 'tdbaccess.dll';
function TDBFieldSetValueAsString(const DBIndex: Integer; const TableName, FieldName: PWideChar;
  const RecNo: Integer; const NewValue: PWideChar): Boolean; stdcall; external 'tdbaccess.dll';
procedure TDBSetPFDToolPath(const PfdToolPath: PWideChar); stdcall; external 'tdbaccess.dll';
function TDBOpenPS3Save(const SaveDirectory: PWideChar): Integer; stdcall; external 'tdbaccess.dll';
function TDBOpenXbox360Save(const SaveFileName: PWideChar): Integer; stdcall; external 'tdbaccess.dll';

implementation

end.
