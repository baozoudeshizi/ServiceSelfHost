; �ýű�ʹ�� HM VNISEdit �ű��༭���򵼲���

; ��װ�����ʼ���峣��

!define PluginName "SmartLab_Mobile"

!define PRODUCT_NAME "SmartLab-Mobile"
;!define PRODUCT_VERSION "2.2.8.12" ; -*- ����?��?3��?��?����1? -*-
;!define PACKAGE_PATH ".\Output" ; -*- ����?��?3��?��?����1? -*-
;!define PACKAGE_NAME "CDS2_Plugin_${PluginName}_${PRODUCT_VERSION}.exe"     ; -*- ����?��?3��?��?����1? -*-
!define PRODUCT_PUBLISHER "�������ǿƼ��ɷ����޹�˾"
!define PRODUCT_WEB_SITE "http://www.wayee.com"
!define PRODUCT_File ".\SourceFile\Mobile\"

!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_RIGHT
!define MUI_HEADERIMAGE_BITMAP_NOSTRETCH

SetCompressor lzma

; ------ MUI �ִ����涨�� (1.67 �汾���ϼ���) ------
!include "MUI.nsh"
!include "MUI2.nsh"
!include "LogicLib.nsh"
!include "x64.nsh"
!include "nsDialogs.nsh"
!include "FileFunc.nsh"

; MUI Ԥ���峣��
SetFont ΢���ź� 9

; ��������
Var InstallDir

/**/
; ��װ����ҳ��
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

; ��װ�����������������
!insertmacro MUI_LANGUAGE "SimpChinese"

; ��װԤ�ͷ��ļ�
!insertmacro MUI_RESERVEFILE_INSTALLOPTIONS
; ------ MUI �ִ����涨����� ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "${PACKAGE_PATH}\${PACKAGE_NAME}"
ShowInstDetails show
; �Ƿ�����װ�ڸ�Ŀ¼��
AllowRootDirInstall false

Section "MainSection" SEC01

  SetOutPath "$InstallDir\CDS2\"
  
  File /r "${PRODUCT_File}\"

SectionEnd

#-- ���� NSIS �ű��༭�������� Function ���α�������� Section ����֮���д���Ա��ⰲװ�������δ��Ԥ֪�����⡣--#

Section -Post
  ;����windows����

  IfFileExists $InstallDir\CDS2\SmartLabService.exe 0 +3
  nsExec::Exec '"sc.exe"/c create SmartLabService binPath= "$InstallDir\CDS2\SmartLabService.exe" start= auto '
  nsExec::Exec '"sc.exe"/c start SmartLabService'
  LogEx::Write "    SmartLabService"
SectionEnd

Function .onInit
;���������ֹ�ظ�����
  InitPluginsDir
  System::Call 'kernel32::CreateMutexA(i 0, i 0, t "WinSnap_installer") i .r1 ?e'
  Pop $R0
  StrCmp $R0 0 +3
  MessageBox MB_OK|MB_TOPMOST|MB_ICONEXCLAMATION "��ֹ��������װ����ͬʱ����!"
  Abort

	${If} ${RunningX64}
  	SetRegView 64
	${Else}
  	SetRegView lastused
	${EndIf}

	ReadRegStr $InstallDir HKLM "Software\SmartLab\CDS2\ChromProg" "InstallDir"
  ${If} $InstallDir == ""
  	MessageBox MB_ICONINFORMATION|MB_OK "�밲װSmartLabCDS2.0���ٽ���SmartLab Mobile��װ��"
  	Abort
  ${EndIf}
	# debug
	;MessageBox MB_ICONINFORMATION|MB_OK $InstallDir
	
# install log
	LogEx::Init "$LOCALAPPDATA\log.txt"
  ${GetTime} "" "L" $0 $1 $2 $3 $4 $5 $6
	LogEx::Write "$2/$1/$0/$4:$5:$6 Log Opened."
	LogEx::Write "    ${PACKAGE_NAME} will install..."

FunctionEnd

Function .onInstSuccess

;nsExec::Exec '$InstallDir\CDS2\SmartLabServiceSelfHost.exe'
 ;ExecShell "" "$InstallDir\CDS2\SmartLabServiceSelfHost.exe"
  ;LogEx::Write "    ִ��SmartLabServiceSelfHost.exe"
  
  LogEx::Write "    Install Success!"
  ${GetTime} "" "L" $0 $1 $2 $3 $4 $5 $6
	LogEx::Write "$2/$1/$0/$4:$5:$6 Log Closed."
  LogEx::Write ""
	LogEx::Write ""
  LogEx::Write ""
	LogEx::Close
FunctionEnd

Function .onInstFailed
  LogEx::Write "    Install Failed!"
  ${GetTime} "" "L" $0 $1 $2 $3 $4 $5 $6
	LogEx::Write "$2/$1/$0/$4:$5:$6 Log Closed."
  LogEx::Write ""
	LogEx::Write ""
  LogEx::Write ""
	LogEx::Close
FunctionEnd
