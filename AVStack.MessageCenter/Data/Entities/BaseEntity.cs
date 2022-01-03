using System;
using System.Text.Json;

namespace AVStack.MessageCenter.Data.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public JsonDocument Json { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime TimeStamp { get; set; }

        public void Dispose() => Json?.Dispose();
    }
}