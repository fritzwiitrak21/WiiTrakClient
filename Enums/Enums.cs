namespace WiiTrakClient.Enums
{
    public enum CartCondition
    {
        Good,
        Damage,
        DamageBeyondRepair
    }

    public enum CartStatus
    {
        InsideGeofence,
        OutsideGeofence,
        PickedUp,
        Lost
    }

    public enum CartOrderedFrom
    {
        Manufacture,
        Seller,
        Lessor
    }

    public enum CartFilterOption {
        All,
        AtStore,
        NeedsPickUp,
        OnTruck,
        Lost
    }
}
