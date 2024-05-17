chcp 65001 > nul
mkdir "Прихована папка"
mkdir "Не прихована папка"
attrib +h "Прихована папка"
cd "Не прихована папка"
help xcopy > copyhelp.txt 
xcopy /c copyhelp.txt ..\"Прихована папка"\copied_copyhelp.txt