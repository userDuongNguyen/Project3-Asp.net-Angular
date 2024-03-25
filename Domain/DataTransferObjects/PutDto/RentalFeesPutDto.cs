namespace Domain.DataTransferObjects.PutDto
{
    public class RentalFeesPutDto
    {
        public Guid Id { get; }
        public float BasicRent { get; set; }
        public float ExtraCost { get; set; }
        public float Deposit { get; set; }
        public float HeatingCost { get; set; }
    }
}
