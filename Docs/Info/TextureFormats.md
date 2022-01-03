# Texture Formats and Compression for BotW
> _I recomend the topmost option, but read the description to find you needs. Every option will work when handled correctly._
> 
> _PS: I don't know much about texture compression._

## Albedo (Color Texture) | Alb

Sampler: `_a`

Supported Format(s):
- `BC1_UNorm` | Good for textures with no Alpha (Transparent) channel.
- `BC1_SRGB` | Same compression as `BC1_UNorm` but it includes the Alpha channel.

## Normal (Bump Mapping) | Nrm

Sampler: `_n`

Supported Format(s):
- `BC5_UNorm`
- `BC1_UNorm`

_Any UNorm or SNorm should work._

## Specular (Shine Mapping) | Spm

Sampler: `_s`

Supported Format(s):
- `BC5_SNorm`
- `BC4_SNorm`
- `BC1_SNorm`

_Any UNorm or SNorm should work._

## Emmision (Emmision Mapping) | Emm

Sampler: `_e`

Supported Format(s):
- `BC5_SNorm`
- `BC4_SNorm`
- `BC1_SNorm`

_Any UNorm or SNorm should work._

## Abient Occlusion (AO Mapping) | Ao

Sampler: `_ao`

Supported Format(s):
- `BC5_SNorm`
- `BC4_SNorm`
- `BC1_SNorm`

_Any UNorm or SNorm should work._

## Metalness (Metalic Mapping) | Mtl

Sampler: `_mtl`

Supported Format(s):
- `BC5_SNorm`
- `BC4_SNorm`
- `BC1_SNorm`

_Any UNorm or SNorm should work._
