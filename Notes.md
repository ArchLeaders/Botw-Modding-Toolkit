Basic-Mod-Creator
=================

Actor Class
-------------

```yaml
actor_create : File
```
**Options**

```yaml
Out Folder
Name
Switch/WiiU (Default set on install)
Actorinfo //To be merged with
```

> > > --- 

Mod Class
---------

```cs
Mod.Create(string name, string[] files);
Mod.Delete(string fullName);
```

> > > --- 

Bake / Actor Extract (Silent Princess)
-------------------------------

```yaml
extract : Arguments
```

> > > --- 

Myst Editor
-----------

```yaml
Easy to read text editing.
```

--- 

Tools
=====

Hyrule-Builder
--------------

```yaml
Unbuilding : Folder
Rebuilding : Folder
SARC Unpacking : File (SBACTORPACK)
SARC Packing : Folder
rstb_to_json : File
```

**Options**

```yaml
Convert to switch
```

> > > --- 

BYML
----

```yaml
Convertion to YAML : File (BYML)
Convertion from YAML to BYML : File (YML)
```

**Options**

```yaml
Change endian Type
Yaz0 Compressed
BYML Type
```

> > > --- 

Botw-Havok
----------

```yaml
hk_to_json : File (HK)
json_to_hk : File (JSON)
hk_compare : Folder, Files (HK)
hkrb_extract : Name (Actor)
hksc_to_hkrb : File (HKSC)
```

**Options**

```yaml
Extract collision by ActorName : Unnessasry when SP comes out.
```

> > > --- 

Yaz-It
------

```yaml
un-yazit : File (Any)
yazit : File (Any)
```

**Options**

```yaml
Yaz0 level
```

> > > --- 

Bars-Tool
---------

```yaml
bars_extractor : File (BARS)
bars_injector : Folder
```

**Options**

```yaml
Extract all bars
```

> > > --- 

Audio-Tools
-----------

```yaml
bfwav_to_wav
bfstp-generator
bcfstm-bcfwav-converter
```

**Options**

```yaml
Change endian type
```

> > > --- 

HKX2
----

```yaml
CreateCollisionAndNavmesh : File
BakeTool : Arguments
```

**Options**

```yaml
HK type
```

> > > --- 

HKLC
----

_null_

> > > --- 

Botw Flag Utility
-----------------

```yaml
generate : Arguments
remove : Arguments
find : Arguments
```

FBX-Converter
-------------

```
Various : File
```

**Options**

```yaml
Out file
```

> > > --- 

Resources
---------

```yaml
Empty textures
Materials
```

> > > --- 

Installers
----------

```yaml
Standalone-Item-Creator
Botw-File-Finder
Ice-Spears //Additional setup optional
Switch-Toolbox //Locke's optional
Looping Audio Converter
Botw-Editor
```

**Installer Only**

```yaml
FBX-Converter
Audacity
Visual-Studio Code
DS4-Windows
Cheat-Engine
```

**PIP**

```yaml
Botw-Actor-Tool
Event-Editor
Myst
WildBits
```

**Required**

```yaml
Ausio-Tools
Bars-Tool
BCML
Botw-havok
BYML
HKLC
HKX2
Hyrule-Builder
Yaz-It
```

**Required To Run**

```yaml
Visual C++ Redist
Python 3.7.9 (x64)
```

> > > --- 

Quick Convert File Extensions
-------------------------

**HKX2**

```yaml
.obj : Converts to HKRB
.sc.obj : Converts to HKSC
.ssc.obj : Converts to SHKSC
.nm.obj : Converts to HKNM2
.snm.obj : Converts to SHKNM2
```

**SARC**

```yaml
.act.sbactorpack :  Unpacks SARC. 'sbactorpack' can be any SARC extension.
```

**SARC-Unpacker**

```yaml
.act : Creates a actor
.mod : Creates a mod template named after the file name (no extension)
```

**FBX-Converter**

```yaml
.dea : Converts to FBX
```

