# hammer-procedural-terrain
A tool for generating procedurally generated displacements using Perlin Noise

## Preview
![Alt](https://grust.co/new/cdn/hammer-procgen.gif)

## How to use
1. Download the latest version from the [Releases](https://github.com/Down-s/hammer-procedural-terrain/releases/tag/Release) tab
2. Open the .exe file
3. Enter your desired parameters, they are all explained in the section below
4. Open output.vmf in hammer
5. Enjoy!

## Parameters
### Displacement Size [Default: 1024]
How big each displacement will be, the lower the value, the more detailed your terrain will be.
I don't suggest making this below 512 if you are filling your map up, unless you have a supercomputer.

### Size Squared [Default: 5]
How many displacements to create outwards from the center. If you set this to the max value it will fill the entire map with displacements.

### Amplitude [Default: 512]
How many units the height of the displacements should be amplified, in other terms it's just the maximum height the terrain can be.
If you set this too low you will just see flat terrain, and if you set it too high you are going to have a terrain made of spikes.

### Scale [Default: 2]
How much the Perlin noise will be scaled. The higher the number, the more spaced out mountains will be.
If you set it too low, your terrain will be jagged, and if you set it too high everything will be pretty flat.

### Octaves [Default: 6]
The amount of layers of perlin noise to be added to the terrain.
The terrain will look more defined the higher this is, but the generation gets slow very quickly the higher the number is.

### Seed [Default: 0]
The seed used for generating the terrain, if you use the same seed on 2 generations, the terrain will be the same (assuming you use the same parameters)
It doesn't matter what you use here, just use some random number you can think of.
