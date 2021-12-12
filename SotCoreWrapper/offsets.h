#pragma once
class offsets {
public:
	struct UWorld
	{
		int PersistentLevel = 0x30;
		int OwningGameInstance = 0x1C0;
	}UWorld;

	struct UGameInstance
	{
		int LocalPlayers = 0x38;
	}UGameInstance;

	struct UPlayer
	{
		int PlayerController = 0x30;
	}ULocalPlayer;

	struct ULevel
	{
		int AActor = 0xA0;
	}ULevel;

	struct APlayerController
	{
		int Pawn = 0x3D8;
		int Character = 0x3E8;
		int CameraManager = 0x460;
		int ControlRotation = 0x2D4;
	}APlayerController;

	struct AActor
	{
		int actorId = 0x18;
		int rootComponent = 0x170;
		int PlayerState = 0x3F0;
		int WieldedItemComponent = 0x810;
		int HealthComponent = 0x838;
		int DrowningComponent = 0xCA8;
	}AActor;

	struct APlayerState
	{
		int PlayerName = 0x3D8;
	}APlayerState;

	struct APlayerCameraManager
	{
		int CameraCache = 0x440;
	}APlayerCameraManager;

	struct AItemProxy
	{
		int AItemInfo = 0x5B0;
	}AItemProxy;

	struct AItemInfo
	{
		int UItemDesc = 0x438;
	}AItemInfo;

	struct ABootyItemInfo
	{
		int BootyType = 0x528;
		int Rarity = 0x6A4;
	}ABootyItemInfo;

	struct AShip
	{
		int CrewOwnershipComponent = 0x760;
		int ShipInternalWaterComponent = 0x05E0;
		int ShipOwningActor = 0x3E0;
	}AShip;

	struct ACannon
	{
		int TimePerFire = 0x5A0;
		int ProjectileSpeed = 0x5A4;
		int ProjectileGravityScale = 0x5A8;
		int ServerPitch = 0x754;
	}ACannon;

	struct UCrewOwnershipComponent
	{
		int CrewId = 0xD4;
	}UCrewOwnershipComponent;

	struct AFauna
	{
		int Name = 0x7F8;
	}AFauna;

	struct AMaptTable
	{
		int ServerCenter = 0x4C0;
		int MapPins = 0x4E0;
		int TrackedShips = 0x4F0;
	}AMapTable;

	struct ACrewService
	{
		int Crews = 0x4A8;
	}ACrewService;

	struct UWieldedItemComponent
	{
		int WieldedItem = 0x2B8;
	}UWieldedItemComponent;

	struct AWieldableItem
	{
		int ItemInfo = 0x6F8;
	}AWieldableItem;
};
extern offsets Offsets;