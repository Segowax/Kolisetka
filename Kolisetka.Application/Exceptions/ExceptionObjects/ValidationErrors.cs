using System.Collections.Generic;

namespace Kolisetka.Application.Exceptions.ExceptionObjects
{
    public class ValidationErrors
    {
        public List<string> Errors { get; set; } = new List<string>();
    }
}
