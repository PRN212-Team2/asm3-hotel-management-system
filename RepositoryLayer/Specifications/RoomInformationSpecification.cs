using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Specifications
{
    public class RoomInformationSpecification : BaseSpecification<RoomInformation>
    {
        public RoomInformationSpecification()
        {
            AddInclude(r => r.RoomType);
        }

        public RoomInformationSpecification(bool status) : base(x => x.RoomStatus == (status ? 1 : 0))
        {
            AddInclude(r => r.RoomType);
        }

        public RoomInformationSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(r => r.RoomType);
        }

        public RoomInformationSpecification(string roomNumber) : base(x => x.RoomNumber == roomNumber) 
        {
        }
    }
}
