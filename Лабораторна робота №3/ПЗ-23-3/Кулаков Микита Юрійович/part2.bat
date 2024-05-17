@echo off
chcp 65001
setlocal EnableDelayedExpansion

REM Перевірка наявності аргументів
if "%~1" == "" (
    echo Використання: %0 [шлях] [/h] [/r] [/a]
    exit /b 1
)

REM Перевірка існування вказаного шляху
if not exist "%~1\*" (
    echo Шлях "%~1" не існує.
    exit /b 2
)

REM Перехід до вказаного каталогу
cd /d "%~1"

REM Виведення інформації про каталог
echo Виконується обробка каталогу: %cd%

REM Отримання аргументів атрибутів
set /a hidden=0
set /a readOnly=0
set /a archive=0

if "%~2" == "/h" (set /a hidden=1)
if "%~3" == "/r" (set /a readOnly=1)
if "%~4" == "/a" (set /a archive=1)

REM Рекурсивний обхід каталогів і зміна атрибутів
for /r %%A in (*) do (
    if !hidden! == 1 (
        attrib +h "%%A" /s /d
        icacls "%%A" /grant Administrators:F
    )
    if !readOnly! == 1 (
        icacls "%%A" /grant SYSTEM:R
    )
    if !archive! == 1 (
        attrib +a "%%A" /s /d
    )

    echo %%A (
        if !hidden! == 1 (echo    Прихований)
        if !readOnly! == 1 (echo    Тільки для читання)
        if !archive! == 1 (echo    Архівний)
        echo )
)

REM Вихід з кодом завершення
if !errorlevel! == 0 (
    exit /b 0
) else (
    echo Помилка! Не вдалося змінити атрибути або дозволи деяких файлів.
    exit /b 4
)