<html xmlns:v="urn:schemas-microsoft-com:vml"
xmlns:o="urn:schemas-microsoft-com:office:office"
xmlns:w="urn:schemas-microsoft-com:office:word"
xmlns:m="http://schemas.microsoft.com/office/2004/12/omml"
xmlns="http://www.w3.org/TR/REC-html40">

<head>
<meta http-equiv=Content-Type content="text/html; charset=windows-1252">
<meta name=ProgId content=Word.Document>
<meta name=Generator content="Microsoft Word 15">
<meta name=Originator content="Microsoft Word 15">
<link rel=File-List
href="BotW%20Basic%20Mod%20Creator%20Docs_files/filelist.xml">
<link rel=Edit-Time-Data
href="BotW%20Basic%20Mod%20Creator%20Docs_files/editdata.mso"

<link rel=themeData
href="BotW%20Basic%20Mod%20Creator%20Docs_files/themedata.thmx">
<link rel=colorSchemeMapping
href="BotW%20Basic%20Mod%20Creator%20Docs_files/colorschememapping.xml">

</head>

<body lang=EN-CA link="#0563C1" vlink="#954F72" style='tab-interval:.5in;
word-wrap:break-word'>

<div class=WordSection1>

<h1><b><span style='font-size:20.0pt;line-height:107%;font-family:"Calibri",sans-serif;
mso-ascii-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;mso-bidi-theme-font:
minor-latin'>Breath of the Wild: Basic Mod Creator<o:p></o:p></span></b></h1>

<p class=MsoNormal><o:p>&nbsp;</o:p></p>

<h1>Contents:</h1>

<h2 style='margin-left:.5in;text-indent:-.25in;mso-list:l2 level1 lfo5'><![if !supportLists]><span
style='font-family:Symbol;mso-fareast-font-family:Symbol;mso-bidi-font-family:
Symbol'><span style='mso-list:Ignore'>-<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><![endif]>Overview</h2>

<h2 style='margin-left:.5in;text-indent:-.25in;mso-list:l2 level1 lfo5'><![if !supportLists]><span
style='font-family:Symbol;mso-fareast-font-family:Symbol;mso-bidi-font-family:
Symbol'><span style='mso-list:Ignore'>-<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><![endif]>Syntax and Commands</h2>

<h2 style='margin-left:.5in;text-indent:-.25in;mso-list:l2 level1 lfo5'><![if !supportLists]><span
style='font-family:Symbol;mso-fareast-font-family:Symbol;mso-bidi-font-family:
Symbol'><span style='mso-list:Ignore'>-<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><![endif]>Credits</h2>

<p class=MsoNormal><o:p>&nbsp;</o:p></p>

<h1>Overview</h1>

<p class=MsoNormal><o:p>&nbsp;</o:p></p>

<p class=MsoNormal><span style='font-size:12.0pt;line-height:107%;color:black;
mso-themecolor:text1'>The main feature of this tool is to allow the user to
create custom preset files (BFT) that when opened with the main executable will
create directories and copy files from the game defined by the preset file.<o:p></o:p></span></p>

<p class=MsoNormal><span style='font-size:12.0pt;line-height:107%;color:black;
mso-themecolor:text1'>Other features include actor creating (limited) and a
faster way make collision.<o:p></o:p></span></p>

<h3>Requires Hyrule-Builder, Python 3.7 (x64), Visual C++ Redistributable to
run ‘readdata’ [<a href="#_Syntax_and_Commands"><b>-r</b></a>] </h3>

<h1><a name="_Syntax_and_Commands"></a>Syntax and Commands</h1>

<p class=MsoNormal><o:p>&nbsp;</o:p></p>

<p class=MsoNormal>‘help’ [-h] Opens the help window in CMD.</p>

<p class=MsoNormal>‘paths’ [-p] Sets your game paths.<span
style='mso-spacerun:yes'>  </span>Example Follows:</p>

<p class=MsoListParagraph style='margin-left:38.7pt;mso-add-space:auto;
text-indent:-.25in;mso-list:l9 level1 lfo7'><![if !supportLists]><span
style='font-family:Symbol;mso-fareast-font-family:Symbol;mso-bidi-font-family:
Symbol'><span style='mso-list:Ignore'>-<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><![endif]>BotWFileGatherTool.exe -p “C:\Breath of the
Wild\The Legend of Zelda: Breath of the Wild [0005000101c9500]\content”
“C:\Breath of the Wild\Update v208 [0005000e101c9500]\content” “C:\Breath of
the Wild\DLC [0005000c101c9500]\content”</p>

<p class=MsoNormal>‘bft’ [-b] Creates a preset file (<b>.BFT</b> - <b>B</b>otW<b>F</b>older<b>T</b>ool)
Options as Follows:</p>

