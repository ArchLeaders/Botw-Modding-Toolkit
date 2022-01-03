# Texture Formats and Compression for BotW
> _I recomend the topmost option, but read the description to find you needs. Every option will work when handled correctly._
> 
> _PS: I don't know much about texture compression._

## Albedo (Color texture.) | Alb

Supported Format(s):
- `BC1_UNorm` | Good for textures with no Alpha (Transparent) channel.
- `BC1_SRGB` | Same compression as `BC1_UNorm` but it includes the Alpha channel.

## Normal (Bump mapping.) | Nrm

Supported Format(s):
- `BC5_UNorm`
- `BC1_UNorm`

_Any UNorm or SNorm should work._

## Specular (Shine mapping.) | Spm

Supported Format(s):
- `BC5_SNorm`
- `BC4_SNorm`
- `BC1_SNorm`

_Any UNorm or SNorm should work._

## Emmision (Emmision mapping.) | Emm

Supported Format(s):
- `BC5_SNorm`
- `BC4_SNorm`
- `BC1_SNorm`

_Any UNorm or SNorm should work._

## Abient Occlusion (AO Mapping) | Ao

Supported Format(s):
- `BC5_SNorm`
- `BC4_SNorm`
- `BC1_SNorm`

_Any UNorm or SNorm should work._
