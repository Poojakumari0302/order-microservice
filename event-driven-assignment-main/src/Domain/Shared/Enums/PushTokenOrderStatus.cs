using System;
using System.ComponentModel;

namespace Domain.Shared.Enums
{
    public enum PushTokenOrderStatus
    {
        [Description("new_order")]
        NewOrder,

        [Description("update_order")]
        UpdateOrder,

        [Description("order_enroute")]
        OrderEnroute,

        [Description("cancel_order")]
        CancelOrder,

        [Description("new_order_offline")]
        NewOrderOffline,

        [Description("complete_order")]
        CompleteOrder
    }
}
