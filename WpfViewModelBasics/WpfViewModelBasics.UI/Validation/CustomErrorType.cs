using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfViewModelBasics.UI.Validation
{
    public class CustomErrorType
    {
        public CustomErrorType(string validationMessage, Severity severity)
        {
            this.ValidationMessage = validationMessage;
            this.Severity = severity;
        }

        public string ValidationMessage { get; private set; }
        public Severity Severity { get; private set; }
    }

    public enum Severity
    {
        WARNING,
        ERROR
    }
}
