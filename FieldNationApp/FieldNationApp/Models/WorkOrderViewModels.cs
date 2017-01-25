using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace FieldNationApp.Models
{
    public partial class WorkOrder
    {
        public WorkOrder()
        {
            this.PaymentInformation = new PaymentInformation();
        }
        public int WorkOrderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public int WorkOrderLocationId { get; set; }
        public virtual WorkOrderLocation WorkOrderLocation { get; set; }
        public virtual PaymentInformation PaymentInformation { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class WorkOrderViewModel
    {
        public WorkOrder WorkOrder { get; set; }
        public IEnumerable<SelectListItem> WorkOrderLocationItems { get; set; }
    }

    public class WorkOrderLocation
    {
        public int WorkOrderLocationId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
    }

    public enum PaymentType
    {
        Fixed,
        Blended
    }

    public class PaymentInformation
    {
        public PaymentType PaymentType { get; set; }
        public decimal Amount { get; set; }
        public float ExpectedHours { get; set; }
        public float MaxHours { get; set; }
    }
}