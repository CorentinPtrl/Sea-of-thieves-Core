
# 🏴‍☠️ Sea of Thieves - External C# Library
Sea of thieves External C# Library, based on [
SoT External Tool Premium](https://github.com/xTeJk/SoT_External_Tool_Premium)

## How to Use

#### Get all actors

```C#
  SotCore core = new SotCore();
  if(core.Prepare())
  {
    SotLevel level = new SotLevel();
    UE4ActorWrapper[] actors = level.getActors();
  }
  else
  {
  //Failed, Sea of thieves not detected
  }
```