<p class=MsoListParagraphCxSpFirst style='text-indent:-.25in;mso-list:l8 level1 lfo10'><![if !supportLists]><span
style='font-family:"Courier New";mso-fareast-font-family:"Courier New"'><span
style='mso-list:Ignore'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;
</span></span></span><![endif]>Options </p>

<p class=MsoListParagraphCxSpMiddle style='text-indent:-.25in;mso-list:l8 level1 lfo10'><![if !supportLists]><span
style='font-family:"Courier New";mso-fareast-font-family:"Courier New"'><span
style='mso-list:Ignore'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;
</span></span></span><![endif]><span style='mso-bidi-font-family:Calibri;
mso-bidi-theme-font:minor-latin'>-m Define BotW Material(s). Example: <span
style='color:black;mso-color-alt:windowtext;background:#D9D9D9;mso-shading:
white;mso-pattern:gray-15 auto'>-m “Glass, Water, Emissions”</span><o:p></o:p></span></p>

<p class=MsoListParagraphCxSpMiddle style='text-indent:-.25in;mso-list:l8 level1 lfo10'><![if !supportLists]><span
style='font-family:"Courier New";mso-fareast-font-family:"Courier New"'><span
style='mso-list:Ignore'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;
</span></span></span><![endif]><span style='mso-bidi-font-family:Calibri;
mso-bidi-theme-font:minor-latin'>-t Add BotW files to the preset file. Example:
<span style='color:black;mso-color-alt:windowtext;background:#D9D9D9;
mso-shading:white;mso-pattern:gray-15 auto'>-t “Update\Model\AnimalBear_A.sbfres
: Game\Movie\Demo101_0\mp4 : DLC\0012\Voice\EUes\Stream_Demo007_0\Demo007_0_Text000.bfstm”
</span><o:p></o:p></span></p>

<p class=MsoListParagraphCxSpMiddle style='text-indent:-.25in;mso-list:l8 level1 lfo10'><![if !supportLists]><span
style='font-family:"Courier New";mso-fareast-font-family:"Courier New"'><span
style='mso-list:Ignore'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;
</span></span></span><![endif]><span style='mso-bidi-font-family:Calibri;
mso-bidi-theme-font:minor-latin'>-fm Reads all directories in a folder, folder
syntax </span><a href="#_Folder_Syntax:"><span style='mso-bidi-font-family:
Calibri;mso-bidi-theme-font:minor-latin'>here</span></a><span style='mso-bidi-font-family:
Calibri;mso-bidi-theme-font:minor-latin'>. Example: <span style='color:black;
mso-color-alt:windowtext;background:#D9D9D9;mso-shading:white;mso-pattern:gray-15 auto'>-fm
“C:\Breath of the Wild\Mods”</span><o:p></o:p></span></p>

<p class=MsoListParagraphCxSpLast style='text-indent:-.25in;mso-list:l8 level1 lfo10'><![if !supportLists]><span
style='font-family:"Courier New";mso-fareast-font-family:"Courier New"'><span
style='mso-list:Ignore'>o<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;
</span></span></span><![endif]><span style='mso-bidi-font-family:Calibri;
mso-bidi-theme-font:minor-latin'>-c Runs BFT file once created in the active
directory (directory running command prompt).<o:p></o:p></span></p>

<p class=MsoNormal>‘bft’ [-b] Example Follows:</p>

<p class=MsoListParagraph style='margin-left:38.7pt;mso-add-space:auto;
text-indent:-.25in;mso-list:l9 level1 lfo7'><![if !supportLists]><span
style='font-family:Symbol;mso-fareast-font-family:Symbol;mso-bidi-font-family:
Symbol'><span style='mso-list:Ignore'>-<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><![endif]>BotWFileGatherTool.exe -b -m <span
style='mso-spacerun:yes'> </span>“Metal, Water” -t “Update\Actor\Pack\TwnObj_TempleOfTime_A_01.sbactorpack
: Update\Model\TwnObj_TempleofTime_A-00.sbfres” -fm “C:\Users\Marcus\Desktop\Mod
Folder” -fs “!Actor, Map_MainField_A-8” -c</p>

<p class=MsoNormal>'createdata' [-cd] Executes BFT file, same as opening a BFT
file with BotWFileGatherTool.exe or drag and dropping onto it. Example Follows:</p>

<p class=MsoListParagraph style='margin-left:38.7pt;mso-add-space:auto;
text-indent:-.25in;mso-list:l9 level1 lfo7'><![if !supportLists]><span
style='font-family:Symbol;mso-fareast-font-family:Symbol;mso-bidi-font-family:
Symbol'><span style='mso-list:Ignore'>-<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><![endif]>BotWFileGatherTool.exe -cd “C:\Users\Marcus\DungeonMod.bft”</p>

