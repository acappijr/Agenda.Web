namespace Agenda.Application.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}


// https://www.red-gate.com/simple-talk/databases/sql-server/learn/when-use-char-varchar-varcharmax/#:~:text=The%20fundamental%20difference%20between%20CHAR,both%20can%20store%20alphanumeric%20data.