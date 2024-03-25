﻿namespace Domain.DataTransferObjects.PutDto
{
    public class ContactMessagePutDto
    {
        public Guid Id { get; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Description { get; set; }
    }
}
