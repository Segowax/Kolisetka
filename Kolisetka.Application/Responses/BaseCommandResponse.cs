using System.Collections.Generic;

namespace Kolisetka.Application.Responses
{
    public  class BaseCommandResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
