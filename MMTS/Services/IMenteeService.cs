using System.ServiceModel;
using MMTS.Models;


namespace MMTS.Services
{
    [ServiceContract]
    public interface IMenteeService
    {
        [OperationContract]
        string GetMenteeInfo(int id);
    }
}