using System.ComponentModel;

namespace Application.Common.Helpers.Exceptions
{
    public enum BusinessExceptionType
    {
        [Description("Uncontrolled business exception")]
        UncontrolledBusinessException = 001,
    }
}
