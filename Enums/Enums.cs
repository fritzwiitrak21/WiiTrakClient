namespace WiiTrakClient.Enums
{
    public enum AssetCondition
    {
        Good,
        Damage,
        DamageBeyondRepair
    }

    public enum AssetStatus
    {
        InsideGeofence,
        OutsideGeofence,
        PickedUp,
        Lost
    }

    public enum AssetOrderedFrom
    {
        Manufacture,
        Seller,
        Lessor
    }

    public enum AssetFilterOption {
        All,
        AtStore,
        NeedsPickUp,
        OnTruck,
        Lost
    }
}
