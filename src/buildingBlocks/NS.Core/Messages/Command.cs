using FluentValidation.Results;
using System;

namespace NS.Core.Messages
{
    public abstract class Command : Message
    {
        public DateTime TimeStamp { get; private set; }

        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            TimeStamp = DateTime.Now;
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}