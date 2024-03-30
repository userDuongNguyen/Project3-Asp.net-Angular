namespace Domain.DataTransferObjects.PutDto
{
    public class RentalFeePutDto
    {
        public int Id { get; }
        public float BasicRent { get; set; }
        public float ExtraCost { get; set; }
        public float Deposit { get; set; }
        public float HeatingCost { get; set; }
    }
}
