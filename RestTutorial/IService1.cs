using System.IO;
using System.ServiceModel;

namespace RestTutorial
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        details[] UpdateData(details data);

        [OperationContract]
        details[] SendData();
    }


}