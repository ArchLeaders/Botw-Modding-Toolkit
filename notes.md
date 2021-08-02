# BMC Command Notes

_[Basic Mod Creator Installer](//download_link)_

Extract Actor
-----------------

Duplicates a vanilla actor and adds collision to the duplicate actor

> Uses Hyrule-Builder by NiceneNerd & HKRB_Extract by Kreny

Syntax:
```
extract_actor ActorName [path\to\out\File/Folder/bcml_mod] [-c, --cache]
```
_If File or Folder is a folder, a mod folder struture will be created in that folder. bcml_mod will create a mod in bcml's mods directory and place the new actor there._

Silent Princess Syntax:
```
sp_cs -e HashID ActorName Field "path\to\update/dlc(0010)\content" "path\to\out\File or Folder" [path\to\update\content]
```

---

Create Actor
------------

Creates an new actor with optional collision and model link.

> *Uses Hyrule-Builder by NiceneNerd & HKX2 by Kreny*

Syntax:
```
create_actor actorName [read folder] [collision file] [model name] [unit name]
```

_Any defined file will take priority over anything found in the read folder._

From file:
```
actorName.act
  --> Absolute\Path\To\Collision.obj
  --> Unit-Name //By default the file name with "_##" removed from the end where applicable.
  --> Out-Folder (default .act folder)
  --> /bcml=Mod Name
```
_If no parameters are defined inside the file, the process will look in the .act root folder for various files. 
If \*.obj is found collision will be added unless the obj is invalid.
If \*.sbfres is found a new model link will be added using the .sbfres file name._

_Output: A new sbactorpack file in the directory running cmd, .act root folder, defined output._

_If the output is a folder a new mod structure will be created._

---

Byml Switching
--------------

Switches BYML endian types and yaz0 compression/uncompression.

> _Uses BYML by Leoetlino & Yaz-It by NiceneNerd_

Syntax:
```
byml_switch bymlFile [yaz0 compression level] [-b --big_endian] [out file] [-o, -open_out]
```
_Yaz0 compression level can be any number from 1 to 9._

From file: _Reads file endian and switches it._
```
file.[yaz0 level].[format].byml
```

> Actorinfo.product.yml.sbyml would be converted to a readable YML file.

_For Byml files, \[yaz0 level\] can't be combined with \[yml\]._
```
file.[yaz0 level].[format].yml
```

> Actorinfo.product.9.sbyml.yml would be converted to a Yaz0 compressed BYML file. 
>
> If the \[format\] starts with 's' it will be Yaz0 compressed at level 7 unless a different Yaz0 level is defined.

---

BotW Video Encoder
------------------

Encodes BotW's movie files to the correct format. Works for WiiU/Cemu and Switch/Yuzu.

> Uses FFMPEG

Syntax:
```
botw_movie [file.mp4] [-s, --switch]
```
From File:
```
file.mp4
```
_Would be converted to WiiU mp4 format._
```
file.webm
```
_Would be converted to Switch webm format._

---

Last Updated 7/31/2021
----------------------
