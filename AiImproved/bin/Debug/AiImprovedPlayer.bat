@ECHO OFF
echo %* > input.json
AiImproved.exe %*
exit /b %errorlevel%