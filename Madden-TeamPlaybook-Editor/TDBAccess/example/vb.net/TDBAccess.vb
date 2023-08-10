Imports System.Runtime.InteropServices

Public Enum TdbFieldType
    tdbString = 0
    tdbBinary = 1
    tdbSInt = 2
    tdbUInt = 3
    tdbFloat = 4
    tdbVarchar = &HD
    tdbLongVarchar = &HE
    tdbInt = &H2CE
End Enum

<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
Public Structure TdbFieldProperties
    Public Name As String
    Public Size As Integer
    Public FieldType As TdbFieldType
End Structure

<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
Public Structure TdbTableProperties
    Public Name As String
    Public FieldCount As Integer
    Public Capacity As Integer
    Public RecordCount As Integer
    Public DeletedCount As Integer
    Public NextDeletedRecord As Integer
    Public Flag0 As Boolean
    Public Flag1 As Boolean
    Public Flag2 As Boolean
    Public Flag3 As Boolean
    Public NonAllocated As Boolean
    Public HasVarchar As Boolean
    Public HasCompressedVarchar As Boolean
End Structure

Public Class TDBAccess
    <DllImport("tdbaccess.dll", CharSet:=CharSet.Unicode)>
    Public Shared Function TDBOpen(ByVal FileName As String) As Integer
    End Function
    <DllImport("tdbaccess.dll")>
    Public Shared Function TDBDatabaseGetTableCount(ByVal DBIndex As Integer) As Integer
    End Function
    <DllImport("tdbaccess.dll")>
    Public Shared Function TDBTableGetProperties(ByVal DBIndex As Integer, ByVal TableIndex As Integer, ByRef TableProperties As TdbTableProperties) As Boolean
    End Function
    <DllImport("tdbaccess.dll", CharSet:=CharSet.Unicode)>
    Public Shared Function TDBFieldGetProperties(ByVal DBIndex As Integer, ByVal TableName As String, ByVal FieldIndex As Integer, ByRef FieldProperties As TdbFieldProperties) As Boolean
    End Function
    <DllImport("tdbaccess.dll")>
    Public Shared Function TDBSave(ByVal DBIndex As Integer) As Boolean
    End Function
    <DllImport("tdbaccess.dll")>
    Public Shared Function TDBClose(ByVal DBIndex As Integer) As Boolean
    End Function
    <DllImport("tdbaccess.dll", CharSet:=CharSet.Unicode)>
    Public Shared Function TDBFieldGetValueAsFloat(ByVal DBIndex As Integer, ByVal TableName As String, ByVal FieldName As String, ByVal RecNo As Integer) As Single
    End Function
End Class
