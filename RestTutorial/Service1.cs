using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Collections;
using System.Xml.Linq;

namespace RestTutorial
{
    public class Service1 : IService1
    {
        [WebInvoke(Method = "PUT",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "update/")]
        public details[] UpdateData(details data)
        {
            XDocument doc = XDocument.Load(@"E:\Tutorial\details.xml");

            var newelement = new XElement("Details", new XElement("Firstname", data.Firstname), new XElement("Lastname", data.Lastname), new XElement("Email", data.Email), new XElement("Mobile", data.Mobile));

            doc.Element("DocumentElement").Add(newelement);

            doc.Save(@"E:\Tutorial\details.xml");
            details[] set = SendData();
            return set;
     
      
            //return new details[]
            //{
            //    new details
            //    {
            //        Firstname = data.Firstname,
            //        Lastname = data.Lastname,
            //        Mobile = data.Mobile,
            //        Email = data.Email
            //    },
            //    new details
            //    {
            //        Firstname = "Dipesh",
            //        Lastname = "Singh",
            //        Mobile = 8056184186,
            //        Email = "dipesh.singh2012@gmail.com"
            //    },
            //    new details
            //    {
            //        Firstname = "Ajitha",
            //        Lastname = "Sekar",
            //        Mobile = 9003482003,
            //        Email = "sekaraji1992@gmail.com"
            //    }
            //};

        }
        [WebInvoke(Method = "GET",
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "getdata/")]
        public details[] SendData()
        {
            try
            {
                XDocument doc = XDocument.Load(@"E:\Tutorial\details.xml");

                //var y = doc.ToString();
                //var z = XDocument.Parse(y);

                IEnumerable <XElement> qdata = (from result in doc.Descendants("DocumentElement").Descendants("Details") select result);

                int length = qdata.Count();
                details[] data = new details[length];
                int i = 0;

                foreach(var x in qdata)
                {
                    data[i] = new details();
                    data[i].Firstname = x.Element("Firstname").Value;
                    data[i].Lastname = x.Element("Lastname").Value;
                    data[i].Email = x.Element("Email").Value;
                    data[i].Mobile = Convert.ToDouble(x.Element("Mobile").Value);
                    i++;
                }
                return data;
            }
            catch(Exception xe)
            {
                throw new FaultException<string>
                    (xe.Message);
            }

            /*return new details[]
            {
                new details
                {
                    Firstname = "Dipesh",
                    Lastname = "Singh",
                    Mobile = 8056184186,
                    Email = "dipesh.singh2012@gmail.com"
                },
                new details
                {
                    Firstname = "Ajitha",
                    Lastname = "Sekar",
                    Mobile = 9003482003,
                    Email = "sekaraji1992@gmail.com"
                }
            };*/  
        }
    }

    public class details
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public double Mobile { get; set; }
        public string Email { get; set; }
    }

}
