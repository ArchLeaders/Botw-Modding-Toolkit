![image](https://user-images.githubusercontent.com/80713508/147896138-00a83250-333b-4623-be1f-c97fbcc8aa53.png)

## Collision Filter Info

Whatever is checked in this menu won't collide with the object.

I.e. if `NPC` is checked NPC's won't collide with the object.

`Ignore` disables collision completly.

## User Data

### Material

The material defines what sounds and effects will be used when something interacts with the object.

If `Water` is slected, splashes will be made when walking on the object, if `Stone` is selected it will sound as like Link is walking on stone, etc . . .

### Sub-Material 

The sub-material is the type of material.

If the `Stone` material is slected, the `Sub-Material` will have options like `Heavy`, `DgnLight`, `Marble`, etc . . .

### Wall-Code

The wall-code defines what will happen when Link tries to climb the object, if set to `No Climb` like shown in the image, Link will slip of the object.

- **Null** is the dafault and allows Link to climb the object.
- **NoClimb** will make link slip of the object. Shrine Walls have this Wall-Code.
- **Hang** ?
- **LadderUp** Presumably acts like a ladder, I'm unsure what the `Up` means?
- **Ladder** Presumably acts like a ladder?
- **Slip** Always behaves like rainy weather?
- **LadderSlide** ?
- **NoSlipRain** Climable even in the rain?
- **NoDashUpAndNoClimbUp** For water, blocks the Dash up feature?
- **IceMakerBlock** The Wall-Code used for thr Crionis blocks.

### Floor-Code

The Floor-Code defines what will happen when link walks on the object.

- **TODO**



