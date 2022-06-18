set WORKSPACE=..

set GEN_CLIENT=%WORKSPACE%\LubanTools\Luban.ClientServer\Luban.ClientServer.exe
set CONF_ROOT=%WORKSPACE%\DesignerConfigs


%GEN_CLIENT% -j cfg --^
 -d %CONF_ROOT%\Defines\__root__.xml ^
 --input_data_dir %CONF_ROOT%\Datas ^
 --output_code_dir ../Assets/GameMain/Scripts/Hotfix/LubanTables ^
 --output_data_dir ..\Assets\GameMain\LubanTables\bytes ^
 --gen_types code_cs_unity_bin,data_bin ^
 -s all 

pause