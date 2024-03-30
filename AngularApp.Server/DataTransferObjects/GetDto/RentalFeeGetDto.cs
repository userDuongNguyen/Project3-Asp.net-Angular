namespace Domain.DataTransferObjects.GetDto
{
    public class RentalFeeGetDto
    {
        public int Id { get; set; }
        public float BasicRent { get; set; }
        public float ExtraCost { get; set; }
        public float Deposit { get; set; }
        public float HeatingCost { get; set; }
    }
}
