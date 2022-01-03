using System;

namespace AVStack.MessageCenter.Data.Entities
{
    public class TemplateEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Body { get; set; }

        public Guid? TemplateGroupId { get; set; }
        public virtual TemplateGroupEntity TemplateGroup { get; set; }
    }
}