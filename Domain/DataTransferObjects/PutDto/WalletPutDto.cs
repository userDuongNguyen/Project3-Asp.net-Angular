namespace Domain.DataTransferObjects.PutDto
{
    public class WalletPutDto
    {
        public Guid Id { get; }
        public float Balance { get; set; }
    }
}
