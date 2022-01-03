# MTK - Actor Namimg Rules
> [Prefix]()\_**Name**\_[Type]()\_[Index]()

When using MTK to generate an actor, the name is collected from the `.hkrb` file name.

But not just the file name in some cases, if the file name has no actor [prefix](https://github.com/ArchLeaders/Botw-Modding-Toolkit/new/master/Docs/Info#prefixes) `FldObj`
will be used by dafault.

For example, if you convert a file named `ActorName_03.hkrb` MTK will output an actor named `FldObj_ActorName_A_03`.

### Prefixes

- FldObj - **F**e**ld** **Obj**ect.
- TwnObj - **T**o**wn** **Obj**ect.
- DgnObj - **D**un**g**eo**n** **Obj**ect.
- DLC_DgnObj - **D**own**l**oadable **C**ontent **D**un**g**eo**n** **Obj**ect. (Prefix for the TotS actors.)

### Types

Just any A-Z letter, this is generally used to define different types of an actor.

### Indexes

This can be any number from 01-99 (you can probably go higher too), this defines the actor type index.

For example `TwnObj_TempleOfTime_Gate_B_02` has an index of `02` and is type `B`.
