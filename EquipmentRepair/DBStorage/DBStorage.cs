using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentRepair.DBStorage
{
    public static class DBStorage
    {
        public static EquipmentRepairDBEntities DB_s { get; set; } = new EquipmentRepairDBEntities();
    }
}
