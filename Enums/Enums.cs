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

    public enum CartFilterOption
    {
        All,
        AtStore,
        NeedsPickUp,
        OnTruck,
        Lost
    }


    public enum Role
    {
        WiiTrak = 1,
        Corporate = 2,
        PrimeCompany = 3,
        SubContractor = 4,
        Driver = 5,
        Store = 6,
        ServiceProvider = 7,
        Technician = 8

    }
}
