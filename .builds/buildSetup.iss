; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!
#define public Dependency_NoExampleSetup
#include "CodeDependencies.iss"

#define MyAppName "ImmoGlobal"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Nemicomp"
#define MyAppURL "https://www.nemicomp.ch"
#define MyAppExeName "ImmoGlobal.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{B7D5AFFE-759D-467A-BE78-78CA5B551428}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DisableDirPage=yes
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=..\OutputInstaller
OutputBaseFilename=ImmoGlobal Setup
Compression=lzma
SolidCompression=yes
WizardStyle=modern
SetupIconFile=..\ImmoGlobal\Resources\icon.ico

[Languages]
Name: "german"; MessagesFile: "compiler:Languages\German.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "..\Output\ImmoGlobal\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\Output\ImmoGlobal\ImmoGlobal.dll.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\Output\ImmoGlobal\Microsoft.Data.SqlClient.SNI.dll"; DestDir: "{app}"; Flags: ignoreversion
;dependencies, not installed
Source: "deps\netcorecheck.exe"; Flags: dontcopy noencryption
Source: "deps\netcorecheck_x64.exe"; Flags: dontcopy noencryption
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Code]
function InitializeSetup: Boolean;
begin
  Dependency_AddDotNet60Desktop;    
  Result := True;
end;