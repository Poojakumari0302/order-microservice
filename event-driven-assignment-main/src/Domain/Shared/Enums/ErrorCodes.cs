using System.ComponentModel;

namespace Domain.Shared.Enums
{
    public enum ErrorCodes
    {

        [Description("Domain Error")]
        Domain,

        [Description("Duplicate Error")]
        DuplicateEntity,

        [Description("Not Found Error")]
        EntityNotFound,

        [Description("Entity State Error")]
        EntityStateTransition,

        [Description("Validation Error")]
        Validation,

        [Description("Device Error")]
        Device = 40100,

        [Description("Order Error")]
        OrderDetailsMissing,
    }
}