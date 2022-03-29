
# 🏴‍☠️ Sea of Thieves - External C# Library
Sea of thieves External C# Library, based on [
RemnantESP](https://github.com/shalzuth/RemnantESP)

#### And if you want some help feel free to join the [discord](https://discord.gg/KkBVKCFdzz)

## How to Use

#### Get all actors

```C#
SotCore core = new SotCore();
//if u are in steam version replace by true
if (core.Prepare(false))
{
    UE4Actor[] actors = core.GetActors();
    foreach (UE4Actor actor in actors)
    {
	//Do your things here
    }
}
else
{
  //Failed, Sea of thieves not detected
}
```

#### Get LocalPlayer

```C#
SotCore core = new SotCore();
//if u are in steam version replace by true
if (core.Prepare(false))
{
    Player localPlayer = core.LocalPlayer;
    //Do your things here
}
else
{
    //Failed, Sea of thieves not detected
}
```

#### Use Player class With Actor
All players name are "BP_PlayerPirate_C"
```C#
Player player = new Player(actor);
```

#### Get Camera Manager

```C#
SotCore core = new SotCore();
//if u are in steam version replace by true
if (core.Prepare(false))
{
    CameraManager cameraManager = core.CameraManager;
    //Do your things here
}
else
{
    //Failed, Sea of thieves not detected
}
```
#### Crew Service
```C#
 foreach(Crew crew in core.Crews)
{        
	foreach(Player player in crew.PreProcessedPlayers)
	{
		//Do your things here
	}
}
```

#### Island Service
```C#
foreach (Island island in core.Islands)
{        
	//Do your things here
}
```


## Credits
DougTheDruid for [SoT-Actor-Names](https://github.com/DougTheDruid/SoT-Actor-Names)