<p class=MsoNormal>‘readdata’ [-r] Reads the active directory and tries to
create a basic actor with it. Example Follows:</p>

<p class=MsoListParagraph style='margin-left:38.7pt;mso-add-space:auto;
text-indent:-.25in;mso-list:l9 level1 lfo7'><![if !supportLists]><span
style='font-family:Symbol;mso-fareast-font-family:Symbol;mso-bidi-font-family:
Symbol'><span style='mso-list:Ignore'>-<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><![endif]>BotWFileGatherTool.exe -r</p>

<p class=MsoNormal>‘quick-collision’ [-c] Looks for obj files in the active
directory and extracts a HKRB file from them using <a
href="https://github.com/krenyy/HKX2">HKX2 Tools</a> by <b>Kreny.<o:p></o:p></b></p>

<p class=MsoListParagraph style='margin-left:38.7pt;mso-add-space:auto;
text-indent:-.25in;mso-list:l9 level1 lfo7'><![if !supportLists]><span
style='font-family:Symbol;mso-fareast-font-family:Symbol;mso-bidi-font-family:
Symbol'><span style='mso-list:Ignore'>-<span style='font:7.0pt "Times New Roman"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</span></span></span><![endif]>BotWFileGatherTool.exe -c</p>

<h3><a name="_Folder_Syntax:"></a>Folder Syntax:</h3>

<p class=MsoNormal><o:p>&nbsp;</o:p></p>

<p class=MsoNormal>Folder syntax is a way to define certain folders, i.e., the game
<b>content</b> folder.</p>

<p class=MsoNormal><b><u style='text-underline:thick'>!</u> </b>Marks the <a
href="https://zeldamods.org/wiki/Content">content</a> folder.<b><o:p></o:p></b></p>

<p class=MsoNormal><b><u style='text-underline:thick'>m~</u></b> Marks the <a
href="#_Folder_Syntax:"
title="Materials (BFMAT) will be placed here. If none such folder is marked, materials will be skipped.">Materials</a>
folder.</p>

<p class=MsoNormal><b><u style='text-underline:thick'>s~</u></b> Marks the <a
href="#_Folder_Syntax:"
title="Models (SBFRES) added to the preset file will be placed here. If none such folder is marked, models will be ignored.">Models</a>
folder.</p>

<p class=MsoNormal><b><u style='text-underline:thick'>t~</u></b> Marks the <a
href="#_Folder_Syntax:"
title="Texture Packs (DDS) will be placed here. If none such folder is marked, textures will be ignored.">Textures</a>
folder.<b><u style='text-underline:thick'> </u></b></p>

<p class=MsoNormal><b><u style='text-underline:thick'>a~</u></b> Marks the <a
href="#_Folder_Syntax:"
title="Shader Animatiopns (YAML) will be placed here. If none such folder is marked, shader animations will be skipped.">Shader
Animation</a> folder.<b><u style='text-underline:thick'> <o:p></o:p></u></b></p>

<p class=MsoNormal><i>Note: Syntax will be removed when created with the BFT.<o:p></o:p></i></p>

<h1>Credits</h1>

<p class=MsoNormal><o:p>&nbsp;</o:p></p>

<p class=MsoNormal><b><span style='font-size:12.0pt;line-height:107%;
color:#0D0D0D;mso-themecolor:text1;mso-themetint:242'>Nicene Nerd (Caleb .S) -
Creator of Hyrule-Builder<o:p></o:p></span></b></p>

<p class=MsoNormal><b><span style='font-size:12.0pt;line-height:107%;
color:#0D0D0D;mso-themecolor:text1;mso-themetint:242'>Kreny (</span></b><b><span
style='font-size:12.0pt;line-height:107%;mso-bidi-font-family:"Segoe UI";
color:#0D0D0D;mso-themecolor:text1;mso-themetint:242'>Martin .K</span></b><b><span
style='font-size:12.0pt;line-height:107%;color:#0D0D0D;mso-themecolor:text1;
mso-themetint:242'>) - Creator of HKX2 Tools<o:p></o:p></span></b></p>

<p class=MsoNormal><b><span style='font-size:12.0pt;line-height:107%;
color:#0D0D0D;mso-themecolor:text1;mso-themetint:242'>Arch Leaders (Marcus .S) –
Breath of the Wild: Basic Mod Creator Developer<o:p></o:p></span></b></p>

<p class=MsoNormal><o:p>&nbsp;</o:p></p>

<p class=MsoNormal><o:p>&nbsp;</o:p></p>

<p class=MsoNormal><o:p>&nbsp;</o:p></p>

<p class=MsoNormal><o:p>&nbsp;</o:p></p>

</div>

</body>

</html>
