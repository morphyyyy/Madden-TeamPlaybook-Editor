program Example;

uses
  Vcl.Forms,
  uMainForm in 'uMainForm.pas' {frmMain},
  uTDBAccess in 'uTDBAccess.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.Title := 'TDBAccess Example';
  Application.CreateForm(TfrmMain, frmMain);
  Application.Run;
end.
