using System.Threading.Tasks;
using FieldNationApp.App_Code;
using FieldNationApp.FieldNationSoapService;
using System.Collections.Generic;

namespace FieldNationApp.Models
{
    public partial class WorkOrder
    {
        public async Task<getWorkOrderRequestsResponse> GetWorkorderRequests(int n)
        {
            var client = new SoapHandlerPortTypeClient();
            Login login = FieldNationLoginFactory.GetLogin();

            return await client.getWorkOrderRequestsAsync(login, n);
        }

        public async Task<createWorkOrderResponse> CreateOnFieldNation()
        {
            var client = new SoapHandlerPortTypeClient();

            Login login = FieldNationLoginFactory.GetLogin();
            FieldNationSoapService.WorkOrder wo = new FieldNationSoapService.WorkOrder();

            wo.description = new ServiceDescription();
            wo.description.category = 1; // THIS HAS TO BE A DEFINED CATEGORY IN FN SEE https://api.fieldnation.com/docs/soap/types ServiceDescription
            wo.description.description = this.Description;
            wo.description.title = this.Title;
            wo.description.instructions = this.Instructions;
            wo.startTime = new TimeRange();
            wo.startTime.timeBegin = this.StartTime;
            wo.startTime.timeEnd = this.EndTime;
            wo.location = new ServiceLocation();
            wo.location.address1 = this.WorkOrderLocation.Address;
            wo.location.city = this.WorkOrderLocation.City;
            wo.location.state = this.WorkOrderLocation.State;
            wo.location.zip = this.WorkOrderLocation.ZipCode;
            wo.payInfo = new PayInfo();
            wo.payInfo.@fixed = new PayInfoFixed();
            wo.payInfo.@fixed.amount = (float)this.PaymentInformation.Amount;
            wo.payInfo.perHour = new PayInfoRate();
            wo.payInfo.perHour.rate = (float)this.PaymentInformation.Amount;
            wo.payInfo.perHour.maxUnits = this.PaymentInformation.MaxHours;

            // Fields that don't exist in our app, but FN needs to know about
            wo.additionalFields = new AdditionalField[] { };
            wo.alertWhenPublished = true;
            wo.closeoutReqs = new CloseoutReq[] { };
            wo.labels = new Label[] { };
            wo.printLink = true;
            wo.techUploads = true;

            return await client.createWorkOrderAsync(login, wo, false);
        }
    }
}