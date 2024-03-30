using System.ComponentModel;

namespace Domain.SeedWork
{

    public enum AccommodationType
    {
        [Description("Basement")]
        Basement,

        [Description("Ground Floor")]
        GroundFloor,

        [Description("Mezzanine")]
        Mezzanine,

        [Description("Apartment")]
        Apartment,

        [Description("Loft")]
        Loft,

        [Description("Duplex")]
        Duplex,

        [Description("Terrace Apartment")]
        TerraceApartment,

        [Description("Penthouse")]
        Penthouse,

        [Description("Attic")]
        Attic,

        [Description("Other")]
        Other
    }
    public enum AccountTypes
    {
        Basic,
        Plus,
        Gold,
        Agent

    }
    public enum AgentRole
    {
        None,
        Brokerage,
        Manager,
    }
    public enum EnergyEfficiencyClass
    {
        [Description("A+")]
        c1 = 1,
        [Description("A")]
        c2 = 2,
        [Description("B")]
        c3 = 3,
        [Description("C")]
        c4 = 4,
        [Description("D")]
        c5 = 5,
        [Description("E")]
        c6 = 6,
        [Description("F")]
        c7 = 7,
        [Description("G")]
        c8 = 8,
        [Description("H")]
        c9 = 9
    }
    public enum Facility
    {
        Simple,
        Normal,
        Upscale,
        Luxury
    }
    public enum HeatingType
    {
        None,
        HeatPowerPlant,
        Electric,
        Floor,
        District,
        Underfloor,
        Gas,
        WoodPellet,
        NightStorage,
        Stove,
        Oil,
        Solar,
        HeatPump,
        Central
    }
    public enum InternetSpeed
    {
        AllType,
        min100Mbit,
        min250Mbit,
        min1000Mbit,

    }
    public enum LivingDetail
    {
        EquippedKitchen,
        Balcony,
        Garden,
        Basement,
        SteplessAccess,
        Evelator,
        GuestToilet,
        SharedApartment,
        HousingPermit
    }
    public enum ParkingType
    {
        Outdoor,
        Carport,
        Duplex,
        Garage,
        ParkingHouse,
        Underground
    }
    public enum PetAllowance
    {
        Allow,
        NotAllow,
        NeedToBeAsked,
    }
    public enum PropertyType
    {
        RentalApartments,
        RentHouses,
        LeaseLand,
        GaragesAndParkingSpaces,
        TemporaryLiving,
        SharedFlats,
        SeniorLiving,
        NursingHomes,
        Condominiums,
        HouseForSale,
        LandForPurchase,
        GaragesAndParkingSpacesForSale,
        InvestmentObjects,
        Foreclosures,
        PrefabricatedAndSolidHouse,

    }
    public enum ResidenceCertificate
    {
        All,
        Required,
        No,
    }
    public enum Salutation
    {
        Mr,
        Mrs
    }
    public enum SubcriptionPack
    {
        SellerPlus,
        RenterPlus,
        FinderPlus
    }


}
