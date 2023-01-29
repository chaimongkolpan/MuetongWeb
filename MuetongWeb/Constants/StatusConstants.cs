using System;
namespace MuetongWeb.Constants
{
	public static class StatusConstants
	{
        public static string PrWaitingApprove = "รอตรวจสอบสั่งสินค้า";
		public static string PrRequested = "สั่งสินค้า";
        public static string PrWaitingOrder = "รอสั่งซื้อ";
        public static string PrWaitingTransfer = "รอสินค้าจัดส่ง";
        public static string PrComplete = "จัดส่งสำเร็จ";
        public static string PrCancel = "ยกเลิก";

        public static string PrDetailWaitingApprove = "รอตรวจสอบสั่งสินค้า";
        public static string PrDetailRequested = "สั่งสินค้า";
        public static string PrDetailWaitingOrder = "รอสั่งซื้อ";
        public static string PrDetailWaitingTransfer = "รอสินค้าจัดส่ง";
        public static string PrDetailComplete = "จัดส่งสำเร็จ";

        public static string PoWaitingApprove = "รอตรวจสอบสั่งซื้อ";
        public static string PoRequested = "รอสินค้าจัดส่ง";
        public static string PoComplete = "จัดส่งสำเร็จ";
        public static string PoCancel = "ยกเลิก";

        public static string BillingWaitingPayment = "รอชำระเงิน";
        public static string BillingWaitingReceipt = "รอรับใบเสร็จ/ใบกำกับภาษี";
        public static string BillingWaitingApprove = "รอตรวจสอบวางบิล";
        public static string BillingComplete = "วางบิลสำเร็จ";
        public static string BillingCancel = "ยกเลิก";
    }
}

