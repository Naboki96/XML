using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XML.Models
{
    [XmlRoot(ElementName = "Employees")]
    public class Employee
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "Surname")]
        public string Surname { get; set; }
        [XmlAttribute(AttributeName = "Login")]
        public string Login { get; set; }
    }

    [XmlRoot(ElementName = "EmployeesList")]
    public class EmployeesList
    {
        [XmlElement(ElementName = "Employee")]
        public List<Employee> Employee { get; set; }
    }
}
