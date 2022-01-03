using System;
using System.Collections.Generic;

namespace AVStack.MessageCenter.Data.Entities
{
    public class TemplateGroupEntity : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<TemplateEntity> Templates { get; set; } = new HashSet<TemplateEntity>();
    }
}