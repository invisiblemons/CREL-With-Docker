using Coravel.Invocable;
using CREL_BE.ApiLayer.Helpers;
using CREL_BE.Filter;
using CREL_BE.Services;

namespace CREL_BE.Invocables;
public class DailyCronJob : IInvocable
{
    private readonly IUnitOfWorkService unitOfWorkService;

    public DailyCronJob(IUnitOfWorkService unitOfWorkService)
    {
        this.unitOfWorkService = unitOfWorkService;
    }

    public async Task Invoke()
    {
        await ContractUpdateAsync();
    }

    // private async Task UpdatePropertyStatusAsync()
    // {
    //     //Console.WriteLine("AAAAA " + DateTime.Now.ToString());
    //     var Contracts = await unitOfWorkService.ContractService.GetDtosWithoutPaging(new ContractFilter()
    //     {
    //         EndDate = DateTime.Now.Date
    //     });
    //     foreach (var contract in Contracts)
    //     {
    //         // if (contract.AppointmentId != null)
    //         // {
    //         //     var appointment = await unitOfWorkService.AppointmentService.GetAppointmentById(contract.AppointmentId.Value);
    //         //     appointment!.Properties.ElementAt(0).Status = 3;
    //         // }
    //         var property = await unitOfWorkService.PropertyService.GetPropertyById(contract.PropertyId);
    //         if (property != null)
    //         {
    //             property.Status = 3;
    //         }
    //     }
    // }

    private async Task ContractUpdateAsync()
    {
        //Console.WriteLine("AAAAA " + DateTime.Now.ToString());
        var contracts = await unitOfWorkService.ContractService.GetDtosWithoutPaging(new ContractFilter()
        {   Status = 2,
            MaxEndDate = DateTime.Now.Date
        });
        foreach (var contract in contracts)
        {
            if (contract.Id != null)
            {
                var realContract = await unitOfWorkService.ContractService.GetContractById(contract.Id.Value);
                if (realContract != null)
                {
                    realContract.Status = 0;
                    realContract.ReasonCancel = "Hết hạn hợp đồng";
                }
            }
        }
        await unitOfWorkService.CommitAsync();
        DevelopHelper.WriteLog(
            @$"Contract Update
            DateTime.Now = {DateTime.Now}
            Contracts = {Newtonsoft.Json.JsonConvert.SerializeObject(contracts)}
            "
        );
    }
}
