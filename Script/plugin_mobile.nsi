; 该脚本使用 HM VNISEdit 脚本编辑器向导产生

; 安装程序初始定义常量

!define PluginName "SmartLab_Mobile"

!define PRODUCT_NAME "SmartLab-Mobile"
;!define PRODUCT_VERSION "2.2.8.12" ; -*- óé?·?3±?á?ìá1? -*-
;!define PACKAGE_PATH ".\Output" ; -*- óé?·?3±?á?ìá1? -*-
;!define PACKAGE_NAME "CDS2_Plugin_${PluginName}_${PRODUCT_VERSION}.exe"     ; -*- óé?·?3±?á?ìá1? -*-
!define PRODUCT_PUBLISHER "安徽皖仪科技股份有限公司"
!define PRODUCT_WEB_SITE "http://www.wayee.com"
!define PRODUCT_File ".\SourceFile\Mobile\"

!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_RIGHT
!define MUI_HEADERIMAGE_BITMAP_NOSTRETCH

SetCompressor lzma

; ------ MUI 现代界面定义 (1.67 版本以上兼容) ------
!include "MUI.nsh"
!include "MUI2.nsh"
!include "LogicLib.nsh"
!include "x64.nsh"
!include "nsDialogs.nsh"
!include "FileFunc.nsh"

; MUI 预定义常量
SetFont 微软雅黑 9

; 声明变量
Var InstallDir

/**/
; 安装过程页面
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

; 安装界面包含的语言设置
!insertmacro MUI_LANGUAGE "SimpChinese"

; 安装预释放文件
!insertmacro MUI_RESERVEFILE_INSTALLOPTIONS
; ------ MUI 现代界面定义结束 ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "${PACKAGE_PATH}\${PACKAGE_NAME}"
ShowInstDetails show
; 是否允许安装在根目录下
AllowRootDirInstall false

Section "MainSection" SEC01

  SetOutPath "$InstallDir\CDS2\"
  
  File /r "${PRODUCT_File}\"

SectionEnd

#-- 根据 NSIS 脚本编辑规则，所有 Function 区段必须放置在 Section 区段之后编写，以避免安装程序出现未可预知的问题。--#

Section -Post
  ;设置windows服务

  IfFileExists $InstallDir\CDS2\SmartLabService.exe 0 +3
  nsExec::Exec '"sc.exe"/c create SmartLabService binPath= "$InstallDir\CDS2\SmartLabService.exe" start= auto '
  nsExec::Exec '"sc.exe"/c start SmartLabService'
  LogEx::Write "    SmartLabService"
SectionEnd

Function .onInit
;创建互斥防止重复运行
  InitPluginsDir
  System::Call 'kernel32::CreateMutexA(i 0, i 0, t "WinSnap_installer") i .r1 ?e'
  Pop $R0
  StrCmp $R0 0 +3
  MessageBox MB_OK|MB_TOPMOST|MB_ICONEXCLAMATION "禁止多个插件安装程序同时运行!"
  Abort

	${If} ${RunningX64}
  	SetRegView 64
	${Else}
  	SetRegView lastused
	${EndIf}

	ReadRegStr $InstallDir HKLM "Software\SmartLab\CDS2\ChromProg" "InstallDir"
  ${If} $InstallDir == ""
  	MessageBox MB_ICONINFORMATION|MB_OK "请安装SmartLabCDS2.0后，再进行SmartLab Mobile安装！"
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
  ;LogEx::Write "    执行SmartLabServiceSelfHost.exe"
  
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
