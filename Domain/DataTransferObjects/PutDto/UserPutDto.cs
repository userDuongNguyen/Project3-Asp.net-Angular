namespace Domain.DataTransferObjects.PutDto
{
    public class UserPutDto
    {
        public Guid Id { get; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; }
    }
}
